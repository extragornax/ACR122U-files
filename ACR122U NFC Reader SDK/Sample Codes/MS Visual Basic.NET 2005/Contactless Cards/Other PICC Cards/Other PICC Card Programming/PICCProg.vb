'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   transact with other PICC cards using ACR122
'
'  File   :         PICCProg.vb             
' 
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         July 28, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'=========================================================================================*/

Public Class PICCCards

    Public retCode, hContext, hCard, Protocol As Long
    Public connActive, autoDet, validATS As Boolean
    Public SendBuff(262), RecvBuff(262) As Byte
    Public SendLen, RecvLen, nBytesRet, reqType, Aprotocol As Integer
    Public dwProtocol As Integer
    Public cbPciLength As Integer
    Public dwState, dwActProtocol As Long
    Public pioSendRequest, pioRecvRequest As SCARD_IO_REQUEST

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        validATS = False
        cbReader.Items.Clear()
        cbReader.Text = ""
        mMsg.Clear()
        displayOut(0, 0, "Program ready")
        bConnect.Enabled = False
        bInit.Enabled = True
        bReset.Enabled = False
        cbIso14443A.Checked = False
        gbGetData.Enabled = False
        tCLA.Text = ""
        tINS.Text = ""
        tP1.Text = ""
        tP2.Text = ""
        tLc.Text = ""
        tLe.Text = ""
        tData.Text = ""
        gbSendApdu.Enabled = False

    End Sub

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bReset.Enabled = True
        bClear.Enabled = True

    End Sub

    Private Sub ClearBuffers()

        Dim indx As Long

        For indx = 0 To 262

            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0

        Next indx

    End Sub

    Private Sub displayOut(ByVal errType As Integer, ByVal retVal As Integer, ByVal PrintText As String)

        Select Case errType

            Case 0
                mMsg.SelectionColor = Drawing.Color.Green
            Case 1
                mMsg.SelectionColor = Drawing.Color.Red
                PrintText = ModWinsCard.GetScardErrMsg(retVal)

            Case 2
                mMsg.SelectionColor = Drawing.Color.Black
                PrintText = "<" + PrintText
            Case 3
                mMsg.SelectionColor = Drawing.Color.Black
                PrintText = ">" + PrintText
            Case 4
                mMsg.SelectionColor = Drawing.Color.Red
                PrintText = ">" + PrintText

        End Select

        mMsg.SelectedText = PrintText & vbCrLf
        mMsg.SelectionColor = Drawing.Color.Black
        mMsg.SelectionStart = mMsg.TextLength
        mMsg.Focus()

    End Sub

    Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)

        Dim sTemp As String
        Dim indx As Integer

        indx = 1
        sTemp = ""
        Ctrl.Items.Clear()

        While (Mid(ReaderList, indx, 1) <> vbNullChar)

            While (Mid(ReaderList, indx, 1) <> vbNullChar)
                sTemp = sTemp + Mid(ReaderList, indx, 1)
                indx = indx + 1
            End While

            indx = indx + 1

            Ctrl.Items.Add(sTemp)

            sTemp = ""
        End While

    End Sub

    Private Sub bInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bInit.Click

        Dim ReaderCount As Integer
        Dim ctr As Integer
        Dim sReaderList As String


        For ctr = 0 To 255
            sReaderList = sReaderList + vbNullChar
        Next

        ReaderCount = 255

        ' 1. Establish context and obtain hContext handle
        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Sub

        End If

        ' 2. List PC/SC card readers installed in the system
        retCode = ModWinsCard.SCardListReaders(hContext, "", sReaderList, ReaderCount)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

            Exit Sub

        End If

        ' Load Available Readers
        Call LoadListToControl(cbReader, sReaderList)
        cbReader.SelectedIndex = 0
        Call EnableButtons()

    End Sub

    Private Sub bConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bConnect.Click

        ' Connect to selected reader using hContext handle and obtain hCard handle
        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        ' Shared Connection
        retCode = ModWinsCard.SCardConnect(hContext, cbReader.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub


        Else

            Call displayOut(0, 0, "Successful connection to " & cbReader.Text)

        End If

        connActive = True
        gbGetData.Enabled = True
        gbSendApdu.Enabled = True
        tCLA.Focus()

    End Sub

    Private Sub bClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bClear.Click

        mMsg.Clear()

    End Sub

    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click

        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        retCode = ModWinsCard.SCardReleaseContext(hCard)
        Call InitMenu()

    End Sub

    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click


        ' terminate the application
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        End

    End Sub

    Private Sub bGetData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetData.Click

        Dim tmpStr As String
        Dim indx As Integer


        validATS = False
        Call ClearBuffers()
        SendBuff(0) = &HFF                              ' CLA
        SendBuff(1) = &HCA                              ' INS

        If cbIso14443A.Checked Then

            SendBuff(2) = &H1                           ' P1 : ISO 14443 A Card

        Else

            SendBuff(2) = &H0                           ' P1 : Other cards

        End If

        SendBuff(3) = &H0                               ' P2
        SendBuff(4) = &H0                               ' Le : Full Length

        SendLen = SendBuff(4) + 5
        RecvLen = &HFF

        retCode = SendAPDUandDisplay(3)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If


        ' Interpret and display return values
        If validATS Then

            If cbIso14443A.Checked Then

                tmpStr = "UID: "

            End If


            For indx = 0 To (RecvLen - 3)

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            displayOut(3, 0, tmpStr.Trim)

        End If

    End Sub


    Private Function SendAPDUandDisplay(ByVal reqType As Integer) As Integer

        Dim indx As Integer
        Dim tmpStr As String

        pioSendRequest.dwProtocol = 2 '2Aprotocol
        pioSendRequest.cbPciLength = Len(pioSendRequest)

        ' Display Apdu In
        tmpStr = ""
        For indx = 0 To SendLen - 1

            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) + " "

        Next indx

        displayOut(2, 0, tmpStr)
        retCode = ModWinsCard.SCardTransmit(hCard, pioSendRequest, SendBuff(0), SendLen, pioSendRequest, RecvBuff(0), RecvLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            displayOut(1, retCode, "")
            SendAPDUandDisplay = retCode
            Exit Function

        Else

            tmpStr = ""
            Select Case reqType

                Case 0  '  Display SW1/SW2 value
                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                    Next indx

                    If Trim(tmpStr) <> "90 00" Then

                        displayOut(4, 0, "Return bytes are not acceptable.")

                    End If

                Case 1  ' Display ATR after checking SW1/SW2

                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                    Next indx

                    If tmpStr.Trim() <> "90 00" Then

                        tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                    Else

                        tmpStr = "ATR : "
                        For indx = 0 To (RecvLen - 3)

                            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                        Next indx

                    End If

                Case 2  ' Display all data

                    For indx = 0 To (RecvLen - 1)

                        tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                    Next indx

                Case 3  ' Interpret SW1/SW2

                    For indx = (RecvLen - 2) To (RecvLen - 1)

                        tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                    Next indx

                    If tmpStr.Trim = "6A 81" Then

                        displayOut(4, 0, "The function is not supported.")
                        SendAPDUandDisplay = retCode
                        Exit Select

                    End If

                    If tmpStr.Trim = "63 00" Then

                        displayOut(4, 0, "The operation failed.")
                        SendAPDUandDisplay = retCode
                        Exit Select

                    End If

                    validATS = True

            End Select

            displayOut(3, 0, tmpStr.Trim())

        End If

        SendAPDUandDisplay = retCode

    End Function

    Private Sub bSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSend.Click

        Dim tmpData As String
        Dim directCmd As Boolean
        Dim indx As Integer
        Dim tmpLong As Byte

        directCmd = True

        ' Validate input
        If tCLA.Text = "" Then

            tCLA.Text = "00"
            tCLA.Focus()
            Exit Sub

        End If

        If Not Byte.TryParse(tCLA.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tCLA.Focus()
            tCLA.Text = ""
            Exit Sub

        End If

        If Not Byte.TryParse(tINS.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tINS.Focus()
            tINS.Text = ""
            Exit Sub
        End If

        If Not Byte.TryParse(tP1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tP1.Focus()
            tP1.Text = ""
            Exit Sub

        End If

        If Not Byte.TryParse(tP2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tP2.Focus()
            tP2.Text = ""
            Exit Sub

        End If

        If Not Byte.TryParse(tLc.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

            tLc.Focus()
            tLc.Text = ""
            Exit Sub

        End If



        tmpData = ""

        Call ClearBuffers()
        SendBuff(0) = CLng("&H" & tCLA.Text)        'CLA

        If tINS.Text <> "" Then

            SendBuff(1) = CLng("&H" & tINS.Text)    'INS

        End If

        If tP1.Text <> "" Then

            directCmd = False

        End If

        If directCmd = False Then

            SendBuff(2) = CLng("&H" & tP1.Text)     'P1

            If tP2.Text = "" Then

                tP2.Text = "00"                     'P2 : Ask user to confirm
                tP2.Focus()
                Exit Sub

            Else

                SendBuff(3) = CLng("&H" & tP2.Text) 'P2

            End If

            If tLc.Text <> "" Then

                SendBuff(4) = CLng("&H" & tLc.Text) 'Lc

                ' Process Data In if Lc > 0
                If SendBuff(4) > 0 Then

                    tmpData = TrimInput(0, tData.Text)
                    tmpData = TrimInput(1, tmpData)

                    ' Check if Data In is consistent with Lc value
                    If SendBuff(4) > Fix((Len(tmpData) / 2)) Then

                        tData.Focus()
                        Exit Sub

                    End If

                    For indx = 0 To SendBuff(4) - 1

                        ' Format Data In
                        SendBuff(indx + 5) = CLng("&H" & Mid$(tmpData, ((indx * 2) + 1), 2))

                    Next indx

                    If Not Byte.TryParse(tLe.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

                        tLe.Focus()
                        tLe.Text = ""
                        Exit Sub

                    End If

                    If tLe.Text <> "" Then

                        SendBuff(SendBuff(4) + 5) = CLng("&H" & tLe.Text)   'Le

                    End If

                Else

                    If tLe.Text <> "" Then

                        If Not Byte.TryParse(tLe.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong) Then

                            tLe.Focus()
                            tLe.Text = ""
                            Exit Sub

                        End If


                        SendBuff(5) = CLng("&H" & tLe.Text)                 'Le

                    End If

                End If

            Else

                If tLe.Text <> "" Then

                    SendBuff(4) = CLng(("&H" & tLe.Text))                     'Le

                End If

            End If

        End If

        If directCmd = True Then

            If tINS.Text = "" Then

                SendLen = &H1

            Else

                SendLen = &H2

            End If

        Else

            If tLc.Text = "" Then

                If tLe.Text <> "" Then

                    SendLen = 5

                Else

                    SendLen = 4

                End If

            Else

                If tLe.Text = "" Then

                    SendLen = SendBuff(4) + 5

                Else

                    SendLen = SendBuff(4) + 6

                End If

            End If

        End If

        RecvLen = &HFF

        retCode = SendAPDUandDisplay(2)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Function TrimInput(ByVal TrimType As Integer, ByVal StrIn As String) As String

        Dim indx As Integer
        Dim tmpstr As String

        tmpstr = ""
        StrIn = Trim(StrIn)

        Select Case TrimType

            Case 0

                For indx = 0 To StrIn.Length - 1

                    If ((StrIn(indx) <> Chr(13)) And (StrIn(indx) <> Chr(10))) Then

                        tmpstr = tmpstr + StrIn(indx)

                    End If

                Next indx

            Case 1

                For indx = 0 To StrIn.Length - 1

                    If StrIn(indx) <> "" Then

                        tmpstr = tmpstr + StrIn(indx)

                    End If

                Next indx

        End Select

        TrimInput = tmpstr

    End Function


End Class
