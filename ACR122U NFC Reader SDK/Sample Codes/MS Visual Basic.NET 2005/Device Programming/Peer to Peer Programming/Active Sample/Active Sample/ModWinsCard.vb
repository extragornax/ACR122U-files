'============================================================================================
'   Author :  Mary Anne C.A. Arana
'   Module :  ModWinscard.vb
'   Company:  Advanced Card Systems Ltd.
'   Date   :  July 11, 2005
'
'   Revision: (Date /Author/Description)
'             (03/13/2008/ Arturo B. Salvamante Jr/ Added Summary, Description, 
'                                                    Error Message Function)
'
'             (05/27/2008/ Aileen Grace L. Sarte/ Added IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND 
'                                                  constant for ACR128)
'
'=============================================================================================

Imports System
Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

''' <summary> 
''' The SCARD_IO_REQUEST structure begins a protocol control information structure. Any protocol-specific information then immediately follows this structure. The entire length of the structure must be aligned with the underlying hardware architecture word size. For example, in Win32 the length of any PCI information must be a multiple of four bytes so that it aligns on a 32-bit boundary. 
''' </summary> 
<StructLayout(LayoutKind.Sequential)> _
Public Structure SCARD_IO_REQUEST
    ''' <summary> 
    ''' Protocol in use. 
    ''' </summary> 
    Public dwProtocol As Integer

    ''' <summary> 
    ''' Length, in bytes, of the SCARD_IO_REQUEST structure plus any following PCI-specific information. 
    ''' </summary> 
    Public cbPciLength As Integer
End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure APDURec
    Public ForSend As Boolean

    ''' <summary> 
    ''' The T=0 instruction class. 
    ''' </summary> 
    Public bCLA As Byte

    ''' <summary> 
    ''' An instruction code in the T=0 instruction class. 
    ''' </summary> 
    Public bINS As Byte

    ''' <summary> 
    ''' Reference codes that complete the instruction code. 
    ''' </summary> 
    Public bP1 As Byte

    ''' <summary> 
    ''' Reference codes that complete the instruction code. 
    ''' </summary> 
    Public bP2 As Byte

    ''' <summary> 
    ''' The number of data bytes to be transmitted during the command, per ISO 7816-4, Section 8.2.1. 
    ''' </summary> 
    Public bP3 As Byte

    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=256)> _
    Public Data As Byte()
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)> _
    Public SW As Byte()
End Structure

''' <summary> 
''' The SCARD_READERSTATE structure is used by functions for tracking smart cards within readers. 
''' </summary> 
<StructLayout(LayoutKind.Sequential)> _
Public Structure SCARD_READERSTATE
    ''' <summary> 
    ''' Pointer to the name of the reader being monitored. 
    ''' </summary> 
    Public RdrName As String

    ''' <summary> 
    ''' Not used by the smart card subsystem. This member is used by the application. 
    ''' </summary> 
    Public UserData As Long

    ''' <summary> 
    ''' Current state of the reader, as seen by the application. This field can take on any of the following values, in combination, as a bit mask. 
    ''' </summary> 
    Public RdrCurrState As Long

    ''' <summary> 
    ''' Current state of the reader, as known by the smart card resource manager. This field can take on any of the following values, in combination, as a bit mask. 
    ''' </summary> 
    Public RdrEventState As Long

    ''' <summary> 
    ''' Number of bytes in the returned ATR. 
    ''' </summary> 
    Public ATRLength As Long

    ''' <summary> 
    ''' ATR of the inserted card, with extra alignment bytes. 
    ''' </summary> 
    Public ATRValue As Byte()
End Structure

Public Class ModWinsCard

    Public Const SCARD_S_SUCCESS = 0
    Public Const SCARD_ATR_LENGTH = 33

#Region "Memory Card Type"

    Public Const CT_MCU As Integer = 0
    ' MCU 
    Public Const CT_IIC_Auto As Integer = 1
    ' IIC (Auto Detect Memory Size) 
    Public Const CT_IIC_1K As Integer = 2
    ' IIC (1K) 
    Public Const CT_IIC_2K As Integer = 3
    ' IIC (2K) 
    Public Const CT_IIC_4K As Integer = 4
    ' IIC (4K) 
    Public Const CT_IIC_8K As Integer = 5
    ' IIC (8K) 
    Public Const CT_IIC_16K As Integer = 6
    ' IIC (16K) 
    Public Const CT_IIC_32K As Integer = 7
    ' IIC (32K) 
    Public Const CT_IIC_64K As Integer = 8
    ' IIC (64K) 
    Public Const CT_IIC_128K As Integer = 9
    ' IIC (128K) 
    Public Const CT_IIC_256K As Integer = 10
    ' IIC (256K) 
    Public Const CT_IIC_512K As Integer = 11
    ' IIC (512K) 
    Public Const CT_IIC_1024K As Integer = 12
    ' IIC (1024K) 
    Public Const CT_AT88SC153 As Integer = 13
    ' AT88SC153 
    Public Const CT_AT88SC1608 As Integer = 14
    ' AT88SC1608 
    Public Const CT_SLE4418 As Integer = 15
    ' SLE4418 
    Public Const CT_SLE4428 As Integer = 16
    ' SLE4428 
    Public Const CT_SLE4432 As Integer = 17
    ' SLE4432 
    Public Const CT_SLE4442 As Integer = 18
    ' SLE4442 
    Public Const CT_SLE4406 As Integer = 19
    ' SLE4406 
    Public Const CT_SLE4436 As Integer = 20
    ' SLE4436 
    Public Const CT_SLE5536 As Integer = 21
    ' SLE5536 
    Public Const CT_MCUT0 As Integer = 22
    ' MCU T=0 
    Public Const CT_MCUT1 As Integer = 23
    ' MCU T=1 
    Public Const CT_MCU_Auto As Integer = 24
    ' MCU Autodetect 
#End Region

#Region "Context Scope"

    ''' <summary> 
    ''' The context is a user context, and any database operations 
    ''' are performed within the domain of the user. 
    ''' </summary> 
    Public Const SCARD_SCOPE_USER As Integer = 0

    ''' <summary> 
    ''' The context is that of the current terminal, and any database 
    ''' operations are performed within the domain of that terminal. 
    ''' (The calling application must have appropriate access permissions 
    ''' for any database actions.) 
    ''' </summary> 
    Public Const SCARD_SCOPE_TERMINAL As Integer = 1

    ''' <summary> 
    ''' The context is the system context, and any database operations 
    ''' are performed within the domain of the system. (The calling 
    ''' application must have appropriate access permissions for any 
    ''' database actions.) 
    ''' </summary> 
    Public Const SCARD_SCOPE_SYSTEM As Integer = 2

    ''' <summary> 
    ''' The application is unaware of the current state, and would like 
    ''' to know. The use of this value results in an immediate return 
    ''' from state transition monitoring services. This is represented 
    ''' by all bits set to zero. 
    ''' </summary> 
    Public Const SCARD_STATE_UNAWARE As Integer = 0

    ''' <summary> 
    ''' The application requested that this reader be ignored. No other 
    ''' bits will be set. 
    ''' </summary> 
    Public Const SCARD_STATE_IGNORE As Integer = 1

    ''' <summary> 
    ''' This implies that there is a difference between the state 
    ''' believed by the application, and the state known by the Service 
    ''' Manager.When this bit is set, the application may assume a 
    ''' significant state change has occurred on this reader. 
    ''' </summary> 
    Public Const SCARD_STATE_CHANGED As Integer = 2

    ''' <summary> 
    ''' This implies that the given reader name is not recognized by 
    ''' the Service Manager. If this bit is set, then SCARD_STATE_CHANGED 
    ''' and SCARD_STATE_IGNORE will also be set. 
    ''' </summary> 
    Public Const SCARD_STATE_UNKNOWN As Integer = 4

    ''' <summary> 
    ''' This implies that the actual state of this reader is not 
    ''' available. If this bit is set, then all the following bits are 
    ''' clear. 
    ''' </summary> 
    Public Const SCARD_STATE_UNAVAILABLE As Integer = 8

    ''' <summary> 
    ''' This implies that there is not card in the reader. If this bit 
    ''' is set, all the following bits will be clear. 
    ''' </summary> 
    Public Const SCARD_STATE_EMPTY As Integer = 16

    ''' <summary> 
    ''' This implies that there is a card in the reader. 
    ''' </summary> 
    Public Const SCARD_STATE_PRESENT As Integer = 32

    ''' <summary> 
    ''' This implies that there is a card in the reader with an ATR 
    ''' matching one of the target cards. If this bit is set, 
    ''' SCARD_STATE_PRESENT will also be set. This bit is only returned 
    ''' on the SCardLocateCard() service. 
    ''' </summary> 
    Public Const SCARD_STATE_ATRMATCH As Integer = 64

    ''' <summary> 
    ''' This implies that the card in the reader is allocated for 
    ''' exclusive use by another application. If this bit is set, 
    ''' SCARD_STATE_PRESENT will also be set. 
    ''' </summary> 
    Public Const SCARD_STATE_EXCLUSIVE As Integer = 128

    ''' <summary> 
    ''' This implies that the card in the reader is in use by one or 
    ''' more other applications, but may be connected to in shared mode. 
    ''' If this bit is set, SCARD_STATE_PRESENT will also be set. 
    ''' </summary> 
    Public Const SCARD_STATE_INUSE As Integer = 256

    ''' <summary> 
    ''' This implies that the card in the reader is unresponsive or not 
    ''' supported by the reader or software. 
    ''' </summary> 
    Public Const SCARD_STATE_MUTE As Integer = 512

    ''' <summary> 
    ''' This implies that the card in the reader has not been powered up. 
    ''' </summary> 
    Public Const SCARD_STATE_UNPOWERED As Integer = 1024

    ''' <summary> 
    ''' This application is not willing to share this card with other 
    ''' applications. 
    ''' </summary> 
    Public Const SCARD_SHARE_EXCLUSIVE As Integer = 1

    ''' <summary> 
    ''' This application is willing to share this card with other 
    ''' applications. 
    ''' </summary> 
    Public Const SCARD_SHARE_SHARED As Integer = 2

    ''' <summary> 
    ''' This application demands direct control of the reader, so it 
    ''' is not available to other applications. 
    ''' </summary> 
    Public Const SCARD_SHARE_DIRECT As Integer = 3

#End Region

#Region "Disposition"

    ''' <summary> 
    ''' Don't do anything special on close 
    ''' </summary> 
    Public Const SCARD_LEAVE_CARD As Integer = 0

    ''' <summary> 
    ''' Reset the card on close 
    ''' </summary> 
    Public Const SCARD_RESET_CARD As Integer = 1

    ''' <summary> 
    ''' Power down the card on close 
    ''' </summary> 
    Public Const SCARD_UNPOWER_CARD As Integer = 2

    ''' <summary> 
    ''' Eject the card on close 
    ''' </summary> 
    Public Const SCARD_EJECT_CARD As Integer = 3

#End Region

#Region "ACS IOCTL Class"
    Public Const FILE_DEVICE_SMARTCARD As Long = &H310000

    ' Reader action IOCTLs
    Public Const IOCTL_SMARTCARD_DIRECT As Long = FILE_DEVICE_SMARTCARD + 2050 * 4
    Public Const IOCTL_SMARTCARD_SELECT_SLOT As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
    Public Const IOCTL_SMARTCARD_DRAW_LCDBMP As Long = FILE_DEVICE_SMARTCARD + 2052 * 4
    Public Const IOCTL_SMARTCARD_DISPLAY_LCD As Long = FILE_DEVICE_SMARTCARD + 2053 * 4
    Public Const IOCTL_SMARTCARD_CLR_LCD As Long = FILE_DEVICE_SMARTCARD + 2054 * 4
    Public Const IOCTL_SMARTCARD_READ_KEYPAD As Long = FILE_DEVICE_SMARTCARD + 2055 * 4
    Public Const IOCTL_SMARTCARD_READ_RTC As Long = FILE_DEVICE_SMARTCARD + 2057 * 4
    Public Const IOCTL_SMARTCARD_SET_RTC As Long = FILE_DEVICE_SMARTCARD + 2058 * 4
    Public Const IOCTL_SMARTCARD_SET_OPTION As Long = FILE_DEVICE_SMARTCARD + 2059 * 4
    Public Const IOCTL_SMARTCARD_SET_LED As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
    Public Const IOCTL_SMARTCARD_LOAD_KEY As Long = FILE_DEVICE_SMARTCARD + 2062 * 4
    Public Const IOCTL_SMARTCARD_READ_EEPROM As Long = FILE_DEVICE_SMARTCARD + 2065 * 4
    Public Const IOCTL_SMARTCARD_WRITE_EEPROM As Long = FILE_DEVICE_SMARTCARD + 2066 * 4
    Public Const IOCTL_SMARTCARD_GET_VERSION As Long = FILE_DEVICE_SMARTCARD + 2067 * 4
    Public Const IOCTL_SMARTCARD_GET_READER_INFO As Long = FILE_DEVICE_SMARTCARD + 2051 * 4
    Public Const IOCTL_SMARTCARD_SET_CARD_TYPE As Long = FILE_DEVICE_SMARTCARD + 2060 * 4
    Public Const IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND As Long = FILE_DEVICE_SMARTCARD + 2079 * 4
    Public Const IOCTL_CCID_ESCAPE_SCARD_CTL_CODE As Long = FILE_DEVICE_SMARTCARD + 3500 * 4

#End Region

#Region "Error Codes"
    Public Const SCARD_F_INTERNAL_ERROR = &H80100001
    Public Const SCARD_E_CANCELLED = &H80100002
    Public Const SCARD_E_INVALID_HANDLE = &H80100003
    Public Const SCARD_E_INVALID_PARAMETER = &H80100004
    Public Const SCARD_E_INVALID_TARGET = &H80100005
    Public Const SCARD_E_NO_MEMORY = &H80100006
    Public Const SCARD_F_WAITED_TOO_Integer = &H80100007
    Public Const SCARD_E_INSUFFICIENT_BUFFER = &H80100008
    Public Const SCARD_E_UNKNOWN_READER = &H80100009
    Public Const SCARD_E_TIMEOUT = &H8010000A
    Public Const SCARD_E_SHARING_VIOLATION = &H8010000B
    Public Const SCARD_E_NO_SMARTCARD = &H8010000C
    Public Const SCARD_E_UNKNOWN_CARD = &H8010000D
    Public Const SCARD_E_CANT_DISPOSE = &H8010000E
    Public Const SCARD_E_PROTO_MISMATCH = &H8010000F
    Public Const SCARD_E_NOT_READY = &H80100010
    Public Const SCARD_E_INVALID_VALUE = &H80100011
    Public Const SCARD_E_SYSTEM_CANCELLED = &H80100012
    Public Const SCARD_E_NO_READERS_AVAILABLE = &H8010002E
    Public Const SCARD_F_COMM_ERROR = &H80100013
    Public Const SCARD_F_UNKNOWN_ERROR = &H80100014
    Public Const SCARD_E_INVALID_ATR = &H80100015
    Public Const SCARD_E_NOT_TRANSACTED = &H80100016
    Public Const SCARD_E_READER_UNAVAILABLE = &H80100017
    Public Const SCARD_P_SHUTDOWN = &H80100018
    Public Const SCARD_E_PCI_TOO_SMALL = &H80100019
    Public Const SCARD_E_READER_UNSUPPORTED = &H8010001A
    Public Const SCARD_E_DUPLICATE_READER = &H8010001B
    Public Const SCARD_E_CARD_UNSUPPORTED = &H8010001C
    Public Const SCARD_E_NO_SERVICE = &H8010001D
    Public Const SCARD_E_SERVICE_STOPPED = &H8010001E
    Public Const SCARD_E_DIR_NOT_FOUND = &H80100023
    Public Const SCARD_W_UNSUPPORTED_CARD = &H80100065
    Public Const SCARD_W_UNRESPONSIVE_CARD = &H80100066
    Public Const SCARD_W_UNPOWERED_CARD = &H80100067
    Public Const SCARD_W_RESET_CARD = &H80100068
    Public Const SCARD_W_REMOVED_CARD = &H80100069

#End Region

#Region "Protocol"
    ''' <summary> 
    ''' There is no active protocol. 
    ''' </summary> 
    Public Const SCARD_PROTOCOL_UNDEFINED = &H0

    ''' <summary> 
    ''' T=0 is the active protocol. 
    ''' </summary> 
    Public Const SCARD_PROTOCOL_T0 = &H1

    ''' <summary> 
    ''' T=1 is the active protocol. 
    ''' </summary> 
    Public Const SCARD_PROTOCOL_T1 = &H2


    ''' <summary> 
    ''' Raw is the active protocol. 
    ''' </summary> 
    Public Const SCARD_PROTOCOL_RAW = &H10000

    ''' <summary>
    ''' Use implicit PTS.
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SCARD_PROTOCOL_DEFAULT = &H80000000

#End Region

#Region "Reader State"

    ''' <summary> 
    ''' This value implies the driver is unaware of the current 
    ''' state of the reader. 
    ''' </summary> 
    Public Const SCARD_UNKNOWN As Integer = 0

    ''' <summary> 
    ''' This value implies there is no card in the reader. 
    ''' </summary> 
    Public Const SCARD_ABSENT As Integer = 1

    ''' <summary> 
    ''' This value implies there is a card is present in the reader, 
    ''' but that it has not been moved into position for use. 
    ''' </summary> 
    Public Const SCARD_PRESENT As Integer = 2

    ''' <summary> 
    ''' This value implies there is a card in the reader in position 
    ''' for use. The card is not powered. 
    ''' </summary> 
    Public Const SCARD_SWALLOWED As Integer = 3

    ''' <summary> 
    ''' This value implies there is power is being provided to the card, 
    ''' but the Reader Driver is unaware of the mode of the card. 
    ''' </summary> 
    Public Const SCARD_POWERED As Integer = 4

    ''' <summary> 
    ''' This value implies the card has been reset and is awaiting 
    ''' PTS negotiation. 
    ''' </summary> 
    Public Const SCARD_NEGOTIABLE As Integer = 5

    ''' <summary> 
    ''' This value implies the card has been reset and specific 
    ''' communication protocols have been established. 
    ''' </summary> 
    Public Const SCARD_SPECIFIC As Integer = 6

#End Region

#Region "Prototypes"

    ''' <summary> 
    ''' The SCardEstablishContext function establishes the resource manager context (the scope) within which database operations are performed. 
    ''' </summary> 
    ''' <param name="dwScope">[in] Scope of the resource manager context. This parameter can be one of the following values.</param> 
    ''' <param name="pvReserved1">[in] Reserved for future use and must be NULL. This parameter will allow a suitably privileged management application to act on behalf of another user.</param> 
    ''' <param name="pvReserved2">[in] Reserved for future use and must be NULL. </param> 
    ''' <param name="phContext">[out] Handle to the established resource manager context. This handle can now be supplied to other functions attempting to do work within this context.</param> 
    ''' <returns></returns> 
    Public Declare Function SCardEstablishContext Lib "Winscard.dll" (ByVal dwScope As Integer, _
                                                                      ByVal pvReserved1 As Integer, _
                                                                      ByVal pvReserved2 As Integer, _
                                                                      ByRef phContext As Integer) As Integer


    ''' <summary> 
    ''' The SCardReleaseContext function closes an established resource manager context, freeing any resources allocated under that context, including SCARDHANDLE objects and memory allocated using the SCARD_AUTOALLOCATE length designator. 
    ''' </summary> 
    ''' <param name="hContext">[in] Handle that identifies the resource manager context. The resource manager context is set by a previous call to SCardEstablishContext.</param> 
    ''' <returns></returns> 
    Public Declare Function SCardReleaseContext Lib "Winscard.dll" (ByVal hContext As Integer) As Integer


    ''' <summary> 
    ''' The SCardConnect function establishes a connection (using a specific resource manager context) between the calling application and a smart card contained by a specific reader. If no card exists in the specified reader, an error is returned. 
    ''' </summary> 
    ''' <param name="hContext">[in] A handle that identifies the resource manager context. The resource manager context is set by a previous call to SCardEstablishContext.</param> 
    ''' <param name="szReaderName">[in] The name of the reader that contains the target card. </param> 
    ''' <param name="dwShareMode">[in] A flag that indicates whether other applications may form connections to the card.</param> 
    ''' <param name="dwPrefProtocol">[in] A bit mask of acceptable protocols for the connection. Possible values may be combined with the OR operation.</param> 
    ''' <param name="hCard">[out] A handle that identifies the connection to the smart card in the designated reader. </param> 
    ''' <param name="ActiveProtocol">[out] A flag that indicates the established active protocol.</param> 
    ''' <returns></returns> 
    Public Declare Function SCardConnect Lib "Winscard.dll" Alias "SCardConnectA" (ByVal hContext As Integer, _
                                                                                   ByVal szReaderName As String, _
                                                                                   ByVal dwShareMode As Integer, _
                                                                                   ByVal dwPrefProtocol As Integer, _
                                                                                   ByRef hCard As Integer, _
                                                                                   ByRef ActiveProtocol As Integer) As Integer


    ''' <summary> 
    ''' The SCardDisconnect function terminates a connection previously opened between the calling application and a smart card in the target reader. 
    ''' </summary> 
    ''' <param name="hCard">[in] Reference value obtained from a previous call to SCardConnect. </param> 
    ''' <param name="Disposition">[in] Action to take on the card in the connected reader on close. </param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardDisconnect Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                                ByVal Disposition As Integer) As Integer


    ''' <summary> 
    ''' The SCardListReaderGroups function provides the list of reader groups that have previously been introduced to the system. 
    ''' </summary> 
    ''' <param name="hContext">[in] Handle that identifies the resource manager context for the query. The resource manager context can be set by a previous call to SCardEstablishContext. This parameter cannot be NULL.</param> 
    ''' <param name="mzGroups">[out] Multi-string that lists the reader groups defined to the system and available to the current user on the current terminal. If this value is NULL, SCardListReaderGroups ignores the buffer length supplied in pcchGroups, writes the length of the buffer that would have been returned if this parameter had not been NULL to pcchGroups, and returns a success code.</param> 
    ''' <param name="pcchGroups">[in, out] Length of the mszGroups buffer in characters, and receives the actual length of the multi-string structure, including all trailing null characters. If the buffer length is specified as SCARD_AUTOALLOCATE, then mszGroups is converted to a pointer to a byte pointer, and receives the address of a block of memory containing the multi-string structure. This block of memory must be deallocated with SCardFreeMemory. </param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardListReaderGroups Lib "Winscard.dll" (ByVal hContext As Integer, _
                                                                      ByRef mzGroups As String, _
                                                                      ByRef pcchGroups As Integer) As Integer


    ''' <summary> 
    ''' The SCardBeginTransaction function starts a transaction, waiting for the completion of all other transactions before it begins. 
    ''' When the transaction starts, all other applications are blocked from accessing the smart card while the transaction is in progress. 
    ''' </summary> 
    ''' <param name="hCard">[in] Reference value obtained from a previous call to SCardConnect.</param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardBeginTransaction Lib "Winscard.dll" (ByVal hCard As Integer) As Integer



    ''' <summary> 
    ''' The SCardEndTransaction function completes a previously declared transaction, allowing other applications to resume interactions with the card. 
    ''' </summary> 
    ''' <param name="hCard">[in] Reference value obtained from a previous call to SCardConnect. This value would also have been used in an earlier call to SCardBeginTransaction.</param> 
    ''' <param name="Disposition">[in] Action to take on the card in the connected reader on close. </param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardEndTransaction Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                                   ByVal Disposition As Integer) As Integer


    Public Declare Function SCardState Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                           ByRef State As Integer, _
                                                           ByRef Protocol As Integer, _
                                                           ByRef ATR As Byte, _
                                                           ByRef ATRLen As Integer) As Integer


    ''' <summary> 
    ''' The SCardStatus function provides the current status of a smart card in a reader. You can call it any time after a successful call to SCardConnect and before a successful call to SCardDisconnect. It does not affect the state of the reader or reader driver. 
    ''' </summary> 
    ''' <param name="hCard">[in] Reference value returned from SCardConnect. </param> 
    ''' <param name="szReaderName">[out] List of friendly names (multiple string) by which the currently connected reader is known. </param> 
    ''' <param name="pcchReaderLen">[in, out] On input, supplies the length of the szReaderName buffer. 
    ''' On output, receives the actual length (in characters) of the reader name list, including the trailing NULL character. If this buffer length is specified as SCARD_AUTOALLOCATE, then szReaderName is converted to a pointer to a byte pointer, and it receives the address of a block of memory that contains the multiple-string structure.</param> 
    ''' <param name="State">[out] Current state of the smart card in the reader. Upon success, it receives one of the following state indicators.</param> 
    ''' <param name="Protocol">[out] Current protocol, if any. The returned value is meaningful only if the returned value of pdwState is SCARD_SPECIFICMODE.</param> 
    ''' <param name="ATR">[out] Pointer to a 32-byte buffer that receives the ATR string from the currently inserted card, if available.</param> 
    ''' <param name="ATRLen">[in, out] On input, supplies the length of the pbAtr buffer. On output, receives the number of bytes in the ATR string (32 bytes maximum). If this buffer length is specified as SCARD_AUTOALLOCATE, then pbAtr is converted to a pointer to a byte pointer, and it receives the address of a block of memory that contains the multiple-string structure.</param> 
    ''' <returns>If the function successfully provides the current status of a smart card in a reader, the return value is SCARD_S_SUCCESS. 
    ''' If the function fails, it returns an error code. For more information, see Smart Card Return Values.</returns> 
    Public Declare Function SCardStatus Lib "Winscard.dll" Alias "SCardStatusA" (ByVal hCard As Integer, _
                                                                                 ByVal szReaderName As String, _
                                                                                 ByRef pcchReaderLen As Integer, _
                                                                                 ByRef State As Integer, _
                                                                                 ByRef Protocol As Integer, _
                                                                                 ByRef ATR As Byte, _
                                                                                 ByRef ATRLen As Integer) As Integer

    ''' <summary> 
    ''' The SCardTransmit function sends a service request to the smart card and expects to receive data back from the card. 
    ''' </summary> 
    ''' <param name="hCard">[in] A reference value returned from the SCardConnect function.</param> 
    ''' <param name="pioSendRequest">[in] A pointer to the protocol header structure for the instruction. This buffer is in the format of an SCARD_IO_REQUEST structure, followed by the specific protocol control information (PCI). 
    ''' For the T=0, T=1, and Raw protocols, the PCI structure is constant. The smart card subsystem supplies a global T=0, T=1, or Raw PCI structure, which you can reference by using the symbols SCARD_PCI_T0, SCARD_PCI_T1, and SCARD_PCI_RAW respectively.</param> 
    ''' <param name="SendBuff">[in] A pointer to the actual data to be written to the card. </param> 
    ''' <param name="SendBuffLen">[in] The length, in bytes, of the pbSendBuffer parameter. </param> 
    ''' <param name="pioRecvRequest">[in, out] Pointer to the protocol header structure for the instruction, followed by a buffer in which to receive any returned protocol control information (PCI) specific to the protocol in use. This parameter can be NULL if no PCI is returned. </param> 
    ''' <param name="RecvBuff">[out] Pointer to any data returned from the card. </param> 
    ''' <param name="RecvBuffLen">[in, out] Supplies the length, in bytes, of the pbRecvBuffer parameter and receives the actual number of bytes received from the smart card. This value cannot be SCARD_AUTOALLOCATE because SCardTransmit does not support SCARD_AUTOALLOCATE.</param> 
    ''' <returns>If the function successfully sends a service request to the smart card, the return value is SCARD_S_SUCCESS. 
    ''' If the function fails, it returns an error code. For more information, see Smart Card Return Values.</returns> 
    Public Declare Function SCardTransmit Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                              ByRef pioSendRequest As SCARD_IO_REQUEST, _
                                                              ByRef SendBuff As Byte, _
                                                              ByVal SendBuffLen As Integer, _
                                                              ByRef pioRecvRequest As SCARD_IO_REQUEST, _
                                                              ByRef RecvBuff As Byte, _
                                                              ByRef RecvBuffLen As Integer) As Integer



    ''' <summary> 
    ''' The SCardListReaders function provides the list of readers within a set of named reader groups, eliminating duplicates. 
    ''' The caller supplies a list of reader groups, and receives the list of readers within the named groups. Unrecognized group names are ignored. 
    ''' </summary> 
    ''' <param name="hContext">[in] Handle that identifies the resource manager context for the query. The resource manager context can be set by a previous call to SCardEstablishContext. This parameter cannot be NULL.</param> 
    ''' <param name="mzGroup">[in] Names of the reader groups defined to the system, as a multi-string. Use a NULL value to list all readers in the system (that is, the SCard$AllReaders group). </param> 
    ''' <param name="ReaderList">[out] Multi-string that lists the card readers within the supplied reader groups. If this value is NULL, SCardListReaders ignores the buffer length supplied in pcchReaders, writes the length of the buffer that would have been returned if this parameter had not been NULL to pcchReaders, and returns a success code.</param> 
    ''' <param name="pcchReaders">[in, out] Length of the mszReaders buffer in characters. This parameter receives the actual length of the multi-string structure, including all trailing null characters. If the buffer length is specified as SCARD_AUTOALLOCATE, then mszReaders is converted to a pointer to a byte pointer, and receives the address of a block of memory containing the multi-string structure. This block of memory must be deallocated with SCardFreeMemory.</param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardListReaders Lib "Winscard.dll" Alias "SCardListReadersA" (ByVal hContext As Integer, _
                                                                                           ByVal mzGroup As String, _
                                                                                           ByVal ReaderList As String, _
                                                                                           ByRef pcchReaders As Integer) As Integer


    ''' <summary>
    ''' The SCardGetStatusChange function blocks execution until the current availability of the cards in a specific set of readers changes.
    ''' </summary>
    ''' <param name="hContext">[in] Handle that identifies the resource manager context. The resource manager context is set by a previous call to SCardEstablishContext. </param>
    ''' <param name="TimeOut">[in] Maximum amount of time (in milliseconds) to wait for an action. A value of zero causes the function to return immediately. A value of INFINITE causes this function never to time out. </param>
    ''' <param name="ReaderState">[in, out] Array of SCARD_READERSTATE structures that specify the readers to watch, and receives the result. </param>
    ''' <param name="ReaderCount">[in] Number of elements in the rgReaderStates array. </param>
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns>
    ''' <remarks></remarks>
    Public Declare Function SCardGetStatusChange Lib "Winscard.dll" Alias "SCardGetStatusChangeA" (ByVal hContext As Integer, _
                                                                                                   ByVal TimeOut As Integer, _
                                                                                                   ByRef ReaderState As SCARD_READERSTATE, _
                                                                                                   ByVal ReaderCount As Integer) As Integer



    ''' <summary> 
    ''' The SCardControl function gives you direct control of the reader. You can call it any time after a successful call to SCardConnect and before a successful call to SCardDisconnect. The effect on the state of the reader depends on the control code. 
    ''' </summary> 
    ''' <param name="hCard">[in] Reference value returned from SCardConnect. </param> 
    ''' <param name="dwControlCode">[in] Control code for the operation. This value identifies the specific operation to be performed.</param> 
    ''' <param name="pvInBuffer">[in] Pointer to a buffer that contains the data required to perform the operation. This parameter can be NULL if the dwControlCode parameter specifies an operation that does not require input data. </param> 
    ''' <param name="cbInBufferSize">[in] Size, in bytes, of the buffer pointed to by lpInBuffer. </param> 
    ''' <param name="pvOutBuffer">[out] Pointer to a buffer that receives the operation's output data. This parameter can be NULL if the dwControlCode parameter specifies an operation that does not produce output data. </param> 
    ''' <param name="cbOutBufferSize">[in] Size, in bytes, of the buffer pointed to by lpOutBuffer. </param> 
    ''' <param name="pcbBytesReturned">[out] Pointer to a DWORD that receives the size, in bytes, of the data stored into the buffer pointed to by lpOutBuffer. </param> 
    ''' <returns>This function returns different values depending on whether it succeeds or fails.</returns> 
    Public Declare Function SCardControl Lib "Winscard.dll" (ByVal hCard As Integer, _
                                                              ByVal dwControlCode As Integer, _
                                                              ByRef pvInBuffer As Byte, _
                                                              ByVal cbInBufferSize As Integer, _
                                                              ByRef pvOutBuffer As Byte, _
                                                              ByVal cbOutBufferSize As Integer, _
                                                              ByRef pcbBytesReturned As Integer) As Integer

#End Region

#Region "Miscellaneous"
    ''' <summary> 
    ''' Returns the specific error message 
    ''' </summary> 
    ''' <param name="errCode">The error code</param> 
    ''' <returns></returns> 
    Public Shared Function GetScardErrMsg(ByVal errCode As Long) As String
        Select Case errCode
            Case SCARD_E_CANCELLED
                Return ("The action was canceled by an SCardCancel request.")
            Case SCARD_E_CANT_DISPOSE
                Return ("The system could not dispose of the media in the requested manner.")
            Case SCARD_E_CARD_UNSUPPORTED
                Return ("The smart card does not meet minimal requirements for support.")
            Case SCARD_E_DUPLICATE_READER
                Return ("The reader driver didn't produce a unique reader name.")
            Case SCARD_E_INSUFFICIENT_BUFFER
                Return ("The data buffer for returned data is too small for the returned data.")
            Case SCARD_E_INVALID_ATR
                Return ("An ATR string obtained from the registry is not a valid ATR string.")
            Case SCARD_E_INVALID_HANDLE
                Return ("The supplied handle was invalid.")
            Case SCARD_E_INVALID_PARAMETER
                Return ("One or more of the supplied parameters could not be properly interpreted.")
            Case SCARD_E_INVALID_TARGET
                Return ("Registry startup information is missing or invalid.")
            Case SCARD_E_INVALID_VALUE
                Return ("One or more of the supplied parameter values could not be properly interpreted.")
            Case SCARD_E_NOT_READY
                Return ("The reader or card is not ready to accept commands.")
            Case SCARD_E_NOT_TRANSACTED
                Return ("An attempt was made to end a non-existent transaction.")
            Case SCARD_E_NO_MEMORY
                Return ("Not enough memory available to complete this command.")
            Case SCARD_E_NO_SERVICE
                Return ("The smart card resource manager is not running.")
            Case SCARD_E_NO_SMARTCARD
                Return ("The operation requires a smart card, but no smart card is currently in the device.")
            Case SCARD_E_PCI_TOO_SMALL
                Return ("The PCI receive buffer was too small.")
            Case SCARD_E_PROTO_MISMATCH
                Return ("The requested protocols are incompatible with the protocol currently in use with the card.")
            Case SCARD_E_READER_UNAVAILABLE
                Return ("The specified reader is not currently available for use.")
            Case SCARD_E_READER_UNSUPPORTED
                Return ("The reader driver does not meet minimal requirements for support.")
            Case SCARD_E_SERVICE_STOPPED
                Return ("The smart card resource manager has shut down.")
            Case SCARD_E_SHARING_VIOLATION
                Return ("The smart card cannot be accessed because of other outstanding connections.")
            Case SCARD_E_SYSTEM_CANCELLED
                Return ("The action was canceled by the system, presumably to log off or shut down.")
            Case SCARD_E_TIMEOUT
                Return ("The user-specified timeout value has expired.")
            Case SCARD_E_UNKNOWN_CARD
                Return ("The specified smart card name is not recognized.")
            Case SCARD_E_UNKNOWN_READER
                Return ("The specified reader name is not recognized.")
            Case SCARD_E_NO_READERS_AVAILABLE
                Return ("No smart card reader is available.")
            Case SCARD_F_COMM_ERROR
                Return ("An internal communications error has been detected.")
            Case SCARD_F_INTERNAL_ERROR
                Return ("An internal consistency check failed.")
            Case SCARD_F_UNKNOWN_ERROR
                Return ("An internal error has been detected, but the source is unknown.")
            Case SCARD_F_WAITED_TOO_Integer
                Return ("An internal consistency timer has expired.")
            Case SCARD_S_SUCCESS
                Return ("No error was encountered.")
            Case SCARD_E_DIR_NOT_FOUND
                Return ("The identified directory does not exist in the smart card..")
            Case SCARD_W_RESET_CARD
                Return ("The smart card has been reset, so any shared state information is invalid.")
            Case SCARD_W_UNPOWERED_CARD
                Return ("Power has been removed from the smart card, so that further communication is not possible.")
            Case SCARD_W_UNRESPONSIVE_CARD
                Return ("The smart card is not responding to a reset.")
            Case SCARD_W_UNSUPPORTED_CARD
                Return ("The reader cannot communicate with the card, due to ATR string configuration conflicts.")
            Case SCARD_W_REMOVED_CARD
                Return ("The smart card has been removed, so further communication is not possible.")
            Case Else
                Return ("Code: " & errCode.ToString() & Chr(13) & Chr(10) & "Description: Undocumented error.")
        End Select
    End Function

    ''' <summary> 
    ''' Load the list from 2nd parameter (readerList) to the ComboBox control from 1st Parameter (ctrl) 
    ''' </summary> 
    ''' <param name="ctrl">The ComboBox control where do you want to load the list of reader</param> 
    ''' <param name="readerList">The string variable that contains the list of readers</param> 
    Public Shared Sub LoadListControl(ByVal ctrl As System.Windows.Forms.ComboBox, ByVal readerList As String)
        ctrl.Items.Clear()
        Dim rList As String() = readerList.Split(Chr(0))
        ctrl.BeginUpdate()
        For Each str As String In rList
            If str.Trim() <> String.Empty Then
                ctrl.Items.Add(str)
            Else
                Exit For
            End If
        Next
        ctrl.EndUpdate()
    End Sub
#End Region

End Class
