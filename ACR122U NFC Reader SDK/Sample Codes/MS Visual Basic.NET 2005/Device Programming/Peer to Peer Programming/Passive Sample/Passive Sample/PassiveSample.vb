'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   set an ACR122 NFC reader to its passive mode and
'                   receive data
'  
'  Author :         Wazer Emmanuel R. Benal
'
'  Module :         ModWinscard.vb
'   
'  Date   :         August 1, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'==========================================================================================

Public Class PassiveSample

    Dim retCode As Long
    Dim hContext, ReaderCount, Protocol, hCard, nBytesRet As Integer
    Dim RdrState As SCARD_READERSTATE
    Dim sReaderList, sReaderGroup, errString, data As String
    Dim ioRequest As SCARD_IO_REQUEST
    Dim SendLen, RecvLen As Integer
    Dim SendBuff(0 To 262) As Byte
    Dim RecvBuff(0 To 262) As Byte
    Dim UidLen As Integer
    Dim Uid As Byte()
    Dim CardConn As Boolean = False
    Dim ReaderConn As Boolean = False

    Public Sub ClearBuffers()

        Dim index As Integer

        For index = 0 To 262
            SendBuff(index) = &H0
            RecvBuff(index) = &H0
        Next

    End Sub

    Public Sub DisplayOut(ByVal out As String, ByVal color As Integer)

        Select Case color
            Case 1 'Notifications
                rbOutput.SelectionColor = Drawing.Color.Green

            Case 2 'Error Messages
                rbOutput.SelectionColor = Drawing.Color.Red

            Case 3 'Input
                rbOutput.SelectionColor = Drawing.Color.Black
                out = "<< " & out

            Case 4 'Output
                rbOutput.SelectionColor = Drawing.Color.Black
                out = ">> " & out
        End Select

        rbOutput.SelectedText = out & vbCrLf
        rbOutput.SelectionStart = rbOutput.TextLength

    End Sub

    Public Function CardControl() As Long

        Dim tempstr As String = String.Empty
        Dim index As Integer = 0

        For index = 0 To SendLen - 1
            tempstr = tempstr + Microsoft.VisualBasic.Right("00" & Hex(SendBuff(index)), 2) + " "
        Next

        DisplayOut(tempstr, 3)

        retCode = ModWinsCard.SCardControl(hCard, _
                                           ModWinsCard.IOCTL_CCID_ESCAPE_SCARD_CTL_CODE, _
                                           SendBuff(0), _
                                           SendLen, _
                                           RecvBuff(0), _
                                           RecvLen, _
                                           nBytesRet)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            DisplayOut(ModWinsCard.GetScardErrMsg(retCode), 2)
            Return retCode
        End If

        tempstr = String.Empty

        For index = 0 To RecvLen - 1
            tempstr = tempstr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(index)), 2) + " "
        Next

        DisplayOut(tempstr, 4)

        Return retCode

    End Function

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInit.Click

        Dim index As Integer = 0
        Dim counter As Integer

        For counter = 0 To 255
            sReaderList = sReaderList & vbNullChar
        Next

        Call ClearBuffers()

        ReaderCount = 255

        'Establish context
        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            DisplayOut(ModWinsCard.GetScardErrMsg(retCode), 2)
            Return
        End If

        'List Readers
        cbReader.Items.Clear()

        'SCardListReaders provides the list of readers within a set of named
        'reader groups, eliminating duplicates
        retCode = ModWinsCard.SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            DisplayOut(ModWinsCard.GetScardErrMsg(retCode), 2)
            Return
        End If

        'Add readers to combobox control
        ModWinsCard.LoadListControl(cbReader, sReaderList)

        'Determine if there are any readers found
        If cbReader.Items.Count > 0 Then
            cbReader.SelectedIndex = 0
        End If

        btnInit.Enabled = False
        btnConnect.Enabled = True

    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        'Connect to selected reader
        retCode = ModWinsCard.SCardConnect(hContext, _
                                           cbReader.SelectedItem.ToString(), _
                                           ModWinsCard.SCARD_SHARE_SHARED, _
                                           ModWinsCard.SCARD_PROTOCOL_T1, _
                                           hCard, _
                                           Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            'Connect to reader directly if no card is polled
            retCode = ModWinsCard.SCardConnect(hContext, _
                                               cbReader.SelectedItem.ToString(), _
                                               ModWinsCard.SCARD_SHARE_DIRECT, _
                                               0, _
                                               hCard, _
                                               Protocol)
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                DisplayOut(ModWinsCard.GetScardErrMsg(retCode), 2)
                Return
            Else
                DisplayOut("Succesful connection to : " & cbReader.SelectedItem.ToString(), 1)
            End If
        Else
            DisplayOut("Succesful connection to : " & cbReader.SelectedItem.ToString(), 1)
        End If

        Call GetFirmware()

        btnConnect.Enabled = False
        btnPassive.Enabled = True
        RecvGroup.Enabled = True

    End Sub

    Public Sub GetFirmware()

        Dim tempstr As String = String.Empty
        Dim index As Integer = 0

        Call ClearBuffers()

        'Get firmware command
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H48
        SendBuff(3) = &H0
        SendBuff(4) = &H0

        SendLen = 5
        RecvLen = 10

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        For index = 0 To RecvLen - 1
            tempstr = tempstr + Microsoft.VisualBasic.Right(Chr(RecvBuff(index)), 2)
        Next

        DisplayOut("Firmware version: " & tempstr, 1)

    End Sub

    Public Sub SetPassive()

        Dim index As Integer = 0
        Dim tempstr As String = String.Empty

        'Setup passive mode
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H27
        SendBuff(5) = &HD4
        SendBuff(6) = &H8C
        SendBuff(7) = &H0
        SendBuff(8) = &H8
        SendBuff(9) = &H0
        SendBuff(10) = &H12
        SendBuff(11) = &H34
        SendBuff(12) = &H56
        SendBuff(13) = &H40
        SendBuff(14) = &H1
        SendBuff(15) = &HFE
        SendBuff(16) = &HA2
        SendBuff(17) = &HA3
        SendBuff(18) = &HA4
        SendBuff(19) = &HA5
        SendBuff(20) = &HA6
        SendBuff(21) = &HA7
        SendBuff(22) = &HC0
        SendBuff(23) = &HC1
        SendBuff(24) = &HC2
        SendBuff(25) = &HC3
        SendBuff(26) = &HC4
        SendBuff(27) = &HC5
        SendBuff(28) = &HC6
        SendBuff(29) = &HC7
        SendBuff(30) = &HFF
        SendBuff(31) = &HFF
        SendBuff(32) = &HAA
        SendBuff(33) = &H99
        SendBuff(34) = &H88
        SendBuff(35) = &H77
        SendBuff(36) = &H66
        SendBuff(37) = &H55
        SendBuff(38) = &H44
        SendBuff(39) = &H33
        SendBuff(40) = &H22
        SendBuff(41) = &H11
        SendBuff(42) = &H0
        SendBuff(43) = &H0

        SendLen = 44
        RecvLen = 22

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Exit Sub
        End If

        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(index)), 2)
        Next

        If tempstr <> "9000" Then
            DisplayOut("Set passive failed!", 2)
            Return
        End If

        Call RecvData()

    End Sub

    Private Sub btnPassive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPassive.Click

        Call SetPassive()

    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click

        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Me.Close()

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        rbOutput.Clear()

    End Sub

    Public Sub Initialize()

        btnInit.Enabled = True
        btnConnect.Enabled = False
        btnPassive.Enabled = False
        RecvGroup.Enabled = False
        tbData.Clear()
        rbOutput.Clear()
        cbReader.Text = ""
        DisplayOut("Program ready", 1)

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Call Initialize()

    End Sub

    Private Sub PassiveSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call Initialize()

    End Sub

    Public Sub RecvData()

        Dim datalen As Byte = &H0
        Dim index As Integer = 0
        Dim tempstr As String = String.Empty

        data = String.Empty

        'Receive first the length of
        'the actual data to be received
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HD4
        SendBuff(6) = &H86

        SendLen = 7
        RecvLen = 6

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        datalen = RecvBuff(3)

        'Send a response with a value of 90 00
        'to the sending device
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &HD4
        SendBuff(6) = &H8E
        SendBuff(7) = &H90
        SendBuff(8) = &H0

        SendLen = 9
        RecvLen = 5

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        'We receive the actual data
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H2
        SendBuff(5) = &HD4
        SendBuff(6) = &H86

        SendLen = 7
        RecvLen = Convert.ToInt32(datalen) + 5

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        For index = 3 To RecvLen - 3
            data = data + Microsoft.VisualBasic.Right(Chr(RecvBuff(index)), 2)
        Next

        'We send the response with a value of 90 00
        'to the sending device
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H4
        SendBuff(5) = &HD4
        SendBuff(6) = &H8E
        SendBuff(7) = &H90
        SendBuff(8) = &H0

        SendLen = 9
        RecvLen = 5

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        tbData.Text = data

    End Sub
End Class
