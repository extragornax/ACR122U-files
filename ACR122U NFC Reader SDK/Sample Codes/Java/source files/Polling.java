/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              Polling.java

  Description:       This sample program outlines the steps on how to
                     execute card detection polling functions using ACR1222

  Author:            M.J.E.C. Castillo

  Date:              June 24, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.filechooser.*;
import javax.swing.filechooser.FileFilter;

public class Polling extends JFrame implements ActionListener{

	//JPCSC Variables
	int retCode, pollCase;
	boolean connActive, detect, dualPoll; 
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
	byte [] ATRVal = new byte[262];
	byte [] szReaders = new byte[1024];
	
	//GUI Variables
    private JButton bClear, bStartPoll;
    private JButton bConn;
    private JButton bGetPoll;
    private JButton bInit;
    private JButton bReset, bQuit;
    private JButton bSetPoll;
    private ButtonGroup bgPollInt;
    private JCheckBox cbOpt1;
    private JCheckBox cbOpt2;
    private JCheckBox cbOpt3;
    private JCheckBox cbOpt4;
    private JCheckBox cbOpt5;
    private JCheckBox cbOpt6;
    private JCheckBox cbOpt7;
    private JComboBox cbReader;
    private JLabel lblReader;
    private JTextArea mMsg;
    private JPanel msgPanel;
    private JPanel pollIntPanel;
    private JPanel pollOptPanel;
    private JRadioButton rbOpt1;
    private JRadioButton rbopt2;
    private JPanel readerPanel;
    private JScrollPane scrPaneMsg;
    private JPanel statusPanel;
    private JTextField tStat1;
    private JTextField tStat2;
    private JTextField tStat3;
    private JTextField tStat4;
    
    // End of variables declaration
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public Polling() {
    	
    	this.setTitle("Polling");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(650,550);
	       bgPollInt = new ButtonGroup();
	       bStartPoll = new JButton();
	        readerPanel = new JPanel();
	        lblReader = new JLabel();
	        bInit = new JButton();
	        bConn = new JButton();
	        pollOptPanel = new JPanel();
	        cbOpt1 = new JCheckBox();
	        cbOpt2 = new JCheckBox();
	        cbOpt3 = new JCheckBox();
	        cbOpt4 = new JCheckBox();
	        cbOpt5 = new JCheckBox();
	        cbOpt6 = new JCheckBox();
	        cbOpt7 = new JCheckBox();
	        pollIntPanel = new JPanel();
	        rbOpt1 = new JRadioButton();
	        rbopt2 = new JRadioButton();
	        bGetPoll = new JButton();
	        bSetPoll = new JButton();
	        statusPanel = new JPanel();
	        tStat1 = new JTextField();
	        tStat2 = new JTextField();
	        tStat3 = new JTextField();
	        tStat4 = new JTextField();
	        msgPanel = new JPanel();
	        scrPaneMsg = new JScrollPane();
	        mMsg = new JTextArea();
	        bReset = new JButton();
	        bClear = new JButton();
	        bQuit = new JButton();
     
     lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
     bInit.setText("Initalize");

     bConn.setText("Connect");

     javax.swing.GroupLayout readerPanelLayout = new javax.swing.GroupLayout(readerPanel);
     readerPanel.setLayout(readerPanelLayout);
     readerPanelLayout.setHorizontalGroup(
         readerPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(readerPanelLayout.createSequentialGroup()
             .addGroup(readerPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addGroup(readerPanelLayout.createSequentialGroup()
                     .addContainerGap()
                     .addComponent(lblReader, javax.swing.GroupLayout.PREFERRED_SIZE, 67, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                     .addComponent(cbReader, 0, 153, Short.MAX_VALUE))
                 .addGroup(readerPanelLayout.createSequentialGroup()
                     .addGap(124, 124, 124)
                     .addGroup(readerPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                         .addComponent(bConn, javax.swing.GroupLayout.DEFAULT_SIZE, 110, Short.MAX_VALUE)
                         .addComponent(bInit, javax.swing.GroupLayout.DEFAULT_SIZE, 110, Short.MAX_VALUE))))
             .addContainerGap())
     );
     readerPanelLayout.setVerticalGroup(
         readerPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(readerPanelLayout.createSequentialGroup()
             .addContainerGap()
             .addGroup(readerPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                 .addComponent(lblReader)
                 .addComponent(cbReader, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
             .addComponent(bInit)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(bConn)
             .addContainerGap(12, Short.MAX_VALUE))
     );

     pollOptPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Operating Parameters"));

     cbOpt1.setText("Automatic PICC Polling");

     cbOpt2.setText("Automatic ATS Generation");

     cbOpt3.setText("Detect ISO14443 Type A Cards");

     cbOpt4.setText("Detect ISO14443 Type B Cards");

     cbOpt5.setText("Detect Topaz Cards");

     cbOpt6.setText("Detect FeliCa 212K Cards");

     cbOpt7.setText("Detect FeliCa 424K Cards");

     pollIntPanel.setBorder(javax.swing.BorderFactory.createTitledBorder("Poll Interval"));
     bgPollInt.add(rbOpt1);
     rbOpt1.setText("250 ms");
     bgPollInt.add(rbopt2);
     rbopt2.setText("500 ms");

     javax.swing.GroupLayout pollIntPanelLayout = new javax.swing.GroupLayout(pollIntPanel);
     pollIntPanel.setLayout(pollIntPanelLayout);
     pollIntPanelLayout.setHorizontalGroup(
         pollIntPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(pollIntPanelLayout.createSequentialGroup()
             .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
             .addGroup(pollIntPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addComponent(rbopt2)
                 .addComponent(rbOpt1)))
     );
     pollIntPanelLayout.setVerticalGroup(
         pollIntPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(pollIntPanelLayout.createSequentialGroup()
             .addComponent(rbOpt1)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(rbopt2)
             .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
     );

     bGetPoll.setText("Get Operating Parameter");

     bSetPoll.setText("Set Operating Parameter");

     javax.swing.GroupLayout pollOptPanelLayout = new javax.swing.GroupLayout(pollOptPanel);
     pollOptPanel.setLayout(pollOptPanelLayout);
     pollOptPanelLayout.setHorizontalGroup(
         pollOptPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(pollOptPanelLayout.createSequentialGroup()
             .addContainerGap()
             .addGroup(pollOptPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                 .addGroup(pollOptPanelLayout.createSequentialGroup()
                     .addComponent(pollIntPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                     .addGroup(pollOptPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                         .addComponent(bSetPoll, javax.swing.GroupLayout.DEFAULT_SIZE, 125, Short.MAX_VALUE)
                         .addComponent(bGetPoll, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                 .addComponent(cbOpt3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                 .addComponent(cbOpt5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                 .addComponent(cbOpt4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                 .addComponent(cbOpt6, javax.swing.GroupLayout.DEFAULT_SIZE, 188, Short.MAX_VALUE)
                 .addComponent(cbOpt2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                 .addComponent(cbOpt1, javax.swing.GroupLayout.DEFAULT_SIZE, 175, Short.MAX_VALUE)
                 .addComponent(cbOpt7, javax.swing.GroupLayout.PREFERRED_SIZE, 178, javax.swing.GroupLayout.PREFERRED_SIZE))
             .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
     );
     pollOptPanelLayout.setVerticalGroup(
         pollOptPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(pollOptPanelLayout.createSequentialGroup()
             .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
             .addComponent(cbOpt1)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt2)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt3)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt4)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt5)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt6)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(cbOpt7)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addGroup(pollOptPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addGroup(pollOptPanelLayout.createSequentialGroup()
                     .addComponent(bGetPoll)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                     .addComponent(bSetPoll))
                 .addComponent(pollIntPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
     );

     tStat1.setBackground(new java.awt.Color(204, 204, 204));

     tStat2.setBackground(new java.awt.Color(204, 204, 204));

     tStat3.setBackground(new java.awt.Color(204, 204, 204));

     tStat4.setBackground(new java.awt.Color(204, 204, 204));

     javax.swing.GroupLayout statusPanelLayout = new javax.swing.GroupLayout(statusPanel);
     statusPanel.setLayout(statusPanelLayout);
     statusPanelLayout.setHorizontalGroup(
         statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(statusPanelLayout.createSequentialGroup()
             .addComponent(tStat1, javax.swing.GroupLayout.PREFERRED_SIZE, 88, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(tStat2, javax.swing.GroupLayout.DEFAULT_SIZE, 136, Short.MAX_VALUE)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(tStat3, javax.swing.GroupLayout.PREFERRED_SIZE, 82, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(tStat4, javax.swing.GroupLayout.PREFERRED_SIZE, 157, javax.swing.GroupLayout.PREFERRED_SIZE))
     );
     statusPanelLayout.setVerticalGroup(
         statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(statusPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
             .addComponent(tStat3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addComponent(tStat4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addComponent(tStat1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addComponent(tStat2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
     );

     mMsg.setColumns(20);
     mMsg.setRows(5);
     scrPaneMsg.setViewportView(mMsg);

     bReset.setText("Reset");

     bClear.setText("Clear");

     bQuit.setText("Quit");

     javax.swing.GroupLayout msgPanelLayout = new javax.swing.GroupLayout(msgPanel);
     msgPanel.setLayout(msgPanelLayout);
     msgPanelLayout.setHorizontalGroup(
         msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(msgPanelLayout.createSequentialGroup()
             .addContainerGap()
             .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addComponent(scrPaneMsg, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, 301, Short.MAX_VALUE)
                 .addGroup(msgPanelLayout.createSequentialGroup()
                     .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 91, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                     .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 97, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                     .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 97, javax.swing.GroupLayout.PREFERRED_SIZE)))
             .addContainerGap())
     );
     msgPanelLayout.setVerticalGroup(
         msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
             .addContainerGap()
             .addComponent(scrPaneMsg, javax.swing.GroupLayout.DEFAULT_SIZE, 383, Short.MAX_VALUE)
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                 .addComponent(bClear)
                 .addComponent(bReset)
                 .addComponent(bQuit)))
     );

     bStartPoll.setText("Start Polling");

     javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
     getContentPane().setLayout(layout);
     layout.setHorizontalGroup(
         layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(layout.createSequentialGroup()
             .addContainerGap()
             .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addGroup(layout.createSequentialGroup()
                     .addComponent(statusPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                     .addContainerGap())
                 .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                     .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                         .addComponent(bStartPoll, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 244, Short.MAX_VALUE)
                         .addComponent(readerPanel, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                         .addComponent(pollOptPanel, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                     .addComponent(msgPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))))
     );
     layout.setVerticalGroup(
         layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
         .addGroup(layout.createSequentialGroup()
             .addContainerGap()
             .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                 .addGroup(layout.createSequentialGroup()
                     .addComponent(readerPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 8, Short.MAX_VALUE)
                     .addComponent(pollOptPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                     .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 9, Short.MAX_VALUE)
                     .addComponent(bStartPoll))
                 .addComponent(msgPanel, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
             .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
             .addComponent(statusPanel, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
             .addContainerGap())
     );
		
     bInit.addActionListener(this); 
     bConn.addActionListener(this);
     bClear.addActionListener(this);
     bReset.addActionListener(this);
     bStartPoll.addActionListener(this);
     bGetPoll.addActionListener(this);
     bSetPoll.addActionListener(this);
     bQuit.addActionListener(this);
		
     bInit.setMnemonic(KeyEvent.VK_I);
     bConn.setMnemonic(KeyEvent.VK_C);
     bClear.setMnemonic(KeyEvent.VK_L);
     bReset.setMnemonic(KeyEvent.VK_R);
     bGetPoll.setMnemonic(KeyEvent.VK_G);
     bSetPoll.setMnemonic(KeyEvent.VK_S);
     bQuit.setMnemonic(KeyEvent.VK_Q);
        
    }

    public void actionPerformed(ActionEvent e) {
    	
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
		
			
			//Look for ACR122 and make it the default reader in the combobox
			for (int i = 0; i < cchReaders[0]-1; i++)
			{
				
				cbReader.setSelectedIndex(i);
				
				if (((String) cbReader.getSelectedItem()).lastIndexOf("ACR122")> -1)
					break;
				
			}
			
			bConn.setEnabled(true);
			bInit.setEnabled(false);
			bReset.setEnabled(true);
			
		}
		
		//Connect button
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
	    		connActive = false;
	    		return;
		    
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
			
		    connActive=true;
			cbOpt1.setEnabled(true);
			cbOpt2.setEnabled(true);
			cbOpt3.setEnabled(true);
			cbOpt4.setEnabled(true);
			cbOpt5.setEnabled(true);
			cbOpt6.setEnabled(true);
			cbOpt7.setEnabled(true);
			rbOpt1.setEnabled(true);
			rbopt2.setEnabled(true);
			bGetPoll.setEnabled(true);
			bSetPoll.setEnabled(true);
			bStartPoll.setEnabled(true);
			rbOpt1.setSelected(true);

		}
		
		//Clear button
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		//Reset Button
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
			timer.stop();
			bStartPoll.setText("Start Polling");
			mMsg.setText("");
			tStat2.setText("");
			tStat4.setText("");
			initMenu();
			cbReader.removeAllItems();
			cbReader.addItem("Please select reader                   ");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
		}
		
		if(bStartPoll== e.getSource())
		{
			
			if(detect)
			{
				
				displayOut(0, 0, "Polling Stopped...");
				bStartPoll.setText("Start Polling");
				tStat2.setText("");
				tStat4.setText("");
				detect = false;
				timer.stop();
				return;
				
			}
			
			displayOut(0, 0, "Polling Started...");
			bStartPoll.setText("End Polling");
			detect = true;
			timer = new Timer(250, pollTimer);
			timer.start();
			
		}
		
		if(bGetPoll == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			//get PICC operating Parameters
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x00;
			SendBuff[2] = (byte)0x50;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x00;
			
			SendLen = 5;
			RecvLen[0] = 2;
			
			retCode = transmit();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//prints the command sent
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			displayOut(3, 0, tmpStr.trim());
			
			//interpret the return response
			if((RecvBuff[0]& 0x80)!= 0)
			{
				
				displayOut(3, 0, "Automatic Polling is enabled.");
				cbOpt1.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Automatic Polling is disabled.");
				cbOpt1.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x40)!= 0)
			{
				
				displayOut(3, 0, "Automatic ATS Generation is enabled.");
				cbOpt2.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Automatic ATS Generation is disabled.");
				cbOpt2.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x20)!= 0)
			{
				
				displayOut(3, 0, "250 ms.");
				rbOpt1.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "500 ms");
				rbopt2.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x10)!= 0)
			{
				
				displayOut(3, 0, "Detect Felica 424K Card Enabled");
				cbOpt7.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Detect Felica 424K Card Disabled");
				cbOpt7.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x08)!= 0)
			{
				
				displayOut(3, 0, "Detect Felica 212K Card Enabled");
				cbOpt6.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Detect Felica 212K Card Disabled");
				cbOpt6.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x04)!= 0)
			{
				
				displayOut(3, 0, "Detect Topaz Card Enabled");
				cbOpt5.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Detect Topaz Card Disabled");
				cbOpt5.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x02)!= 0)
			{
				
				displayOut(3, 0, "Detect ISO 14443 type B Card Enabled");
				cbOpt4.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Detect ISO 14443 Type B Card Disabled");
				cbOpt4.setSelected(false);
				
			}
			
			if((RecvBuff[0]& 0x01)!= 0)
			{
				
				displayOut(3, 0, "Detect ISO 14443 Type A Card Enabled");
				cbOpt3.setSelected(true);
				
			}
			else
			{
				
				displayOut(3, 0, "Detect ISO 14443 Type A Card Disabled");
				cbOpt3.setSelected(false);
				
			}
			
		}

		if(bSetPoll == e.getSource())
		{
			
			String tmpStr="", tmpHex="";
			//set operating parameter
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x00;
			SendBuff[2] = (byte)0x51;
			SendBuff[3] = (byte)0x00;
			
			if(cbOpt3.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x01);
				displayOut(3, 0, "Detect ISO 14443 Type A Card Enabled");
			}
			else
				displayOut(3, 0, "Detect ISO 14443 Type A Card Disabled");
				
			if(cbOpt4.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x02);
				displayOut(3, 0, "Detect ISO 14443 Type B Card Enabled");
			}
			else
				displayOut(3, 0, "Detect ISO 14443 Type B Card Disabled");
			
			if(cbOpt5.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x04);
				displayOut(3, 0, "Detect Topaz Card Enabled");
			}
			else
				displayOut(3, 0, "Detect Topaz Card Disabled");
			
			if(cbOpt6.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x08);
				displayOut(3, 0, "Detect FeliCa 212K Card Enabled");
			}
			else
				displayOut(3, 0, "Detect FeliCa 212K Card Disabled");
			
			if(cbOpt7.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x10);
				displayOut(3, 0, "Detect FeliCa 424K Card Enabled");
			}
			else
				displayOut(3, 0, "Detect FeliCa 424K Card Disabled");
			
			if(rbOpt1.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x20);
				displayOut(3, 0, "Poll Interval is 250 ms.");
			}
			else
				displayOut(3, 0, "Poll Interval is 500 ms.");
			
			if(cbOpt2.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x40);
				displayOut(3, 0, "Automatic ATS Generation is Enabled");
			}
			else
				displayOut(3, 0, "Automatic ATS Generation Disabled");
			
			if(cbOpt1.isSelected())
			{
				SendBuff[3] = (byte)(SendBuff[3] | 0x80);
				displayOut(3, 0, "Automatic Polling is Enabled");
			}
			else
				displayOut(3, 0, "Automatic Polling Disabled");
			
			SendBuff[4] = (byte) 0x00;
			
			SendLen = 5;
			RecvLen[0] = 1;
			
			//prints the command sent
			for(int i =0; i<SendLen; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			displayOut(2, 0, tmpStr);
			
			retCode = transmit();
			if (retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
			//prints the response recieved
			tmpStr = "";
			for(int i =0; i<RecvLen[0]; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			displayOut(3, 0, tmpStr.trim());
			
		}
    }
    
	//timer for automatic polling
	ActionListener pollTimer = new ActionListener() {
	      public void actionPerformed(ActionEvent evt) {
		
			//always use valid connections
	    	retCode = callCardConnect(1);
	    	
	    	if(retCode != ACSModule.SCARD_S_SUCCESS)
	    	{
	    		
	    		displayOut(6, 0, "No Card within Range.");
	    		tStat2.setText("");
	    		return;
	    		
	    	}
	    	
	    	if(checkCard())
	    		displayOut(6, 0, "Card is detected");
	    	else
	    	{
	    		displayOut(6, 0, "No Card within Range");
	    		tStat2.setText("");
	    	}
		}
	};
	
	public boolean checkCard()
	{
		
		int tmpWord;
		//getATR and check card type
		tmpWord = 32;
		ATRLen[0] = tmpWord;
		int[] state = new int[1];
		int[] readerLen = new int[1];
		
		String rdrcon= (String)cbReader.getSelectedItem();  
	    
	    byte [] tmpReader	= rdrcon.getBytes();
	    byte [] readerName	= new byte[rdrcon.length()+1];
	      
	      for (int i=0; i<rdrcon.length(); i++)
	      	readerName[i] = tmpReader[i];
	      readerName[rdrcon.length()] = 0; //set null terminator
    
	    retCode = jacs.jSCardStatus(hCard, 
	    							tmpReader, 
	    							readerLen, 
	    							state, 
	    							PrefProtocols, 
	    							ATRVal, 
	    							ATRLen);
	    
	    if(retCode!= ACSModule.SCARD_S_SUCCESS)
		    return false;
	   //JOptionPane.showMessageDialog(this, ATRVal[14]);
	   interpretATR();
	   return true;
	    
	   
	}
	
	public void interpretATR()
	{
		
		String RIDVal, cardName, tmpHex="";
		cardName= "";
		
		// Interpret ATR and guess card
	    // Mifare cards using ISO 14443 Part 3 Supplemental Document
		if (ATRLen[0]>14){
			
			if (ATRVal[12] == 3)
			{
				switch(ATRVal[14])
				{
				
				case 0x11: cardName = cardName + " Felica 212K"; break;
				case 0x12: cardName = cardName + " Felica 424K"; break;
				case 0x04: cardName = cardName + " Topaz"; break;
				
				}
			}
			
			if (ATRVal[12] == 3)
			{		
				if(ATRVal[13] == 0)
				{
					
						switch(ATRVal[14])
						{
						
							case 0x01: cardName = cardName + " Mifare Standard 1K"; break;
	                        case 0x02: cardName = cardName + " Mifare Standard 4K"; break;
	                        case 0x03: cardName = cardName + " Mifare Ultra light"; break;
	                        case 0x04: cardName = cardName + " SLE55R_XXXX"; break;
	                        case 0x06: cardName = cardName + " SR176"; break;
	                        case 0x07: cardName = cardName + " SRI X4K"; break;
	                        case 0x08: cardName = cardName + " AT88RF020"; break;
	                        case 0x09: cardName = cardName + " AT88SC0204CRF"; break;
	                        case 0x0A: cardName = cardName + " AT88SC0808CRF"; break;
	                        case 0x0B: cardName = cardName + " AT88SC1616CRF"; break;
                            case 0x0C: cardName = cardName + " AT88SC3216CRF"; break;
	                        case 0x0D: cardName = cardName + " AT88SC6416CRF"; break;
                            case 0x0E: cardName = cardName + " SRF55V10P"; break;
                            case 0x0F: cardName = cardName + " SRF55V02P"; break;
                            case 0x10: cardName = cardName + " SRF55V10S"; break;
                            case 0x11: cardName = cardName + " SRF55V02S"; break;
                            case 0x12: cardName = cardName + " TAG IT"; break;
                            case 0x13: cardName = cardName + " LRI512"; break;
                            case 0x14: cardName = cardName + " ICODESLI"; break;
                            case 0x15: cardName = cardName + " TEMPSENS"; break;
                            case 0x16: cardName = cardName + " I.CODE1"; break;
                            case 0x17: cardName = cardName + " PicoPass 2K"; break;
                            case 0x18: cardName = cardName + " PicoPass 2KS"; break;
                            case 0x19: cardName = cardName + " PicoPass 16K"; break;
                            case 0x1A: cardName = cardName + " PicoPass 16KS"; break;
	                        case 0x1B: cardName = cardName + " PicoPass 16K(8x2)";break;
	                        case 0x1C: cardName = cardName + " PicoPass 16KS(8x2)";break;
	                        case 0x1D: cardName = cardName + " PicoPass 32KS(16+16)";break;
	                        case 0x1E: cardName = cardName + " PicoPass 32KS(16+8x2)";break;
	                        case 0x1F: cardName = cardName + " PicoPass 32KS(8x2+16)";break;
	                        case 0x20: cardName = cardName + " PicoPass 32KS(8x2+8x2)";break;
	                        case 0x21: cardName = cardName + " LRI64";break;
	                        case 0x22: cardName = cardName + " I.CODE UID";break;
	                        case 0x23: cardName = cardName + " I.CODE EPC";break;
	                        case 0x24: cardName = cardName + " LRI12";break;
	                        case 0x25: cardName = cardName + " LRI128";break;
	                        case 0x26: cardName = cardName + " Mifare Mini";break;
						
						}
						
					}
					else
					{
						
						if (ATRVal[13]==0xFF)
						{
							
							switch(ATRVal[14])
							{
							
								case 9: cardName = cardName + " Mifare Mini"; break;
							
							}
							
						}
						
					}
					
				}
			
			displayOut(5, 0, cardName);
		}
		
		if(ATRLen[0] ==11)
		{
			
			RIDVal = "";
			
			for(int i=4; i<10; i++)
			{
				tmpHex = Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				RIDVal +=  tmpHex;  
				
				
			}
			
			if (RIDVal.equals("067577810280"))
			{
				
				displayOut(5, 0, "Mifare DESFire");
				
			}
			
		}
		
		if(ATRLen[0] ==17)
		{
			
			RIDVal = "";
			
			for(int i=4; i<16; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)ATRVal[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				RIDVal +=  tmpHex;  
				
			}
			
			if (RIDVal.equals("50122345561253544E3381C3"))
			{
				
				displayOut(5, 0, "ST19XRC8E");
				
			}
			
		}
		
	}

	public int callCardConnect(int reqType)
	{
		
		if (connActive)
			retCode = jacs.jSCardDisconnect(hCard, ACSModule.SCARD_UNPOWER_CARD);
		
		//shared connection
		String rdrcon = (String)cbReader.getSelectedItem();
		retCode = jacs.jSCardConnect(hContext, 
									 rdrcon, 
									 ACSModule.SCARD_SHARE_SHARED, 
									 ACSModule.SCARD_PROTOCOL_T0 | ACSModule.SCARD_PROTOCOL_T1, 
									 hCard, 
									 PrefProtocols);
		
		if((retCode == ACSModule.SCARD_S_SUCCESS)&& (reqType != 1))
		{

			displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
			return retCode;
			
		}
		return retCode;
	}
	
	public int transmit()
	{
		
		ACSModule.SCARD_IO_REQUEST IO_REQ = new ACSModule.SCARD_IO_REQUEST(); 
		ACSModule.SCARD_IO_REQUEST IO_REQ_Recv = new ACSModule.SCARD_IO_REQUEST(); 
		IO_REQ.dwProtocol = PrefProtocols[0];
		IO_REQ.cbPciLength = 8;
		IO_REQ_Recv.dwProtocol = PrefProtocols[0];
		IO_REQ_Recv.cbPciLength = 8;
		RecvLen[0] = 262;
		
		String tmpStr, tmpHex="";
		tmpStr = "";
		
		for(int i=0; i<SendLen; i++)
		{
			tmpHex = Integer.toHexString(((Byte)SendBuff[i]).intValue() & 0xFF).toUpperCase();
			
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
			
		}
		else
		{
			
			tmpStr = "";
			
			for(int i =0; i<RecvLen[0]; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += " " + tmpHex;  
				
			}
			
			displayOut(3, 0, tmpStr.trim());
		}
		
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
			case 5: tStat2.setText(printText);break;
			case 6: tStat4.setText(printText);break;
			default: mMsg.append("- " + printText + "\n");
		
		}
		
	}
	

	
	public void initMenu()
	{
	
		connActive = false;
		displayOut(0, 0, "Program Ready");
		bConn.setEnabled(false);
		bReset.setEnabled(false);
		bInit.setEnabled(true);
		tStat1.setText("Card Type");
		tStat3.setText("Card Status");
		cbOpt1.setEnabled(false);
		cbOpt2.setEnabled(false);
		cbOpt3.setEnabled(false);
		cbOpt4.setEnabled(false);
		cbOpt5.setEnabled(false);
		cbOpt6.setEnabled(false);
		cbOpt7.setEnabled(false);
		rbOpt1.setEnabled(false);
		rbopt2.setEnabled(false);
		bGetPoll.setEnabled(false);
		bSetPoll.setEnabled(false);
		bStartPoll.setEnabled(false);
		
	}
	

	

    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Polling().setVisible(true);
            }
        });
    }



}
