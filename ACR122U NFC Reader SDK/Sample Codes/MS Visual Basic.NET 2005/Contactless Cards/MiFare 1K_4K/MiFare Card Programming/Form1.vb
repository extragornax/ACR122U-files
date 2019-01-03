'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   transact with Mifare 1K/4K cards using ACR122
'  
'  File   :         MifareProg.vb    
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.vb
'   
'  Date   :         June 28, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'=========================================================================================

Public Class MiFareCardProg

    Dim retCode, Protocol, hContext, hCard, ReaderCount As Integer
    Dim RdrState As SCARD_READERSTATE
    Dim sReaderList As String
    Dim sReaderGroup As String
    Dim errString As String
    Dim SendLen, RecvLen As Integer
    Dim ReaderLen, ATRLen As Integer
    Dim dwState, dwActProtocol As Long
    Dim ATRVal(256) As Byte
    Dim connActive As Boolean = False
    Dim SendBuff(262), RecvBuff(262) As Byte
    Public pioSendRequest, pioRecvRequest As SCARD_IO_REQUEST

    Private Sub InitMenu()

        connActive = False
        cbReader.Text = ""
        mMsg.Text = ""
        mMsg.Clear()
        bInit.Enabled = True
        bConnect.Enabled = False
        bClear.Enabled = False
        gbLoadKeys.Enabled = False
        gbAuth.Enabled = False
        gbBinOps.Enabled = False
        gbValBlk.Enabled = False
        Call displayOut(0, 0, "Program ready")

    End Sub

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
        bClear.Enabled = True

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

    Private Sub ClearBuffers()

        Dim indx As Long

        For indx = 0 To 262

            RecvBuff(indx) = &H0
            SendBuff(indx) = &H0

        Next indx

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
        gbLoadKeys.Enabled = True
        gbAuth.Enabled = True
        gbBinOps.Enabled = True
        gbValBlk.Enabled = True
        tKeyNum.Focus()
        rbKType1.Checked = True


    End Sub

    Private Sub MiFareCardProg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub bLoadKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLoadKey.Click

        Dim tmpLong As Byte
        Dim indx As Integer
        Dim tmpStr As String

        If (tKeyNum.Text = "" Or Not Byte.TryParse(tKeyNum.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKeyNum.Focus()
            tKeyNum.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKeyNum.Text) > &H1 Then
            tKeyNum.Text = "1"
            Exit Sub
        End If

        If (tKey1.Text = "" Or Not Byte.TryParse(tKey1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey1.Focus()
            tKey1.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey1.Text) <> &HFF Then
            tKey1.Text = "FF"
            Exit Sub

        End If

        If (tKey2.Text = "" Or Not Byte.TryParse(tKey2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey2.Focus()
            tKey2.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey2.Text) <> &HFF Then
            tKey2.Text = "FF"
            Exit Sub

        End If

        If (tKey3.Text = "" Or Not Byte.TryParse(tKey3.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey3.Focus()
            tKey3.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey3.Text) <> &HFF Then
            tKey3.Text = "FF"
            Exit Sub

        End If

        If (tKey4.Text = "" Or Not Byte.TryParse(tKey4.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey4.Focus()
            tKey4.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey4.Text) <> &HFF Then
            tKey4.Text = "FF"
            Exit Sub

        End If

        If (tKey5.Text = "" Or Not Byte.TryParse(tKey5.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey5.Focus()
            tKey5.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey5.Text) <> &HFF Then
            tKey5.Text = "FF"
            Exit Sub

        End If

        If (tKey6.Text = "" Or Not Byte.TryParse(tKey6.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tKey6.Focus()
            tKey6.Text = ""
            Exit Sub

        ElseIf CLng("&H" & tKey6.Text) <> &HFF Then
            tKey6.Text = "FF"
            Exit Sub

        End If

        Call ClearBuffers()
        'Load Authentication Keys command
        SendBuff(0) = &HFF                          'Class
        SendBuff(1) = &H82                          'INS
        SendBuff(2) = &H0                           'P1 : Key Structure
        SendBuff(3) = CLng("&H" + tKeyNum.Text)     'P2 : Key Number
        SendBuff(4) = &H6                           'P3 : Lc
        SendBuff(5) = CLng("&H" + tKey1.Text)       'Key 1
        SendBuff(6) = CLng("&H" + tKey2.Text)       'Key 2
        SendBuff(7) = CLng("&H" + tKey3.Text)       'Key 3
        SendBuff(8) = CLng("&H" + tKey4.Text)       'Key 4
        SendBuff(9) = CLng("&H" + tKey5.Text)       'Key 5
        SendBuff(10) = CLng("&H" + tKey6.Text)      'Key 6

        SendLen = 11
        RecvLen = 2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx
            'Check for response
            If tmpStr.Trim <> "90 00" Then

                Call displayOut(4, 0, "Load authentication keys error!")

            End If
        End If

    End Sub

    Public Function SendAPDU()

        Dim indx As Integer
        Dim tmpStr As String

        pioSendRequest.dwProtocol = Protocol
        pioSendRequest.cbPciLength = Len(pioSendRequest)

        tmpStr = ""

        For indx = 0 To SendLen - 1

            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) + " "

        Next indx

        Call displayOut(2, 0, tmpStr)

        retCode = ModWinsCard.SCardTransmit(hCard, pioSendRequest, SendBuff(0), SendLen, pioSendRequest, RecvBuff(0), RecvLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(2, retCode, "")
            SendAPDU = retCode
            Exit Function
        End If

        tmpStr = ""

        For indx = 0 To RecvLen - 1

            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

        Next indx

        Call displayOut(3, 0, tmpStr)

        SendAPDU = retCode

    End Function

    Private Sub bQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bQuit.Click

        ' terminate the application
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End
    End Sub

    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click

        If connActive Then

            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)

        End If

        retCode = ModWinsCard.SCardReleaseContext(hCard)
        Call InitMenu()
    End Sub

    Private Sub bClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bClear.Click

        mMsg.Clear()

    End Sub

    Private Sub bAuth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bAuth.Click

        Dim tmpLong As Byte
        Dim tempInt, indx As Integer
        Dim tmpStr As String

        ' Validate input
        If tBlkNo.Text = "" Or Not Integer.TryParse(tBlkNo.Text, tempInt) Then

            tBlkNo.Focus()
            tBlkNo.Text = ""
            Exit Sub

        End If

        If CInt(tBlkNo.Text) > 319 Then

            tBlkNo.Text = "319"

        End If

        If (tAuthenKeyNum.Text = "" Or Not Byte.TryParse(tKey1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tAuthenKeyNum.Focus()
            Exit Sub

        ElseIf CInt("&H" & tAuthenKeyNum.Text) > &H1 Then

            tAuthenKeyNum.Text = "1"
            Exit Sub

        End If

        Call ClearBuffers()
        'Authentication command
        SendBuff(0) = &HFF                          'Class
        SendBuff(1) = &H86                          'INS
        SendBuff(2) = &H0                           'P1
        SendBuff(3) = &H0                           'P2
        SendBuff(4) = &H5                           'Lc
        SendBuff(5) = &H1                           'Byte 1 : Version number
        SendBuff(6) = &H0                           'Byte 2
        SendBuff(7) = CInt(tBlkNo.Text)          'Byte 3 : Block number

        If rbKType1.Checked = True Then
            SendBuff(8) = &H60                      'Byte 4 : Key Type A
        ElseIf rbKType2.Checked = True Then
            SendBuff(8) = &H61                      'Byte 4 : Key Type B
        End If

        SendBuff(9) = CInt("&H" & tAuthenKeyNum.Text) 'Byte 5 : Key number

        SendLen = 10
        RecvLen = 2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = 0 To RecvLen - 1

                'tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Checking for response
            If tmpStr.Trim = "90 00" Then

                Call displayOut(0, 0, "Authentication success")
            Else
                Call displayOut(4, 0, "Authentication failed")
            End If
        End If


    End Sub

    Private Sub bBinRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinRead.Click

        Dim tmpStr As String
        Dim indx As Integer

        ' Validate Inputs
        tBinData.Text = ""

        If tBinBlk.Text = "" Then

            tBinBlk.Focus()
            Exit Sub

        End If

        If CInt(tBinBlk.Text) > 319 Then

            tBinBlk.Text = "319"
            Exit Sub

        End If

        If tBinLen.Text = "" Then

            tBinLen.Focus()
            Exit Sub
        ElseIf CInt(tBinLen.Text) > 16 Then
            tBinLen.Text = "16"
            tBinLen.Focus()
            Exit Sub

        End If


        Call ClearBuffers()
        'Read Binary Block command
        SendBuff(0) = &HFF                              'Class
        SendBuff(1) = &HB0                              'INS
        SendBuff(2) = &H0                               'P1
        SendBuff(3) = CInt("&H" & tBinBlk.Text) 'P2 : Block number
        SendBuff(4) = CInt(tBinLen.Text)                  'Le : Number of bytes to read

        SendLen = 5
        RecvLen = CInt(tBinLen.Text) + 2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1
                'tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response
            If tmpStr.Trim = "90 00" Then
                tmpStr = ""
                For indx = 0 To RecvLen - 3

                    'tmpstr = tempstr & Right$(Chr(RecvBuff(index)), 2)
                    tmpStr = tmpStr + Chr(RecvBuff(indx))
                    'tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

                Next indx

                tBinData.Text = tmpStr
            Else
                Call displayOut(4, 0, "Read block error!")
            End If
        End If

    End Sub

    Private Sub bBinUpd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBinUpd.Click


        Dim tmpStr As String
        Dim indx, tempInt As Integer

        ' Validate inputs
        If (tBinBlk.Text = "" Or Not Integer.TryParse(tBinBlk.Text, tempInt)) Then

            tBinBlk.Focus()
            tBinBlk.Text = ""
            Exit Sub

        End If

        If CInt(tBinBlk.Text) > 319 Then

            tBinBlk.Text = "319"
            Exit Sub

        End If

        If (tBinLen.Text = "" Or Not Integer.TryParse(tBinLen.Text, tempInt)) Then

            tBinLen.Focus()
            tBinLen.Text = ""
            Exit Sub
        ElseIf CInt(tBinLen.Text) <> 16 Then
            tBinLen.Text = "16"
            tBinLen.Focus()
            Exit Sub

        End If

        If tBinData.Text = "" Then

            tBinData.Focus()
            Exit Sub

        End If

        Call ClearBuffers()
        'Update Binary Block command
        SendBuff(0) = &HFF                              'Class
        SendBuff(1) = &HD6                              'INS
        SendBuff(2) = &H0                               'P1
        SendBuff(3) = CInt("&H" + tBinBlk.Text)         'P2 : Block Number
        SendBuff(4) = CInt(tBinLen.Text)                'Lc

        For indx = 0 To Len(tBinData.Text) - 1

            SendBuff(indx + 5) = Asc(Mid(tBinData.Text, indx + 1, 1)) 'Data In

        Next indx

        SendLen = SendBuff(4) + 5 'CInt(tbLen.Text) + 5
        RecvLen = 2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                'tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response
            If tmpStr.Trim = "90 00" Then
                tBinData.Text = ""
            Else
                Call displayOut(2, 0, "")
            End If
        End If


    End Sub

    Private Sub tBinLen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tBinLen.TextChanged

        Dim numbyte As Byte

        If Byte.TryParse(tBinLen.Text, numbyte) Then

            tBinData.MaxLength = numbyte

        End If

    End Sub

    Private Sub bValStor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValStor.Click

        Dim Amount As Long
        Dim tempInt, indx As Integer
        Dim tmpStr As String


        ' Validate inputs
        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                      ' CLA
        SendBuff(1) = &HD7                                      ' INS
        SendBuff(2) = &H0                                       ' P1
        SendBuff(3) = CInt(tValBlk.Text)                        ' P2 : Block No.    
        SendBuff(4) = &H5                                       ' Lc : Data length
        SendBuff(5) = &H0                                       ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF                   ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF                   ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                    ' Amount middle byte
        SendBuff(9) = Amount And &HFF                           ' Amount LSByte
        SendLen = SendBuff(4) + 5
        RecvLen = &H2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                'tmpstr = tmpstr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response
            If tmpStr.Trim <> "90 00" Then
                Call displayOut(4, 0, "Store value error!")
            End If
        End If


    End Sub

    Private Sub bValInc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValInc.Click


        Dim Amount As Long
        Dim tempInt, indx As Integer
        Dim tmpStr As String


        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H5                                   ' Lc : Data length
        SendBuff(5) = &H1                                   ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF               ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF               ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                ' Amount middle byte
        SendBuff(9) = Amount And &HFF                       ' Amount LSByte

        SendLen = SendBuff(4) + 5
        RecvLen = &H2


        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                'tmpstr = tmpstr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response            
            If tmpStr.Trim <> "90 00" Then
                Call displayOut(4, 0, "Increment error!")
            End If
        End If


    End Sub

    Private Sub bValRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValRead.Click


        Dim Amount As Long
        Dim indx As Integer
        Dim tmpStr As String

        If tValBlk.Text = "" Then

            tValBlk.Focus()
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValAmt.Text = ""
        tValSrc.Text = ""
        tValTar.Text = ""

        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HB1                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H4                                   ' Le

        SendLen = &H5
        RecvLen = &H6


        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response            
            If tmpStr.Trim = "90 00" Then
                ' Call displayOut(1, retCode, "")
                Amount = RecvBuff(3)
                Amount = Amount + (RecvBuff(2) * 256)
                Amount = Amount + (RecvBuff(1) * 256 * 256)
                Amount = Amount + (RecvBuff(0) * 256 * 256 * 256)
                tValAmt.Text = CInt(Amount)


            Else

                Call displayOut(4, 0, "Read value error!")
            End If
        End If

    End Sub

    Private Sub bValDec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValDec.Click


        Dim Amount As Long
        Dim tempInt, indx As Integer
        Dim tmpStr As String

        If (tValAmt.Text = "" Or Not Integer.TryParse(tValAmt.Text, tempInt)) Then

            tValAmt.Focus()
            tValAmt.Text = ""
            Exit Sub

        End If

        If Convert.ToInt64(tValAmt.Text) > 4294967295 Then

            tValAmt.Text = "4294967295"
            tValAmt.Focus()
            Exit Sub

        End If

        If (tValBlk.Text = "" Or Not Integer.TryParse(tValBlk.Text, tempInt)) Then

            tValBlk.Focus()
            tValBlk.Text = ""
            Exit Sub

        End If

        If CInt(tValBlk.Text) > 319 Then

            tValBlk.Text = "319"
            Exit Sub

        End If

        tValSrc.Text = ""
        tValTar.Text = ""

        Amount = Convert.ToInt64(tValAmt.Text)
        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValBlk.Text)                    ' P2 : Block No.
        SendBuff(4) = &H5                                   ' Lc : Data length
        SendBuff(5) = &H2                                   ' VB_OP Value
        SendBuff(6) = (Amount >> 24) And &HFF               ' Amount MSByte
        SendBuff(7) = (Amount >> 16) And &HFF               ' Amount middle byte
        SendBuff(8) = (Amount >> 8) And &HFF                ' Amount middle byte
        SendBuff(9) = Amount And &HFF                       ' Amount LSByte

        SendLen = SendBuff(4) + 5
        RecvLen = &H2


        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else
            For indx = RecvLen - 2 To RecvLen - 1

                'tmpstr = tmpstr & Right$("00" & Hex(RecvBuff(index)), 2)
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            'Check for response            
            If tmpStr.Trim <> "90 00" Then
                Call displayOut(4, 0, "Decrement error!")
            End If
        End If


    End Sub

    Private Sub bValRes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bValRes.Click


        Dim tempInt, indx As Integer
        Dim tmpStr As String

        ' Validate inputs
        If (tValSrc.Text = "" Or Not Integer.TryParse(tValSrc.Text, tempInt)) Then

            tValSrc.Focus()
            tValSrc.Text = ""
            Exit Sub

        End If

        If (tValTar.Text = "" Or Not Integer.TryParse(tValTar.Text, tempInt)) Then

            tValTar.Focus()
            tValTar.Text = ""
            Exit Sub

        End If


        If CInt(tValSrc.Text) <= 0 Then

            tValSrc.Text = "1"
            Exit Sub

        End If


        If CInt(tValTar.Text) <= 0 Then

            tValTar.Text = "1"
            Exit Sub

        End If


        If CInt(tValSrc.Text) > 319 Then

            tValSrc.Text = "319"
            Exit Sub

        End If

        If CInt(tValTar.Text) > 319 Then

            tValTar.Text = "319"
            Exit Sub

        End If

        tValAmt.Text = ""
        tValBlk.Text = ""

        Call ClearBuffers()
        SendBuff(0) = &HFF                                  ' CLA
        SendBuff(1) = &HD7                                  ' INS
        SendBuff(2) = &H0                                   ' P1
        SendBuff(3) = CInt(tValSrc.Text)                    ' P2 : Source Block No.
        SendBuff(4) = &H2                                   ' Lc
        SendBuff(5) = &H3                                   ' Data In Byte 1
        SendBuff(6) = CInt(tValTar.Text)                    ' P2 : Target Block No.

        SendLen = &H7
        RecvLen = &H2

        retCode = SendAPDU()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        Else

            For indx = RecvLen - 2 To RecvLen - 1
                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
            Next indx

            'Check for response
            If tmpStr.Trim <> "90 00" Then
                Call displayOut(4, 0, "Restore value error!")
            End If

        End If

    End Sub
End Class
