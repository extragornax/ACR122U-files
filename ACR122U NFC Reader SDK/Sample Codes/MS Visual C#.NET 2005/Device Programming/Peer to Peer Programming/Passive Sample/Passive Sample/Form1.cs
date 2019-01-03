//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              frmActive.cs
//
//  Description:       This sample program outlines the steps
//                     on how to set an ACR122 NFC reader to its
//                     active mode and sending data
//
//  Author:            Daryl M. Rojas
//
//  Date:              August 5, 2008
//
//  Revision Trail:   (Date/Author/Description)
//
//======================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PassiveSample
{

    public partial class PassiveSample : Form
    {
        public int retCode, hContext, hCard, Protocol, ReaderCount, nBytesRet;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, ATRLen, dwState, dwActProtocol;
        public byte[] ATRVal = new byte[257];
        string sReaderList, data;
        byte[] sReaderGroup;
        ModWinsCard.SCARD_IO_REQUEST ioRequest;

        public PassiveSample()
        {
            InitializeComponent();
        }
        private void InitMenu()
        {
            connActive = false;
            cbReader.Text = "";
            mMsg.Text = "";
            bInit.Enabled = true;
            bConnect.Enabled = false;
            bClear.Enabled = false;
            displayOut(0, 0, "Program ready");
            bActive.Enabled = false;

        }

        private void EnableButtons()
        {

            bInit.Enabled = false;
            bConnect.Enabled = true;
            bClear.Enabled = true;
            bActive.Enabled = true;

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

        private void PassiveSample_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            retCode = ModWinsCard.SCardConnect(hContext, cbReader.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_SHARED,
                                            ModWinsCard.SCARD_PROTOCOL_T1, ref hCard, ref Protocol);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                retCode = ModWinsCard.SCardConnect(hContext, cbReader.SelectedItem.ToString(), ModWinsCard.SCARD_SHARE_DIRECT,
                                            0, ref hCard, ref Protocol);


                if (retCode != ModWinsCard.SCARD_S_SUCCESS)


                    displayOut(1, retCode, "");
                else
                {
                    displayOut(0, 0, "Successful connection to " + cbReader.Text);

                }
            }
            else
            {
                displayOut(0, 0, "Successful connection to " + cbReader.Text);
            }

            GetFirmware();
        
        }

        public int CardControl()
        {

            string tempstr = string.Empty;
            int index = 0;

            for (index = 0; index <= SendLen - 1; index++)
            {
                //tempstr = tempstr + Microsoft.VisualBasic.Right("00" + Conversion.Hex(SendBuff(index)), 2) + " ";

                tempstr = tempstr + " " + string.Format("{0:X2}", SendBuff[index]);
            }

            displayOut(2, 0, tempstr.Trim());

            //retCode = ModWinsCard.SCardControl(hCard, ModWinsCard.IOCTL_CCID_ESCAPE_SCARD_CTL_CODE, SendBuff[0], SendLen, RecvBuff[0], RecvLen, nBytesRet);
            retCode = ModWinsCard.SCardControl(hCard, (uint)ModWinsCard.IOCTL_CCID_ESCAPE_SCARD_CTL_CODE, ref SendBuff[0], SendLen, ref RecvBuff[0], RecvLen, ref nBytesRet);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {

                displayOut(2, retCode, "");
                return retCode;
            }

            tempstr = string.Empty;

            for (index = 0; index <= RecvLen - 1; index++)
            {
                //tempstr = tempstr + Microsoft.VisualBasic.Right("00" + Conversion.Hex(RecvBuff(index)), 2) + " ";
                tempstr = tempstr + " " + string.Format("{0:X2}", RecvBuff[index]);
            }

            //DisplayOut(tempstr, 4);
            displayOut(3, 0, tempstr.Trim());

            return retCode;

        }

        private void GetFirmware()
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

            retCode = CardControl();

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

        private void bActive_Click(object sender, EventArgs e)
        {
            SetPassive();
        }

        public void SetPassive()
        {
            int indx;
            String tempstr = "";

            //Setup passive mode
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x27;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x8C;
            SendBuff[7] = 0x00;
            SendBuff[8] = 0x08;
            SendBuff[9] = 0x00;
            SendBuff[10] = 0x12;
            SendBuff[11] = 0x34;
            SendBuff[12] = 0x56;
            SendBuff[13] = 0x40;
            SendBuff[14] = 0x01;
            SendBuff[15] = 0xFE;
            SendBuff[16] = 0xA2;
            SendBuff[17] = 0xA3;
            SendBuff[18] = 0xA4;
            SendBuff[19] = 0xA5;
            SendBuff[20] = 0xA6;
            SendBuff[21] = 0xA7;
            SendBuff[22] = 0xC0;
            SendBuff[23] = 0xC1;
            SendBuff[24] = 0xC2;
            SendBuff[25] = 0xC3;
            SendBuff[26] = 0xC4;
            SendBuff[27] = 0xC5;
            SendBuff[28] = 0xC6;
            SendBuff[29] = 0xC7;
            SendBuff[30] = 0xFF;
            SendBuff[31] = 0xFF;
            SendBuff[32] = 0xAA;
            SendBuff[33] = 0x99;
            SendBuff[34] = 0x88;
            SendBuff[35] = 0x77;
            SendBuff[36] = 0x66;
            SendBuff[37] = 0x55;
            SendBuff[38] = 0x44;
            SendBuff[39] = 0x33;
            SendBuff[40] = 0x22;
            SendBuff[41] = 0x11;
            SendBuff[42] = 0x00;
            SendBuff[43] = 0x00;

            SendLen = 44;
            RecvLen = 22;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return; 
            }

            for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
            {
                tempstr = tempstr + " " + string.Format("{0:X2}", RecvBuff[indx]);
            }

            if (tempstr.Trim() != "90 00")
            {
                displayOut(2, 0, "Set passive failed!");
                return;
            }



            RecvData();

        }

        public void RecvData()
        {

            byte datalen = 0;
            int index = 0;

            data = string.Empty;

            //Receive first the length of
            //the actual data to be received
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x02;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x86;

            SendLen = 7;
            RecvLen = 6;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            datalen = RecvBuff[3];

            //Send a response with a value of 90 00
            //to the sending device
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x04;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x8E;
            SendBuff[7] = 0x90;
            SendBuff[8] = 0x00;

            SendLen = 9;
            RecvLen = 5;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            //We receive the actual data
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x02;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x86;

            SendLen = 7;
            RecvLen = Convert.ToInt32(datalen) + 5;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            for (index = 3; index <= RecvLen - 3; index++)
            {
                //data = data + Microsoft.VisualBasic.Right(Strings.Chr(RecvBuff(index)), 2);
                data = data + System.Text.ASCIIEncoding.ASCII.GetString(RecvBuff, index, 2);


            }

            //We send the response with a value of 90 00
            //to the sending device
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x04;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x8E;
            SendBuff[7] = 0x90;
            SendBuff[8] = 0x00;

            SendLen = 9;
            RecvLen = 5;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            tbData.Text = data;

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



       
    }
}