'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   set an ACR122 NFC reader to its active mode and
'                   send data
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

Public Class ActiveSample

    Dim retCode As Long
    Dim hContext, ReaderCount, Protocol, hCard, nBytesRet As Integer
    Dim RdrState As SCARD_READERSTATE
    Dim sReaderList, sReaderGroup, errString As String
    Dim ioRequest As SCARD_IO_REQUEST
    Dim SendLen, RecvLen As Integer
    Dim SendBuff(0 To 262) As Byte
    Dim RecvBuff(0 To 262) As Byte
    Dim UidLen As Integer
    Dim Uid As Byte()

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

    Public Sub Initialize()

        btnInit.Enabled = True
        btnConnect.Enabled = False
        SendGroup.Enabled = False
        btnActive.Enabled = False
        cbReader.Text = ""
        rbOutput.Clear()
        DisplayOut("Program ready", 1)

    End Sub

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInit.Click

        Dim readerStr As String = String.Empty
        Dim tempstr As String = String.Empty
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
        btnActive.Enabled = True
        SendGroup.Enabled = True

    End Sub

    Public Sub SetActive()

        Dim index As Integer = 0
        Dim tempstr As String = String.Empty

        Call ClearBuffers()

        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &HA
        SendBuff(5) = &HD4
        SendBuff(6) = &H56
        SendBuff(7) = &H1
        SendBuff(8) = &H2
        SendBuff(9) = &H1
        SendBuff(10) = &H0
        SendBuff(11) = &HFF
        SendBuff(12) = &HFF
        SendBuff(13) = &H0
        SendBuff(14) = &H0

        SendLen = 15
        RecvLen = 21

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(index)), 2)
        Next

        If tempstr <> "9000" Then
            DisplayOut("Set active failed!", 2)
            Return
        End If

        Call SendData()

    End Sub

    Private Sub btnActive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActive.Click

        Call SetActive()

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        rbOutput.Clear()

    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click

        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Me.Close()

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        retCode = ModWinsCard.SCardReleaseContext(hContext)
        Call Initialize()

    End Sub

    Private Sub ActiveSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call Initialize()

    End Sub

    Public Sub SendData()
        Dim tempdata(0 To 512) As Char
        Dim data As String = String.Empty
        Dim datalen As Integer
        Dim index As Integer = 0
        Dim tempstr As String = String.Empty

        'Transfer string data to a char array
        'and determine its length
        data = tbData.Text
        datalen = data.Length

        For index = 0 To datalen - 1
            tempdata(index) = data.Substring(index, 1)
        Next

        'Send the length of the data first
        'so that the receiving device would
        'know how long the data would be
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = &H1
        SendBuff(5) = &HD4
        SendBuff(6) = &H40
        SendBuff(7) = &H1
        SendBuff(8) = Convert.ToSByte(datalen, 10)  'the length of the data

        SendLen = 9
        RecvLen = 7

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

        'Send actual data
        Call ClearBuffers()
        SendBuff(0) = &HFF
        SendBuff(1) = &H0
        SendBuff(2) = &H0
        SendBuff(3) = &H0
        SendBuff(4) = Convert.ToSByte(datalen, 10)
        SendBuff(5) = &HD4
        SendBuff(6) = &H40
        SendBuff(7) = &H1

        For index = 0 To datalen - 1
            SendBuff(index + 8) = Microsoft.VisualBasic.AscW(tempdata(index))
        Next

        SendLen = datalen + 8
        RecvLen = 7

        retCode = CardControl()
        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            Return
        End If

    End Sub
End Class
