//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              COtherPICCCardsDlg.cpp
//
//  Description:       This sample program outlines the steps on how to
//                     transact with other PICC cards using ACR122
//
//  Author:            M.J.E.C. Castillo
//
//	Date:              July 30, 2008
//
//	Revision Trail:   (Date/Author/Description)

//====================================================================================================

#include "stdafx.h"
#include "Other PICC Cards.h"
#include "Other PICC CardsDlg.h"

//Define constants//////////////////////////////////////////////////
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262
////////////////////////////////////////////////////////////////////

//Other PICC Cards Inlude File
#include "WINSCARD.h"

//Global Variables
	SCARDCONTEXT			hContext;
	SCARDHANDLE				hCard;
	unsigned long			dwActProtocol;
	DWORD					dwSend, dwRecv, size = 64;
	SCARD_IO_REQUEST		ioRequest;
	int						retCode;
    char					readerName [256];
	DWORD					SendLen, RecvLen, ByteRet;
	BYTE					SendBuff[262], RecvBuff[262];
	SCARD_IO_REQUEST		IO_REQ;
	unsigned char			HByteArray[16];
	int						reqtype;
	bool					autodet = false, validATS, isLE = false;
	COtherPICCCardsDlg	*pThis = NULL;

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

void ClearBuffers();
static CString GetScardErrMsg( int code );
void DisplayOut( CString str, COLORREF color );
int SendAPDU( int SendType );
void Initializer();
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

//Transmits APDU to Card
int SendAPDU( int SendType )
{

	char tempstr[MAX];
	int index;

	ioRequest.dwProtocol = dwActProtocol;
	ioRequest.cbPciLength = sizeof( SCARD_IO_REQUEST );
	
	//Display APDU In
	sprintf( tempstr, "< " );
	for( index = 0; index != SendLen; index++ )
	{
	
		sprintf( tempstr, "%s%02X ", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );
	if( isLE == true )
	{

		SendLen = SendLen - 1;

	}

	retCode = SCardTransmit( hCard,
							 &ioRequest,
							 SendBuff,
							 SendLen,
							 NULL,
							 RecvBuff,
							 &RecvLen );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return retCode;
	}

	
		sprintf( tempstr, "> " );
		switch( SendType )
		{
			
			
		
			case 1: 
				for( index = 0; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				break;

			case 2:	
				for( index = RecvLen - 2; index != RecvLen; index++ )
				{
				
					sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
				}
				if( strcmp( tempstr, "> 6A 81 " ) == 0 )
				{
				
					DisplayOut( "This function is not supported\n", RED );
					return retCode;
				
				}

				validATS = true;

				break;
		

		}
		sprintf( tempstr, "%s\n", tempstr );
		DisplayOut( tempstr, BLACK );
	
	

	
	return retCode;

}

void Initializer()
{

	DisplayOut( "Program Ready\n", GREEN );
	pThis->cbReader.SetWindowText( "" );

	pThis->btnInit.EnableWindow( true );
	pThis->btnClear.EnableWindow( true );
	pThis->btnQuit.EnableWindow( true );
	pThis->btnConnect.EnableWindow( false );
	pThis->btnReset.EnableWindow( false );
	pThis->btnGetData.EnableWindow( false );
	pThis->btnSendCmd.EnableWindow( false );

	pThis->check1.EnableWindow( false );
	pThis->tbCLA.EnableWindow( false );
	pThis->tbINS.EnableWindow( false );
	pThis->tbP1.EnableWindow( false );
	pThis->tbP2.EnableWindow( false );
	pThis->tbLc.EnableWindow( false );
	pThis->tbLe.EnableWindow( false );
	pThis->tbData.EnableWindow( false );

	pThis->check1.SetCheck( false );
	pThis->tbCLA.SetWindowText( "" );
	pThis->tbINS.SetWindowText( "" );
	pThis->tbP1.SetWindowText( "" );
	pThis->tbP2.SetWindowText( "" );
	pThis->tbLc.SetWindowText( "" );
	pThis->tbLe.SetWindowText( "" );
	pThis->tbData.SetWindowText( "" );

	pThis->tbCLA.SetLimitText( 2 );
	pThis->tbINS.SetLimitText( 2 );
	pThis->tbP1.SetLimitText( 2 );
	pThis->tbP2.SetLimitText( 2 );
	pThis->tbLc.SetLimitText( 2 );
	pThis->tbLe.SetLimitText( 2 );

}

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
		data1 == 'F' )
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
// COtherPICCCardsDlg dialog

COtherPICCCardsDlg::COtherPICCCardsDlg(CWnd* pParent /*=NULL*/)
	: CDialog(COtherPICCCardsDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(COtherPICCCardsDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void COtherPICCCardsDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(COtherPICCCardsDlg)
	DDX_Control(pDX, IDC_RICHEDIT1, rbResult);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON7, btnQuit);
	DDX_Control(pDX, IDC_BUTTON6, btnReset);
	DDX_Control(pDX, IDC_BUTTON5, btnClear);
	DDX_Control(pDX, IDC_EDIT7, tbData);
	DDX_Control(pDX, IDC_EDIT6, tbLe);
	DDX_Control(pDX, IDC_EDIT5, tbLc);
	DDX_Control(pDX, IDC_EDIT4, tbP2);
	DDX_Control(pDX, IDC_EDIT3, tbP1);
	DDX_Control(pDX, IDC_EDIT2, tbINS);
	DDX_Control(pDX, IDC_EDIT1, tbCLA);
	DDX_Control(pDX, IDC_BUTTON4, btnSendCmd);
	DDX_Control(pDX, IDC_CHECK1, check1);
	DDX_Control(pDX, IDC_BUTTON3, btnGetData);
	DDX_Control(pDX, IDC_BUTTON2, btnConnect);
	DDX_Control(pDX, IDC_BUTTON1, btnInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(COtherPICCCardsDlg, CDialog)
	//{{AFX_MSG_MAP(COtherPICCCardsDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConnect)
	ON_BN_CLICKED(IDC_BUTTON5, OnClear)
	ON_BN_CLICKED(IDC_BUTTON6, OnReset)
	ON_BN_CLICKED(IDC_BUTTON7, OnQuit)
	ON_BN_CLICKED(IDC_BUTTON3, OnGetData)
	ON_BN_CLICKED(IDC_BUTTON4, OnSendCmd)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// COtherPICCCardsDlg message handlers

BOOL COtherPICCCardsDlg::OnInitDialog()
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
	//  when the application's main window is not a dialog
	SetIcon(m_hIconBig,	TRUE);		// Set big icon
	SetIcon(m_hIconSmall,	FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	pThis = this;
	Initializer();
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void COtherPICCCardsDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void COtherPICCCardsDlg::OnPaint() 
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
HCURSOR COtherPICCCardsDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void COtherPICCCardsDlg::OnInit() 
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

	btnInit.EnableWindow( false );
	btnConnect.EnableWindow( true );

}

void COtherPICCCardsDlg::OnConnect() 
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
							&Protocol );

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

	btnReset.EnableWindow( true );
	btnGetData.EnableWindow( true );
	btnSendCmd.EnableWindow( true );

	check1.EnableWindow( true );
	tbCLA.EnableWindow( true );
	tbINS.EnableWindow( true );
	tbP1.EnableWindow( true );
	tbP2.EnableWindow( true );
	tbLc.EnableWindow( true );
	tbLe.EnableWindow( true );
	tbData.EnableWindow( true );
	
}

void COtherPICCCardsDlg::OnClear() 
{

	rbResult.SetWindowText( "" );

}

void COtherPICCCardsDlg::OnReset() 
{
	
	rbResult.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	Initializer();
	
}

void COtherPICCCardsDlg::OnQuit() 
{
	
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}

void COtherPICCCardsDlg::OnGetData() 
{

	char tempstr[MAX];
	int index;

	validATS = false;
	ClearBuffers();
	SendBuff[0] = 0xFF;					//CLA
	SendBuff[1] = 0xCA;					//INS

	if( check1.GetCheck() == true )
	{
	
		SendBuff[2] = 0x01;				//P1 : ISO 14443 A Card
	
	}
	else
	{
	
		SendBuff[2] = 0x00;				//P1 : Other cards
	
	}

	SendBuff[3] = 0x00;					//P2
	SendBuff[4] = 0x00;					//Le : Full Length

	SendLen = 5;
	RecvLen = 0xFF;

	retCode = SendAPDU( 2 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}

	//Interpret and display return values
	if( validATS == true )
	{
	
		if( check1.GetCheck() == true )
		{
		
			sprintf( tempstr, "UID :" );
		
		}
		else
		{
		
			sprintf( tempstr, "ATS :" );
		
		}

		for( index = 0; index != RecvLen - 2; index++ )
		{
		
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
		
		}

		sprintf( tempstr, "%s\n", tempstr );
		DisplayOut( tempstr, BLACK );

	}
}

void COtherPICCCardsDlg::OnSendCmd() 
{

	char tempstr[32767], holder[4];
	char tempdata[32767], holder2[2];
	int index, tempval, index2;
	bool directCmd;

	directCmd = true;
	isLE = false;

	//Validate inputs
	tbCLA.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
		
		tbCLA.SetWindowText( "00" );
		tbCLA.SetFocus();
		return;
	
	}
	

	sprintf( tempdata, "" );

	ClearBuffers();

	tbCLA.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	SendBuff[0] = tempval;					//CLA

	tbINS.GetWindowText( holder, 4 );
	if( HexCheck( holder[0], holder[1] ) == 0 )
	{
		sscanf( holder, "%X", &tempval );
		if( strcmp( holder, "" ) != 0 )
		{

			SendBuff[1] = tempval;				//INS

		}
	}
	else
	{
	
		tbINS.SetWindowText( "00" );
	
	}
	
	tbP1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	if( strcmp( holder, "" ) != 0 )
	{
	
		directCmd = false;
	
	}

	if( directCmd == false )
	{
	
		SendBuff[2] = tempval;				//P1

		tbP2.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
		{
		
			tbP2.SetWindowText( "00" );
			tbP2.SetFocus();
			return;
		
		}
		else
		{
		
			SendBuff[3] = tempval;			//P2
		
		}

		tbLc.GetWindowText( holder, 4 );
		sscanf( holder, "%X", &tempval );
		if( strcmp( holder, "" ) != 0 )
		{
		
			SendBuff[4] = tempval;			// Lc
			//Process Data In if Lc > 0
			if( SendBuff[4] > 0 )
			{
			
				tbData.GetWindowText( tempstr, 32767 );
				sprintf( tempdata, "" );
				for( index = 0; index < strlen( tempstr ); index++ )
				{

					if( isspace( tempdata[index] ) == 0 )
					{
		
						sprintf( tempdata, "%s%c", tempdata, tempstr[index] );
		
					}
			
				}
				//Check if Data In is consistent with Lc value
				if( SendBuff[4] > ( strlen( tempdata ) / 2 ) )
				{
				
					tbData.SetFocus();
					return;
				
				}
				sprintf( holder2 , "" );
				for( index = index2 = 0; index2 != SendBuff[4]; index = index + 2, index2++ )
				{
				
					sprintf( holder2, "%c", tempdata[index] );
					//Format Data In
					sprintf( holder2, "%s%c", holder, tempdata[index + 1] );
					sscanf( holder2, "%X", &tempval );
					SendBuff[index2 + 5] = tempval;
				
				}

				tbLe.GetWindowText( holder, 4 );
				sscanf( holder, "%X", &tempval );
				if( strcmp( holder, "" ) != 0 )
				{
				
					SendBuff[SendBuff[4] + 5] = tempval;	//Le
				
				}
			
			}
			else
			{
			
				tbLe.GetWindowText( holder, 4 );
				sscanf( holder, "%X", &tempval );
				if( strcmp( holder, "" ) != 0 )
				{
				
					SendBuff[5] = tempval;					//Le
				
				}

			}

		}
		else
		{
		
			tbLe.GetWindowText( holder, 4 );
			sscanf( holder, "%X", &tempval );
			if( strcmp( holder, "" ) != 0 )
			{
			
				SendBuff[4] = tempval;
			
			}
		
		}

	}

	if( directCmd == true )
	{
	
		tbINS.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 )
		{
		
			SendLen = 0x01;
		
		}
		else
		{
		
			SendLen = 0x02;

		}
	
	}
	else
	{
	
		tbLc.GetWindowText( holder, 4 );
		if( strcmp( holder, "" ) == 0 )
		{
		
			tbLe.GetWindowText( holder, 4 );
			if( strcmp( holder, "" ) != 0 )
			{
			
				SendLen = 5;
			
			}
			else
			{
			
				SendLen = 4;
			
			}
		
		}
		else
		{
		
			tbLe.GetWindowText( holder, 4 );
			if( strcmp( holder, "" ) == 0 )
			{
			
				SendLen = SendBuff[4] + 5;
			
			}
			else
			{
			
				SendLen = SendBuff[4] + 6;
				isLE = true;
			}
		
		}
	
	}

	RecvLen = 0xFF;

	retCode = SendAPDU( 1 );
	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;
	
	}
	
}
