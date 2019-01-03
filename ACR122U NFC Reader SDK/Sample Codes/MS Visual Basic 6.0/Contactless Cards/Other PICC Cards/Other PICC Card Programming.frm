VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form frmOther 
   Caption         =   "Other PICC Card Programming"
   ClientHeight    =   5655
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   9375
   Icon            =   "Other PICC Card Programming.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5655
   ScaleWidth      =   9375
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton btnQuit 
      Caption         =   "Quit"
      Height          =   375
      Left            =   7680
      TabIndex        =   21
      Top             =   5160
      Width           =   1575
   End
   Begin VB.CommandButton btnReset 
      Caption         =   "Reset"
      Height          =   375
      Left            =   5760
      TabIndex        =   20
      Top             =   5160
      Width           =   1575
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear Output"
      Height          =   375
      Left            =   3840
      TabIndex        =   19
      Top             =   5160
      Width           =   1575
   End
   Begin VB.Frame Frame2 
      Caption         =   "Send Card Command"
      Height          =   3015
      Left            =   120
      TabIndex        =   8
      Top             =   2520
      Width           =   3615
      Begin VB.CommandButton btnSendCommand 
         Caption         =   "Send Card Command"
         Height          =   375
         Left            =   1680
         TabIndex        =   18
         Top             =   2520
         Width           =   1815
      End
      Begin VB.TextBox tbData 
         Height          =   735
         Left            =   240
         MultiLine       =   -1  'True
         TabIndex        =   17
         Top             =   1560
         Width           =   3015
      End
      Begin VB.TextBox tbLe 
         Height          =   285
         Left            =   2640
         MaxLength       =   2
         TabIndex        =   15
         Top             =   720
         Width           =   375
      End
      Begin VB.TextBox tbLc 
         Height          =   285
         Left            =   2160
         MaxLength       =   2
         TabIndex        =   14
         Top             =   720
         Width           =   375
      End
      Begin VB.TextBox tbP2 
         Height          =   285
         Left            =   1680
         MaxLength       =   2
         TabIndex        =   13
         Top             =   720
         Width           =   375
      End
      Begin VB.TextBox tbP1 
         Height          =   285
         Left            =   1200
         MaxLength       =   2
         TabIndex        =   12
         Top             =   720
         Width           =   375
      End
      Begin VB.TextBox tbINS 
         Height          =   285
         Left            =   720
         MaxLength       =   2
         TabIndex        =   11
         Top             =   720
         Width           =   375
      End
      Begin VB.TextBox tbCLA 
         Height          =   285
         Left            =   240
         MaxLength       =   2
         TabIndex        =   10
         Top             =   720
         Width           =   375
      End
      Begin VB.Label Label3 
         Caption         =   "Data In"
         Height          =   255
         Left            =   240
         TabIndex        =   16
         Top             =   1200
         Width           =   615
      End
      Begin VB.Label Label2 
         Caption         =   "CLA    INS       P1      P2      Lc       Le"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   480
         Width           =   3015
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Get Data Function"
      Height          =   855
      Left            =   120
      TabIndex        =   5
      Top             =   1560
      Width           =   3615
      Begin VB.CheckBox check1 
         Caption         =   "ISO 14443 A Card"
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   360
         Width           =   1695
      End
      Begin VB.CommandButton btnGetData 
         Caption         =   "Get Data"
         Height          =   375
         Left            =   2040
         TabIndex        =   6
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.CommandButton btnConnect 
      Caption         =   "Connect"
      Height          =   375
      Left            =   2040
      TabIndex        =   4
      Top             =   1080
      Width           =   1695
   End
   Begin VB.CommandButton btnInit 
      Caption         =   "Initialize"
      Height          =   375
      Left            =   2040
      TabIndex        =   3
      Top             =   600
      Width           =   1695
   End
   Begin RichTextLib.RichTextBox rbOutput 
      Height          =   4935
      Left            =   3840
      TabIndex        =   2
      Top             =   120
      Width           =   5415
      _ExtentX        =   9551
      _ExtentY        =   8705
      _Version        =   393217
      TextRTF         =   $"Other PICC Card Programming.frx":17A2
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   120
      Width           =   2415
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   1095
   End
End
Attribute VB_Name = "frmOther"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              Other PICC Card Programming.frm
'
'  Description:       This sample program outlines the steps on how to
'                     transact with other PICC cards using ACR122
'
'  Author:            Wazer Emmanuel R. Benal
'
'  Date:              July 24, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Const INVALID_SW1SW2 = -450

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive, autoDet As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim RdrState As SCARD_READERSTATE
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 255) As Byte
Dim RecvBuff(0 To 255) As Byte
Dim validATS As Boolean

Public Sub ClearBuffers()

    Dim index As Integer
    
    For index = 0 To 255
        RecvBuff(index) = &H0
        SendBuff(index) = &H0
    Next index
    
End Sub

Public Sub DisplayOut(ByVal out As String, ByVal mode As Integer)

    Select Case mode
        Case 1
            rbOutput.SelColor = vbBlue
            
        Case 2
            rbOutput.SelColor = vbRed
            
        Case 3
            rbOutput.SelColor = vbBlack
            out = "<< " & out
            
        Case 4
            rbOutput.SelColor = vbBlack
            out = ">> " & out
    End Select
    
    rbOutput.SelText = out & vbCrLf
    rbOutput.SelStart = Len(rbOutput.Text)
    rbOutput.SelColor = vbBlack
    
End Sub

Public Function SendAPDU(ByVal mode As Integer)

    Dim index As Integer
    Dim tempstr As String
    
    ioRequest.dwProtocol = Protocol
    ioRequest.cbPciLength = Len(ioRequest)
    
    tempstr = ""
    
    For index = 0 To SendLen - 1
        tempstr = tempstr & Right$("00" & Hex(SendBuff(index)), 2) & " "
    Next index
    
    Call DisplayOut(tempstr, 3)
    
    retCode = SCardTransmit(hCard, _
                            ioRequest, _
                            SendBuff(0), _
                            SendLen, _
                            ioRequest, _
                            RecvBuff(0), _
                            RecvLen)
                            
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(GetScardErrMsg(retCode), 2)
        SendAPDU = retCode
        Exit Function
    End If
    
    tempstr = ""
    
    Select Case mode
        Case 1
            For index = 0 To RecvLen - 1
                tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2) & " "
            Next index
              
        Case 2
            For index = RecvLen - 2 To RecvLen - 1
                If InStr(Hex(RecvBuff(index)), "A") = 2 Then
                    tempstr = tempstr & Hex(RecvBuff(index))
                Else
                    tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
                End If
            Next index
            
            If tempstr = "6A81" Then
                Call DisplayOut("This function is not supported", 2)
                SendAPDU = retCode
                Exit Function
            End If
            
            validATS = True
    End Select
    
    Call DisplayOut(tempstr, 4)
    SendAPDU = retCode
    
End Function

Private Sub btnClear_Click()

    rbOutput.Text = ""
    
End Sub

Private Sub btnConnect_Click()

    'Connect to reader using a shared connection
    retCode = SCardConnect(hContext, _
                           cbReader.Text, _
                           SCARD_SHARE_SHARED, _
                           SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                           hCard, _
                           Protocol)
                           
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(GetScardErrMsg(retCode), 2)
        Exit Sub
    Else
        Call DisplayOut("Successful connection to " & cbReader.Text, 1)
    End If
    
    btnConnect.Enabled = False
    Frame1.Enabled = True
    Frame2.Enabled = True
    
End Sub

Private Sub btnGetData_Click()

    Dim index As Integer
    Dim tempstr As String
    
    validATS = False
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &HCA
    
    If check1.Value = vbChecked Then
        SendBuff(2) = &H1
    Else
        SendBuff(2) = &H0
    End If
    
    SendBuff(3) = &H0
    SendBuff(4) = &H0
    
    SendLen = 5
    RecvLen = &HFF
    
    retCode = SendAPDU(2)
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    End If
    
    'Interpret and display return values
    If validATS Then
        If check1.Value = vbChecked Then
            tempstr = "UID: "
        Else
            tempstr = "ATS: "
        End If
        
        For index = 0 To RecvLen - 3
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2) & " "
        Next index
        
        Call DisplayOut(Trim(tempstr), 4)
    End If
    
End Sub

Private Sub btnInit_Click()

    Dim index As Integer
    
    For index = 0 To 255
        sReaderList = sReaderList & vbNullChar
    Next index
    
    ReaderCount = 255
    
    'Establish context
    retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
    
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(GetScardErrMsg(retCode), 2)
        Exit Sub
    End If
    
    'List readers
    Call cbReader.Clear
    
    retCode = SCardListReaders(hContext, _
                               sReaderGroup, _
                               sReaderList, _
                               ReaderCount)
                               
    If retCode <> SCARD_S_SUCCESS Then
        Call DisplayOut(GetScardErrMsg(retCode), 2)
        Exit Sub
    End If
    
    'Add readers to combobox control
    Call LoadListToControl(cbReader, sReaderList)
    cbReader.ListIndex = 0
    
    'Set default reader to ACR122 NFC Reader
    For index = 0 To cbReader.ListIndex - 1
        cbReader.ListIndex = index
        
        If InStr(cbReader.Text, "ACR122U") > 0 Then
            Exit For
        End If
    Next index
    
    btnConnect.Enabled = True
    btnInit.Enabled = False
        
End Sub

Private Sub btnQuit_Click()

    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    retCode = SCardReleaseContext(hContext)
    Unload Me
    
End Sub

Private Sub btnReset_Click()

    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    retCode = SCardReleaseContext(hContext)
    Call Initialize
    
End Sub

Private Sub btnSendCommand_Click()

    Dim tempdata As String
    Dim directCmd As Boolean
    Dim index As Integer
    
    directCmd = True
    
    'validate inputs
    If tbCLA.Text = "" Then
        tbCLA.Text = "00"
        tbCLA.SetFocus
        Exit Sub
    End If
    
    tempdata = ""
    
    Call ClearBuffers
    SendBuff(0) = CInt("&H" & tbCLA.Text)
    
    If tbINS.Text <> "" Then
        SendBuff(1) = CInt("&H" & tbINS.Text)
    End If
    
    If tbP1.Text <> "" Then
        directCmd = False
    End If
    
    If directCmd = False Then
        SendBuff(2) = CInt("&H" & tbP1.Text)
        
        If tbP2.Text = "" Then
            tbP2.Text = "00"
            tbP2.SetFocus
            Exit Sub
        Else
            SendBuff(3) = CInt("&H" & tbP2.Text)
        End If
        
        If tbLc.Text <> "" Then
            SendBuff(4) = CInt("&H" & tbLc.Text)
            
            'Process Data In if Lc > 0
            If SendBuff(4) > 0 Then
                tempdata = TrimInput(0, tbData.Text)
                tempdata = TrimInput(1, tempdata)
                
                'Check if Data In is consistent with Lc value
                If SendBuff(4) > Fix((Len(tempdata) / 2)) Then
                    tbData.SetFocus
                    Exit Sub
                End If
                
                For index = 0 To SendBuff(4) - 1
                    'Format Data In
                    SendBuff(index + 5) = CInt("&H" & Mid$(tempdata, ((index * 2) + 1), 2))
                Next index
                
                If tbLe.Text <> "" Then
                    SendBuff(SendBuff(4) + 5) = CInt("&H" & tbLe.Text)
                End If
            Else
                If tbLe.Text <> "" Then
                    SendBuff(5) = CInt("&H" & tbLe.Text)
                End If
            End If
        Else
            If tbLe.Text <> "" Then
                SendBuff(4) = CInt("&H" & tbLe.Text)
            End If
        End If
    End If
    
    If directCmd = True Then
        If tbINS.Text = "" Then
            SendLen = 1
        Else
            SendLen = 2
        End If
    Else
        If tbLc.Text = "" Then
            If tbLe.Text <> "" Then
                SendLen = 5
            Else
                SendLen = 4
            End If
        Else
            If tbLe.Text = "" Then
                SendLen = SendBuff(4) + 5
            Else
                SendLen = SendBuff(4) + 6
            End If
        End If
    End If
    
    RecvLen = &HFF
    
    retCode = SendAPDU(1)
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    End If
    
End Sub

Private Function TrimInput(trimType As Integer, strIn As String) As String

    Dim index As Integer
    Dim tmpStr As String
    Dim charArray(0 To 99) As String

    'place each character of string to an array
    For index = 1 To Len(strIn)
        charArray(index) = Mid$(strIn, index, 1)
    Next index

    tmpStr = ""
    strIn = Trim(strIn)
    Select Case trimType
        Case 0
            For index = 1 To Len(strIn)
                If (charArray(index) <> Chr(13)) And (charArray(index) <> Chr(10)) Then
                    tmpStr = tmpStr + charArray(index)
                End If
            Next index
    
        Case 1
            For index = 1 To Len(strIn)
                If charArray(index) <> " " Then
                    tmpStr = tmpStr + charArray(index)
                End If
            Next index
    End Select
    
    TrimInput = tmpStr

End Function

Private Sub Form_Load()

    Call Initialize
    
End Sub

Private Sub tbCLA_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbINS_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbLc_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbLe_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbP1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbP2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Public Sub Initialize()

    cbReader.Text = ""
    btnConnect.Enabled = False
    Frame1.Enabled = False
    Frame2.Enabled = False
    btnInit.Enabled = True
    tbCLA.Text = ""
    tbINS.Text = ""
    tbP1.Text = ""
    tbP2.Text = ""
    tbLc.Text = ""
    tbLe.Text = ""
    tbData.Text = ""
    Call DisplayOut("Program ready", 1)
    
End Sub
