// Other PICC Cards.h : main header file for the OTHER PICC CARDS application
//

#if !defined(AFX_OTHERPICCCARDS_H__BD711DAE_B39B_4FEB_AB59_7EBC24EF9E4B__INCLUDED_)
#define AFX_OTHERPICCCARDS_H__BD711DAE_B39B_4FEB_AB59_7EBC24EF9E4B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// COtherPICCCardsApp:
// See Other PICC Cards.cpp for the implementation of this class
//

class COtherPICCCardsApp : public CWinApp
{
public:
	COtherPICCCardsApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(COtherPICCCardsApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(COtherPICCCardsApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_OTHERPICCCARDS_H__BD711DAE_B39B_4FEB_AB59_7EBC24EF9E4B__INCLUDED_)
