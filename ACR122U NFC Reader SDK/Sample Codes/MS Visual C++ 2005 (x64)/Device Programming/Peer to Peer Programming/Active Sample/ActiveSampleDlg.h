// ActiveSampleDlg.h : header file
//

#if !defined(AFX_ACTIVESAMPLEDLG_H__989FEF4F_C6FA_4C05_A5CB_2077AD529CAA__INCLUDED_)
#define AFX_ACTIVESAMPLEDLG_H__989FEF4F_C6FA_4C05_A5CB_2077AD529CAA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CActiveSampleDlg dialog

class CActiveSampleDlg : public CDialog
{
// Construction
public:
	CActiveSampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CActiveSampleDlg)
	enum { IDD = IDD_ACTIVESAMPLE_DIALOG };
	CRichEditCtrl	mMsg;
	CRichEditCtrl	mData;
	CButton	bQuit;
	CButton	bReset;
	CButton	bClear;
	CButton	bSetMode;
	CButton	bConn;
	CButton	bInit;
	CComboBox	cbReader;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CActiveSampleDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CActiveSampleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConn();
	afx_msg void OnSetMode();
	afx_msg void OnClear();
	afx_msg void Onzreset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_ACTIVESAMPLEDLG_H__989FEF4F_C6FA_4C05_A5CB_2077AD529CAA__INCLUDED_)
