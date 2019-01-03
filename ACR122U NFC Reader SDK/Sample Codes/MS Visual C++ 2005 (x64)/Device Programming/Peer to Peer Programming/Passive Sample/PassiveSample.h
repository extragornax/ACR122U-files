// PassiveSample.h : main header file for the PASSIVESAMPLE application
//

#if !defined(AFX_PASSIVESAMPLE_H__6184A328_DF67_4E99_8A93_9D3A4EA9AD14__INCLUDED_)
#define AFX_PASSIVESAMPLE_H__6184A328_DF67_4E99_8A93_9D3A4EA9AD14__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CPassiveSampleApp:
// See PassiveSample.cpp for the implementation of this class
//

class CPassiveSampleApp : public CWinApp
{
public:
	CPassiveSampleApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPassiveSampleApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CPassiveSampleApp)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PASSIVESAMPLE_H__6184A328_DF67_4E99_8A93_9D3A4EA9AD14__INCLUDED_)
