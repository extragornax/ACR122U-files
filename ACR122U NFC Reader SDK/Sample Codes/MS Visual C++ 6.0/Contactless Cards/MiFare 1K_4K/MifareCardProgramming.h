// MifareCardProgramming.h : main header file for the MIFARECARDPROGRAMMING application
//

#if !defined(AFX_MIFARECARDPROGRAMMING_H__424861E7_9949_4F2B_AA01_C90B24E195BB__INCLUDED_)
#define AFX_MIFARECARDPROGRAMMING_H__424861E7_9949_4F2B_AA01_C90B24E195BB__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMifareCardProgrammingApp:
// See MifareCardProgramming.cpp for the implementation of this class
//

class CMifareCardProgrammingApp : public CWinApp
{
public:
	CMifareCardProgrammingApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMifareCardProgrammingApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMifareCardProgrammingApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MIFARECARDPROGRAMMING_H__424861E7_9949_4F2B_AA01_C90B24E195BB__INCLUDED_)
