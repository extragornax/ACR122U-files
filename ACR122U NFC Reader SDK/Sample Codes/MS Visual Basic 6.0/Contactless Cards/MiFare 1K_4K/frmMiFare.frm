VERSION 5.00
Object = "{3B7C8863-D78F-101B-B9B5-04021C009402}#1.2#0"; "RICHTX32.OCX"
Begin VB.Form frmMiFare 
   Caption         =   "MiFare Card Programming"
   ClientHeight    =   8235
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   9960
   Icon            =   "frmMiFare.frx":0000
   LinkTopic       =   "Form1"
   ScaleHeight     =   8235
   ScaleWidth      =   9960
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton btnQuit 
      Caption         =   "Quit"
      Height          =   375
      Left            =   8160
      TabIndex        =   43
      Top             =   7680
      Width           =   1695
   End
   Begin VB.CommandButton btnReset 
      Caption         =   "Reset"
      Height          =   375
      Left            =   6240
      TabIndex        =   42
      Top             =   7680
      Width           =   1695
   End
   Begin VB.CommandButton btnClear 
      Caption         =   "Clear Output"
      Height          =   375
      Left            =   4440
      TabIndex        =   41
      Top             =   7680
      Width           =   1575
   End
   Begin VB.Frame Frame5 
      Caption         =   "Value Block Functions"
      Height          =   2655
      Left            =   4440
      TabIndex        =   27
      Top             =   120
      Width           =   5415
      Begin VB.CommandButton btnRestoreVal 
         Caption         =   "Restore Value"
         Height          =   375
         Left            =   3480
         TabIndex        =   40
         Top             =   2160
         Width           =   1695
      End
      Begin VB.CommandButton btnReadVal 
         Caption         =   "Read Value"
         Height          =   375
         Left            =   3480
         TabIndex        =   39
         Top             =   1680
         Width           =   1695
      End
      Begin VB.CommandButton btnDec 
         Caption         =   "Decrement"
         Height          =   375
         Left            =   3480
         TabIndex        =   38
         Top             =   1200
         Width           =   1695
      End
      Begin VB.CommandButton btnInc 
         Caption         =   "Increment"
         Height          =   375
         Left            =   3480
         TabIndex        =   37
         Top             =   720
         Width           =   1695
      End
      Begin VB.CommandButton btnStore 
         Caption         =   "Store Value"
         Height          =   375
         Left            =   3480
         TabIndex        =   36
         Top             =   240
         Width           =   1695
      End
      Begin VB.TextBox tbTarget 
         Height          =   285
         Left            =   1440
         MaxLength       =   3
         TabIndex        =   35
         Top             =   1920
         Width           =   375
      End
      Begin VB.TextBox tbSource 
         Height          =   285
         Left            =   1440
         MaxLength       =   3
         TabIndex        =   34
         Top             =   1440
         Width           =   375
      End
      Begin VB.TextBox tbValueBlockNo 
         Height          =   285
         Left            =   1440
         MaxLength       =   3
         TabIndex        =   33
         Top             =   960
         Width           =   375
      End
      Begin VB.TextBox tbValue 
         Height          =   285
         Left            =   1440
         TabIndex        =   32
         Top             =   480
         Width           =   1575
      End
      Begin VB.Label Label12 
         Caption         =   "Target Block"
         Height          =   255
         Left            =   240
         TabIndex        =   31
         Top             =   1920
         Width           =   975
      End
      Begin VB.Label Label11 
         Caption         =   "Source Block"
         Height          =   255
         Left            =   240
         TabIndex        =   30
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label10 
         Caption         =   "Block No"
         Height          =   255
         Left            =   240
         TabIndex        =   29
         Top             =   960
         Width           =   735
      End
      Begin VB.Label Label9 
         Caption         =   "Value Amount"
         Height          =   255
         Left            =   240
         TabIndex        =   28
         Top             =   480
         Width           =   1095
      End
   End
   Begin VB.Frame Frame4 
      Caption         =   "Binary Block Functions"
      Height          =   2415
      Left            =   120
      TabIndex        =   19
      Top             =   5640
      Width           =   4215
      Begin VB.CommandButton btnWriteBin 
         Caption         =   "Update Block"
         Height          =   375
         Left            =   2160
         TabIndex        =   45
         Top             =   1800
         Width           =   1575
      End
      Begin VB.CommandButton btnReadBin 
         Caption         =   "Read Block"
         Height          =   375
         Left            =   360
         TabIndex        =   26
         Top             =   1800
         Width           =   1575
      End
      Begin VB.TextBox tbData 
         Height          =   375
         Left            =   360
         MaxLength       =   16
         TabIndex        =   25
         Top             =   1200
         Width           =   3375
      End
      Begin VB.TextBox tbLen 
         BeginProperty DataFormat 
            Type            =   1
            Format          =   "0"
            HaveTrueFalseNull=   0
            FirstDayOfWeek  =   0
            FirstWeekOfYear =   0
            LCID            =   1033
            SubFormatType   =   1
         EndProperty
         Height          =   315
         Left            =   3000
         MaxLength       =   2
         TabIndex        =   23
         Top             =   480
         Width           =   375
      End
      Begin VB.TextBox tbBinaryBlockNo 
         Height          =   315
         Left            =   1320
         MaxLength       =   3
         TabIndex        =   20
         Top             =   480
         Width           =   375
      End
      Begin VB.Label Label8 
         Caption         =   "Data (text)"
         Height          =   255
         Left            =   360
         TabIndex        =   24
         Top             =   960
         Width           =   855
      End
      Begin VB.Label Label4 
         Caption         =   "Block No"
         Height          =   255
         Left            =   360
         TabIndex        =   22
         Top             =   600
         Width           =   735
      End
      Begin VB.Label Label7 
         Caption         =   "Length"
         Height          =   255
         Left            =   2280
         TabIndex        =   21
         Top             =   600
         Width           =   615
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Authentication"
      Height          =   1935
      Left            =   120
      TabIndex        =   10
      Top             =   3600
      Width           =   4215
      Begin VB.Frame Frame3 
         Caption         =   "Key Type"
         Height          =   1095
         Left            =   2520
         TabIndex        =   16
         Top             =   240
         Width           =   1575
         Begin VB.OptionButton rKeyB 
            Caption         =   "Key B"
            Height          =   375
            Left            =   360
            TabIndex        =   18
            Top             =   600
            Width           =   855
         End
         Begin VB.OptionButton rKeyA 
            Caption         =   "Key A"
            Height          =   255
            Left            =   360
            TabIndex        =   17
            Top             =   360
            Width           =   855
         End
      End
      Begin VB.CommandButton btnAuthen 
         Caption         =   "Authenticate"
         Height          =   375
         Left            =   2400
         TabIndex        =   15
         Top             =   1440
         Width           =   1695
      End
      Begin VB.TextBox tbBlockNo 
         Height          =   315
         Left            =   1680
         MaxLength       =   3
         TabIndex        =   14
         Top             =   480
         Width           =   375
      End
      Begin VB.TextBox tbAuthenKeyNum 
         Height          =   315
         Left            =   1680
         MaxLength       =   2
         TabIndex        =   11
         Top             =   960
         Width           =   375
      End
      Begin VB.Label Label6 
         Caption         =   "Block No"
         Height          =   255
         Left            =   240
         TabIndex        =   13
         Top             =   600
         Width           =   975
      End
      Begin VB.Label Label5 
         Caption         =   "Key Store No"
         Height          =   255
         Left            =   240
         TabIndex        =   12
         Top             =   1020
         Width           =   975
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Load Authentication Keys to Device"
      Height          =   1815
      Left            =   120
      TabIndex        =   5
      Top             =   1680
      Width           =   4215
      Begin VB.TextBox tbKeyVal1 
         Height          =   315
         Left            =   1680
         MaxLength       =   2
         TabIndex        =   50
         Top             =   840
         Width           =   375
      End
      Begin VB.TextBox tbKeyVal2 
         Height          =   315
         Left            =   2040
         MaxLength       =   2
         TabIndex        =   49
         Top             =   840
         Width           =   375
      End
      Begin VB.TextBox tbKeyVal3 
         Height          =   315
         Left            =   2400
         MaxLength       =   2
         TabIndex        =   48
         Top             =   840
         Width           =   375
      End
      Begin VB.TextBox tbKeyVal4 
         Height          =   315
         Left            =   2760
         MaxLength       =   2
         TabIndex        =   47
         Top             =   840
         Width           =   375
      End
      Begin VB.TextBox tbKeyVal5 
         Height          =   315
         Left            =   3120
         MaxLength       =   2
         TabIndex        =   46
         Top             =   840
         Width           =   375
      End
      Begin VB.CommandButton btnLoadKey 
         Caption         =   "Load Keys"
         Height          =   375
         Left            =   2400
         TabIndex        =   44
         Top             =   1320
         Width           =   1695
      End
      Begin VB.TextBox tbKeyVal6 
         Height          =   315
         Left            =   3480
         MaxLength       =   2
         TabIndex        =   9
         Top             =   840
         Width           =   375
      End
      Begin VB.TextBox tbKeyNum 
         Height          =   315
         Left            =   1680
         MaxLength       =   2
         TabIndex        =   7
         Top             =   420
         Width           =   375
      End
      Begin VB.Label Label3 
         Caption         =   "Key Value Input"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   960
         Width           =   1215
      End
      Begin VB.Label Label2 
         Caption         =   "Key Store No"
         Height          =   255
         Left            =   240
         TabIndex        =   6
         Top             =   480
         Width           =   975
      End
   End
   Begin VB.CommandButton btnConnect 
      Caption         =   "Connect"
      Height          =   375
      Left            =   2640
      TabIndex        =   4
      Top             =   1080
      Width           =   1695
   End
   Begin VB.CommandButton btnInit 
      Caption         =   "Initialize"
      Height          =   375
      Left            =   2640
      TabIndex        =   3
      Top             =   600
      Width           =   1695
   End
   Begin RichTextLib.RichTextBox rbOutput 
      Height          =   4695
      Left            =   4440
      TabIndex        =   2
      Top             =   2880
      Width           =   5415
      _ExtentX        =   9551
      _ExtentY        =   8281
      _Version        =   393217
      Enabled         =   -1  'True
      ScrollBars      =   2
      TextRTF         =   $"frmMiFare.frx":17A2
   End
   Begin VB.ComboBox cbReader 
      Height          =   315
      Left            =   1320
      TabIndex        =   1
      Top             =   120
      Width           =   3015
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
Attribute VB_Name = "frmMiFare"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              frmMiFare.frm
'
'  Description:       This sample program outlines the steps on how to
'                     transact with MiFare cards using ACR122
'
'  Author:            Wazer Emmanuel R. Benal
'
'  Date:              July 23, 2008
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

Public Function SendAPDU()

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
    
    For index = 0 To RecvLen - 1
        tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2) & " "
    Next index
    
    Call DisplayOut(tempstr, 4)
    
    SendAPDU = retCode
    
End Function

Private Sub btnAuthen_Click()

    Dim tempstr As String
    Dim index As Integer
    
    'Check if the inputs are correct
    If tbBlockNo.Text = "" Then
        tbBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbBlockNo.Text) > 319 Then
        tbBlockNo.Text = "319"
        tbBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbAuthenKeyNum.Text = "" Then
        tbAuthenKeyNum.SetFocus
        Exit Sub
    ElseIf CInt("&H" & tbAuthenKeyNum.Text) > 1 Then
        tbAuthenKeyNum.Text = "1"
        Exit Sub
    End If
            
    Call ClearBuffers
    'Authentication command
    SendBuff(0) = &HFF                          'Class
    SendBuff(1) = &H86                          'INS
    SendBuff(2) = &H0                           'P1
    SendBuff(3) = &H0                           'P2
    SendBuff(4) = &H5                           'Lc
    SendBuff(5) = &H1                           'Byte 1 : Version number
    SendBuff(6) = &H0                           'Byte 2
    SendBuff(7) = CInt(tbBlockNo.Text)          'Byte 3 : Block number
    
    If rKeyA.Value = True Then
        SendBuff(8) = &H60                      'Byte 4 : Key Type A
    ElseIf rKeyB.Value = True Then
        SendBuff(8) = &H61                      'Byte 4 : Key Type B
    End If
    
    SendBuff(9) = CInt("&H" & tbAuthenKeyNum.Text) 'Byte 5 : Key number
    
    SendLen = 10
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = 0 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Checking for response
        If tempstr = "9000" Then
            Call DisplayOut("Authentication success", 1)
        Else
            Call DisplayOut("Authentication failed", 2)
        End If
    End If
    
End Sub

Private Sub btnClear_Click()

    rbOutput.Text = ""
    
End Sub

Private Sub btnDec_Click()

    Dim amount As Long
    Dim tempval As Long
    Dim excessval As Long
    Dim index As Integer
    Dim tempstr As String
        
    If tbValueBlockNo.Text = "" Then
        tbValueBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbValueBlockNo.Text) > 319 Then
        tbValueBlockNo.Text = "319"
        tbValueBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbValue.Text = "" Then
        tbValue.SetFocus
        Exit Sub
    ElseIf CLng(tbValue.Text) > 2147483647 Then
        tbValue.Text = "2147483647"
        tbValue.SetFocus
        Exit Sub
    End If
    
    tbSource.Text = ""
    tbTarget.Text = ""
    
    amount = CLng(tbValue.Text)
    Call ClearBuffers
    'Value Block Operation command
    SendBuff(0) = &HFF
    SendBuff(1) = &HD7
    SendBuff(2) = &H0
    SendBuff(3) = CInt(tbValueBlockNo.Text)
    SendBuff(4) = &H5
    SendBuff(5) = &H2
    
    'Shift bit to the right
    tempval = amount
    For index = 1 To 24
        tempval = tempval / 2
    Next index
    
    SendBuff(6) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 16
        tempval = tempval / 2
    Next index
    
    SendBuff(7) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 8
        tempval = tempval / 2
    Next index
    
    SendBuff(8) = tempval And &HFF
    SendBuff(9) = amount And &HFF
    
    SendLen = 10
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr <> "9000" Then
            Call DisplayOut("Decrement error!", 2)
        End If
    End If
    
End Sub

Private Sub btnInc_Click()

    Dim amount As Long
    Dim tempval As Long
    Dim excessval As Long
    Dim index As Integer
    Dim tempstr As String
        
    If tbValueBlockNo.Text = "" Then
        tbValueBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbValueBlockNo.Text) > 319 Then
        tbValueBlockNo.Text = "319"
        tbValueBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbValue.Text = "" Then
        tbValue.SetFocus
        Exit Sub
    ElseIf CLng(tbValue.Text) > 2147483647 Then
        tbValue.Text = "2147483647"
        tbValue.SetFocus
        Exit Sub
    End If
    
    tbSource.Text = ""
    tbTarget.Text = ""
    
    amount = CLng(tbValue.Text)
    Call ClearBuffers
    'Value Block Operation command
    SendBuff(0) = &HFF
    SendBuff(1) = &HD7
    SendBuff(2) = &H0
    SendBuff(3) = CInt(tbValueBlockNo.Text)
    SendBuff(4) = &H5
    SendBuff(5) = &H1
    
    'Shift bit to the right
    tempval = amount
    For index = 1 To 24
        tempval = tempval / 2
    Next index
    
    SendBuff(6) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 16
        tempval = tempval / 2
    Next index
    
    SendBuff(7) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 8
        tempval = tempval / 2
    Next index
    
    SendBuff(8) = tempval And &HFF
    SendBuff(9) = amount And &HFF
    
    SendLen = 10
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr <> "9000" Then
            Call DisplayOut("Increment error!", 2)
        End If
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
    
    btnInit.Enabled = False
    btnConnect.Enabled = True
        
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
    Frame4.Enabled = True
    Frame5.Enabled = True
    
End Sub

Private Sub btnLoadKey_Click()

    Dim index As Integer
    Dim tempstr As String
    
    'Check if the inputs are correct
    If tbKeyNum.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    ElseIf CInt("&H" & tbKeyNum.Text) > 1 Then
        tbKeyNum.Text = "1"
        Exit Sub
    End If
    
    If tbKeyVal1.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If
    If tbKeyVal2.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If
    If tbKeyVal3.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If
    If tbKeyVal4.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If
    If tbKeyVal5.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If
    If tbKeyVal6.Text = "" Then
        tbKeyNum.SetFocus
        Exit Sub
    End If

    Call ClearBuffers
    'Load Authentication Keys command
    SendBuff(0) = &HFF                          'Class
    SendBuff(1) = &H82                          'INS
    SendBuff(2) = &H0                           'P1 : Key Structure
    SendBuff(3) = CInt("&H" & tbKeyNum.Text)    'P2 : Key Number
    SendBuff(4) = &H6                           'P3 : Lc
    SendBuff(5) = CInt("&H" & tbKeyVal1.Text)   'Key 1
    SendBuff(6) = CInt("&H" & tbKeyVal2.Text)   'Key 2
    SendBuff(7) = CInt("&H" & tbKeyVal3.Text)   'Key 3
    SendBuff(8) = CInt("&H" & tbKeyVal4.Text)   'Key 4
    SendBuff(9) = CInt("&H" & tbKeyVal5.Text)   'Key 5
    SendBuff(10) = CInt("&H" & tbKeyVal6.Text)  'Key 6
    
    SendLen = 11
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr <> "9000" Then
            Call DisplayOut("Load authentication keys error!", 2)
        End If
    End If
    
End Sub

Private Sub btnQuit_Click()

    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    retCode = SCardReleaseContext(hContext)
    Unload Me
    
End Sub

Private Sub btnReadBin_Click()

    Dim index As Integer
    Dim tempstr As String
    
    If tbBinaryBlockNo.Text = "" Then
        tbBinaryBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbBlockNo.Text) > 319 Then
        tbBlockNo.Text = "319"
        tbBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbLen.Text = "" Then
        tbLen.SetFocus
        Exit Sub
    ElseIf CInt(tbLen.Text) > 16 Then
        tbLen.Text = "16"
        tbLen.SetFocus
        Exit Sub
    End If
    
    Call ClearBuffers
    'Read Binary Block command
    SendBuff(0) = &HFF                              'Class
    SendBuff(1) = &HB0                              'INS
    SendBuff(2) = &H0                               'P1
    SendBuff(3) = CInt("&H" & tbBinaryBlockNo.Text) 'P2 : Block number
    SendBuff(4) = CInt(tbLen.Text)                  'Le : Number of bytes to read
    
    SendLen = 5
    RecvLen = CInt(tbLen.Text) + 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr = "9000" Then
            tempstr = ""
            For index = 0 To RecvLen - 3
                tempstr = tempstr & Right$(Chr(RecvBuff(index)), 2)
            Next index
            tbData.Text = tempstr
        Else
            Call DisplayOut("Read block error!", 2)
        End If
    End If
    
End Sub

Private Sub btnReadVal_Click()

    Dim index As Integer
    Dim tempstr As String
    Dim amount As Long
        
    If tbValueBlockNo.Text = "" Then
        tbValueBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbValueBlockNo.Text) > 319 Then
        tbValueBlockNo.Text = "319"
        tbValueBlockNo.SetFocus
        Exit Sub
    End If
    
    Call ClearBuffers
    'Read Value Block command
    SendBuff(0) = &HFF
    SendBuff(1) = &HB1
    SendBuff(2) = &H0
    SendBuff(3) = CInt(tbValueBlockNo.Text)
    SendBuff(4) = &H4
    
    SendLen = 5
    RecvLen = 6
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr = "9000" Then
            amount = RecvBuff(3)
            amount = amount + (RecvBuff(2) * 256)
            amount = amount + (RecvBuff(1) * 65536)     '256 * 256
            amount = amount + (RecvBuff(0) * 16777216)  '256 * 256 * 256
            tbValue.Text = CStr(amount)
        Else
            Call DisplayOut("Read value error!", 2)
        End If
    End If
    
End Sub

Private Sub btnReset_Click()

    retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD)
    retCode = SCardReleaseContext(hContext)
    rbOutput.Text = ""
    Call Initialize
    
End Sub

Private Sub btnRestoreVal_Click()

    Dim index As Integer
    Dim tempstr As String
    
    If tbSource.Text = "" Then
        tbSource.SetFocus
        Exit Sub
    ElseIf CInt(tbSource.Text) > 319 Then
        tbSource.Text = "319"
        tbSource.SetFocus
        Exit Sub
    End If
    
    If tbTarget.Text = "" Then
        tbTarget.SetFocus
        Exit Sub
    ElseIf CInt(tbTarget.Text) > 319 Then
        tbTarget.Text = "319"
        tbTarget.SetFocus
        Exit Sub
    End If
    
    tbValue.Text = ""
    tbValueBlockNo.Text = ""
    
    Call ClearBuffers
    'Restore value command
    SendBuff(0) = &HFF
    SendBuff(1) = &HD7
    SendBuff(2) = &H0
    SendBuff(3) = CInt(tbSource.Text)
    SendBuff(4) = &H2
    SendBuff(5) = &H3
    SendBuff(6) = CInt(tbTarget.Text)
    
    SendLen = 7
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr <> "9000" Then
            Call DisplayOut("Restore value error!", 2)
        End If
    End If
    
End Sub

Private Sub btnStore_Click()

    Dim amount As Long
    Dim tempval As Long
    Dim excessval As Long
    Dim index As Integer
    Dim tempstr As String
        
    If tbValueBlockNo.Text = "" Then
        tbValueBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbValueBlockNo.Text) > 319 Then
        tbValueBlockNo.Text = "319"
        tbValueBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbValue.Text = "" Then
        tbValue.SetFocus
        Exit Sub
    ElseIf CLng(tbValue.Text) > 2147483647 Then
        tbValue.Text = "2147483647"
        tbValue.SetFocus
        Exit Sub
    End If
    
    tbSource.Text = ""
    tbTarget.Text = ""
    
    amount = CLng(tbValue.Text)
    Call ClearBuffers
    'Value Block Operation command
    SendBuff(0) = &HFF
    SendBuff(1) = &HD7
    SendBuff(2) = &H0
    SendBuff(3) = CInt(tbValueBlockNo.Text)
    SendBuff(4) = &H5
    SendBuff(5) = &H0
    
    'Shift bit to the right
    tempval = amount
    For index = 1 To 24
        tempval = tempval / 2
    Next index
    
    SendBuff(6) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 16
        tempval = tempval / 2
    Next index
    
    SendBuff(7) = tempval And &HFF
    
    tempval = amount
    For index = 1 To 8
        tempval = tempval / 2
    Next index
    
    SendBuff(8) = tempval And &HFF
    SendBuff(9) = amount And &HFF
    
    SendLen = 10
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr <> "9000" Then
            Call DisplayOut("Store value error!", 2)
        End If
    End If
    
End Sub

Private Sub btnWriteBin_Click()

    Dim index As Integer
    Dim tempstr As String
    
    If tbBinaryBlockNo.Text = "" Then
        tbBinaryBlockNo.SetFocus
        Exit Sub
    ElseIf CInt(tbBlockNo.Text) > 319 Then
        tbBlockNo.Text = "319"
        tbBlockNo.SetFocus
        Exit Sub
    End If
    
    If tbLen.Text = "" Then
        tbLen.SetFocus
        Exit Sub
    ElseIf CInt(tbLen.Text) > 16 Then
        tbLen.Text = "16"
        tbLen.SetFocus
        Exit Sub
    End If
    
    If CInt(tbLen.Text) > 0 And tbData.Text = "" Then
        tbData.SetFocus
        Exit Sub
    End If
    
    Call ClearBuffers
    'Update Binary Block command
    SendBuff(0) = &HFF                              'Class
    SendBuff(1) = &HD6                              'INS
    SendBuff(2) = &H0                               'P1
    SendBuff(3) = CInt(tbBinaryBlockNo.Text)        'P2 : Block Number
    SendBuff(4) = CInt(tbLen.Text)                  'Lc
    
    For index = 0 To Len(tbData.Text) - 1
        SendBuff(index + 5) = Asc(Mid(tbData.Text, index + 1, 1)) 'Data In
    Next index
    
    SendLen = SendBuff(4) + 5 'CInt(tbLen.Text) + 5
    RecvLen = 2
    
    retCode = SendAPDU
    If retCode <> SCARD_S_SUCCESS Then
        Exit Sub
    Else
        For index = RecvLen - 2 To RecvLen - 1
            tempstr = tempstr & Right$("00" & Hex(RecvBuff(index)), 2)
        Next index
        'Check for response
        If tempstr = "9000" Then
            tbData.Text = ""
        Else
            Call DisplayOut("Update block error!", 2)
        End If
    End If
    
End Sub

Private Sub Form_Load()

    Call Initialize
    
End Sub

Private Sub tbAuthenKeyNum_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal3_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal4_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal5_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbAuthenKeyVal6_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbBinaryBlockNo_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbBlockNo_KeyPress(KeyAscii As Integer)
    
    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tbKeyNum_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal1_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal2_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal3_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal4_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal5_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbKeyVal6_KeyPress(KeyAscii As Integer)

    If KeyAscii > 96 And KeyAscii < 103 Then
        KeyAscii = KeyAscii - 32
    End If
    
    If KeyAscii < 32 Or InStr("0123456789ABCDEF", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If

End Sub

Private Sub tbLen_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tbSource_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tbTarget_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tbValue_KeyPress(KeyAscii As Integer)

    If KeyAscii < 32 Or InStr("0123456789", Chr$(KeyAscii)) > 0 Then
        'do nothing
    Else
        KeyAscii = 0
    End If
    
End Sub

Private Sub tbValueBlockNo_KeyPress(KeyAscii As Integer)

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
    btnInit.Enabled = True
    btnConnect.Enabled = False
    Frame1.Enabled = False
    Frame2.Enabled = False
    Frame4.Enabled = False
    Frame5.Enabled = False
    tbKeyNum.Text = ""
    tbKeyVal1.Text = ""
    tbKeyVal2.Text = ""
    tbKeyVal3.Text = ""
    tbKeyVal4.Text = ""
    tbKeyVal5.Text = ""
    tbKeyVal6.Text = ""
    tbBlockNo.Text = ""
    tbAuthenKeyNum.Text = ""
    tbBinaryBlockNo.Text = ""
    tbLen.Text = ""
    tbData.Text = ""
    tbValue.Text = ""
    tbSource.Text = ""
    tbTarget.Text = ""
    tbValueBlockNo.Text = ""
    rKeyA.Value = True
    Call DisplayOut("Program ready", 1)
   
End Sub
