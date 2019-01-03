VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form ActiveMain 
   Caption         =   "Active Device Sample"
   ClientHeight    =   6015
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8175
   Icon            =   "ActiveSample.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6015
   ScaleWidth      =   8175
   StartUpPosition =   3  'Windows Default
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5775
      Left            =   3840
      TabIndex        =   10
      Top             =   120
      Width           =   4215
      _ExtentX        =   7435
      _ExtentY        =   10186
      _Version        =   393217
      TextRTF         =   $"ActiveSample.frx":17A2
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   1800
      TabIndex        =   9
      Top             =   5520
      Width           =   1935
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   1800
      TabIndex        =   8
      Top             =   5040
      Width           =   1935
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Output"
      Height          =   375
      Left            =   1800
      TabIndex        =   7
      Top             =   4560
      Width           =   1935
   End
   Begin VB.Frame frSendData 
      Caption         =   "Send Data"
      Height          =   2175
      Left            =   120
      TabIndex        =   5
      Top             =   2280
      Width           =   3615
      Begin RichTextLib.RichTextBox mData 
         Height          =   1815
         Left            =   120
         TabIndex        =   6
         Top             =   240
         Width           =   3375
         _ExtentX        =   5953
         _ExtentY        =   3201
         _Version        =   393217
         TextRTF         =   $"ActiveSample.frx":1824
      End
   End
   Begin VB.CommandButton bSetMode 
      Caption         =   "&Set Active Mode and Send Data"
      Height          =   375
      Left            =   1200
      TabIndex        =   4
      Top             =   1680
      Width           =   2535
   End
   Begin VB.CommandButton bConn 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   1200
      TabIndex        =   3
      Top             =   1200
      Width           =   2535
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initalize"
      Height          =   375
      Left            =   1200
      TabIndex        =   2
      Top             =   720
      Width           =   2535
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   1
      Top             =   240
      Width           =   2535
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   360
      Width           =   1215
   End
End
Attribute VB_Name = "ActiveMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd
'
'  File :         ActiveSample.frm
'
'  Description:     This sample program outlines the steps on how to
'                   set an ACR122 NFC reader to its active mode and
'                   send data
'
'  Author :         M.J.E.C.Castillo
'
'  Date   :         August 4, 2008
'
' Revision Trail:   (Date/Author/Description)
'
'==========================================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen, nBytesRet As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

Private Sub ClearBuffers()

  Dim intIndx As Long
  
  For intIndx = 0 To 262
  
    RecvBuff(intIndx) = &H0
    SendBuff(intIndx) = &H0
    
  Next intIndx
  
End Sub

Private Sub DisplayOut(ByVal mType As Integer, ByVal msgCode As Long, ByVal PrintText As String)

  Select Case mType
  
    Case 0                   ' Notifications only
      mMsg.SelColor = &H4000
      
    Case 1                   ' Error Messages
      mMsg.SelColor = vbRed
      PrintText = GetScardErrMsg(retCode)
      
    Case 2
      mMsg.SelColor = vbBlack
      PrintText = "< " & PrintText
      
    Case 3
      mMsg.SelColor = vbBlack
      PrintText = "> " & PrintText
      
  End Select
  
  mMsg.SelText = PrintText & vbCrLf
  mMsg.SelStart = Len(mMsg.Text)
  mMsg.SelColor = vbBlack

End Sub

Private Function CardControl() As Long

Dim tmpStr As String
Dim intIndx As Integer

    'Display Apdu In
    tmpStr = "SCardControl: "
    
    For intIndx = 0 To SendLen - 1
        
        'checks if hex value ends in A since VB6 does not format it properly
        If InStr(Hex(SendBuff(intIndx)), "A") = 2 Then
            
            tmpStr = tmpStr & Hex(SendBuff(intIndx)) & " "
            
        Else
    
            tmpStr = tmpStr & Right$("00" & Hex(SendBuff(intIndx)), 2) & " "
        
        End If
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
    retCode = SCardControl(hCard, _
                        IOCTL_CCID_ESCAPE_SCARD_CTL_CODE, _
                        SendBuff(0), _
                        SendLen, _
                        RecvBuff(0), _
                        RecvLen, _
                        nBytesRet)
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
        CardControl = retCode
        Exit Function
        
    End If
    
        tmpStr = ""
        
        For intIndx = 0 To (RecvLen - 1)
        
            tmpStr = tmpStr & Right$("00" & Hex(RecvBuff(intIndx)), 2) & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
    CardControl = retCode

End Function

Private Sub GetFirmware()

Dim tmpStr As String
Dim intIndx As Integer

    Call ClearBuffers
    
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H48
    SendBuff(3) = &H0
    SendBuff(4) = &H0
    
    SendLen = 5
    RecvLen = 10
    
    retCode = CardControl
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    tmpStr = "Firmware Version: "
    
    For intIndx = 0 To RecvLen
        
        If RecvBuff(intIndx) <> &H0 Then
        
            tmpStr = tmpStr & Chr(RecvBuff(intIndx))
        
        End If
        
    Next intIndx
    
    Call DisplayOut(3, 0, tmpStr)

End Sub

Public Sub Initialize()

        bInit.Enabled = True
        bConn.Enabled = False
        frSendData.Enabled = False
        bSetMode.Enabled = False
        cbReader.Text = ""
        mData.Text = ""
        bReset.Enabled = False
        Call DisplayOut(0, 0, "Program ready")

    End Sub

Private Sub bClear_Click()

    'clear the message area
    mMsg.Text = ""
    
End Sub

Private Sub bConn_Click()

    If ConnActive Then
  
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
  
    End If

    retCode = SCardConnect(hContext, _
                            cbReader.Text, _
                            SCARD_SHARE_SHARED, _
                            SCARD_PROTOCOL_T1, _
                            hCard, _
                            Protocol)
    If retCode <> SCARD_S_SUCCESS Then
    
       'Connect to reader directly if no card is polled
        retCode = SCardConnect(hContext, _
                            cbReader.Text, _
                            SCARD_SHARE_DIRECT, _
                            0, _
                            hCard, _
                            Protocol)
                            
        If retCode <> SCARD_S_SUCCESS Then
            Call DisplayOut(1, retCode, "")
        '    ConnActive = False
            Exit Sub
        Else
        
            Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
        
        End If
  
    Else
        
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
        
    End If
    
    Call GetFirmware

    'add buttons
    frSendData.Enabled = True
    bSetMode.Enabled = True
    
    
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
    bReset.Enabled = True
    bConn.Enabled = True

  'Look for ACR122 and make it the default reader in the combobox
  For intIndx = 0 To cbReader.ListCount - 1
    
    cbReader.ListIndex = intIndx
    
    If InStr(cbReader.Text, "ACR122") > 0 Then
        
        Exit For
       
    End If
     
  Next intIndx
  
End Sub

Private Sub bQuit_Click()

    'diconnect and exit application
    If ConnActive Then
    
      retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
      ConnActive = False
      
    End If
    
    retCode = SCardReleaseContext(hContext)
    Unload Me
  
End Sub

Private Sub bReset_Click()

    'resets the connection
    If ConnActive Then
    
        retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    
    End If
    
    retCode = SCardReleaseContext(hCard)
    Call Initialize
    
End Sub

Private Sub bSetMode_Click()

Call SetActive

End Sub

Private Sub Form_Load()

    Call Initialize

End Sub

Private Sub SetActive()

Dim tempStr As String
Dim index As Integer

    Call ClearBuffers
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
    
    retCode = CardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    For index = RecvLen - 2 To RecvLen - 1
    
         tempStr = tempStr & Right$("00" & Hex(RecvBuff(index)), 2)
         
     Next index
     
     'Check for response
     If tempStr <> "9000" Then
     
         Call DisplayOut(2, 0, "Set active failed!")
         
     End If
    
    Call Sending

End Sub

Public Sub Sending()

    Dim tempdata(1 To 512) As String
    Dim data As String
    Dim datalen As Integer
    Dim index As Integer
    
    'Transfer string data to a char arra
    'and determine its length
    
    data = mData.Text
    datalen = Len(data)
    
    For index = 1 To datalen
    
        tempdata(index) = Mid$(data, index, 1)
    
    Next index

    'Send the length of the data first
    'so that the receiving device would
    'know how long the data would be
        
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H1
    SendBuff(5) = &HD4
    SendBuff(6) = &H40
    SendBuff(7) = &H1
    SendBuff(8) = datalen
    
    SendLen = 9
    RecvLen = 7
    
    retCode = CardControl
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    'send actual data
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = datalen
    SendBuff(5) = &HD4
    SendBuff(6) = &H40
    SendBuff(7) = &H1
    
    For index = 0 To datalen - 1
    
        SendBuff(index + 8) = Asc(tempdata(index + 1))
    
    Next index
    
    SendLen = datalen + 8
    RecvLen = 7
    
    retCode = CardControl
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub
