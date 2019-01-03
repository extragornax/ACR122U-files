/*
  Copyright(C):      Advanced Card Systems Ltd

  File:              mainApplet.java

  Description:       This program enables the user to browse the sample codes for ACR122

  Author:            M.J.E.C. Castillo

  Date:              August 4, 2008

  Revision Trail:   (Date/Author/Description)

======================================================================*/

import java.awt.*;
import java.lang.*;
import java.awt.event.*;
import javax.swing.text.*;
import javax.swing.*;
import java.applet.Applet;
import javax.swing.JApplet;

public class mainApplet extends JApplet implements ActionListener
{
	
	//Variables
	boolean openMifare=false, openGetATR=false, openDevProg=false, openPolling=false, openOtherPICC=false;
	boolean openActive=false, openPassive=false;
	
	//GUI Variables
    private JButton bDevProg, bGetATR, bMifare, bOtherPICC, bPolling, bP2P;
	
	static mainMifareProg mifare;
	static GetATR getATR;
	static DevProg devProg;
	static Polling polling;
	static OtherPICC otherPICC;
	static ActiveSample active;
	static PassiveSample passive;
	
	public void init() 
   	{
		setSize(220,300);
	    bMifare = new JButton();
	    bGetATR = new JButton();
	    bDevProg = new JButton();
	    bPolling = new JButton();
	    bOtherPICC = new JButton();
	    bP2P = new JButton();

        bMifare.setText("Mifare Card Programming");
        bGetATR.setText("Get ATR");
        bDevProg.setText("Device Programming");
        bPolling.setText("Polling");
        bOtherPICC.setText("Other PICC Cards");
        bP2P.setText("Peer to Peer Programming");

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(bGetATR, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bOtherPICC, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bDevProg, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bPolling, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bMifare, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE)
                    .addComponent(bP2P, GroupLayout.PREFERRED_SIZE, 190, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(28, 28, 28)
                .addComponent(bDevProg)
                .addGap(18, 18, 18)
                .addComponent(bGetATR)
                .addGap(18, 18, 18)
                .addComponent(bPolling)
                .addGap(18, 18, 18)
                .addComponent(bMifare)
                .addGap(18, 18, 18)
                .addComponent(bOtherPICC)
                .addGap(18, 18, 18)
                .addComponent(bP2P)
                .addContainerGap(30, Short.MAX_VALUE))
        );
        
        bMifare.addActionListener(this);
        bGetATR.addActionListener(this);
        bDevProg.addActionListener(this);
        bPolling.addActionListener(this);
        bOtherPICC.addActionListener(this);
        bP2P.addActionListener(this);
   		
   	}
	
	public void actionPerformed(ActionEvent e) 
	{
		
		if(bMifare == e.getSource())
		{
			closeFrames();
			
			if(openMifare == false)
			{
				mifare = new mainMifareProg();
				mifare.setVisible(true);
				openMifare = true;
			}
			else
			{
			
				mifare.dispose();
				mifare = new mainMifareProg();
				mifare.setVisible(true);
				openMifare = true;
			}
			
		}
		
		if(bGetATR == e.getSource())
		{
			
			closeFrames();
			
			if(openGetATR == false)
			{
				getATR = new GetATR();
				getATR.setVisible(true);
				openGetATR = true;
			}
			else
			{
				getATR.dispose();
				getATR = new GetATR();
				getATR.setVisible(true);
				openGetATR = true;
			}
			
		}
		
		if(bDevProg == e.getSource())
		{
			closeFrames();
			
			if(openDevProg == false)
			{
				devProg = new DevProg();
				devProg.setVisible(true);
				openDevProg = true;
			}
			else
			{
				devProg.dispose();
				devProg = new DevProg();
				devProg.setVisible(true);
				openDevProg = true;
			}
			
		}
		
		if(bPolling == e.getSource())
		{
			closeFrames();
			
			if(openPolling == false)
			{
				polling = new Polling();
				polling.setVisible(true);
				openPolling = true;
			}
			else
			{
				polling.dispose();
				polling = new Polling();
				polling.setVisible(true);
				openPolling = true;
			}
			
		}
		
		if(bOtherPICC == e.getSource())
		{
			closeFrames();
			
			if(openOtherPICC == false)
			{
				otherPICC = new OtherPICC();
				otherPICC.setVisible(true);	
				openOtherPICC = true;
			}
			else
			{
				otherPICC.dispose();
				otherPICC = new OtherPICC();
				otherPICC.setVisible(true);	
				openOtherPICC = true;
			}
			
		}
		
		if(bP2P == e.getSource())
		{
			closeFrames();
			
			if((openActive == false)&&(openPassive == false))
			{
				active = new ActiveSample();
				passive = new PassiveSample();
				active.setVisible(true);	
				passive.setVisible(true);
				openActive = true;
				openPassive = true;
			}
			else
			{
				active.dispose();
				passive.dispose();
				active = new ActiveSample();
				passive = new PassiveSample();
				active.setVisible(true);	
				passive.setVisible(true);
				openActive = true;
				openPassive = true;
			}
			
		}
		
	}
	
	public void closeFrames()
	{
		
		if(openMifare==true)
		{
			mifare.dispose();
			openMifare = false;
		}
		
		if(openGetATR==true)
		{
			getATR.dispose();
			openGetATR = false;
		}
		
		if(openDevProg==true)
		{
			devProg.dispose();
			openDevProg = false;
		}
		
		if(openPolling==true)
		{
			polling.dispose();
			openPolling = false;
		}
		
		if(openOtherPICC==true)
		{
			otherPICC.dispose();
			openOtherPICC = false;
		}
		
		if(openActive==true)
		{
			active.dispose();
			openActive = false;
		}
		
		if(openPassive==true)
		{
			passive.dispose();
			openPassive = false;
		}
		
	}
	
	public void start()
	{
	
	}
	
}