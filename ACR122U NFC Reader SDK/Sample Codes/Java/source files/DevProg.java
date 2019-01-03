/*
 *   Copyright(C):      Advanced Card Systems Ltd
 *
 * File:              devProg.java
 *
 * Description:       This sample program outlines the steps on how to
 *                    set the LED?Buzzer and antenna of the ACR122 NFC reader
 *
 * Author:            M.J.E.C. Castillo
 *
 * Date:              June 24, 2008
 *
 * Revision Trail:   (Date/Author/Description)
 *
 *======================================================================*/

import java.io.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.filechooser.*;
import javax.swing.filechooser.FileFilter;

public class DevProg extends JFrame implements ActionListener, KeyListener{

	//JPCSC Variables
	int retCode;
	boolean connActive; 
	
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
	
	static String VALIDCHARSHEX = "ABCDEFabcdef0123456789";
	
	//GUI Variables
    private JPanel BlinkPanel, T1Panel, T2Panel, antennaPanel;
    private JButton bClear, bConn, bInit, bReset, bSetAnt, bSetLED, bGetFW, bQuit, bGetStat;
    private JComboBox cbReader;
    private JPanel greenBlinkPanel, greenFinalPanel, greenInitPanel;
    private JPanel greenLedPanel, greenStateMaskPanel, linkPanel, msgPanel;
    private JLabel lblInit, lblReader, lblRep, lblToggle, lblms1, lblms2;
    private JTextArea mMsg;
    private JRadioButton rbAntOff, rbAntOn, rbLinkOpt1, rbLinkOpt2, rbLinkOpt3;
    private JRadioButton rbLinkOpt4, rbgreenBlinkOff, rbgreenBlinkOn, rbgreenFinOff;
    private JRadioButton rbgreenFinOn, rbgreenInitOff, rbgreenInitOn, rbgreenStateMaskOff;
    private JRadioButton rbgreenStateMaskOn, rbredBlinkOff, rbredBlinkOn, rbredFinOff;
    private JRadioButton rbredFinOn, rbredInitOff, rbredInitOn, rbredStateMaskOff, rbredStateMaskOn;
    private JPanel readerPanel, redBlinkPanel, redFinalPanel, redInitPanel, redLedPanel, redStateMaskPanel;
    private JScrollPane scrPaneMsg;
    private JTextField tRepeat, tT1, tT2;
    private ButtonGroup bgAnt,bgredFinal, bgredInit, bgredBlink, bgredState, bggreenFinal, bggreenInit;
    private ButtonGroup bggreenBlink, bggreenState, bgLink;
    
	static JacspcscLoader jacs = new JacspcscLoader();
    

    public DevProg() {
    	
    	this.setTitle("Device Programming");
        initComponents();
        initMenu();
    }


    @SuppressWarnings("unchecked")

    private void initComponents() {

		setSize(810,550);
	    readerPanel = new JPanel();
	    lblReader = new JLabel();
	    cbReader = new JComboBox();
	    bInit = new JButton();
	    bConn = new JButton();
	    bGetFW = new JButton();
	    bGetStat = new JButton();
	    antennaPanel = new JPanel();
	    rbAntOn = new JRadioButton();
	    rbAntOff = new JRadioButton();
	    bSetAnt = new JButton();
	    redLedPanel = new JPanel();
	    redFinalPanel = new JPanel();
	    rbredFinOn = new JRadioButton();
	    rbredFinOff = new JRadioButton();
	    redStateMaskPanel = new JPanel();
	    rbredStateMaskOn = new JRadioButton();
	    rbredStateMaskOff = new JRadioButton();
	    redInitPanel = new JPanel();
	    rbredInitOn = new JRadioButton();
	    rbredInitOff = new JRadioButton();
	    redBlinkPanel = new JPanel();
	    rbredBlinkOn = new JRadioButton();
	    rbredBlinkOff = new JRadioButton();
	    greenLedPanel = new JPanel();
	    greenFinalPanel = new JPanel();
	    rbgreenFinOn = new JRadioButton();
	    rbgreenFinOff = new JRadioButton();
	    greenStateMaskPanel = new JPanel();
	    rbgreenStateMaskOn = new JRadioButton();
	    rbgreenStateMaskOff = new JRadioButton();
	    greenInitPanel = new JPanel();
	    rbgreenInitOn = new JRadioButton();
	    rbgreenInitOff = new JRadioButton();
	    greenBlinkPanel = new JPanel();
	    rbgreenBlinkOn = new JRadioButton();
	    rbgreenBlinkOff = new JRadioButton();
	    BlinkPanel = new JPanel();
	    T1Panel = new JPanel();
	    lblInit = new JLabel();
	    tT1 = new JTextField();
	    lblms1 = new JLabel();
	    T2Panel = new JPanel();
	    lblToggle = new JLabel();
	    tT2 = new JTextField();
	    lblms2 = new JLabel();
	    lblRep = new JLabel();
	    tRepeat = new JTextField();
	    linkPanel = new JPanel();
	    rbLinkOpt1 = new JRadioButton();
	    rbLinkOpt2 = new JRadioButton();
	    rbLinkOpt3 = new JRadioButton();
	    rbLinkOpt4 = new JRadioButton();
	    bSetLED = new JButton();
	    msgPanel = new JPanel();
	    scrPaneMsg = new JScrollPane();
	    mMsg = new JTextArea();
	    bClear = new JButton();
	    bReset = new JButton();
	    bQuit = new JButton();
	    bgAnt = new ButtonGroup();
	    bgredFinal= new ButtonGroup();
	    bgredInit= new ButtonGroup();
	    bgredBlink= new ButtonGroup();
	    bgredState= new ButtonGroup();
	    bggreenFinal= new ButtonGroup();
	    bggreenInit= new ButtonGroup();
	    bggreenBlink= new ButtonGroup();
	    bggreenState= new ButtonGroup();
	    bgLink= new ButtonGroup();
        
        lblReader.setText("Select Reader");

		String[] rdrNameDef = {"Please select reader                   "};	
		cbReader = new JComboBox(rdrNameDef);
		cbReader.setSelectedIndex(0);
		
        bInit.setText("Initalize");
        bConn.setText("Connect");
        bGetFW.setText("Get Firmware Version");
        bGetStat.setText("Get Status");

        GroupLayout readerPanelLayout = new GroupLayout(readerPanel);
        readerPanel.setLayout(readerPanelLayout);
        readerPanelLayout.setHorizontalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(bGetFW, GroupLayout.DEFAULT_SIZE, 219, Short.MAX_VALUE)
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addComponent(lblReader)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(cbReader, 0, 148, Short.MAX_VALUE))
                    .addGroup(readerPanelLayout.createSequentialGroup()
                        .addComponent(bInit, GroupLayout.PREFERRED_SIZE, 108, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bConn, GroupLayout.PREFERRED_SIZE, 130, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
            .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                .addGroup(readerPanelLayout.createSequentialGroup()
                    .addContainerGap()
                    .addComponent(bGetStat, GroupLayout.DEFAULT_SIZE, 219, Short.MAX_VALUE)
                    .addContainerGap()))
        );
        readerPanelLayout.setVerticalGroup(
            readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(readerPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(lblReader)
                    .addComponent(cbReader, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bInit)
                    .addComponent(bConn))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(bGetFW)
                .addContainerGap(32, Short.MAX_VALUE))
            .addGroup(readerPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                .addGroup(GroupLayout.Alignment.TRAILING, readerPanelLayout.createSequentialGroup()
                    .addContainerGap(99, Short.MAX_VALUE)
                    .addComponent(bGetStat)
                    .addGap(1, 1, 1)))
        );

        antennaPanel.setBorder(BorderFactory.createTitledBorder("Antenna Setting"));
        bgAnt.add(rbAntOn);
        rbAntOn.setText("On");
        bgAnt.add(rbAntOff);
        rbAntOff.setText("Off");

        bSetAnt.setText("Set Antenna");

        GroupLayout antennaPanelLayout = new GroupLayout(antennaPanel);
        antennaPanel.setLayout(antennaPanelLayout);
        antennaPanelLayout.setHorizontalGroup(
            antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(antennaPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(rbAntOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbAntOff)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, 36, Short.MAX_VALUE)
                .addComponent(bSetAnt)
                .addContainerGap())
        );
        antennaPanelLayout.setVerticalGroup(
            antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(antennaPanelLayout.createSequentialGroup()
                .addGroup(antennaPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(rbAntOn)
                    .addComponent(rbAntOff)
                    .addComponent(bSetAnt))
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        redLedPanel.setBorder(BorderFactory.createTitledBorder("Red LED"));

        redFinalPanel.setBorder(BorderFactory.createTitledBorder("Final LED State"));
        bgredFinal.add(rbredFinOn);
        rbredFinOn.setText("On");
        bgredFinal.add(rbredFinOff);
        rbredFinOff.setText("Off");

        GroupLayout redFinalPanelLayout = new GroupLayout(redFinalPanel);
        redFinalPanel.setLayout(redFinalPanelLayout);
        redFinalPanelLayout.setHorizontalGroup(
            redFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redFinalPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbredFinOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rbredFinOff))
        );
        redFinalPanelLayout.setVerticalGroup(
            redFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbredFinOn)
                .addComponent(rbredFinOff))
        );

        redStateMaskPanel.setBorder(BorderFactory.createTitledBorder("LED State Mask"));
        bgredState.add(rbredStateMaskOn);
        rbredStateMaskOn.setText("On");
        bgredState.add(rbredStateMaskOff);
        rbredStateMaskOff.setText("Off");

        GroupLayout redStateMaskPanelLayout = new GroupLayout(redStateMaskPanel);
        redStateMaskPanel.setLayout(redStateMaskPanelLayout);
        redStateMaskPanelLayout.setHorizontalGroup(
            redStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redStateMaskPanelLayout.createSequentialGroup()
                .addComponent(rbredStateMaskOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbredStateMaskOff)
                .addContainerGap(16, Short.MAX_VALUE))
        );
        redStateMaskPanelLayout.setVerticalGroup(
            redStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbredStateMaskOn)
                .addComponent(rbredStateMaskOff))
        );

        redInitPanel.setBorder(BorderFactory.createTitledBorder("Inital Blinking State"));
        bgredInit.add(rbredInitOn);
        rbredInitOn.setText("On");
        bgredInit.add(rbredInitOff);
        rbredInitOff.setText("Off");

        GroupLayout redInitPanelLayout = new GroupLayout(redInitPanel);
        redInitPanel.setLayout(redInitPanelLayout);
        redInitPanelLayout.setHorizontalGroup(
            redInitPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redInitPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbredInitOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rbredInitOff))
        );
        redInitPanelLayout.setVerticalGroup(
            redInitPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redInitPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbredInitOn)
                .addComponent(rbredInitOff))
        );

        redBlinkPanel.setBorder(BorderFactory.createTitledBorder("LED Blinking Mask"));
        bgredBlink.add(rbredBlinkOn);
        rbredBlinkOn.setText("On");
        bgredBlink.add(rbredBlinkOff);
        rbredBlinkOff.setText("Off");

        GroupLayout redBlinkPanelLayout = new GroupLayout(redBlinkPanel);
        redBlinkPanel.setLayout(redBlinkPanelLayout);
        redBlinkPanelLayout.setHorizontalGroup(
            redBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, redBlinkPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbredBlinkOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbredBlinkOff)
                .addGap(58, 58, 58))
        );
        redBlinkPanelLayout.setVerticalGroup(
            redBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbredBlinkOn)
                .addComponent(rbredBlinkOff))
        );

        GroupLayout redLedPanelLayout = new GroupLayout(redLedPanel);
        redLedPanel.setLayout(redLedPanelLayout);
        redLedPanelLayout.setHorizontalGroup(
            redLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redLedPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(redLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(redInitPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(redFinalPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(redLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(redStateMaskPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(redBlinkPanel, GroupLayout.PREFERRED_SIZE, 108, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(3, Short.MAX_VALUE))
        );
        redLedPanelLayout.setVerticalGroup(
            redLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(redLedPanelLayout.createSequentialGroup()
                .addGroup(redLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(redLedPanelLayout.createSequentialGroup()
                        .addComponent(redFinalPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(redInitPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(redLedPanelLayout.createSequentialGroup()
                        .addComponent(redStateMaskPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(redBlinkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );

        greenLedPanel.setBorder(BorderFactory.createTitledBorder("Green LED"));

        greenFinalPanel.setBorder(BorderFactory.createTitledBorder("Final LED State"));
        bggreenFinal.add(rbgreenFinOn);
        rbgreenFinOn.setText("On");
        bggreenFinal.add(rbgreenFinOff);
        rbgreenFinOff.setText("Off");

        GroupLayout greenFinalPanelLayout = new GroupLayout(greenFinalPanel);
        greenFinalPanel.setLayout(greenFinalPanelLayout);
        greenFinalPanelLayout.setHorizontalGroup(
            greenFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenFinalPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbgreenFinOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rbgreenFinOff))
        );
        greenFinalPanelLayout.setVerticalGroup(
            greenFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenFinalPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbgreenFinOn)
                .addComponent(rbgreenFinOff))
        );

        greenStateMaskPanel.setBorder(BorderFactory.createTitledBorder("LED State Mask"));
        bggreenState.add(rbgreenStateMaskOn);
        rbgreenStateMaskOn.setText("On");
        bggreenState.add(rbgreenStateMaskOff);
        rbgreenStateMaskOff.setText("Off");

        GroupLayout greenStateMaskPanelLayout = new GroupLayout(greenStateMaskPanel);
        greenStateMaskPanel.setLayout(greenStateMaskPanelLayout);
        greenStateMaskPanelLayout.setHorizontalGroup(
            greenStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenStateMaskPanelLayout.createSequentialGroup()
                .addComponent(rbgreenStateMaskOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbgreenStateMaskOff)
                .addContainerGap(16, Short.MAX_VALUE))
        );
        greenStateMaskPanelLayout.setVerticalGroup(
            greenStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenStateMaskPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbgreenStateMaskOn)
                .addComponent(rbgreenStateMaskOff))
        );

        greenInitPanel.setBorder(BorderFactory.createTitledBorder("Inital Blinking State"));
        bggreenInit.add(rbgreenInitOn);
        rbgreenInitOn.setText("On");
        bggreenInit.add(rbgreenInitOff);
        rbgreenInitOff.setText("Off");

        GroupLayout greenInitPanelLayout = new GroupLayout(greenInitPanel);
        greenInitPanel.setLayout(greenInitPanelLayout);
        greenInitPanelLayout.setHorizontalGroup(
            greenInitPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenInitPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbgreenInitOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(rbgreenInitOff))
        );
        greenInitPanelLayout.setVerticalGroup(
            greenInitPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenInitPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbgreenInitOn)
                .addComponent(rbgreenInitOff))
        );

        greenBlinkPanel.setBorder(BorderFactory.createTitledBorder("LED Blinking Mask"));
        bggreenBlink.add(rbgreenBlinkOn);
        rbgreenBlinkOn.setText("On");
        bggreenBlink.add(rbgreenBlinkOff);
        rbgreenBlinkOff.setText("Off");

        GroupLayout greenBlinkPanelLayout = new GroupLayout(greenBlinkPanel);
        greenBlinkPanel.setLayout(greenBlinkPanelLayout);
        greenBlinkPanelLayout.setHorizontalGroup(
            greenBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, greenBlinkPanelLayout.createSequentialGroup()
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(rbgreenBlinkOn)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbgreenBlinkOff)
                .addGap(58, 58, 58))
        );
        greenBlinkPanelLayout.setVerticalGroup(
            greenBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenBlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                .addComponent(rbgreenBlinkOn)
                .addComponent(rbgreenBlinkOff))
        );

        GroupLayout greenLedPanelLayout = new GroupLayout(greenLedPanel);
        greenLedPanel.setLayout(greenLedPanelLayout);
        greenLedPanelLayout.setHorizontalGroup(
            greenLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenLedPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(greenLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(greenInitPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(greenFinalPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(greenLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(greenStateMaskPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(greenBlinkPanel, GroupLayout.PREFERRED_SIZE, 108, GroupLayout.PREFERRED_SIZE))
                .addContainerGap(3, Short.MAX_VALUE))
        );
        greenLedPanelLayout.setVerticalGroup(
            greenLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(greenLedPanelLayout.createSequentialGroup()
                .addGroup(greenLedPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(greenLedPanelLayout.createSequentialGroup()
                        .addComponent(greenFinalPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(greenInitPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(greenLedPanelLayout.createSequentialGroup()
                        .addComponent(greenStateMaskPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(greenBlinkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );

        BlinkPanel.setBorder(BorderFactory.createTitledBorder("Blinking Duration Control"));

        T1Panel.setBorder(BorderFactory.createTitledBorder("T1 Duration"));

        lblInit.setText("Inital Blinking State");

        lblms1.setText("x 100 ms");

        GroupLayout T1PanelLayout = new GroupLayout(T1Panel);
        T1Panel.setLayout(T1PanelLayout);
        T1PanelLayout.setHorizontalGroup(
            T1PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(T1PanelLayout.createSequentialGroup()
                .addGap(19, 19, 19)
                .addGroup(T1PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(GroupLayout.Alignment.TRAILING, T1PanelLayout.createSequentialGroup()
                        .addComponent(tT1, GroupLayout.DEFAULT_SIZE, 30, Short.MAX_VALUE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(lblms1)
                        .addGap(8, 8, 8))
                    .addComponent(lblInit))
                .addContainerGap(21, Short.MAX_VALUE))
        );
        T1PanelLayout.setVerticalGroup(
            T1PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, T1PanelLayout.createSequentialGroup()
                .addComponent(lblInit)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(T1PanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tT1, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblms1))
                .addContainerGap())
        );

        T2Panel.setBorder(BorderFactory.createTitledBorder("T2 Duration"));

        lblToggle.setText("Toggle Blinking State");

        lblms2.setText("x 100 ms");

        GroupLayout T2PanelLayout = new GroupLayout(T2Panel);
        T2Panel.setLayout(T2PanelLayout);
        T2PanelLayout.setHorizontalGroup(
            T2PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(T2PanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(T2PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(T2PanelLayout.createSequentialGroup()
                        .addGap(10, 10, 10)
                        .addComponent(tT2, GroupLayout.PREFERRED_SIZE, 34, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                        .addComponent(lblms2))
                    .addComponent(lblToggle))
                .addContainerGap(22, Short.MAX_VALUE))
        );
        T2PanelLayout.setVerticalGroup(
            T2PanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, T2PanelLayout.createSequentialGroup()
                .addComponent(lblToggle)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(T2PanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(tT2, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                    .addComponent(lblms2))
                .addContainerGap())
        );

        lblRep.setText("Number of Repetitons");

        linkPanel.setBorder(BorderFactory.createTitledBorder("Link to Buzzer"));
        bgLink.add(rbLinkOpt1);
        rbLinkOpt1.setText("Buzzer Off");
        bgLink.add(rbLinkOpt2);
        rbLinkOpt2.setText("T1 Duration");
        bgLink.add(rbLinkOpt3);
        rbLinkOpt3.setText("T2 Duration");
        bgLink.add(rbLinkOpt4);
        rbLinkOpt4.setText("T1 and T2 Duration");

        GroupLayout linkPanelLayout = new GroupLayout(linkPanel);
        linkPanel.setLayout(linkPanelLayout);
        linkPanelLayout.setHorizontalGroup(
            linkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(linkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(linkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(rbLinkOpt1)
                    .addComponent(rbLinkOpt2)
                    .addComponent(rbLinkOpt3))
                .addContainerGap(44, Short.MAX_VALUE))
            .addGroup(GroupLayout.Alignment.TRAILING, linkPanelLayout.createSequentialGroup()
                .addContainerGap(8, Short.MAX_VALUE)
                .addComponent(rbLinkOpt4)
                .addContainerGap())
        );
        linkPanelLayout.setVerticalGroup(
            linkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(linkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(rbLinkOpt1)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbLinkOpt2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbLinkOpt3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(rbLinkOpt4)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        GroupLayout BlinkPanelLayout = new GroupLayout(BlinkPanel);
        BlinkPanel.setLayout(BlinkPanelLayout);
        BlinkPanelLayout.setHorizontalGroup(
            BlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, BlinkPanelLayout.createSequentialGroup()
                .addContainerGap(54, Short.MAX_VALUE)
                .addComponent(tRepeat, GroupLayout.PREFERRED_SIZE, 59, GroupLayout.PREFERRED_SIZE)
                .addGap(50, 50, 50))
            .addGroup(BlinkPanelLayout.createSequentialGroup()
                .addGroup(BlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.TRAILING)
                    .addGroup(GroupLayout.Alignment.LEADING, BlinkPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(T1Panel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(GroupLayout.Alignment.LEADING, BlinkPanelLayout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(linkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE))
                    .addGroup(GroupLayout.Alignment.LEADING, BlinkPanelLayout.createSequentialGroup()
                        .addGap(33, 33, 33)
                        .addComponent(lblRep)))
                .addContainerGap())
            .addGroup(BlinkPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(T2Panel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addContainerGap())
        );
        BlinkPanelLayout.setVerticalGroup(
            BlinkPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(BlinkPanelLayout.createSequentialGroup()
                .addComponent(T1Panel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(T2Panel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(lblRep)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(tRepeat, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addGap(16, 16, 16)
                .addComponent(linkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        bSetLED.setText("Set LED/Buzzer Control");
        bSetLED.setActionCommand("Set LED/Buzzer Control");

        scrPaneMsg.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);

        mMsg.setColumns(20);
        mMsg.setRows(5);
        scrPaneMsg.setViewportView(mMsg);

        bClear.setText("Clear");

        bReset.setText("Reset");

        bQuit.setText("Quit");

        GroupLayout msgPanelLayout = new GroupLayout(msgPanel);
        msgPanel.setLayout(msgPanelLayout);
        msgPanelLayout.setHorizontalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(scrPaneMsg, GroupLayout.DEFAULT_SIZE, 289, Short.MAX_VALUE)
                    .addGroup(msgPanelLayout.createSequentialGroup()
                        .addComponent(bClear, GroupLayout.PREFERRED_SIZE, 93, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(bReset, GroupLayout.PREFERRED_SIZE, 92, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(bQuit, GroupLayout.PREFERRED_SIZE, 92, GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );
        msgPanelLayout.setVerticalGroup(
            msgPanelLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(GroupLayout.Alignment.TRAILING, msgPanelLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(scrPaneMsg, GroupLayout.DEFAULT_SIZE, 400, Short.MAX_VALUE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(msgPanelLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(bClear)
                    .addComponent(bReset)
                    .addComponent(bQuit))
                .addContainerGap())
        );

        GroupLayout layout = new GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createParallelGroup(GroupLayout.Alignment.TRAILING, false)
                        .addComponent(redLedPanel, GroupLayout.Alignment.LEADING, 0, 230, Short.MAX_VALUE)
                        .addComponent(readerPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(antennaPanel, GroupLayout.Alignment.LEADING, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addComponent(greenLedPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING, false)
                    .addComponent(bSetLED, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(BlinkPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(23, 23, 23)
                .addComponent(BlinkPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(bSetLED, GroupLayout.PREFERRED_SIZE, 31, GroupLayout.PREFERRED_SIZE)
                .addContainerGap(80, Short.MAX_VALUE))
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(msgPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addContainerGap())
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(readerPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(antennaPanel, GroupLayout.PREFERRED_SIZE, GroupLayout.DEFAULT_SIZE, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(redLedPanel, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(greenLedPanel, GroupLayout.PREFERRED_SIZE, 132, GroupLayout.PREFERRED_SIZE)
                        .addGap(72, 72, 72))))
        );
        
        bInit.setMnemonic(KeyEvent.VK_I);
        bConn.setMnemonic(KeyEvent.VK_C);
        bGetFW.setMnemonic(KeyEvent.VK_F);
        bReset.setMnemonic(KeyEvent.VK_R);
        bClear.setMnemonic(KeyEvent.VK_L);
        bSetAnt.setMnemonic(KeyEvent.VK_A);
        bSetLED.setMnemonic(KeyEvent.VK_L);
        bQuit.setMnemonic(KeyEvent.VK_Q);
        bGetStat.setMnemonic(KeyEvent.VK_G);
        
        bInit.addActionListener(this);
        bConn.addActionListener(this);
        bGetFW.addActionListener(this);
        bReset.addActionListener(this);
        bClear.addActionListener(this);
        bSetAnt.addActionListener(this);
        bSetLED.addActionListener(this);
        bQuit.addActionListener(this);
        bGetStat.addActionListener(this);
        
        tT1.addKeyListener(this);
        tT2.addKeyListener(this);
        tRepeat.addKeyListener(this);
        
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
			bClear.setEnabled(true);
			bReset.setEnabled(true);
			
			//Look for ACR128 SAM and make it the default reader in the combobox
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
			
		    //add buttons
		    connActive=true;
		    bGetFW.setEnabled(true);
			rbAntOn.setEnabled(true);
			rbAntOff.setEnabled(true);
			bSetAnt.setEnabled(true);
			rbredFinOn.setEnabled(true);
			rbredFinOff.setEnabled(true);
			rbredStateMaskOn.setEnabled(true);
			rbredStateMaskOff.setEnabled(true);
			rbredInitOn.setEnabled(true);
			rbredInitOff.setEnabled(true);
			rbredBlinkOn.setEnabled(true);
			rbredBlinkOff.setEnabled(true);
			rbgreenFinOn.setEnabled(true);
			rbgreenFinOff.setEnabled(true);
			rbgreenStateMaskOn.setEnabled(true);
			rbgreenStateMaskOff.setEnabled(true);
			rbgreenInitOn.setEnabled(true);
			rbgreenInitOff.setEnabled(true);
			rbgreenBlinkOn.setEnabled(true);
			rbgreenBlinkOff.setEnabled(true);
			tT1.setEnabled(true);
			tT2.setEnabled(true);
			tRepeat.setEnabled(true);
			rbLinkOpt1.setEnabled(true);
			rbLinkOpt2.setEnabled(true);
			rbLinkOpt3.setEnabled(true);
			rbLinkOpt4.setEnabled(true);
			bSetLED.setEnabled(true);
			rbAntOn.setSelected(true);
			rbredFinOff.setSelected(true);
			rbredStateMaskOff.setSelected(true);
			rbredInitOff.setSelected(true);
			rbredBlinkOff.setSelected(true);
			rbgreenFinOff.setSelected(true);
			rbgreenStateMaskOff.setSelected(true);
			rbgreenInitOff.setSelected(true);
			rbgreenBlinkOff.setSelected(true);
			rbLinkOpt1.setSelected(true);
			bGetStat.setEnabled(true);

		}
		
		if(bGetFW == e.getSource())
		{
			String tmpStr;
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x00;
			SendBuff[2] = (byte)0x48;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x00;
			
			SendLen = 5;
			RecvLen[0] = 10;
			
			retCode = transmit();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				return;
				
			}
			
			//interpret firmware data
			tmpStr = "Firmware Version: ";
			
			for (int i=0; i<RecvLen[0] ; i++)
			{
				
				if ((RecvBuff[i] & 0xFF) != 0x00)
					tmpStr = tmpStr + (char)(RecvBuff[i]);
				
			}
			
			displayOut(3, 0, tmpStr);
			
			
		}
		
		if(bGetStat == e.getSource())
		{
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x00;
			SendBuff[2] = (byte)0x00;
			SendBuff[3] = (byte)0x00;
			SendBuff[4] = (byte)0x02;
			SendBuff[5] = (byte)0xD4;
			SendBuff[6] = (byte)0x04;
			SendLen = 7;
			RecvLen[0] = 12;
			
			retCode = transmit();
			
			if (retCode != ACSModule.SCARD_S_SUCCESS)
			{
				
				return;
				
			}
			
			//interpret data
			String tmpStr = "", tmpHex="";
			
			for(int i =0; i<RecvLen[0]; i++)
			{
				
				tmpHex = Integer.toHexString(((Byte)RecvBuff[i]).intValue() & 0xFF).toUpperCase();
				
				//For single character hex
				if (tmpHex.length() == 1) 
					tmpHex = "0" + tmpHex;
				
				tmpStr += tmpHex;  
				
			}
			
			//displayOut(3, 0, tmpStr.trim());
			if ((tmpStr.lastIndexOf("D505280000809000")> -1)||(tmpStr.lastIndexOf("D505000000809000")> -1))
			{
				displayOut(3, 0, "No Tag is in the field: " + Integer.toHexString(((Byte)RecvBuff[2]).intValue() & 0xFF).toUpperCase());
			}
			else
			{
				//error code
				displayOut(3, 0, "Error Code: " + Integer.toHexString(((Byte)RecvBuff[2]).intValue() & 0xFF).toUpperCase());
				
				//Field indicates if an external RF field is present and detected
			    //(Field=0x01 or not (Field 0x00)
				if(RecvBuff[3] == 0x01 )
				{
					
					displayOut(3, 0, "External RF field is Present and detected: " + Integer.toHexString(((Byte)RecvBuff[3]).intValue() & 0xFF).toUpperCase());
					
				}
				else
				{
					
					displayOut(3, 0, "External RF field is NOT Present and NOT detected: " + Integer.toHexString(((Byte)RecvBuff[3]).intValue() & 0xFF).toUpperCase());
					
				}
				
				//Number of targets acting as initiator.The default value is 1
				displayOut(3, 0, "Number of Target: " + Integer.toHexString(((Byte)RecvBuff[4]).intValue() & 0xFF).toUpperCase());
				
				//Logical number
				displayOut(3, 0, "Number of Target: " + Integer.toHexString(((Byte)RecvBuff[5]).intValue() & 0xFF).toUpperCase());
				
				//Bit rate in reception
			    switch(RecvBuff[6])
			    { 
			       case 0x00: 
			    	   displayOut(3, 0, "Bit Rate in Reception: " +Integer.toHexString(((Byte)RecvBuff[6]).intValue() & 0xFF).toUpperCase()+ " (106 kbps)");
			    	   break;
			       case 0x01: 
			    	   displayOut(3, 0, "Bit Rate in Reception: " +Integer.toHexString(((Byte)RecvBuff[6]).intValue() & 0xFF).toUpperCase()+ " (212 kbps)");
			    	   break;
			       case 0x02: 
			    	   displayOut(3, 0, "Bit Rate in Reception: " +Integer.toHexString(((Byte)RecvBuff[6]).intValue() & 0xFF).toUpperCase()+ " (424 kbps)");
			    	   break;
			       
			    }
			    
			    //Bit rate in transmission
			    switch (RecvBuff[7])
			    {
			       
			       case 0x00: 
			       			displayOut(3, 0, "Bit Rate in Transmission: "+Integer.toHexString(((Byte)RecvBuff[7]).intValue() & 0xFF).toUpperCase()+ " (106 kbps)");
			       			break;
			       case 0x01: 
			    	   		displayOut(3, 0, "Bit Rate in Transmission: " +Integer.toHexString(((Byte)RecvBuff[7]).intValue() & 0xFF).toUpperCase()+ " (212 kbps)");
			    	   		break;
			       case 0x02: 
				    	   displayOut(3, 0, "Bit Rate in Transmission: " +Integer.toHexString(((Byte)RecvBuff[7]).intValue() & 0xFF).toUpperCase()+ " (424 kbps)");
				    	   break;
			       
			    }
			    
			    switch (RecvBuff[8])
			    {
			    
			        case 0x00: 
			        		displayOut(3, 0, "Modulation Type: " +Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase()+ " (ISO14443 or Mifare)");
			        		break;
			        case 0x01: 
			        		displayOut(3, 0, "Modulation Type: " +Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase()+ " (Active mode)");
			        		break;
			        case 0x02: 
			        		displayOut(3, 0, "Modulation Type: " +Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase()+ " (Innovision Jewel tag)");
			        		break;
			        case 0x10: 
			        		displayOut(3, 0, "Modulation Type: " +Integer.toHexString(((Byte)RecvBuff[8]).intValue() & 0xFF).toUpperCase()+ " (Felica)");
			        		break;
			        
			    }
		}
	    
	}
		
		if(bSetAnt == e.getSource())
		{
			
			//set antenna options
			clearBuffers();
			SendBuff[0] = (byte) 0xFF;
			SendBuff[1] = (byte) 0x00;
			SendBuff[2] = (byte) 0x00;
			SendBuff[3] = (byte) 0x00;
			SendBuff[4] = (byte) 0x04;
			SendBuff[5] = (byte) 0xD4;
			SendBuff[6] = (byte) 0x32;
			SendBuff[7] = (byte) 0x01;
			
			if (rbAntOn.isSelected())
				SendBuff[8] = (byte)0x01;
			else
				SendBuff[8] = (byte)0x00;
			
			SendLen = 9;

			retCode = transmit();
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
	
		}
		
		if(bSetLED == e.getSource())
		{
			
			//validate input
			if (tT1.getText().equals(""))
			{
				tT1.requestFocus();
				return;
			}
			
			if (tT2.getText().equals(""))
			{
				tT2.requestFocus();
				return;
			}
			
			if (tRepeat.getText().equals("") || tRepeat.getText().equals("0") || tRepeat.getText().equals("00"))
			{
				tRepeat.requestFocus();
				return;
			}
			
			clearBuffers();
			SendBuff[0] = (byte)0xFF;
			SendBuff[1] = (byte)0x00;
			SendBuff[2] = (byte)0x40;
			SendBuff[3] = (byte)0x0;
			
			if(rbredFinOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x01));
			
			if(rbgreenFinOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x02));
			
			if(rbredStateMaskOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x04));
			
			if(rbgreenStateMaskOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x08));
			
			if(rbredInitOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x10));
			
			if(rbgreenInitOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x20));
			
			if(rbredBlinkOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x40));
			
			if(rbgreenBlinkOn.isSelected())
				SendBuff[3] = (byte)(SendBuff[3] | (0x80));
			
			SendBuff[4] = (byte) 0x40;
			SendBuff[5] = (byte) ((Integer)Integer.parseInt(tT1.getText(), 16)).byteValue();
			SendBuff[6] = (byte) ((Integer)Integer.parseInt(tT2.getText(), 16)).byteValue();
			SendBuff[7] = (byte) ((Integer)Integer.parseInt(tRepeat.getText(), 16)).byteValue();
			
			if(rbLinkOpt1.isSelected())
				SendBuff[8] = (byte)0x00;
			
			if(rbLinkOpt2.isSelected())
				SendBuff[8] = (byte)0x01;
			
			if(rbLinkOpt3.isSelected())
				SendBuff[8] = (byte)0x02;
			
			if(rbLinkOpt4.isSelected())
				SendBuff[8] = (byte)0x03;
			
			SendLen = 9;
			
			retCode = transmit();
			
			if(retCode != ACSModule.SCARD_S_SUCCESS)
				return;
			
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
			cbReader.addItem("Please select reader                   ");
			
		}
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
	
	public void clearBuffers()
	{
		
		for(int i=0; i<262; i++)
		{
			
			SendBuff[i]=(byte) 0x00;
			RecvBuff[i]= (byte) 0x00;
			
		}
		
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
  		if(tT1.isFocusOwner() || tT2.isFocusOwner() || tRepeat.isFocusOwner())
  		{
  			
  			if (VALIDCHARSHEX.indexOf(x) == -1 ) 
  				ke.setKeyChar(empty);		
	  			
  		}

  			
  		//Limit character length
	  	if(tT1.isFocusOwner() || tT2.isFocusOwner() || tRepeat.isFocusOwner())
	  	{
  			if(((JTextField)ke.getSource()).getText().length() >= 2 ) 
	  		{
	  			
		  		ke.setKeyChar(empty);	
		  		return;
	  			
	  		}
	  	}
	
  	    	
	}
	

	
	public void initMenu()
	{
	
		connActive = false;
		bConn.setEnabled(false);
		bInit.setEnabled(true);
		bGetFW.setEnabled(false);
		bReset.setEnabled(false);
		rbAntOn.setEnabled(false);
		rbAntOff.setEnabled(false);
		bSetAnt.setEnabled(false);
		rbredFinOn.setEnabled(false);
		rbredFinOff.setEnabled(false);
		rbredStateMaskOn.setEnabled(false);
		rbredStateMaskOff.setEnabled(false);
		rbredInitOn.setEnabled(false);
		rbredInitOff.setEnabled(false);
		rbredBlinkOn.setEnabled(false);
		rbredBlinkOff.setEnabled(false);
		rbgreenFinOn.setEnabled(false);
		rbgreenFinOff.setEnabled(false);
		rbgreenStateMaskOn.setEnabled(false);
		rbgreenStateMaskOff.setEnabled(false);
		rbgreenInitOn.setEnabled(false);
		rbgreenInitOff.setEnabled(false);
		rbgreenBlinkOn.setEnabled(false);
		rbgreenBlinkOff.setEnabled(false);
		tT1.setEnabled(false);
		tT2.setEnabled(false);
		tRepeat.setEnabled(false);
		rbLinkOpt1.setEnabled(false);
		rbLinkOpt2.setEnabled(false);
		rbLinkOpt3.setEnabled(false);
		rbLinkOpt4.setEnabled(false);
		bGetStat.setEnabled(false);
		tT1.setText("00");
		tT2.setText("00");
		tRepeat.setText("01");
		bSetLED.setEnabled(false);
		displayOut(0, 0, "Program Ready");
		
	}
	

	

    
    public static void main(String args[]) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                new DevProg().setVisible(true);
            }
        });
    }



}
