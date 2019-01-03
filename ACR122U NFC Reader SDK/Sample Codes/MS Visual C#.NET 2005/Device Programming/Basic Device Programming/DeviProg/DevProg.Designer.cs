namespace DeviProg
{
    partial class DeviceProgramming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceProgramming));
            this.bSetLED = new System.Windows.Forms.Button();
            this.gbLinktoBuzz = new System.Windows.Forms.GroupBox();
            this.rbLinkToBuzzOpt4 = new System.Windows.Forms.RadioButton();
            this.rbLinkToBuzzOpt3 = new System.Windows.Forms.RadioButton();
            this.rbLinkToBuzzOpt2 = new System.Windows.Forms.RadioButton();
            this.rbLinkToBuzzOpt1 = new System.Windows.Forms.RadioButton();
            this.gbBlinkDuration = new System.Windows.Forms.GroupBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.tRepeat = new System.Windows.Forms.TextBox();
            this.gbT2 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.tT2 = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.gbT1 = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.tT1 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.rbGreenFinalOff = new System.Windows.Forms.RadioButton();
            this.rbGreenStateMaskOn = new System.Windows.Forms.RadioButton();
            this.rbGreenStateMaskOff = new System.Windows.Forms.RadioButton();
            this.gbGreenFinal = new System.Windows.Forms.GroupBox();
            this.rbGreenFinalOn = new System.Windows.Forms.RadioButton();
            this.bConnect = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.mMsg = new System.Windows.Forms.RichTextBox();
            this.gbGreenStateMask = new System.Windows.Forms.GroupBox();
            this.rbGreenInitOn = new System.Windows.Forms.RadioButton();
            this.rbGreenInitOff = new System.Windows.Forms.RadioButton();
            this.rbAntOn = new System.Windows.Forms.RadioButton();
            this.gbRed = new System.Windows.Forms.GroupBox();
            this.gbRedBlinkMask = new System.Windows.Forms.GroupBox();
            this.rbRedBlinkMaskOn = new System.Windows.Forms.RadioButton();
            this.rbRedBlinkMaskOff = new System.Windows.Forms.RadioButton();
            this.gbRedInit = new System.Windows.Forms.GroupBox();
            this.rbRedInitOn = new System.Windows.Forms.RadioButton();
            this.rbRedInitOff = new System.Windows.Forms.RadioButton();
            this.gbRedStateMask = new System.Windows.Forms.GroupBox();
            this.rbRedStateMaskOn = new System.Windows.Forms.RadioButton();
            this.rbRedStateMaskOff = new System.Windows.Forms.RadioButton();
            this.gbRedFinal = new System.Windows.Forms.GroupBox();
            this.rbRedFinalOn = new System.Windows.Forms.RadioButton();
            this.rbRedFinalOff = new System.Windows.Forms.RadioButton();
            this.bSetAntenna = new System.Windows.Forms.Button();
            this.gbAntenna = new System.Windows.Forms.GroupBox();
            this.rbAntOff = new System.Windows.Forms.RadioButton();
            this.bGetFW = new System.Windows.Forms.Button();
            this.bInit = new System.Windows.Forms.Button();
            this.cbReader = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbGreenBlinkMask = new System.Windows.Forms.GroupBox();
            this.rbGreenBlinkMaskOn = new System.Windows.Forms.RadioButton();
            this.rbGreenBlinkMaskOff = new System.Windows.Forms.RadioButton();
            this.gbGreenInit = new System.Windows.Forms.GroupBox();
            this.gbGreen = new System.Windows.Forms.GroupBox();
            this.bQuit = new System.Windows.Forms.Button();
            this.bStatus = new System.Windows.Forms.Button();
            this.gbLinktoBuzz.SuspendLayout();
            this.gbBlinkDuration.SuspendLayout();
            this.gbT2.SuspendLayout();
            this.gbT1.SuspendLayout();
            this.gbGreenFinal.SuspendLayout();
            this.gbGreenStateMask.SuspendLayout();
            this.gbRed.SuspendLayout();
            this.gbRedBlinkMask.SuspendLayout();
            this.gbRedInit.SuspendLayout();
            this.gbRedStateMask.SuspendLayout();
            this.gbRedFinal.SuspendLayout();
            this.gbAntenna.SuspendLayout();
            this.gbGreenBlinkMask.SuspendLayout();
            this.gbGreenInit.SuspendLayout();
            this.gbGreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // bSetLED
            // 
            this.bSetLED.Location = new System.Drawing.Point(13, 359);
            this.bSetLED.Name = "bSetLED";
            this.bSetLED.Size = new System.Drawing.Size(215, 32);
            this.bSetLED.TabIndex = 23;
            this.bSetLED.Text = "Set Bi-Color LED/Buzzer Control";
            this.bSetLED.UseVisualStyleBackColor = true;
            this.bSetLED.Click += new System.EventHandler(this.bSetLED_Click);
            // 
            // gbLinktoBuzz
            // 
            this.gbLinktoBuzz.Controls.Add(this.rbLinkToBuzzOpt4);
            this.gbLinktoBuzz.Controls.Add(this.rbLinkToBuzzOpt3);
            this.gbLinktoBuzz.Controls.Add(this.rbLinkToBuzzOpt2);
            this.gbLinktoBuzz.Controls.Add(this.rbLinkToBuzzOpt1);
            this.gbLinktoBuzz.Location = new System.Drawing.Point(13, 191);
            this.gbLinktoBuzz.Name = "gbLinktoBuzz";
            this.gbLinktoBuzz.Size = new System.Drawing.Size(215, 138);
            this.gbLinktoBuzz.TabIndex = 4;
            this.gbLinktoBuzz.TabStop = false;
            this.gbLinktoBuzz.Text = "Link to Buzzer";
            // 
            // rbLinkToBuzzOpt4
            // 
            this.rbLinkToBuzzOpt4.AutoSize = true;
            this.rbLinkToBuzzOpt4.Location = new System.Drawing.Point(22, 100);
            this.rbLinkToBuzzOpt4.Name = "rbLinkToBuzzOpt4";
            this.rbLinkToBuzzOpt4.Size = new System.Drawing.Size(118, 17);
            this.rbLinkToBuzzOpt4.TabIndex = 3;
            this.rbLinkToBuzzOpt4.TabStop = true;
            this.rbLinkToBuzzOpt4.Text = "T1 and T2 Duration";
            this.rbLinkToBuzzOpt4.UseVisualStyleBackColor = true;
            // 
            // rbLinkToBuzzOpt3
            // 
            this.rbLinkToBuzzOpt3.AutoSize = true;
            this.rbLinkToBuzzOpt3.Location = new System.Drawing.Point(22, 77);
            this.rbLinkToBuzzOpt3.Name = "rbLinkToBuzzOpt3";
            this.rbLinkToBuzzOpt3.Size = new System.Drawing.Size(81, 17);
            this.rbLinkToBuzzOpt3.TabIndex = 2;
            this.rbLinkToBuzzOpt3.TabStop = true;
            this.rbLinkToBuzzOpt3.Text = "T2 Duration";
            this.rbLinkToBuzzOpt3.UseVisualStyleBackColor = true;
            // 
            // rbLinkToBuzzOpt2
            // 
            this.rbLinkToBuzzOpt2.AutoSize = true;
            this.rbLinkToBuzzOpt2.Location = new System.Drawing.Point(22, 49);
            this.rbLinkToBuzzOpt2.Name = "rbLinkToBuzzOpt2";
            this.rbLinkToBuzzOpt2.Size = new System.Drawing.Size(81, 17);
            this.rbLinkToBuzzOpt2.TabIndex = 1;
            this.rbLinkToBuzzOpt2.TabStop = true;
            this.rbLinkToBuzzOpt2.Text = "T1 Duration";
            this.rbLinkToBuzzOpt2.UseVisualStyleBackColor = true;
            // 
            // rbLinkToBuzzOpt1
            // 
            this.rbLinkToBuzzOpt1.AutoSize = true;
            this.rbLinkToBuzzOpt1.Location = new System.Drawing.Point(22, 26);
            this.rbLinkToBuzzOpt1.Name = "rbLinkToBuzzOpt1";
            this.rbLinkToBuzzOpt1.Size = new System.Drawing.Size(74, 17);
            this.rbLinkToBuzzOpt1.TabIndex = 0;
            this.rbLinkToBuzzOpt1.TabStop = true;
            this.rbLinkToBuzzOpt1.Text = "Buzzer Off";
            this.rbLinkToBuzzOpt1.UseVisualStyleBackColor = true;
            // 
            // gbBlinkDuration
            // 
            this.gbBlinkDuration.Controls.Add(this.bSetLED);
            this.gbBlinkDuration.Controls.Add(this.gbLinktoBuzz);
            this.gbBlinkDuration.Controls.Add(this.Label6);
            this.gbBlinkDuration.Controls.Add(this.tRepeat);
            this.gbBlinkDuration.Controls.Add(this.gbT2);
            this.gbBlinkDuration.Controls.Add(this.gbT1);
            this.gbBlinkDuration.Location = new System.Drawing.Point(284, 13);
            this.gbBlinkDuration.Name = "gbBlinkDuration";
            this.gbBlinkDuration.Size = new System.Drawing.Size(244, 417);
            this.gbBlinkDuration.TabIndex = 40;
            this.gbBlinkDuration.TabStop = false;
            this.gbBlinkDuration.Text = "Blinking Duration Control";
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(19, 157);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(118, 31);
            this.Label6.TabIndex = 3;
            this.Label6.Text = "Number of Repetitions: ";
            // 
            // tRepeat
            // 
            this.tRepeat.Location = new System.Drawing.Point(143, 154);
            this.tRepeat.MaxLength = 2;
            this.tRepeat.Name = "tRepeat";
            this.tRepeat.Size = new System.Drawing.Size(44, 20);
            this.tRepeat.TabIndex = 3;
            // 
            // gbT2
            // 
            this.gbT2.Controls.Add(this.Label4);
            this.gbT2.Controls.Add(this.tT2);
            this.gbT2.Controls.Add(this.Label5);
            this.gbT2.Location = new System.Drawing.Point(13, 84);
            this.gbT2.Name = "gbT2";
            this.gbT2.Size = new System.Drawing.Size(215, 55);
            this.gbT2.TabIndex = 1;
            this.gbT2.TabStop = false;
            this.gbT2.Text = "T2 Duration";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(146, 24);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(46, 13);
            this.Label4.TabIndex = 2;
            this.Label4.Text = "x100 ms";
            // 
            // tT2
            // 
            this.tT2.Location = new System.Drawing.Point(96, 21);
            this.tT2.MaxLength = 2;
            this.tT2.Name = "tT2";
            this.tT2.Size = new System.Drawing.Size(44, 20);
            this.tT2.TabIndex = 1;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(6, 21);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(84, 31);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Toggle Blinking State";
            // 
            // gbT1
            // 
            this.gbT1.Controls.Add(this.Label3);
            this.gbT1.Controls.Add(this.tT1);
            this.gbT1.Controls.Add(this.Label2);
            this.gbT1.Location = new System.Drawing.Point(13, 23);
            this.gbT1.Name = "gbT1";
            this.gbT1.Size = new System.Drawing.Size(215, 55);
            this.gbT1.TabIndex = 0;
            this.gbT1.TabStop = false;
            this.gbT1.Text = "T1 Duration";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(146, 21);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(46, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "x100 ms";
            // 
            // tT1
            // 
            this.tT1.Location = new System.Drawing.Point(96, 19);
            this.tT1.MaxLength = 2;
            this.tT1.Name = "tT1";
            this.tT1.Size = new System.Drawing.Size(44, 20);
            this.tT1.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(6, 21);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(84, 31);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Initial Blinking State";
            // 
            // rbGreenFinalOff
            // 
            this.rbGreenFinalOff.AutoSize = true;
            this.rbGreenFinalOff.Location = new System.Drawing.Point(56, 15);
            this.rbGreenFinalOff.Name = "rbGreenFinalOff";
            this.rbGreenFinalOff.Size = new System.Drawing.Size(39, 17);
            this.rbGreenFinalOff.TabIndex = 23;
            this.rbGreenFinalOff.TabStop = true;
            this.rbGreenFinalOff.Text = "Off";
            this.rbGreenFinalOff.UseVisualStyleBackColor = true;
            // 
            // rbGreenStateMaskOn
            // 
            this.rbGreenStateMaskOn.AutoSize = true;
            this.rbGreenStateMaskOn.Location = new System.Drawing.Point(11, 15);
            this.rbGreenStateMaskOn.Name = "rbGreenStateMaskOn";
            this.rbGreenStateMaskOn.Size = new System.Drawing.Size(39, 17);
            this.rbGreenStateMaskOn.TabIndex = 22;
            this.rbGreenStateMaskOn.TabStop = true;
            this.rbGreenStateMaskOn.Text = "On";
            this.rbGreenStateMaskOn.UseVisualStyleBackColor = true;
            // 
            // rbGreenStateMaskOff
            // 
            this.rbGreenStateMaskOff.AutoSize = true;
            this.rbGreenStateMaskOff.Location = new System.Drawing.Point(65, 15);
            this.rbGreenStateMaskOff.Name = "rbGreenStateMaskOff";
            this.rbGreenStateMaskOff.Size = new System.Drawing.Size(39, 17);
            this.rbGreenStateMaskOff.TabIndex = 23;
            this.rbGreenStateMaskOff.TabStop = true;
            this.rbGreenStateMaskOff.Text = "Off";
            this.rbGreenStateMaskOff.UseVisualStyleBackColor = true;
            // 
            // gbGreenFinal
            // 
            this.gbGreenFinal.Controls.Add(this.rbGreenFinalOn);
            this.gbGreenFinal.Controls.Add(this.rbGreenFinalOff);
            this.gbGreenFinal.Location = new System.Drawing.Point(8, 19);
            this.gbGreenFinal.Name = "gbGreenFinal";
            this.gbGreenFinal.Size = new System.Drawing.Size(116, 38);
            this.gbGreenFinal.TabIndex = 0;
            this.gbGreenFinal.TabStop = false;
            this.gbGreenFinal.Text = "Final LED State";
            // 
            // rbGreenFinalOn
            // 
            this.rbGreenFinalOn.AutoSize = true;
            this.rbGreenFinalOn.Location = new System.Drawing.Point(11, 15);
            this.rbGreenFinalOn.Name = "rbGreenFinalOn";
            this.rbGreenFinalOn.Size = new System.Drawing.Size(39, 17);
            this.rbGreenFinalOn.TabIndex = 22;
            this.rbGreenFinalOn.TabStop = true;
            this.rbGreenFinalOn.Text = "On";
            this.rbGreenFinalOn.UseVisualStyleBackColor = true;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(139, 76);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(129, 23);
            this.bConnect.TabIndex = 35;
            this.bConnect.Text = "Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(534, 407);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(97, 23);
            this.bClear.TabIndex = 42;
            this.bClear.Text = "Clear Screen";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(642, 407);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(83, 23);
            this.bReset.TabIndex = 43;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // mMsg
            // 
            this.mMsg.Location = new System.Drawing.Point(534, 16);
            this.mMsg.Name = "mMsg";
            this.mMsg.Size = new System.Drawing.Size(276, 385);
            this.mMsg.TabIndex = 41;
            this.mMsg.Text = "";
            // 
            // gbGreenStateMask
            // 
            this.gbGreenStateMask.Controls.Add(this.rbGreenStateMaskOn);
            this.gbGreenStateMask.Controls.Add(this.rbGreenStateMaskOff);
            this.gbGreenStateMask.Location = new System.Drawing.Point(130, 19);
            this.gbGreenStateMask.Name = "gbGreenStateMask";
            this.gbGreenStateMask.Size = new System.Drawing.Size(120, 38);
            this.gbGreenStateMask.TabIndex = 24;
            this.gbGreenStateMask.TabStop = false;
            this.gbGreenStateMask.Text = "LED State Mask";
            // 
            // rbGreenInitOn
            // 
            this.rbGreenInitOn.AutoSize = true;
            this.rbGreenInitOn.Location = new System.Drawing.Point(11, 15);
            this.rbGreenInitOn.Name = "rbGreenInitOn";
            this.rbGreenInitOn.Size = new System.Drawing.Size(39, 17);
            this.rbGreenInitOn.TabIndex = 22;
            this.rbGreenInitOn.TabStop = true;
            this.rbGreenInitOn.Text = "On";
            this.rbGreenInitOn.UseVisualStyleBackColor = true;
            // 
            // rbGreenInitOff
            // 
            this.rbGreenInitOff.AutoSize = true;
            this.rbGreenInitOff.Location = new System.Drawing.Point(56, 15);
            this.rbGreenInitOff.Name = "rbGreenInitOff";
            this.rbGreenInitOff.Size = new System.Drawing.Size(39, 17);
            this.rbGreenInitOff.TabIndex = 23;
            this.rbGreenInitOff.TabStop = true;
            this.rbGreenInitOff.Text = "Off";
            this.rbGreenInitOff.UseVisualStyleBackColor = true;
            // 
            // rbAntOn
            // 
            this.rbAntOn.AutoSize = true;
            this.rbAntOn.Location = new System.Drawing.Point(19, 22);
            this.rbAntOn.Name = "rbAntOn";
            this.rbAntOn.Size = new System.Drawing.Size(39, 17);
            this.rbAntOn.TabIndex = 20;
            this.rbAntOn.TabStop = true;
            this.rbAntOn.Text = "On";
            this.rbAntOn.UseVisualStyleBackColor = true;
            // 
            // gbRed
            // 
            this.gbRed.Controls.Add(this.gbRedBlinkMask);
            this.gbRed.Controls.Add(this.gbRedInit);
            this.gbRed.Controls.Add(this.gbRedStateMask);
            this.gbRed.Controls.Add(this.gbRedFinal);
            this.gbRed.Location = new System.Drawing.Point(12, 204);
            this.gbRed.Name = "gbRed";
            this.gbRed.Size = new System.Drawing.Size(256, 110);
            this.gbRed.TabIndex = 38;
            this.gbRed.TabStop = false;
            this.gbRed.Text = "Red LED";
            // 
            // gbRedBlinkMask
            // 
            this.gbRedBlinkMask.Controls.Add(this.rbRedBlinkMaskOn);
            this.gbRedBlinkMask.Controls.Add(this.rbRedBlinkMaskOff);
            this.gbRedBlinkMask.Location = new System.Drawing.Point(130, 63);
            this.gbRedBlinkMask.Name = "gbRedBlinkMask";
            this.gbRedBlinkMask.Size = new System.Drawing.Size(120, 38);
            this.gbRedBlinkMask.TabIndex = 25;
            this.gbRedBlinkMask.TabStop = false;
            this.gbRedBlinkMask.Text = "LED Blinking Mask";
            // 
            // rbRedBlinkMaskOn
            // 
            this.rbRedBlinkMaskOn.AutoSize = true;
            this.rbRedBlinkMaskOn.Location = new System.Drawing.Point(11, 15);
            this.rbRedBlinkMaskOn.Name = "rbRedBlinkMaskOn";
            this.rbRedBlinkMaskOn.Size = new System.Drawing.Size(39, 17);
            this.rbRedBlinkMaskOn.TabIndex = 22;
            this.rbRedBlinkMaskOn.TabStop = true;
            this.rbRedBlinkMaskOn.Text = "On";
            this.rbRedBlinkMaskOn.UseVisualStyleBackColor = true;
            // 
            // rbRedBlinkMaskOff
            // 
            this.rbRedBlinkMaskOff.AutoSize = true;
            this.rbRedBlinkMaskOff.Location = new System.Drawing.Point(65, 15);
            this.rbRedBlinkMaskOff.Name = "rbRedBlinkMaskOff";
            this.rbRedBlinkMaskOff.Size = new System.Drawing.Size(39, 17);
            this.rbRedBlinkMaskOff.TabIndex = 23;
            this.rbRedBlinkMaskOff.TabStop = true;
            this.rbRedBlinkMaskOff.Text = "Off";
            this.rbRedBlinkMaskOff.UseVisualStyleBackColor = true;
            // 
            // gbRedInit
            // 
            this.gbRedInit.Controls.Add(this.rbRedInitOn);
            this.gbRedInit.Controls.Add(this.rbRedInitOff);
            this.gbRedInit.Location = new System.Drawing.Point(8, 63);
            this.gbRedInit.Name = "gbRedInit";
            this.gbRedInit.Size = new System.Drawing.Size(116, 38);
            this.gbRedInit.TabIndex = 24;
            this.gbRedInit.TabStop = false;
            this.gbRedInit.Text = "Initial Blinking State";
            // 
            // rbRedInitOn
            // 
            this.rbRedInitOn.AutoSize = true;
            this.rbRedInitOn.Location = new System.Drawing.Point(11, 15);
            this.rbRedInitOn.Name = "rbRedInitOn";
            this.rbRedInitOn.Size = new System.Drawing.Size(39, 17);
            this.rbRedInitOn.TabIndex = 22;
            this.rbRedInitOn.TabStop = true;
            this.rbRedInitOn.Text = "On";
            this.rbRedInitOn.UseVisualStyleBackColor = true;
            // 
            // rbRedInitOff
            // 
            this.rbRedInitOff.AutoSize = true;
            this.rbRedInitOff.Location = new System.Drawing.Point(56, 15);
            this.rbRedInitOff.Name = "rbRedInitOff";
            this.rbRedInitOff.Size = new System.Drawing.Size(39, 17);
            this.rbRedInitOff.TabIndex = 23;
            this.rbRedInitOff.TabStop = true;
            this.rbRedInitOff.Text = "Off";
            this.rbRedInitOff.UseVisualStyleBackColor = true;
            // 
            // gbRedStateMask
            // 
            this.gbRedStateMask.Controls.Add(this.rbRedStateMaskOn);
            this.gbRedStateMask.Controls.Add(this.rbRedStateMaskOff);
            this.gbRedStateMask.Location = new System.Drawing.Point(130, 19);
            this.gbRedStateMask.Name = "gbRedStateMask";
            this.gbRedStateMask.Size = new System.Drawing.Size(120, 38);
            this.gbRedStateMask.TabIndex = 24;
            this.gbRedStateMask.TabStop = false;
            this.gbRedStateMask.Text = "LED State Mask";
            // 
            // rbRedStateMaskOn
            // 
            this.rbRedStateMaskOn.AutoSize = true;
            this.rbRedStateMaskOn.Location = new System.Drawing.Point(11, 15);
            this.rbRedStateMaskOn.Name = "rbRedStateMaskOn";
            this.rbRedStateMaskOn.Size = new System.Drawing.Size(39, 17);
            this.rbRedStateMaskOn.TabIndex = 22;
            this.rbRedStateMaskOn.TabStop = true;
            this.rbRedStateMaskOn.Text = "On";
            this.rbRedStateMaskOn.UseVisualStyleBackColor = true;
            // 
            // rbRedStateMaskOff
            // 
            this.rbRedStateMaskOff.AutoSize = true;
            this.rbRedStateMaskOff.Location = new System.Drawing.Point(65, 15);
            this.rbRedStateMaskOff.Name = "rbRedStateMaskOff";
            this.rbRedStateMaskOff.Size = new System.Drawing.Size(39, 17);
            this.rbRedStateMaskOff.TabIndex = 23;
            this.rbRedStateMaskOff.TabStop = true;
            this.rbRedStateMaskOff.Text = "Off";
            this.rbRedStateMaskOff.UseVisualStyleBackColor = true;
            // 
            // gbRedFinal
            // 
            this.gbRedFinal.Controls.Add(this.rbRedFinalOn);
            this.gbRedFinal.Controls.Add(this.rbRedFinalOff);
            this.gbRedFinal.Location = new System.Drawing.Point(8, 19);
            this.gbRedFinal.Name = "gbRedFinal";
            this.gbRedFinal.Size = new System.Drawing.Size(116, 38);
            this.gbRedFinal.TabIndex = 0;
            this.gbRedFinal.TabStop = false;
            this.gbRedFinal.Text = "Final LED State";
            // 
            // rbRedFinalOn
            // 
            this.rbRedFinalOn.AutoSize = true;
            this.rbRedFinalOn.Location = new System.Drawing.Point(11, 15);
            this.rbRedFinalOn.Name = "rbRedFinalOn";
            this.rbRedFinalOn.Size = new System.Drawing.Size(39, 17);
            this.rbRedFinalOn.TabIndex = 22;
            this.rbRedFinalOn.TabStop = true;
            this.rbRedFinalOn.Text = "On";
            this.rbRedFinalOn.UseVisualStyleBackColor = true;
            // 
            // rbRedFinalOff
            // 
            this.rbRedFinalOff.AutoSize = true;
            this.rbRedFinalOff.Location = new System.Drawing.Point(56, 15);
            this.rbRedFinalOff.Name = "rbRedFinalOff";
            this.rbRedFinalOff.Size = new System.Drawing.Size(39, 17);
            this.rbRedFinalOff.TabIndex = 23;
            this.rbRedFinalOff.TabStop = true;
            this.rbRedFinalOff.Text = "Off";
            this.rbRedFinalOff.UseVisualStyleBackColor = true;
            // 
            // bSetAntenna
            // 
            this.bSetAntenna.Location = new System.Drawing.Point(127, 19);
            this.bSetAntenna.Name = "bSetAntenna";
            this.bSetAntenna.Size = new System.Drawing.Size(123, 23);
            this.bSetAntenna.TabIndex = 22;
            this.bSetAntenna.Text = "Set Antenna";
            this.bSetAntenna.UseVisualStyleBackColor = true;
            this.bSetAntenna.Click += new System.EventHandler(this.bSetAntenna_Click);
            // 
            // gbAntenna
            // 
            this.gbAntenna.Controls.Add(this.bSetAntenna);
            this.gbAntenna.Controls.Add(this.rbAntOn);
            this.gbAntenna.Controls.Add(this.rbAntOff);
            this.gbAntenna.Location = new System.Drawing.Point(12, 139);
            this.gbAntenna.Name = "gbAntenna";
            this.gbAntenna.Size = new System.Drawing.Size(256, 59);
            this.gbAntenna.TabIndex = 37;
            this.gbAntenna.TabStop = false;
            this.gbAntenna.Text = "Antenna Settings";
            // 
            // rbAntOff
            // 
            this.rbAntOff.AutoSize = true;
            this.rbAntOff.Location = new System.Drawing.Point(64, 22);
            this.rbAntOff.Name = "rbAntOff";
            this.rbAntOff.Size = new System.Drawing.Size(39, 17);
            this.rbAntOff.TabIndex = 21;
            this.rbAntOff.TabStop = true;
            this.rbAntOff.Text = "Off";
            this.rbAntOff.UseVisualStyleBackColor = true;
            // 
            // bGetFW
            // 
            this.bGetFW.Location = new System.Drawing.Point(12, 110);
            this.bGetFW.Name = "bGetFW";
            this.bGetFW.Size = new System.Drawing.Size(129, 28);
            this.bGetFW.TabIndex = 36;
            this.bGetFW.Text = "Get Firmware Version";
            this.bGetFW.UseVisualStyleBackColor = true;
            this.bGetFW.Click += new System.EventHandler(this.bGetFW_Click);
            // 
            // bInit
            // 
            this.bInit.Location = new System.Drawing.Point(139, 47);
            this.bInit.Name = "bInit";
            this.bInit.Size = new System.Drawing.Size(129, 23);
            this.bInit.TabIndex = 34;
            this.bInit.Text = "Initialize";
            this.bInit.UseVisualStyleBackColor = true;
            this.bInit.Click += new System.EventHandler(this.bInit_Click);
            // 
            // cbReader
            // 
            this.cbReader.FormattingEnabled = true;
            this.cbReader.Location = new System.Drawing.Point(90, 13);
            this.cbReader.Name = "cbReader";
            this.cbReader.Size = new System.Drawing.Size(178, 21);
            this.cbReader.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Select Reader";
            // 
            // gbGreenBlinkMask
            // 
            this.gbGreenBlinkMask.Controls.Add(this.rbGreenBlinkMaskOn);
            this.gbGreenBlinkMask.Controls.Add(this.rbGreenBlinkMaskOff);
            this.gbGreenBlinkMask.Location = new System.Drawing.Point(130, 63);
            this.gbGreenBlinkMask.Name = "gbGreenBlinkMask";
            this.gbGreenBlinkMask.Size = new System.Drawing.Size(120, 38);
            this.gbGreenBlinkMask.TabIndex = 25;
            this.gbGreenBlinkMask.TabStop = false;
            this.gbGreenBlinkMask.Text = "LED Blinking Mask";
            // 
            // rbGreenBlinkMaskOn
            // 
            this.rbGreenBlinkMaskOn.AutoSize = true;
            this.rbGreenBlinkMaskOn.Location = new System.Drawing.Point(11, 15);
            this.rbGreenBlinkMaskOn.Name = "rbGreenBlinkMaskOn";
            this.rbGreenBlinkMaskOn.Size = new System.Drawing.Size(39, 17);
            this.rbGreenBlinkMaskOn.TabIndex = 22;
            this.rbGreenBlinkMaskOn.TabStop = true;
            this.rbGreenBlinkMaskOn.Text = "On";
            this.rbGreenBlinkMaskOn.UseVisualStyleBackColor = true;
            // 
            // rbGreenBlinkMaskOff
            // 
            this.rbGreenBlinkMaskOff.AutoSize = true;
            this.rbGreenBlinkMaskOff.Location = new System.Drawing.Point(65, 15);
            this.rbGreenBlinkMaskOff.Name = "rbGreenBlinkMaskOff";
            this.rbGreenBlinkMaskOff.Size = new System.Drawing.Size(39, 17);
            this.rbGreenBlinkMaskOff.TabIndex = 23;
            this.rbGreenBlinkMaskOff.TabStop = true;
            this.rbGreenBlinkMaskOff.Text = "Off";
            this.rbGreenBlinkMaskOff.UseVisualStyleBackColor = true;
            // 
            // gbGreenInit
            // 
            this.gbGreenInit.Controls.Add(this.rbGreenInitOn);
            this.gbGreenInit.Controls.Add(this.rbGreenInitOff);
            this.gbGreenInit.Location = new System.Drawing.Point(8, 63);
            this.gbGreenInit.Name = "gbGreenInit";
            this.gbGreenInit.Size = new System.Drawing.Size(116, 38);
            this.gbGreenInit.TabIndex = 24;
            this.gbGreenInit.TabStop = false;
            this.gbGreenInit.Text = "Initial Blinking State";
            // 
            // gbGreen
            // 
            this.gbGreen.Controls.Add(this.gbGreenBlinkMask);
            this.gbGreen.Controls.Add(this.gbGreenInit);
            this.gbGreen.Controls.Add(this.gbGreenStateMask);
            this.gbGreen.Controls.Add(this.gbGreenFinal);
            this.gbGreen.Location = new System.Drawing.Point(12, 320);
            this.gbGreen.Name = "gbGreen";
            this.gbGreen.Size = new System.Drawing.Size(256, 110);
            this.gbGreen.TabIndex = 39;
            this.gbGreen.TabStop = false;
            this.gbGreen.Text = "Green LED";
            // 
            // bQuit
            // 
            this.bQuit.Location = new System.Drawing.Point(731, 407);
            this.bQuit.Name = "bQuit";
            this.bQuit.Size = new System.Drawing.Size(84, 23);
            this.bQuit.TabIndex = 44;
            this.bQuit.Text = "Quit";
            this.bQuit.UseVisualStyleBackColor = true;
            this.bQuit.Click += new System.EventHandler(this.bQuit_Click);
            // 
            // bStatus
            // 
            this.bStatus.Location = new System.Drawing.Point(147, 110);
            this.bStatus.Name = "bStatus";
            this.bStatus.Size = new System.Drawing.Size(121, 28);
            this.bStatus.TabIndex = 45;
            this.bStatus.Text = "Get Status";
            this.bStatus.UseVisualStyleBackColor = true;
            this.bStatus.Click += new System.EventHandler(this.bStatus_Click);
            // 
            // DeviceProgramming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 443);
            this.Controls.Add(this.bStatus);
            this.Controls.Add(this.gbBlinkDuration);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.mMsg);
            this.Controls.Add(this.gbRed);
            this.Controls.Add(this.gbAntenna);
            this.Controls.Add(this.bGetFW);
            this.Controls.Add(this.bInit);
            this.Controls.Add(this.cbReader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbGreen);
            this.Controls.Add(this.bQuit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeviceProgramming";
            this.Text = "ACR122 Device Programming";
            this.Load += new System.EventHandler(this.DeviceProgramming_Load);
            this.gbLinktoBuzz.ResumeLayout(false);
            this.gbLinktoBuzz.PerformLayout();
            this.gbBlinkDuration.ResumeLayout(false);
            this.gbBlinkDuration.PerformLayout();
            this.gbT2.ResumeLayout(false);
            this.gbT2.PerformLayout();
            this.gbT1.ResumeLayout(false);
            this.gbT1.PerformLayout();
            this.gbGreenFinal.ResumeLayout(false);
            this.gbGreenFinal.PerformLayout();
            this.gbGreenStateMask.ResumeLayout(false);
            this.gbGreenStateMask.PerformLayout();
            this.gbRed.ResumeLayout(false);
            this.gbRedBlinkMask.ResumeLayout(false);
            this.gbRedBlinkMask.PerformLayout();
            this.gbRedInit.ResumeLayout(false);
            this.gbRedInit.PerformLayout();
            this.gbRedStateMask.ResumeLayout(false);
            this.gbRedStateMask.PerformLayout();
            this.gbRedFinal.ResumeLayout(false);
            this.gbRedFinal.PerformLayout();
            this.gbAntenna.ResumeLayout(false);
            this.gbAntenna.PerformLayout();
            this.gbGreenBlinkMask.ResumeLayout(false);
            this.gbGreenBlinkMask.PerformLayout();
            this.gbGreenInit.ResumeLayout(false);
            this.gbGreenInit.PerformLayout();
            this.gbGreen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bSetLED;
        internal System.Windows.Forms.GroupBox gbLinktoBuzz;
        internal System.Windows.Forms.RadioButton rbLinkToBuzzOpt4;
        internal System.Windows.Forms.RadioButton rbLinkToBuzzOpt3;
        internal System.Windows.Forms.RadioButton rbLinkToBuzzOpt2;
        internal System.Windows.Forms.RadioButton rbLinkToBuzzOpt1;
        internal System.Windows.Forms.GroupBox gbBlinkDuration;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox tRepeat;
        internal System.Windows.Forms.GroupBox gbT2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox tT2;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.GroupBox gbT1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox tT1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.RadioButton rbGreenFinalOff;
        internal System.Windows.Forms.RadioButton rbGreenStateMaskOn;
        internal System.Windows.Forms.RadioButton rbGreenStateMaskOff;
        internal System.Windows.Forms.GroupBox gbGreenFinal;
        internal System.Windows.Forms.RadioButton rbGreenFinalOn;
        private System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.Button bClear;
        internal System.Windows.Forms.Button bReset;
        private System.Windows.Forms.RichTextBox mMsg;
        internal System.Windows.Forms.GroupBox gbGreenStateMask;
        internal System.Windows.Forms.RadioButton rbGreenInitOn;
        internal System.Windows.Forms.RadioButton rbGreenInitOff;
        internal System.Windows.Forms.RadioButton rbAntOn;
        internal System.Windows.Forms.GroupBox gbRed;
        internal System.Windows.Forms.GroupBox gbRedBlinkMask;
        internal System.Windows.Forms.RadioButton rbRedBlinkMaskOn;
        internal System.Windows.Forms.RadioButton rbRedBlinkMaskOff;
        internal System.Windows.Forms.GroupBox gbRedInit;
        internal System.Windows.Forms.RadioButton rbRedInitOn;
        internal System.Windows.Forms.RadioButton rbRedInitOff;
        internal System.Windows.Forms.GroupBox gbRedStateMask;
        internal System.Windows.Forms.RadioButton rbRedStateMaskOn;
        internal System.Windows.Forms.RadioButton rbRedStateMaskOff;
        internal System.Windows.Forms.GroupBox gbRedFinal;
        internal System.Windows.Forms.RadioButton rbRedFinalOn;
        internal System.Windows.Forms.RadioButton rbRedFinalOff;
        private System.Windows.Forms.Button bSetAntenna;
        internal System.Windows.Forms.GroupBox gbAntenna;
        internal System.Windows.Forms.RadioButton rbAntOff;
        internal System.Windows.Forms.Button bGetFW;
        private System.Windows.Forms.Button bInit;
        private System.Windows.Forms.ComboBox cbReader;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.GroupBox gbGreenBlinkMask;
        internal System.Windows.Forms.RadioButton rbGreenBlinkMaskOn;
        internal System.Windows.Forms.RadioButton rbGreenBlinkMaskOff;
        internal System.Windows.Forms.GroupBox gbGreenInit;
        internal System.Windows.Forms.GroupBox gbGreen;
        internal System.Windows.Forms.Button bQuit;
        internal System.Windows.Forms.Button bStatus;
    }
}

