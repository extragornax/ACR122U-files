namespace ActiveSample
{
    partial class ActiveSample
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
            this.bQuit = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.SendGroup = new System.Windows.Forms.GroupBox();
            this.bActive = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tbData = new System.Windows.Forms.TextBox();
            this.SendGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(144, 348);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(121, 27);
            this.bQuit.TabIndex = 29;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(144, 315);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(121, 27);
            this.bReset.TabIndex = 28;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(144, 282);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(121, 27);
            this.bClear.TabIndex = 27;
            this.bClear.Text = "Clear Output";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // SendGroup
            // 
            this.SendGroup.Controls.Add(this.tbData);
            this.SendGroup.Location = new System.Drawing.Point(9, 136);
            this.SendGroup.Name = "SendGroup";
            this.SendGroup.Size = new System.Drawing.Size(256, 140);
            this.SendGroup.TabIndex = 26;
            this.SendGroup.TabStop = false;
            this.SendGroup.Text = "Send Data";
            // 
            // bActive
            // 
            this.bActive.Location = new System.Drawing.Point(87, 103);
            this.bActive.Name = "bActive";
            this.bActive.Size = new System.Drawing.Size(178, 27);
            this.bActive.TabIndex = 25;
            this.bActive.Text = "Set Active Mode and Send Data";
            this.bActive.UseVisualStyleBackColor = true;
            this.bActive.Click += new System.EventHandler(this.bActive_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(87, 37);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(178, 27);
            this.bInit.TabIndex = 23;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(271, 10);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(357, 366);
            this.mMsg.TabIndex = 22;
            this.mMsg.Text = "";
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(87, 70);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(178, 27);
            this.bConnect.TabIndex = 24;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(87, 10);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(178, 21);
            this.cbReader.TabIndex = 21;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 13);
            this.Label1.TabIndex = 20;
            this.Label1.Text = "Select Reader";
            // 
            // tbData
            // 
            this.tbData.Location = new System.Drawing.Point(6, 19);
            this.tbData.Multiline = true;
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(244, 115);
            this.tbData.TabIndex = 1;
            // 
            // ActiveSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 386);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.SendGroup);
            this.Controls.Add(this.bActive);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.Label1);
            this.Name = "ActiveSample";
            this.Text = "Active Device Sample";
            this.Load += new System.EventHandler(this.ActiveSample_Load);
            this.SendGroup.ResumeLayout(false);
            this.SendGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.Button bReset;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.GroupBox SendGroup;
        internal System.Windows.Forms.Button bActive;
        internal System.Windows.Forms.Button bInit;
        internal System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.ComboBox cbReader;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox tbData;
    }
}

