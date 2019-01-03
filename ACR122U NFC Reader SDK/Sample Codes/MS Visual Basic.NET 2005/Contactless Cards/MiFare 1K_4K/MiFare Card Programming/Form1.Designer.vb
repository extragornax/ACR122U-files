<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MiFareCardProg
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
        Me.bValRes = New System.Windows.Forms.Button
        Me.bValRead = New System.Windows.Forms.Button
        Me.bValInc = New System.Windows.Forms.Button
        Me.gbAuth = New System.Windows.Forms.GroupBox
        Me.bAuth = New System.Windows.Forms.Button
        Me.tAuthenKeyNum = New System.Windows.Forms.TextBox
        Me.tBlkNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.gbKType = New System.Windows.Forms.GroupBox
        Me.rbKType2 = New System.Windows.Forms.RadioButton
        Me.rbKType1 = New System.Windows.Forms.RadioButton
        Me.bValDec = New System.Windows.Forms.Button
        Me.bValStor = New System.Windows.Forms.Button
        Me.gbBinOps = New System.Windows.Forms.GroupBox
        Me.bBinUpd = New System.Windows.Forms.Button
        Me.bBinRead = New System.Windows.Forms.Button
        Me.tBinData = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.tBinLen = New System.Windows.Forms.TextBox
        Me.tBinBlk = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.tValTar = New System.Windows.Forms.TextBox
        Me.gbValBlk = New System.Windows.Forms.GroupBox
        Me.tValSrc = New System.Windows.Forms.TextBox
        Me.tValBlk = New System.Windows.Forms.TextBox
        Me.tValAmt = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.bReset = New System.Windows.Forms.Button
        Me.bClear = New System.Windows.Forms.Button
        Me.bLoadKey = New System.Windows.Forms.Button
        Me.tKey6 = New System.Windows.Forms.TextBox
        Me.tKey5 = New System.Windows.Forms.TextBox
        Me.tKey4 = New System.Windows.Forms.TextBox
        Me.tKey3 = New System.Windows.Forms.TextBox
        Me.tKey1 = New System.Windows.Forms.TextBox
        Me.tKey2 = New System.Windows.Forms.TextBox
        Me.bConnect = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.tKeyNum = New System.Windows.Forms.TextBox
        Me.gbLoadKeys = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.bQuit = New System.Windows.Forms.Button
        Me.mMsg = New System.Windows.Forms.RichTextBox
        Me.gbAuth.SuspendLayout()
        Me.gbKType.SuspendLayout()
        Me.gbBinOps.SuspendLayout()
        Me.gbValBlk.SuspendLayout()
        Me.gbLoadKeys.SuspendLayout()
        Me.SuspendLayout()
        '
        'bValRes
        '
        Me.bValRes.Location = New System.Drawing.Point(237, 133)
        Me.bValRes.Name = "bValRes"
        Me.bValRes.Size = New System.Drawing.Size(101, 23)
        Me.bValRes.TabIndex = 27
        Me.bValRes.Text = "Restore Value"
        Me.bValRes.UseVisualStyleBackColor = True
        '
        'bValRead
        '
        Me.bValRead.Location = New System.Drawing.Point(237, 104)
        Me.bValRead.Name = "bValRead"
        Me.bValRead.Size = New System.Drawing.Size(101, 23)
        Me.bValRead.TabIndex = 26
        Me.bValRead.Text = "Read Value"
        Me.bValRead.UseVisualStyleBackColor = True
        '
        'bValInc
        '
        Me.bValInc.Location = New System.Drawing.Point(237, 50)
        Me.bValInc.Name = "bValInc"
        Me.bValInc.Size = New System.Drawing.Size(101, 23)
        Me.bValInc.TabIndex = 24
        Me.bValInc.Text = "Increment"
        Me.bValInc.UseVisualStyleBackColor = True
        '
        'gbAuth
        '
        Me.gbAuth.Controls.Add(Me.bAuth)
        Me.gbAuth.Controls.Add(Me.tAuthenKeyNum)
        Me.gbAuth.Controls.Add(Me.tBlkNo)
        Me.gbAuth.Controls.Add(Me.Label5)
        Me.gbAuth.Controls.Add(Me.Label4)
        Me.gbAuth.Controls.Add(Me.gbKType)
        Me.gbAuth.Location = New System.Drawing.Point(12, 231)
        Me.gbAuth.Name = "gbAuth"
        Me.gbAuth.Size = New System.Drawing.Size(297, 146)
        Me.gbAuth.TabIndex = 31
        Me.gbAuth.TabStop = False
        Me.gbAuth.Text = "Authentication Function"
        '
        'bAuth
        '
        Me.bAuth.Location = New System.Drawing.Point(169, 111)
        Me.bAuth.Name = "bAuth"
        Me.bAuth.Size = New System.Drawing.Size(118, 23)
        Me.bAuth.TabIndex = 13
        Me.bAuth.Text = "Authenticate"
        Me.bAuth.UseVisualStyleBackColor = True
        '
        'tAuthenKeyNum
        '
        Me.tAuthenKeyNum.Location = New System.Drawing.Point(109, 52)
        Me.tAuthenKeyNum.MaxLength = 2
        Me.tAuthenKeyNum.Name = "tAuthenKeyNum"
        Me.tAuthenKeyNum.Size = New System.Drawing.Size(25, 20)
        Me.tAuthenKeyNum.TabIndex = 6
        '
        'tBlkNo
        '
        Me.tBlkNo.Location = New System.Drawing.Point(109, 25)
        Me.tBlkNo.MaxLength = 3
        Me.tBlkNo.Name = "tBlkNo"
        Me.tBlkNo.Size = New System.Drawing.Size(25, 20)
        Me.tBlkNo.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 52)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Key Store Number"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Block No (Dec)"
        '
        'gbKType
        '
        Me.gbKType.Controls.Add(Me.rbKType2)
        Me.gbKType.Controls.Add(Me.rbKType1)
        Me.gbKType.Location = New System.Drawing.Point(169, 9)
        Me.gbKType.Name = "gbKType"
        Me.gbKType.Size = New System.Drawing.Size(109, 86)
        Me.gbKType.TabIndex = 1
        Me.gbKType.TabStop = False
        Me.gbKType.Text = "Key Type"
        '
        'rbKType2
        '
        Me.rbKType2.AutoSize = True
        Me.rbKType2.Location = New System.Drawing.Point(16, 53)
        Me.rbKType2.Name = "rbKType2"
        Me.rbKType2.Size = New System.Drawing.Size(53, 17)
        Me.rbKType2.TabIndex = 2
        Me.rbKType2.TabStop = True
        Me.rbKType2.Text = "Key B"
        Me.rbKType2.UseVisualStyleBackColor = True
        '
        'rbKType1
        '
        Me.rbKType1.AutoSize = True
        Me.rbKType1.Location = New System.Drawing.Point(16, 19)
        Me.rbKType1.Name = "rbKType1"
        Me.rbKType1.Size = New System.Drawing.Size(53, 17)
        Me.rbKType1.TabIndex = 1
        Me.rbKType1.TabStop = True
        Me.rbKType1.Text = "Key A"
        Me.rbKType1.UseVisualStyleBackColor = True
        '
        'bValDec
        '
        Me.bValDec.Location = New System.Drawing.Point(237, 76)
        Me.bValDec.Name = "bValDec"
        Me.bValDec.Size = New System.Drawing.Size(101, 23)
        Me.bValDec.TabIndex = 25
        Me.bValDec.Text = "Decrement"
        Me.bValDec.UseVisualStyleBackColor = True
        '
        'bValStor
        '
        Me.bValStor.Location = New System.Drawing.Point(237, 22)
        Me.bValStor.Name = "bValStor"
        Me.bValStor.Size = New System.Drawing.Size(101, 23)
        Me.bValStor.TabIndex = 23
        Me.bValStor.Text = "Store Value"
        Me.bValStor.UseVisualStyleBackColor = True
        '
        'gbBinOps
        '
        Me.gbBinOps.Controls.Add(Me.bBinUpd)
        Me.gbBinOps.Controls.Add(Me.bBinRead)
        Me.gbBinOps.Controls.Add(Me.tBinData)
        Me.gbBinOps.Controls.Add(Me.Label9)
        Me.gbBinOps.Controls.Add(Me.tBinLen)
        Me.gbBinOps.Controls.Add(Me.tBinBlk)
        Me.gbBinOps.Controls.Add(Me.Label8)
        Me.gbBinOps.Controls.Add(Me.Label7)
        Me.gbBinOps.Location = New System.Drawing.Point(12, 383)
        Me.gbBinOps.Name = "gbBinOps"
        Me.gbBinOps.Size = New System.Drawing.Size(297, 135)
        Me.gbBinOps.TabIndex = 32
        Me.gbBinOps.TabStop = False
        Me.gbBinOps.Text = "Binary Block Functions"
        '
        'bBinUpd
        '
        Me.bBinUpd.Location = New System.Drawing.Point(140, 106)
        Me.bBinUpd.Name = "bBinUpd"
        Me.bBinUpd.Size = New System.Drawing.Size(116, 23)
        Me.bBinUpd.TabIndex = 19
        Me.bBinUpd.Text = "Update Block"
        Me.bBinUpd.UseVisualStyleBackColor = True
        '
        'bBinRead
        '
        Me.bBinRead.Location = New System.Drawing.Point(27, 106)
        Me.bBinRead.Name = "bBinRead"
        Me.bBinRead.Size = New System.Drawing.Size(107, 23)
        Me.bBinRead.TabIndex = 18
        Me.bBinRead.Text = "Read Block"
        Me.bBinRead.UseVisualStyleBackColor = True
        '
        'tBinData
        '
        Me.tBinData.Location = New System.Drawing.Point(12, 78)
        Me.tBinData.Name = "tBinData"
        Me.tBinData.Size = New System.Drawing.Size(252, 20)
        Me.tBinData.TabIndex = 17
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Data (Text)"
        '
        'tBinLen
        '
        Me.tBinLen.Location = New System.Drawing.Point(231, 25)
        Me.tBinLen.MaxLength = 2
        Me.tBinLen.Name = "tBinLen"
        Me.tBinLen.Size = New System.Drawing.Size(33, 20)
        Me.tBinLen.TabIndex = 15
        '
        'tBinBlk
        '
        Me.tBinBlk.Location = New System.Drawing.Point(101, 25)
        Me.tBinBlk.MaxLength = 2
        Me.tBinBlk.Name = "tBinBlk"
        Me.tBinBlk.Size = New System.Drawing.Size(33, 20)
        Me.tBinBlk.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(156, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 13)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Length (Dec)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 28)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Start Block (Dec)"
        '
        'tValTar
        '
        Me.tValTar.Location = New System.Drawing.Point(96, 104)
        Me.tValTar.MaxLength = 2
        Me.tValTar.Name = "tValTar"
        Me.tValTar.Size = New System.Drawing.Size(33, 20)
        Me.tValTar.TabIndex = 22
        '
        'gbValBlk
        '
        Me.gbValBlk.Controls.Add(Me.bValRes)
        Me.gbValBlk.Controls.Add(Me.bValRead)
        Me.gbValBlk.Controls.Add(Me.bValDec)
        Me.gbValBlk.Controls.Add(Me.bValInc)
        Me.gbValBlk.Controls.Add(Me.bValStor)
        Me.gbValBlk.Controls.Add(Me.tValTar)
        Me.gbValBlk.Controls.Add(Me.tValSrc)
        Me.gbValBlk.Controls.Add(Me.tValBlk)
        Me.gbValBlk.Controls.Add(Me.tValAmt)
        Me.gbValBlk.Controls.Add(Me.Label13)
        Me.gbValBlk.Controls.Add(Me.Label12)
        Me.gbValBlk.Controls.Add(Me.Label11)
        Me.gbValBlk.Controls.Add(Me.Label10)
        Me.gbValBlk.Location = New System.Drawing.Point(315, 14)
        Me.gbValBlk.Name = "gbValBlk"
        Me.gbValBlk.Size = New System.Drawing.Size(344, 168)
        Me.gbValBlk.TabIndex = 33
        Me.gbValBlk.TabStop = False
        Me.gbValBlk.Text = "Value Block Functions"
        '
        'tValSrc
        '
        Me.tValSrc.Location = New System.Drawing.Point(96, 79)
        Me.tValSrc.MaxLength = 2
        Me.tValSrc.Name = "tValSrc"
        Me.tValSrc.Size = New System.Drawing.Size(33, 20)
        Me.tValSrc.TabIndex = 21
        '
        'tValBlk
        '
        Me.tValBlk.Location = New System.Drawing.Point(96, 53)
        Me.tValBlk.MaxLength = 2
        Me.tValBlk.Name = "tValBlk"
        Me.tValBlk.Size = New System.Drawing.Size(33, 20)
        Me.tValBlk.TabIndex = 20
        '
        'tValAmt
        '
        Me.tValAmt.Location = New System.Drawing.Point(96, 24)
        Me.tValAmt.Name = "tValAmt"
        Me.tValAmt.Size = New System.Drawing.Size(111, 20)
        Me.tValAmt.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(17, 111)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Target Block"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(17, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Source Block"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(17, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Block No."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(17, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Value Amount"
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(433, 495)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(113, 23)
        Me.bReset.TabIndex = 36
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(315, 495)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(102, 23)
        Me.bClear.TabIndex = 35
        Me.bClear.Text = "Clear"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'bLoadKey
        '
        Me.bLoadKey.Location = New System.Drawing.Point(169, 87)
        Me.bLoadKey.Name = "bLoadKey"
        Me.bLoadKey.Size = New System.Drawing.Size(118, 23)
        Me.bLoadKey.TabIndex = 11
        Me.bLoadKey.Text = "Load Keys"
        Me.bLoadKey.UseVisualStyleBackColor = True
        '
        'tKey6
        '
        Me.tKey6.Location = New System.Drawing.Point(262, 57)
        Me.tKey6.MaxLength = 2
        Me.tKey6.Name = "tKey6"
        Me.tKey6.Size = New System.Drawing.Size(25, 20)
        Me.tKey6.TabIndex = 10
        '
        'tKey5
        '
        Me.tKey5.Location = New System.Drawing.Point(231, 57)
        Me.tKey5.MaxLength = 2
        Me.tKey5.Name = "tKey5"
        Me.tKey5.Size = New System.Drawing.Size(25, 20)
        Me.tKey5.TabIndex = 9
        '
        'tKey4
        '
        Me.tKey4.Location = New System.Drawing.Point(200, 57)
        Me.tKey4.MaxLength = 2
        Me.tKey4.Name = "tKey4"
        Me.tKey4.Size = New System.Drawing.Size(25, 20)
        Me.tKey4.TabIndex = 8
        '
        'tKey3
        '
        Me.tKey3.Location = New System.Drawing.Point(169, 57)
        Me.tKey3.MaxLength = 2
        Me.tKey3.Name = "tKey3"
        Me.tKey3.Size = New System.Drawing.Size(25, 20)
        Me.tKey3.TabIndex = 7
        '
        'tKey1
        '
        Me.tKey1.Location = New System.Drawing.Point(107, 57)
        Me.tKey1.MaxLength = 2
        Me.tKey1.Name = "tKey1"
        Me.tKey1.Size = New System.Drawing.Size(25, 20)
        Me.tKey1.TabIndex = 6
        '
        'tKey2
        '
        Me.tKey2.Location = New System.Drawing.Point(138, 57)
        Me.tKey2.MaxLength = 2
        Me.tKey2.Name = "tKey2"
        Me.tKey2.Size = New System.Drawing.Size(25, 20)
        Me.tKey2.TabIndex = 5
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(191, 76)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(117, 23)
        Me.bConnect.TabIndex = 29
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(191, 47)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(117, 23)
        Me.bInit.TabIndex = 28
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'tKeyNum
        '
        Me.tKeyNum.Location = New System.Drawing.Point(107, 23)
        Me.tKeyNum.MaxLength = 2
        Me.tKeyNum.Name = "tKeyNum"
        Me.tKeyNum.Size = New System.Drawing.Size(25, 20)
        Me.tKeyNum.TabIndex = 4
        '
        'gbLoadKeys
        '
        Me.gbLoadKeys.Controls.Add(Me.bLoadKey)
        Me.gbLoadKeys.Controls.Add(Me.tKey6)
        Me.gbLoadKeys.Controls.Add(Me.tKey5)
        Me.gbLoadKeys.Controls.Add(Me.tKey4)
        Me.gbLoadKeys.Controls.Add(Me.tKey3)
        Me.gbLoadKeys.Controls.Add(Me.tKey1)
        Me.gbLoadKeys.Controls.Add(Me.tKey2)
        Me.gbLoadKeys.Controls.Add(Me.tKeyNum)
        Me.gbLoadKeys.Controls.Add(Me.Label3)
        Me.gbLoadKeys.Controls.Add(Me.Label2)
        Me.gbLoadKeys.Location = New System.Drawing.Point(12, 105)
        Me.gbLoadKeys.Name = "gbLoadKeys"
        Me.gbLoadKeys.Size = New System.Drawing.Size(296, 120)
        Me.gbLoadKeys.TabIndex = 30
        Me.gbLoadKeys.TabStop = False
        Me.gbLoadKeys.Text = "Load Authentication Keys to Device"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Key Value Input"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Key Store No."
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 20)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(216, 21)
        Me.cbReader.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Select Reader"
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(558, 495)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(101, 23)
        Me.bQuit.TabIndex = 37
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'mMsg
        '
        Me.mMsg.Location = New System.Drawing.Point(315, 196)
        Me.mMsg.Name = "mMsg"
        Me.mMsg.Size = New System.Drawing.Size(344, 293)
        Me.mMsg.TabIndex = 38
        Me.mMsg.Text = ""
        '
        'MiFareCardProg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 530)
        Me.Controls.Add(Me.mMsg)
        Me.Controls.Add(Me.gbAuth)
        Me.Controls.Add(Me.gbBinOps)
        Me.Controls.Add(Me.gbValBlk)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.gbLoadKeys)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.bQuit)
        Me.Name = "MiFareCardProg"
        Me.Text = "MiFare Card Programming"
        Me.gbAuth.ResumeLayout(False)
        Me.gbAuth.PerformLayout()
        Me.gbKType.ResumeLayout(False)
        Me.gbKType.PerformLayout()
        Me.gbBinOps.ResumeLayout(False)
        Me.gbBinOps.PerformLayout()
        Me.gbValBlk.ResumeLayout(False)
        Me.gbValBlk.PerformLayout()
        Me.gbLoadKeys.ResumeLayout(False)
        Me.gbLoadKeys.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bValRes As System.Windows.Forms.Button
    Friend WithEvents bValRead As System.Windows.Forms.Button
    Friend WithEvents bValInc As System.Windows.Forms.Button
    Friend WithEvents gbAuth As System.Windows.Forms.GroupBox
    Friend WithEvents bAuth As System.Windows.Forms.Button
    Friend WithEvents tAuthenKeyNum As System.Windows.Forms.TextBox
    Friend WithEvents tBlkNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents gbKType As System.Windows.Forms.GroupBox
    Friend WithEvents rbKType2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbKType1 As System.Windows.Forms.RadioButton
    Friend WithEvents bValDec As System.Windows.Forms.Button
    Friend WithEvents bValStor As System.Windows.Forms.Button
    Friend WithEvents gbBinOps As System.Windows.Forms.GroupBox
    Friend WithEvents bBinUpd As System.Windows.Forms.Button
    Friend WithEvents bBinRead As System.Windows.Forms.Button
    Friend WithEvents tBinData As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tBinLen As System.Windows.Forms.TextBox
    Friend WithEvents tBinBlk As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tValTar As System.Windows.Forms.TextBox
    Friend WithEvents gbValBlk As System.Windows.Forms.GroupBox
    Friend WithEvents tValSrc As System.Windows.Forms.TextBox
    Friend WithEvents tValBlk As System.Windows.Forms.TextBox
    Friend WithEvents tValAmt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents bLoadKey As System.Windows.Forms.Button
    Friend WithEvents tKey6 As System.Windows.Forms.TextBox
    Friend WithEvents tKey5 As System.Windows.Forms.TextBox
    Friend WithEvents tKey4 As System.Windows.Forms.TextBox
    Friend WithEvents tKey3 As System.Windows.Forms.TextBox
    Friend WithEvents tKey1 As System.Windows.Forms.TextBox
    Friend WithEvents tKey2 As System.Windows.Forms.TextBox
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents tKeyNum As System.Windows.Forms.TextBox
    Friend WithEvents gbLoadKeys As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents mMsg As System.Windows.Forms.RichTextBox

End Class
