<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActiveSample
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ActiveSample))
        Me.btnQuit = New System.Windows.Forms.Button
        Me.btnReset = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.SendGroup = New System.Windows.Forms.GroupBox
        Me.tbData = New System.Windows.Forms.TextBox
        Me.btnActive = New System.Windows.Forms.Button
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnInit = New System.Windows.Forms.Button
        Me.rbOutput = New System.Windows.Forms.RichTextBox
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SendGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(144, 350)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(121, 27)
        Me.btnQuit.TabIndex = 19
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(144, 317)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(121, 27)
        Me.btnReset.TabIndex = 18
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(144, 284)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(121, 27)
        Me.btnClear.TabIndex = 17
        Me.btnClear.Text = "Clear Output"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'SendGroup
        '
        Me.SendGroup.Controls.Add(Me.tbData)
        Me.SendGroup.Location = New System.Drawing.Point(9, 138)
        Me.SendGroup.Name = "SendGroup"
        Me.SendGroup.Size = New System.Drawing.Size(256, 140)
        Me.SendGroup.TabIndex = 16
        Me.SendGroup.TabStop = False
        Me.SendGroup.Text = "Send Data"
        '
        'tbData
        '
        Me.tbData.Location = New System.Drawing.Point(6, 19)
        Me.tbData.Multiline = True
        Me.tbData.Name = "tbData"
        Me.tbData.Size = New System.Drawing.Size(244, 115)
        Me.tbData.TabIndex = 0
        '
        'btnActive
        '
        Me.btnActive.Location = New System.Drawing.Point(87, 105)
        Me.btnActive.Name = "btnActive"
        Me.btnActive.Size = New System.Drawing.Size(178, 27)
        Me.btnActive.TabIndex = 15
        Me.btnActive.Text = "Set Active Mode and Send Data"
        Me.btnActive.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(87, 72)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(178, 27)
        Me.btnConnect.TabIndex = 14
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnInit
        '
        Me.btnInit.Location = New System.Drawing.Point(87, 39)
        Me.btnInit.Name = "btnInit"
        Me.btnInit.Size = New System.Drawing.Size(178, 27)
        Me.btnInit.TabIndex = 13
        Me.btnInit.Text = "Initialize"
        Me.btnInit.UseVisualStyleBackColor = True
        '
        'rbOutput
        '
        Me.rbOutput.Location = New System.Drawing.Point(271, 12)
        Me.rbOutput.Name = "rbOutput"
        Me.rbOutput.Size = New System.Drawing.Size(357, 366)
        Me.rbOutput.TabIndex = 12
        Me.rbOutput.Text = ""
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(87, 12)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(178, 21)
        Me.cbReader.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Select Reader"
        '
        'ActiveSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 386)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.SendGroup)
        Me.Controls.Add(Me.btnActive)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnInit)
        Me.Controls.Add(Me.rbOutput)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ActiveSample"
        Me.Text = "Active Device Sample"
        Me.SendGroup.ResumeLayout(False)
        Me.SendGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents SendGroup As System.Windows.Forms.GroupBox
    Friend WithEvents tbData As System.Windows.Forms.TextBox
    Friend WithEvents btnActive As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnInit As System.Windows.Forms.Button
    Friend WithEvents rbOutput As System.Windows.Forms.RichTextBox
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
