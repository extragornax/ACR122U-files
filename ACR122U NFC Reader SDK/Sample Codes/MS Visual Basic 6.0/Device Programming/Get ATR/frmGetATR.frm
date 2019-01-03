VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form frmGetATR 
   Caption         =   "Get ATR"
   ClientHeight    =   4965
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   9540
   Icon            =   "frmGetATR.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   4965
   ScaleWidth      =   9540
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton btnQuit 
      Caption         =   "Quit"
      Height          =   375
      Left            =   2280
      TabIndex        =   8
      Top             =   4440
      Width           =   1575
   End
   Begin VB.CommandButton btnReset 
      Caption         =   "Reset"
      Height          =   375
      Left            =   2280
      TabIndex        =   7
      Top             =   3960
      Width           =   1575
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear Output"
      Height          =   375
      Left            =   2280
      TabIndex        =   6
      Top             =   3480
      Width           =   1575
   End
   Begin RichTextLib.RichTextBox rbOutput 
      Height          =   4695
      Left            =   3960
      TabIndex        =   5
      Top             =   120
      Width           =   5415
      _ExtentX        =   9551
      _ExtentY        =   8281
      _Version        =   393217
      Enabled         =   -1  'True
      TextRTF         =   $"frmGetATR.frx":17A2
   End
   Begin VB.CommandButton btnATR 
      Caption         =   "Get ATR"
      Height          =   375
      Left            =   2280
      TabIndex        =   4
      Top             =   1560
      Width           =   1575
   End
   Begin VB.CommandButton btnConnect 
      Caption         =   "Connect"
      Height          =   375
      Left            =   2280
      TabIndex        =   3
      Top             =   1080
      Width           =   1575
   End
   Begin VB.CommandButton btnInit 
      Caption         =   "Initialize"
      Height          =   375
      Left            =   2280
      TabIndex        =   2
      Top             =   600
      Width           =   1575
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   120
      Width           =   2535
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
Attribute VB_Name = "frmGetATR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              frmGetATR.frm
'
'  Description:       This sample program outlines the steps on how to
'                     get ATR from cards using ACR122
'
'  Author:            Wazer Emmanuel R. Benal
'
'  Date:              June 2, 2008
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
Dim SendLen, RecvLen, ATRLen As Long
Dim SendBuff(0 To 255) As Byte
Dim RecvBuff(0 To 255) As Byte
Dim validATS As Boolean
Dim ATRVal(0 To 128) As Byte

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

Private Sub btnATR_Click()

    Dim ReaderLen, State As Long
    Dim tmpStr As String
    Dim intIndx As Integer
    Dim tmpWord As Long

    Call DisplayOut("Invoke SCardStatus", 1)
    'Invoke SCardStatus using hCard handle and valid reader name
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
        Call DisplayOut(GetScardErrMsg(retCode), 1)
    Else
        'Format ATRVal returned and display string as ATR value
        tmpStr = "ATR Length: " & CInt(ATRLen)
        Call DisplayOut(tmpStr, 4)
        tmpStr = "ATR Value: "
        
        For intIndx = 0 To CInt(ATRLen) - 1
           tmpStr = tmpStr & Format(Hex(ATRVal(intIndx)), "00") & " "
        Next intIndx
        
        Call DisplayOut(tmpStr, 4)
        
        'Interpret dwActProtocol returned and display as active protocol
        tmpStr = "Active Protocol: "
        
        Select Case CInt(Protocol)
            
            Case 1
                tmpStr = tmpStr & "T=0"
            Case 2
                If InStr(cbReader.Text, "ACR128U PICC") > 0 Then
                    tmpStr = tmpStr & "T=CL"
                Else
                    tmpStr = tmpStr & "T=1"
                End If
            Case Else
                tmpStr = "No protocol is defined."
        
        End Select
        
        Call DisplayOut(tmpStr, 4)
        Call InterpretATR
    
    End If
    
End Sub

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
    btnATR.Enabled = True
    
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

Private Sub InterpretATR()

Dim RIDVal, cardName, sATRStr, lATRStr, tmpVal As String
Dim indx, indx2 As Integer

    '4. Interpret ATR and guess card
    ' 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
    If CInt(ATRLen) > 14 Then
    
        RIDVal = ""
        sATRStr = ""
        lATRStr = ""
        
        For indx = 7 To 11
        
            RIDVal = RIDVal & Format(Hex(ATRVal(indx)), "00")
            
        Next indx
        
        
        For indx = 0 To 4
            
            'shift bit to right
            tmpVal = ATRVal(indx)
            For indx2 = 1 To 4
        
                tmpVal = tmpVal / 2
        
            Next indx2
        
            If ((indx = 1) And (tmpVal = 8)) Then
            
                lATRStr = lATRStr & "8X"
                sATRStr = sATRStr & "8X"
            
            Else
            
                If indx = 4 Then
                
                    lATRStr = lATRStr & Format(Hex(ATRVal(indx)), "00")
                    
                Else
                
                    lATRStr = lATRStr & Format(Hex(ATRVal(indx)), "00")
                    sATRStr = sATRStr & Format(Hex(ATRVal(indx)), "00")
                
                End If
            
            End If
        
        Next indx
        
        If RIDVal = "A000000306" Then
        
            cardName = ""
            
            Select Case ATRVal(12)
            
                Case 0
                    cardName = "No card information"
                    
                Case 1
                    cardName = "ISO 14443 A, Part1 Card Type"
                    
                Case 2
                    cardName = "ISO 14443 A, Part2 Card Type"
                    
                Case 3
                    cardName = "ISO 14443 A, Part3 Card Type"
                
                Case 5
                    cardName = "ISO 14443 B, Part1 Card Type"
                
                Case 6
                    cardName = "ISO 14443 B, Part2 Card Type"
                    
                Case 7
                    cardName = "ISO 14443 B, Part3 Card Type"
                    
                Case 9
                    cardName = "ISO 15693, Part1 Card Type"
                    
                Case 10
                    cardName = "ISO 15693, Part2 Card Type"
                    
                Case 11
                    cardName = "ISO 15693, Part3 Card Type"
                    
                Case 12
                    cardName = "ISO 15693, Part4 Card Type"
                    
                Case 13
                    cardName = "Contact Card (7816-10) IIC Card Type"
                    
                Case 14
                    cardName = "Contact Card (7816-10) Extended IIC Card Type"
                    
                Case 15
                    cardName = "Contact Card (7816-10) 2WBP Card Type"
                    
                Case 16
                    cardName = "Contact Card (7816-10) 3WBP Card Type"
                    
                Case Else
                    cardName = "Undefined card"
            
            End Select
        
        End If
        
        If ATRVal(12) = &H3 Then
        
            If ATRVal(13) = &H0 Then
            
                Select Case ATRVal(14)
                
                    Case &H1
                        cardName = cardName & ": Mifare Standard 1K"
                        
                    Case &H2
                        cardName = cardName & ": Mifare Standard 4K"
                        
                    Case &H3
                        cardName = cardName & ": Mifare Ultra light"
                        
                    Case &H4
                        cardName = cardName & ": SLE55R_XXXX"
                        
                    Case &H6
                        cardName = cardName & ": SR176"
                        
                    Case &H7
                        cardName = cardName & ": SRI X4K"
                        
                    Case &H8
                        cardName = cardName & ": AT88RF020"
                        
                    Case &H9
                        cardName = cardName & ": AT88SC0204CRF"
                        
                    Case &HA
                        cardName = cardName & ": AT88SC0808CRF"
                        
                    Case &HB
                         cardName = cardName & ": AT88SC1616CRF"
                         
                    Case &HC
                        cardName = cardName & ": AT88SC3216CRF"
                        
                    Case &HD
                        cardName = cardName & ": AT88SC6416CRF"
                        
                    Case &HE
                        cardName = cardName & ": SRF55V10P"
                        
                    Case &HF
                        cardName = cardName & ": SRF55V02P"
                        
                    Case &H10
                        cardName = cardName & ": SRF55V10S"
                        
                    Case &H11
                        cardName = cardName & ": SRF55V02S"
                        
                    Case &H12
                        cardName = cardName & ": TAG IT"
                        
                    Case &H13
                        cardName = cardName & ": LRI512"
                        
                    Case &H14
                        cardName = cardName & ": ICODESLI"
                        
                    Case &H15
                        cardName = cardName & ": TEMPSENS"
        
                    Case &H16
                        cardName = cardName & ": I.CODE1"
                        
                    Case &H17
                        cardName = cardName & ": PicoPass 2K"
                        
                    Case &H18
                        cardName = cardName & ": PicoPass 2KS"
                        
                    Case &H19
                        cardName = cardName & ": PicoPass 16K"
                        
                    Case &H1A
                        cardName = cardName & ": PicoPass 16KS"
                        
                    Case &H1B
                        cardName = cardName & ": PicoPass 16K(8x2)"
                        
                    Case &H1C
                        cardName = cardName & ": PicoPass 16KS(8x2)"

                    Case &H1D
                        cardName = cardName & ": PicoPass 32KS(16+16)"
            
                    Case &H1E
                        cardName = cardName & ": PicoPass 32KS(16+8x2)"
            
                    Case &H1F
                        cardName = cardName & ": PicoPass 32KS(8x2+16)"
            
                    Case &H20
                        cardName = cardName & ": PicoPass 32KS(8x2+8x2)"
            
                    Case &H21
                        cardName = cardName & ": LRI64"
            
                    Case &H22
                        cardName = cardName & ": I.CODE UID"
                
                    Case &H23
                        cardName = cardName & ": I.CODE EPC"
            
                    Case &H24
                        cardName = cardName & ": LRI12"
            
                    Case &H25
                        cardName = cardName & ": LRI128"
            
                    Case &H26
                        cardName = cardName & ": Mifare Mini"
            
                End Select
            
            Else
            
                If ATRVal(13) = &HFF Then
                
                    Select Case ATRVal(14)
                    
                        Case &H9
                            cardName = cardName & ": Mifare Mini"
                    
                    End Select
                
                End If
            
            End If
            
            Call DisplayOut(cardName & " is detected", 4)
        
        End If
        
    End If
    
    '4.2. Mifare DESFire card using ISO 14443 Part 4
    If CInt(ATRLen) = 11 Then
    
        RIDVal = ""
        
        For indx = 4 To 9
        
            RIDVal = RIDVal & Format(Hex(RecvBuff(indx)), "00")
        
        Next indx
        
        If RIDVal = "067577810280" Then
        
            Call DisplayOut("Mifare DESFire is detected", 4)
        
        End If
    
    End If
    
    '4.3. Other cards using ISO 14443 Part 4
    If CInt(ATRLen) = 17 Then
    
        RIDVal = ""
        
        For indx = 4 To 15
        
            RIDVal = RIDVal & Format(Hex(RecvBuff(indx)), "00")
        
        Next indx
        
        If RIDVal = "50122345561253544E3381C3" Then
        
            Call DisplayOut("ST19XRC8E is detected", 4)
        
        End If
    
    End If
    
    '4.4. other cards using ISO 14443 Type A or B
    If lATRStr = "3B8X800150" Then
    
        Call DisplayOut("ISO 14443B is detected", 4)
        
    Else
    
        If sATRStr = "3B8X8001" Then
        
            Call DisplayOut("ISO 14443A is detected", 4)
        
        End If
    
    End If
    
End Sub

Public Sub Initialize()

    btnInit.Enabled = True
    btnConnect.Enabled = False
    btnATR.Enabled = False
    rbOutput.Text = ""
    Call DisplayOut("Program ready", 1)
    
End Sub

Private Sub btnQuit_Click()

    Unload Me
    
End Sub

Private Sub btnReset_Click()

    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    retCode = SCardReleaseContext(hContext)
    Call Initialize
    
End Sub

Private Sub Form_Load()
    
    Call Initialize
    
End Sub
