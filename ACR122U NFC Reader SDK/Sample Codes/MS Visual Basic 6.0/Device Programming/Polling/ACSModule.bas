Attribute VB_Name = "ModWinScard"
'===============================================================================
'
' Company   : Advanced Card Systems, Ltd
'
' Author    : Alcendor Lorzano Chan
'
' Date      : 18 / 09 / 2001
'
'
' Note : Special consideration must be taken when passing LPSTR
'        to DLL's, refer to MSDN for these cases
'
' Warning : For those using UNICODE characters. Those API's in
'           the "DECLARE" statement that end with SCARDxxxxA, you
'           must change it to SCARDxxxW. e.g SCardListReadersA() to
'           SCardListReadersW(). This is to differetiate Unicode ("W")
'           from non-Unicode API's ("A"). Look at Winscard.h for details
' Revision Trail: (Date/Author/Description)
' 1. (16 August 2002/J.I.R.Mission/Added SCardGetStatusChange function)
' 2. (16 December 2002)/J.I.R.Mission/Modified APDURec to include
'                      DataIn, DataOut, and SW1/SW2 bytes
' 3. (21 May 2003)/J.I.R.Mission/Added GetScardErrMsg routine to interpret
'                      PC/SC error
' 4. (21 June 2004)/J.I.R.Mission/Added ACS IOCTL classes and
'                      ScardControl function
' 5. (18 August 2005/J.I.R.Mission/Added memory card type constants
'
' 6. (17 January 2007 / R.C. Siman / Modify LoadListToControl function
'
' 7. (27 May 2008 / A.G. Sarte / Added IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND
'                                               constant for ACR128)
'
'===============================================================================

Public Type SCARD_IO_REQUEST
    dwProtocol As Long
    cbPciLength As Long
End Type

Public Type APDURec
    bCLA As Byte
    bINS As Byte
    bP1 As Byte
    bP2 As Byte
    bP3 As Byte
'    DATA(1 To 255) As Byte
    DataIn(1 To 255) As Byte
    DataOut(1 To 255) As Byte
    SW(1 To 2) As Byte
    IsSend As Boolean
End Type

Public Type SCARD_READERSTATE
    RdrName As String
    UserData As Long
    RdrCurrState As Long
    RdrEventState As Long
    ATRLength As Long
    ATRValue(1 To 36) As Byte
End Type


Global Const SCARD_S_SUCCESS = 0
Global Const SCARD_ATR_LENGTH = 33

'===========================================================
'  Memory Card type constants
'===========================================================
Global Const CT_MCU = &H0                     ' MCU
Global Const CT_IIC_Auto = &H1                ' IIC (Auto Detect Memory Size)
Global Const CT_IIC_1K = &H2                  ' IIC (1K)
Global Const CT_IIC_2K = &H3                  ' IIC (2K)
Global Const CT_IIC_4K = &H4                  ' IIC (4K)
Global Const CT_IIC_8K = &H5                  ' IIC (8K)
Global Const CT_IIC_16K = &H6                 ' IIC (16K)
Global Const CT_IIC_32K = &H7                 ' IIC (32K)
Global Const CT_IIC_64K = &H8                 ' IIC (64K)
Global Const CT_IIC_128K = &H9                ' IIC (128K)
Global Const CT_IIC_256K = &HA                ' IIC (256K)
Global Const CT_IIC_512K = &HB                ' IIC (512K)
Global Const CT_IIC_1024K = &HC               ' IIC (1024K)
Global Const CT_AT88SC153 = &HD               ' AT88SC153
Global Const CT_AT88SC1608 = &HE              ' AT88SC1608
Global Const CT_SLE4418 = &HF                 ' SLE4418
Global Const CT_SLE4428 = &H10                ' SLE4428
Global Const CT_SLE4432 = &H11                ' SLE4432
Global Const CT_SLE4442 = &H12                ' SLE4442
Global Const CT_SLE4406 = &H13                ' SLE4406
Global Const CT_SLE4436 = &H14                ' SLE4436
Global Const CT_SLE5536 = &H15                ' SLE5536
Global Const CT_MCUT0 = &H16                  ' MCU T=0
Global Const CT_MCUT1 = &H17                  ' MCU T=1
Global Const CT_MCU_Auto = &H18               ' MCU Autodetect

'==========================================================================
' Context Scope
'==========================================================================
Global Const SCARD_SCOPE_USER = 0 ' The context is a user context, and any
                                  ' database operations are performed within the
                                  ' domain of the user.
Global Const SCARD_SCOPE_TERMINAL = 1 ' The context is that of the current terminal,
                                      ' and any database operations are performed
                                      ' within the domain of that terminal.  (The
                                      ' calling application must have appropriate
                                      ' access permissions for any database actions.)
Global Const SCARD_SCOPE_SYSTEM = 2 ' The context is the system context, and any
                                    ' database operations are performed within the
                                    ' domain of the system.  (The calling
                                    ' application must have appropriate access
                                    ' permissions for any database actions.)

'==========================================================================
' Context Scope
'==========================================================================
Global Const SCARD_STATE_UNAWARE = &H0 ' The application is unaware of the
                                            ' current state, and would like to
                                            ' know.  The use of this value
                                            ' results in an immediate return
                                            ' from state transition monitoring
                                            ' services.  This is represented by
                                            ' all bits set to zero.
Global Const SCARD_STATE_IGNORE = &H1 ' The application requested that
                                            ' this reader be ignored.  No other
                                            ' bits will be set.
Global Const SCARD_STATE_CHANGED = &H2 ' This implies that there is a
                                            ' difference between the state
                                            ' believed by the application, and
                                            ' the state known by the Service
                                            ' Manager.  When this bit is set,
                                            ' the application may assume a
                                            ' significant state change has
                                            ' occurred on this reader.

Global Const SCARD_STATE_UNKNOWN = &H4 ' This implies that the given
                                            ' reader name is not recognized by
                                            ' the Service Manager.  If this bit
                                            ' is set, then SCARD_STATE_CHANGED
                                            ' and SCARD_STATE_IGNORE will also
                                            ' be set.
Global Const SCARD_STATE_UNAVAILABLE = &H8 ' This implies that the actual
                                            ' state of this reader is not
                                            ' available.  If this bit is set,
                                            ' then all the following bits are
                                            ' clear.
Global Const SCARD_STATE_EMPTY = &H10 ' This implies that there is not
                                            ' card in the reader.  If this bit
                                            ' is set, all the following bits
                                            ' will be clear.
Global Const SCARD_STATE_PRESENT = &H20 ' This implies that there is a card
                                            ' in the reader.
Global Const SCARD_STATE_ATRMATCH = &H40 ' This implies that there is a card
                                            ' in the reader with an ATR
                                            ' matching one of the target cards.
                                            ' If this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.  This bit is only returned
                                            ' on the SCardLocateCard() service.
Global Const SCARD_STATE_EXCLUSIVE = &H80 ' This implies that the card in the
                                            ' reader is allocated for exclusive
                                            ' use by another application.  If
                                            ' this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.
Global Const SCARD_STATE_INUSE = &H100 ' This implies that the card in the
                                            ' reader is in use by one or more
                                            ' other applications, but may be
                                            ' connected to in shared mode.  If
                                            ' this bit is set,
                                            ' SCARD_STATE_PRESENT will also be
                                            ' set.
Global Const SCARD_STATE_MUTE = &H200 ' This implies that the card in the
                                            ' reader is unresponsive or not
                                            ' supported by the reader or
                                            ' software.
Global Const SCARD_STATE_UNPOWERED = &H400 ' This implies that the card in the
                                            ' reader has not been powered up.


'===========================================================
'
'===========================================================
Global Const SCARD_SHARE_EXCLUSIVE = 1 ' This application is not willing to share this
                                ' card with other applications.
Global Const SCARD_SHARE_SHARED = 2 ' This application is willing to share this
                                ' card with other applications.
Global Const SCARD_SHARE_DIRECT = 3 ' This application demands direct control of
                                ' the reader, so it is not available to other
                                ' applications.

'===========================================================
'   Disposition
'===========================================================
Global Const SCARD_LEAVE_CARD = 0 ' Don't do anything special on close
Global Const SCARD_RESET_CARD = 1 ' Reset the card on close
Global Const SCARD_UNPOWER_CARD = 2 ' Power down the card on close
Global Const SCARD_EJECT_CARD = 3 ' Eject the card on close

'===========================================================
' ACS IOCTL class
'===========================================================
Public Const FILE_DEVICE_SMARTCARD      As Long = &H310000

' Reader action IOCTLs
Public Const IOCTL_SMARTCARD_DIRECT           As Long = FILE_DEVICE_SMARTCARD + 2050 * 4
Public Const IOCTL_SMARTCARD_SELECT_SLOT   As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
Public Const IOCTL_SMARTCARD_DRAW_LCDBMP   As Long = FILE_DEVICE_SMARTCARD + 2052 * 4
Public Const IOCTL_SMARTCARD_DISPLAY_LCD      As Long = FILE_DEVICE_SMARTCARD + 2053 * 4
Public Const IOCTL_SMARTCARD_CLR_LCD        As Long = FILE_DEVICE_SMARTCARD + 2054 * 4
Public Const IOCTL_SMARTCARD_READ_KEYPAD           As Long = FILE_DEVICE_SMARTCARD + 2055 * 4
Public Const IOCTL_SMARTCARD_READ_RTC         As Long = FILE_DEVICE_SMARTCARD + 2057 * 4
Public Const IOCTL_SMARTCARD_SET_RTC      As Long = FILE_DEVICE_SMARTCARD + 2058 * 4
Public Const IOCTL_SMARTCARD_SET_OPTION       As Long = FILE_DEVICE_SMARTCARD + 2059 * 4
Public Const IOCTL_SMARTCARD_SET_LED       As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
Public Const IOCTL_SMARTCARD_LOAD_KEY    As Long = FILE_DEVICE_SMARTCARD + 2062 * 4
Public Const IOCTL_SMARTCARD_READ_EEPROM       As Long = FILE_DEVICE_SMARTCARD + 2065 * 4
Public Const IOCTL_SMARTCARD_WRITE_EEPROM       As Long = FILE_DEVICE_SMARTCARD + 2066 * 4
Public Const IOCTL_SMARTCARD_GET_VERSION  As Long = FILE_DEVICE_SMARTCARD + 2067 * 4
Public Const IOCTL_SMARTCARD_GET_READER_INFO  As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
Public Const IOCTL_SMARTCARD_SET_CARD_TYPE  As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
Public Const IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND As Long = FILE_DEVICE_SMARTCARD + 2079 * 4

'===========================================================
'   Error Codes
'===========================================================
Global Const SCARD_F_INTERNAL_ERROR = &H80100001
Global Const SCARD_E_CANCELLED = &H80100002
Global Const SCARD_E_INVALID_HANDLE = &H80100003
Global Const SCARD_E_INVALID_PARAMETER = &H80100004
Global Const SCARD_E_INVALID_TARGET = &H80100005
Global Const SCARD_E_NO_MEMORY = &H80100006
Global Const SCARD_F_WAITED_TOO_LONG = &H80100007
Global Const SCARD_E_INSUFFICIENT_BUFFER = &H80100008
Global Const SCARD_E_UNKNOWN_READER = &H80100009
Global Const SCARD_E_TIMEOUT = &H8010000A
Global Const SCARD_E_SHARING_VIOLATION = &H8010000B
Global Const SCARD_E_NO_SMARTCARD = &H8010000C
Global Const SCARD_E_UNKNOWN_CARD = &H8010000D
Global Const SCARD_E_CANT_DISPOSE = &H8010000E
Global Const SCARD_E_PROTO_MISMATCH = &H8010000F
Global Const SCARD_E_NOT_READY = &H80100010
Global Const SCARD_E_INVALID_VALUE = &H80100011
Global Const SCARD_E_SYSTEM_CANCELLED = &H80100012
Global Const SCARD_F_COMM_ERROR = &H80100013
Global Const SCARD_F_UNKNOWN_ERROR = &H80100014
Global Const SCARD_E_INVALID_ATR = &H80100015
Global Const SCARD_E_NOT_TRANSACTED = &H80100016
Global Const SCARD_E_READER_UNAVAILABLE = &H80100017
Global Const SCARD_P_SHUTDOWN = &H80100018
Global Const SCARD_E_PCI_TOO_SMALL = &H80100019
Global Const SCARD_E_READER_UNSUPPORTED = &H8010001A
Global Const SCARD_E_DUPLICATE_READER = &H8010001B
Global Const SCARD_E_CARD_UNSUPPORTED = &H8010001C
Global Const SCARD_E_NO_SERVICE = &H8010001D
Global Const SCARD_E_SERVICE_STOPPED = &H8010001E
Global Const SCARD_W_UNSUPPORTED_CARD = &H80100065
Global Const SCARD_W_UNRESPONSIVE_CARD = &H80100066
Global Const SCARD_W_UNPOWERED_CARD = &H80100067
Global Const SCARD_W_RESET_CARD = &H80100068
Global Const SCARD_W_REMOVED_CARD = &H80100069


'===========================================================
'   Protocol
'===========================================================
Global Const SCARD_PROTOCOL_UNDEFINED = &H0           ' There is no active protocol.
Global Const SCARD_PROTOCOL_T0 = &H1                  ' T=0 is the active protocol.
Global Const SCARD_PROTOCOL_T1 = &H2                  ' T=1 is the active protocol.
Global Const SCARD_PROTOCOL_RAW = &H10000             ' Raw is the active protocol.
Global Const SCARD_PROTOCOL_DEFAULT = &H80000000      ' Use implicit PTS.


'===========================================================
'   Reader State
'===========================================================
Global Const SCARD_UNKNOWN = 0    ' This value implies the driver is unaware
                                  ' of the current state of the reader.
Global Const SCARD_ABSENT = 1     ' This value implies there is no card in
                                  ' the reader.
Global Const SCARD_PRESENT = 2    ' This value implies there is a card is
                                  ' present in the reader, but that it has
                                  ' not been moved into position for use.
Global Const SCARD_SWALLOWED = 3  ' This value implies there is a card in the
                                  ' reader in position for use.  The card is
                                  ' not powered.
Global Const SCARD_POWERED = 4    ' This value implies there is power is
                                  ' being provided to the card, but the
                                  ' Reader Driver is unaware of the mode of
                                  ' the card.
Global Const SCARD_NEGOTIABLE = 5 ' This value implies the card has been
                                  ' reset and is awaiting PTS negotiation.
Global Const SCARD_SPECIFIC = 6   ' This value implies the card has been
                                  ' reset and specific communication
                                  ' protocols have been established.

'==========================================================================
' Prototypes
'==========================================================================
Public Declare Function SCardEstablishContext Lib "winscard.dll" (ByVal dwScope As Long, _
                                                                  ByVal pvReserved1 As Long, _
                                                                  ByVal pvReserved2 As Long, _
                                                                  ByRef phContext As Long) As Long
                                                                  
Public Declare Function SCardReleaseContext Lib "winscard.dll" (ByVal hContext As Long) As Long

Public Declare Function SCardConnect Lib "winscard.dll" Alias "SCardConnectA" (ByVal hContext As Long, _
                                                                               ByVal szReaderName As String, _
                                                                               ByVal dwShareMode As Long, _
                                                                               ByVal dwPrefProtocol As Long, _
                                                                               ByRef hCard As Long, _
                                                                               ByRef ActiveProtocol As Long) As Long
                                                         
Public Declare Function SCardDisconnect Lib "winscard.dll" (ByVal hCard As Long, _
                                                            ByVal Disposistion As Long) As Long

Public Declare Function SCardBeginTransaction Lib "winscard.dll" (ByVal hCard As Long) As Long

Public Declare Function SCardEndTransaction Lib "winscard.dll" (ByVal hCard As Long, _
                                                                ByVal Disposition As Long) As Long

Public Declare Function SCardState Lib "winscard.dll" (ByVal hCard As Long, _
                                                       ByRef State As Long, _
                                                       ByRef Protocol As Long, _
                                                       ByRef ATR As Byte, _
                                                       ByRef ATRLen As Long) As Long

Public Declare Function SCardStatus Lib "winscard.dll" Alias "SCardStatusA" (ByVal hCard As Long, _
                                                                             ByVal szReaderName As String, _
                                                                             ByRef pcchReaderLen As Long, _
                                                                             ByRef State As Long, _
                                                                             ByRef Protocol As Long, _
                                                                             ByRef ATR As Byte, _
                                                                             ByRef ATRLen As Long) As Long

Public Declare Function SCardTransmit Lib "winscard.dll" (ByVal hCard As Long, _
                                                          pioSendRequest As SCARD_IO_REQUEST, _
                                                          ByRef SendBuff As Byte, _
                                                          ByVal SendBuffLen As Long, _
                                                          ByRef pioRecvRequest As SCARD_IO_REQUEST, _
                                                          ByRef RecvBuff As Byte, _
                                                          ByRef RecvBuffLen As Long) As Long
                                                          
Public Declare Function SCardListReaders Lib "winscard.dll" Alias "SCardListReadersA" (ByVal hContext As Long, _
                                                            ByVal mzGroup As String, _
                                                            ByVal ReaderList As String, _
                                                            ByRef pcchReaders As Long) As Long

Public Declare Function SCardGetStatusChange Lib "winscard.dll" Alias "SCardGetStatusChangeA" (ByVal hContext As Long, _
                                                          ByVal TimeOut As Long, _
                                                          ByRef ReaderState As SCARD_READERSTATE, _
                                                          ByVal ReaderCount As Long) As Long

Public Declare Function SCardControl Lib "winscard.dll" (ByVal hCard As Long, _
                                                          ByVal dwControlCode As Long, _
                                                          ByRef pvInBuffer As Byte, _
                                                          ByVal cbInBufferSize As Long, _
                                                          ByRef pvOutBuffer As Byte, _
                                                          ByVal cbOutBufferSize As Long, _
                                                          ByRef pcbBytesReturned As Long) As Long


'==========================================================================
'==========================================================================

Public Sub LoadListToControl(ByVal Ctrl As ComboBox, ByVal ReaderList As String)
Dim sTemp As String
Dim indx As Integer

indx = 1
sTemp = ""
Ctrl.Clear

While (Mid(ReaderList, indx, 1) <> vbNullChar)
    
    While (Mid(ReaderList, indx, 1) <> vbNullChar)
       sTemp = sTemp + Mid(ReaderList, indx, 1)
       indx = indx + 1
    Wend
    
    indx = indx + 1
    
    Ctrl.AddItem sTemp
    
    sTemp = ""
    
Wend

End Sub


Public Function GetScardErrMsg(ByVal ReturnCode As Long) As String
  Select Case ReturnCode
    Case SCARD_E_CANCELLED
    GetScardErrMsg = "The action was canceled by an SCardCancel request."
    Case SCARD_E_CANT_DISPOSE
    GetScardErrMsg = "The system could not dispose of the media in the requested manner."
    Case SCARD_E_CARD_UNSUPPORTED
    GetScardErrMsg = "The smart card does not meet minimal requirements for support."
    Case SCARD_E_DUPLICATE_READER
    GetScardErrMsg = "The reader driver didn't produce a unique reader name."
    Case SCARD_E_INSUFFICIENT_BUFFER
    GetScardErrMsg = "The data buffer for returned data is too small for the returned data."
    Case SCARD_E_INVALID_ATR
    GetScardErrMsg = "An ATR string obtained from the registry is not a valid ATR string."
    Case SCARD_E_INVALID_HANDLE
    GetScardErrMsg = "The supplied handle was invalid."
    Case SCARD_E_INVALID_PARAMETER
    GetScardErrMsg = "One or more of the supplied parameters could not be properly interpreted."
    Case SCARD_E_INVALID_TARGET
    GetScardErrMsg = "Registry startup information is missing or invalid."
    Case SCARD_E_INVALID_VALUE
    GetScardErrMsg = "One or more of the supplied parameter values could not be properly interpreted."
    Case SCARD_E_NOT_READY
    GetScardErrMsg = "The reader or card is not ready to accept commands."
    Case SCARD_E_NOT_TRANSACTED
    GetScardErrMsg = "An attempt was made to end a non-existent transaction."
    Case SCARD_E_NO_MEMORY
    GetScardErrMsg = "Not enough memory available to complete this command."
    Case SCARD_E_NO_SERVICE
    GetScardErrMsg = "The smart card resource manager is not running."
    Case SCARD_E_NO_SMARTCARD
    GetScardErrMsg = "The operation requires a smart card, but no smart card is currently in the device."
    Case SCARD_E_PCI_TOO_SMALL
    GetScardErrMsg = "The PCI receive buffer was too small."
    Case SCARD_E_PROTO_MISMATCH
    GetScardErrMsg = "The requested protocols are incompatible with the protocol currently in use with the card."
    Case SCARD_E_READER_UNAVAILABLE
    GetScardErrMsg = "The specified reader is not currently available for use."
    Case SCARD_E_READER_UNSUPPORTED
    GetScardErrMsg = "The reader driver does not meet minimal requirements for support."
    Case SCARD_E_SERVICE_STOPPED
    GetScardErrMsg = "The smart card resource manager has shut down."
    Case SCARD_E_SHARING_VIOLATION
    GetScardErrMsg = "The smart card cannot be accessed because of other outstanding connections."
    Case SCARD_E_SYSTEM_CANCELLED
    GetScardErrMsg = "The action was canceled by the system, presumably to log off or shut down."
    Case SCARD_E_TIMEOUT
    GetScardErrMsg = "The user-specified timeout value has expired."
    Case SCARD_E_UNKNOWN_CARD
    GetScardErrMsg = "The specified smart card name is not recognized."
    Case SCARD_E_UNKNOWN_READER
    GetScardErrMsg = "The specified reader name is not recognized."
    Case SCARD_F_COMM_ERROR
    GetScardErrMsg = "An internal communications error has been detected."
    Case SCARD_F_INTERNAL_ERROR
    GetScardErrMsg = "An internal consistency check failed."
    Case SCARD_F_UNKNOWN_ERROR
    GetScardErrMsg = "An internal error has been detected, but the source is unknown."
    Case SCARD_F_WAITED_TOO_LONG
    GetScardErrMsg = "An internal consistency timer has expired."
    Case SCARD_S_SUCCESS
    GetScardErrMsg = "No error was encountered."
    Case SCARD_W_REMOVED_CARD
    GetScardErrMsg = "The smart card has been removed, so that further communication is not possible."
    Case SCARD_W_RESET_CARD
    GetScardErrMsg = "The smart card has been reset, so any shared state information is invalid."
    Case SCARD_W_UNPOWERED_CARD
    GetScardErrMsg = "Power has been removed from the smart card, so that further communication is not possible."
    Case SCARD_W_UNRESPONSIVE_CARD
    GetScardErrMsg = "The smart card is not responding to a reset."
    Case SCARD_W_UNSUPPORTED_CARD
    GetScardErrMsg = "The reader cannot communicate with the card, due to ATR string configuration conflicts."
    Case Else
    GetScardErrMsg = "?"
    End Select
  
End Function










