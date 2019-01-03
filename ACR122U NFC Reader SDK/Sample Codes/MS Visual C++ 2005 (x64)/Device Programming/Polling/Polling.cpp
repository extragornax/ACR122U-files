// Polling.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "Polling.h"
#include "PollingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CPollingApp

BEGIN_MESSAGE_MAP(CPollingApp, CWinApp)
	//{{AFX_MSG_MAP(CPollingApp)
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CPollingApp construction

CPollingApp::CPollingApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CPollingApp object

CPollingApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CPollingApp initialization

BOOL CPollingApp::InitInstance()
{
	AfxEnableControlContainer();
	AfxInitRichEdit();
	// Standard initialization

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CPollingDlg dlg;
	m_pMainWnd = &dlg;
	int nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
	}
	else if (nResponse == IDCANCEL)
	{
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}
