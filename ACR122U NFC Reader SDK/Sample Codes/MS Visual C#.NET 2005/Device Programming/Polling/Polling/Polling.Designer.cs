namespace Polling
{
    partial class Polling
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Polling));
            this.cbOpt7 = new System.Windows.Forms.CheckBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.cbOpt6 = new System.Windows.Forms.CheckBox();
            this.cbOpt5 = new System.Windows.Forms.CheckBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsMsg1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsMsg4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.gbPollInt = new System.Windows.Forms.GroupBox();
            this.opt500 = new System.Windows.Forms.RadioButton();
            this.opt250 = new System.Windows.Forms.RadioButton();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.pollTimer = new System.Windows.Forms.Timer(this.components);
            this.bSetPollOpt = new System.Windows.Forms.Button();
            this.bStartPoll = new System.Windows.Forms.Button();
            this.bGetPollOpt = new System.Windows.Forms.Button();
            this.gbPollOpt = new System.Windows.Forms.GroupBox();
            this.cbOpt4 = new System.Windows.Forms.CheckBox();
            this.cbOpt3 = new System.Windows.Forms.CheckBox();
            this.cbOpt2 = new System.Windows.Forms.CheckBox();
            this.cbOpt1 = new System.Windows.Forms.CheckBox();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.bQuit = new System.Windows.Forms.Button();
            this.statusBar.SuspendLayout();
            this.gbPollInt.SuspendLayout();
            this.gbPollOpt.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbOpt7
            // 
            this.cbOpt7.AutoSize = true;
            this.cbOpt7.Location = new System.Drawing.Point(18, 166);
            this.cbOpt7.Name = "cbOpt7";
            this.cbOpt7.Size = new System.Drawing.Size(148, 17);
            this.cbOpt7.TabIndex = 6;
            this.cbOpt7.Text = "Detect FeliCa 242K Cards";
            this.cbOpt7.UseVisualStyleBackColor = true;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(150, 65);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(123, 23);
            this.bConnect.TabIndex = 47;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // cbOpt6
            // 
            this.cbOpt6.AutoSize = true;
            this.cbOpt6.Location = new System.Drawing.Point(18, 143);
            this.cbOpt6.Name = "cbOpt6";
            this.cbOpt6.Size = new System.Drawing.Size(148, 17);
            this.cbOpt6.TabIndex = 5;
            this.cbOpt6.Text = "Detect FeliCa 212K Cards";
            this.cbOpt6.UseVisualStyleBackColor = true;
            // 
            // cbOpt5
            // 
            this.cbOpt5.AutoSize = true;
            this.cbOpt5.Location = new System.Drawing.Point(18, 120);
            this.cbOpt5.Name = "cbOpt5";
            this.cbOpt5.Size = new System.Drawing.Size(121, 17);
            this.cbOpt5.TabIndex = 4;
            this.cbOpt5.Text = "Detect Topaz Cards";
            this.cbOpt5.UseVisualStyleBackColor = true;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMsg1,
            this.tsMsg2,
            this.tsMsg3,
            this.tsMsg4});
            this.statusBar.Location = new System.Drawing.Point(0, 425);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(614, 22);
            this.statusBar.TabIndex = 50;
            this.statusBar.Text = "StatusStrip1";
            // 
            // tsMsg1
            // 
            this.tsMsg1.AutoSize = false;
            this.tsMsg1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg1.Name = "tsMsg1";
            this.tsMsg1.Size = new System.Drawing.Size(100, 17);
            this.tsMsg1.Text = "Card Type";
            // 
            // tsMsg2
            // 
            this.tsMsg2.AutoSize = false;
            this.tsMsg2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg2.Name = "tsMsg2";
            this.tsMsg2.Size = new System.Drawing.Size(180, 17);
            // 
            // tsMsg3
            // 
            this.tsMsg3.AutoSize = false;
            this.tsMsg3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg3.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg3.Name = "tsMsg3";
            this.tsMsg3.Size = new System.Drawing.Size(100, 17);
            this.tsMsg3.Text = "Card Status";
            // 
            // tsMsg4
            // 
            this.tsMsg4.AutoSize = false;
            this.tsMsg4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsMsg4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.tsMsg4.Name = "tsMsg4";
            this.tsMsg4.Size = new System.Drawing.Size(180, 17);
            this.tsMsg4.Text = " ";
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(279, 6);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(325, 355);
            this.mMsg.TabIndex = 54;
            this.mMsg.Text = "";
            // 
            // gbPollInt
            // 
            this.gbPollInt.Controls.Add(this.opt500);
            this.gbPollInt.Controls.Add(this.opt250);
            this.gbPollInt.Location = new System.Drawing.Point(18, 189);
            this.gbPollInt.Name = "gbPollInt";
            this.gbPollInt.Size = new System.Drawing.Size(89, 65);
            this.gbPollInt.TabIndex = 7;
            this.gbPollInt.TabStop = false;
            this.gbPollInt.Text = "Poll Interval";
            // 
            // opt500
            // 
            this.opt500.AutoSize = true;
            this.opt500.Location = new System.Drawing.Point(9, 42);
            this.opt500.Name = "opt500";
            this.opt500.Size = new System.Drawing.Size(71, 17);
            this.opt500.TabIndex = 3;
            this.opt500.TabStop = true;
            this.opt500.Text = "500 msec";
            this.opt500.UseVisualStyleBackColor = true;
            // 
            // opt250
            // 
            this.opt250.AutoSize = true;
            this.opt250.Location = new System.Drawing.Point(9, 17);
            this.opt250.Name = "opt250";
            this.opt250.Size = new System.Drawing.Size(71, 17);
            this.opt250.TabIndex = 2;
            this.opt250.TabStop = true;
            this.opt250.Text = "250 msec";
            this.opt250.UseVisualStyleBackColor = true;
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(396, 376);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(114, 23);
            this.bReset.TabIndex = 52;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(279, 376);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(111, 23);
            this.bClear.TabIndex = 51;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // pollTimer
            // 
            this.pollTimer.Tick += new System.EventHandler(this.pollTimer_Tick);
            // 
            // bSetPollOpt
            // 
            this.bSetPollOpt.Location = new System.Drawing.Point(124, 229);
            this.bSetPollOpt.Name = "bSetPollOpt";
            this.bSetPollOpt.Size = new System.Drawing.Size(116, 23);
            this.bSetPollOpt.TabIndex = 13;
            this.bSetPollOpt.Text = "Set Polling Option";
            this.bSetPollOpt.UseVisualStyleBackColor = true;
            this.bSetPollOpt.Click += new System.EventHandler(this.bSetPollOpt_Click);
            // 
            // bStartPoll
            // 
            this.bStartPoll.Location = new System.Drawing.Point(18, 376);
            this.bStartPoll.Name = "bStartPoll";
            this.bStartPoll.Size = new System.Drawing.Size(255, 23);
            this.bStartPoll.TabIndex = 49;
            this.bStartPoll.Text = "Start Polling";
            this.bStartPoll.UseVisualStyleBackColor = true;
            this.bStartPoll.Click += new System.EventHandler(this.bStartPoll_Click);
            // 
            // bGetPollOpt
            // 
            this.bGetPollOpt.Location = new System.Drawing.Point(124, 200);
            this.bGetPollOpt.Name = "bGetPollOpt";
            this.bGetPollOpt.Size = new System.Drawing.Size(116, 23);
            this.bGetPollOpt.TabIndex = 12;
            this.bGetPollOpt.Text = "Get Polling Option";
            this.bGetPollOpt.UseVisualStyleBackColor = true;
            this.bGetPollOpt.Click += new System.EventHandler(this.bGetPollOpt_Click);
            // 
            // gbPollOpt
            // 
            this.gbPollOpt.Controls.Add(this.bSetPollOpt);
            this.gbPollOpt.Controls.Add(this.bGetPollOpt);
            this.gbPollOpt.Controls.Add(this.gbPollInt);
            this.gbPollOpt.Controls.Add(this.cbOpt7);
            this.gbPollOpt.Controls.Add(this.cbOpt6);
            this.gbPollOpt.Controls.Add(this.cbOpt5);
            this.gbPollOpt.Controls.Add(this.cbOpt4);
            this.gbPollOpt.Controls.Add(this.cbOpt3);
            this.gbPollOpt.Controls.Add(this.cbOpt2);
            this.gbPollOpt.Controls.Add(this.cbOpt1);
            this.gbPollOpt.Location = new System.Drawing.Point(15, 94);
            this.gbPollOpt.Name = "gbPollOpt";
            this.gbPollOpt.Size = new System.Drawing.Size(258, 270);
            this.gbPollOpt.TabIndex = 48;
            this.gbPollOpt.TabStop = false;
            this.gbPollOpt.Text = "Operating Parameters";
            // 
            // cbOpt4
            // 
            this.cbOpt4.AutoSize = true;
            this.cbOpt4.Location = new System.Drawing.Point(18, 97);
            this.cbOpt4.Name = "cbOpt4";
            this.cbOpt4.Size = new System.Drawing.Size(176, 17);
            this.cbOpt4.TabIndex = 3;
            this.cbOpt4.Text = "Detect ISO14443 Type B Cards";
            this.cbOpt4.UseVisualStyleBackColor = true;
            // 
            // cbOpt3
            // 
            this.cbOpt3.AutoSize = true;
            this.cbOpt3.Location = new System.Drawing.Point(18, 74);
            this.cbOpt3.Name = "cbOpt3";
            this.cbOpt3.Size = new System.Drawing.Size(176, 17);
            this.cbOpt3.TabIndex = 2;
            this.cbOpt3.Text = "Detect ISO14443 Type A Cards";
            this.cbOpt3.UseVisualStyleBackColor = true;
            // 
            // cbOpt2
            // 
            this.cbOpt2.AutoSize = true;
            this.cbOpt2.Location = new System.Drawing.Point(18, 51);
            this.cbOpt2.Name = "cbOpt2";
            this.cbOpt2.Size = new System.Drawing.Size(155, 17);
            this.cbOpt2.TabIndex = 1;
            this.cbOpt2.Text = "Automatic ATS  Generation";
            this.cbOpt2.UseVisualStyleBackColor = true;
            // 
            // cbOpt1
            // 
            this.cbOpt1.AutoSize = true;
            this.cbOpt1.Location = new System.Drawing.Point(18, 28);
            this.cbOpt1.Name = "cbOpt1";
            this.cbOpt1.Size = new System.Drawing.Size(134, 17);
            this.cbOpt1.TabIndex = 0;
            this.cbOpt1.Text = "Automatic PICC Polling";
            this.cbOpt1.UseVisualStyleBackColor = true;
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(150, 36);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(123, 23);
            this.bInit.TabIndex = 46;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 9);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(180, 21);
            this.cbReader.TabIndex = 45;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 44;
            this.Label1.Text = "Select Reader";
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(516, 376);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(88, 23);
            this.bQuit.TabIndex = 53;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // Polling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 447);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bStartPoll);
            this.Controls.Add(this.gbPollOpt);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.bQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Polling";
            this.Text = "Polling Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.gbPollInt.ResumeLayout(false);
            this.gbPollInt.PerformLayout();
            this.gbPollOpt.ResumeLayout(false);
            this.gbPollOpt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox cbOpt7;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.CheckBox cbOpt6;
        internal System.Windows.Forms.CheckBox cbOpt5;
        internal System.Windows.Forms.StatusStrip statusBar;
        internal System.Windows.Forms.ToolStripStatusLabel tsMsg1;
        internal System.Windows.Forms.ToolStripStatusLabel tsMsg2;
        internal System.Windows.Forms.ToolStripStatusLabel tsMsg3;
        internal System.Windows.Forms.ToolStripStatusLabel tsMsg4;
        internal System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.GroupBox gbPollInt;
        internal System.Windows.Forms.RadioButton opt500;
        internal System.Windows.Forms.RadioButton opt250;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.Timer pollTimer;
        internal System.Windows.Forms.Button bSetPollOpt;
        internal System.Windows.Forms.Button bStartPoll;
        internal System.Windows.Forms.Button bGetPollOpt;
        internal System.Windows.Forms.GroupBox gbPollOpt;
        internal System.Windows.Forms.CheckBox cbOpt4;
        internal System.Windows.Forms.CheckBox cbOpt3;
        internal System.Windows.Forms.CheckBox cbOpt2;
        internal System.Windows.Forms.CheckBox cbOpt1;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button bQuit;
    }
}

