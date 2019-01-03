'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                    set the LED?Buzzer and antenna of the ACR122 NFC reader
'  
'  File   :         DevProg.vb    
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

Public Class DeviceProgramming

    Dim retCode, hContext, hCard, Protocol As Long
    Dim connActive As Boolean = False
    Dim SendBuff(262), RecvBuff(262) As Byte
    Dim SendLen, RecvLen, nBytesRet As Integer
    Dim sReaderList As String
    Dim sReaderGroup As String
    Dim ioRequest As SCARD_IO_REQUEST


    Private Sub DeviceProgramming_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call InitMenu()

    End Sub

    Private Sub InitMenu()

        connActive = False
        cbReader.Text = ""
        mMsg.Text = ""
        mMsg.Clear()
        bInit.Enabled = True
        bConnect.Enabled = False
        bClear.Enabled = False
        gbAntenna.Enabled = False
        gbRed.Enabled = False
        gbGreen.Enabled = False
        gbBlinkDuration.Enabled = False
        bSetLED.Enabled = False
        bGetFW.Enabled = False
        bStatus.Enabled = False
        Call displayOut(0, 0, "Program ready")


    End Sub

    Private Sub EnableButtons()

        bInit.Enabled = False
        bConnect.Enabled = True
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

                PrintText = ModWinsCard.GetScardErrMsg(retVal)
                mMsg.SelectionColor = Drawing.Color.Red
            Case 2
                mMsg.SelectionColor = Drawing.Color.Black
                PrintText = "<" + PrintText
            Case 3
                mMsg.SelectionColor = Drawing.Color.Black
                PrintText = ">" + PrintText
            Case 4
                mMsg.SelectionColor = Drawing.Color.Green

        End Select

        mMsg.SelectedText = PrintText & vbCrLf
        mMsg.SelectionColor = Drawing.Color.Black
        mMsg.SelectionStart = mMsg.TextLength
        mMsg.Focus()

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
        bGetFW.Enabled = True
        bStatus.Enabled = True
        gbAntenna.Enabled = True
        gbRed.Enabled = True
        gbGreen.Enabled = True
        gbBlinkDuration.Enabled = True
        bSetLED.Enabled = True
        bGetFW.Enabled = True
        rbAntOn.Checked = True
        rbRedFinalOff.Checked = True
        rbRedStateMaskOff.Checked = True
        rbRedInitOff.Checked = True
        rbRedBlinkMaskOff.Checked = True
        rbGreenFinalOff.Checked = True
        rbGreenStateMaskOff.Checked = True
        rbGreenInitOff.Checked = True
        rbGreenBlinkMaskOff.Checked = True
        rbLinkToBuzzOpt1.Checked = True
        tT1.Text = "00"
        tT2.Text = "00"
        tRepeat.Text = "01"

    End Sub

    Private Sub bGetFW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGetFW.Click

        ' Get the firmaware version of the reader
        Dim tmpStr As String
        Dim intIndx As Integer

        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H48
        SendBuff(3) = &H0
        SendBuff(4) = &H0
        SendLen = 5
        RecvLen = 10

        retCode = Transmit()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If


        ' Interpret firmware data
        tmpStr = "Firmware Version: "

        For intIndx = 0 To RecvLen

            If RecvBuff(intIndx) <> &H0 Then

                tmpStr = tmpStr & Chr(RecvBuff(intIndx))

            End If

        Next intIndx

        Call displayOut(3, 0, tmpStr)

    End Sub

    Private Function Transmit() As Long

        Dim tmpStr As String
        Dim indx As Integer

        'Display Apdu In
        tmpStr = ""

        For indx = 0 To SendLen - 1

            tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(indx)), 2) + " "

        Next indx

        Call displayOut(2, 0, tmpStr)

        ioRequest.dwProtocol = Protocol
        ioRequest.cbPciLength = Len(ioRequest)

        RecvLen = 262

        'Issue SCardTransmit
        retCode = ModWinsCard.SCardTransmit(hCard, _
                             ioRequest, _
                             SendBuff(0), _
                             SendLen, _
                             ioRequest, _
                             RecvBuff(0), _
                             RecvLen)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Call displayOut(1, retCode, "")

        Else

            tmpStr = ""

            For indx = 0 To (RecvLen - 1)

                tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "

            Next indx

            Call displayOut(3, 0, Trim(tmpStr))

        End If

        Transmit = retCode

    End Function

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

    Private Sub bSetAntenna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetAntenna.Click

        'set the antenna options
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &HD4
        SendBuff(6) = &H32
        SendBuff(7) = &H1

        If rbAntOn.Checked = True Then

            SendBuff(8) = &H1

        Else

            SendBuff(8) = &H0

        End If

        SendLen = 9

        retCode = Transmit()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Private Sub bSetLED_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSetLED.Click

        ' Set the LED/Buzzer Settings
      
        Dim tmpLong As Byte

        ' Validate input
        If (tT1.Text = "" Or Not Byte.TryParse(tT1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tT1.Focus()
            tT1.Text = ""
            Exit Sub

        End If

        If (tT2.Text = "" Or Not Byte.TryParse(tT2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tT2.Focus()
            tT2.Text = ""
            Exit Sub

        End If

        If (tRepeat.Text = "" Or Not Byte.TryParse(tRepeat.Text.Trim(), System.Globalization.NumberStyles.HexNumber, Nothing, tmpLong)) Then

            tRepeat.Focus()
            tRepeat.Text = ""
            Exit Sub

        End If

        If tT1.Text = "" Then

            tT1.Focus()
            Exit Sub

        End If


        If tT2.Text = "" Then

            tT2.Focus()
            Exit Sub

        End If

        If tRepeat.Text = "" Then

            tRepeat.Focus()
            Exit Sub

        End If

        If tRepeat.Text = "00" Then

            tRepeat.Text = "01"
            Exit Sub

        End If

        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H40
        SendBuff(3) = &H0

        If rbRedFinalOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H1

        End If

        If rbGreenFinalOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H2

        End If

        If rbRedStateMaskOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H4

        End If

        If rbGreenStateMaskOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H8

        End If

        If rbRedInitOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H10

        End If

        If rbGreenInitOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H20

        End If

        If rbRedBlinkMaskOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H40

        End If

        If rbGreenBlinkMaskOn.Checked = True Then

            SendBuff(3) = SendBuff(3) Or &H80

        End If

        SendBuff(4) = &H40
        SendBuff(5) = CByte("&H" + tT1.Text)
        SendBuff(6) = CByte("&H" + tT2.Text)
        SendBuff(7) = CByte("&H" + tRepeat.Text)

        If rbLinkToBuzzOpt1.Checked = True Then

            SendBuff(8) = &H0

        End If

        If rbLinkToBuzzOpt2.Checked = True Then

            SendBuff(8) = &H1

        End If

        If rbLinkToBuzzOpt3.Checked = True Then

            SendBuff(8) = &H2

        End If

        If rbLinkToBuzzOpt4.Checked = True Then

            SendBuff(8) = &H3

        End If

        SendLen = 9
        RecvLen = 2

        retCode = Transmit()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

    End Sub

    Public Function GetResponse() As Boolean
        'Issue Get Response for Tag found 
        SendLen = 5
        RecvLen = RecvBuff(1)
        RecvBuff = New Byte(RecvLen - 1) {}
        SendBuff = New Byte(SendLen - 1) {}

        'Get Response Command
        SendBuff(0) = &HFF
        SendBuff(1) = &HC0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = CByte(RecvLen)

        retCode = Transmit()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return False
        End If

        Return True

    End Function

    Private Sub bStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bStatus.Click

        Dim tmpStr As String
        Dim indx As Long
        
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HD4
        SendBuff(6) = &H4
        SendLen = 7
        RecvLen = 12

        retCode = Transmit()

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            Exit Sub

        End If

        tmpStr = ""

        For indx = 0 To (RecvLen - 1)

            tmpStr = tmpStr & Format(Hex(RecvBuff(indx)), "00")

        Next indx

        If InStr(tmpStr, "D505280000809000") Then

            'No Tag is in the field
            Call displayOut(1, 0, "No Tag is in the field: " & +Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(0)), 2) & " " & +Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) + "")

        Else

            'Interpret data
            'error code
            Call displayOut(3, 0, "Error Code: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(2)), 2) + "")

            'Field indicates if an external RF field is present and detected
            '(Field=0x01 or not (Field 0x00)
            If RecvBuff(3) = &H1 Then
                Call displayOut(3, 0, "External RF field is Present and detected: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(3)), 2) + "")
            Else
                Call displayOut(3, 0, "External RF field is NOT Present and NOT detected: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(3)), 2) + "")
            End If

            'Number of targets currently controlled acting as initiator.The default value is 1
            Call displayOut(3, 0, "Number of Target: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(4)), 2) + "")

            'Logical number
            Call displayOut(3, 0, "Number of Target: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(5)), 2) + "")
            'Bit rate in reception
            Select Case (RecvBuff(6))

                Case &H0 : Call displayOut(3, 0, "Bit Rate in Reception: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(6)), 2) + "" & " (106 kbps)")
                Case &H1 : Call displayOut(3, 0, "Bit Rate in Reception: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(6)), 2) + "" & " (212 kbps)")
                Case &H2 : Call displayOut(3, 0, "Bit Rate in Reception: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(6)), 2) + "" & " (424 kbps)")

            End Select

            'Bit rate in transmission
            Select Case (RecvBuff(7))

                Case &H0 : Call displayOut(3, 0, "Bit Rate in Transmission: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(7)), 2) + "" & " (106 kbps)")
                Case &H1 : Call displayOut(3, 0, "Bit Rate in Transmission: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(7)), 2) + "" & " (212 kbps)")
                Case &H2 : Call displayOut(3, 0, "Bit Rate in Transmission: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(7)), 2) + "" & " (424 kbps)")

            End Select

            Select Case (RecvBuff(8))

                Case &H0 : Call displayOut(3, 0, "Modulation Type: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) + "" & " (ISO14443 or Mifare)")
                Case &H1 : Call displayOut(3, 0, "Modulation Type: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) + "" & " (Active mode)")
                Case &H2 : Call displayOut(3, 0, "Modulation Type: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) + "" & " (Innovision Jewel tag)")
                Case &H10 : Call displayOut(3, 0, "Modulation Type: " + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(8)), 2) + "" & " (Felica)")

            End Select
        End If

    End Sub

End Class

