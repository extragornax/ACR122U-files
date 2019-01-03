/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mifareProg.java

  Description:       This sample program outlines the steps on how to
                     transact with MiFare cards using ACR122

  Author:            M.J.E.C. Castillo

  Date:              July 28, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.filechooser.*;
import javax.swing.filechooser.FileFilter;

public class mainMifareProg extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive, validATS; 
	static String VALIDCHARS = "0123456789";
	static String VALIDCHARSHEX = "ABCDEFabcdef0123456789";
	Timer timer;
	
	//All variables that requires pass-by-reference calls to functions are
	//declared as 'Array of int' with length 1
	//Java does not process pass-by-ref to int-type variables, thus Array of int was used.
	int [] ATRLen = new int[1]; 
	int [] hContext = new int[1]; 
	int [] cchReaders = new int[1];
	int [] hCard = new int[1];
	int [] PrefProtocols = new int[1]; 		
	int [] RecvLen = new int[1];
	int SendLen = 0;
	int [] nBytesRet =new int[1];
	byte [] SendBuff = new byte[262];
	byte [] RecvBuff = new byte[262];
	byte [] szReaders = new byte[1024];
	int reqType;
	ACSModule.SCARD_READERSTATE rdrState = new ACSModule.SCARD_READERSTATE();
	
	//GUI Variables
    private JButton bBinRead, bClear, bConn, bInit, bLoadKey, bReset, bValDec;
    private JButton bValInc, bValRead, bValRes, bValStore, bauth, bBinUpd, bQuit;
    private JComboBox cbReader;
    private JLabel jLabel1, jLabel10, jLabel11, jLabel12, jLabel2, jLabel3;
    private JLabel jLabel4, jLabel6, jLabel7, jLabel8, jLabel9, lblReader;
    private JPanel keyTypePanel, keyValPanel, authPanel, binBlkPanel, msgPanel;
    private JTextArea mMsg;
    private JRadioButton rbKeyA, rbKeyB;
    private JPanel readerPanel, valBlkPanel, storeAuthPanel;
    private JScrollPane scrlPaneMsg;
    private JTextField tBinBlk, tBinData, tBinLen, tBlkNo, tKey1, tKey2, tKey3;
    private JTextField tKey4, tKey5,tKey6, tKeyAdd, tMemAdd, tValAmt, tValBlk;
    private JTextField tValSrc, tValTar;
    private ButtonGroup bgKey;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public mainMifareProg() {
    	
    	this.setTitle("Mifare Card Programming");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(750,620);
        readerPanel = new JPanel();
        lblReader = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        bConn = new JButton();
        authPanel = new JPanel();
        jLabel3 = new JLabel();
        jLabel4 = new JLabel();
        keyTypePanel = new JPanel();
        rbKeyA = new JRadioButton();
        rbKeyB = new JRadioButton();
        bauth = new JButton();
        tKeyAdd = new JTextField();
        tBlkNo = new JTextField();
        binBlkPanel = new JPanel();
        jLabel6 = new JLabel();
        jLabel7 = new JLabel();
        jLabel8 = new JLabel();
        tBinBlk = new JTextField();
        tBinLen = new JTextField();
        tBinData = new JTextField();
        bBinRead = new JButton();
        bBinUpd = new JButton();
        storeAuthPanel = new JPanel();
        jLabel1 = new JLabel();
        jLabel2 = new JLabel();
        bLoadKey = new JButton();
        keyValPanel = new JPanel();
        tKey1 = new JTextField();
        tKey2 = new JTextField();
        tKey3 = new JTextField();
        tKey4 = new JTextField();
        tKey5 = new JTextField();
        tKey6 = new JTextField();
        tMemAdd = new JTextField();
        valBlkPanel = new JPanel();
        jLabel9 = new JLabel();
        tValAmt = new JTextField();
        jLabel10 = new JLabel();
        tValBlk = new JTextField();
        jLabel11 = new JLabel();
        tValSrc = new JTextField();
        jLabel12 = new JLabel();
        tValTar = new JTextField();
        bValStore = new JButton();
        bValInc = new JButton();
        bValDec = new JButton();
        bValRead = new JButton();
        bValRes = new JButton();
        msgPanel = new JPanel();
        scrlPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        bgKey = new ButtonGroup();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        lblReader.setText("Select Reader");

        cbReader.setModel(new javax.swing.DefaultComboBoxModel(new String[] { "Select reader" }));

        bInit.setText("Initialize");

        bConn.setText("Connect");

        javax.swing.GroupLayout readerPanel6Layout = new javax.swing.GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanel6Layout);
        readerPanel6Layout.setHorizontalGroup(
            readerPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(readerPanel6Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(readerPanel6Layout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 207, Short.MAX_VALUE))
                    .addComponent(bInit, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 110, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(bConn, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 109, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        readerPanel6Layout.setVerticalGroup(
            readerPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(readerPanel6Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanel6Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bInit)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bConn)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        authPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Authentication Function"));

        jLabel3.setText("Block No. (Dec)");

        jLabel4.setText("Key Store No.");

        keyTypePanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Key Type"));
        bgKey.add(rbKeyA);
        bgKey.add(rbKeyB);
        rbKeyA.setText("Key A");

        rbKeyB.setText("Key B");

        javax.swing.GroupLayout keyTypePanelLayout = new javax.swing.GroupLayout(keyTypePanel);
        keyTypePanel.setLayout(keyTypePanelLayout);
        keyTypePanelLayout.setHorizontalGroup(
            keyTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(keyTypePanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(keyTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(rbKeyA)
                    .addComponent(rbKeyB))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        keyTypePanelLayout.setVerticalGroup(
            keyTypePanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(keyTypePanelLayout.createSequentialGroup()
                .addComponent(rbKeyA)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbKeyB))
        );

        bauth.setText("Authenticate");

        javax.swing.GroupLayout authPanelLayout = new javax.swing.GroupLayout(authPanel);
        authPanel.setLayout(authPanelLayout);
        authPanelLayout.setHorizontalGroup(
            authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(authPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(jLabel3)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBlkNo, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(jLabel4)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(tKeyAdd, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 63, Short.MAX_VALUE)
                .addComponent(keyTypePanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(59, 59, 59))
            .addGroup(authPanelLayout.createSequentialGroup()
                .addGap(78, 78, 78)
                .addComponent(bauth, javax.swing.GroupLayout.PREFERRED_SIZE, 130, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(104, Short.MAX_VALUE))
        );
        authPanelLayout.setVerticalGroup(
            authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(authPanelLayout.createSequentialGroup()
                .addGroup(authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addGroup(authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tBlkNo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(5, 5, 5)
                        .addGroup(authPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel4)
                            .addComponent(tKeyAdd, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(22, 22, 22))
                    .addGroup(authPanelLayout.createSequentialGroup()
                        .addComponent(keyTypePanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bauth)
                .addContainerGap())
        );

        binBlkPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Binary Block Function"));

        jLabel6.setText("Block No.");

        jLabel7.setText("Length");

        jLabel8.setText("Data (text)");

        bBinRead.setText("Read Block");

        bBinUpd.setText("Update Block");

        javax.swing.GroupLayout binBlkPanelLayout = new javax.swing.GroupLayout(binBlkPanel);
        binBlkPanel.setLayout(binBlkPanelLayout);
        binBlkPanelLayout.setHorizontalGroup(
            binBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(binBlkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(binBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(tBinData, javax.swing.GroupLayout.DEFAULT_SIZE, 292, Short.MAX_VALUE)
                    .addGroup(binBlkPanelLayout.createSequentialGroup()
                        .addGap(25, 25, 25)
                        .addComponent(bBinRead, javax.swing.GroupLayout.PREFERRED_SIZE, 112, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(bBinUpd, javax.swing.GroupLayout.PREFERRED_SIZE, 107, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(binBlkPanelLayout.createSequentialGroup()
                        .addComponent(jLabel6)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBinBlk, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(53, 53, 53)
                        .addComponent(jLabel7)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(tBinLen, javax.swing.GroupLayout.PREFERRED_SIZE, 42, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel8))
                .addContainerGap())
        );
        binBlkPanelLayout.setVerticalGroup(
            binBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(binBlkPanelLayout.createSequentialGroup()
                .addGroup(binBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel6)
                    .addComponent(tBinBlk, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel7)
                    .addComponent(tBinLen, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel8)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tBinData, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(binBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bBinUpd)
                    .addComponent(bBinRead))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        storeAuthPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Store Authentication Keys to Device"));

        jLabel1.setText("key Store No.");

        jLabel2.setText("Key Value Input");

        bLoadKey.setText("Load Key");

        javax.swing.GroupLayout keyValPanelLayout = new javax.swing.GroupLayout(keyValPanel);
        keyValPanel.setLayout(keyValPanelLayout);
        keyValPanelLayout.setHorizontalGroup(
            keyValPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(keyValPanelLayout.createSequentialGroup()
                .addComponent(tKey1, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey2, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey3, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey4, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tKey5, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(8, 8, 8)
                .addComponent(tKey6, javax.swing.GroupLayout.PREFERRED_SIZE, 30, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        keyValPanelLayout.setVerticalGroup(
            keyValPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(keyValPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                .addComponent(tKey1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey5, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addComponent(tKey6, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
        );

        javax.swing.GroupLayout storeAuthPanelLayout = new javax.swing.GroupLayout(storeAuthPanel);
        storeAuthPanel.setLayout(storeAuthPanelLayout);
        storeAuthPanelLayout.setHorizontalGroup(
            storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, storeAuthPanelLayout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1)
                    .addComponent(jLabel2))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(bLoadKey, javax.swing.GroupLayout.PREFERRED_SIZE, 115, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(tMemAdd, javax.swing.GroupLayout.PREFERRED_SIZE, 29, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(keyValPanel, javax.swing.GroupLayout.PREFERRED_SIZE, 211, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        storeAuthPanelLayout.setVerticalGroup(
            storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(storeAuthPanelLayout.createSequentialGroup()
                .addGroup(storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel1)
                    .addComponent(tMemAdd, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(storeAuthPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(jLabel2)
                    .addComponent(keyValPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bLoadKey)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        valBlkPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Value Block Function"));

        jLabel9.setText("Value Amount");

        jLabel10.setText("Block No.");

        jLabel11.setText("Source Block");

        jLabel12.setText("Target Block");

        bValStore.setText("Store Value");

        bValInc.setText("Increment");

        bValDec.setText("Decrement");

        bValRead.setText("Read Value");

        bValRes.setText("Restore Value");

        javax.swing.GroupLayout valBlkPanelLayout = new javax.swing.GroupLayout(valBlkPanel);
        valBlkPanel.setLayout(valBlkPanelLayout);
        valBlkPanelLayout.setHorizontalGroup(
            valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(valBlkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(valBlkPanelLayout.createSequentialGroup()
                        .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel9)
                            .addComponent(jLabel10))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(tValAmt, javax.swing.GroupLayout.PREFERRED_SIZE, 104, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                                .addComponent(tValSrc, javax.swing.GroupLayout.Alignment.LEADING, 0, 0, Short.MAX_VALUE)
                                .addComponent(tValBlk, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE))))
                    .addComponent(jLabel11)
                    .addGroup(valBlkPanelLayout.createSequentialGroup()
                        .addComponent(jLabel12)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(tValTar, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                    .addComponent(bValRes, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValRead, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValDec, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValInc, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(bValStore, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.PREFERRED_SIZE, 106, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(30, Short.MAX_VALUE))
        );
        valBlkPanelLayout.setVerticalGroup(
            valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(valBlkPanelLayout.createSequentialGroup()
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel9)
                    .addComponent(tValAmt, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValStore))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel10)
                    .addComponent(tValBlk, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValInc))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel11)
                    .addComponent(tValSrc, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(bValDec))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(valBlkPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel12)
                    .addComponent(bValRead)
                    .addComponent(tValTar, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bValRes)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrlPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");

        bReset.setText("Reset");

        bQuit.setText("Quit");

        javax.swing.GroupLayout msgPanelLayout = new javax.swing.GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                        .addGap(13, 13, 13)
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 97, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, javax.swing.GroupLayout.DEFAULT_SIZE, 100, Short.MAX_VALUE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 106, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(scrlPaneMsg, javax.swing.GroupLayout.DEFAULT_SIZE, 318, Short.MAX_VALUE)))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrlPaneMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 276, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 12, Short.MAX_VALUE)
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(storeAuthPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(readerPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(28, 28, 28))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(binBlkPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(authPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)))
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(msgPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(valBlkPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(readerPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(storeAuthPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(authPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(binBlkPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addGap(8, 8, 8)
                        .addComponent(valBlkPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(msgPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addContainerGap(42, Short.MAX_VALUE))
        );

        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bClear.setMnemonic(KeyEvent.VK_L);
        bLoadKey.setMnemonic(KeyEvent.VK_K);
        bauth.setMnemonic(KeyEvent.VK_K);
        bBinRead.setMnemonic(KeyEvent.VK_E);
        bBinUpd.setMnemonic(KeyEvent.VK_U);
        bValStore.setMnemonic(KeyEvent.VK_S);
        bValInc.setMnemonic(KeyEvent.VK_N);
        bValDec.setMnemonic(KeyEvent.VK_D);
        bValRead.setMnemonic(KeyEvent.VK_A);
        bValRes.setMnemonic(KeyEvent.VK_T);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        
        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bReset.addActionListener(this);
        bClear.addActionListener(this);
        bLoadKey.addActionListener(this);
        bauth.addActionListener(this);
        bBinRead.addActionListener(this);
        bBinUpd.addActionListener(this);
        bValStore.addActionListener(this);
        bValInc.addActionListener(this);
        bValDec.addActionListener(this);
        bValRead.addActionListener(this);
        bValRes.addActionListener(this);
        bQuit.addActionListener(this);
        
        tMemAdd.addKeyListener(this);
        tKey1.addKeyListener(this);
        tKey2.addKeyListener(this);
        tKey3.addKeyListener(this);
        tKey4.addKeyListener(this);
        tKey5.addKeyListener(this);
        tKey6.addKeyListener(this);
        tBlkNo.addKeyListener(this);
        tKeyAdd.addKeyListener(this);
        tBinBlk.addKeyListener(this);
        tBinLen.addKeyListener(this);
        tValAmt.addKeyListener(this);
        tValBlk.addKeyListener(this);
        tValSrc.addKeyListener(this);
        tValTar.addKeyListener(this);
        
        
    }

	public void actionPerformed(ActionEvent e) 
	{
		
		if(bInit == e.getSource())
		{
			
			//1. Establish context and obtain hContext handle
			retCode = jacs.jSCardEstablishContext(ACSModule.SCARD_SCOPE_USER, 0, 0, hContext);
		    
			if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		    
				mMsg.append("Calling SCardEstablishContext...FAILED\n");
		      	displayOut(1, retCode, "");
		      	
		    }
			
			//2. List PC/SC card readers installed in the system
			retCode = jacs.jSCardListReaders(hContext, 0, szReaders, cchReaders);
      		
			int offset = 0;
			cbReader.removeAllItems();
			
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
			  	if (szReaders[i] == 0x00)
			  	{			  		
			  		
			  		cbReader.addItem(new String(szReaders, offset, i - offset));
			  		offset = i+1;
			  		
			  	}
			}
			
			if (cbReader.getItemCount() == 0)
				cbReader.addItem("No PC/SC reader detected");
			    
			cbReader.setSelectedIndex(0);
			bConn.setEnabled(true);
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			
			//Look for ACR122 and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]; i++)
			{
				
				cbReader.setSelectedIndex(i);
				
				if (((String) cbReader.getSelectedItem()).lastIndexOf("ACR122")> -1)
					break;
				else
					cbReader.setSelectedIndex(0);
				
			}
			
		}
		
		if(bConn == e.getSource())
		{
			
			if(connActive)
			{
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				
			}
			
			String rdrcon = (String)cbReader.getSelectedItem();  	      	      	
		    
		    retCode = jacs.jSCardConnect(hContext, 
		    							rdrcon, 
		    							ACSModule.SCARD_SHARE_SHARED,
		    							ACSModule.SCARD_PROTOCOL_T1 | ACSModule.SCARD_PROTOCOL_T0,
		      							hCard, 
		      							PrefProtocols);
		    
		    if (retCode != ACSModule.SCARD_S_SUCCESS)
		    {
		      	displayOut(1, retCode, "");
	    		connActive = true;
	    		return;
		    
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
			
		    connActive=true;
			bLoadKey.setEnabled(true);
			bauth.setEnabled(true);
			bBinRead.setEnabled(true);
			bBinUpd.setEnabled(true);
			bValStore.setEnabled(true);
			bValInc.setEnabled(true);
			bValDec.setEnabled(true);
			bValRead.setEnabled(true);
			bValRes.setEnabled(true);
			tMemAdd.setEnabled(true);
			tBlkNo.setEnabled(true);
			tKey1.setEnabled(true);
			tKey2.setEnabled(true);
			tKey3.setEnabled(true);
			tKey4.setEnabled(true);
			tKey5.setEnabled(true);
			tKey6.setEnabled(true);
			tKeyAdd.setEnabled(true);
			rbKeyA.setEnabled(true);
			rbKeyB.setEnabled(true);
			tBinBlk.setEnabled(true);
			tBinLen.setEnabled(true);
			tBinData.setEnabled(true);
			tValSrc.setEnabled(true);
			tValAmt.setEnabled(true);
			tValBlk.setEnabled(true);
			tValTar.setEnabled(true);
			rbKeyA.setSelected(true);
			
		}
		
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		if(bReset==e.getSource())
		{
			
			//disconnect
			if (connActive){
				
				retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
				connActive= false;
			
			}
		    
			//release context
			retCode = jacs.jSCardReleaseContext(hContext);
			//System.exit(0);
			
			mMsg.setText("");
			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader    ");
			
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if(bLoadKey==e.getSource())
		{
			String tmpStr="", tmpHex="";
			//validate input
			if(tMemAdd.getText().equals(""))
			{
				
				tMemAdd.requestFocus();
				return;
				
			}
			else
				if((((Integer)Integer.parseInt(tMemAdd.getText(), 16)).byteValue()!= 1)||(((Integer)Integer.parseInt(tMemAdd.getText(), 16)).byteValue()!= 0))
				{
					tMemAdd.setText("1");
					
				}
			
			if(tKey1.getText().equals(""))
			{

				tKey1.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey1.getText(), 16)).byteValue()!=0xFF)
				{
					tKey1.setText("FF");
					
				}
			
			if(tKey2.getText().equals(""))
			{
	
				tKey2.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey2.getText(), 16)).byteValue()!=0xFF)
				{
					tKey2.setText("FF");
					
				}
				
			if(tKey3.getText().equals(""))
			{
	
				tKey3.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey3.getText(), 16)).byteValue()!=0xFF)
				{
					tKey3.setText("FF");
					
				}

			if(tKey4.getText().equals(""))
			{
	
				tKey4.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey4.getText(), 16)).byteValue()!=0xFF)
				{
					tKey4.setText("FF");
					
				}

			if(tKey5.getText().equals(""))
			{

				tKey5.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey5.getText(), 16)).byteValue()!=0xFF)
				{
					tKey5.setText("FF");
					
				}
			
			if(tKey6.getText().equals(""))
			{

				tKey6.requestFocus();
				return;
				
			}
			else
				if(((Integer)Integer.parseInt(tKey6.getText(), 16)).byteValue()!=0xFF)
				{
					tKey6.setText("FF");
					
				}
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x82;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tMemAdd.getText(), 16)).byteValue();
			SendBuff[4] = (byte)0x06;
			SendBuff[5] = (byte)((Integer)Integer.parseInt(tKey1.getText(), 16)).byteValue();
			SendBuff[6] = (byte)((Integer)Integer.parseInt(tKey2.getText(), 16)).byteValue();
			SendBuff[7] = (byte)((Integer)Integer.parseInt(tKey3.getText(), 16)).byteValue();
			SendBuff[8] = (byte)((Integer)Integer.parseInt(tKey4.getText(), 16)).byteValue();
			SendBuff[9] = (byte)((Integer)Integer.parseInt(tKey5.getText(), 16)).byteValue();
			SendBuff[10] = (byte)((Integer)Integer.parseInt(tKey6.getText(), 16)).byteValue();
			
			SendLen = 11;
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(!tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Load authentication keys error!");
				
			}
			
		}
		
		if(bauth == e.getSource())
		{
			String tmpStr="", tmpHex="";
			
			if(tBlkNo.getText().equals(""))
			{
				tBlkNo.requestFocus();
				return;
			}
			else
				if((Integer.parseInt(tBlkNo.getText()))>319)
				{
					tBlkNo.setText("319");
					tBlkNo.requestFocus();
					return;
					
				}
				
			if(tKeyAdd.getText().equals(""))
			{
				tKeyAdd.requestFocus();
				return;
			}
			else
				if(((Integer)Integer.parseInt(tKeyAdd.getText(), 16)).byteValue()>0x1)
				{
					tKeyAdd.setText("1");
					tKeyAdd.requestFocus();
					return;
						
				}			
			
			clearBuffers();
			//authentication command
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x86;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x05;
			SendBuff[5] = (byte)0x01;
			SendBuff[6] = (byte)0x00;
			SendBuff[7] = (byte)(Integer.parseInt(tBlkNo.getText()));
			
			if(rbKeyA.isSelected())
				SendBuff[8] = (byte)0x60;
			else
				SendBuff[8] = (byte)0x61;
			
			SendBuff[9] = (byte)((Integer)Integer.parseInt(tKeyAdd.getText(), 16)).byteValue();
			
			SendLen = 10;
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Authentication Success");
				else
					displayOut(1, 0, "Authentication Failed");
				
			}
			
			
		}
		
		if(bBinRead == e.getSource())
		{
			String tmpStr="", tmpHex="";
			if(tBinBlk.getText().equals(""))
			{
				
				tBinBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tBinBlk.getText())>319)
				{
					
					tBinBlk.setText("319");
					tBinBlk.requestFocus();
					return;
					
				}
			
			if(tBinLen.getText().equals(""))
			{
				
				tBinLen.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tBinLen.getText())<16)
				{
					
					tBinLen.setText("16");
					tBinLen.requestFocus();
					return;
					
				}
			
			clearBuffers();
			//read binary block command
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB0;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tBinBlk.getText(), 16)).byteValue();
			SendBuff[4] = (byte)(Integer.parseInt(tBinLen.getText()));
			
			SendLen = 5;
			RecvLen[0] = Integer.parseInt(tBinLen.getText()) + 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(tmpStr.trim().equals("90 00"))
				{
					tmpStr = "";
					for(int i =0; i<RecvLen[0]-1; i++)
						if(RecvBuff[i] <= 0)
							tmpStr = tmpStr + " ";
						else
							tmpStr = tmpStr + (char)RecvBuff[i];
						
					tBinData.setText(tmpStr);
					
				}
				else
					displayOut(2, 0, "Read Block Error!");
				
			}
			
		}
		
		if(bBinUpd == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			if(tBinBlk.getText().equals(""))
			{
				
				tBinBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tBinBlk.getText())>319)
				{
					
					tBinBlk.setText("319");
					tBinBlk.requestFocus();
					return;
					
				}
			
			if(tBinLen.getText().equals(""))
			{
				
				tBinLen.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tBinLen.getText())>16)
				{
					
					tBinLen.setText("16");
					tBinLen.requestFocus();
					return;
					
				}
			
			if((Integer.parseInt(tBinLen.getText()) > 0) && (tBinData.getText().equals("")))
			{
				
				tBinData.requestFocus();
				return;
				
			}
			
			clearBuffers();
			//read binary block command
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD6;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)((Integer)Integer.parseInt(tBinBlk.getText(), 16)).byteValue();
			SendBuff[4] = (byte)(Integer.parseInt(tBinLen.getText()));
			
			for(int i=0; i<tBinData.getText().length(); i++)
			{
				SendBuff[i + 5] = (byte) (int)(tBinData.getText().charAt(i));
			}
			
			SendLen = (Integer.parseInt(tBinLen.getText())) + 5;
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(tmpStr.trim().equals("90 00"))
				{
					
					tBinData.setText("");
					
				}
				else
					displayOut(2, 0, "Update Block Error!");
				
			}
			
		}
		
		if(bValStore == e.getSource())
		{
			
			int amt;
			String tmpStr="", tmpHex="";
			
			if(tValBlk.getText().equals(""))
			{
				
				tValBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValBlk.getText())>319)
				{
					
					tValBlk.setText("319");
					tValBlk.requestFocus();
					return;
					
				}
			
			if(tValAmt.getText().equals(""))
			{
				
				tValAmt.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValAmt.getText())>2147483647)
				{
					
					tValAmt.setText("2147483647");
					tValAmt.requestFocus();
					return;
					
				}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amt = Integer.parseInt(tValAmt.getText());
			clearBuffers();
			
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD7;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)(Integer.parseInt(tValBlk.getText()));
			SendBuff[4] = (byte)0x05;
			SendBuff[5] = (byte)0x00;
			SendBuff[6] = (byte) ((amt >> 24) & 0xFF);
			SendBuff[7] = (byte) ((amt >> 16) & 0xFF);
			SendBuff[8] = (byte) ((amt >> 8) & 0xFF);
			SendBuff[9]=(byte)(amt & 0xFF);
			
			SendLen = 10; 
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(!tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Store Value Error!");
				
			}
			
		}
		
		if(bValInc == e.getSource())
		{
			
			int amt;
			String tmpStr="", tmpHex="";
			
			if(tValBlk.getText().equals(""))
			{
				
				tValBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValBlk.getText())>319)
				{
					
					tValBlk.setText("319");
					tValBlk.requestFocus();
					return;
					
				}
			
			if(tValAmt.getText().equals(""))
			{
				
				tValAmt.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValAmt.getText())>2147483647)
				{
					
					tValAmt.setText("2147483647");
					tValAmt.requestFocus();
					return;
					
				}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amt = Integer.parseInt(tValAmt.getText());
			clearBuffers();
			
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD7;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)(Integer.parseInt(tValBlk.getText()));
			SendBuff[4] = (byte)0x05;
			SendBuff[5] = (byte)0x01;
			SendBuff[6] = (byte) ((amt >> 24) & 0xFF);
			SendBuff[7] = (byte) ((amt >> 16) & 0xFF);
			SendBuff[8] = (byte) ((amt >> 8) & 0xFF);
			SendBuff[9]=(byte)(amt & 0xFF);
			
			SendLen = 10; 
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(!tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Increment Error!");
				
			}
			
		}
		
		if(bValDec == e.getSource())
		{
			
			int amt;
			String tmpStr="", tmpHex="";
			
			if(tValBlk.getText().equals(""))
			{
				
				tValBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValBlk.getText())>319)
				{
					
					tValBlk.setText("319");
					tValBlk.requestFocus();
					return;
					
				}
			
			if(tValAmt.getText().equals(""))
			{
				
				tValAmt.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValAmt.getText())>2147483647)
				{
					
					tValAmt.setText("2147483647");
					tValAmt.requestFocus();
					return;
					
				}
			
			tValSrc.setText("");
			tValTar.setText("");
			
			amt = Integer.parseInt(tValAmt.getText());
			clearBuffers();
			
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xD7;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)(Integer.parseInt(tValBlk.getText()));
			SendBuff[4] = (byte)0x05;
			SendBuff[5] = (byte)0x02;
			SendBuff[6] = (byte) ((amt >> 24) & 0xFF);
			SendBuff[7] = (byte) ((amt >> 16) & 0xFF);
			SendBuff[8] = (byte) ((amt >> 8) & 0xFF);
			SendBuff[9]=(byte)(amt & 0xFF);
			
			SendLen = 10; 
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(!tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Decrement Error!");
				
			}
			
		}
		
		if(bValRead == e.getSource())
		{
			
			int amt;
			String tmpStr="", tmpHex="";
			
			if(tValBlk.getText().equals(""))
			{
				
				tValBlk.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValBlk.getText())>319)
				{
					
					tValBlk.setText("319");
					tValBlk.requestFocus();
					return;
					
				}
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xB1;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)(Integer.parseInt(tValBlk.getText()));
			SendBuff[4] = (byte)0x04;
			
			SendLen = 5;
			RecvLen[0] = 6;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(tmpStr.trim().equals("90 00"))
				{
					amt = RecvBuff[3] & 0xFF;
					amt = amt + ((RecvBuff[2]& 0xFF) * 256);
					amt = amt + ((RecvBuff[1]& 0xFF) * 256 * 256);
					amt = amt + ((RecvBuff[0]& 0xFF) * 256 * 256 * 256);
					tValAmt.setText(""+amt);
					
				}
				else
					displayOut(2, 0, "Read Value Error!");
				
			}
			
		}
		
		if(bValRes == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			
			if(tValSrc.getText().equals(""))
			{
				
				tValSrc.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValSrc.getText())>319)
				{
					
					tValSrc.setText("319");
					tValSrc.requestFocus();
					return;
					
				}
			
			if(tValTar.getText().equals(""))
			{
				
				tValTar.requestFocus();
				return;
				
			}
			else
				if(Integer.parseInt(tValTar.getText())>319)
				{
					
					tValTar.setText("319");
					tValTar.requestFocus();
					return;
					
				}
			
			tValAmt.setText("");
			tValBlk.setText("");
			
			clearBuffers();
			SendBuff[0] = (byte) 0xFF;
			SendBuff[1] = (byte) 0xD7;
			SendBuff[2] = (byte) 0x00;
			SendBuff[3] = (byte) (Integer.parseInt(tValSrc.getText()));
			SendBuff[4] = (byte) 0x02;
			SendBuff[5] = (byte) 0x03;
			SendBuff[6] = (byte) (Integer.parseInt(tValTar.getText()));
			
			SendLen = 7;
			RecvLen[0] = 2;
			
			retCode = sendAPDUandDisplay();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			else
			{
				
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				//check for response
				if(!tmpStr.trim().equals("90 00"))
					displayOut(2, 0, "Restore Value Error!");
				
			}
			
		}
				
	}
    
	public int sendAPDUandDisplay()
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
		
		String tmpStr = "", tmpHex="";
		
		for(int i =0; i<SendLen; i++)
		{
			
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
			//JOptionPane.showMessageDialog(this, SendBuff[4]);
			//For single character hex
			if (tmpHex.length() == 1) 
				tmpHex = "0" + tmpHex;
			
			tmpStr += " " + tmpHex;  
			
		}
		
		displayOut(2, 0, tmpStr);
		
		retCode = jacs.jSCardTransmit(hCard, 
									 IO_REQ, 
									 SendBuff, 
									 SendLen, 
									 null, 
									 RecvBuff, 
									 RecvLen);
				
		if (retCode != ACSModule.SCARD_S_SUCCESS)
		{
			
			displayOut(1, retCode, "");
			return retCode;
			
		}
	
			tmpStr="";
			
			
				for(int i =0; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}

		displayOut(4, 0, tmpStr);
		return retCode;
	}
	
	public void clearBuffers()
	{
		
		for(int i=0; i<262; i++)
		{
			
			SendBuff[i]=(byte) 0x00;
			RecvBuff[i]= (byte) 0x00;
			
		}
		
	}
	
	public void displayOut(int mType, int msgCode, String printText)
	{

		switch(mType)
		{
		
			case 1: 
				{
					
					mMsg.append("! " + printText);
					mMsg.append(ACSModule.GetScardErrMsg(msgCode) + "\n");
					break;
					
				}
			case 2: mMsg.append("< " + printText + "\n");break;
			case 3: mMsg.append("> " + printText + "\n");break;
			default: mMsg.append("- " + printText + "\n");
		
		}
		
	}
	

	
	public void initMenu()
	{
		
		
		connActive = false;
		bConn.setEnabled(false);
		bInit.setEnabled(true);
		bReset.setEnabled(false);
		bLoadKey.setEnabled(false);
		bauth.setEnabled(false);
		bBinRead.setEnabled(false);
		bBinUpd.setEnabled(false);
		bValStore.setEnabled(false);
		bValInc.setEnabled(false);
		bValDec.setEnabled(false);
		bValRead.setEnabled(false);
		bValRes.setEnabled(false);
		tMemAdd.setEnabled(false);
		tBlkNo.setEnabled(false);
		tKey1.setEnabled(false);
		tKey2.setEnabled(false);
		tKey3.setEnabled(false);
		tKey4.setEnabled(false);
		tKey5.setEnabled(false);
		tKey6.setEnabled(false);
		tKeyAdd.setEnabled(false);
		rbKeyA.setEnabled(false);
		rbKeyB.setEnabled(false);
		tBinBlk.setEnabled(false);
		tBinLen.setEnabled(false);
		tBinData.setEnabled(false);
		tValSrc.setEnabled(false);
		tValAmt.setEnabled(false);
		tValBlk.setEnabled(false);
		tValTar.setEnabled(false);
		tKey1.setText("");
		tKey2.setText("");
		tKey3.setText("");
		tKey4.setText("");
		tKey5.setText("");
		tKey6.setText("");
		tMemAdd.setText("");
		tBlkNo.setText("");
		tKeyAdd.setText("");
		tBinBlk.setText("");
		tBinLen.setText("");
		tBinData.setText("");
		tValAmt.setText("");
		tValBlk.setText("");
		tValSrc.setText("");
		tValTar.setText("");
		mMsg.setText("");
		displayOut(0, 0, "Program Ready");
		
	}
	
	public void keyReleased(KeyEvent ke) {
		
	}
	
	public void keyPressed(KeyEvent ke) {
  		//restrict paste actions
		if (ke.getKeyCode() == KeyEvent.VK_V ) 
			ke.setKeyCode(KeyEvent.VK_UNDO );						
  	}
	
	public void keyTyped(KeyEvent ke) 
	{  		
  		Character x = (Character)ke.getKeyChar();
  		char empty = '\r';
  		

  		//Check valid characters
  		if(tBlkNo.isFocusOwner() || tBinBlk.isFocusOwner() || tBinLen.isFocusOwner() || tValAmt.isFocusOwner() || tValSrc.isFocusOwner() || tValBlk.isFocusOwner() || tValTar.isFocusOwner())
  		{	
  		
  			if (VALIDCHARS.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  			
  		}
  		else
  		{
  			
  			if (VALIDCHARSHEX.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);
  			
  		}
  					  
		//Limit character length
  		if(tBlkNo.isFocusOwner() || tBinBlk.isFocusOwner() || tValBlk.isFocusOwner() || tValSrc.isFocusOwner() || tValTar.isFocusOwner())
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 3 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  		else if(tValAmt.isFocusOwner())
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 10 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  		else
  		{
  			
  			if   (((JTextField)ke.getSource()).getText().length() >= 2 ) 
  			{
		
  				ke.setKeyChar(empty);	
  				return;
  				
  			}
  			
  		}
  	    	
	}
    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new mainMifareProg().setVisible(true);
            }
        });
    }



}
