// Device Programming.h : main header file for the DEVICE PROGRAMMING application
//

#if !defined(AFX_DEVICEPROGRAMMING_H__3CA07A94_0232_483B_97C6_7D619B4021B3__INCLUDED_)
#define AFX_DEVICEPROGRAMMING_H__3CA07A94_0232_483B_97C6_7D619B4021B3__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingApp:
// See Device Programming.cpp for the implementation of this class
//

class CDeviceProgrammingApp : public CWinApp
{
public:
	CDeviceProgrammingApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDeviceProgrammingApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CDeviceProgrammingApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEVICEPROGRAMMING_H__3CA07A94_0232_483B_97C6_7D619B4021B3__INCLUDED_)
