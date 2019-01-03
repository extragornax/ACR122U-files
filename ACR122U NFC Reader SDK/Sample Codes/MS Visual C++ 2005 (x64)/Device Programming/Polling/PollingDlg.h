// PollingDlg.h : header file
//

#if !defined(AFX_POLLINGDLG_H__7A889FE2_F67E_488D_B8BD_7ADB37ADC040__INCLUDED_)
#define AFX_POLLINGDLG_H__7A889FE2_F67E_488D_B8BD_7ADB37ADC040__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CPollingDlg dialog

class CPollingDlg : public CDialog
{
// Construction
public:
	CPollingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPollingDlg)
	enum { IDD = IDD_POLLING_DIALOG };
	CButton	cbOpt7;
	CButton	cbOpt6;
	CButton	cbOpt5;
	CButton	cbOpt4;
	CButton	cbOpt3;
	CButton	cbOpt2;
	CButton	cbOpt1;
	CButton	rbOpt2;
	CButton	rbOpt1;
	CComboBox	cbReader;
	CRichEditCtrl	mMsg;
	CButton	bClear;
	CButton	bReset;
	CButton	bQuit;
	CButton	bPoll;
	CButton	bSetPoll;
	CButton	bGetPoll;
	CButton	bConn;
	CButton	bInit;
	CStatusBar m_bar;
	//}}AFX_DATA
	
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPollingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CPollingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConn();
	afx_msg void OnGetPoll();
	afx_msg void OnSetPoll();
	afx_msg void OnPoll();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_POLLINGDLG_H__7A889FE2_F67E_488D_B8BD_7ADB37ADC040__INCLUDED_)
