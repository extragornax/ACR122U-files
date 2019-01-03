/*=========================================================================================
  Copyright(C):    Advanced Card Systems Ltd

  File :         PassiveSample.frm

  Description:     This sample program outlines the steps on how to
                   set an ACR122 NFC reader to its passive mode and
                   receive data

  Author :         M.J.E.C.Castillo

  Date   :         August 5, 2008

 Revision Trail:   (Date/Author/Description)

==========================================================================================*/

#include "stdafx.h"
#include "PassiveSample.h"
#include "PassiveSampleDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

//Define constants//////////////////////////////////////////////////
#define IOCTL_CCID_ESCAPE_SCARD_CTL_CODE SCARD_CTL_CODE(3500)
#define BLACK RGB(0, 0, 0)
#define RED RGB(255, 0, 0)
#define GREEN RGB(0, 0x99, 0)
#define MAX 262
////////////////////////////////////////////////////////////////////

//Advanced Device Programming Inlude File
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
	bool					autodet = false;
	CPassiveSampleDlg	*pThis = NULL;

	void ClearBuffers();
	static CString GetScardErrMsg( int code );
	void DisplayOut( CString str, COLORREF color );
	int CallCardControl();
	void Initialize();
	void GetFirmware();
	void RecvData();

void RecvData()
{

	char tempstr[262], tempdata[262];
	int index, dataLen ;
	BYTE temparr[512];

//	pThis->mData.GetWindowText( tempdata, 262 );
//	dataLen = strlen(tempdata);

	ClearBuffers();
	SendBuff[0] = 0xFF;
    SendBuff[1] = 0x00;
    SendBuff[2] = 0x00;
    SendBuff[3] = 0x00;
    SendBuff[4] = 0x02;
    SendBuff[5] = 0xD4;
    SendBuff[6] = 0x86;

	SendLen = 7;
	RecvLen = 6;

	retCode = CallCardControl();

	if (retCode != SCARD_S_SUCCESS)
		return;

	dataLen = RecvBuff[3];

	//Send a response with a value of 90 00
    //to the sending device
	SendBuff[0] = 0xFF;
	SendBuff[1] = 0x00;
	SendBuff[2] = 0x00;
	SendBuff[3] = 0x00;
	SendBuff[4] = 0x04;
	SendBuff[5] = 0xD4;
	SendBuff[6] = 0x8E;
	SendBuff[7] = 0x90;
	SendBuff[8] = 0x00;

	SendLen = 9;
	RecvLen = 5;

	retCode = CallCardControl();

	if (retCode != SCARD_S_SUCCESS)
		return;

	//we recieve the actual data
	ClearBuffers();
	SendBuff[0] = 0xFF;
    SendBuff[1] = 0x00;
    SendBuff[2] = 0x00;
    SendBuff[3] = 0x00;
    SendBuff[4] = 0x02;
    SendBuff[5] = 0xD4;
    SendBuff[6] = 0x86;

	SendLen = 7;
	RecvLen = dataLen + 5;

	retCode = CallCardControl();

	if (retCode != SCARD_S_SUCCESS)
		return;

	sprintf( tempstr, "" );
	for( index = 3; index <= RecvLen-3; index++ )
	{
	
		sprintf( tempstr, "%s%c", tempstr, RecvBuff[index] );
	
	}
	
	//We send the response with a value of 90 00
    //to the sending device

     ClearBuffers();
    SendBuff[0] = 0xFF;
    SendBuff[1] = 0x00;
    SendBuff[2] = 0x00;
    SendBuff[3] = 0x00;
    SendBuff[4] = 0x04;
    SendBuff[5] = 0xD4;
    SendBuff[6] = 0x8E;
    SendBuff[7] = 0x90;
    SendBuff[8] = 0x00;

    SendLen = 9;
    RecvLen = 5;


	retCode = CallCardControl();

	if(retCode != SCARD_S_SUCCESS)
		return;

	pThis->mData.SetWindowText( tempstr );

}

void GetFirmware()
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

	retCode = CallCardControl();

	if( retCode != SCARD_S_SUCCESS )
	{
	
		return;

	}
	
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

void Initialize()
{
	pThis->bConn.EnableWindow(false);
	pThis->bInit.EnableWindow(true);
	pThis->bSetMode.EnableWindow(false);
	pThis->bReset.EnableWindow(false);
	pThis->mData.EnableWindow(false);
	DisplayOut( "Program Ready\n", GREEN );


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

//Displays the message with the corresponding color
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

int CallCardControl()
{

	char tempstr[262];
	char tempstr2[262];
	int index;

	sprintf( tempstr,"< SCardControl: " );
	for( index = 0; index <= SendLen - 1; index++ )
	{
	
		sprintf( tempstr, "%s %02X", tempstr, SendBuff[index] );
	
	}
	sprintf( tempstr, "%s\n", tempstr );
	DisplayOut( tempstr, BLACK );
		
	retCode = SCardControl( hCard,
							IOCTL_CCID_ESCAPE_SCARD_CTL_CODE,
							&SendBuff,
							SendLen,
							&RecvBuff,
							RecvLen,
							&ByteRet );

	if( retCode != SCARD_S_SUCCESS )
	{
		
		DisplayOut( GetScardErrMsg( retCode ), RED );
		return retCode;
	
	}

	sprintf( tempstr2, ">" );
	for( index = 0; index <= RecvLen - 1; index++ )
	{
	
		sprintf( tempstr2, "%s %02X",tempstr2, RecvBuff[index] );
	
	}
	sprintf( tempstr2, "%s \n", tempstr2 );
	DisplayOut( tempstr2, BLACK );

	return retCode;

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
// CPassiveSampleDlg dialog

CPassiveSampleDlg::CPassiveSampleDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CPassiveSampleDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CPassiveSampleDlg)
	//}}AFX_DATA_INIT
	m_hIconBig   = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_hIconSmall = AfxGetApp()->LoadIcon(IDR_SMALLICON);
}

void CPassiveSampleDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CPassiveSampleDlg)
	DDX_Control(pDX, IDC_RICHEDIT2, mMsg);
	DDX_Control(pDX, IDC_RICHEDIT1, mData);
	DDX_Control(pDX, IDC_BUTTON5, bReset);
	DDX_Control(pDX, IDC_BUTTON6, bQuit);
	DDX_Control(pDX, IDC_BUTTON4, bClear);
	DDX_Control(pDX, IDC_BUTTON3, bSetMode);
	DDX_Control(pDX, IDC_BUTTON2, bConn);
	DDX_Control(pDX, IDC_BUTTON1, bInit);
	DDX_Control(pDX, IDC_COMBO1, cbReader);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CPassiveSampleDlg, CDialog)
	//{{AFX_MSG_MAP(CPassiveSampleDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnInit)
	ON_BN_CLICKED(IDC_BUTTON2, OnConn)
	ON_BN_CLICKED(IDC_BUTTON3, OnSetMode)
	ON_BN_CLICKED(IDC_BUTTON4, OnClear)
	ON_BN_CLICKED(IDC_BUTTON5, OnReset)
	ON_BN_CLICKED(IDC_BUTTON6, OnQuit)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPassiveSampleDlg message handlers

BOOL CPassiveSampleDlg::OnInitDialog()
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
	Initialize();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CPassiveSampleDlg::OnSysCommand(UINT nID, LPARAM lParam)
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

void CPassiveSampleDlg::OnPaint() 
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

HCURSOR CPassiveSampleDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIconSmall;
}

void CPassiveSampleDlg::OnInit() 
{
	
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

	index = cbReader.FindStringExact( -1, "ACS ACR122 0" );
	cbReader.SetCurSel( index );

	bConn.EnableWindow( true );
	bInit.EnableWindow( false );
	bReset.EnableWindow( true );
	
}

void CPassiveSampleDlg::OnConn() 
{
	
	DWORD Protocol = 1;
	char buffer1[100];
	char buffer2[100];

	cbReader.GetLBText( cbReader.GetCurSel(), readerName );
	
	//Connect to selected reader
	retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_SHARED,
							SCARD_PROTOCOL_T1,
							&hCard,
							&Protocol );

	if( retCode != SCARD_S_SUCCESS )
	{
	
		//connect to reader directly if no card is polled
		retCode = SCardConnect( hContext,
							readerName,
							SCARD_SHARE_DIRECT,
							0,
							&hCard,
							&Protocol );
			if( retCode != SCARD_S_SUCCESS )
			{
				//Failed to connect to reader
				DisplayOut( GetScardErrMsg( retCode ), RED );
				return;
			}
			else
			{

				//Successful connection to reader
				IO_REQ.dwProtocol = Protocol;
				IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

				cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
				sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
				DisplayOut( buffer1, GREEN );

			}
		
	}
	else
	{

		//Successful connection to reader
		IO_REQ.dwProtocol = Protocol;
		IO_REQ.cbPciLength = sizeof( SCARD_IO_REQUEST );

		cbReader.GetLBText( cbReader.GetCurSel(), buffer2 );
		sprintf( buffer1, "%s %s \n", "Successful connection to ", buffer2 );
		DisplayOut( buffer1, GREEN );
	
	}

	GetFirmware();

	pThis->bConn.EnableWindow( true );
	pThis->bSetMode.EnableWindow( true );
	pThis->mData.EnableWindow( true );
	
}

void CPassiveSampleDlg::OnSetMode() 
{
	char tempstr[262];

	ClearBuffers();
    SendBuff[0] = 0xFF;
    SendBuff[1] = 0x00;
    SendBuff[2] = 0x00;
    SendBuff[3] = 0x00;
    SendBuff[4] = 0x27;
    SendBuff[5] = 0xD4;
    SendBuff[6] = 0x8C;
    SendBuff[7] = 0x00;
    SendBuff[8] = 0x08;
    SendBuff[9] = 0x00;
    SendBuff[10] = 0x12;
    SendBuff[11] = 0x34;
    SendBuff[12] = 0x56;
    SendBuff[13] = 0x40;
    SendBuff[14] = 0x01;
    SendBuff[15] = 0xFE;
    SendBuff[16] = 0xA2;
    SendBuff[17] = 0xA3;
    SendBuff[18] = 0xA4;
    SendBuff[19] = 0xA5;
    SendBuff[20] = 0xA6;
    SendBuff[21] = 0xA7;
    SendBuff[22] = 0xC0;
    SendBuff[23] = 0xC1;
    SendBuff[24] = 0xC2;
    SendBuff[25] = 0xC3;
    SendBuff[26] = 0xC4;
    SendBuff[27] = 0xC5;
    SendBuff[28] = 0xC6;
    SendBuff[29] = 0xC7;
    SendBuff[30] = 0xFF;
    SendBuff[31] = 0xFF;
    SendBuff[32] = 0xAA;
    SendBuff[33] = 0x99;
    SendBuff[34] = 0x88;
    SendBuff[35] = 0x77;
    SendBuff[36] = 0x66;
    SendBuff[37] = 0x55;
    SendBuff[38] = 0x44;
    SendBuff[39] = 0x33;
    SendBuff[40] = 0x22;
    SendBuff[41] = 0x11;
    SendBuff[42] = 0x00;
    SendBuff[43] = 0x00;

    SendLen = 44;
    RecvLen = 22;

	retCode = CallCardControl();

	if(retCode != SCARD_S_SUCCESS)
		return;

	sprintf( tempstr, "> " );
	for(int index = RecvLen - 2; index < RecvLen; index++ )
	{
			
		sprintf( tempstr, "%s%02X ", tempstr, RecvBuff[index] );
			
	}
	
	if( strcmp( tempstr, "> 90 00 " ) != 0 )
	{
			
	
		DisplayOut( "Set Passive Failed!\n" , RED );
			
	}


	RecvData();
	
}

void CPassiveSampleDlg::OnClear() 
{
	
	mMsg.SetWindowText( "" );
	
}

void CPassiveSampleDlg::OnReset() 
{
	
	mMsg.SetWindowText( "" );
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	Initialize();
	
}

void CPassiveSampleDlg::OnQuit() 
{
	
	retCode = SCardDisconnect( hCard, SCARD_UNPOWER_CARD );
	retCode = SCardReleaseContext( hContext );
	CDialog::OnCancel();
	
}
