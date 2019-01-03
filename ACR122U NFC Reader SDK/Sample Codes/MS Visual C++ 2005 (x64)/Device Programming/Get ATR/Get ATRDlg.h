// Get ATRDlg.h : header file
//

#if !defined(AFX_GETATRDLG_H__11134DFC_FC10_408B_8D2A_F7C8765CA32F__INCLUDED_)
#define AFX_GETATRDLG_H__11134DFC_FC10_408B_8D2A_F7C8765CA32F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CGetATRDlg dialog

class CGetATRDlg : public CDialog
{
// Construction
public:
	CGetATRDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CGetATRDlg)
	enum { IDD = IDD_GETATR_DIALOG };
	CButton	btnQuit;
	CButton	btnReset;
	CButton	btnClear;
	CRichEditCtrl	rbResult;
	CComboBox	cbReader;
	CButton	btnGetATR;
	CButton	btnConnect;
	CButton	btnInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CGetATRDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CGetATRDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConnect();
	afx_msg void OnGetATR();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_GETATRDLG_H__11134DFC_FC10_408B_8D2A_F7C8765CA32F__INCLUDED_)
