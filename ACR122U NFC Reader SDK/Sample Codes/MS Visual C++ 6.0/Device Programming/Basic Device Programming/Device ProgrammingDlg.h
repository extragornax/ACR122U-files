// Device ProgrammingDlg.h : header file
//

#if !defined(AFX_DEVICEPROGRAMMINGDLG_H__1AFB1888_0299_409D_96BB_4101DF5BE5E9__INCLUDED_)
#define AFX_DEVICEPROGRAMMINGDLG_H__1AFB1888_0299_409D_96BB_4101DF5BE5E9__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CDeviceProgrammingDlg dialog

class CDeviceProgrammingDlg : public CDialog
{
// Construction
public:
	CDeviceProgrammingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CDeviceProgrammingDlg)
	enum { IDD = IDD_DEVICEPROGRAMMING_DIALOG };
	CButton	btnStatus;
	CButton	rbGreenBlinkOff;
	CButton	rbGreenBlinkOn;
	CButton	rbGreenInitOff;
	CButton	rbGreenInitOn;
	CButton	rbGreenStateOff;
	CButton	rbGreenStateOn;
	CButton	rbGreenFinOff;
	CButton	rbGreenFinOn;
	CButton	rbRedBlinkOff;
	CButton	rbRedBlinkOn;
	CButton	rbRedInitOff;
	CButton	rbRedInitOn;
	CButton	rbRedStateOff;
	CButton	rbRedStateOn;
	CButton	rbRedFinOff;
	CButton	rbRedFinOn;
	CButton	rbAntOn;
	CButton	rbAntOff;
	CButton	rbT1T2Dur;
	CButton	rbT2Dur;
	CButton	rbT1Dur;
	CButton	rbBuzzOff;
	CEdit	tRepeat;
	CEdit	tT2;
	CEdit	tT1;
	CComboBox	cbReader;
	CRichEditCtrl	mMsg;
	CButton	bQuit;
	CButton	bReset;
	CButton	bClear;
	CButton	bSetLED;
	CButton	bSetAnt;
	CButton	bGetFW;
	CButton	bConn;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CDeviceProgrammingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CDeviceProgrammingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConn();
	afx_msg void OnGetFW();
	afx_msg void OnSetAnt();
	afx_msg void OnSetLED();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnAntOn();
	afx_msg void OnAntOff();
	afx_msg void OnT1Dur();
	afx_msg void OnBuzzOff();
	afx_msg void OnT2Dur();
	afx_msg void OnT1T2Dur();
	afx_msg void OnRedFinOn();
	afx_msg void OnRedFinOff();
	afx_msg void OnRedStateOn();
	afx_msg void OnRedStateOff();
	afx_msg void OnRedInitOn();
	afx_msg void OnRedInitOff();
	afx_msg void OnRedBlinkOn();
	afx_msg void OnRedBlinkOff();
	afx_msg void OnGreenFinOn();
	afx_msg void OnGreenFinOff();
	afx_msg void OnGreenStateOn();
	afx_msg void OnGreenStateOff();
	afx_msg void OnGreenInitOn();
	afx_msg void OnGreenInitOff();
	afx_msg void OnGreenBlinkOn();
	afx_msg void OnGreenBlinkOff();
	afx_msg void OnGetStatus();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DEVICEPROGRAMMINGDLG_H__1AFB1888_0299_409D_96BB_4101DF5BE5E9__INCLUDED_)
