<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetATR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GetATR))
        Me.bGetAtr = New System.Windows.Forms.Button
        Me.bConnect = New System.Windows.Forms.Button
        Me.bInit = New System.Windows.Forms.Button
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.bReset = New System.Windows.Forms.Button
        Me.bQuit = New System.Windows.Forms.Button
        Me.bClear = New System.Windows.Forms.Button
        Me.mMSg = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'bGetAtr
        '
        Me.bGetAtr.Location = New System.Drawing.Point(165, 101)
        Me.bGetAtr.Name = "bGetAtr"
        Me.bGetAtr.Size = New System.Drawing.Size(101, 23)
        Me.bGetAtr.TabIndex = 9
        Me.bGetAtr.Text = "Get ATR"
        Me.bGetAtr.UseVisualStyleBackColor = True
        '
        'bConnect
        '
        Me.bConnect.Location = New System.Drawing.Point(163, 72)
        Me.bConnect.Name = "bConnect"
        Me.bConnect.Size = New System.Drawing.Size(103, 23)
        Me.bConnect.TabIndex = 8
        Me.bConnect.Text = "Connect"
        Me.bConnect.UseVisualStyleBackColor = True
        '
        'bInit
        '
        Me.bInit.Location = New System.Drawing.Point(161, 39)
        Me.bInit.Name = "bInit"
        Me.bInit.Size = New System.Drawing.Size(105, 23)
        Me.bInit.TabIndex = 7
        Me.bInit.Text = "Initialize"
        Me.bInit.UseVisualStyleBackColor = True
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(94, 12)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(172, 21)
        Me.cbReader.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select Reader"
        '
        'bReset
        '
        Me.bReset.Location = New System.Drawing.Point(170, 259)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(94, 23)
        Me.bReset.TabIndex = 13
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = True
        '
        'bQuit
        '
        Me.bQuit.Location = New System.Drawing.Point(170, 288)
        Me.bQuit.Name = "bQuit"
        Me.bQuit.Size = New System.Drawing.Size(96, 23)
        Me.bQuit.TabIndex = 12
        Me.bQuit.Text = "Quit"
        Me.bQuit.UseVisualStyleBackColor = True
        '
        'bClear
        '
        Me.bClear.Location = New System.Drawing.Point(170, 230)
        Me.bClear.Name = "bClear"
        Me.bClear.Size = New System.Drawing.Size(96, 23)
        Me.bClear.TabIndex = 11
        Me.bClear.Text = "Clear Screen"
        Me.bClear.UseVisualStyleBackColor = True
        '
        'mMSg
        '
        Me.mMSg.Location = New System.Drawing.Point(285, 12)
        Me.mMSg.Name = "mMSg"
        Me.mMSg.Size = New System.Drawing.Size(355, 299)
        Me.mMSg.TabIndex = 14
        Me.mMSg.Text = ""
        '
        'GetATR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 336)
        Me.Controls.Add(Me.mMSg)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bQuit)
        Me.Controls.Add(Me.bClear)
        Me.Controls.Add(Me.bGetAtr)
        Me.Controls.Add(Me.bConnect)
        Me.Controls.Add(Me.bInit)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GetATR"
        Me.Text = "ACR 122 Get ATR"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bGetAtr As System.Windows.Forms.Button
    Friend WithEvents bConnect As System.Windows.Forms.Button
    Friend WithEvents bInit As System.Windows.Forms.Button
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bQuit As System.Windows.Forms.Button
    Friend WithEvents bClear As System.Windows.Forms.Button
    Friend WithEvents mMSg As System.Windows.Forms.RichTextBox

End Class
