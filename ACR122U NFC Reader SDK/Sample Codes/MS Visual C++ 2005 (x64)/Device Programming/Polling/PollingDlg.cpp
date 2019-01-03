/*
'  Copyright(C):      Advanced Card Systems Ltd
'
'  File:              PollingDlg.cpp
'  Description:       This sample program outlines the steps on how to
'                     execute card detection polling functions using ACR1222
'
'  Author:            M.J.E.C. Castillo
'
'  Date:              June 31, 2008
'
'  Revision Trail:   (Date/Author/Description)
'
'======================================================================*/

#include "stdafx.h"
#include "Polling.h"
#include "PollingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


#define INVALID_SW1SW2 -450
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)

//Initializers for the status bar
static UINT BASED_CODE indicators[] =
{
    ID_INDICATOR_PANE1,
    ID_INDICATOR_PANE2,
	ID_INDICATOR_PANE3,
	ID_INDICATOR_PANE4
};

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
	DWORD					SendLen, RecvLen, ByteRet, ATRLen;
	BYTE					SendBuff[262], RecvBuff[262], ATRVal[262];
	SCARD_IO_REQUEST		IO_REQ;
	bool					detect = false;
	unsigned char			HByteArray[16];
	CPollingDlg	*pThis = NULL;


void ClearBuffers();
static CString GetScardErrMsg( int code );
int Transmit();
void DisplayOut( CString str, COLORREF color );
void Init();
int CallCardConnect( int reqType );
bool CheckCard();
void InterpretATR();

//Timer function used for polling
void CALLBACK TimerFunc1 ( HWND, UINT, UINT_PTR, DWORD )
{

	//use valid connection
	retCode = CallCardConnect(1);

	if(retCode != SCARD_S_SUCCESS)
	{
		pThis->m_bar.SetPaneText( 3, "No Card within Range.\n" );
		pThis->m_bar.SetPaneText( 1, "" );
		return;
	}

	if(CheckCard())
		pThis->m_bar.SetPaneText( 3, "Card is Detected.\n" );
	else
	{
		pThis->m_bar.SetPaneText( 3, "No Card within Range.\n" );
		pThis->m_bar.SetPaneText( 1, "" );
	}
	

}

bool CheckCard()
{

	//get ATR and check type of card
	DWORD readerLen = 100, dwState;
	char temparray[150];
	char tempstr[150];
	char buffer[15];
	ATRLen = 32;

	//Get ATR function
	pThis->cbReader.GetLBText( pThis->cbReader.GetCurSel(), readerName );

	retCode = SCardStatus( hCard,
						   readerName,
						   &readerLen,
						   &dwState,
						   &dwActProtocol,
						   ATRVal,
						   &ATRLen );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		return false;
	
	}
	else
	{

		InterpretATR();
		return true;

	}

}

int CallCardConnect( int reqType )
{

	int index;
	char buffer[262];

	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );

	//Shared Connection
	pThis->cbReader.GetLBText( pThis->cbReader.GetCurSel(), readerName );
	
	//Connect to selected reader
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_SHARED,
							SCARD_PROTOCOL_T0|SCARD_PROTOCOL_T1,
							&hCard,
							&dwActProtocol );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		if(reqType != 1)
		{

			DisplayOut( GetScardErrMsg( retCode ), RED );
		}
		return retCode;
	
	}
	else
	{
	
		if(reqType != 1)
		{
			sprintf( buffer, "Successful to connection to %s\n", readerName );
			DisplayOut( buffer, GREEN );
		}

		return retCode;

	}

}

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

//Displays the message in the rich edit box with the respective color
void DisplayOut( CString str, COLORREF color )
{

	int nOldLines = 0,
		nNewLines = 0,
		nScroll = 0;
	long nInsertPoint = 0;
	CHARFORMAT cf;

	//Save number of lines before insertion of new text
	nOldLines = pThis->mMsg.GetLineCount();

	//Initialize character format structure
	cf.cbSize		= sizeof( CHARFORMAT );
	cf.dwMask		= CFM_COLOR;
	cf.dwEffects	= 0;	// To disable CFE_AUTOCOLOR
	cf.crTextColor	= color;

	//Set insertion point to end of text
	nInsertPoint = pThis->mMsg.GetWindowTextLength();
	pThis->mMsg.SetSel( nInsertPoint, -1 );
	
	//Set the character format
	pThis->mMsg.SetSelectionCharFormat( cf );

	//Insert string at the current caret poisiton
	pThis->mMsg.ReplaceSel( str );

	nNewLines = pThis->mMsg.GetLineCount();
	nScroll	= nNewLines - nOldLines;
	pThis->mMsg.LineScroll( 1 );
	
}

int Transmit()
{


		
	retCode = SCardTransmit(hCard,
							NULL,
							SendBuff,
							SendLen,
							NULL,  
							RecvBuff,
							&RecvLen);

	if( retCode != SCARD_S_SUCCESS )
	{
		
		DisplayOut( GetScardErrMsg( retCode ), RED );
		
	
	}

	return retCode;

}

void InterpretATR()
{

	char RIDVal[262], sATRStr[262], lATRStr[262], cardname[262], tempstr[262];
	int index;

	//Interpret ATR and guess card
	sprintf( cardname, "" );
	//Mifare cards using ISO 14443 Part 3 Supplemental Document
	if( ATRLen > 14 )
	{
		if(ATRVal[12] == 0x03)
		{

			switch(ATRVal[14])
			{

				case 0x11:
					sprintf( cardname, "%s Felica 212K", cardname );
					break;

				case 0x12:
					sprintf( cardname, "%s Felica 424K", cardname );
					break;

				case 0x04:
					sprintf( cardname, "%s Topaz", cardname );
					break;

			}

		}
	

		if( ATRVal[12] == 0x03 )
		{
			
			if( ATRVal[13] == 0x00 )
			{
				
				switch( ATRVal[14] )
				{
				
						case 0x01:
							sprintf( cardname, "%s Mifare Standard 1K", cardname );
							break;
						case 0x02:
							sprintf( cardname, "%s Mifare Standard 4K", cardname );
							break;
						case 0x03:
							sprintf( cardname, "%s Mifare Ultra light", cardname );
							break;
						case 0x04:
							sprintf( cardname, "%s SLE55R_XXXX", cardname );
							break;
						case 0x06:
							sprintf( cardname, "%s SR176", cardname );
							break;
						case 0x07:
							sprintf( cardname, "%s SRI X4K", cardname );
							break;
						case 0x08:
							sprintf( cardname, "%s AT88RF020", cardname );
							break;
						case 0x09:
							sprintf( cardname, "%s AT88SC0204CRF", cardname );
							break;
						case 0x0A:
							sprintf( cardname, "%s AT88SC0808CRF", cardname );
							break;
						case 0x0B:
							sprintf( cardname, "%s AT88SC1616CRF", cardname );
							break;
						case 0x0C:
							sprintf( cardname, "%s AT88SC3216CRF", cardname );
							break;
						case 0x0D:
							sprintf( cardname, "%s AT88SC6416CRF", cardname );
							break;
						case 0x0E:
							sprintf( cardname, "%s SRF55V10P", cardname );
							break;
						case 0x0F:
							sprintf( cardname, "%s SRF55V02P", cardname );
							break;
						case 0x10:
							sprintf( cardname, "%s SRF55V10S", cardname );
							break;
						case 0x11:
							sprintf( cardname, "%s SRF55V02S", cardname );
							break;
						case 0x12:
							sprintf( cardname, "%s TAG IT", cardname );
							break;
						case 0x13:
							sprintf( cardname, "%s LRI512", cardname );
							break;
						case 0x14:
							sprintf( cardname, "%s ICODESLI", cardname );
							break;
						case 0x15:
							sprintf( cardname, "%s TEMPSENS", cardname );
							break;
						case 0x16:
							sprintf( cardname, "%s I.CODE1", cardname );
							break;
						case 0x17:
							sprintf( cardname, "%s PicoPass 2K", cardname );
							break;
						case 0x18:
							sprintf( cardname, "%s PicoPass 2KS", cardname );
							break;
						case 0x19:
							sprintf( cardname, "%s PicoPass 16K", cardname );
							break;
						case 0x1A:
							sprintf( cardname, "%s PicoPass 16KS", cardname );
							break;
						case 0x1B:
							sprintf( cardname, "%s PicoPass 16K(8x2)", cardname );
							break;
						case 0x1C:
							sprintf( cardname, "%s PicoPass 16KS(8x2)", cardname );
							break;
						case 0x1D:
							sprintf( cardname, "%s PicoPass 32KS(16+16)", cardname );
							break;
						case 0x1E:
							sprintf( cardname, "%s PicoPass 32KS(16+8x2)", cardname );
							break;
						case 0x1F:
							sprintf( cardname, "%s PicoPass 32KS(8x2+16)", cardname );
							break;
						case 0x20:
							sprintf( cardname, "%s PicoPass 32KS(8x2+8x2)", cardname );
							break;
						case 0x21:
							sprintf( cardname, "%s LRI64", cardname );
							break;
						case 0x22:
							sprintf( cardname, "%s I.CODE UID", cardname );
							break;
						case 0x23:
							sprintf( cardname, "%s I.CODE EPC", cardname );
							break;
						case 0x24:
							sprintf( cardname, "%s LRI12", cardname );
							break;
						case 0x25:
							sprintf( cardname, "%s LRI128", cardname );
							break;
						case 0x26:
							sprintf( cardname, "%s Mifare Mini", cardname );
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
			sprintf( cardname, "%s \n", cardname );
			pThis->m_bar.SetPaneText( 1, cardname );
		
		
			
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
		
			pThis->m_bar.SetPaneText( 1, "Mifare DESFire" );
			
		
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
		
			pThis->m_bar.SetPaneText( 1, "ST19XRC8E" );
		
		}
	
	}

	//Other cards using ISO 14443 Type A or B
	if( strcmp( lATRStr, "3B8X800150" ) == 0 )
	{
	
		DisplayOut( "> ISO 14443B\n", BLACK );
	
	}
	else
	{
	
		if( strcmp( sATRStr, "3B8X8001" ) == 0 )
		{
		
			pThis->m_bar.SetPaneText( 1, "ISO 14443A" );
		
		}
	
	}

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

void Init()
{

	DisplayOut( "Program Ready\n", GREEN );
	pThis->bInit.EnableWindow( true );
	pThis->bConn.EnableWindow( false );
	pThis->bGetPoll.EnableWindow( false );
	pThis->bSetPoll.EnableWindow( false );
	pThis->bPoll.EnableWindow( false );
	pThis->cbOpt1.EnableWindow( false );
	pThis->cbOpt2.EnableWindow( false );
	pThis->cbOpt3.EnableWindow( false );
	pThis->cbOpt4.EnableWindow( false );
	pThis->cbOpt5.EnableWindow( false );
	pThis->cbOpt6.EnableWindow( false );
	pThis->cbOpt7.EnableWindow( false );
	pThis->rbOpt1.EnableWindow( false );
	pThis->rbOpt2.EnableWindow( false );
	pThis->bReset.EnableWindow( false );
	detect = false;
	pThis->KillTimer(1);

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
// CPollingDlg dialog

CPollingDlg::CPollingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPollingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPollingDlg)
	//}}AFX_DATA_INIT
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CPollingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPollingDlg)
	DDX_Control(pDX, IDC_CHECK7, cbOpt7);
	DDX_Control(pDX, IDC_CHECK6, cbOpt6);
	DDX_Control(pDX, IDC_CHECK5, cbOpt5);
	DDX_Control(pDX, IDC_CHECK4, cbOpt4);
	DDX_Control(pDX, IDC_CHECK3, cbOpt3);
	DDX_Control(pDX, IDC_CHECK2, cbOpt2);
	DDX_Control(pDX, IDC_CHECK1, cbOpt1);
	DDX_Control(pDX, IDC_RADIO2, rbOpt2);
	DDX_Control(pDX, IDC_RADIO1, rbOpt1);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_RICHEDIT1, mMsg);
	DDX_Control(pDX, IDC_BUTTON6, bClear);
	DDX_Control(pDX, IDC_BUTTON7, bReset);
	DDX_Control(pDX, IDC_BUTTON8, bQuit);
	DDX_Control(pDX, IDC_BUTTON5, bPoll);
	DDX_Control(pDX, IDC_BUTTON4, bSetPoll);
	DDX_Control(pDX, IDC_BUTTON3, bGetPoll);
	DDX_Control(pDX, IDC_BUTTON2, bConn);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPollingDlg, CDialog)
	//{{AFX_MSG_MAP(CPollingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConn)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetPoll)
	ON_BN_CLICKED(IDC_BUTTON4, OnSetPoll)
	ON_BN_CLICKED(IDC_BUTTON5, OnPoll)
	ON_BN_CLICKED(IDC_BUTTON6, OnClear)
	ON_BN_CLICKED(IDC_BUTTON7, OnReset)
	ON_BN_CLICKED(IDC_BUTTON8, OnQuit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPollingDlg message handlers

BOOL CPollingDlg::OnInitDialog()
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

	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small 
	
	// TODO: Add extra initialization here
		pThis = this;
	m_bar.Create( this );
	m_bar.SetIndicators( indicators, 4 );
	CRect rect;
	GetClientRect( &rect );
	m_bar.SetPaneInfo( 0, ID_INDICATOR_PANE1, SBPS_NORMAL, 83 );
	m_bar.SetPaneInfo( 1, ID_INDICATOR_PANE2, SBPS_NORMAL, 150 );
	m_bar.SetPaneInfo( 2, ID_INDICATOR_PANE3, SBPS_NORMAL, 83 );
	m_bar.SetPaneInfo( 3, ID_INDICATOR_PANE4, SBPS_NORMAL, 150 );
	RepositionBars( AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST,
					ID_INDICATOR_PANE4 );

	m_bar.SetPaneText( 0, "Card Type" );
	m_bar.SetPaneText( 2, "Card Status" );

	Init();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPollingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CPollingDlg::OnPaint() 
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

HCURSOR CPollingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CPollingDlg::OnInit() 
{

		int len = 64;
	int index;

	//Establish Context
	retCode = SCardEstablishContext( SCARD_SCOPE_USER,
									 NULL,
									 NULL,
									 &hContext );
	
	if( retCode != SCARD_S_SUCCESS )
	{
		mMsg.SetWindowText( GetScardErrMsg( retCode ) );
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
	
		mMsg.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	if( readerName == NULL )
	{
	
		mMsg.SetWindowText( GetScardErrMsg( retCode ) );
		return;
	
	}

	cbReader.ResetContent();
	char *p = readerName;
	while ( *p )
	{
		int i;
    	for (int i=0;p[i];i++);
		  i++;
	    if ( *p != 0 )
		{
     		cbReader.AddString( p );
		}
		p = &p[i];
	}

	index = cbReader.FindStringExact( -1, "ACS ACR122 0" );
	cbReader.SetCurSel( index );

	bConn.EnableWindow( true );
	bReset.EnableWindow( true );
	
}

void CPollingDlg::OnConn() 
{

		DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];

	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	//Connect to selected reader
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
//	IO_REQ.dwProtocol = Protocol;
//	IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

	cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
	sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
	DisplayOut( buffer1, GREEN );

	bInit.EnableWindow( false );
	bGetPoll.EnableWindow( true );
	bSetPoll.EnableWindow( true );
	bPoll.EnableWindow( true );
	cbOpt1.EnableWindow( true );
	cbOpt2.EnableWindow( true );
	cbOpt3.EnableWindow( true );
	cbOpt4.EnableWindow( true );
	cbOpt5.EnableWindow( true );
	cbOpt6.EnableWindow( true );
	cbOpt7.EnableWindow( true );
	rbOpt1.EnableWindow( true );
	rbOpt2.EnableWindow( true );
	
	
}

void CPollingDlg::OnGetPoll() 
{

	char tempstr[262];
	char tempstr2[262];
	int index;

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x50;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x00;

	SendLen = 5;
	RecvLen = 2;

	retCode = Transmit();

	if(retCode != SCARD_S_SUCCESS)
		return;

	//prints the command sent
	sprintf( tempstr, "<" );
	for( index = 0; index <= SendLen-1  ; index++ )
	{

		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}

	sprintf( tempstr, "%s\n", tempstr );

	DisplayOut( tempstr, BLACK );

	//prints response
	sprintf( tempstr2, ">" );
	for( index = 0; index <= RecvLen - 1; index++ )
	{
		
		sprintf( tempstr2, "%s %02X",tempstr2, RecvBuff[index] );
		
	}
	sprintf( tempstr2, "%s \n", tempstr2 );
	DisplayOut( tempstr2, BLACK );

	//interpret the return response
	if((RecvBuff[0] & 0x80) != 0)
	{
	
		DisplayOut( "Automatic Polling is enabled.\n", BLACK );
		cbOpt1.SetCheck(true);

	}
	else
	{

		DisplayOut( "Automatic Polling is disabled.\n", BLACK );
		cbOpt1.SetCheck(false);

	}

	if((RecvBuff[0] & 0x40) != 0)
	{
	
		DisplayOut( "Automatic ATS Generation is enabled.\n", BLACK );
		cbOpt2.SetCheck(true);

	}
	else
	{

		DisplayOut( "Automatic ATS Generation is disabled.\n", BLACK );
		cbOpt2.SetCheck(false);

	}

	if((RecvBuff[0] & 0x20) != 0)
	{
	
		DisplayOut( "250 ms.\n", BLACK );
		rbOpt1.SetCheck(true);

	}
	else
	{

		DisplayOut( "500 ms.\n", BLACK );
		rbOpt2.SetCheck(true);

	}

	
	if((RecvBuff[0] & 0x10) != 0)
	{
	
		DisplayOut( "Detect Felica 424K Card enabled.\n", BLACK );
		cbOpt7.SetCheck(true);

	}
	else
	{

		DisplayOut( "Detect FeliCa 424K Card disabled.\n", BLACK );
		cbOpt7.SetCheck(false);

	}

	
	if((RecvBuff[0] & 0x08) != 0)
	{
	
		DisplayOut( "Detect FeliCa 212K Card enabled.\n", BLACK );
		cbOpt6.SetCheck(true);

	}
	else
	{

		DisplayOut( "Detect Felica 212K Card disabled.\n", BLACK );
		cbOpt6.SetCheck(false);

	}

	
	if((RecvBuff[0] & 0x04) != 0)
	{
	
		DisplayOut( "Detect Topaz Card enabled.\n", BLACK );
		cbOpt5.SetCheck(true);

	}
	else
	{

		DisplayOut( "Detect Topaz Card disabled.\n", BLACK );
		cbOpt5.SetCheck(false);

	}

	
	if((RecvBuff[0] & 0x02) != 0)
	{
	
		DisplayOut( "Detect ISO 14443 Type B Card enabled.\n", BLACK );
		cbOpt4.SetCheck(true);

	}
	else
	{

		DisplayOut( "Detect ISO 14443 Type B Card disabled.\n", BLACK );
		cbOpt4.SetCheck(false);

	}

	if((RecvBuff[0] & 0x01) != 0)
	{
	
		DisplayOut( "Detect ISO 14443 Type A Card enabled.\n", BLACK );
		cbOpt3.SetCheck(true);

	}
	else
	{

		DisplayOut( "Detect ISO 14443 Type A Card disabled.\n", BLACK );
		cbOpt3.SetCheck(false);

	}

}

void CPollingDlg::OnSetPoll() 
{
	
	char tempstr[262];
	char tempstr2[262];
	int index;

	//validate input
	if((rbOpt1.GetCheck() == false) && (rbOpt2.GetCheck() == false))
	{

		rbOpt1.SetCheck(true);
		rbOpt1.SetFocus();

	}

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x51;
	SendBuff[3] = 0x00;

	if(cbOpt3.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x01);
		DisplayOut( "Detect ISO 14443 Type A Card enabled.\n", BLACK );

	}
	else
		DisplayOut( "Detect ISO 14443 Type A Card disabled.\n", BLACK );

	if(cbOpt4.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x02);
		DisplayOut( "Detect ISO 14443 Type B Card enabled.\n", BLACK );

	}
	else
		DisplayOut( "Detect ISO 14443 Type B Card disabled.\n", BLACK );

	if(cbOpt5.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x04);
		DisplayOut( "Detect Topaz Card enabled.\n", BLACK );

	}
	else
		DisplayOut( "Detect Topaz Card disabled.\n", BLACK );

	if(cbOpt6.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x08);
		DisplayOut( "Detect FeliCa 212K Card enabled.\n", BLACK );

	}
	else
		DisplayOut( "Detect FeliCa 212K Card disabled.\n", BLACK );

	if(cbOpt7.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x10);
		DisplayOut( "Detect Felica 424K Card enabled.\n", BLACK );

	}
	else
		DisplayOut( "Detect FeliCa 424K Card disabled.\n", BLACK );

	if(rbOpt1.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x20);
		DisplayOut( "Polling interval 250 ms. enabled.\n", BLACK );

	}
	else
		DisplayOut( "Polling Interval 500 ms. disabled.\n", BLACK );

	if(cbOpt2.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x40);
		DisplayOut( "Automatic ATS Generation is enabled.\n", BLACK );

	}
	else
		DisplayOut( "Automatic ATS Generation is disabled.\n", BLACK );

	if(cbOpt1.GetCheck() == true)
	{

		SendBuff[3] = (SendBuff[3] | 0x80);
		DisplayOut( "Automatic Polling is enabled.\n", BLACK );

	}
	else
		DisplayOut( "Automatic Polling is disabled.\n", BLACK );

	SendBuff[4] = 0x00;

	SendLen = 5;
	RecvLen = 1;

	//prints command sent
	sprintf( tempstr, "<" );
	for( index = 0; index <= SendLen-1  ; index++ )
	{

		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}

	sprintf( tempstr, "%s\n", tempstr );

	DisplayOut( tempstr, BLACK );

	retCode = Transmit();

	if(retCode != SCARD_S_SUCCESS)
		return;


	//prints response
	sprintf( tempstr2, ">" );
	for( index = 0; index <= RecvLen - 1; index++ )
	{
		
		sprintf( tempstr2, "%s %02X",tempstr2, RecvBuff[index] );
		
	}
	sprintf( tempstr2, "%s \n", tempstr2 );
	DisplayOut( tempstr2, BLACK );
	
}

void CPollingDlg::OnPoll() 
{
	
	if(detect)
	{

		DisplayOut( "Polling Stopped\n", BLACK );
		bPoll.SetWindowText("Start Polling");
		detect = false;
		m_bar.SetPaneText( 1, "" );
		m_bar.SetPaneText( 3, "" );
		KillTimer(1);
		return;

	}

	DisplayOut( "Polling Started\n", BLACK );
	bPoll.SetWindowText("End Polling");
	detect = true;
	SetTimer(1, 250, TimerFunc1 );
	


	
}

void CPollingDlg::OnClear() 
{
	
	mMsg.SetWindowText( "" );
	
}

void CPollingDlg::OnReset() 
{
	
	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	mMsg.SetWindowText( "" );
	Init();
	
}

void CPollingDlg::OnQuit() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnCancel();
	
}
