VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form PollingMain 
   Caption         =   "ACR122 Polling"
   ClientHeight    =   6015
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8280
   Icon            =   "polling.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6015
   ScaleWidth      =   8280
   StartUpPosition =   3  'Windows Default
   Begin VB.Timer pollTimer 
      Interval        =   250
      Left            =   7920
      Top             =   0
   End
   Begin VB.CommandButton bStartPoll 
      Caption         =   "Start Polling"
      Height          =   375
      Left            =   120
      TabIndex        =   17
      Top             =   5160
      Width           =   3735
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear"
      Height          =   375
      Left            =   3960
      TabIndex        =   16
      Top             =   5160
      Width           =   1335
   End
   Begin MSComctlLib.StatusBar sbStatus 
      Align           =   2  'Align Bottom
      Height          =   375
      Left            =   0
      TabIndex        =   15
      Top             =   5640
      Width           =   8280
      _ExtentX        =   14605
      _ExtentY        =   661
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   4
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   2646
            MinWidth        =   2646
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   4410
            MinWidth        =   4410
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   2646
            MinWidth        =   2646
         EndProperty
         BeginProperty Panel4 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   4410
            MinWidth        =   4410
         EndProperty
      EndProperty
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   4935
      Left            =   3960
      TabIndex        =   4
      Top             =   120
      Width           =   4215
      _ExtentX        =   7435
      _ExtentY        =   8705
      _Version        =   393217
      TextRTF         =   $"polling.frx":17A2
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   5400
      TabIndex        =   14
      Top             =   5160
      Width           =   1335
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   6840
      TabIndex        =   13
      Top             =   5160
      Width           =   1335
   End
   Begin VB.Frame frPollOpt 
      Caption         =   "Operating Parameters"
      Height          =   3375
      Left            =   120
      TabIndex        =   5
      Top             =   1680
      Width           =   3735
      Begin VB.CheckBox cbOpt7 
         Caption         =   "Detect FeliCa 424K Cards"
         Height          =   255
         Left            =   240
         TabIndex        =   22
         Top             =   1800
         Width           =   3015
      End
      Begin VB.CheckBox cbOpt6 
         Caption         =   "Detect FeliCa 212K Cards"
         Height          =   195
         Left            =   240
         TabIndex        =   21
         Top             =   1560
         Width           =   2655
      End
      Begin VB.CheckBox cbOpt5 
         Caption         =   "Detect Topaz Cards"
         Height          =   255
         Left            =   240
         TabIndex        =   20
         Top             =   1320
         Width           =   2415
      End
      Begin VB.CheckBox cbOpt4 
         Caption         =   "Detect ISO14443 Type B Cards"
         Height          =   255
         Left            =   240
         TabIndex        =   19
         Top             =   1080
         Width           =   2775
      End
      Begin VB.CheckBox cbOpt3 
         Caption         =   "Detect ISO14443 Type A Cards"
         Height          =   255
         Left            =   240
         TabIndex        =   18
         Top             =   840
         Width           =   2775
      End
      Begin VB.CheckBox cbOpt2 
         Caption         =   "Automatic ATS Generation"
         Height          =   255
         Left            =   240
         TabIndex        =   12
         Top             =   600
         Width           =   3255
      End
      Begin VB.CheckBox cbOpt1 
         Caption         =   "Automatic PICC Polling"
         Height          =   255
         Left            =   240
         TabIndex        =   11
         Top             =   360
         Width           =   3255
      End
      Begin VB.CommandButton bSetPoll 
         Caption         =   "&Set Polling Options"
         Height          =   375
         Left            =   1680
         TabIndex        =   10
         Top             =   2760
         Width           =   1695
      End
      Begin VB.CommandButton bGetPoll 
         Caption         =   "&Get Polling Options"
         Height          =   375
         Left            =   1680
         TabIndex        =   9
         Top             =   2280
         Width           =   1695
      End
      Begin VB.Frame frPollInt 
         Caption         =   "Poll Interval"
         Height          =   1095
         Left            =   240
         TabIndex        =   6
         Top             =   2160
         Width           =   1215
         Begin VB.OptionButton rbOpt1 
            Caption         =   "250 ms"
            Height          =   255
            Left            =   120
            TabIndex        =   8
            Top             =   240
            Width           =   975
         End
         Begin VB.OptionButton rbOpt2 
            Caption         =   "500 ms"
            Height          =   255
            Left            =   120
            TabIndex        =   7
            Top             =   600
            Width           =   855
         End
      End
   End
   Begin VB.CommandButton bConn 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   2280
      TabIndex        =   3
      Top             =   1200
      Width           =   1455
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   2280
      TabIndex        =   2
      Top             =   720
      Width           =   1455
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   0
      Text            =   "Select Reader"
      Top             =   240
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   360
      Width           =   1215
   End
End
Attribute VB_Name = "PollingMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              polling.frm
'
'  Description:       This sample program outlines the steps on how to
'                     execute card detection polling functions using ACR1222
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 23, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive, detect As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen, ATRLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte
Dim ATRVal(0 To 128) As Byte

Private Sub bClear_Click()

    'clear message box
    mMsg.Text = ""

End Sub

Private Sub bConn_Click()

    'connect to reader
    Call CardConnect(0)
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    ConnActive = True
    
    'enable buttons
    frPollOpt.Enabled = True
    bStartPoll.Enabled = True

End Sub

Private Sub getParam()

    'get the PICC Operating Parameter of the reader.
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H50
    SendBuff(3) = &H0
    SendBuff(4) = &H0
    SendLen = 5
    RecvLen = 2
    
    retCode = Transmit
    
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
 
End Sub

Private Sub bGetPoll_Click()

    Dim tmpStr As String
    Dim intIndx As Integer

    Call getParam
    
    'prints the command sent
    For intIndx = 0 To SendLen - 1
        
        tmpStr = tmpStr & Format(Hex(SendBuff(intIndx)), "00") & " "
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
    'print the response recieved
    tmpStr = ""
        
    For intIndx = 0 To RecvLen - 1
        
        tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00") & " "
        
    Next intIndx
        
    Call DisplayOut(3, 0, Trim(tmpStr))
        
    'interpret the return response
    If (RecvBuff(0) And &H80) <> 0 Then
        
        Call DisplayOut(3, 0, "Automatic Polling is enabled.")
        cbOpt1.Value = vbChecked
            
    Else
    
        Call DisplayOut(3, 0, "Automatic Polling is disabled.")
        cbOpt1.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(0) And &H40) <> 0 Then
        
        Call DisplayOut(3, 0, "Automatic ATS Generation is enabled.")
        cbOpt2.Value = vbChecked
    
    Else
            
        Call DisplayOut(3, 0, "Automatic ATS Generation is disabled.")
        cbOpt2.Value = vbUnchecked
    
    End If
    
     If (RecvBuff(0) And &H20) <> 0 Then
        
        Call DisplayOut(3, 0, "250 ms.")
        rbOpt1.Value = True
    
    Else
            
        Call DisplayOut(3, 0, "500 ms.")
        rbOpt2.Value = True
    
    End If
    
     If (RecvBuff(0) And &H10) <> 0 Then
        
        Call DisplayOut(3, 0, "Detect Felica 424K Card Enabled")
        cbOpt7.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Detect Felica 424K Card Disabled")
        cbOpt7.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(0) And &H8) <> 0 Then
        
        Call DisplayOut(3, 0, "Detect Felica 212K Card Enabled")
        cbOpt6.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Detect Felica 212K Card Disabled")
        cbOpt6.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(0) And &H4) <> 0 Then
        
        Call DisplayOut(3, 0, "Detect Topaz Card Enabled")
        cbOpt5.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Detect Topaz Card Disabled")
        cbOpt5.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(0) And &H2) <> 0 Then
        
        Call DisplayOut(3, 0, "Detect ISO14443 Type B Card Enabled")
        cbOpt4.Value = vbChecked
    
    Else
    
        Call DisplayOut(3, 0, "Detect ISO14443 Type B Card Disabled")
        cbOpt4.Value = vbUnchecked
    
    End If
    
    If (RecvBuff(0) And &H1) <> 0 Then
        
        Call DisplayOut(3, 0, "Detect ISO14443 Type A Card Enabled")
        cbOpt3.Value = vbChecked
        
    Else
    
        Call DisplayOut(3, 0, "Detect ISO14443 Type A Card Disabled")
        cbOpt3.Value = vbUnchecked
    
    End If

End Sub

Private Sub bInit_Click()

Dim intIndx As Integer

    sReaderList = String(255, vbNullChar)
    ReaderCount = 255
     
    ' 1. Establish context and obtain hContext handle
    retCode = SCardEstablishContext(SCARD_SCOPE_USER, 0, 0, hContext)
  
    If retCode <> SCARD_S_SUCCESS Then
        
        Call DisplayOut(1, retCode, "")
        Exit Sub
    
    End If
  
    ' 2. List PC/SC card readers installed in the system
    retCode = SCardListReaders(hContext, sReaderGroup, sReaderList, ReaderCount)
  
    If retCode <> SCARD_S_SUCCESS Then
  
        Call DisplayOut(1, retCode, "")
        Exit Sub
    
    End If
  
    Call LoadListToControl(cbReader, sReaderList)
    cbReader.ListIndex = 0
    bInit.Enabled = False
    bConn.Enabled = True

    'Look for ACR122 and make it the default reader in the combobox
    For intIndx = 0 To cbReader.ListCount - 1
    
        cbReader.ListIndex = intIndx
    
        If InStr(cbReader.Text, "ACR122") > 0 Then
        
            Exit For
       
        End If
     
    Next intIndx

End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
  
    ' Notifications only
    Case 0
      mMsg.SelColor = &H4000
      mMsg.SelText = PrintText & vbCrLf
      
    ' Error Messages
    Case 1
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
      mMsg.SelText = PrintText & vbCrLf
      
    ' input data
    Case 2
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
      mMsg.SelText = PrintText & vbCrLf
      
    ' output data
    Case 3
      mMsg.SelColor = vbBlack
      PrintText = "> " & PrintText
      mMsg.SelText = PrintText & vbCrLf
      
    'for ACOS1 error
    Case 4
      mMsg.SelColor = vbRed
      mMsg.SelText = PrintText & vbCrLf
      
    Case 5
      sbStatus.Panels(4) = PrintText
      
    Case 6
      sbStatus.Panels(2) = PrintText
      
  End Select

  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack
 

End Sub

Private Function CardConnect(connType As Integer) As Boolean

Dim intIndx As Integer

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    'Connect
    retCode = SCardConnect(hContext, _
                        cbReader.Text, _
                        SCARD_SHARE_SHARED, _
                        SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                        hCard, _
                        Protocol)
  
    If retCode <> SCARD_S_SUCCESS Then
      
      If connType <> 1 Then
          Call DisplayOut(1, retCode, "")
      End If
      ConnActive = False
      CardConnect = retCode
      Exit Function
    
    Else
      
      If connType <> 1 Then
      
          Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
          
      End If
      
      CardConnect = retCode
      
    End If
  
End Function

Private Sub initmenu()

    mMsg.Text = ""
    cbOpt1.Value = vbUnchecked
    cbOpt2.Value = vbUnchecked
    cbOpt3.Value = vbUnchecked
    cbOpt4.Value = vbUnchecked
    cbOpt5.Value = vbUnchecked
    cbOpt6.Value = vbUnchecked
    cbOpt7.Value = vbUnchecked
    bInit.Enabled = True
    bConn.Enabled = False
    pollTimer.Enabled = False
    frPollOpt.Enabled = False
    bStartPoll.Enabled = False
    sbStatus.Panels(1) = "Card Type"
    sbStatus.Panels(3) = "Card Status"
    Call DisplayOut(0, 0, "Program ready")
    detect = False
    

End Sub

Private Sub bQuit_Click()
    
    'disconnect and quits the application
    pollTimer.Enabled = False

    If ConnActive Then
  
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
        ConnActive = False
    
    End If
  
    retCode = SCardReleaseContext(hContext)
    Unload Me

End Sub

Private Sub bReset_Click()

    'resets the connection
    bStartPoll.Caption = "StartPolling"
    sbStatus.Panels(2) = ""
    sbStatus.Panels(4) = ""
    pollTimer.Enabled = False

    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
  
    retCode = SCardReleaseContext(hCard)
    Call initmenu

End Sub

Private Sub bSetPoll_Click()

    Dim tmpStr As String
    Dim intIndx As Integer
    
    'validate input
    If ((rbOpt1.Value = False) And (rbOpt2.Value = False)) Then
       
       rbOpt1.SetFocus
       
    End If
    
    'set the PICC Operating Parameter of the reader.
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H51
    SendBuff(3) = &H0
    
    If cbOpt3.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H1
        Call DisplayOut(3, 0, "Detect ISO14443 Type A Card Enabled")
        
    Else
    
        Call DisplayOut(3, 0, "Detect ISO14443 Type A Card Disabled")
    
    End If
    
    If cbOpt4.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H2
        Call DisplayOut(3, 0, "Detect ISO14443 Type B Card Enabled")
    Else
    
        Call DisplayOut(3, 0, "Detect ISO14443 Type B Card Disabled")
        
    End If
    
    If cbOpt5.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H4
        Call DisplayOut(3, 0, "Detect Topaz Card Enabled")
        
    Else
    
        Call DisplayOut(3, 0, "Detect Topaz Card Disabled")
    
    End If
    
    If cbOpt6.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H8
        Call DisplayOut(3, 0, "Detect FeliCa 212K Card Enabled")
        
    Else
    
        Call DisplayOut(3, 0, "Detect FeliCa 212K Card Disabled")
    
    End If
    
    If cbOpt7.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H10
        Call DisplayOut(3, 0, "Detect FeliCa 424K Card Enabled")
    
    Else
        
        Call DisplayOut(3, 0, "Detect Felica 424K Card Disabled")
        
    End If
    
    If rbOpt1.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H20
        pollTimer.Interval = 250
        Call DisplayOut(3, 0, "Poll Interval is 250 ms")
    
    Else
    
        pollTimer.Interval = 500
        Call DisplayOut(3, 0, "Poll Interval is 500 ms")
    
    End If
    
    If cbOpt2.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H40
        Call DisplayOut(3, 0, "Automatic ATS Generation is Enabled")
        
    Else
    
        Call DisplayOut(3, 0, "Automatic ATS Generation is Disabled")
    
    End If

    If cbOpt1.Value = vbChecked Then
    
        SendBuff(3) = SendBuff(3) Or &H80
        Call DisplayOut(3, 0, "Automatic PICC Polling is Enabled")
        
    Else
    
        Call DisplayOut(3, 0, "Automatic PICC Polling is Disabled")
    
    End If

    SendBuff(4) = &H0
    
    SendLen = 5
    RecvLen = 1
    
    'prints the command sent
    For intIndx = 0 To SendLen - 1
        
        tmpStr = tmpStr & Format(Hex(SendBuff(intIndx)), "00") & " "
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
    retCode = Transmit
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
    
    End If
    
    'prints the response recievd
    tmpStr = ""
        
    For intIndx = 0 To RecvLen - 1
        
        tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00") & " "
        
    Next intIndx
        
    Call DisplayOut(3, 0, Trim(tmpStr))

End Sub

Private Sub bStartPoll_Click()

    If detect Then
    
        Call DisplayOut(0, 0, "Polling Stopped")
        bStartPoll.Caption = "Start Polling"
        pollTimer.Enabled = False
        sbStatus.Panels(2) = ""
        sbStatus.Panels(4) = ""
        detect = False
        Exit Sub
        
    End If
        
   Call DisplayOut(0, 0, "Polling Started")
   bStartPoll.Caption = "End Polling"
   pollTimer.Enabled = True
   detect = True
   
End Sub

Private Sub Form_Load()

    Call initmenu

End Sub

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Function Transmit() As Long

Dim tmpStr As String
Dim intIndx As Integer

    'Display Apdu In
    
    ioRequest.dwProtocol = Protocol
    ioRequest.cbPciLength = Len(ioRequest)
  
    RecvLen = 262
  
    'Issue SCardTransmit
     retCode = SCardTransmit(hCard, _
                          ioRequest, _
                          SendBuff(0), _
                          SendLen, _
                          ioRequest, _
                          RecvBuff(0), _
                          RecvLen)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
    
    End If
    
    Transmit = retCode

End Function

Private Function CheckCard() As Boolean

  'Variable declaration
    Dim TagFound As Boolean
    Dim ReaderLen, State As Long
    Dim tmpStr As String
    Dim intIndx As Integer
    Dim tmpWord As Long

    'getATR then check type of card
    tmpWord = 32
    ATRLen = tmpWord
       
    retCode = SCardStatus(hCard, _
                         cbReader.Text, _
                         ReaderLen, _
                         State, _
                         Protocol, _
                         ATRVal(0), _
                         ATRLen)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        'Call DisplayOut(1, retCode, "")
        CheckCard = False
        Exit Function
    
    Else
    
        Call InterpretATR
        CheckCard = True
    
    End If

    
End Function


Private Sub pollTimer_Timer()

    'Always use a valid connection
    retCode = CardConnect(1)
        
    If retCode <> SCARD_S_SUCCESS Then
        
       Call DisplayOut(5, 0, "No card within range.")
        sbStatus.Panels(2) = ""
       Exit Sub
        
    End If

    If CheckCard Then
    
        Call DisplayOut(5, 0, "Card is detected.")
        
    Else
    
        Call DisplayOut(5, 0, "No card within range.")
        sbStatus.Panels(2) = ""
    
    End If

End Sub

Private Sub InterpretATR()

Dim RIDVal, cardName, sATRStr, lATRStr, tmpVal As String
Dim indx, indx2 As Integer

    'Interpret ATR and guess card
    'Mifare cards using ISO 14443 Part 3 Supplemental Document
    If CInt(ATRLen) > 14 Then

        
        If ATRVal(12) = &H3 Then
        
            Select Case ATRVal(14)
                
                Case &H11
                    cardName = cardName & " FeliCa 212K"
                
                Case &H12
                    cardName = cardName & " Felica 424K"
                    
                Case &H4
                    cardName = cardName + " Topaz"
                    
            End Select
        
        End If
        
        If ATRVal(12) = &H3 Then
        
            If ATRVal(13) = &H0 Then
            
                Select Case ATRVal(14)
                
                    Case &H1
                        cardName = cardName & " Mifare Standard 1K"
                        
                    Case &H2
                        cardName = cardName & " Mifare Standard 4K"
                        
                    Case &H3
                        cardName = cardName & " Mifare Ultra light"
                        
                    Case &H4
                        cardName = cardName & " SLE55R_XXXX"
                        
                    Case &H6
                        cardName = cardName & " SR176"
                        
                    Case &H7
                        cardName = cardName & " SRI X4K"
                        
                    Case &H8
                        cardName = cardName & " AT88RF020"
                        
                    Case &H9
                        cardName = cardName & " AT88SC0204CRF"
                        
                    Case &HA
                        cardName = cardName & " AT88SC0808CRF"
                        
                    Case &HB
                         cardName = cardName & " AT88SC1616CRF"
                         
                    Case &HC
                        cardName = cardName & " AT88SC3216CRF"
                        
                    Case &HD
                        cardName = cardName & " AT88SC6416CRF"
                        
                    Case &HE
                        cardName = cardName & " SRF55V10P"
                        
                    Case &HF
                        cardName = cardName & " SRF55V02P"
                        
                    Case &H10
                        cardName = cardName & " SRF55V10S"
                        
                    Case &H11
                        cardName = cardName & " SRF55V02S"
                        
                    Case &H12
                        cardName = cardName & " TAG IT"
                        
                    Case &H13
                        cardName = cardName & " LRI512"
                        
                    Case &H14
                        cardName = cardName & " ICODESLI"
                        
                    Case &H15
                        cardName = cardName & " TEMPSENS"
        
                    Case &H16
                        cardName = cardName & " I.CODE1"
                        
                    Case &H17
                        cardName = cardName & " PicoPass 2K"
                        
                    Case &H18
                        cardName = cardName & " PicoPass 2KS"
                        
                    Case &H19
                        cardName = cardName & " PicoPass 16K"
                        
                    Case &H1A
                        cardName = cardName & " PicoPass 16KS"
                        
                    Case &H1B
                        cardName = cardName & " PicoPass 16K(8x2)"
                        
                    Case &H1C
                        cardName = cardName & " PicoPass 16KS(8x2)"

                    Case &H1D
                        cardName = cardName & " PicoPass 32KS(16+16)"
            
                    Case &H1E
                        cardName = cardName & " PicoPass 32KS(16+8x2)"
            
                    Case &H1F
                        cardName = cardName & " PicoPass 32KS(8x2+16)"
            
                    Case &H20
                        cardName = cardName & " PicoPass 32KS(8x2+8x2)"
            
                    Case &H21
                        cardName = cardName & " LRI64"
            
                    Case &H22
                        cardName = cardName & " I.CODE UID"
                
                    Case &H23
                        cardName = cardName & " I.CODE EPC"
            
                    Case &H24
                        cardName = cardName & " LRI12"
            
                    Case &H25
                        cardName = cardName & " LRI128"
            
                    Case &H26
                        cardName = cardName & " Mifare Mini"
            
                End Select
            
            Else
            
                If ATRVal(13) = &HFF Then
                
                    Select Case ATRVal(14)
                    
                        Case &H9
                            cardName = cardName & " Mifare Mini"
                    
                    End Select
                
                End If
            
            End If
            
            Call DisplayOut(6, 0, cardName & " is detected.")
        
        End If
        
    End If
    
    
    'Mifare DESFire card using ISO 14443 Part 4
    If CInt(ATRLen) = 11 Then
    
        RIDVal = ""
        
        For indx = 4 To 9
        
            RIDVal = RIDVal & Format(Hex(ATRVal(indx)), "00")
        
        Next indx

        If RIDVal = "067577810280" Then
        
            Call DisplayOut(6, 0, "Mifare DESFire is detected.")
        
        End If
    
    End If
    
    'Other cards using ISO 14443 Part 4
    If CInt(ATRLen) = 17 Then
    
        RIDVal = ""
        
        For indx = 4 To 15
        
            RIDVal = RIDVal & Format(Hex(ATRVal(indx)), "00")
        
        Next indx
        
        If RIDVal = "50122345561253544E3381C3" Then
        
            Call DisplayOut(6, 0, "ST19XRC8E is detected.")
        
        End If
    
    End If
    
    'other cards using ISO 14443 Type A or B
    If lATRStr = "3B8X800150" Then
    
        Call DisplayOut(6, 0, "ISO 14443B is detected.")
        
    Else
    
        If sATRStr = "3B8X8001" Then
        
            Call DisplayOut(6, 0, "ISO 14443A is detected.")
        
        End If
    
    End If
    
End Sub

