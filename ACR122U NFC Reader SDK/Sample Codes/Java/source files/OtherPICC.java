/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              otherPICC.java

  Description:       This sample program outlines the steps on how to
                     transact with other PICC cards using ACR122

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

public class OtherPICC extends JFrame implements ActionListener, KeyListener{

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
    private JButton bClear, bConn, bGetData, bInit, bReset, bSend, bQuit;
    private JCheckBox cbIso14443A;
    private JComboBox cbReader;
    private JPanel getDataPanel, cardComPanel, msgPanel, readerPanel;
    private JLabel lblLc, lblLe, lblP1, lblP2, lblReader, lblCLA, lblData, lblINS;
    private JTextArea mData, mMsg;
    private JScrollPane scrPaneData, scrPaneMsg;
    private JTextField tCLA, tLc,tLe, tP1, tP2, tINS;
	
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public OtherPICC() {
    	
    	this.setTitle("Other PICC Cards");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(650,470);
	    cardComPanel = new JPanel();
        tCLA = new JTextField();
        lblCLA = new JLabel();
        tINS = new JTextField();
        lblINS = new JLabel();
        tP1 = new JTextField();
        lblP1 = new JLabel();
        tP2 = new JTextField();
        lblP2 = new JLabel();
        tLc = new JTextField();
        lblLc = new JLabel();
        tLe = new JTextField();
        lblLe = new JLabel();
        scrPaneData = new JScrollPane();
        mData = new JTextArea();
        lblData = new JLabel();
        bSend = new JButton();
        readerPanel = new JPanel();
        lblReader = new JLabel();
        cbReader = new JComboBox();
        bInit = new JButton();
        bConn = new JButton();
        getDataPanel = new JPanel();
        cbIso14443A = new JCheckBox();
        bGetData = new JButton();
        msgPanel = new JPanel();
        scrPaneMsg = new JScrollPane();
        mMsg = new JTextArea();
        bClear = new JButton();
        bReset = new JButton();
        bQuit = new JButton();
        
        cardComPanel.setBorder(BorderFactory.createTitledBorder("Send Card Command"));

        lblCLA.setHorizontalAlignment(SwingConstants.CENTER);
        lblCLA.setText("CLA");

        lblINS.setHorizontalAlignment(SwingConstants.CENTER);
        lblINS.setText("INS");

        lblP1.setHorizontalAlignment(SwingConstants.CENTER);
        lblP1.setText("P1");

        lblP2.setHorizontalAlignment(SwingConstants.CENTER);
        lblP2.setText("P2");

        lblLc.setHorizontalAlignment(SwingConstants.CENTER);
        lblLc.setText("Lc");

        lblLe.setHorizontalAlignment(SwingConstants.CENTER);
        lblLe.setText("Le");

        mData.setColumns(20);
        mData.setRows(5);
        scrPaneData.setViewportView(mData);

        lblData.setText("Data In");

        bSend.setText("Send Card Command");

        GroupLayout cardComPanelLayout = new GroupLayout(cardComPanel);
        cardComPanel.setLayout(cardComPanelLayout);
        cardComPanelLayout.setHorizontalGroup(
            cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(cardComPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(scrPaneData, GroupLayout.DEFAULT_SIZE, 236, Short.MAX_VALUE)
                    .addGroup(cardComPanelLayout.createSequentialGroup()
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                            .addComponent(lblCLA, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tCLA, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(lblINS, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tINS, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                            .addComponent(lblP1, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tP1, GroupLayout.DEFAULT_SIZE, 33, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(lblP2, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tP2, GroupLayout.DEFAULT_SIZE, 34, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(lblLc, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tLc, GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                            .addComponent(lblLe, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(tLe, GroupLayout.DEFAULT_SIZE, 36, Short.MAX_VALUE)))
                    .addComponent(lblData)
                    .addComponent(bSend, GroupLayout.Alignment.TRAILING, GroupLayout.PREFERRED_SIZE, 160, GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );
        cardComPanelLayout.setVerticalGroup(
            cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(cardComPanelLayout.createSequentialGroup()
                .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblCLA)
                    .addComponent(lblINS)
                    .addComponent(lblP1)
                    .addComponent(lblP2)
                    .addComponent(lblLc)
                    .addComponent(lblLe))
                .addGap(7, 7, 7)
                .addGroup(cardComPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tCLA, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tINS, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tP1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tP2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tLc, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(tLe, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addGap(4, 4, 4)
                .addComponent(lblData)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(scrPaneData, GroupLayout.PREFERRED_SIZE, 72, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(bSend)
                .addContainerGap())
        );
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bInit.setText("Initialize");

        bConn.setText("Connect");

        GroupLayout readerPanelLayout = new GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanelLayout);
        readerPanelLayout.setHorizontalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 175, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.TRAILING, readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                        .addComponent(bConn, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bInit, GroupLayout.Alignment.TRAILING, GroupLayout.DEFAULT_SIZE, 109, Short.MAX_VALUE)))
                .addContainerGap())
        );
        readerPanelLayout.setVerticalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(bConn)
                .addContainerGap())
        );

        getDataPanel.setBorder(BorderFactory.createTitledBorder("Get Data Function"));

        cbIso14443A.setText("ISO 14443 A Card");

        bGetData.setText("Get Data");

        GroupLayout getDataPanelLayout = new GroupLayout(getDataPanel);
        getDataPanel.setLayout(getDataPanelLayout);
        getDataPanelLayout.setHorizontalGroup(
            getDataPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(getDataPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(cbIso14443A)
                .addGap(18, 18, 18)
                .addComponent(bGetData, GroupLayout.PREFERRED_SIZE, 107, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        getDataPanelLayout.setVerticalGroup(
            getDataPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(getDataPanelLayout.createSequentialGroup()
                .addGroup(getDataPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(cbIso14443A)
                    .addComponent(bGetData, GroupLayout.PREFERRED_SIZE, 23, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(22, Short.MAX_VALUE))
        );

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");

        bReset.setText("Reset");

        bQuit.setText("Quit");

        javax.swing.GroupLayout msgPanelLayout = new javax.swing.GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(scrPaneMsg, javax.swing.GroupLayout.DEFAULT_SIZE, 268, Short.MAX_VALUE)
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addComponent(bClear, javax.swing.GroupLayout.PREFERRED_SIZE, 88, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, javax.swing.GroupLayout.PREFERRED_SIZE, 84, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bQuit, javax.swing.GroupLayout.PREFERRED_SIZE, 84, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrPaneMsg, javax.swing.GroupLayout.PREFERRED_SIZE, 343, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(msgPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGap(0, 567, Short.MAX_VALUE)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(cardComPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(readerPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(getDataPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(msgPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGap(12, 12, 12))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGap(0, 416, Short.MAX_VALUE)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(getDataPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cardComPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
		
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bReset.setMnemonic(KeyEvent.VK_R);
        bClear.setMnemonic(KeyEvent.VK_L);
        bGetData.setMnemonic(KeyEvent.VK_G);
        bSend.setMnemonic(KeyEvent.VK_S);
        bQuit.setMnemonic(KeyEvent.VK_Q);

        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bReset.addActionListener(this);
        bClear.addActionListener(this);
        bGetData.addActionListener(this);
        bSend.addActionListener(this);
        bQuit.addActionListener(this);
        
        tCLA.addKeyListener(this);
        tINS.addKeyListener(this);
        tP1.addKeyListener(this);
        tP2.addKeyListener(this);
        tLc.addKeyListener(this);
        tLe.addKeyListener(this);
        mData.addKeyListener(this);
        
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
	    		connActive = false;
	    		return;
		    
		    } 
		    else 
		    {	      	      
		      	
		    	displayOut(0, 0, "Successful connection to " + (String)cbReader.getSelectedItem());
		      	
		    }
			
		    connActive=true;
			bGetData.setEnabled(true);
			bSend.setEnabled(true);
			cbIso14443A.setEnabled(true);
			tCLA.setEnabled(true);
			tINS.setEnabled(true);
			tP1.setEnabled(true);
			tP2.setEnabled(true);
			tLc.setEnabled(true);
			tLe.setEnabled(true);
			
		}
		
		if(bClear == e.getSource())
		{
			
			mMsg.setText("");
			
		}
		
		if(bQuit == e.getSource())
		{
			
			this.dispose();
			
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
		
		if(bGetData == e.getSource())
		{
			
			validATS = false;
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0xCA;
			
			if(cbIso14443A.isSelected())
				SendBuff[2] = (byte)0x01;
			else
				SendBuff[2] = (byte)0x00;
			
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x00;
			
			SendLen = 5;
			RecvLen[0] = 0xFF;
			
			retCode = sendAPDUandDisplay(2);
			
			if(retCode!= ACSModule.SCARD_S_SUCCESS)
				return;
			
			String tmpStr="", tmpHex="";
			//interpret and display return values
				
				if(cbIso14443A.isSelected())
					tmpStr = "UID: ";
				else
					tmpStr = "";
				
				for(int i =0; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				displayOut(2, 0, tmpStr);

		}
		
		if(bSend == e.getSource())
		{
			
			boolean directCom;
			String tmpData;
			
			directCom = true;
			//validate input
			if(tCLA.getText().equals(""))
			{
				tCLA.setText("00");
				tCLA.requestFocus();
				return;
			}
			
			clearBuffers();
			SendBuff[0] = (byte)((Integer)Integer.parseInt(tCLA.getText(), 16)).byteValue();
			
			if(!tINS.getText().equals(""))
				SendBuff[1] = (byte)((Integer)Integer.parseInt(tINS.getText(), 16)).byteValue();
			
			if(!tP1.getText().equals(""))
				directCom = false;
			
			if(!directCom)
			{
			
				SendBuff[2] = (byte)((Integer)Integer.parseInt(tP1.getText(), 16)).byteValue();
				
				if (tP2.getText().equals(""))
				{
					
					tP2.setText("00");
					tP2.requestFocus();
					return;
					
				}
				else
					SendBuff[3] = (byte)((Integer)Integer.parseInt(tP2.getText(), 16)).byteValue();
				
				if(!tLc.getText().equals(""))
				{
					
					SendBuff[4] = (byte)((Integer)Integer.parseInt(tLc.getText(), 16)).byteValue();
					
					//process data in if  Lc > 0
					if(SendBuff[4] > 0 )
					{
						
						tmpData = trimInput(0, mData.getText().trim());
						tmpData = trimInput(1, tmpData);
						
						//check if data is consistent with Lc value
						if(SendBuff[4] > (tmpData.length() / 2))
						{
							
							mData.selectAll();
							mData.requestFocus();
							return;
							
						}
						
						for(int i =0; i<SendBuff[4]; i++)
						{
					
							SendBuff[i+5]=(byte)((Integer)Integer.parseInt(tmpData.substring(((i*2)+1),((i*2)+3)), 16)).byteValue();
						
						}
					
						if (!tLe.getText().equals(""))
							SendBuff[SendBuff[4]+5]= (byte)((Integer)Integer.parseInt(tLe.getText(), 16)).byteValue();
					
						
					}
					else
					{
						
						if(!tLe.getText().equals(""))
							SendBuff[5] = (byte)((Integer)Integer.parseInt(tLe.getText(), 16)).byteValue();	
						
					}
				}
				else
				{
					
					if(!tLe.getText().equals(""))
						SendBuff[4] = (byte)((Integer)Integer.parseInt(tLe.getText(), 16)).byteValue();
				
				}
			}
			
			if(directCom){
				
				if (tINS.getText().equals(""))
					SendLen =  0x01;
				else
					SendLen = 0x02;
		
			}
			else
			{
			
				if (tLc.getText().equals(""))
				{
				
					if(!tLe.getText().equals(""))
						SendLen = 5;
					else
						SendLen = 4;
		
				}
				else
				{
				
					if(tLe.getText().equals(""))
						SendLen = SendBuff[4]+5;
					else
						SendLen = SendBuff[4]+6;
				
				}
			
			}
		
			RecvLen[0] = 0xFF;
			
			retCode = sendAPDUandDisplay(1);
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
				
		}
    }
    
	public String trimInput(int trimType, String strIn)
	{
		
		String tmpStr="";
		
		strIn = strIn.trim();
		
		switch(trimType)
		{
		
		case 0:
		{
			for (int i=0; i<strIn.length(); i++)
				if((strIn.charAt(i)!=(char)13)&&(strIn.charAt(i)!=(char)10))
					tmpStr = tmpStr + strIn.charAt(i);
			break;
		}
		
		case 1:
			for(int i=0; i<strIn.length(); i++)
				if (strIn.charAt(i)!=(char)32)
					tmpStr = tmpStr + strIn.charAt(i);
		
		}
		
		return tmpStr;
		
	}
	
	public int sendAPDUandDisplay(int sendType)
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
			
			switch(sendType)
			{
			
			case 1: 
			{
				for(int i =0; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex;  
					
				}
				
				break;
			}
			
			case 2:
			{	
				for(int i =RecvLen[0]-2; i<RecvLen[0]; i++)
				{
					
					tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
					
					//For single character hex
					if (tmpHex.length() == 1) 
						tmpHex = "0" + tmpHex;
					
					tmpStr += " " + tmpHex; 
					
				}
				
				if(tmpStr.trim().equals("6A 81"))
				{
				
					displayOut(0, 0, "This function is not supported");
					return retCode;
				}
				
				validATS = true;
				break;
			}
				
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
		bGetData.setEnabled(false);
		bSend.setEnabled(false);
		cbIso14443A.setEnabled(false);
		tCLA.setEnabled(false);
		tINS.setEnabled(false);
		tP1.setEnabled(false);
		tP2.setEnabled(false);
		tLc.setEnabled(false);
		tLe.setEnabled(false);
		tCLA.setText("");
		tINS.setText("");
		tP1.setText("");
		tP2.setText("");
		tLc.setText("");
		tLe.setText("");
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

  			//Check valid characters
  			if (VALIDCHARSHEX.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);

  			
  		//Limit character length
	  	if(tCLA.isFocusOwner() || tINS.isFocusOwner() || tP1.isFocusOwner() || tP2.isFocusOwner() || tLc.isFocusOwner() || tLe.isFocusOwner())
	  	{
  			if(((JTextField)ke.getSource()).getText().length() >= 2 ) 
	  		{
	  			
		  		ke.setKeyChar(empty);	
		  		return;
	  			
	  		}
	  	}
  	    	
	}
	
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new OtherPICC().setVisible(true);
            }
        });
    }



}


