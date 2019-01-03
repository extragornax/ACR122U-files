namespace MiFare_Programming
{
    partial class MiFareCardProg
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
            this.bReset = new System.Windows.Forms.Button();
            this.tKey6 = new System.Windows.Forms.TextBox();
            this.tKey5 = new System.Windows.Forms.TextBox();
            this.tKey4 = new System.Windows.Forms.TextBox();
            this.gbValBlk = new System.Windows.Forms.GroupBox();
            this.bValRes = new System.Windows.Forms.Button();
            this.bValRead = new System.Windows.Forms.Button();
            this.bValDec = new System.Windows.Forms.Button();
            this.bValInc = new System.Windows.Forms.Button();
            this.bValStor = new System.Windows.Forms.Button();
            this.tValTar = new System.Windows.Forms.TextBox();
            this.tValSrc = new System.Windows.Forms.TextBox();
            this.tValBlk = new System.Windows.Forms.TextBox();
            this.tValAmt = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.bLoadKey = new System.Windows.Forms.Button();
            this.tKey3 = new System.Windows.Forms.TextBox();
            this.bClear = new System.Windows.Forms.Button();
            this.bQuit = new System.Windows.Forms.Button();
            this.tKey1 = new System.Windows.Forms.TextBox();
            this.tKey2 = new System.Windows.Forms.TextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.bInit = new System.Windows.Forms.Button();
            this.gbLoadKeys = new System.Windows.Forms.GroupBox();
            this.tKeyNum = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.bAuth = new System.Windows.Forms.Button();
            this.rbKType2 = new System.Windows.Forms.RadioButton();
            this.gbAuth = new System.Windows.Forms.GroupBox();
            this.tAuthenKeyNum = new System.Windows.Forms.TextBox();
            this.tBlkNo = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.gbKType = new System.Windows.Forms.GroupBox();
            this.rbKType1 = new System.Windows.Forms.RadioButton();
            this.tBinBlk = new System.Windows.Forms.TextBox();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.tBinData = new System.Windows.Forms.TextBox();
            this.bBinRead = new System.Windows.Forms.Button();
            this.bBinUpd = new System.Windows.Forms.Button();
            this.tBinLen = new System.Windows.Forms.TextBox();
            this.gbBinOps = new System.Windows.Forms.GroupBox();
            this.gbValBlk.SuspendLayout();
            this.gbLoadKeys.SuspendLayout();
            this.gbAuth.SuspendLayout();
            this.gbKType.SuspendLayout();
            this.gbBinOps.SuspendLayout();
            this.SuspendLayout();
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(433, 494);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(113, 23);
            this.bReset.TabIndex = 48;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // tKey6
            // 
            this.tKey6.Location = new System.Drawing.Point(262, 57);
            this.tKey6.MaxLength = 2;
            this.tKey6.Name = "tKey6";
            this.tKey6.Size = new System.Drawing.Size(25, 20);
            this.tKey6.TabIndex = 10;
            // 
            // tKey5
            // 
            this.tKey5.Location = new System.Drawing.Point(231, 57);
            this.tKey5.MaxLength = 2;
            this.tKey5.Name = "tKey5";
            this.tKey5.Size = new System.Drawing.Size(25, 20);
            this.tKey5.TabIndex = 9;
            // 
            // tKey4
            // 
            this.tKey4.Location = new System.Drawing.Point(200, 57);
            this.tKey4.MaxLength = 2;
            this.tKey4.Name = "tKey4";
            this.tKey4.Size = new System.Drawing.Size(25, 20);
            this.tKey4.TabIndex = 8;
            // 
            // gbValBlk
            // 
            this.gbValBlk.Controls.Add(this.bValRes);
            this.gbValBlk.Controls.Add(this.bValRead);
            this.gbValBlk.Controls.Add(this.bValDec);
            this.gbValBlk.Controls.Add(this.bValInc);
            this.gbValBlk.Controls.Add(this.bValStor);
            this.gbValBlk.Controls.Add(this.tValTar);
            this.gbValBlk.Controls.Add(this.tValSrc);
            this.gbValBlk.Controls.Add(this.tValBlk);
            this.gbValBlk.Controls.Add(this.tValAmt);
            this.gbValBlk.Controls.Add(this.Label13);
            this.gbValBlk.Controls.Add(this.Label12);
            this.gbValBlk.Controls.Add(this.Label11);
            this.gbValBlk.Controls.Add(this.Label10);
            this.gbValBlk.Location = new System.Drawing.Point(315, 13);
            this.gbValBlk.Name = "gbValBlk";
            this.gbValBlk.Size = new System.Drawing.Size(344, 168);
            this.gbValBlk.TabIndex = 46;
            this.gbValBlk.TabStop = false;
            this.gbValBlk.Text = "Value Block Functions";
            // 
            // bValRes
            // 
            this.bValRes.Location = new System.Drawing.Point(237, 133);
            this.bValRes.Name = "bValRes";
            this.bValRes.Size = new System.Drawing.Size(101, 23);
            this.bValRes.TabIndex = 27;
            this.bValRes.Text = "Restore Value";
            this.bValRes.UseVisualStyleBackColor = true;
            this.bValRes.Click += new System.EventHandler(this.bValRes_Click);
            // 
            // bValRead
            // 
            this.bValRead.Location = new System.Drawing.Point(237, 104);
            this.bValRead.Name = "bValRead";
            this.bValRead.Size = new System.Drawing.Size(101, 23);
            this.bValRead.TabIndex = 26;
            this.bValRead.Text = "Read Value";
            this.bValRead.UseVisualStyleBackColor = true;
            this.bValRead.Click += new System.EventHandler(this.bValRead_Click);
            // 
            // bValDec
            // 
            this.bValDec.Location = new System.Drawing.Point(237, 76);
            this.bValDec.Name = "bValDec";
            this.bValDec.Size = new System.Drawing.Size(101, 23);
            this.bValDec.TabIndex = 25;
            this.bValDec.Text = "Decrement";
            this.bValDec.UseVisualStyleBackColor = true;
            this.bValDec.Click += new System.EventHandler(this.bValDec_Click);
            // 
            // bValInc
            // 
            this.bValInc.Location = new System.Drawing.Point(237, 50);
            this.bValInc.Name = "bValInc";
            this.bValInc.Size = new System.Drawing.Size(101, 23);
            this.bValInc.TabIndex = 24;
            this.bValInc.Text = "Increment";
            this.bValInc.UseVisualStyleBackColor = true;
            this.bValInc.Click += new System.EventHandler(this.bValInc_Click);
            // 
            // bValStor
            // 
            this.bValStor.Location = new System.Drawing.Point(237, 22);
            this.bValStor.Name = "bValStor";
            this.bValStor.Size = new System.Drawing.Size(101, 23);
            this.bValStor.TabIndex = 23;
            this.bValStor.Text = "Store Value";
            this.bValStor.UseVisualStyleBackColor = true;
            this.bValStor.Click += new System.EventHandler(this.bValStor_Click);
            // 
            // tValTar
            // 
            this.tValTar.Location = new System.Drawing.Point(96, 104);
            this.tValTar.MaxLength = 2;
            this.tValTar.Name = "tValTar";
            this.tValTar.Size = new System.Drawing.Size(33, 20);
            this.tValTar.TabIndex = 22;
            // 
            // tValSrc
            // 
            this.tValSrc.Location = new System.Drawing.Point(96, 79);
            this.tValSrc.MaxLength = 2;
            this.tValSrc.Name = "tValSrc";
            this.tValSrc.Size = new System.Drawing.Size(33, 20);
            this.tValSrc.TabIndex = 21;
            // 
            // tValBlk
            // 
            this.tValBlk.Location = new System.Drawing.Point(96, 53);
            this.tValBlk.MaxLength = 2;
            this.tValBlk.Name = "tValBlk";
            this.tValBlk.Size = new System.Drawing.Size(33, 20);
            this.tValBlk.TabIndex = 20;
            // 
            // tValAmt
            // 
            this.tValAmt.Location = new System.Drawing.Point(96, 24);
            this.tValAmt.Name = "tValAmt";
            this.tValAmt.Size = new System.Drawing.Size(111, 20);
            this.tValAmt.TabIndex = 4;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(17, 111);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(68, 13);
            this.Label13.TabIndex = 3;
            this.Label13.Text = "Target Block";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(17, 82);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(71, 13);
            this.Label12.TabIndex = 2;
            this.Label12.Text = "Source Block";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(17, 53);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(54, 13);
            this.Label11.TabIndex = 1;
            this.Label11.Text = "Block No.";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(17, 27);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(73, 13);
            this.Label10.TabIndex = 0;
            this.Label10.Text = "Value Amount";
            // 
            // bLoadKey
            // 
            this.bLoadKey.Location = new System.Drawing.Point(169, 87);
            this.bLoadKey.Name = "bLoadKey";
            this.bLoadKey.Size = new System.Drawing.Size(118, 23);
            this.bLoadKey.TabIndex = 11;
            this.bLoadKey.Text = "Load Keys";
            this.bLoadKey.UseVisualStyleBackColor = true;
            this.bLoadKey.Click += new System.EventHandler(this.bLoadKey_Click);
            // 
            // tKey3
            // 
            this.tKey3.Location = new System.Drawing.Point(169, 57);
            this.tKey3.MaxLength = 2;
            this.tKey3.Name = "tKey3";
            this.tKey3.Size = new System.Drawing.Size(25, 20);
            this.tKey3.TabIndex = 7;
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(315, 494);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(102, 23);
            this.bClear.TabIndex = 47;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(558, 494);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(101, 23);
            this.bQuit.TabIndex = 49;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // tKey1
            // 
            this.tKey1.Location = new System.Drawing.Point(107, 57);
            this.tKey1.MaxLength = 2;
            this.tKey1.Name = "tKey1";
            this.tKey1.Size = new System.Drawing.Size(25, 20);
            this.tKey1.TabIndex = 6;
            // 
            // tKey2
            // 
            this.tKey2.Location = new System.Drawing.Point(138, 57);
            this.tKey2.MaxLength = 2;
            this.tKey2.Name = "tKey2";
            this.tKey2.Size = new System.Drawing.Size(25, 20);
            this.tKey2.TabIndex = 5;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(191, 75);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(117, 23);
            this.bConnect.TabIndex = 42;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 23);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 13);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Key Store No.";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 53);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(82, 13);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Key Value Input";
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(191, 46);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(117, 23);
            this.bInit.TabIndex = 41;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // gbLoadKeys
            // 
            this.gbLoadKeys.Controls.Add(this.bLoadKey);
            this.gbLoadKeys.Controls.Add(this.tKey6);
            this.gbLoadKeys.Controls.Add(this.tKey5);
            this.gbLoadKeys.Controls.Add(this.tKey4);
            this.gbLoadKeys.Controls.Add(this.tKey3);
            this.gbLoadKeys.Controls.Add(this.tKey1);
            this.gbLoadKeys.Controls.Add(this.tKey2);
            this.gbLoadKeys.Controls.Add(this.tKeyNum);
            this.gbLoadKeys.Controls.Add(this.Label3);
            this.gbLoadKeys.Controls.Add(this.Label2);
            this.gbLoadKeys.Location = new System.Drawing.Point(12, 104);
            this.gbLoadKeys.Name = "gbLoadKeys";
            this.gbLoadKeys.Size = new System.Drawing.Size(296, 120);
            this.gbLoadKeys.TabIndex = 43;
            this.gbLoadKeys.TabStop = false;
            this.gbLoadKeys.Text = "Load Authentication Keys to Device";
            // 
            // tKeyNum
            // 
            this.tKeyNum.Location = new System.Drawing.Point(107, 23);
            this.tKeyNum.MaxLength = 2;
            this.tKeyNum.Name = "tKeyNum";
            this.tKeyNum.Size = new System.Drawing.Size(25, 20);
            this.tKeyNum.TabIndex = 4;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(7, 28);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(88, 13);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Start Block (Dec)";
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 19);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(216, 21);
            this.cbReader.TabIndex = 40;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 39;
            this.Label1.Text = "Select Reader";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(156, 28);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(69, 13);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Length (Dec)";
            // 
            // bAuth
            // 
            this.bAuth.Location = new System.Drawing.Point(169, 111);
            this.bAuth.Name = "bAuth";
            this.bAuth.Size = new System.Drawing.Size(118, 23);
            this.bAuth.TabIndex = 13;
            this.bAuth.Text = "Authenticate";
            this.bAuth.UseVisualStyleBackColor = true;
            this.bAuth.Click += new System.EventHandler(this.bAuth_Click);
            // 
            // rbKType2
            // 
            this.rbKType2.AutoSize = true;
            this.rbKType2.Location = new System.Drawing.Point(16, 53);
            this.rbKType2.Name = "rbKType2";
            this.rbKType2.Size = new System.Drawing.Size(53, 17);
            this.rbKType2.TabIndex = 2;
            this.rbKType2.TabStop = true;
            this.rbKType2.Text = "Key B";
            this.rbKType2.UseVisualStyleBackColor = true;
            // 
            // gbAuth
            // 
            this.gbAuth.Controls.Add(this.bAuth);
            this.gbAuth.Controls.Add(this.tAuthenKeyNum);
            this.gbAuth.Controls.Add(this.tBlkNo);
            this.gbAuth.Controls.Add(this.Label5);
            this.gbAuth.Controls.Add(this.Label4);
            this.gbAuth.Controls.Add(this.gbKType);
            this.gbAuth.Location = new System.Drawing.Point(12, 230);
            this.gbAuth.Name = "gbAuth";
            this.gbAuth.Size = new System.Drawing.Size(297, 146);
            this.gbAuth.TabIndex = 44;
            this.gbAuth.TabStop = false;
            this.gbAuth.Text = "Authentication Function";
            // 
            // tAuthenKeyNum
            // 
            this.tAuthenKeyNum.Location = new System.Drawing.Point(109, 52);
            this.tAuthenKeyNum.MaxLength = 2;
            this.tAuthenKeyNum.Name = "tAuthenKeyNum";
            this.tAuthenKeyNum.Size = new System.Drawing.Size(25, 20);
            this.tAuthenKeyNum.TabIndex = 6;
            // 
            // tBlkNo
            // 
            this.tBlkNo.Location = new System.Drawing.Point(109, 25);
            this.tBlkNo.MaxLength = 3;
            this.tBlkNo.Name = "tBlkNo";
            this.tBlkNo.Size = new System.Drawing.Size(25, 20);
            this.tBlkNo.TabIndex = 5;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(7, 52);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(93, 13);
            this.Label5.TabIndex = 3;
            this.Label5.Text = "Key Store Number";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(9, 28);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(80, 13);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "Block No (Dec)";
            // 
            // gbKType
            // 
            this.gbKType.Controls.Add(this.rbKType2);
            this.gbKType.Controls.Add(this.rbKType1);
            this.gbKType.Location = new System.Drawing.Point(169, 9);
            this.gbKType.Name = "gbKType";
            this.gbKType.Size = new System.Drawing.Size(109, 86);
            this.gbKType.TabIndex = 1;
            this.gbKType.TabStop = false;
            this.gbKType.Text = "Key Type";
            // 
            // rbKType1
            // 
            this.rbKType1.AutoSize = true;
            this.rbKType1.Location = new System.Drawing.Point(16, 19);
            this.rbKType1.Name = "rbKType1";
            this.rbKType1.Size = new System.Drawing.Size(53, 17);
            this.rbKType1.TabIndex = 1;
            this.rbKType1.TabStop = true;
            this.rbKType1.Text = "Key A";
            this.rbKType1.UseVisualStyleBackColor = true;
            // 
            // tBinBlk
            // 
            this.tBinBlk.Location = new System.Drawing.Point(101, 25);
            this.tBinBlk.MaxLength = 2;
            this.tBinBlk.Name = "tBinBlk";
            this.tBinBlk.Size = new System.Drawing.Size(33, 20);
            this.tBinBlk.TabIndex = 14;
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(315, 195);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(344, 293);
            this.mMsg.TabIndex = 50;
            this.mMsg.Text = "";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(9, 62);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(60, 13);
            this.Label9.TabIndex = 16;
            this.Label9.Text = "Data (Text)";
            // 
            // tBinData
            // 
            this.tBinData.Location = new System.Drawing.Point(12, 78);
            this.tBinData.Name = "tBinData";
            this.tBinData.Size = new System.Drawing.Size(252, 20);
            this.tBinData.TabIndex = 17;
            // 
            // bBinRead
            // 
            this.bBinRead.Location = new System.Drawing.Point(27, 106);
            this.bBinRead.Name = "bBinRead";
            this.bBinRead.Size = new System.Drawing.Size(107, 23);
            this.bBinRead.TabIndex = 18;
            this.bBinRead.Text = "Read Block";
            this.bBinRead.UseVisualStyleBackColor = true;
            this.bBinRead.Click += new System.EventHandler(this.bBinRead_Click);
            // 
            // bBinUpd
            // 
            this.bBinUpd.Location = new System.Drawing.Point(140, 106);
            this.bBinUpd.Name = "bBinUpd";
            this.bBinUpd.Size = new System.Drawing.Size(116, 23);
            this.bBinUpd.TabIndex = 19;
            this.bBinUpd.Text = "Update Block";
            this.bBinUpd.UseVisualStyleBackColor = true;
            this.bBinUpd.Click += new System.EventHandler(this.bBinUpd_Click);
            // 
            // tBinLen
            // 
            this.tBinLen.Location = new System.Drawing.Point(231, 25);
            this.tBinLen.MaxLength = 2;
            this.tBinLen.Name = "tBinLen";
            this.tBinLen.Size = new System.Drawing.Size(33, 20);
            this.tBinLen.TabIndex = 15;
            this.tBinLen.TextChanged += new System.EventHandler(this.tBinLen_TextChanged);
            // 
            // gbBinOps
            // 
            this.gbBinOps.Controls.Add(this.bBinUpd);
            this.gbBinOps.Controls.Add(this.bBinRead);
            this.gbBinOps.Controls.Add(this.tBinData);
            this.gbBinOps.Controls.Add(this.Label9);
            this.gbBinOps.Controls.Add(this.tBinLen);
            this.gbBinOps.Controls.Add(this.tBinBlk);
            this.gbBinOps.Controls.Add(this.Label8);
            this.gbBinOps.Controls.Add(this.Label7);
            this.gbBinOps.Location = new System.Drawing.Point(12, 382);
            this.gbBinOps.Name = "gbBinOps";
            this.gbBinOps.Size = new System.Drawing.Size(297, 135);
            this.gbBinOps.TabIndex = 45;
            this.gbBinOps.TabStop = false;
            this.gbBinOps.Text = "Binary Block Functions";
            // 
            // MiFareCardProg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 530);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.gbValBlk);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.gbLoadKeys);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.gbAuth);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.gbBinOps);
            this.Name = "MiFareCardProg";
            this.Text = "MiFare Card Programming";
            this.Load += new System.EventHandler(this.MiFareCardProg_Load);
            this.gbValBlk.ResumeLayout(false);
            this.gbValBlk.PerformLayout();
            this.gbLoadKeys.ResumeLayout(false);
            this.gbLoadKeys.PerformLayout();
            this.gbAuth.ResumeLayout(false);
            this.gbAuth.PerformLayout();
            this.gbKType.ResumeLayout(false);
            this.gbKType.PerformLayout();
            this.gbBinOps.ResumeLayout(false);
            this.gbBinOps.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.TextBox tKey6;
        internal System.Windows.Forms.TextBox tKey5;
        internal System.Windows.Forms.TextBox tKey4;
        internal System.Windows.Forms.GroupBox gbValBlk;
        internal System.Windows.Forms.Button bValRes;
        internal System.Windows.Forms.Button bValRead;
        internal System.Windows.Forms.Button bValDec;
        internal System.Windows.Forms.Button bValInc;
        internal System.Windows.Forms.Button bValStor;
        internal System.Windows.Forms.TextBox tValTar;
        internal System.Windows.Forms.TextBox tValSrc;
        internal System.Windows.Forms.TextBox tValBlk;
        internal System.Windows.Forms.TextBox tValAmt;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Button bLoadKey;
        internal System.Windows.Forms.TextBox tKey3;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.TextBox tKey1;
        internal System.Windows.Forms.TextBox tKey2;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.GroupBox gbLoadKeys;
        internal System.Windows.Forms.TextBox tKeyNum;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Button bAuth;
        internal System.Windows.Forms.RadioButton rbKType2;
        internal System.Windows.Forms.GroupBox gbAuth;
        internal System.Windows.Forms.TextBox tAuthenKeyNum;
        internal System.Windows.Forms.TextBox tBlkNo;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.GroupBox gbKType;
        internal System.Windows.Forms.RadioButton rbKType1;
        internal System.Windows.Forms.TextBox tBinBlk;
        internal System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.TextBox tBinData;
        internal System.Windows.Forms.Button bBinRead;
        internal System.Windows.Forms.Button bBinUpd;
        internal System.Windows.Forms.TextBox tBinLen;
        internal System.Windows.Forms.GroupBox gbBinOps;
    }
}

