<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PassiveSample
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PassiveSample))
        Me.Label1 = New System.Windows.Forms.Label
        Me.cbReader = New System.Windows.Forms.ComboBox
        Me.rbOutput = New System.Windows.Forms.RichTextBox
        Me.btnInit = New System.Windows.Forms.Button
        Me.btnConnect = New System.Windows.Forms.Button
        Me.btnPassive = New System.Windows.Forms.Button
        Me.RecvGroup = New System.Windows.Forms.GroupBox
        Me.tbData = New System.Windows.Forms.TextBox
        Me.btnQuit = New System.Windows.Forms.Button
        Me.btnReset = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.RecvGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Reader"
        '
        'cbReader
        '
        Me.cbReader.FormattingEnabled = True
        Me.cbReader.Location = New System.Drawing.Point(93, 6)
        Me.cbReader.Name = "cbReader"
        Me.cbReader.Size = New System.Drawing.Size(178, 21)
        Me.cbReader.TabIndex = 1
        '
        'rbOutput
        '
        Me.rbOutput.Location = New System.Drawing.Point(277, 6)
        Me.rbOutput.Name = "rbOutput"
        Me.rbOutput.Size = New System.Drawing.Size(357, 366)
        Me.rbOutput.TabIndex = 2
        Me.rbOutput.Text = ""
        '
        'btnInit
        '
        Me.btnInit.Location = New System.Drawing.Point(71, 33)
        Me.btnInit.Name = "btnInit"
        Me.btnInit.Size = New System.Drawing.Size(200, 27)
        Me.btnInit.TabIndex = 3
        Me.btnInit.Text = "Initialize"
        Me.btnInit.UseVisualStyleBackColor = True
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(71, 66)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(200, 27)
        Me.btnConnect.TabIndex = 4
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnPassive
        '
        Me.btnPassive.Location = New System.Drawing.Point(71, 99)
        Me.btnPassive.Name = "btnPassive"
        Me.btnPassive.Size = New System.Drawing.Size(200, 27)
        Me.btnPassive.TabIndex = 5
        Me.btnPassive.Text = "Set Passive Mode and Receive Data"
        Me.btnPassive.UseVisualStyleBackColor = True
        '
        'RecvGroup
        '
        Me.RecvGroup.Controls.Add(Me.tbData)
        Me.RecvGroup.Location = New System.Drawing.Point(15, 132)
        Me.RecvGroup.Name = "RecvGroup"
        Me.RecvGroup.Size = New System.Drawing.Size(256, 123)
        Me.RecvGroup.TabIndex = 6
        Me.RecvGroup.TabStop = False
        Me.RecvGroup.Text = "Receive Data"
        '
        'tbData
        '
        Me.tbData.Location = New System.Drawing.Point(6, 19)
        Me.tbData.Multiline = True
        Me.tbData.Name = "tbData"
        Me.tbData.Size = New System.Drawing.Size(244, 98)
        Me.tbData.TabIndex = 0
        '
        'btnQuit
        '
        Me.btnQuit.Location = New System.Drawing.Point(150, 344)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(121, 27)
        Me.btnQuit.TabIndex = 9
        Me.btnQuit.Text = "Quit"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(150, 311)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(121, 27)
        Me.btnReset.TabIndex = 8
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(150, 278)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(121, 27)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear Output"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'PassiveSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 382)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.RecvGroup)
        Me.Controls.Add(Me.btnPassive)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.btnInit)
        Me.Controls.Add(Me.rbOutput)
        Me.Controls.Add(Me.cbReader)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PassiveSample"
        Me.Text = "Passive Device Sample"
        Me.RecvGroup.ResumeLayout(False)
        Me.RecvGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbReader As System.Windows.Forms.ComboBox
    Friend WithEvents rbOutput As System.Windows.Forms.RichTextBox
    Friend WithEvents btnInit As System.Windows.Forms.Button
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents btnPassive As System.Windows.Forms.Button
    Friend WithEvents RecvGroup As System.Windows.Forms.GroupBox
    Friend WithEvents tbData As System.Windows.Forms.TextBox
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button

End Class
