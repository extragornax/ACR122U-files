// Other PICC CardsDlg.h : header file
//

#if !defined(AFX_OTHERPICCCARDSDLG_H__8089C9E2_CE69_4243_A77E_BE94269FD9D2__INCLUDED_)
#define AFX_OTHERPICCCARDSDLG_H__8089C9E2_CE69_4243_A77E_BE94269FD9D2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// COtherPICCCardsDlg dialog

class COtherPICCCardsDlg : public CDialog
{
// Construction
public:
	COtherPICCCardsDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(COtherPICCCardsDlg)
	enum { IDD = IDD_OTHERPICCCARDS_DIALOG };
	CRichEditCtrl	rbResult;
	CComboBox	cbReader;
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CEdit	tbData;
	CEdit	tbLe;
	CEdit	tbLc;
	CEdit	tbP2;
	CEdit	tbP1;
	CEdit	tbINS;
	CEdit	tbCLA;
	CButton	btnSendCmd;
	CButton	check1;
	CButton	btnGetData;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(COtherPICCCardsDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(COtherPICCCardsDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnGetData();
	afx_msg void OnSendCmd();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_OTHERPICCCARDSDLG_H__8089C9E2_CE69_4243_A77E_BE94269FD9D2__INCLUDED_)
