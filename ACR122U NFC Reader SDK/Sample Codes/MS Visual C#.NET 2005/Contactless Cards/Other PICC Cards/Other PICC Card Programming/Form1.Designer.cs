namespace Other_PICC_Card_Programming
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbSendApdu = new System.Windows.Forms.GroupBox();
            this.tData = new System.Windows.Forms.TextBox();
            this.bSend = new System.Windows.Forms.Button();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.tINS = new System.Windows.Forms.TextBox();
            this.tP1 = new System.Windows.Forms.TextBox();
            this.tP2 = new System.Windows.Forms.TextBox();
            this.tLc = new System.Windows.Forms.TextBox();
            this.tLe = new System.Windows.Forms.TextBox();
            this.tCLA = new System.Windows.Forms.TextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.gbGetData = new System.Windows.Forms.GroupBox();
            this.bGetData = new System.Windows.Forms.Button();
            this.cbIso14443A = new System.Windows.Forms.CheckBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.gbSendApdu.SuspendLayout();
            this.gbGetData.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSendApdu
            // 
            this.gbSendApdu.Controls.Add(this.tData);
            this.gbSendApdu.Controls.Add(this.bSend);
            this.gbSendApdu.Controls.Add(this.Label8);
            this.gbSendApdu.Controls.Add(this.Label7);
            this.gbSendApdu.Controls.Add(this.Label6);
            this.gbSendApdu.Controls.Add(this.Label5);
            this.gbSendApdu.Controls.Add(this.Label4);
            this.gbSendApdu.Controls.Add(this.Label3);
            this.gbSendApdu.Controls.Add(this.Label2);
            this.gbSendApdu.Controls.Add(this.tINS);
            this.gbSendApdu.Controls.Add(this.tP1);
            this.gbSendApdu.Controls.Add(this.tP2);
            this.gbSendApdu.Controls.Add(this.tLc);
            this.gbSendApdu.Controls.Add(this.tLe);
            this.gbSendApdu.Controls.Add(this.tCLA);
            this.gbSendApdu.Location = new System.Drawing.Point(15, 174);
            this.gbSendApdu.Name = "gbSendApdu";
            this.gbSendApdu.Size = new System.Drawing.Size(299, 238);
            this.gbSendApdu.TabIndex = 45;
            this.gbSendApdu.TabStop = false;
            this.gbSendApdu.Text = "Send Card Command";
            // 
            // tData
            // 
            this.tData.Location = new System.Drawing.Point(12, 101);
            this.tData.Multiline = true;
            this.tData.Name = "tData";
            this.tData.Size = new System.Drawing.Size(265, 94);
            this.tData.TabIndex = 13;
            // 
            // bSend
            // 
            this.bSend.Location = new System.Drawing.Point(152, 202);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(125, 23);
            this.bSend.TabIndex = 11;
            this.bSend.Text = "Send Card Command";
            this.bSend.UseVisualStyleBackColor = true;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(9, 81);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(42, 13);
            this.Label8.TabIndex = 12;
            this.Label8.Text = "Data In";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(199, 25);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(19, 13);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "Le";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(165, 25);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(19, 13);
            this.Label6.TabIndex = 10;
            this.Label6.Text = "Lc";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(123, 25);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(20, 13);
            this.Label5.TabIndex = 9;
            this.Label5.Text = "P2";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(88, 25);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(20, 13);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "P1";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(57, 25);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(25, 13);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "INS";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 25);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(27, 13);
            this.Label2.TabIndex = 6;
            this.Label2.Text = "CLA";
            // 
            // tINS
            // 
            this.tINS.Location = new System.Drawing.Point(50, 43);
            this.tINS.MaxLength = 2;
            this.tINS.Name = "tINS";
            this.tINS.Size = new System.Drawing.Size(32, 20);
            this.tINS.TabIndex = 6;
            // 
            // tP1
            // 
            this.tP1.Location = new System.Drawing.Point(88, 43);
            this.tP1.MaxLength = 2;
            this.tP1.Name = "tP1";
            this.tP1.Size = new System.Drawing.Size(32, 20);
            this.tP1.TabIndex = 7;
            // 
            // tP2
            // 
            this.tP2.Location = new System.Drawing.Point(126, 43);
            this.tP2.MaxLength = 2;
            this.tP2.Name = "tP2";
            this.tP2.Size = new System.Drawing.Size(32, 20);
            this.tP2.TabIndex = 8;
            // 
            // tLc
            // 
            this.tLc.Location = new System.Drawing.Point(164, 43);
            this.tLc.MaxLength = 2;
            this.tLc.Name = "tLc";
            this.tLc.Size = new System.Drawing.Size(32, 20);
            this.tLc.TabIndex = 9;
            // 
            // tLe
            // 
            this.tLe.Location = new System.Drawing.Point(202, 43);
            this.tLe.MaxLength = 2;
            this.tLe.Name = "tLe";
            this.tLe.Size = new System.Drawing.Size(32, 20);
            this.tLe.TabIndex = 10;
            // 
            // tCLA
            // 
            this.tCLA.Location = new System.Drawing.Point(12, 43);
            this.tCLA.MaxLength = 2;
            this.tCLA.Name = "tCLA";
            this.tCLA.Size = new System.Drawing.Size(32, 20);
            this.tCLA.TabIndex = 5;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(183, 70);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(131, 23);
            this.bConnect.TabIndex = 43;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(183, 41);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(131, 23);
            this.bInit.TabIndex = 42;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 14);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(221, 21);
            this.cbReader.TabIndex = 41;
            // 
            // gbGetData
            // 
            this.gbGetData.Controls.Add(this.bGetData);
            this.gbGetData.Controls.Add(this.cbIso14443A);
            this.gbGetData.Location = new System.Drawing.Point(15, 99);
            this.gbGetData.Name = "gbGetData";
            this.gbGetData.Size = new System.Drawing.Size(299, 69);
            this.gbGetData.TabIndex = 44;
            this.gbGetData.TabStop = false;
            this.gbGetData.Text = "Get Data Function";
            // 
            // bGetData
            // 
            this.bGetData.Location = new System.Drawing.Point(152, 25);
            this.bGetData.Name = "bGetData";
            this.bGetData.Size = new System.Drawing.Size(125, 23);
            this.bGetData.TabIndex = 4;
            this.bGetData.Text = "Get Data";
            this.bGetData.UseVisualStyleBackColor = true;
            this.bGetData.Click += new System.EventHandler(this.bGetData_Click);
            // 
            // cbIso14443A
            // 
            this.cbIso14443A.AutoSize = true;
            this.cbIso14443A.Location = new System.Drawing.Point(15, 29);
            this.cbIso14443A.Name = "cbIso14443A";
            this.cbIso14443A.Size = new System.Drawing.Size(105, 17);
            this.cbIso14443A.TabIndex = 0;
            this.cbIso14443A.Text = "Iso 14443A Card";
            this.cbIso14443A.UseVisualStyleBackColor = true;
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(527, 386);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(99, 23);
            this.bQuit.TabIndex = 48;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 17);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 40;
            this.Label1.Text = "Select Reader";
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(320, 13);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(306, 367);
            this.mMsg.TabIndex = 49;
            this.mMsg.Text = "";
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(425, 386);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(99, 23);
            this.bReset.TabIndex = 47;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(320, 386);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(99, 23);
            this.bClear.TabIndex = 46;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 425);
            this.Controls.Add(this.gbSendApdu);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.gbGetData);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbSendApdu.ResumeLayout(false);
            this.gbSendApdu.PerformLayout();
            this.gbGetData.ResumeLayout(false);
            this.gbGetData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbSendApdu;
        internal System.Windows.Forms.TextBox tData;
        internal System.Windows.Forms.Button bSend;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox tINS;
        internal System.Windows.Forms.TextBox tP1;
        internal System.Windows.Forms.TextBox tP2;
        internal System.Windows.Forms.TextBox tLc;
        internal System.Windows.Forms.TextBox tLe;
        internal System.Windows.Forms.TextBox tCLA;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.GroupBox gbGetData;
        internal System.Windows.Forms.Button bGetData;
        internal System.Windows.Forms.CheckBox cbIso14443A;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
    }
}

