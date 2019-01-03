/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                     get ATR from cards using ACR128
'  
'  File :           GetATR.cs
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 22, 2008
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

namespace Get_ATR
{
    public partial class GetATR : Form
    {

        public int retCode, hContext, hCard, Protocol, ReaderCount;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, ATRLen, dwState, dwActProtocol;
        public byte[] ATRVal = new byte[257];
        string sReaderList;
        byte[] sReaderGroup;
        
        public GetATR()
        {
            InitializeComponent();
        }


        private void displayOut(int errType, int retVal, string PrintText)
        {

            switch (errType)
            {

                case 0:
                    mMsg.SelectionColor = Color.Green;
                    break;
                case 1:
                    mMsg.SelectionColor = Color.Red;
                    PrintText = ModWinsCard.GetScardErrMsg(retVal);
                    break;
                case 2:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = "<" + PrintText;
                    break;
                case 3:
                    mMsg.SelectionColor = Color.Black;
                    PrintText = ">" + PrintText;
                    break;
                case 4:
                    break;

            }
            
            mMsg.AppendText(PrintText);
            mMsg.AppendText("\n");
            mMsg.SelectionColor = Color.Black;
            mMsg.Focus();

        }

        private void InitMenu()
        {
            connActive = false;
            cbReader.Text = "";
            mMsg.Text = "";
            mMsg.Clear();
            bInit.Enabled = true;
            bConnect.Enabled = false;
            bGetAtr.Enabled = false;
            bClear.Enabled = false;
            displayOut(0, 0, "Program ready");

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bGetAtr.Enabled = true;
            bClear.Enabled = true;

        }

        private void bInit_Click(object sender, EventArgs e)
        {

            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";

            //Establish Context
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

            //Convert reader buffer to string
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
        private void bConnect_Click(object sender, EventArgs e)
        {


            retCode = ModWinsCard.SCardConnect(hContext, cbReader.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_SHARED,
                                              ModWinsCard.SCARD_PROTOCOL_T0 | ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)

                displayOut(1,retCode,"");
            else
            {
                displayOut(0, 0, "Successful connection to " + cbReader.Text);
               
            }
            connActive = true;
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMenu();
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

        private void bGetAtr_Click(object sender, EventArgs e)
        {            
            
            string tmpStr;
            int indx;

            displayOut(0, 0, "Invoke Card Status");
            ATRLen = 33;

            retCode = ModWinsCard.SCardStatus(hCard, cbReader.Text, ref ReaderLen, ref dwState, ref dwActProtocol, ref ATRVal[0], ref ATRLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
                System.Environment.Exit(0);

            }

            else
            {

                tmpStr = "ATR Length : " + ATRLen.ToString();
                displayOut(3, 0, tmpStr);

                tmpStr = "ATR Value : ";

                for (indx = 0; indx <= ATRLen - 1; indx++)
                {

                    tmpStr = tmpStr + " " + string.Format("{0:X2}", ATRVal[indx]);
                  
                }

                displayOut(3, 0, tmpStr);

            }

            tmpStr = "Active Protocol: ";

            switch (dwActProtocol)
            {

                case 1:
                    tmpStr = tmpStr + "T=0";
                    break;
                case 2:
                    tmpStr = tmpStr + "T=1";    
                    break;

                default:
                    tmpStr = "No protocol is defined.";
                    break;
            }

            displayOut(3, 1, tmpStr);
            InterpretATR();           
        
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

                if (RIDVal != "A0 00 00 03 06")
                {

                    switch (ATRVal[12])
                    {

                        case 0:
                            cardName = "No card information";
                            break;
                        case 1:
                            cardName = "ISO 14443 A, Part1 Card Type";
                            break;
                        case 2:
                            cardName = "ISO 14443 A, Part2 Card Type";
                            break;
                        case 3:
                            cardName = "ISO 14443 A, Part3 Card Type";
                            break;
                        case 5:
                            cardName = "ISO 14443 B, Part1 Card Type";
                            break;
                        case 6:
                            cardName = "ISO 14443 B, Part2 Card Type";
                            break;
                        case 7:
                            cardName = "ISO 14443 B, Part3 Card Type";
                            break;
                        case 9:
                            cardName = "ISO 15693, Part1 Card Type";
                            break;
                        case 10:
                            cardName = "ISO 15693, Part2 Card Type";
                            break;
                        case 11:
                            cardName = "ISO 15693, Part3 Card Type";
                            break;
                        case 12:
                            cardName = "ISO 15693, Part4 Card Type";
                            break;
                        case 13:
                            cardName = "Contact Card (7816-10) IIC Card Type";
                            break;
                        case 14:
                            cardName = "Contact Card (7816-10) Extended IIC Card Type";
                            break;
                        case 15:
                            cardName = "0Contact Card (7816-10) 2WBP Card Type";
                            break;
                        case 16:
                            cardName = "Contact Card (7816-10) 3WBP Card Type";
                            break;

                    }

                }
                
                // Felica and Topaz Cards
                if (ATRVal[12] == 0x03)
                {
                    if (ATRVal[13] == 0xF0)
                    {
                            switch (ATRVal[14])
                            {
                                case 0x11:
                                    cardName = cardName + ": FeliCa 212K";
                                    break;
                                case 0x12:
                                    cardName = cardName + ": Felica 424K";
                                    break;
                                case 0x04:
                                    cardName = cardName + ": Topaz";
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
                                cardName = cardName + ": Mifare Standard 1K";
                                break;
                            case 0x02:
                                cardName = cardName + ": Mifare Standard 4K";
                                break;
                            case 0x03:
                                cardName = cardName + ": Mifare Ultra light";
                                break;
                            case 0x04:
                                cardName = cardName + ": SLE55R_XXXX";
                                break;
                            case 0x06:
                                cardName = cardName + ": SR176";
                                break;
                            case 0x07:
                                cardName = cardName + ": SRI X4K";
                                break;
                            case 0x08:
                                cardName = cardName + ": AT88RF020";
                                break;
                            case 0x09:
                                cardName = cardName + ": AT88SC0204CRF";
                                break;
                            case 0x0A:
                                cardName = cardName + ": AT88SC0808CRF";
                                break;
                            case 0x0B:
                                cardName = cardName + ": AT88SC1616CRF";
                                break;
                            case 0x0C:
                                cardName = cardName + ": AT88SC3216CRF";
                                break;
                            case 0x0D:
                                cardName = cardName + ": AT88SC6416CRF";
                                break;
                            case 0x0E:
                                cardName = cardName + ": SRF55V10P";
                                break;
                            case 0xF:
                                cardName = cardName + ": SRF55V02P";
                                break;
                            case 0x10:
                                cardName = cardName + ": SRF55V10S";
                                break;
                            case 0x11:
                                cardName = cardName + ": SRF55V02S";
                                break;
                            case 0x12:
                                cardName = cardName + ": TAG IT";
                                break;
                            case 0x13:
                                cardName = cardName + ": LRI512";
                                break;
                            case 0x14:
                                cardName = cardName + ": ICODESLI";
                                break;
                            case 0x15:
                                cardName = cardName + ": TEMPSENS";
                                break;
                            case 0x16:
                                cardName = cardName + ": I.CODE1";
                                break;
                            case 0x17:
                                cardName = cardName + ": PicoPass 2K";
                                break;
                            case 0x18:
                                cardName = cardName + ": PicoPass 2KS";
                                break;
                            case 0x19:
                                cardName = cardName + ": PicoPass 16K";
                                break;
                            case 0x1A:
                                cardName = cardName + ": PicoPass 16KS";
                                break;
                            case 0x1B:
                                cardName = cardName + ": PicoPass 16K(8x2)";
                                break;
                            case 0x1C:
                                cardName = cardName + ": PicoPass 16KS(8x2)";
                                break;

                            case 0x1D:
                                cardName = cardName + ": PicoPass 32KS(16+16)";
                                break;
                            case 0x1E:
                                cardName = cardName + ": PicoPass 32KS(16+8x2)";
                                break;
                            case 0x1F:
                                cardName = cardName + ": PicoPass 32KS(8x2+16)";
                                break;
                            case 0x20:
                                cardName = cardName + ": PicoPass 32KS(8x2+8x2)";
                                break;
                            case 0x21:
                                cardName = cardName + ": LRI64";
                                break;
                            case 0x22:
                                cardName = cardName + ": I.CODE UID";
                                break;
                            case 0x23:
                                cardName = cardName + ": I.CODE EPC";
                                break;
                            case 0x24:
                                cardName = cardName + ": LRI12";
                                break;
                            case 0x25:
                                cardName = cardName + ": LRI128";
                                break;
                            case 0x26:
                                cardName = cardName + ": Mifare Mini";
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
                                    cardName = cardName + ": Mifare Mini";
                                    break;

                            }

                        }

                    }

                    displayOut(3, 0, cardName + " is detected.");

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

                    displayOut(3, 0, "Mifare DESFire is detected.");

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

                    displayOut(3, 0, "ST19XRC8E is detected.");

                }

            }

            //4.4. other cards using ISO 14443 Type A or B
            lATRStr = "";
            sATRStr = "";

            if (lATRStr == "3B8X800150")
            {

                displayOut(3, 0, "ISO 14443B is detected.");
            }

            else
            {

                if (sATRStr == "3B8X8001")
                {

                    displayOut(3, 0, "ISO 14443A is detected.");

                }

            }


        }

    
    }
}