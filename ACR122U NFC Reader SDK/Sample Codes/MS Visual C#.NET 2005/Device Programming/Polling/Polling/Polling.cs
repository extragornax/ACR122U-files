/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                   execute card detection polling functions using ACR128
'
'  File   :         Polling.cs         
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         July 30, 2008
'
' Revision Trail:   (Date/Author/Description) 
'
'=========================================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Polling
{
    public partial class Polling : Form
    {
             
        public long pollCase;
        public bool connActive = false;
        public bool autoDet;
        public bool dualPoll;
        public bool detect;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen;
        public int RecvLen;
        public int nBytesRet;
        public int ATRLen;
        byte[] ATRVal = new byte[257];
        public ModWinsCard.SCARD_READERSTATE RdrState;
        public ModWinsCard.SCARD_IO_REQUEST ioRequest;
        public int dwState, dwActProtocol;         
        public int retCode, hContext, hCard, Protocol, ReaderCount;

        public Polling()
        {
            InitializeComponent();
        }

        private void InitMenu()
        {
            connActive = false;
            cbReader.Text = "";
            mMsg.Text = "";
            mMsg.Clear();
            bInit.Enabled = true;
            bConnect.Enabled = false;           
            bClear.Enabled = false;
            cbOpt1.Checked = false;
            cbOpt2.Checked = false;
            cbOpt3.Checked = false;
            cbOpt4.Checked = false;
            cbOpt5.Checked = false;
            cbOpt6.Checked = false;
            cbOpt7.Checked = false;
            pollTimer.Enabled = false;
            gbPollOpt.Enabled = false;
            bStartPoll.Enabled = false;
            displayOut(0, 0, "Program ready");

        }

        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    mMsg.SelectionColor = Color.Green;
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;
                case 1:
                    mMsg.SelectionColor = Color.Red;
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;
                case 2:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = "<" + PrintText;
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;
                case 3:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = ">" + PrintText;
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;
                case 4:
                    mMsg.SelectionColor = Color.Red;
                    PrintText = ">" + PrintText;
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;

                case 5:
                    mMsg.SelectionColor = Color.Black;
                    tsMsg4.Text = PrintText;
                    break;

                case 6:
                    mMsg.SelectionColor = Color.Black;
                    tsMsg2.Text = PrintText;
                    break;

                case 7:
                    mMsg.SelectionColor = Color.Purple;
                    mMsg.AppendText(PrintText);
                    mMsg.AppendText("\n");
                    break;

            }
            
            mMsg.Focus();

        }

        private void bInit_Click(object sender, EventArgs e)
        {

            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            // Establish Context
            retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;

            }

            // 2. List PC/SC card readers installed in the system

            retCode = ModWinsCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

                return;
            }

            EnableButtons();

            byte[] ReadersList = new byte[pcchReaders];

            // Fill reader list
            retCode = ModWinsCard.SCardListReaders(this.hContext, null, ReadersList, ref pcchReaders);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                mMsg.AppendText("SCardListReaders Error: " + ModWinsCard.GetScardErrMsg(retCode));

                return;
            }
            else
            {
                displayOut(0, 0, " ");
            }

            rName = "";
            indx = 0;

            // Convert reader buffer to string
            while (ReadersList[indx] != 0)
            {

                while (ReadersList[indx] != 0)
                {
                    rName = rName + (char)ReadersList[indx];
                    indx = indx + 1;
                }

                //Add reader name to list
                cbReader.Items.Add(rName);
                rName = "";
                indx = indx + 1;

            }

            if (cbReader.Items.Count > 0)
            {
                cbReader.SelectedIndex = 0;

            }

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;           
            bClear.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {

            retCode = ModWinsCard.SCardConnect(hContext, cbReader.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_SHARED,
                                              ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)

                displayOut(1, retCode, "");
            else
            {
                displayOut(0, 0, "Successful connection to " + cbReader.Text);

            }
            connActive = true;
            bClear.Enabled = true;
            cbOpt1.Checked = true;
            cbOpt2.Checked = true;
            cbOpt3.Checked = true;
            cbOpt4.Checked = true;
            cbOpt5.Checked = true;
            cbOpt6.Checked = true;
            cbOpt7.Checked = true;
            gbPollOpt.Enabled = true;
            bStartPoll.Enabled = true;
            opt250.Checked = true;

        }

        private void bClear_Click(object sender, EventArgs e)
        {
            mMsg.Clear();
        }

        private void bReset_Click(object sender, EventArgs e)
        {

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            retCode = ModWinsCard.SCardReleaseContext(hCard);
            InitMenu();
        }

        private void bQuit_Click(object sender, EventArgs e)
        {

            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);

        }
        private void ClearBuffers()
        {

            long indx;

            for (indx = 0; indx <= 262; indx++)
            {

                RecvBuff[indx] = 0;
                SendBuff[indx] = 0;

            }

        }
        private int Transmit() 
        {
         
            ioRequest.dwProtocol = Protocol;
            ioRequest.cbPciLength = 8;


            RecvLen = 262;

            // Issue SCardTransmit
            retCode = ModWinsCard.SCardTransmit(hCard, ref ioRequest, ref SendBuff[0], SendLen, ref ioRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");

            }
            return retCode;
        

        }

        private void getParam()
        {

             // get the PICC Operating Parameter of the reader.
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x50;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x00;
            SendLen = 5;
            RecvLen = 2;

            retCode = Transmit();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
            return;
            }

        }

        private void bGetPollOpt_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;

            getParam();

            //prints the command sent
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {
               
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);

            //print the response recieved
            tmpStr = "";

            for (indx = 0; indx <= RecvLen - 1; indx++)
            {

                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            displayOut(3, 0, (tmpStr).Trim());

            //interpret the return response
            if ((RecvBuff[0] & 0x80) != 0)
            {

                displayOut(3, 0, "Automatic Polling is enabled.");
                cbOpt1.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Automatic Polling is disabled.");
                cbOpt1.Checked = false;

            }

            if ((RecvBuff[0] & 0x40) != 0)
            {

                displayOut(3, 0, "Automatic ATS Generation is enabled.");
                cbOpt2.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Automatic ATS Generation is disabled.");
                cbOpt2.Checked = false;

            }

            if ((RecvBuff[0] & 0x20) != 0)
            {

                displayOut(3, 0, "250 ms.");
                opt250.Checked = true;
            }

            else
            {

                displayOut(3, 0, "500 ms.");
                opt500.Checked = true;

            }

            if ((RecvBuff[0] & 0x10) != 0)
            {

                displayOut(3, 0, "Detect Felica 424K Card Enabled");
                cbOpt7.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Detect Felica 424K Card Disabled");
                cbOpt7.Checked = false;

            }

            if ((RecvBuff[0] & 0x08) != 0)
            {

                displayOut(3, 0, "Detect Felica 212K Card Enabled");
                cbOpt6.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Detect Felica 212K Card Disabled");
                cbOpt6.Checked = false;

            }

            if ((RecvBuff[0] & 0x04) != 0)
            {

                displayOut(3, 0, "Detect Topaz Card Enabled");
                cbOpt5.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Detect Topaz Card Disabled");
                cbOpt5.Checked = false;

            }

            if ((RecvBuff[0] & 0x02) != 0)
            {

                displayOut(3, 0, "Detect ISO14443 Type B Card Enabled");
                cbOpt4.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Detect ISO14443 Type B Card Disabled");
                cbOpt4.Checked = false;

            }

            if ((RecvBuff[0] & 0x01) != 0)
            {

                displayOut(3, 0, "Detect ISO14443 Type A Card Enabled");
                cbOpt3.Checked = true;
            }

            else
            {

                displayOut(3, 0, "Detect ISO14443 Type A Card Disabled");
                cbOpt3.Checked = false;

            }
        
        }

        private void bSetPollOpt_Click(object sender, EventArgs e)
        {

            string tmpStr;
            int indx;

            //set the PICC Operating Parameter of the reader.
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x51;
            SendBuff[3] = 0x00;

            if (cbOpt3.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x01);
                displayOut(3, 0, "Detect ISO14443 Type A Card Enabled");
            }

            else
            {

                displayOut(3, 0, "Detect ISO14443 Type A Card Disabled");

            }

            if (cbOpt4.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x02);
                displayOut(3, 0, "Detect ISO14443 Type B Card Enabled");
            }
            else
            {

                displayOut(3, 0, "Detect ISO14443 Type B Card Disabled");

            }

            if (cbOpt5.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x04);
                displayOut(3, 0, "Detect Topaz Card Enabled");
            }

            else
            {

                displayOut(3, 0, "Detect Topaz Card Disabled");

            }

            if (cbOpt6.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x08);
                displayOut(3, 0, "Detect FeliCa 212K Card Enabled");
            }

            else
            {

                displayOut(3, 0, "Detect FeliCa 212K Card Disabled");

            }

            if (cbOpt7.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x10);
                displayOut(3, 0, "Detect FeliCa 424K Card Enabled");
            }

            else
            {

                displayOut(3, 0, "Detect Felica 424K Card Disabled");

            }

            if (opt250.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x20);
                pollTimer.Interval = 250;
                displayOut(3, 0, "Poll Interval is 250 ms");
            }

            else
            {

                pollTimer.Interval = 500;
                displayOut(3, 0, "Poll Interval is 500 ms");

            }

            if (cbOpt2.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x40);
                displayOut(3, 0, "Automatic ATS Generation is Enabled");
            }

            else
            {

                displayOut(3, 0, "Automatic ATS Generation is Disabled");

            }

            if (cbOpt1.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x80);
                displayOut(3, 0, "Automatic PICC Polling is Enabled");
            }

            else
            {

                displayOut(3, 0, "Automatic PICC Polling is Disabled");

            }

            SendBuff[4] = 0x00;

            SendLen = 5;
            RecvLen = 1;

            //prints the command sent
            tmpStr = "";
            for (indx = 0; indx <= SendLen - 1; indx++)
            {
                
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);

            retCode = Transmit();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 
            }

            //prints the response recievd
            tmpStr = "";

            for (indx = 0; indx <= RecvLen - 1; indx++)
            {
                
                tmpStr = tmpStr + " " + string.Format("{0:X2}", RecvBuff[indx]);

            }

            displayOut(3, 0, (tmpStr).Trim());

        }

        private void bStartPoll_Click(object sender, EventArgs e)
        {
            pollTimer.Enabled = true;
            if (detect)
            {

                displayOut(7, 0, "Polling Stopped");
                bStartPoll.Text = "Start Polling";
                pollTimer.Enabled = false;
                tsMsg2.Text = "";
                tsMsg4.Text = "";
                detect = false;
                return;

            }

            displayOut(7, 0, "Polling Started");
            bStartPoll.Text = "End Polling";
            pollTimer.Enabled = true;
            detect = true;
        }

        private void pollTimer_Tick(object sender, EventArgs e)
        {
            //Always use a valid connection
            retCode = CardConnect(1);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(5, 0, "No card within range.");
                tsMsg2.Text = "";
                return; 

            }

            //CheckCard();

            if (CheckCard())
            {

                displayOut(5, 0, "Card is detected.");
            }

            else
            {

                displayOut(5, 0, "No card within range.");
                tsMsg2.Text = "";

            }
        }

        private int CardConnect(int connType)
        {
            //bool functionReturnValue = false;

            if (connActive)
            {

                retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);

            }

            //Connect
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.Text, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                if (connType != 1)
                {
                    displayOut(1, retCode, "");
                }
                connActive = false;
                return retCode; 
            }

            else
            {

                if (connType != 1)
                {

                    displayOut(0, 0, "Successful connection to " + cbReader.Text);

                }

                //functionReturnValue = retCode;

            }
            return retCode;

        }

        private bool CheckCard()
        {
            bool functionReturnValue = false;

            //Variable declaration
            int ReaderLen = 0;
            long tmpWord;

            tmpWord = 32;
            ATRLen = Convert.ToInt32(tmpWord);

            retCode = ModWinsCard.SCardStatus(hCard, cbReader.Text, ref ReaderLen, ref dwState, ref dwActProtocol, ref ATRVal[0], ref ATRLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                //Call DisplayOut(1, retCode, "")
                functionReturnValue = false;
                return functionReturnValue; 
            }

            else
            {

                InterpretATR();
                functionReturnValue = true;

            }
            return functionReturnValue;

        }

        private void InterpretATR()
        {

            string RIDVal, cardName, sATRStr, lATRStr, tmpVal;
            int indx, indx2;

            //4. Interpret ATR and guess card
            // 4.1. Mifare cards using ISO 14443 Part 3 Supplemental Document
            if ((int)(ATRLen) > 14)
            {

                RIDVal = "";
                sATRStr = "";
                lATRStr = "";

                for (indx = 7; indx <= 11; indx++)
                {

                    RIDVal = RIDVal + " " + string.Format("{0:X2}", ATRVal[indx]);

                }


                for (indx = 0; indx <= 4; indx++)
                {

                    //shift bit to right
                    tmpVal = ATRVal[indx].ToString();

                    for (indx2 = 1; indx2 <= 4; indx2++)
                    {

                        tmpVal = Convert.ToString(Convert.ToInt32(tmpVal) / 2);

                    }

                    if (((indx == '1') & (tmpVal == "8")))
                    {

                        lATRStr = lATRStr + "8X";
                        sATRStr = sATRStr + "8X";
                    }

                    else
                    {

                        if (indx == 4)
                        {

                            lATRStr = lATRStr + " " + string.Format("{0:X2}", ATRVal[indx]);
                        }

                        else
                        {

                            lATRStr = lATRStr + " " + string.Format("{0:X2}", ATRVal[indx]);
                            sATRStr = sATRStr + " " + string.Format("{0:X2}", ATRVal[indx]);

                        }

                    }

                }

                cardName = "";

                // Felica and Topaz Cards
                if (ATRVal[12] == 0x03)
                {
                    if (ATRVal[13] == 0xF0)
                    {
                        switch (ATRVal[14])
                        {
                            case 0x11:
                                cardName = " FeliCa 212K";
                                break;
                            case 0x12:
                                cardName = " Felica 424K";
                                break;
                            case 0x04:
                                cardName = " Topaz";
                                break;

                        }

                    }
                }


                if (ATRVal[12] == 0x03)
                {

                    if (ATRVal[13] == 0x00)
                    {

                        switch (ATRVal[14])
                        {

                            case 0x01:
                                cardName = cardName + " Mifare Standard 1K";
                                break;
                            case 0x02:
                                cardName = cardName + " Mifare Standard 4K";
                                break;
                            case 0x03:
                                cardName = cardName + " Mifare Ultra light";
                                break;
                            case 0x04:
                                cardName = cardName + " SLE55R_XXXX";
                                break;
                            case 0x06:
                                cardName = cardName + " SR176";
                                break;
                            case 0x07:
                                cardName = cardName + " SRI X4K";
                                break;
                            case 0x08:
                                cardName = cardName + " AT88RF020";
                                break;
                            case 0x09:
                                cardName = cardName + " AT88SC0204CRF";
                                break;
                            case 0x0A:
                                cardName = cardName + " AT88SC0808CRF";
                                break;
                            case 0x0B:
                                cardName = cardName + " AT88SC1616CRF";
                                break;
                            case 0x0C:
                                cardName = cardName + " AT88SC3216CRF";
                                break;
                            case 0x0D:
                                cardName = cardName + " AT88SC6416CRF";
                                break;
                            case 0x0E:
                                cardName = cardName + " SRF55V10P";
                                break;
                            case 0xF:
                                cardName = cardName + " SRF55V02P";
                                break;
                            case 0x10:
                                cardName = cardName + " SRF55V10S";
                                break;
                            case 0x11:
                                cardName = cardName + " SRF55V02S";
                                break;
                            case 0x12:
                                cardName = cardName + " TAG IT";
                                break;
                            case 0x13:
                                cardName = cardName + " LRI512";
                                break;
                            case 0x14:
                                cardName = cardName + " ICODESLI";
                                break;
                            case 0x15:
                                cardName = cardName + " TEMPSENS";
                                break;
                            case 0x16:
                                cardName = cardName + " I.CODE1";
                                break;
                            case 0x17:
                                cardName = cardName + " PicoPass 2K";
                                break;
                            case 0x18:
                                cardName = cardName + " PicoPass 2KS";
                                break;
                            case 0x19:
                                cardName = cardName + " PicoPass 16K";
                                break;
                            case 0x1A:
                                cardName = cardName + " PicoPass 16KS";
                                break;
                            case 0x1B:
                                cardName = cardName + " PicoPass 16K(8x2)";
                                break;
                            case 0x1C:
                                cardName = cardName + " PicoPass 16KS(8x2)";
                                break;

                            case 0x1D:
                                cardName = cardName + " PicoPass 32KS(16+16)";
                                break;
                            case 0x1E:
                                cardName = cardName + " PicoPass 32KS(16+8x2)";
                                break;
                            case 0x1F:
                                cardName = cardName + " PicoPass 32KS(8x2+16)";
                                break;
                            case 0x20:
                                cardName = cardName + " PicoPass 32KS(8x2+8x2)";
                                break;
                            case 0x21:
                                cardName = cardName + " LRI64";
                                break;
                            case 0x22:
                                cardName = cardName + " I.CODE UID";
                                break;
                            case 0x23:
                                cardName = cardName + " I.CODE EPC";
                                break;
                            case 0x24:
                                cardName = cardName + " LRI12";
                                break;
                            case 0x25:
                                cardName = cardName + " LRI128";
                                break;
                            case 0x26:
                                cardName = cardName + " Mifare Mini";
                                break;

                        }
                    }

                    else
                    {

                        if (ATRVal[13] == 0xFF)
                        {

                            switch (ATRVal[14])
                            {

                                case 0x09:
                                    cardName = cardName + " Mifare Mini";
                                    break;

                            }

                        }

                    }

                    displayOut(6, 0, cardName);

                }

            }

            //4.2. Mifare DESFire card using ISO 14443 Part 4
            if ((int)ATRLen == 11)
            {

                RIDVal = "";

                for (indx = 4; indx <= 9; indx++)
                {

                    RIDVal = RIDVal + " " + string.Format("{0:X2}", ATRVal[indx]);

                }

                if (RIDVal == " 06 75 77 81 02 80")
                {

                    displayOut(6, 0, "Mifare DESFire");

                }

            }

            //4.3. Other cards using ISO 14443 Part 4
            if ((int)ATRLen == 17)
            {

                RIDVal = "";

                for (indx = 4; indx <= 15; indx++)
                {

                    RIDVal = RIDVal + " " + string.Format("{0:X2}", RecvBuff[indx]);

                }

                if (RIDVal == "50122345561253544E3381C3")
                {

                    displayOut(6, 0, "ST19XRC8E");

                }

            }

            //4.4. other cards using ISO 14443 Type A or B
            lATRStr = "";
            sATRStr = "";

            if (lATRStr == "3B8X800150")
            {

                displayOut(6, 0, "ISO 14443B ");
            }

            else
            {

                if (sATRStr == "3B8X8001")
                {

                    displayOut(6, 0, "ISO 14443A");

                }

            }


        }

    
    }
}