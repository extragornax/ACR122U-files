/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              Device ProgrammingDlg.cpp

  Description:       This sample program outlines the steps on how to
                     set the LED?Buzzer and antenna of the ACR122 NFC reader

  Author:            M.J.E.C. Castillo

  Date:              June 28, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

#include "stdafx.h"
#include "Device Programming.h"
#include "Device ProgrammingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#define INVALID_SW1SW2 -450
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)

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
	DWORD					SendLen, RecvLen, ByteRet;;
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	CDeviceProgrammingDlg	*pThis = NULL;


void ClearBuffers();
static CString GetScardErrMsg( int code );
int Transmit();
void DisplayOut( CString str, COLORREF color );
int HexCheck( char data1, char data2 );

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

	char tempstr[262];
	char tempstr2[262];
	int index;

	sprintf( tempstr, ">" );
	for( index = 0; index <= SendLen-1  ; index++ )
	{

		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}

	sprintf( tempstr, "%s\n", tempstr );

	DisplayOut( tempstr, BLACK );
		
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
	else
	{

		sprintf( tempstr2, ">" );
		for( index = 0; index <= RecvLen - 1; index++ )
		{
		
			sprintf( tempstr2, "%s %02X",tempstr2, RecvBuff[index] );
		
		}
		sprintf( tempstr2, "%s \n", tempstr2 );
		DisplayOut( tempstr2, BLACK );


	}

	return retCode;

}

//Error checking for inputs that needs to be in hex format
int HexCheck( char data1, char data2 )
{

	int retval = 1;
	bool state1, state2;

	if( data1 == '0' ||
		data1 == '1' ||
		data1 == '2' ||
		data1 == '3' ||
		data1 == '4' ||
		data1 == '5' ||
		data1 == '6' ||
		data1 == '7' ||
		data1 == '8' ||
		data1 == '9' ||
		data1 == 'A' ||
		data1 == 'B' ||
		data1 == 'C' ||
		data1 == 'D' ||
		data1 == 'E' ||
		data1 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' )
	{
	
		state1 = true;
	
	}
	else
	{
	
		state1 = false;
	
	}

	if( data2 == '0' ||
		data2 == '1' ||
		data2 == '2' ||
		data2 == '3' ||
		data2 == '4' ||
		data2 == '5' ||
		data2 == '6' ||
		data2 == '7' ||
		data2 == '8' ||
		data2 == '9' ||
		data2 == 'A' ||
		data2 == 'B' ||
		data2 == 'C' ||
		data2 == 'D' ||
		data2 == 'E' ||
		data2 == 'F' ||
		data1 == 'a' ||
		data1 == 'b' ||
		data1 == 'c' ||
		data1 == 'd' ||
		data1 == 'e' ||
		data1 == 'f' ||
		data2 == NULL )
	{
	
		state2 = true;
	
	}
	else
	{
	
		state2 = false;
	
	}

	if( state1 == true && state2 == true )
	{
	
		retval = 0;
	
	}
	else
	{
	
		retval = 1;
	
	}
				
	return retval;
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
// CDeviceProgrammingDlg dialog

CDeviceProgrammingDlg::CDeviceProgrammingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CDeviceProgrammingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CDeviceProgrammingDlg)
	//}}AFX_DATA_INIT
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CDeviceProgrammingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CDeviceProgrammingDlg)
	DDX_Control(pDX, IDC_BUTTON9, btnStatus);
	DDX_Control(pDX, IDC_RADIO18, rbGreenBlinkOff);
	DDX_Control(pDX, IDC_RADIO17, rbGreenBlinkOn);
	DDX_Control(pDX, IDC_RADIO16, rbGreenInitOff);
	DDX_Control(pDX, IDC_RADIO15, rbGreenInitOn);
	DDX_Control(pDX, IDC_RADIO14, rbGreenStateOff);
	DDX_Control(pDX, IDC_RADIO13, rbGreenStateOn);
	DDX_Control(pDX, IDC_RADIO12, rbGreenFinOff);
	DDX_Control(pDX, IDC_RADIO11, rbGreenFinOn);
	DDX_Control(pDX, IDC_RADIO10, rbRedBlinkOff);
	DDX_Control(pDX, IDC_RADIO9, rbRedBlinkOn);
	DDX_Control(pDX, IDC_RADIO8, rbRedInitOff);
	DDX_Control(pDX, IDC_RADIO7, rbRedInitOn);
	DDX_Control(pDX, IDC_RADIO6, rbRedStateOff);
	DDX_Control(pDX, IDC_RADIO5, rbRedStateOn);
	DDX_Control(pDX, IDC_RADIO4, rbRedFinOff);
	DDX_Control(pDX, IDC_RADIO3, rbRedFinOn);
	DDX_Control(pDX, IDC_RADIO1, rbAntOn);
	DDX_Control(pDX, IDC_RADIO2, rbAntOff);
	DDX_Control(pDX, IDC_RADIO22, rbT1T2Dur);
	DDX_Control(pDX, IDC_RADIO21, rbT2Dur);
	DDX_Control(pDX, IDC_RADIO20, rbT1Dur);
	DDX_Control(pDX, IDC_RADIO19, rbBuzzOff);
	DDX_Control(pDX, IDC_EDIT3, tRepeat);
	DDX_Control(pDX, IDC_EDIT2, tT2);
	DDX_Control(pDX, IDC_EDIT1, tT1);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_RICHEDIT2, mMsg);
	DDX_Control(pDX, IDC_BUTTON8, bQuit);
	DDX_Control(pDX, IDC_BUTTON7, bReset);
	DDX_Control(pDX, IDC_BUTTON6, bClear);
	DDX_Control(pDX, IDC_BUTTON5, bSetLED);
	DDX_Control(pDX, IDC_BUTTON4, bSetAnt);
	DDX_Control(pDX, IDC_BUTTON3, bGetFW);
	DDX_Control(pDX, IDC_BUTTON2, bConn);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CDeviceProgrammingDlg, CDialog)
	//{{AFX_MSG_MAP(CDeviceProgrammingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConn)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetFW)
	ON_BN_CLICKED(IDC_BUTTON4, OnSetAnt)
	ON_BN_CLICKED(IDC_BUTTON5, OnSetLED)
	ON_BN_CLICKED(IDC_BUTTON6, OnClear)
	ON_BN_CLICKED(IDC_BUTTON7, OnReset)
	ON_BN_CLICKED(IDC_BUTTON8, OnQuit)
	ON_BN_CLICKED(IDC_RADIO1, OnAntOn)
	ON_BN_CLICKED(IDC_RADIO2, OnAntOff)
	ON_BN_CLICKED(IDC_RADIO20, OnT1Dur)
	ON_BN_CLICKED(IDC_RADIO19, OnBuzzOff)
	ON_BN_CLICKED(IDC_RADIO21, OnT2Dur)
	ON_BN_CLICKED(IDC_RADIO22, OnT1T2Dur)
	ON_BN_CLICKED(IDC_RADIO3, OnRedFinOn)
	ON_BN_CLICKED(IDC_RADIO4, OnRedFinOff)
	ON_BN_CLICKED(IDC_RADIO5, OnRedStateOn)
	ON_BN_CLICKED(IDC_RADIO6, OnRedStateOff)
	ON_BN_CLICKED(IDC_RADIO7, OnRedInitOn)
	ON_BN_CLICKED(IDC_RADIO8, OnRedInitOff)
	ON_BN_CLICKED(IDC_RADIO9, OnRedBlinkOn)
	ON_BN_CLICKED(IDC_RADIO10, OnRedBlinkOff)
	ON_BN_CLICKED(IDC_RADIO11, OnGreenFinOn)
	ON_BN_CLICKED(IDC_RADIO12, OnGreenFinOff)
	ON_BN_CLICKED(IDC_RADIO13, OnGreenStateOn)
	ON_BN_CLICKED(IDC_RADIO14, OnGreenStateOff)
	ON_BN_CLICKED(IDC_RADIO15, OnGreenInitOn)
	ON_BN_CLICKED(IDC_RADIO16, OnGreenInitOff)
	ON_BN_CLICKED(IDC_RADIO17, OnGreenBlinkOn)
	ON_BN_CLICKED(IDC_RADIO18, OnGreenBlinkOff)
	ON_BN_CLICKED(IDC_BUTTON9, OnGetStatus)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingDlg message handlers

BOOL CDeviceProgrammingDlg::OnInitDialog()
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
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	pThis = this;
	DisplayOut( "Program Ready\n", GREEN );
	bConn.EnableWindow( false );
	bGetFW.EnableWindow( false );
	bSetAnt.EnableWindow( false );
	bSetLED.EnableWindow( false );
	bReset.EnableWindow( false );
	tT1.EnableWindow( false );
	tT2.EnableWindow( false );
	tRepeat.EnableWindow( false );

	pThis->tT1.SetLimitText( 2 );
	pThis->tT2.SetLimitText( 2 );
	pThis->tRepeat.SetLimitText( 2 );

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CDeviceProgrammingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CDeviceProgrammingDlg::OnPaint() 
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

HCURSOR CDeviceProgrammingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CDeviceProgrammingDlg::OnInit() 
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


void CDeviceProgrammingDlg::OnConn() 
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

	bGetFW.EnableWindow( true );
	bInit.EnableWindow( false );
	bSetAnt.EnableWindow( true );
	bSetLED.EnableWindow( true );
	tT1.EnableWindow( true );
	tT2.EnableWindow( true );
	tRepeat.EnableWindow( true );
	rbAntOn.SetCheck(true);
	rbRedFinOff.SetCheck(true);
	rbRedStateOff.SetCheck(true);
	rbRedInitOff.SetCheck(true);
	rbRedBlinkOff.SetCheck(true);
	rbGreenFinOff.SetCheck(true);
	rbGreenStateOff.SetCheck(true);
	rbGreenInitOff.SetCheck(true);
	rbGreenBlinkOff.SetCheck(true);
	rbBuzzOff.SetCheck(true);
	tT1.SetWindowText("00");
	tT2.SetWindowText("00");
	tRepeat.SetWindowText("01");

	
}

void CDeviceProgrammingDlg::OnGetFW() 
{

	char tempstr[262];
	int index;
	
	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x48;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x00;

	SendLen = 5;
	RecvLen = 10;

	retCode = Transmit();
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}

	//interpret firmware data
	sprintf( tempstr, "> Firmware Version: " );
	for( index = 0; index <= RecvLen; index++ )
	{

		if( RecvBuff[index] != 0x00 )
		{

			sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
		
		}
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );

	
}

void CDeviceProgrammingDlg::OnSetAnt() 
{
	
	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x04;
	SendBuff[5] = 0xD4;
	SendBuff[6] = 0x32;
	SendBuff[7] = 0x01;

	if(rbAntOn.GetCheck() == true)
		SendBuff[8] = 0x01;
	else if(rbAntOff.GetCheck() == true)
		SendBuff[8] = 0x00;

	SendLen = 9;
	
	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}

	
}

void CDeviceProgrammingDlg::OnSetLED() 
{
	
	int tempVal;
	char holder[4];
	//validate input

	tT1.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tT1.SetFocus();
		return;
		
	}

	tT2.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tT2.SetFocus();
		return;
		
	}

	tRepeat.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tRepeat.SetFocus();
		return;
		
	}

	ClearBuffers();

	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x40;
	SendBuff[3] = 0x00;

	if(rbRedFinOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x01;

	if(rbGreenFinOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x02;

	if(rbRedStateOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x04;

	if(rbGreenStateOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x08;

	if(rbRedInitOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x10;

	if(rbGreenInitOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x20;

	if(rbRedBlinkOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x40;

	if(rbGreenBlinkOn.GetCheck() == true)
		SendBuff[3] = SendBuff[3] | 0x80;

	SendBuff[4] = 0x40;

	tT1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[5] = tempVal;

	tT2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[6] = tempVal;

	tRepeat.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[7] = tempVal;

	if(rbBuzzOff.GetCheck() == true)	
		SendBuff[8] = 0x00;

	if(rbT1Dur.GetCheck() == true)	
		SendBuff[8] = 0x01;

	if(rbT2Dur.GetCheck() == true)	
		SendBuff[8] = 0x02;

	if(rbT1T2Dur.GetCheck() == true)	
		SendBuff[8] = 0x03;

	SendLen = 9;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}

}

void CDeviceProgrammingDlg::OnClear() 
{

	mMsg.SetWindowText( "" );
	
}

void CDeviceProgrammingDlg::OnReset() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	mMsg.SetWindowText( "" );
	OnInitDialog();
	
}

void CDeviceProgrammingDlg::OnQuit() 
{

	retCode = SCardDisconnect(hCard, SCARD_UNPOWER_CARD);
	retCode = SCardReleaseContext(hContext);
	CDialog::OnCancel();
	
}


void CDeviceProgrammingDlg::OnAntOn() 
{
	
	rbAntOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnAntOff() 
{
	
	rbAntOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnT1Dur() 
{
	
	rbBuzzOff.SetCheck(false);
	rbT2Dur.SetCheck(false);
	rbT1T2Dur.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnBuzzOff() 
{

	rbT1Dur.SetCheck(false);
	rbT2Dur.SetCheck(false);
	rbT1T2Dur.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnT2Dur() 
{
	
	rbBuzzOff.SetCheck(false);
	rbT1Dur.SetCheck(false);
	rbT1T2Dur.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnT1T2Dur() 
{
	
	rbBuzzOff.SetCheck(false);
	rbT1Dur.SetCheck(false);
	rbT2Dur.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedFinOn() 
{
	rbRedFinOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedFinOff() 
{
	rbRedFinOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedStateOn() 
{
	
	rbRedStateOff.SetCheck(false);

}

void CDeviceProgrammingDlg::OnRedStateOff() 
{
	
	rbRedStateOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedInitOn() 
{
	
	rbRedInitOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedInitOff() 
{
	
	rbRedInitOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedBlinkOn() 
{
	
	rbRedBlinkOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnRedBlinkOff() 
{
	
	rbRedBlinkOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenFinOn() 
{
	
	rbGreenFinOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenFinOff() 
{
	
	rbGreenFinOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenStateOn() 
{
	
	rbGreenStateOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenStateOff() 
{
	
	rbGreenStateOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenInitOn() 
{
	
	rbGreenInitOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenInitOff() 
{
	
	rbGreenInitOn.SetCheck(false);	
	
}

void CDeviceProgrammingDlg::OnGreenBlinkOn() 
{
	
	rbGreenBlinkOff.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGreenBlinkOff() 
{
	
	rbGreenBlinkOn.SetCheck(false);
	
}

void CDeviceProgrammingDlg::OnGetStatus() 
{
	
	int index;
	char tempstr[262];
	char tempstr2[262];

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x02;
	SendBuff[5] = 0xD4;
	SendBuff[6] = 0x04;

	SendLen = 7;
	RecvLen = 12;

	retCode = Transmit();
	if(retCode != SCARD_S_SUCCESS)
	{
		
		return;

	}

	//Interpret data
	sprintf( tempstr, "" );

	for(index = 0; index < RecvLen; index++)
	{
	
		sprintf( tempstr, "%s%02X", tempstr, RecvBuff[index] );
	
	}

	if( (strcmp( tempstr, "D505280000809000" ) == 0) || (strcmp( tempstr, "D505000000809000") == 0))
	{
	
		//no tag is in the field
		sprintf( tempstr2, "> No tag is in the field: %02X", RecvBuff[1] );
		DisplayOut( tempstr2, BLACK ); 
	
	}
	else
	{
	
		//error code
		sprintf( tempstr2, "> Error Code: %02X", RecvBuff[2] );
		DisplayOut( tempstr2, BLACK );
	
		//Field indicates if an external RF field is present and detected
        //(Field=0x01 or not (Field 0x00)
		if( RecvBuff[3] == 0x01 )
		{
		
			sprintf(tempstr2, "> External RF field is Present and detected: %02X", RecvBuff[3]);
			DisplayOut( tempstr2, BLACK );
		
		}
		else
		{
		
			sprintf(tempstr2, "> External RF field is NOT Present and NOT detected: %02X", RecvBuff[3]);
			DisplayOut( tempstr2, BLACK );
		
		}

		//Number of targets currently controlled by the PN532 acting as initiator.The default value is 1
		sprintf( tempstr2, "> Number of Target: %02X", RecvBuff[4] );
		DisplayOut( tempstr2, BLACK );

		//Logical number
		sprintf( tempstr2, "> Number of Target: %02X", RecvBuff[5] );
		DisplayOut( tempstr2, BLACK );

		//Bit rate in reception
		switch( RecvBuff[6] )
		{
		
			case 0x00: sprintf( tempstr2, "> Bit rate in reception: %02X (106 kbps)", RecvBuff[6]);
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x01: sprintf( tempstr2, "> Bit rate in reception: %02X (212 kbps)", RecvBuff[6]);
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x02: sprintf( tempstr2, "> Bit rate in reception: %02X (424 kbps)", RecvBuff[6]);
					   DisplayOut( tempstr2, BLACK );
					   break;
		
		}

		//Bit rate in transmission
		switch( RecvBuff[7] )
		{
		
			case 0x00: sprintf( tempstr2, "> Bit rate in transmission: %02X (106 kbps)", RecvBuff[7]);
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x01: sprintf( tempstr2, "> Bit rate in transmission: %02X (212 kbps)", RecvBuff[7]);
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x02: sprintf( tempstr2, "> Bit rate in transmission: %02X (424 kbps)", RecvBuff[7]);
					   DisplayOut( tempstr2, BLACK );
					   break;
		
		}

		switch( RecvBuff[8] )
		{
		
			case 0x00: sprintf( tempstr2, "> Modulation type: %02X (ISO14443 or MiFare)", RecvBuff[8] );
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x01: sprintf( tempstr2, "> Modulation type: %02X (Active Mode)", RecvBuff[8] );
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x02: sprintf( tempstr2, "> Modulation type: %02X (Innovision Jewel Tag)", RecvBuff[8] );
					   DisplayOut( tempstr2, BLACK );
					   break;

			case 0x10: sprintf( tempstr2, "> Modulation type: %02X (Felica)", RecvBuff[8] );
					   DisplayOut( tempstr2, BLACK );
					   break;
		
		}
	}
}
