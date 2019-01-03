// ActiveSample.h : main header file for the ACTIVESAMPLE application
//

#if !defined(AFX_ACTIVESAMPLE_H__60A53C42_4927_4BC8_8AEB_E5D0D956A85C__INCLUDED_)
#define AFX_ACTIVESAMPLE_H__60A53C42_4927_4BC8_8AEB_E5D0D956A85C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CActiveSampleApp:
// See ActiveSample.cpp for the implementation of this class
//

class CActiveSampleApp : public CWinApp
{
public:
	CActiveSampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CActiveSampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CActiveSampleApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ACTIVESAMPLE_H__60A53C42_4927_4BC8_8AEB_E5D0D956A85C__INCLUDED_)
