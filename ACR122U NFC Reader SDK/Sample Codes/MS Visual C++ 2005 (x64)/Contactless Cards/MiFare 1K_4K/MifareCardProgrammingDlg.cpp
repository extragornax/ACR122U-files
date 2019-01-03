/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              MiFareCardProgrammingDlg.cpp

  Description:       This sample program outlines the steps on how to
                     transact with MiFare cards using ACR122

  Author:            M.J.E.C. Castillo

  Date:              July 30, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

#include "stdafx.h"
#include "MifareCardProgramming.h"
#include "MifareCardProgrammingDlg.h"

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
	CMifareCardProgrammingDlg	*pThis = NULL;


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
// CMifareCardProgrammingDlg dialog

CMifareCardProgrammingDlg::CMifareCardProgrammingDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMifareCardProgrammingDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMifareCardProgrammingDlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CMifareCardProgrammingDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMifareCardProgrammingDlg)
	DDX_Control(pDX, IDC_EDIT16, tValTar);
	DDX_Control(pDX, IDC_EDIT15, tValSrc);
	DDX_Control(pDX, IDC_EDIT14, tValBlk);
	DDX_Control(pDX, IDC_EDIT13, tValAmt);
	DDX_Control(pDX, IDC_EDIT12, tData);
	DDX_Control(pDX, IDC_EDIT11, tBinLen);
	DDX_Control(pDX, IDC_EDIT10, tBinBlk);
	DDX_Control(pDX, IDC_RADIO2, rbTypeB);
	DDX_Control(pDX, IDC_RADIO1, rbTypeA);
	DDX_Control(pDX, IDC_EDIT9, tStoreNo);
	DDX_Control(pDX, IDC_EDIT8, tBlkNo);
	DDX_Control(pDX, IDC_EDIT6, tKey5);
	DDX_Control(pDX, IDC_EDIT7, tKey6);
	DDX_Control(pDX, IDC_EDIT5, tKey4);
	DDX_Control(pDX, IDC_EDIT4, tKey3);
	DDX_Control(pDX, IDC_EDIT3, tKey2);
	DDX_Control(pDX, IDC_EDIT2, tKey1);
	DDX_Control(pDX, IDC_EDIT1, tKeyStore);
	DDX_Control(pDX, IDC_RICHEDIT3, mMsg);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	DDX_Control(pDX, IDC_BUTTON14, bQuit);
	DDX_Control(pDX, IDC_BUTTON13, bReset);
	DDX_Control(pDX, IDC_BUTTON12, bClear);
	DDX_Control(pDX, IDC_BUTTON11, bValRes);
	DDX_Control(pDX, IDC_BUTTON10, bValRead);
	DDX_Control(pDX, IDC_BUTTON9, bValDec);
	DDX_Control(pDX, IDC_BUTTON8, bValInc);
	DDX_Control(pDX, IDC_BUTTON7, bValStore);
	DDX_Control(pDX, IDC_BUTTON6, bBinUpd);
	DDX_Control(pDX, IDC_BUTTON5, bBinRead);
	DDX_Control(pDX, IDC_BUTTON4, bAuth);
	DDX_Control(pDX, IDC_BUTTON3, bLoad);
	DDX_Control(pDX, IDC_BUTTON2, bConn);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMifareCardProgrammingDlg, CDialog)
	//{{AFX_MSG_MAP(CMifareCardProgrammingDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConn)
	ON_BN_CLICKED(IDC_BUTTON3, OnLoad)
	ON_BN_CLICKED(IDC_BUTTON4, OnAuth)
	ON_BN_CLICKED(IDC_BUTTON5, OnBinRead)
	ON_BN_CLICKED(IDC_BUTTON6, OnBinUpd)
	ON_BN_CLICKED(IDC_BUTTON7, OnValStore)
	ON_BN_CLICKED(IDC_BUTTON8, OnValInc)
	ON_BN_CLICKED(IDC_BUTTON9, OnValDec)
	ON_BN_CLICKED(IDC_BUTTON10, OnValRead)
	ON_BN_CLICKED(IDC_BUTTON11, OnValRes)
	ON_BN_CLICKED(IDC_BUTTON12, OnClear)
	ON_BN_CLICKED(IDC_BUTTON13, OnReset)
	ON_BN_CLICKED(IDC_BUTTON14, OnQuit)
	ON_BN_CLICKED(IDC_RADIO1, OnTypeA)
	ON_BN_CLICKED(IDC_RADIO2, OnTypeB)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMifareCardProgrammingDlg message handlers

BOOL CMifareCardProgrammingDlg::OnInitDialog()
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
	SetIcon(m_hIconSmall,	FALSE);		// Set small 
	
	// TODO: Add extra initialization here
	pThis = this;
	DisplayOut( "Program Ready\n", GREEN );
	bConn.EnableWindow( false );
	bLoad.EnableWindow( false );
	bAuth.EnableWindow( false );
	bBinRead.EnableWindow( false );
	bBinUpd.EnableWindow( false );
	bValStore.EnableWindow( false );
	bValInc.EnableWindow( false );
	bValDec.EnableWindow( false );
	bValRead.EnableWindow( false );
	bValRes.EnableWindow( false );
	bReset.EnableWindow( false );
	tKeyStore.EnableWindow( false );
	tKey1.EnableWindow( false );
	tKey2.EnableWindow( false );
	tKey3.EnableWindow( false );
	tKey4.EnableWindow( false );
	tKey5.EnableWindow( false );
	tKey6.EnableWindow( false );
	tStoreNo.EnableWindow( false );
	tBinLen.EnableWindow( false );
	tValBlk.EnableWindow( false );
	tValSrc.EnableWindow( false );
	tValTar.EnableWindow( false );
	tBlkNo.EnableWindow( false );
	tBinBlk.EnableWindow( false );
	tValAmt.EnableWindow( false );
	rbTypeA.EnableWindow( false );
	rbTypeB.EnableWindow( false );
	tData.EnableWindow( false );

	pThis->tKeyStore.SetLimitText( 2 );
	pThis->tKey1.SetLimitText( 2 );
	pThis->tKey2.SetLimitText( 2 );
	pThis->tKey3.SetLimitText( 2 );
	pThis->tKey4.SetLimitText( 2 );
	pThis->tKey5.SetLimitText( 2 );
	pThis->tKey6.SetLimitText( 2 );
	pThis->tStoreNo.SetLimitText( 2 );
	pThis->tBinLen.SetLimitText( 2 );
	pThis->tValBlk.SetLimitText( 2 );
	pThis->tValSrc.SetLimitText( 2 );
	pThis->tValTar.SetLimitText( 2 );
	pThis->tBlkNo.SetLimitText( 3 );
	pThis->tBinBlk.SetLimitText( 3 );
	pThis->tValAmt.SetLimitText( 10 );
	
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CMifareCardProgrammingDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CMifareCardProgrammingDlg::OnPaint() 
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
HCURSOR CMifareCardProgrammingDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CMifareCardProgrammingDlg::OnInit() 
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

void CMifareCardProgrammingDlg::OnConn() 
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

	bLoad.EnableWindow( true );
	bAuth.EnableWindow( true );
	bBinRead.EnableWindow( true );
	bBinUpd.EnableWindow( true );
	bValStore.EnableWindow( true );
	bValInc.EnableWindow( true );
	bValDec.EnableWindow( true );
	bValRead.EnableWindow( true );
	bValRes.EnableWindow( true );
	bReset.EnableWindow( true );
	tKeyStore.EnableWindow( true );
	tKey1.EnableWindow( true );
	tKey2.EnableWindow( true );
	tKey3.EnableWindow( true );
	tKey4.EnableWindow( true );
	tKey5.EnableWindow( true );
	tKey6.EnableWindow( true );
	tStoreNo.EnableWindow( true );
	tBinLen.EnableWindow( true );
	tValBlk.EnableWindow( true );
	tValSrc.EnableWindow( true );
	tValTar.EnableWindow( true );
	tBlkNo.EnableWindow( true );
	tBinBlk.EnableWindow( true );
	tValAmt.EnableWindow( true );
	rbTypeA.SetCheck( true );
	rbTypeA.EnableWindow( true );
	rbTypeB.EnableWindow( true );
	tData.EnableWindow( true );

}

void CMifareCardProgrammingDlg::OnLoad() 
{
	char holder[4];
	int tempVal;
	char tempstr[262];

	tKeyStore.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKeyStore.SetWindowText("");
		tKeyStore.SetFocus();
		return;
	
	}
	else
		if(atoi (holder) >1)
		{
			tKeyStore.SetWindowText("1");
			return;
		}


	tKey1.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey1.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey1.SetWindowText("FF");
			return;
		}

	tKey2.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey2.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey2.SetWindowText("FF");
			return;
		}

	tKey3.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey3.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey3.SetWindowText("FF");
			return;
		}

	tKey4.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey4.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey4.SetWindowText("FF");
			return;
		}

	tKey5.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey5.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey5.SetWindowText("FF");
			return;
		}

	tKey6.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0 )
	{
	
		tKey6.SetFocus();
		return;
	
	}
	else
		if( strcmp( holder, "FF" ) != 0 )
		{
			tKey6.SetWindowText("FF");
			return;
		}

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x82;
	SendBuff[2] = 0x00;

	tKeyStore.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[3] = tempVal;

	SendBuff[4] = 0x06;

	tKey1.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[5] = tempVal;

	tKey2.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[6] = tempVal;

	tKey3.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[7] = tempVal;

	tKey4.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[8] = tempVal;

	tKey5.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[9] = tempVal;

	tKey6.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[10] = tempVal;

	SendLen = 11;
	RecvLen = 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{
				
		
			DisplayOut( "Load Authentication Keys Error!\n" , RED );
				
		}

	}


}

void CMifareCardProgrammingDlg::OnAuth() 
{
	
	char holder[4];
	int tempVal;
	char tempstr[262];

	tBlkNo.GetWindowText( holder, 3 );
	sscanf( holder, "%d", &tempVal );
	if( strcmp( holder, "" ) == 0 || HexCheck( holder[0], holder[1] ) != 0)
	{
	
		tBlkNo.SetFocus();
		return;
	
	}
	else
		if( tempVal > 319 )
		{
			tBlkNo.SetWindowText("319");
			tBlkNo.SetFocus();
			return;
		}

	tStoreNo.GetWindowText( holder, 3 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tStoreNo.SetFocus();
		return;
	
	}
	else
		if( atoi (holder) > 1 )
		{
			tStoreNo.SetWindowText("1");
			return;
		}

	ClearBuffers();
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x86;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x05;
	SendBuff[5] = 0x01;
	SendBuff[6] = 0x00;
	
	tBlkNo.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[7] = tempVal;

	if(rbTypeA.GetCheck() == true)
		SendBuff[8] = 0x60;
	else
		SendBuff[8] = 0x61;

	tStoreNo.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempVal );
	SendBuff[9] = tempVal;

	SendLen = 10;
	RecvLen = 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) == 0 )
		{
				
		
			DisplayOut( "Authentication success!\n" , BLACK );
				
		}
		else
		{

			DisplayOut( "Authentication failed!\n" , RED );

		}

	}

	
}

void CMifareCardProgrammingDlg::OnBinRead() 
{
	
	char tempstr[262], holder[4];
	int index, tempval;

	tData.SetWindowText( "" );
	
	//Validate inputs
	tBinBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tBinBlk.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tBinBlk.SetWindowText( "319" );
		tBinBlk.SetFocus();
		return;
	
	}

	tBinLen.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tBinLen.SetFocus();
		return;
	
	}
	else if( tempval < 16 )
	{
	
		tBinLen.SetWindowText( "16" );
		tBinLen.SetFocus();
		return;
	
	}
	
	ClearBuffers();
	SendBuff[0] = 0xFF;						
	SendBuff[1] = 0xB0;						
	SendBuff[2] = 0x00;	
	
	tBinBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	SendBuff[3] = tempval;				

	tBinLen.GetWindowText( holder, 4 );
	SendBuff[4] = atoi(holder);		

	SendLen = 5;
	RecvLen = SendBuff[4] + 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) == 0 )
		{
				
		
			sprintf( tempstr, "" );
			for(int index = 0; index < RecvLen-2; index++ )
			{
			
				sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
			
			}

			tData.SetWindowText( tempstr );
				
		}
		else
		{

			DisplayOut( "Read block error!\n" , RED );

		}

	}

}

void CMifareCardProgrammingDlg::OnBinUpd() 
{

	char tempstr[262], holder[50];
	int index, tempval;
	
	//Validate inputs
	tBinBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tBinBlk.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tBinBlk.SetWindowText( "319" );
		tBinBlk.SetFocus();
		return;
	
	}

	tData.GetWindowText( holder, 50 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tData.SetFocus();
		return;
	
	}

	tBinLen.GetWindowText( holder, 4 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tBinLen.SetFocus();
		return;
	
	}

	tData.GetWindowText( tempstr, 50 );

	ClearBuffers();
	SendBuff[0] = 0xFF;						
	SendBuff[1] = 0xD6;						
	SendBuff[2] = 0x00;	
	
	tBinBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%X", &tempval );
	SendBuff[3] = tempval;				

	tBinLen.GetWindowText( holder, 4 );
	SendBuff[4] = atoi(holder);	
	
	for( index = 0; index < strlen( tempstr ); index++ )
	{
	
		SendBuff[index + 5] = int( tempstr[index] );		
	}
	
	SendLen = SendBuff[4] + 5;
	RecvLen = 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) == 0 )
		{

			tData.SetWindowText( "" );
				
		}
		else
		{

			DisplayOut( "Update block error!\n" , RED );

		}

	}

}

void CMifareCardProgrammingDlg::OnValStore() 
{
	
	char holder[12],tempstr[262];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;

	//Validate Inputs
	tValAmt.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValAmt.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tValAmt.SetWindowText( "4294967294" );
		tValAmt.SetFocus();
		return;
	
	}

	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValBlk.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tValBlk.SetWindowText( "319" );
		tValBlk.SetFocus();
		return;
	
	}

	tValSrc.SetWindowText( "" );
	tValTar.SetWindowText( "" );

	tValAmt.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;				
	SendBuff[1] = 0xD7;						
	SendBuff[2] = 0x00;			
	
	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						

	SendBuff[4] = 0x05;						
	SendBuff[5] = 0x00;						
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	
	SendBuff[9] = ( Amount & 0xFF );		

	SendLen = 10;
	RecvLen = 2;

		retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{

			DisplayOut( "Store Value error!\n" , RED );
				
		}

	}
	
}

void CMifareCardProgrammingDlg::OnValInc() 
{

		char holder[12],tempstr[262];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;

	//Validate Inputs
	tValAmt.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValAmt.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tValAmt.SetWindowText( "4294967294" );
		tValAmt.SetFocus();
		return;
	
	}

	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValBlk.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tValBlk.SetWindowText( "319" );
		tValBlk.SetFocus();
		return;
	
	}

	tValSrc.SetWindowText( "" );
	tValTar.SetWindowText( "" );

	tValAmt.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;				
	SendBuff[1] = 0xD7;						
	SendBuff[2] = 0x00;			
	
	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						

	SendBuff[4] = 0x05;						
	SendBuff[5] = 0x01;						
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	
	SendBuff[9] = ( Amount & 0xFF );		

	SendLen = 10;
	RecvLen = 2;

		retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{

			DisplayOut( "Increment Value error!\n" , RED );
				
		}

	}
	
}

void CMifareCardProgrammingDlg::OnValDec() 
{
	
		char holder[12],tempstr[262];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;

	//Validate Inputs
	tValAmt.GetWindowText( holder, 12 );
	sscanf( holder, "%lu", &tempval1 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValAmt.SetFocus();
		return;
	
	}
	else if( tempval1 > 4294967294 )
	{
	
		tValAmt.SetWindowText( "4294967294" );
		tValAmt.SetFocus();
		return;
	
	}

	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValBlk.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tValBlk.SetWindowText( "319" );
		tValBlk.SetFocus();
		return;
	
	}

	tValSrc.SetWindowText( "" );
	tValTar.SetWindowText( "" );

	tValAmt.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval1 );
	Amount = tempval1;

	ClearBuffers();
	SendBuff[0] = 0xFF;				
	SendBuff[1] = 0xD7;						
	SendBuff[2] = 0x00;			
	
	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;						

	SendBuff[4] = 0x05;						
	SendBuff[5] = 0x02;						
	
	SendBuff[6] = ( ( Amount >> 24 ) & 0xFF );	
	SendBuff[7] = ( ( Amount >> 16 ) & 0xFF );
	SendBuff[8] = ( ( Amount >> 8 ) & 0xFF );	
	SendBuff[9] = ( Amount & 0xFF );		

	SendLen = 10;
	RecvLen = 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{

			DisplayOut( "Decrement Value error!\n" , RED );
				
		}

	}

	
}

void CMifareCardProgrammingDlg::OnValRead() 
{
	
	char tempstr[262], holder[12];
	int tempval2;
	unsigned long tempval1;
	DWORD Amount;
	
	//Validate inputs
	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValBlk.SetFocus();
		return;
	
	}
	else if( tempval2 > 319 )
	{
	
		tValBlk.SetWindowText( "319" );
		tValBlk.SetFocus();
		return;
	
	}
	
	tValAmt.SetWindowText( "" );
	tValSrc.SetWindowText( "" );
	tValTar.SetWindowText( "" );

	ClearBuffers();
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xB1;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tValBlk.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval2 );
	SendBuff[3] = tempval2;					//P2 : Block No.

	SendBuff[4] = 0x04;						//Le

	SendLen = 5;
	RecvLen = 6;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) == 0 )
		{

			Amount = RecvBuff[3];
			Amount = Amount + ( RecvBuff[2] * 256 );
			Amount = Amount + ( RecvBuff[1] * 256 * 256 );
			Amount = Amount + ( RecvBuff[0] * 256 * 256 * 256 );

			sprintf( tempstr, "%lu", Amount );
			tValAmt.SetWindowText( tempstr );
				
		}
		else
		{

			DisplayOut( "Read Value error!\n" , RED );

		}

	}
	
}

void CMifareCardProgrammingDlg::OnValRes() 
{

	char holder[4], tempstr[262];
	int tempval;
	
	//Validate inputs
	tValSrc.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValSrc.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tValSrc.SetWindowText( "319" );
		tValSrc.SetFocus();
		return;
	
	}

	tBlkNo.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	if( strcmp( holder, "" ) == 0 )
	{
	
		tValTar.SetFocus();
		return;
	
	}
	else if( tempval > 319 )
	{
	
		tValTar.SetWindowText( "319" );
		tValTar.SetFocus();
		return;
	
	}

	tValAmt.SetWindowText( "" );
	tValBlk.SetWindowText( "" );

	ClearBuffers();	
	SendBuff[0] = 0xFF;						//CLA
	SendBuff[1] = 0xD7;						//INS
	SendBuff[2] = 0x00;						//P1
	
	//First put the input in a char array
	//then put it into a int variable using
	//sscanf
	tValSrc.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[3] = tempval;					//P2 : Source Block No.

	SendBuff[4] = 0x02;						//Lc
	SendBuff[5] = 0x03;						//Data In Byte 1

	tValTar.GetWindowText( holder, 4 );
	sscanf( holder, "%d", &tempval );
	SendBuff[6] = tempval;					//P2 : Target Block No.
	
	SendLen = 7;
	RecvLen = 2;

	retCode = Transmit();

	if( retCode != SCARD_S_SUCCESS )
	{

		return;

	}
	else
	{

		sprintf( tempstr, "> " );
		for(int index = RecvLen - 2; index < RecvLen; index++ )
		{
				
			sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
				
		}
		
		if( strcmp( tempstr, "> 90 00 " ) != 0 )
		{

			DisplayOut( "Restore Value error!\n" , RED );
				
		}


	}
	
}

void CMifareCardProgrammingDlg::OnClear() 
{
	
	mMsg.SetWindowText( "" );
	
}

void CMifareCardProgrammingDlg::OnReset() 
{
	
	mMsg.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	OnInitDialog();
	
}

void CMifareCardProgrammingDlg::OnQuit() 
{

	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}

void CMifareCardProgrammingDlg::OnTypeA() 
{
	
	rbTypeB.SetCheck( false );
	
}

void CMifareCardProgrammingDlg::OnTypeB() 
{

	rbTypeA.SetCheck( false );
	
}
