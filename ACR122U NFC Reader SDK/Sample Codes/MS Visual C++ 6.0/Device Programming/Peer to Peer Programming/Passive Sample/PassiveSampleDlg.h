// PassiveSampleDlg.h : header file
//

#if !defined(AFX_PASSIVESAMPLEDLG_H__3AF89FE1_8F69_4E38_8298_21127CB13BB1__INCLUDED_)
#define AFX_PASSIVESAMPLEDLG_H__3AF89FE1_8F69_4E38_8298_21127CB13BB1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CPassiveSampleDlg dialog

class CPassiveSampleDlg : public CDialog
{
// Construction
public:
	CPassiveSampleDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CPassiveSampleDlg)
	enum { IDD = IDD_PASSIVESAMPLE_DIALOG };
	CRichEditCtrl	mMsg;
	CRichEditCtrl	mData;
	CButton	bReset;
	CButton	bQuit;
	CButton	bClear;
	CButton	bSetMode;
	CButton	bConn;
	CButton	bInit;
	CComboBox	cbReader;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CPassiveSampleDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CPassiveSampleDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConn();
	afx_msg void OnSetMode();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_PASSIVESAMPLEDLG_H__3AF89FE1_8F69_4E38_8298_21127CB13BB1__INCLUDED_)
