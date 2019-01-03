VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form PassiveMain 
   Caption         =   "Passive Device Sample"
   ClientHeight    =   5925
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   8055
   Icon            =   "PassiveSample.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   5925
   ScaleWidth      =   8055
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   1680
      TabIndex        =   9
      Top             =   5400
      Width           =   1935
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   1680
      TabIndex        =   8
      Top             =   4920
      Width           =   1935
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear Output"
      Height          =   375
      Left            =   1680
      TabIndex        =   7
      Top             =   4440
      Width           =   1935
   End
   Begin VB.Frame frRecvData 
      Caption         =   "Recieve Data"
      Height          =   2175
      Left            =   0
      TabIndex        =   5
      Top             =   2160
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
         Enabled         =   -1  'True
         TextRTF         =   $"PassiveSample.frx":17A2
      End
   End
   Begin VB.CommandButton bSetMode 
      Caption         =   "&Set Passive Mode and Recieve Data"
      Height          =   375
      Left            =   720
      TabIndex        =   4
      Top             =   1560
      Width           =   2895
   End
   Begin VB.CommandButton bConn 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   720
      TabIndex        =   3
      Top             =   1080
      Width           =   2895
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initalize"
      Height          =   375
      Left            =   720
      TabIndex        =   2
      Top             =   600
      Width           =   2895
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1080
      TabIndex        =   1
      Top             =   120
      Width           =   2535
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5775
      Left            =   3720
      TabIndex        =   10
      Top             =   0
      Width           =   4215
      _ExtentX        =   7435
      _ExtentY        =   10186
      _Version        =   393217
      Enabled         =   -1  'True
      TextRTF         =   $"PassiveSample.frx":1824
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   0
      TabIndex        =   0
      Top             =   240
      Width           =   1215
   End
End
Attribute VB_Name = "PassiveMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd
'
'  File :         PassiveSample.frm
'
'  Description:     This sample program outlines the steps on how to
'                   set an ACR122 NFC reader to its passive mode and
'                   receive data
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
        frRecvData.Enabled = False
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
    frRecvData.Enabled = True
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

Call SetPassive

End Sub

Private Sub Form_Load()

    Call Initialize

End Sub


Public Sub RecvData()

    Dim tempdata(0 To 512) As String
    Dim data As String
    Dim datalen As Integer
    Dim index As Integer
    
    'Receive first the length of
    'the actual data to be received
    Call ClearBuffers
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
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If
    
    datalen = RecvBuff(3)

    'Send a response with a value of 90 00
    'to the sending device
    Call ClearBuffers
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
    If retCode <> SCARD_S_SUCCESS Then
        
        Exit Sub
        
    End If

    'We receive the actual data
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HD4
    SendBuff(6) = &H86

    SendLen = 7
    RecvLen = datalen + 5

    retCode = CardControl()
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    End If

    For index = 3 To RecvLen - 3
        data = data & Chr(RecvBuff(index))
    Next

    'We send the response with a value of 90 00
    'to the sending device
    Call ClearBuffers
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
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    End If

    mData.Text = data


End Sub

Private Sub SetPassive()

Dim tempStr As String
Dim index As Integer

    Call ClearBuffers
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

    
    retCode = CardControl
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    For index = RecvLen - 2 To RecvLen - 1
    
        tempStr = tempStr & Right$("00" & Hex(RecvBuff(index)), 2)
        
    Next index
    
    'Check for response
    If tempStr <> "9000" Then
    
        Call DisplayOut(2, 0, "Set passive failed!")
        
    End If

    Call RecvData
    
End Sub
