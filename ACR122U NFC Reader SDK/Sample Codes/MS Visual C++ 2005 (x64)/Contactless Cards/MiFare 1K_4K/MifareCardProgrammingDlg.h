// MifareCardProgrammingDlg.h : header file
//

#if !defined(AFX_MIFARECARDPROGRAMMINGDLG_H__696ED0E0_C6D3_4FBA_A6E7_20CE27CF3F51__INCLUDED_)
#define AFX_MIFARECARDPROGRAMMINGDLG_H__696ED0E0_C6D3_4FBA_A6E7_20CE27CF3F51__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CMifareCardProgrammingDlg dialog

class CMifareCardProgrammingDlg : public CDialog
{
// Construction
public:
	CMifareCardProgrammingDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMifareCardProgrammingDlg)
	enum { IDD = IDD_MIFARECARDPROGRAMMING_DIALOG };
	CEdit	tValTar;
	CEdit	tValSrc;
	CEdit	tValBlk;
	CEdit	tValAmt;
	CEdit	tData;
	CEdit	tBinLen;
	CEdit	tBinBlk;
	CButton	rbTypeB;
	CButton	rbTypeA;
	CEdit	tStoreNo;
	CEdit	tBlkNo;
	CEdit	tKey5;
	CEdit	tKey6;
	CEdit	tKey4;
	CEdit	tKey3;
	CEdit	tKey2;
	CEdit	tKey1;
	CEdit	tKeyStore;
	CRichEditCtrl	mMsg;
	CComboBox	cbReader;
	CButton	bQuit;
	CButton	bReset;
	CButton	bClear;
	CButton	bValRes;
	CButton	bValRead;
	CButton	bValDec;
	CButton	bValInc;
	CButton	bValStore;
	CButton	bBinUpd;
	CButton	bBinRead;
	CButton	bAuth;
	CButton	bLoad;
	CButton	bConn;
	CButton	bInit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMifareCardProgrammingDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON 	m_hIconBig,
			m_hIconSmall;

	// Generated message map functions
	//{{AFX_MSG(CMifareCardProgrammingDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnInit();
	afx_msg void OnConn();
	afx_msg void OnLoad();
	afx_msg void OnAuth();
	afx_msg void OnBinRead();
	afx_msg void OnBinUpd();
	afx_msg void OnValStore();
	afx_msg void OnValInc();
	afx_msg void OnValDec();
	afx_msg void OnValRead();
	afx_msg void OnValRes();
	afx_msg void OnClear();
	afx_msg void OnReset();
	afx_msg void OnQuit();
	afx_msg void OnTypeA();
	afx_msg void OnTypeB();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MIFARECARDPROGRAMMINGDLG_H__696ED0E0_C6D3_4FBA_A6E7_20CE27CF3F51__INCLUDED_)
