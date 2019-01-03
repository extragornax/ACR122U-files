/*=========================================================================================
'  Copyright(C):    Advanced Card Systems Ltd 
' 
'  Description:     This sample program outlines the steps on how to
'                    set the LED?Buzzer and antenna of the ACR122 NFC reader
'  
'  File   :         DevProg.cs    
'
'  Author :         Daryl M. Rojas
'
'  Module :         ModWinscard.cs
'   
'  Date   :         June 28, 2008
'
'  Revision Trail:  (Date/Author/Description) 
'
'=========================================================================================*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeviProg
{
    public partial class DeviceProgramming : Form
    {
        public int retCode, hContext, hCard, Protocol, ReaderCount;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, ATRLen, dwState, dwActProtocol;
        public byte[] ATRVal = new byte[257];
        string sReaderList;
        byte[] sReaderGroup;
        ModWinsCard.SCARD_IO_REQUEST ioRequest;
        
        public DeviceProgramming()
        {
            InitializeComponent();
        }

        private void DeviceProgramming_Load(object sender, EventArgs e)
        {
            InitMenu();
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
            bInit.Enabled = true;
            bConnect.Enabled = false;
            bClear.Enabled = false;
            bStatus.Enabled = false;
            gbAntenna.Enabled = false;
            gbRed.Enabled = false;
            gbGreen.Enabled = false;
            gbBlinkDuration.Enabled = false;
            bSetLED.Enabled = false;
            bGetFW.Enabled = false;
            displayOut(0, 0, "Program ready");
        
        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;            
            bClear.Enabled = true;

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

                displayOut(1, retCode, "");
            else
            {
                displayOut(0, 0, "Successful connection to " + cbReader.Text);

            }
            connActive = true;
            bGetFW.Enabled = true;
            bStatus.Enabled = true;
            gbAntenna.Enabled = true;
            gbRed.Enabled = true;
            gbGreen.Enabled = true;
            gbBlinkDuration.Enabled = true;
            bSetLED.Enabled = true;
            bGetFW.Enabled = true;
            rbAntOn.Checked = true;
            rbRedFinalOff.Checked = true;
            rbRedStateMaskOff.Checked = true;
            rbRedInitOff.Checked = true;
            rbRedBlinkMaskOff.Checked = true;
            rbGreenFinalOff.Checked = true;
            rbGreenStateMaskOff.Checked = true;
            rbGreenInitOff.Checked = true;
            rbGreenBlinkMaskOff.Checked = true;
            rbLinkToBuzzOpt1.Checked = true;
            tT1.Text = "00";
            tT2.Text = "00";
            tRepeat.Text = "01";
        

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

        private void bGetFW_Click(object sender, EventArgs e)
        {

            // Get the firmaware version of the reader
            string tmpStr;
            int intIndx;

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x48;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x00;
            SendLen = 5;
            RecvLen = 10;

            retCode = Transmit();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }


            // Interpret firmware data
            tmpStr = "Firmware Version: ";

            for (intIndx = 0; intIndx <= RecvLen; intIndx++)
            {

                if (RecvBuff[intIndx] != 0)
                {
                
                    tmpStr = tmpStr + System.Text.ASCIIEncoding.ASCII.GetString(RecvBuff, 0, 19);                                        
                }

            }

            displayOut(3, 0, tmpStr);
        }

        private int Transmit()
        {

            string tmpStr;
            int indx;

            //Display Apdu In
            tmpStr = "";

            for (indx = 0; indx <= SendLen - 1; indx++)
            {
                
                tmpStr = tmpStr + " " + string.Format("{0:X2}", SendBuff[indx]);

            }

            displayOut(2, 0, tmpStr);

            ioRequest.dwProtocol = Protocol;
            ioRequest.cbPciLength = 8;

            RecvLen = 262;

            //Issue SCardTransmit           
            retCode = ModWinsCard.SCardTransmit(hCard, ref ioRequest, ref SendBuff[0], SendLen, ref ioRequest, ref RecvBuff[0], ref RecvLen);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(1, retCode, "");
            }

            else
            {

                tmpStr = "";

                for (indx = 0; indx <= (RecvLen - 1); indx++)
                {
                   
                    tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]) + " ";

                }

                displayOut(3, 0,tmpStr);

            }
            return retCode;


        }

        private void bSetAntenna_Click(object sender, EventArgs e)
        {
            //set the antenna options
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x04;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x32;
            SendBuff[7] = 0x01;

            if (rbAntOn.Checked == true)
            {

                SendBuff[8] = 0x01;
            }

            else
            {

                SendBuff[8] = 0x00;

            }

            SendLen = 9;

            retCode = Transmit();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

        }

        private void bSetLED_Click(object sender, EventArgs e)
        {

            // Set the LED/Buzzer Settings
            string tmpStr;
            long intIndx;
            byte tmpLong;

            // Validate input
            if ((tT1.Text == "" | !byte.TryParse(tT1.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tT1.Focus();
                tT1.Text = "";
                return; 

            }

            if ((tT2.Text == "" | !byte.TryParse(tT2.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tT2.Focus();
                tT2.Text = "";
                return; 

            }

            if ((tRepeat.Text == "" | !byte.TryParse(tRepeat.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong)))
            {

                tRepeat.Focus();
                tRepeat.Text = "";
                return; 

            }

            if (tT1.Text == "")
            {

                tT1.Focus();
                return; 

            }


            if (tT2.Text == "")
            {

                tT2.Focus();
                return; 

            }

            if (tRepeat.Text == "")
            {

                tRepeat.Focus();
                return; 

            }

            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x40;
            SendBuff[3] = 0x00;

            if (rbRedFinalOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x01);

            }

            if (rbGreenFinalOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x02);

            }

            if (rbRedStateMaskOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x04);

            }

            if (rbGreenBlinkMaskOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x08);

            }

            if (rbRedInitOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x10);

            }

            if (rbGreenInitOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x20);

            }

            if (rbRedBlinkMaskOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x40);

            }

            if (rbGreenBlinkMaskOn.Checked == true)
            {

                SendBuff[3] = (byte)(SendBuff[3] | 0x80);

            }

            SendBuff[4] = 0x40;          
            SendBuff[5] = byte.Parse(tT1.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[6] = byte.Parse(tT2.Text, System.Globalization.NumberStyles.HexNumber);
            SendBuff[7] = byte.Parse(tRepeat.Text, System.Globalization.NumberStyles.HexNumber);

            if (rbLinkToBuzzOpt1.Checked == true)
            {

                SendBuff[8] = 0x00;

            }

            if (rbLinkToBuzzOpt2.Checked == true)
            {

                SendBuff[8] = 0x01;

            }

            if (rbLinkToBuzzOpt3.Checked == true)
            {

                SendBuff[8] = 0x02;

            }

            if (rbLinkToBuzzOpt4.Checked == true)
            {

                SendBuff[8] = 0x03;

            }

            SendLen = 9;
            RecvLen = 2;

            retCode = Transmit();

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                return; 

            }

        }

        private void bStatus_Click(object sender, EventArgs e)
        {
            String tmpStr;
            long indx;
        
        ClearBuffers();
        SendBuff[0] = 0xFF;
        SendBuff[1] = 0x00;
        SendBuff[2] = 0x00;
        SendBuff[3] = 0x00;
        SendBuff[4] = 0x02;
        SendBuff[5] = 0xD4;
        SendBuff[6] = 0x04;
        SendLen = 7;
        RecvLen = 12;

        retCode = Transmit();

        if (retCode != ModWinsCard.SCARD_S_SUCCESS)

            return;

        tmpStr = "";


        for (indx = 0; indx <= (RecvLen - 1); indx++)
        {

            tmpStr = tmpStr + string.Format("{0:X2}", RecvBuff[indx]);

        }

        if (tmpStr == "D505280000809000")
        {

            // No Tag is in the field
            displayOut(1, 0, "No Tag is in the field: " + string.Format("{0:X2}", RecvBuff[0]) + " " + string.Format("{0:X2}", RecvBuff[1]) + " ");

        }
        else
        {


            // Interpret data
            // error code
            displayOut(3, 0, "Error Code: " + string.Format("{0:X2}", RecvBuff[2]) + " ");

            // Field indicates if an external RF field is present and detected
            //(Field=0x01 or not (Field 0x00)
            if (RecvBuff[3] == 0x01)
                displayOut(3, 0, "External RF field is Present and detected: " + string.Format("{0:X2}", RecvBuff[3]) + " ");
            else
                displayOut(3, 0, "External RF field is NOT Present and NOT detected: " + string.Format("{0:X2}", RecvBuff[3]) + " ");


            //Number of targets currently controlled by the PN532 acting as initiator.The default value is 1
            displayOut(3, 0, "Number of Target: " + string.Format("{0:X2}", RecvBuff[4]) + " ");

            //Logical number
            displayOut(3, 0, "Number of Target: " + string.Format("{0:X2}", RecvBuff[5]) + " ");
            //Bit rate in reception
            switch (RecvBuff[6])
            {

                case 0x00: displayOut(3, 0, "Bit Rate in Reception: " + string.Format("{0:X2}", RecvBuff[6]) + " " + " (106 kbps)"); break;
                case 0x01: displayOut(3, 0, "Bit Rate in Reception: " + string.Format("{0:X2}", RecvBuff[6]) + " " + " (212 kbps)"); break;
                case 0x02: displayOut(3, 0, "Bit Rate in Reception: " + string.Format("{0:X2}", RecvBuff[6]) + " " + " (424 kbps)"); break;

            }

            //Bit rate in transmission
            switch (RecvBuff[7])
            {

                case 0x00: displayOut(3, 0, "Bit Rate in Transmission: " + string.Format("{0:X2}", RecvBuff[7]) + " " + " (106 kbps)"); break;
                case 0x01: displayOut(3, 0, "Bit Rate in Transmission: " + string.Format("{0:X2}", RecvBuff[7]) + " " + " (212 kbps)"); break;
                case 0x02: displayOut(3, 0, "Bit Rate in Transmission: " + string.Format("{0:X2}", RecvBuff[7]) + " " + " (424 kbps)"); break;

            }

            switch (RecvBuff[8])
            {

                case 0x00: displayOut(3, 0, "Modulation Type: " + string.Format("{0:X2}", RecvBuff[8]) + " " + " (ISO14443 or Mifare)"); break;
                case 0x01: displayOut(3, 0, "Modulation Type: " + string.Format("{0:X2}", RecvBuff[8]) + " " + " (Active mode)"); break;
                case 0x02: displayOut(3, 0, "Modulation Type: " + string.Format("{0:X2}", RecvBuff[8]) + " " + " (Innovision Jewel tag)"); break;
                case 0x10: displayOut(3, 0, "Modulation Type: " + string.Format("{0:X2}", RecvBuff[8]) + " " + " (Felica)"); break;

            }

        }

        }
       
    }
}