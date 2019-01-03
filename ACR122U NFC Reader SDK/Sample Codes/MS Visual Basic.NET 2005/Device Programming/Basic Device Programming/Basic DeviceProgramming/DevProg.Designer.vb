<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeviceProgramming
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeviceProgramming))
        Me.bConnect = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.label1 = New System.Windows.Forms.Label
        Me.bGetFW = New System.Windows.Forms.Button
        Me.rbAntOn = New System.Windows.Forms.RadioButton
        Me.rbAntOff = New System.Windows.Forms.RadioButton
        Me.gbAntenna = New System.Windows.Forms.GroupBox
        Me.bSetAntenna = New System.Windows.Forms.Button
        Me.gbRed = New System.Windows.Forms.GroupBox
        Me.gbRedBlinkMask = New System.Windows.Forms.GroupBox
        Me.rbRedBlinkMaskOn = New System.Windows.Forms.RadioButton
        Me.rbRedBlinkMaskOff = New System.Windows.Forms.RadioButton
        Me.gbRedInit = New System.Windows.Forms.GroupBox
        Me.rbRedInitOn = New System.Windows.Forms.RadioButton
        Me.rbRedInitOff = New System.Windows.Forms.RadioButton
        Me.gbRedStateMask = New System.Windows.Forms.GroupBox
        Me.rbRedStateMaskOn = New System.Windows.Forms.RadioButton
        Me.rbRedStateMaskOff = New System.Windows.Forms.RadioButton
        Me.gbRedFinal = New System.Windows.Forms.GroupBox
        Me.rbRedFinalOn = New System.Windows.Forms.RadioButton
        Me.rbRedFinalOff = New System.Windows.Forms.RadioButton
        Me.gbGreen = New System.Windows.Forms.GroupBox
        Me.gbGreenBlinkMask = New System.Windows.Forms.GroupBox
        Me.rbGreenBlinkMaskOn = New System.Windows.Forms.RadioButton
        Me.rbGreenBlinkMaskOff = New System.Windows.Forms.RadioButton
        Me.gbGreenInit = New System.Windows.Forms.GroupBox
        Me.rbGreenInitOn = New System.Windows.Forms.RadioButton
        Me.rbGreenInitOff = New System.Windows.Forms.RadioButton
        Me.gbGreenStateMask = New System.Windows.Forms.GroupBox
        Me.rbGreenStateMaskOn = New System.Windows.Forms.RadioButton
        Me.rbGreenStateMaskOff = New System.Windows.Forms.RadioButton
        Me.gbGreenFinal = New System.Windows.Forms.GroupBox
        Me.rbGreenFinalOn = New System.Windows.Forms.RadioButton
        Me.rbGreenFinalOff = New System.Windows.Forms.RadioButton
        Me.gbBlinkDuration = New System.Windows.Forms.GroupBox
        Me.bSetLED = New System.Windows.Forms.Button
        Me.gbLinktoBuzz = New System.Windows.Forms.GroupBox
        Me.rbLinkToBuzzOpt4 = New System.Windows.Forms.RadioButton
        Me.rbLinkToBuzzOpt3 = New System.Windows.Forms.RadioButton
        Me.rbLinkToBuzzOpt2 = New System.Windows.Forms.RadioButton
        Me.rbLinkToBuzzOpt1 = New System.Windows.Forms.RadioButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.tRepeat = New System.Windows.Forms.TextBox
        Me.gbT2 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.tT2 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.gbT1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.tT1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.mMsg = New System.Windows.Forms.RichTextBox
        Me.bClear = New System.Windows.Forms.Button
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.bStatus = New System.Windows.Forms.Button
        Me.gbAntenna.SuspendLayout()
        Me.gbRed.SuspendLayout()
        Me.gbRedBlinkMask.SuspendLayout()
        Me.gbRedInit.SuspendLayout()
        Me.gbRedStateMask.SuspendLayout()
        Me.gbRedFinal.SuspendLayout()
        Me.gbGreen.SuspendLayout()
        Me.gbGreenBlinkMask.SuspendLayout()
        Me.gbGreenInit.SuspendLayout()
        Me.gbGreenStateMask.SuspendLayout()
        Me.gbGreenFinal.SuspendLayout()
        Me.gbBlinkDuration.SuspendLayout()
        Me.gbLinktoBuzz.SuspendLayout()
        Me.gbT2.SuspendLayout()
        Me.gbT1.SuspendLayout()
        Me.SuspendLayout()
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(142, 75)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(129, 23)
        Me.bConnect.TabIndex = 16
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(142, 46)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(129, 23)
        Me.bInit.TabIndex = 15
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 12)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(178, 21)
        Me.cbReader.TabIndex = 14
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(12, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(75, 13)
        Me.label1.TabIndex = 13
        Me.label1.Text = "Select Reader"
        '
        'bGetFW
        '
        Me.bGetFW.Location = New System.Drawing.Point(10, 104)
        Me.bGetFW.Name = "bGetFW"
        Me.bGetFW.Size = New System.Drawing.Size(129, 28)
        Me.bGetFW.TabIndex = 19
        Me.bGetFW.Text = "Get Firmware Version"
        Me.bGetFW.UseVisualStyleBackColor = True
        '
        'rbAntOn
        '
        Me.rbAntOn.AutoSize = True
        Me.rbAntOn.Location = New System.Drawing.Point(19, 22)
        Me.rbAntOn.Name = "rbAntOn"
        Me.rbAntOn.Size = New System.Drawing.Size(39, 17)
        Me.rbAntOn.TabIndex = 20
        Me.rbAntOn.TabStop = True
        Me.rbAntOn.Text = "On"
        Me.rbAntOn.UseVisualStyleBackColor = True
        '
        'rbAntOff
        '
        Me.rbAntOff.AutoSize = True
        Me.rbAntOff.Location = New System.Drawing.Point(64, 22)
        Me.rbAntOff.Name = "rbAntOff"
        Me.rbAntOff.Size = New System.Drawing.Size(39, 17)
        Me.rbAntOff.TabIndex = 21
        Me.rbAntOff.TabStop = True
        Me.rbAntOff.Text = "Off"
        Me.rbAntOff.UseVisualStyleBackColor = True
        '
        'gbAntenna
        '
        Me.gbAntenna.Controls.Add(Me.bSetAntenna)
        Me.gbAntenna.Controls.Add(Me.rbAntOn)
        Me.gbAntenna.Controls.Add(Me.rbAntOff)
        Me.gbAntenna.Location = New System.Drawing.Point(15, 138)
        Me.gbAntenna.Name = "gbAntenna"
        Me.gbAntenna.Size = New System.Drawing.Size(256, 59)
        Me.gbAntenna.TabIndex = 22
        Me.gbAntenna.TabStop = False
        Me.gbAntenna.Text = "Antenna Settings"
        '
        'bSetAntenna
        '
        Me.bSetAntenna.Location = New System.Drawing.Point(127, 19)
        Me.bSetAntenna.Name = "bSetAntenna"
        Me.bSetAntenna.Size = New System.Drawing.Size(123, 23)
        Me.bSetAntenna.TabIndex = 22
        Me.bSetAntenna.Text = "Set Antenna"
        Me.bSetAntenna.UseVisualStyleBackColor = True
        '
        'gbRed
        '
        Me.gbRed.Controls.Add(Me.gbRedBlinkMask)
        Me.gbRed.Controls.Add(Me.gbRedInit)
        Me.gbRed.Controls.Add(Me.gbRedStateMask)
        Me.gbRed.Controls.Add(Me.gbRedFinal)
        Me.gbRed.Location = New System.Drawing.Point(15, 203)
        Me.gbRed.Name = "gbRed"
        Me.gbRed.Size = New System.Drawing.Size(256, 110)
        Me.gbRed.TabIndex = 23
        Me.gbRed.TabStop = False
        Me.gbRed.Text = "Red LED"
        '
        'gbRedBlinkMask
        '
        Me.gbRedBlinkMask.Controls.Add(Me.rbRedBlinkMaskOn)
        Me.gbRedBlinkMask.Controls.Add(Me.rbRedBlinkMaskOff)
        Me.gbRedBlinkMask.Location = New System.Drawing.Point(130, 63)
        Me.gbRedBlinkMask.Name = "gbRedBlinkMask"
        Me.gbRedBlinkMask.Size = New System.Drawing.Size(120, 38)
        Me.gbRedBlinkMask.TabIndex = 25
        Me.gbRedBlinkMask.TabStop = False
        Me.gbRedBlinkMask.Text = "LED Blinking Mask"
        '
        'rbRedBlinkMaskOn
        '
        Me.rbRedBlinkMaskOn.AutoSize = True
        Me.rbRedBlinkMaskOn.Location = New System.Drawing.Point(11, 15)
        Me.rbRedBlinkMaskOn.Name = "rbRedBlinkMaskOn"
        Me.rbRedBlinkMaskOn.Size = New System.Drawing.Size(39, 17)
        Me.rbRedBlinkMaskOn.TabIndex = 22
        Me.rbRedBlinkMaskOn.TabStop = True
        Me.rbRedBlinkMaskOn.Text = "On"
        Me.rbRedBlinkMaskOn.UseVisualStyleBackColor = True
        '
        'rbRedBlinkMaskOff
        '
        Me.rbRedBlinkMaskOff.AutoSize = True
        Me.rbRedBlinkMaskOff.Location = New System.Drawing.Point(65, 15)
        Me.rbRedBlinkMaskOff.Name = "rbRedBlinkMaskOff"
        Me.rbRedBlinkMaskOff.Size = New System.Drawing.Size(39, 17)
        Me.rbRedBlinkMaskOff.TabIndex = 23
        Me.rbRedBlinkMaskOff.TabStop = True
        Me.rbRedBlinkMaskOff.Text = "Off"
        Me.rbRedBlinkMaskOff.UseVisualStyleBackColor = True
        '
        'gbRedInit
        '
        Me.gbRedInit.Controls.Add(Me.rbRedInitOn)
        Me.gbRedInit.Controls.Add(Me.rbRedInitOff)
        Me.gbRedInit.Location = New System.Drawing.Point(8, 63)
        Me.gbRedInit.Name = "gbRedInit"
        Me.gbRedInit.Size = New System.Drawing.Size(116, 38)
        Me.gbRedInit.TabIndex = 24
        Me.gbRedInit.TabStop = False
        Me.gbRedInit.Text = "Initial Blinking State"
        '
        'rbRedInitOn
        '
        Me.rbRedInitOn.AutoSize = True
        Me.rbRedInitOn.Location = New System.Drawing.Point(11, 15)
        Me.rbRedInitOn.Name = "rbRedInitOn"
        Me.rbRedInitOn.Size = New System.Drawing.Size(39, 17)
        Me.rbRedInitOn.TabIndex = 22
        Me.rbRedInitOn.TabStop = True
        Me.rbRedInitOn.Text = "On"
        Me.rbRedInitOn.UseVisualStyleBackColor = True
        '
        'rbRedInitOff
        '
        Me.rbRedInitOff.AutoSize = True
        Me.rbRedInitOff.Location = New System.Drawing.Point(56, 15)
        Me.rbRedInitOff.Name = "rbRedInitOff"
        Me.rbRedInitOff.Size = New System.Drawing.Size(39, 17)
        Me.rbRedInitOff.TabIndex = 23
        Me.rbRedInitOff.TabStop = True
        Me.rbRedInitOff.Text = "Off"
        Me.rbRedInitOff.UseVisualStyleBackColor = True
        '
        'gbRedStateMask
        '
        Me.gbRedStateMask.Controls.Add(Me.rbRedStateMaskOn)
        Me.gbRedStateMask.Controls.Add(Me.rbRedStateMaskOff)
        Me.gbRedStateMask.Location = New System.Drawing.Point(130, 19)
        Me.gbRedStateMask.Name = "gbRedStateMask"
        Me.gbRedStateMask.Size = New System.Drawing.Size(120, 38)
        Me.gbRedStateMask.TabIndex = 24
        Me.gbRedStateMask.TabStop = False
        Me.gbRedStateMask.Text = "LED State Mask"
        '
        'rbRedStateMaskOn
        '
        Me.rbRedStateMaskOn.AutoSize = True
        Me.rbRedStateMaskOn.Location = New System.Drawing.Point(11, 15)
        Me.rbRedStateMaskOn.Name = "rbRedStateMaskOn"
        Me.rbRedStateMaskOn.Size = New System.Drawing.Size(39, 17)
        Me.rbRedStateMaskOn.TabIndex = 22
        Me.rbRedStateMaskOn.TabStop = True
        Me.rbRedStateMaskOn.Text = "On"
        Me.rbRedStateMaskOn.UseVisualStyleBackColor = True
        '
        'rbRedStateMaskOff
        '
        Me.rbRedStateMaskOff.AutoSize = True
        Me.rbRedStateMaskOff.Location = New System.Drawing.Point(65, 15)
        Me.rbRedStateMaskOff.Name = "rbRedStateMaskOff"
        Me.rbRedStateMaskOff.Size = New System.Drawing.Size(39, 17)
        Me.rbRedStateMaskOff.TabIndex = 23
        Me.rbRedStateMaskOff.TabStop = True
        Me.rbRedStateMaskOff.Text = "Off"
        Me.rbRedStateMaskOff.UseVisualStyleBackColor = True
        '
        'gbRedFinal
        '
        Me.gbRedFinal.Controls.Add(Me.rbRedFinalOn)
        Me.gbRedFinal.Controls.Add(Me.rbRedFinalOff)
        Me.gbRedFinal.Location = New System.Drawing.Point(8, 19)
        Me.gbRedFinal.Name = "gbRedFinal"
        Me.gbRedFinal.Size = New System.Drawing.Size(116, 38)
        Me.gbRedFinal.TabIndex = 0
        Me.gbRedFinal.TabStop = False
        Me.gbRedFinal.Text = "Final LED State"
        '
        'rbRedFinalOn
        '
        Me.rbRedFinalOn.AutoSize = True
        Me.rbRedFinalOn.Location = New System.Drawing.Point(11, 15)
        Me.rbRedFinalOn.Name = "rbRedFinalOn"
        Me.rbRedFinalOn.Size = New System.Drawing.Size(39, 17)
        Me.rbRedFinalOn.TabIndex = 22
        Me.rbRedFinalOn.TabStop = True
        Me.rbRedFinalOn.Text = "On"
        Me.rbRedFinalOn.UseVisualStyleBackColor = True
        '
        'rbRedFinalOff
        '
        Me.rbRedFinalOff.AutoSize = True
        Me.rbRedFinalOff.Location = New System.Drawing.Point(56, 15)
        Me.rbRedFinalOff.Name = "rbRedFinalOff"
        Me.rbRedFinalOff.Size = New System.Drawing.Size(39, 17)
        Me.rbRedFinalOff.TabIndex = 23
        Me.rbRedFinalOff.TabStop = True
        Me.rbRedFinalOff.Text = "Off"
        Me.rbRedFinalOff.UseVisualStyleBackColor = True
        '
        'gbGreen
        '
        Me.gbGreen.Controls.Add(Me.gbGreenBlinkMask)
        Me.gbGreen.Controls.Add(Me.gbGreenInit)
        Me.gbGreen.Controls.Add(Me.gbGreenStateMask)
        Me.gbGreen.Controls.Add(Me.gbGreenFinal)
        Me.gbGreen.Location = New System.Drawing.Point(15, 319)
        Me.gbGreen.Name = "gbGreen"
        Me.gbGreen.Size = New System.Drawing.Size(256, 110)
        Me.gbGreen.TabIndex = 26
        Me.gbGreen.TabStop = False
        Me.gbGreen.Text = "Green LED"
        '
        'gbGreenBlinkMask
        '
        Me.gbGreenBlinkMask.Controls.Add(Me.rbGreenBlinkMaskOn)
        Me.gbGreenBlinkMask.Controls.Add(Me.rbGreenBlinkMaskOff)
        Me.gbGreenBlinkMask.Location = New System.Drawing.Point(130, 63)
        Me.gbGreenBlinkMask.Name = "gbGreenBlinkMask"
        Me.gbGreenBlinkMask.Size = New System.Drawing.Size(120, 38)
        Me.gbGreenBlinkMask.TabIndex = 25
        Me.gbGreenBlinkMask.TabStop = False
        Me.gbGreenBlinkMask.Text = "LED Blinking Mask"
        '
        'rbGreenBlinkMaskOn
        '
        Me.rbGreenBlinkMaskOn.AutoSize = True
        Me.rbGreenBlinkMaskOn.Location = New System.Drawing.Point(11, 15)
        Me.rbGreenBlinkMaskOn.Name = "rbGreenBlinkMaskOn"
        Me.rbGreenBlinkMaskOn.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenBlinkMaskOn.TabIndex = 22
        Me.rbGreenBlinkMaskOn.TabStop = True
        Me.rbGreenBlinkMaskOn.Text = "On"
        Me.rbGreenBlinkMaskOn.UseVisualStyleBackColor = True
        '
        'rbGreenBlinkMaskOff
        '
        Me.rbGreenBlinkMaskOff.AutoSize = True
        Me.rbGreenBlinkMaskOff.Location = New System.Drawing.Point(65, 15)
        Me.rbGreenBlinkMaskOff.Name = "rbGreenBlinkMaskOff"
        Me.rbGreenBlinkMaskOff.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenBlinkMaskOff.TabIndex = 23
        Me.rbGreenBlinkMaskOff.TabStop = True
        Me.rbGreenBlinkMaskOff.Text = "Off"
        Me.rbGreenBlinkMaskOff.UseVisualStyleBackColor = True
        '
        'gbGreenInit
        '
        Me.gbGreenInit.Controls.Add(Me.rbGreenInitOn)
        Me.gbGreenInit.Controls.Add(Me.rbGreenInitOff)
        Me.gbGreenInit.Location = New System.Drawing.Point(8, 63)
        Me.gbGreenInit.Name = "gbGreenInit"
        Me.gbGreenInit.Size = New System.Drawing.Size(116, 38)
        Me.gbGreenInit.TabIndex = 24
        Me.gbGreenInit.TabStop = False
        Me.gbGreenInit.Text = "Initial Blinking State"
        '
        'rbGreenInitOn
        '
        Me.rbGreenInitOn.AutoSize = True
        Me.rbGreenInitOn.Location = New System.Drawing.Point(11, 15)
        Me.rbGreenInitOn.Name = "rbGreenInitOn"
        Me.rbGreenInitOn.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenInitOn.TabIndex = 22
        Me.rbGreenInitOn.TabStop = True
        Me.rbGreenInitOn.Text = "On"
        Me.rbGreenInitOn.UseVisualStyleBackColor = True
        '
        'rbGreenInitOff
        '
        Me.rbGreenInitOff.AutoSize = True
        Me.rbGreenInitOff.Location = New System.Drawing.Point(56, 15)
        Me.rbGreenInitOff.Name = "rbGreenInitOff"
        Me.rbGreenInitOff.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenInitOff.TabIndex = 23
        Me.rbGreenInitOff.TabStop = True
        Me.rbGreenInitOff.Text = "Off"
        Me.rbGreenInitOff.UseVisualStyleBackColor = True
        '
        'gbGreenStateMask
        '
        Me.gbGreenStateMask.Controls.Add(Me.rbGreenStateMaskOn)
        Me.gbGreenStateMask.Controls.Add(Me.rbGreenStateMaskOff)
        Me.gbGreenStateMask.Location = New System.Drawing.Point(130, 19)
        Me.gbGreenStateMask.Name = "gbGreenStateMask"
        Me.gbGreenStateMask.Size = New System.Drawing.Size(120, 38)
        Me.gbGreenStateMask.TabIndex = 24
        Me.gbGreenStateMask.TabStop = False
        Me.gbGreenStateMask.Text = "LED State Mask"
        '
        'rbGreenStateMaskOn
        '
        Me.rbGreenStateMaskOn.AutoSize = True
        Me.rbGreenStateMaskOn.Location = New System.Drawing.Point(11, 15)
        Me.rbGreenStateMaskOn.Name = "rbGreenStateMaskOn"
        Me.rbGreenStateMaskOn.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenStateMaskOn.TabIndex = 22
        Me.rbGreenStateMaskOn.TabStop = True
        Me.rbGreenStateMaskOn.Text = "On"
        Me.rbGreenStateMaskOn.UseVisualStyleBackColor = True
        '
        'rbGreenStateMaskOff
        '
        Me.rbGreenStateMaskOff.AutoSize = True
        Me.rbGreenStateMaskOff.Location = New System.Drawing.Point(65, 15)
        Me.rbGreenStateMaskOff.Name = "rbGreenStateMaskOff"
        Me.rbGreenStateMaskOff.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenStateMaskOff.TabIndex = 23
        Me.rbGreenStateMaskOff.TabStop = True
        Me.rbGreenStateMaskOff.Text = "Off"
        Me.rbGreenStateMaskOff.UseVisualStyleBackColor = True
        '
        'gbGreenFinal
        '
        Me.gbGreenFinal.Controls.Add(Me.rbGreenFinalOn)
        Me.gbGreenFinal.Controls.Add(Me.rbGreenFinalOff)
        Me.gbGreenFinal.Location = New System.Drawing.Point(8, 19)
        Me.gbGreenFinal.Name = "gbGreenFinal"
        Me.gbGreenFinal.Size = New System.Drawing.Size(116, 38)
        Me.gbGreenFinal.TabIndex = 0
        Me.gbGreenFinal.TabStop = False
        Me.gbGreenFinal.Text = "Final LED State"
        '
        'rbGreenFinalOn
        '
        Me.rbGreenFinalOn.AutoSize = True
        Me.rbGreenFinalOn.Location = New System.Drawing.Point(11, 15)
        Me.rbGreenFinalOn.Name = "rbGreenFinalOn"
        Me.rbGreenFinalOn.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenFinalOn.TabIndex = 22
        Me.rbGreenFinalOn.TabStop = True
        Me.rbGreenFinalOn.Text = "On"
        Me.rbGreenFinalOn.UseVisualStyleBackColor = True
        '
        'rbGreenFinalOff
        '
        Me.rbGreenFinalOff.AutoSize = True
        Me.rbGreenFinalOff.Location = New System.Drawing.Point(56, 15)
        Me.rbGreenFinalOff.Name = "rbGreenFinalOff"
        Me.rbGreenFinalOff.Size = New System.Drawing.Size(39, 17)
        Me.rbGreenFinalOff.TabIndex = 23
        Me.rbGreenFinalOff.TabStop = True
        Me.rbGreenFinalOff.Text = "Off"
        Me.rbGreenFinalOff.UseVisualStyleBackColor = True
        '
        'gbBlinkDuration
        '
        Me.gbBlinkDuration.Controls.Add(Me.bSetLED)
        Me.gbBlinkDuration.Controls.Add(Me.gbLinktoBuzz)
        Me.gbBlinkDuration.Controls.Add(Me.Label6)
        Me.gbBlinkDuration.Controls.Add(Me.tRepeat)
        Me.gbBlinkDuration.Controls.Add(Me.gbT2)
        Me.gbBlinkDuration.Controls.Add(Me.gbT1)
        Me.gbBlinkDuration.Location = New System.Drawing.Point(287, 12)
        Me.gbBlinkDuration.Name = "gbBlinkDuration"
        Me.gbBlinkDuration.Size = New System.Drawing.Size(244, 417)
        Me.gbBlinkDuration.TabIndex = 27
        Me.gbBlinkDuration.TabStop = False
        Me.gbBlinkDuration.Text = "Blinking Duration Control"
        '
        'bSetLED
        '
        Me.bSetLED.Location = New System.Drawing.Point(13, 359)
        Me.bSetLED.Name = "bSetLED"
        Me.bSetLED.Size = New System.Drawing.Size(215, 32)
        Me.bSetLED.TabIndex = 23
        Me.bSetLED.Text = "Set Bi-Color LED/Buzzer Control"
        Me.bSetLED.UseVisualStyleBackColor = True
        '
        'gbLinktoBuzz
        '
        Me.gbLinktoBuzz.Controls.Add(Me.rbLinkToBuzzOpt4)
        Me.gbLinktoBuzz.Controls.Add(Me.rbLinkToBuzzOpt3)
        Me.gbLinktoBuzz.Controls.Add(Me.rbLinkToBuzzOpt2)
        Me.gbLinktoBuzz.Controls.Add(Me.rbLinkToBuzzOpt1)
        Me.gbLinktoBuzz.Location = New System.Drawing.Point(13, 191)
        Me.gbLinktoBuzz.Name = "gbLinktoBuzz"
        Me.gbLinktoBuzz.Size = New System.Drawing.Size(215, 138)
        Me.gbLinktoBuzz.TabIndex = 4
        Me.gbLinktoBuzz.TabStop = False
        Me.gbLinktoBuzz.Text = "Link to Buzzer"
        '
        'rbLinkToBuzzOpt4
        '
        Me.rbLinkToBuzzOpt4.AutoSize = True
        Me.rbLinkToBuzzOpt4.Location = New System.Drawing.Point(22, 100)
        Me.rbLinkToBuzzOpt4.Name = "rbLinkToBuzzOpt4"
        Me.rbLinkToBuzzOpt4.Size = New System.Drawing.Size(118, 17)
        Me.rbLinkToBuzzOpt4.TabIndex = 3
        Me.rbLinkToBuzzOpt4.TabStop = True
        Me.rbLinkToBuzzOpt4.Text = "T1 and T2 Duration"
        Me.rbLinkToBuzzOpt4.UseVisualStyleBackColor = True
        '
        'rbLinkToBuzzOpt3
        '
        Me.rbLinkToBuzzOpt3.AutoSize = True
        Me.rbLinkToBuzzOpt3.Location = New System.Drawing.Point(22, 77)
        Me.rbLinkToBuzzOpt3.Name = "rbLinkToBuzzOpt3"
        Me.rbLinkToBuzzOpt3.Size = New System.Drawing.Size(81, 17)
        Me.rbLinkToBuzzOpt3.TabIndex = 2
        Me.rbLinkToBuzzOpt3.TabStop = True
        Me.rbLinkToBuzzOpt3.Text = "T2 Duration"
        Me.rbLinkToBuzzOpt3.UseVisualStyleBackColor = True
        '
        'rbLinkToBuzzOpt2
        '
        Me.rbLinkToBuzzOpt2.AutoSize = True
        Me.rbLinkToBuzzOpt2.Location = New System.Drawing.Point(22, 49)
        Me.rbLinkToBuzzOpt2.Name = "rbLinkToBuzzOpt2"
        Me.rbLinkToBuzzOpt2.Size = New System.Drawing.Size(81, 17)
        Me.rbLinkToBuzzOpt2.TabIndex = 1
        Me.rbLinkToBuzzOpt2.TabStop = True
        Me.rbLinkToBuzzOpt2.Text = "T1 Duration"
        Me.rbLinkToBuzzOpt2.UseVisualStyleBackColor = True
        '
        'rbLinkToBuzzOpt1
        '
        Me.rbLinkToBuzzOpt1.AutoSize = True
        Me.rbLinkToBuzzOpt1.Location = New System.Drawing.Point(22, 26)
        Me.rbLinkToBuzzOpt1.Name = "rbLinkToBuzzOpt1"
        Me.rbLinkToBuzzOpt1.Size = New System.Drawing.Size(74, 17)
        Me.rbLinkToBuzzOpt1.TabIndex = 0
        Me.rbLinkToBuzzOpt1.TabStop = True
        Me.rbLinkToBuzzOpt1.Text = "Buzzer Off"
        Me.rbLinkToBuzzOpt1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(19, 157)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 31)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Number of Repetitions: "
        '
        'tRepeat
        '
        Me.tRepeat.Location = New System.Drawing.Point(143, 154)
        Me.tRepeat.MaxLength = 2
        Me.tRepeat.Name = "tRepeat"
        Me.tRepeat.Size = New System.Drawing.Size(44, 20)
        Me.tRepeat.TabIndex = 3
        '
        'gbT2
        '
        Me.gbT2.Controls.Add(Me.Label4)
        Me.gbT2.Controls.Add(Me.tT2)
        Me.gbT2.Controls.Add(Me.Label5)
        Me.gbT2.Location = New System.Drawing.Point(13, 84)
        Me.gbT2.Name = "gbT2"
        Me.gbT2.Size = New System.Drawing.Size(215, 55)
        Me.gbT2.TabIndex = 1
        Me.gbT2.TabStop = False
        Me.gbT2.Text = "T2 Duration"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(146, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "x100 ms"
        '
        'tT2
        '
        Me.tT2.Location = New System.Drawing.Point(96, 21)
        Me.tT2.MaxLength = 2
        Me.tT2.Name = "tT2"
        Me.tT2.Size = New System.Drawing.Size(44, 20)
        Me.tT2.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 31)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Toggle Blinking State"
        '
        'gbT1
        '
        Me.gbT1.Controls.Add(Me.Label3)
        Me.gbT1.Controls.Add(Me.tT1)
        Me.gbT1.Controls.Add(Me.Label2)
        Me.gbT1.Location = New System.Drawing.Point(13, 23)
        Me.gbT1.Name = "gbT1"
        Me.gbT1.Size = New System.Drawing.Size(215, 55)
        Me.gbT1.TabIndex = 0
        Me.gbT1.TabStop = False
        Me.gbT1.Text = "T1 Duration"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(146, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "x100 ms"
        '
        'tT1
        '
        Me.tT1.Location = New System.Drawing.Point(96, 19)
        Me.tT1.MaxLength = 2
        Me.tT1.Name = "tT1"
        Me.tT1.Size = New System.Drawing.Size(44, 20)
        Me.tT1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 31)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Initial Blinking State"
        '
        'mMsg
        '
        Me.mMsg.Location = New System.Drawing.Point(537, 15)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(276, 385)
        Me.mMsg.TabIndex = 28
        Me.mMsg.Text = ""
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(537, 406)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(97, 23)
        Me.bClear.TabIndex = 29
        Me.bClear.Text = "Clear Screen"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(645, 406)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(83, 23)
        Me.bReset.TabIndex = 30
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(734, 406)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(84, 23)
        Me.bQuit.TabIndex = 31
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'bStatus
        '
        Me.bStatus.Location = New System.Drawing.Point(145, 104)
        Me.bStatus.Name = "bStatus"
        Me.bStatus.Size = New System.Drawing.Size(129, 28)
        Me.bStatus.TabIndex = 32
        Me.bStatus.Text = "Get Status"
        Me.bStatus.UseVisualStyleBackColor = True
        '
        'DeviceProgramming
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(825, 443)
        Me.Controls.Add(Me.bStatus)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.gbBlinkDuration)
        Me.Controls.Add(Me.gbGreen)
        Me.Controls.Add(Me.gbRed)
        Me.Controls.Add(Me.gbAntenna)
        Me.Controls.Add(Me.bGetFW)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DeviceProgramming"
        Me.Text = "ACR 122 Device Programming"
        Me.gbAntenna.ResumeLayout(False)
        Me.gbAntenna.PerformLayout()
        Me.gbRed.ResumeLayout(False)
        Me.gbRedBlinkMask.ResumeLayout(False)
        Me.gbRedBlinkMask.PerformLayout()
        Me.gbRedInit.ResumeLayout(False)
        Me.gbRedInit.PerformLayout()
        Me.gbRedStateMask.ResumeLayout(False)
        Me.gbRedStateMask.PerformLayout()
        Me.gbRedFinal.ResumeLayout(False)
        Me.gbRedFinal.PerformLayout()
        Me.gbGreen.ResumeLayout(False)
        Me.gbGreenBlinkMask.ResumeLayout(False)
        Me.gbGreenBlinkMask.PerformLayout()
        Me.gbGreenInit.ResumeLayout(False)
        Me.gbGreenInit.PerformLayout()
        Me.gbGreenStateMask.ResumeLayout(False)
        Me.gbGreenStateMask.PerformLayout()
        Me.gbGreenFinal.ResumeLayout(False)
        Me.gbGreenFinal.PerformLayout()
        Me.gbBlinkDuration.ResumeLayout(False)
        Me.gbBlinkDuration.PerformLayout()
        Me.gbLinktoBuzz.ResumeLayout(False)
        Me.gbLinktoBuzz.PerformLayout()
        Me.gbT2.ResumeLayout(False)
        Me.gbT2.PerformLayout()
        Me.gbT1.ResumeLayout(False)
        Me.gbT1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents bConnect As System.Windows.Forms.Button
    Private WithEvents bInit As System.Windows.Forms.Button
    Private WithEvents cbReader As System.Windows.Forms.ComboBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents bGetFW As System.Windows.Forms.Button
    Friend WithEvents rbAntOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbAntOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbAntenna As System.Windows.Forms.GroupBox
    Private WithEvents bSetAntenna As System.Windows.Forms.Button
    Friend WithEvents gbRed As System.Windows.Forms.GroupBox
    Friend WithEvents gbRedFinal As System.Windows.Forms.GroupBox
    Friend WithEvents rbRedFinalOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbRedFinalOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbRedStateMask As System.Windows.Forms.GroupBox
    Friend WithEvents rbRedStateMaskOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbRedStateMaskOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbRedInit As System.Windows.Forms.GroupBox
    Friend WithEvents rbRedInitOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbRedInitOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbRedBlinkMask As System.Windows.Forms.GroupBox
    Friend WithEvents rbRedBlinkMaskOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbRedBlinkMaskOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbGreen As System.Windows.Forms.GroupBox
    Friend WithEvents gbGreenBlinkMask As System.Windows.Forms.GroupBox
    Friend WithEvents rbGreenBlinkMaskOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbGreenBlinkMaskOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbGreenInit As System.Windows.Forms.GroupBox
    Friend WithEvents rbGreenInitOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbGreenInitOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbGreenStateMask As System.Windows.Forms.GroupBox
    Friend WithEvents rbGreenStateMaskOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbGreenStateMaskOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbGreenFinal As System.Windows.Forms.GroupBox
    Friend WithEvents rbGreenFinalOn As System.Windows.Forms.RadioButton
    Friend WithEvents rbGreenFinalOff As System.Windows.Forms.RadioButton
    Friend WithEvents gbBlinkDuration As System.Windows.Forms.GroupBox
    Friend WithEvents gbT1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tT1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents gbT2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tT2 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents tRepeat As System.Windows.Forms.TextBox
    Friend WithEvents gbLinktoBuzz As System.Windows.Forms.GroupBox
    Friend WithEvents rbLinkToBuzzOpt4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbLinkToBuzzOpt3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbLinkToBuzzOpt2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbLinkToBuzzOpt1 As System.Windows.Forms.RadioButton
    Private WithEvents bSetLED As System.Windows.Forms.Button
    Private WithEvents mMsg As System.Windows.Forms.RichTextBox
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents bStatus As System.Windows.Forms.Button

End Class
