//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              DeviceProgrammingDlg.cpp
//
//  Description:       This sample program demonstrates how to get the ATR of the smart
//					   card.
//
//  Author:            Daryl M. Rojas
//
//	Date:              June 25, 2008
//
//	Revision Trail:   (Date/Author/Description)

//=====================================================================
#include "stdafx.h"
#include "Get ATR.h"
#include "Get ATRDlg.h"

#define IOCTL_SMARTCARD_ACR128_ESCAPE_COMMAND SCARD_CTL_CODE(2079)
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 255

//Device Programming Inlude File
#include "WINSCARD.h"

//Global Variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	LPCBYTE					pbSend;
	DWORD					dwSend, dwRecv, size = 64;
	LPBYTE					pbRecv;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet, ATRLen = 32;
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	CGetATRDlg				*pThis = NULL;
	BYTE					ATRVal[128];
	char					Buffer[32767];


#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void ClearBuffers();
static CString GetScardErrMsg( int code );
void DisplayOut( CString str, COLORREF color );
void InterpretATR();

//Clears the buffers of any data
void ClearBuffers()
{

	int index;
	
	for( index = 0; index <= 262; index++ )
	{
	
		SendBuff[index] = 0x00;
		RecvBuff[index] = 0x00;
	
	}

}

//Displays the message with the corresponding color
void DisplayOut( CString str, COLORREF color )
{

	int nOldLines = 0,
		nNewLines = 0,
		nScroll = 0;
	long nInsertPoint = 0;
	CHARFORMAT cf;

	//Save number of lines before insertion of new text
	nOldLines = pThis->rbResult.GetLineCount();

	//Initialize character format structure
	cf.cbSize		= sizeof( CHARFORMAT );
	cf.dwMask		= CFM_COLOR;
	cf.dwEffects	= 0;	// To disable CFE_AUTOCOLOR
	cf.crTextColor	= color;

	//Set insertion point to end of text
	nInsertPoint = pThis->rbResult.GetWindowTextLength();
	pThis->rbResult.SetSel( nInsertPoint, -1 );
	
	//Set the character format
	pThis->rbResult.SetSelectionCharFormat( cf );

	//Insert string at the current caret poisiton
	pThis->rbResult.ReplaceSel( str );

	nNewLines = pThis->rbResult.GetLineCount();
	nScroll	= nNewLines - nOldLines;
	pThis->rbResult.LineScroll( 1 );
	
}


//SmartCard Error Handler
static CString GetScardErrMsg(int code)
{
	switch( code )
	{
	// Smartcard Reader interface errors
	case SCARD_E_CANCELLED:
		return ("The action was canceled by an SCardCancel request.\n");
		break;
	case SCARD_E_CANT_DISPOSE:
		return ("The system could not dispose of the media in the requested manner.\n");
		break;
	case SCARD_E_CARD_UNSUPPORTED:
		return ("The smart card does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_DUPLICATE_READER:
		return ("The reader driver didn't produce a unique reader name.\n");
		break;
	case SCARD_E_INSUFFICIENT_BUFFER:
		return ("The data buffer for returned data is too small for the returned data.\n");
		break;
	case SCARD_E_INVALID_ATR:
		return ("An ATR string obtained from the registry is not a valid ATR string.\n");
		break;
	case SCARD_E_INVALID_HANDLE:
		return ("The supplied handle was invalid.\n");
		break;
	case SCARD_E_INVALID_PARAMETER:
		return ("One or more of the supplied parameters could not be properly interpreted.\n");
		break;
	case SCARD_E_INVALID_TARGET:
		return ("Registry startup information is missing or invalid.\n");
		break;
	case SCARD_E_INVALID_VALUE:
		return ("One or more of the supplied parameter values could not be properly interpreted.\n");
		break;
	case SCARD_E_NOT_READY:
		return ("The reader or card is not ready to accept commands.\n");
		break;
	case SCARD_E_NOT_TRANSACTED:
		return ("An attempt was made to end a non-existent transaction.\n");
		break;
	case SCARD_E_NO_MEMORY:
		return ("Not enough memory available to complete this command.\n");
		break;
	case SCARD_E_NO_SERVICE:
		return ("The smart card resource manager is not running.\n");
		break;
	case SCARD_E_NO_SMARTCARD:
		return ("The operation requires a smart card, but no smart card is currently in the device.\n");
		break;
	case SCARD_E_PCI_TOO_SMALL:
		return ("The PCI receive buffer was too small.\n");
		break;
	case SCARD_E_PROTO_MISMATCH:
		return ("The requested protocols are incompatible with the protocol currently in use with the card.\n");
		break;
	case SCARD_E_READER_UNAVAILABLE:
		return ("The specified reader is not currently available for use.\n");
		break;
	case SCARD_E_READER_UNSUPPORTED:
		return ("The reader driver does not meet minimal requirements for support.\n");
		break;
	case SCARD_E_SERVICE_STOPPED:
		return ("The smart card resource manager has shut down.\n");
		break;
	case SCARD_E_SHARING_VIOLATION:
		return ("The smart card cannot be accessed because of other outstanding connections.\n");
		break;
	case SCARD_E_SYSTEM_CANCELLED:
		return ("The action was canceled by the system, presumably to log off or shut down.\n");
		break;
	case SCARD_E_TIMEOUT:
		return ("The user-specified timeout value has expired.\n");
		break;
	case SCARD_E_UNKNOWN_CARD:
		return ("The specified smart card name is not recognized.\n");
		break;
	case SCARD_E_UNKNOWN_READER:
		return ("The specified reader name is not recognized.\n");
		break;
	case SCARD_F_COMM_ERROR:
		return ("An internal communications error has been detected.\n");
		break;
	case SCARD_F_INTERNAL_ERROR:
		return ("An internal consistency check failed.\n");
		break;
	case SCARD_F_UNKNOWN_ERROR:
		return ("An internal error has been detected, but the source is unknown.\n");
		break;
	case SCARD_F_WAITED_TOO_LONG:
		return ("An internal consistency timer has expired.\n");
		break;
	case SCARD_W_REMOVED_CARD:
		return ("The smart card has been removed and no further communication is possible.\n");
		break;
	case SCARD_W_RESET_CARD:
		return ("The smart card has been reset, so any shared state information is invalid.\n");
		break;
	case SCARD_W_UNPOWERED_CARD:
		return ("Power has been removed from the smart card and no further communication is possible.\n");
		break;
	case SCARD_W_UNRESPONSIVE_CARD:
		return ("The smart card is not responding to a reset.\n");
		break;
	case SCARD_W_UNSUPPORTED_CARD:
		return ("The reader cannot communicate with the card due to ATR string configuration conflicts.\n");
		break;
	}
	return ("Error is not documented.\n");
}

void InterpretATR()
{

	char RIDVal[MAX], sATRStr[MAX], lATRStr[MAX], cardname[MAX];
	int index;

	//Interpret ATR and guess card
	
	//Mifare cards using ISO 14443 Part 3 Supplemental Document
	if( ATRLen > 14 )
	{
	
		sprintf( RIDVal, "" );
		sprintf( sATRStr, "" );
		sprintf( lATRStr, "" );
		for( index = 7; index < 12; index++ )
		{
		
			sprintf( RIDVal, "%s%02X", RIDVal, ATRVal[index] );
		
		}
		for( index = 0; index < 5; index++ )
		{
		
			if( index == 1 && ( ATRVal[index] >> 4 ) == 8 )
			{
			
				sprintf( lATRStr, "8X" );
				sprintf( sATRStr, "8X" );
			
			}
			else
			{
			
				if( index = 4 )
				{
				
					sprintf( lATRStr, "%s%02X", lATRStr, ATRVal[index] );
				
				}
				else
				{
				
					sprintf( lATRStr, "%s%02X", lATRStr, ATRVal[index] );
					sprintf( sATRStr, "%s%02X", sATRStr, ATRVal[index] );
				
				}
			
			}
		
		}
		
		if( strcmp( RIDVal, "A000000306" ) == 0 )
		{
		
			sprintf( cardname, "" );
			switch( ATRVal[12] )
			{
			
				case 0:
					sprintf( cardname, "> No card information" );
					break;
				case 1:
					sprintf( cardname, "> ISO 14443 A, Part1 Card Type" );
					break;
				case 2:
					sprintf( cardname, "> ISO 14443 A, Part2 Card Type" );
					break;
				case 3:
					sprintf( cardname, "> ISO 14443 A, Part3 Card Type" );
					break;
				case 5:
					sprintf( cardname, "> ISO 14443 B, Part1 Card Type" );
					break;
				case 6:
					sprintf( cardname, "> ISO 14443 B, Part2 Card Type" );
					break;
				case 7:
					sprintf( cardname, "> ISO 14443 B, Part3 Card Type" );
					break;
				case 9:
					sprintf( cardname, "> ISO 15693, Part1 Card Type" );
					break;
				case 10:
					sprintf( cardname, "> ISO 15693, Part2 Card Type" );
					break;
				case 11:
					sprintf( cardname, "> ISO 15693, Part3 Card Type" );
					break;
				case 12:
					sprintf( cardname, "> ISO 15693, Part4 Card Type" );
					break;
				case 13:
					sprintf( cardname, "> Contact Card (7816-10) IIC Card Type" );
					break;
				case 14:
					sprintf( cardname, "> Contact Card (7816-10) Extended IIC Card Type" );
					break;
				case 15:
					sprintf( cardname, "> Contact Card (7816-10) 2WBP Card Type" );
					break;
				case 16:
					sprintf( cardname, "> Contact Card (7816-10) 3WBP Card Type" );
					break;
				default:
					sprintf( cardname, "> Undefined card" );
			}


			// Felica and Topaz Cards
			if( ATRVal[12] == 0x03 )
			{
			
				if( ATRVal[13] == 0xF0 )
				{

					switch (ATRVal[14])	
					{
						case 0x11:
							sprintf( cardname, "%s: FeliCa 212K", cardname );
							break;
						case 0x12:
							sprintf( cardname, "%s: Felica 424K", cardname );
							break;
						case 0x04:
							sprintf( cardname, "%s:  Topaz", cardname );
							break;
						default:
							sprintf( cardname, "> Undefined card" );
					}
				}
			}
			
			if( ATRVal[12] == 0x03 )
			{
			
				if( ATRVal[13] == 0x00 )
				{
				
					switch( ATRVal[14] )
					{
					
						case 0x01:
							sprintf( cardname, "%s: Mifare Standard 1K", cardname );
							break;
						case 0x02:
							sprintf( cardname, "%s: Mifare Standard 4K", cardname );
							break;
						case 0x03:
							sprintf( cardname, "%s: Mifare Ultra light", cardname );
							break;
						case 0x04:
							sprintf( cardname, "%s: SLE55R_XXXX", cardname );
							break;
						case 0x06:
							sprintf( cardname, "%s: SR176", cardname );
							break;
						case 0x07:
							sprintf( cardname, "%s: SRI X4K", cardname );
							break;
						case 0x08:
							sprintf( cardname, "%s: AT88RF020", cardname );
							break;
						case 0x09:
							sprintf( cardname, "%s: AT88SC0204CRF", cardname );
							break;
						case 0x0A:
							sprintf( cardname, "%s: AT88SC0808CRF", cardname );
							break;
						case 0x0B:
							sprintf( cardname, "%s: AT88SC1616CRF", cardname );
							break;
						case 0x0C:
							sprintf( cardname, "%s: AT88SC3216CRF", cardname );
							break;
						case 0x0D:
							sprintf( cardname, "%s: AT88SC6416CRF", cardname );
							break;
						case 0x0E:
							sprintf( cardname, "%s: SRF55V10P", cardname );
							break;
						case 0x0F:
							sprintf( cardname, "%s: SRF55V02P", cardname );
							break;
						case 0x10:
							sprintf( cardname, "%s: SRF55V10S", cardname );
							break;
						case 0x11:
							sprintf( cardname, "%s: SRF55V02S", cardname );
							break;
						case 0x12:
							sprintf( cardname, "%s: TAG IT", cardname );
							break;
						case 0x13:
							sprintf( cardname, "%s: LRI512", cardname );
							break;
						case 0x14:
							sprintf( cardname, "%s: ICODESLI", cardname );
							break;
						case 0x15:
							sprintf( cardname, "%s: TEMPSENS", cardname );
							break;
						case 0x16:
							sprintf( cardname, "%s: I.CODE1", cardname );
							break;
						case 0x17:
							sprintf( cardname, "%s: PicoPass 2K", cardname );
							break;
						case 0x18:
							sprintf( cardname, "%s: PicoPass 2KS", cardname );
							break;
						case 0x19:
							sprintf( cardname, "%s: PicoPass 16K", cardname );
							break;
						case 0x1A:
							sprintf( cardname, "%s: PicoPass 16KS", cardname );
							break;
						case 0x1B:
							sprintf( cardname, "%s: PicoPass 16K(8x2)", cardname );
							break;
						case 0x1C:
							sprintf( cardname, "%s: PicoPass 16KS(8x2)", cardname );
							break;
						case 0x1D:
							sprintf( cardname, "%s: PicoPass 32KS(16+16)", cardname );
							break;
						case 0x1E:
							sprintf( cardname, "%s: PicoPass 32KS(16+8x2)", cardname );
							break;
						case 0x1F:
							sprintf( cardname, "%s: PicoPass 32KS(8x2+16)", cardname );
							break;
						case 0x20:
							sprintf( cardname, "%s: PicoPass 32KS(8x2+8x2)", cardname );
							break;
						case 0x21:
							sprintf( cardname, "%s: LRI64", cardname );
							break;
						case 0x22:
							sprintf( cardname, "%s: I.CODE UID", cardname );
							break;
						case 0x23:
							sprintf( cardname, "%s: I.CODE EPC", cardname );
							break;
						case 0x24:
							sprintf( cardname, "%s: LRI12", cardname );
							break;
						case 0x25:
							sprintf( cardname, "%s: LRI128", cardname );
							break;
						case 0x26:
							sprintf( cardname, "%s: Mifare Mini", cardname );
							break;

					}
				
				}
				else
				{
				
					if( ATRVal[13] == 0xFF )
					{
					
						switch( ATRVal[14] )
						{
						
							case 0x09:
								sprintf( cardname, ": Mifare Mini", cardname );
						
						}
					
					}
				
				}
			
			}
			sprintf( cardname, "%s is detected\n", cardname );
			DisplayOut( cardname, BLACK );
		
		}
			
	}

	//Mifare DESFire card using ISO 14443 Part 4
	if( ATRLen == 11 )
	{
	
		sprintf( RIDVal, "" );
		for( index = 4; index < 10; index++ )
		{
		
			sprintf( RIDVal, "%s%02X", RIDVal, ATRVal[index] );
		
		}
		if( strcmp( RIDVal, "067577810280" ) == 0 )
		{
		
			DisplayOut( "> Mifare DESFire is  detected\n", BLACK );
		
		}
	
	}

	//Other cards using ISO 14443 Part 4
	if( ATRLen == 17 )
	{
	
		sprintf( RIDVal, "" );
		for( index = 4; index < 16; index++ )
		{
		
			sprintf( RIDVal, "%s%02X", RIDVal, ATRVal[index] );
		
		}
		if( strcmp( RIDVal, "50122345561253544E3381C3" ) == 0 )
		{
		
			DisplayOut( "> ST19XRC8E is detected\n", BLACK );
		
		}
	
	}

	//Other cards using ISO 14443 Type A or B
	if( strcmp( lATRStr, "3B8X800150" ) == 0 )
	{
	
		DisplayOut( "> ISO 14443B is detected\n", BLACK );
	
	}
	else
	{
	
		if( strcmp( sATRStr, "3B8X8001" ) == 0 )
		{
		
			DisplayOut( "> ISO 14443A is detected\n", BLACK );
		
		}
	
	}

}
/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CGetATRDlg dialog

CGetATRDlg::CGetATRDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CGetATRDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CGetATRDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CGetATRDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CGetATRDlg)
	DDX_Control(pDX, IDC_BUTTON6, btnQuit);
	DDX_Control(pDX, IDC_BUTTON5, btnReset);
	DDX_Control(pDX, IDC_BUTTON4, btnClear);
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON3, btnGetATR);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CGetATRDlg, CDialog)
	//{{AFX_MSG_MAP(CGetATRDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetATR)
	ON_BN_CLICKED(IDC_BUTTON4, OnClear)
	ON_BN_CLICKED(IDC_BUTTON5, OnReset)
	ON_BN_CLICKED(IDC_BUTTON6, OnQuit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CGetATRDlg message handlers

BOOL CGetATRDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	pThis = this;
	cbReader.SetWindowText( "" );
	btnInit.EnableWindow( true );
	btnConnect.EnableWindow( false );
	btnGetATR.EnableWindow( false );
	btnClear.EnableWindow( true );
	btnReset.EnableWindow( false );
	btnQuit.EnableWindow( true );
	DisplayOut( "Program ready\n", GREEN );
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CGetATRDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CGetATRDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIconSmall);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CGetATRDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CGetATRDlg::OnInit() 
{

	int index;

	//Establish Context
	retCode = SCardEstablishContext( SCARD_SCOPE_USER,
									 NULL,
									 NULL,
									 &hContext );
	
	if( retCode != SCARD_S_SUCCESS )
	{
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	//List PC/SC Card Readers
	size = 256;
	retCode = SCardListReaders( hContext,
								NULL,
								readerName,
								&size );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	if( readerName == NULL )
	{
	
		rbResult.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}
	//Put readernames inside the combobox
	cbReader.ResetContent();
	char *p = readerName;
	while ( *p )
	{
    	for (int i=0;p[i];i++);
	      i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}


	btnInit.EnableWindow( false );
	btnConnect.EnableWindow( true );
	btnReset.EnableWindow( true );

}

void CGetATRDlg::OnConnect() 
{

	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];
	int index;

	cbReader.GetLBText( cbReader.GetCurSel(), readerName );

	retCode = SCardConnect( hContext,
		readerName,
		SCARD_SHARE_SHARED,
		SCARD_PROTOCOL_T0|SCARD_PROTOCOL_T1,
		&hCard,
		&dwActProtocol );
		
	if( retCode != SCARD_S_SUCCESS )
		{
			
			//Failed to connect to reader
			DisplayOut( GetScardErrMsg( retCode ), RED );
			return;

		}
		
	

	//Successful connection to reader
	IO_REQ.dwProtocol = Protocol;
	IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

	cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
	sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
	DisplayOut( buffer1, GREEN );

	btnGetATR.EnableWindow( true );
	btnConnect.EnableWindow( false );

}

void CGetATRDlg::OnGetATR() 
{

	DWORD ReaderLen = 100, dwState;
	DWORD tempword;
	char tempstr[MAX];
	int index;
	
	DisplayOut( "Invoke SCardStatus\n", GREEN );
	//Invoke SCardStatus using hCard handle and valid reader name
	tempword = 32;
	ATRLen = tempword;

	retCode = SCardStatus( hCard,
						   readerName,
						   &ReaderLen,
						   &dwState,
						   &IO_REQ.dwProtocol,
						   ATRVal,
						   &ATRLen );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
	
	}
	else
	{
	
		//Format ATRVal returned and display string as ATR value
		sprintf( tempstr, "> ATR Length: %d\n", ATRLen );
		DisplayOut( tempstr, BLACK );
		sprintf( tempstr, "> ATR Value: " );
		for( index = 0; index != ATRLen; index++ )
		{
		
			sprintf( tempstr, "%s%02X ", tempstr, ATRVal[index] );
		
		}
		sprintf( tempstr, "%s\n", tempstr );
		DisplayOut( tempstr, BLACK );
		
		//Interpret dwActProtocol returned and display as active protocol
		sprintf( tempstr, "> Active Protocol: " );

		DisplayOut( tempstr, BLACK );

		InterpretATR();
	}
		
}

void CGetATRDlg::OnClear() 
{

	rbResult.SetWindowText( "" );

}

void CGetATRDlg::OnReset() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	rbResult.SetWindowText( "" );
	OnInitDialog();

}

void CGetATRDlg::OnQuit() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnCancel();

}
