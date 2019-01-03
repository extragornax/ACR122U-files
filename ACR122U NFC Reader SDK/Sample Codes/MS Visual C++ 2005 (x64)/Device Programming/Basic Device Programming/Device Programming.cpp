// Device Programming.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "Device Programming.h"
#include "Device ProgrammingDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingApp

BEGIN_MESSAGE_MAP(CDeviceProgrammingApp, CWinApp)
	//{{AFX_MSG_MAP(CDeviceProgrammingApp)
	//}}AFX_MSG
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingApp construction

CDeviceProgrammingApp::CDeviceProgrammingApp()
{
}

/////////////////////////////////////////////////////////////////////////////
// The one and only CDeviceProgrammingApp object

CDeviceProgrammingApp theApp;

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingApp initialization

BOOL CDeviceProgrammingApp::InitInstance()
{
	AfxEnableControlContainer();
	AfxInitRichEdit();
	// Standard initialization

#ifdef _AFXDLL
	Enable3dControls();			// Call this when using MFC in a shared DLL
#else
	Enable3dControlsStatic();	// Call this when linking to MFC statically
#endif

	CDeviceProgrammingDlg dlg;
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
