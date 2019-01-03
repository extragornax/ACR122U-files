//  Copyright(C):      Advanced Card Systems Ltd
//
//  File:              frmActive.pas
//
//  Description:       This sample program outlines the steps
//                     on how to set an ACR122 NFC reader to its
//                     active mode and sending data
//
//  Author:            Wazer Emmanuel R. Benal
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

namespace ActiveSample
{
    public partial class ActiveSample : Form
    {
        public int retCode, hContext, hCard, Protocol, ReaderCount, nBytesRet;
        public bool connActive = false;
        public byte[] SendBuff = new byte[263];
        public byte[] RecvBuff = new byte[263];
        public int SendLen, RecvLen, ReaderLen, ATRLen, dwState, dwActProtocol;
        public byte[] ATRVal = new byte[257];
        string sReaderList;
        byte[] sReaderGroup;
        ModWinsCard.SCARD_IO_REQUEST ioRequest;

        public ActiveSample()
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

        private void ActiveSample_Load(object sender, EventArgs e)
        {
            InitMenu();
        }

        private void bQuit_Click(object sender, EventArgs e)
        {
            // terminate the application
            retCode = ModWinsCard.SCardReleaseContext(hContext);
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD);
            System.Environment.Exit(0);

        }

        private void bActive_Click(object sender, EventArgs e)
        {
            SetActive();
        }

        public void SetActive()
        {
            int indx;
            String tempstr="";
            ClearBuffers();
            
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x0A;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x56;
            SendBuff[7] = 0x01;
            SendBuff[8] = 0x02;
            SendBuff[9] = 0x01;
            SendBuff[10] = 0x00;
            SendBuff[11] = 0xFF;
            SendBuff[12] = 0xFF;
            SendBuff[13] = 0x00;
            SendBuff[14] = 0x00;
            
            SendLen = 15;
            RecvLen = 21;


            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)            
                return;

      
            for (indx = RecvLen - 2; indx <= RecvLen - 1; indx++)
            {
                tempstr = tempstr + " " + string.Format("{0:X2}", RecvBuff[indx]);
            }

            if (tempstr.Trim() != "90 00")
            {
                displayOut(2, 0, "Set active failed!");
                return;
            }

            sending();
        
        }

        public int CardControl()
        {

            string tempstr = "";
            int index = 0;

            for (index = 0; index <= SendLen - 1; index++)
            {               

                tempstr = tempstr + " " + string.Format("{0:X2}", SendBuff[index]);
            }

            displayOut(2,0,tempstr);

            
            retCode = ModWinsCard.SCardControl(hCard, (int)ModWinsCard.IOCTL_CCID_ESCAPE_SCARD_CTL_CODE, ref SendBuff[0], SendLen, ref RecvBuff[0], RecvLen, ref nBytesRet);

            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                
                displayOut(2, retCode, "");
                return retCode;
            }

            tempstr = "";

            for (index = 0; index <= RecvLen - 1; index++)
            {
               
                tempstr = tempstr + " " + string.Format("{0:X2}", RecvBuff[index]);
            }

            //DisplayOut(tempstr, 4);
            displayOut(3, 0, tempstr);

            return retCode;

        }

        public void sending()
        {
            char[] tempdata = new char[513];
            byte[] tmpbyte;
            byte tmpLong;
            string data = string.Empty;
            int datalen;
            int index = 0;

            //Transfer string data to a char array
            //and determine its length
            data = tbData.Text;
            datalen = data.Length;

            for (index = 0; index <= datalen - 1; index++)
            {
          
                tempdata[index] = data[index];
            
            }

            if (tbData.Text == "" | !byte.TryParse(tbData.Text.Trim(), System.Globalization.NumberStyles.HexNumber, null, out tmpLong))
            {
                tbData.Focus();
                tbData.Text = "";
                return;
            }


            //Send the length of the data first
            //so that the receiving device would
            //know how long the data would be
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;
            SendBuff[4] = 0x01;
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x40;
            SendBuff[7] = 0x01;       
            SendBuff[8] = (byte)datalen;

            //the length of the data

            SendLen = 9;
            RecvLen = 7;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                return;
            }

            //Send actual data
            ClearBuffers();
            SendBuff[0] = 0xFF;
            SendBuff[1] = 0x00;
            SendBuff[2] = 0x00;
            SendBuff[3] = 0x00;     
            SendBuff[4] = (byte)(datalen);
            SendBuff[5] = 0xD4;
            SendBuff[6] = 0x40;
            SendBuff[7] = 0x01;

            for (index = 0; index <= datalen - 1 ; index++)
            {
                SendBuff[index + 8] = (byte)((int)(tempdata[index]));

                
            }

            SendLen = datalen + 8;
            RecvLen = 7;

            retCode = CardControl();
            if (retCode != ModWinsCard.SCARD_S_SUCCESS)
            {
                displayOut(2,retCode,"");
                return;
            }
        }
    

    
    //////////////////////////////////////////////////////////////////////
    }

}