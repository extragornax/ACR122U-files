namespace Get_ATR
{
    partial class GetATR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetATR));
            this.bQuit = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bGetAtr = new System.Windows.Forms.Button();
            this.bConnect = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(166, 272);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(105, 23);
            this.bQuit.TabIndex = 16;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(164, 244);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(107, 23);
            this.bReset.TabIndex = 15;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(164, 215);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(107, 23);
            this.bClear.TabIndex = 14;
            this.bClear.Text = "Clear Screen";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bGetAtr
            // 
            this.bGetAtr.Location = new System.Drawing.Point(164, 113);
            this.bGetAtr.Name = "bGetAtr";
            this.bGetAtr.Size = new System.Drawing.Size(107, 23);
            this.bGetAtr.TabIndex = 13;
            this.bGetAtr.Text = "Get ATR";
            this.bGetAtr.UseVisualStyleBackColor = true;
            this.bGetAtr.Click += new System.EventHandler(this.bGetAtr_Click);
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(162, 83);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(109, 23);
            this.bConnect.TabIndex = 12;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(161, 53);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(110, 23);
            this.bInit.TabIndex = 11;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(93, 20);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(178, 21);
            this.cbReader.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select Reader";
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(290, 19);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(374, 276);
            this.mMsg.TabIndex = 17;
            this.mMsg.Text = "";
            // 
            // GetATR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 317);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.bQuit);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bGetAtr);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GetATR";
            this.Text = "ACR 122 Get ATR";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bQuit;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bGetAtr;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.ComboBox cbReader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox mMsg;
    }
}

