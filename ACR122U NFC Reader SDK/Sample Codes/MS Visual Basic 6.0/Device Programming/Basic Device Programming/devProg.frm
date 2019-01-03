VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form devProgMain 
   Caption         =   "ACR122 Device Programming"
   ClientHeight    =   6030
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11790
   Icon            =   "devProg.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   6030
   ScaleWidth      =   11790
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton bGetStat 
      Caption         =   "&Get Status"
      Height          =   375
      Left            =   1920
      TabIndex        =   56
      Top             =   1080
      Width           =   1695
   End
   Begin VB.Frame frAntenna 
      Caption         =   "Antenna Settings"
      Height          =   735
      Left            =   120
      TabIndex        =   52
      Top             =   1560
      Width           =   3495
      Begin VB.CommandButton bSetAntenna 
         Caption         =   "Set &Antenna"
         Height          =   375
         Left            =   1920
         TabIndex        =   55
         Top             =   240
         Width           =   1335
      End
      Begin VB.OptionButton rbAntOff 
         Caption         =   "Off"
         Height          =   255
         Left            =   960
         TabIndex        =   54
         Top             =   360
         Width           =   855
      End
      Begin VB.OptionButton rbAntOn 
         Caption         =   "On"
         Height          =   255
         Left            =   240
         TabIndex        =   53
         Top             =   360
         Width           =   1215
      End
   End
   Begin VB.CommandButton bSetLED 
      Caption         =   "&Set Bi-Color LED/Buzzer Control"
      Height          =   375
      Left            =   3720
      TabIndex        =   51
      Top             =   5520
      Width           =   3255
   End
   Begin VB.Frame frBlinkDuration 
      Caption         =   "Blinking Duration Control"
      Height          =   5295
      Left            =   3720
      TabIndex        =   35
      Top             =   120
      Width           =   3255
      Begin VB.Frame FrLinktoBuzz 
         Caption         =   "Link to Buzzer"
         Height          =   1935
         Left            =   120
         TabIndex        =   46
         Top             =   3120
         Width           =   2895
         Begin VB.OptionButton rbLinkToBuzzOpt4 
            Caption         =   "T1 and T2 Duration"
            Height          =   255
            Left            =   120
            TabIndex        =   50
            Top             =   1440
            Width           =   1695
         End
         Begin VB.OptionButton rbLinkToBuzzOpt3 
            Caption         =   "T2 Duration"
            Height          =   255
            Left            =   120
            TabIndex        =   49
            Top             =   1080
            Width           =   1335
         End
         Begin VB.OptionButton rbLinkToBuzzOpt2 
            Caption         =   "T1 Duration"
            Height          =   255
            Left            =   120
            TabIndex        =   48
            Top             =   720
            Width           =   1215
         End
         Begin VB.OptionButton rbLinkToBuzzOpt1 
            Caption         =   "Buzzer Off"
            Height          =   255
            Left            =   120
            TabIndex        =   47
            Top             =   360
            Width           =   1335
         End
      End
      Begin VB.TextBox tRepeat 
         Alignment       =   1  'Right Justify
         Height          =   285
         Left            =   2040
         MaxLength       =   2
         TabIndex        =   45
         Top             =   2640
         Width           =   615
      End
      Begin VB.Frame frT2 
         Caption         =   "T2 Duration"
         Height          =   975
         Left            =   120
         TabIndex        =   40
         Top             =   1440
         Width           =   2895
         Begin VB.TextBox tT2 
            Alignment       =   1  'Right Justify
            Height          =   285
            Left            =   1320
            MaxLength       =   2
            TabIndex        =   42
            Top             =   360
            Width           =   615
         End
         Begin VB.Label Label5 
            Caption         =   "x 100 ms"
            Height          =   255
            Left            =   2040
            TabIndex        =   43
            Top             =   360
            Width           =   735
         End
         Begin VB.Label Label4 
            Caption         =   "Toggle Blinking State"
            Height          =   375
            Left            =   120
            TabIndex        =   41
            Top             =   360
            Width           =   1095
         End
      End
      Begin VB.Frame frT1 
         Caption         =   "T1 Duration"
         Height          =   975
         Left            =   120
         TabIndex        =   36
         Top             =   360
         Width           =   2895
         Begin VB.TextBox tT1 
            Alignment       =   1  'Right Justify
            Height          =   285
            Left            =   1320
            MaxLength       =   2
            TabIndex        =   38
            Top             =   360
            Width           =   615
         End
         Begin VB.Label Label3 
            Caption         =   "x 100 ms"
            Height          =   255
            Left            =   2040
            TabIndex        =   39
            Top             =   360
            Width           =   735
         End
         Begin VB.Label Label2 
            Caption         =   "Inital Blinking State"
            Height          =   375
            Left            =   120
            TabIndex        =   37
            Top             =   360
            Width           =   1215
         End
      End
      Begin VB.Label Label6 
         Caption         =   "Number of Repetitions: "
         Height          =   255
         Left            =   240
         TabIndex        =   44
         Top             =   2640
         Width           =   1815
      End
   End
   Begin VB.Frame frGreen 
      Caption         =   "Green LED"
      Height          =   1695
      Left            =   120
      TabIndex        =   22
      Top             =   4200
      Width           =   3495
      Begin VB.Frame frBlinkMask 
         Caption         =   "LED Blinking Mask"
         Height          =   615
         Left            =   1800
         TabIndex        =   32
         Top             =   960
         Width           =   1575
         Begin VB.OptionButton rbGreenBlinkMaskOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   34
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbGreenBlinkMaskOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   33
            Top             =   240
            Width           =   855
         End
      End
      Begin VB.Frame frGreenInit 
         Caption         =   "Inital Blinking State"
         Height          =   615
         Left            =   120
         TabIndex        =   29
         Top             =   960
         Width           =   1575
         Begin VB.OptionButton rbGreenInitOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   31
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbGreenInitOn 
            Caption         =   "On"
            Height          =   195
            Left            =   120
            TabIndex        =   30
            Top             =   240
            Width           =   615
         End
      End
      Begin VB.Frame frGreenStateMask 
         Caption         =   "LED State Mask"
         Height          =   615
         Left            =   1800
         TabIndex        =   26
         Top             =   240
         Width           =   1575
         Begin VB.OptionButton rbGreenStateMaskOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   28
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbGreenStateMaskOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   27
            Top             =   240
            Width           =   735
         End
      End
      Begin VB.Frame frGreenFinalState 
         Caption         =   "Final LED State"
         Height          =   615
         Left            =   120
         TabIndex        =   23
         Top             =   240
         Width           =   1575
         Begin VB.OptionButton rbGreenFinalOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   25
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbGreenFinalOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   24
            Top             =   240
            Width           =   615
         End
      End
   End
   Begin VB.Frame frRed 
      Caption         =   "Red LED"
      Height          =   1695
      Left            =   120
      TabIndex        =   9
      Top             =   2400
      Width           =   3495
      Begin VB.Frame frRedBlinkMask 
         Caption         =   "LED Blinking Mask"
         Height          =   615
         Left            =   1800
         TabIndex        =   19
         Top             =   960
         Width           =   1575
         Begin VB.OptionButton rbRedBlinkMaskOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   21
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbRedBlinkMaskOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   20
            Top             =   240
            Width           =   615
         End
      End
      Begin VB.Frame frRedInit 
         Caption         =   "Initial Blinking State"
         Height          =   615
         Left            =   120
         TabIndex        =   16
         Top             =   960
         Width           =   1575
         Begin VB.OptionButton rbRedInitOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   18
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbRedInitOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   17
            Top             =   240
            Width           =   615
         End
      End
      Begin VB.Frame frRedStateMask 
         Caption         =   "LED State Mask"
         Height          =   615
         Left            =   1800
         TabIndex        =   13
         Top             =   240
         Width           =   1575
         Begin VB.OptionButton rbRedStateMaskOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   15
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbRedStateMaskOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   14
            Top             =   240
            Width           =   615
         End
      End
      Begin VB.Frame frRedFinal 
         Caption         =   "Final LED State"
         Height          =   615
         Left            =   120
         TabIndex        =   10
         Top             =   240
         Width           =   1575
         Begin VB.OptionButton rbRedFinalOff 
            Caption         =   "Off"
            Height          =   255
            Left            =   840
            TabIndex        =   12
            Top             =   240
            Width           =   615
         End
         Begin VB.OptionButton rbRedFinalOn 
            Caption         =   "On"
            Height          =   255
            Left            =   120
            TabIndex        =   11
            Top             =   240
            Width           =   1095
         End
      End
   End
   Begin VB.CommandButton bGetFW 
      Caption         =   "Get &Firmware Version"
      Height          =   375
      Left            =   120
      TabIndex        =   8
      Top             =   1080
      Width           =   1695
   End
   Begin VB.CommandButton bClear 
      Caption         =   "C&lear"
      Height          =   375
      Left            =   7080
      TabIndex        =   7
      Top             =   5520
      Width           =   1455
   End
   Begin VB.CommandButton bReset 
      Caption         =   "&Reset"
      Height          =   375
      Left            =   8640
      TabIndex        =   6
      Top             =   5520
      Width           =   1455
   End
   Begin VB.CommandButton bQuit 
      Caption         =   "&Quit"
      Height          =   375
      Left            =   10200
      TabIndex        =   5
      Top             =   5520
      Width           =   1455
   End
   Begin RichTextLib.RichTextBox mMsg 
      Height          =   5295
      Left            =   7080
      TabIndex        =   4
      Top             =   120
      Width           =   4575
      _ExtentX        =   8070
      _ExtentY        =   9340
      _Version        =   393217
      ScrollBars      =   2
      TextRTF         =   $"devProg.frx":17A2
   End
   Begin VB.CommandButton bConn 
      Caption         =   "&Connect"
      Height          =   375
      Left            =   1920
      TabIndex        =   3
      Top             =   600
      Width           =   1695
   End
   Begin VB.CommandButton bInit 
      Caption         =   "&Initialize"
      Height          =   375
      Left            =   120
      TabIndex        =   2
      Top             =   600
      Width           =   1695
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1200
      TabIndex        =   1
      Text            =   "Select Reader"
      Top             =   120
      Width           =   2415
   End
   Begin VB.Label Label1 
      Caption         =   "Select Reader"
      Height          =   255
      Left            =   120
      TabIndex        =   0
      Top             =   240
      Width           =   1335
   End
End
Attribute VB_Name = "devProgMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              devprog.frm
'
'  Description:       This sample program outlines the steps on how to
'                     set the LED?Buzzer and antenna of the ACR122 NFC reader
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 22, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================

Option Explicit

Dim retCode, Protocol, hContext, hCard, ReaderCount As Long
Dim sReaderList As String * 256
Dim sReaderGroup As String
Dim ConnActive As Boolean
Dim ioRequest As SCARD_IO_REQUEST
Dim SendLen, RecvLen As Long
Dim SendBuff(0 To 262) As Byte
Dim RecvBuff(0 To 262) As Byte

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
                            SCARD_PROTOCOL_T0 Or SCARD_PROTOCOL_T1, _
                            hCard, _
                            Protocol)
    If retCode <> SCARD_S_SUCCESS Then
    
        Call DisplayOut(1, retCode, "")
        ConnActive = False
        Exit Sub
  
    Else
        
        Call DisplayOut(0, 0, "Successful connection to " & cbReader.Text)
        
    End If

    'add buttons
    ConnActive = True
    bGetFW.Enabled = True
    bGetStat.Enabled = True
    frAntenna.Enabled = True
    frRed.Enabled = True
    frGreen.Enabled = True
    frBlinkDuration.Enabled = True
    bSetLED.Enabled = True
    rbAntOn.Value = True
    rbRedFinalOff.Value = True
    rbRedStateMaskOff.Value = True
    rbRedInitOff.Value = True
    rbRedBlinkMaskOff.Value = True
    rbGreenFinalOff.Value = True
    rbGreenStateMaskOff.Value = True
    rbGreenInitOff.Value = True
    rbGreenBlinkMaskOff.Value = True
    rbLinkToBuzzOpt1.Value = True
    tT1.Text = "00"
    tT2.Text = "00"
    tRepeat.Text = "01"
    
End Sub

Private Sub bGetFW_Click()

'get the firmaware version of the reader
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
    
    retCode = Transmit
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
        
    'Interpret firmware data
    tmpStr = "Firmware Version: "
    
    For intIndx = 0 To RecvLen
        
        If RecvBuff(intIndx) <> &H0 Then
        
            tmpStr = tmpStr & Chr(RecvBuff(intIndx))
        
        End If
        
    Next intIndx
    
    Call DisplayOut(3, 0, tmpStr)

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

Private Sub InitMenu()

    bInit.Enabled = True
    bConn.Enabled = False
    bReset.Enabled = False
    ConnActive = False
    frAntenna.Enabled = False
    frRed.Enabled = False
    frGreen.Enabled = False
    frBlinkDuration.Enabled = False
    bSetLED.Enabled = False
    bGetFW.Enabled = False
    bGetStat.Enabled = False
    cbReader.Text = "Select Reader"
    mMsg.Text = ""
    Call DisplayOut(0, 0, "Program ready")

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
    Call InitMenu
    
End Sub

Private Sub bSetAntenna_Click()

    'set the antenna options
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H4
    SendBuff(5) = &HD4
    SendBuff(6) = &H32
    SendBuff(7) = &H1
    
    If rbAntOn.Value = True Then
    
        SendBuff(8) = &H1
    
    Else
        
        SendBuff(8) = &H0
    
    End If
    
    SendLen = 9
    
    retCode = Transmit
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bSetLED_Click()

'set the LED/Buzzer Settings
Dim tmpStr As String
Dim intIndx As Long
   
   'validate input
   If tT1.Text = "" Then
        
        tT1.SetFocus
        Exit Sub
        
    End If
    
    
    If tT2.Text = "" Then
        
        tT2.SetFocus
        Exit Sub
        
    End If
    
    If tRepeat.Text = "" Or tRepeat.Text = "0" Or tRepeat.Text = "00" Then
        
        tRepeat.Text = "01"
        tRepeat.SetFocus
        Exit Sub
        
    End If
   
    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H40
    SendBuff(3) = &H0
    
    If rbRedFinalOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H1
    
    End If
    
    If rbGreenFinalOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H2
    
    End If
    
    If rbRedStateMaskOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H4
    
    End If
    
    If rbGreenStateMaskOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H8
    
    End If
    
    If rbRedInitOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H10
    
    End If
    
    If rbGreenInitOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H20
    
    End If
    
    If rbRedBlinkMaskOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H40
    
    End If
    
    If rbGreenBlinkMaskOn.Value = True Then
    
        SendBuff(3) = SendBuff(3) Or &H80
    
    End If
    
    SendBuff(4) = &H40
    SendBuff(5) = CByte("&H" & tT1)
    SendBuff(6) = CByte("&H" & tT2)
    SendBuff(7) = CByte("&H" & tRepeat)
   
    If rbLinkToBuzzOpt1.Value = True Then
    
        SendBuff(8) = &H0
    
    End If
    
    If rbLinkToBuzzOpt2.Value = True Then
    
        SendBuff(8) = &H1
    
    End If
    
    If rbLinkToBuzzOpt3.Value = True Then
    
        SendBuff(8) = &H2
    
    End If
    
    If rbLinkToBuzzOpt4.Value = True Then
    
        SendBuff(8) = &H3
    
    End If
    
    SendLen = 9
    RecvLen = 2
    
    retCode = Transmit
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If

End Sub

Private Sub bGetStat_Click()

Dim tmpStr As String
Dim intIndx As Integer

    Call ClearBuffers
    SendBuff(0) = &HFF
    SendBuff(1) = &H0
    SendBuff(2) = &H0
    SendBuff(3) = &H0
    SendBuff(4) = &H2
    SendBuff(5) = &HD4
    SendBuff(6) = &H4
    SendLen = 7
    RecvLen = 12
    
    retCode = Transmit
    
    If retCode <> SCARD_S_SUCCESS Then
    
        Exit Sub
    
    End If
    
    'Interpret data
    tmpStr = ""
        
        For intIndx = 0 To (RecvLen - 1)
        
            tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00")
        
        Next intIndx

    If InStr(tmpStr, "D505280000809000") Or InStr(tmpStr, "D505000000809000") Then
    
        'No Tag is in the field
        Call DisplayOut(1, 0, "No Tag is in the field: " & Right("00" & Hex(RecvBuff(0)), 2) & " " & Right("00" & Hex(RecvBuff(1)), 2))

    Else
    
        'error code
        Call DisplayOut(3, 0, "Error Code: " & Format(Hex(RecvBuff(2)), "00"))
        
        'Field indicates if an external RF field is present and detected
        '(Field=0x01 or not (Field 0x00)
        If RecvBuff(3) = &H1 Then
            Call DisplayOut(3, 0, "External RF field is Present and detected: " & Format(Hex(RecvBuff(3)), "00"))
        Else
            Call DisplayOut(3, 0, "External RF field is NOT Present and NOT detected: " & Format(Hex(RecvBuff(3)), "00"))
        End If
    
        'Number of targets.The default value is 1
        Call DisplayOut(3, 0, "Number of Target: " & Format(Hex(RecvBuff(4)), "00"))
        
        'Logical number
        Call DisplayOut(3, 0, "Number of Target: " & Format(Hex(RecvBuff(5)), "00"))
        
        'Bit rate in reception
        Select Case (RecvBuff(6))
           
           Case &H0: Call DisplayOut(3, 0, "Bit Rate in Reception: " & Format(Hex(RecvBuff(6)), "00") & " (106 kbps)")
           Case &H1: Call DisplayOut(3, 0, "Bit Rate in Reception: " & Format(Hex(RecvBuff(6)), "00") & " (212 kbps)")
           Case &H2: Call DisplayOut(3, 0, "Bit Rate in Reception: " & Format(Hex(RecvBuff(6)), "00") & " (424 kbps)")
           
        End Select
        
        'Bit rate in transmission
        Select Case (RecvBuff(7))
           
           Case &H0: Call DisplayOut(3, 0, "Bit Rate in Transmission: " & Format(Hex(RecvBuff(7)), "00") & " (106 kbps)")
           Case &H1: Call DisplayOut(3, 0, "Bit Rate in Transmission: " & Format(Hex(RecvBuff(7)), "00") & " (212 kbps)")
           Case &H2: Call DisplayOut(3, 0, "Bit Rate in Transmission: " & Format(Hex(RecvBuff(7)), "00") & " (424 kbps)")
           
        End Select
        
        Select Case (RecvBuff(8))
           
           Case &H0: Call DisplayOut(3, 0, "Modulation Type: " & Format(Hex(RecvBuff(8)), "00") & " (ISO14443 or Mifare)")
           Case &H1: Call DisplayOut(3, 0, "Modulation Type: " & Format(Hex(RecvBuff(8)), "00") & " (Active mode)")
           Case &H2: Call DisplayOut(3, 0, "Modulation Type: " & Format(Hex(RecvBuff(8)), "00") & " (Innovision Jewel tag)")
           Case &H10: Call DisplayOut(3, 0, "Modulation Type: " & Format(Hex(RecvBuff(8)), "00") & " (Felica)")
           
        End Select
    
    End If

End Sub

Private Sub Form_Load()

    Call InitMenu

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
    tmpStr = ""
    
    For intIndx = 0 To SendLen - 1
        
        tmpStr = tmpStr & Format(Hex(SendBuff(intIndx)), "00") & " "
     
    Next intIndx
    
    Call DisplayOut(2, 0, tmpStr)
    
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
    
    Else
    
        tmpStr = ""
        
        For intIndx = 0 To (RecvLen - 1)
        
            tmpStr = tmpStr & Format(Hex(RecvBuff(intIndx)), "00") & " "
        
        Next intIndx
        
        Call DisplayOut(3, 0, Trim(tmpStr))
    
    End If
    
    Transmit = retCode

End Function

Private Sub tRepeat_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub

Private Sub tT1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub


Private Sub tT2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
            
            KeyAscii = KeyAscii - 32
            
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If


End Sub
