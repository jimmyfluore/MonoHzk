<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HzkExporter
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Canvas = New System.Windows.Forms.PictureBox()
        Me.SelectFontCom = New System.Windows.Forms.Button()
        Me.HzkSizeCombo = New System.Windows.Forms.ComboBox()
        Me.UpCom = New System.Windows.Forms.Button()
        Me.DownCom = New System.Windows.Forms.Button()
        Me.LeftCom = New System.Windows.Forms.Button()
        Me.RightCom = New System.Windows.Forms.Button()
        Me.ExportAscCom = New System.Windows.Forms.Button()
        Me.ExportHzkCom = New System.Windows.Forms.Button()
        Me.SampleText = New System.Windows.Forms.TextBox()
        Me.HzkFontDialog = New System.Windows.Forms.FontDialog()
        Me.HzkSaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.ExportHzkBGW = New System.ComponentModel.BackgroundWorker()
        Me.ExportAscBGW = New System.ComponentModel.BackgroundWorker()
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Canvas
        '
        Me.Canvas.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Canvas.Location = New System.Drawing.Point(10, 36)
        Me.Canvas.Margin = New System.Windows.Forms.Padding(2)
        Me.Canvas.Name = "Canvas"
        Me.Canvas.Size = New System.Drawing.Size(65, 65)
        Me.Canvas.TabIndex = 0
        Me.Canvas.TabStop = False
        '
        'SelectFontCom
        '
        Me.SelectFontCom.Location = New System.Drawing.Point(10, 6)
        Me.SelectFontCom.Margin = New System.Windows.Forms.Padding(2)
        Me.SelectFontCom.Name = "SelectFontCom"
        Me.SelectFontCom.Size = New System.Drawing.Size(124, 26)
        Me.SelectFontCom.TabIndex = 1
        Me.SelectFontCom.Text = "选择字体"
        Me.SelectFontCom.UseVisualStyleBackColor = True
        '
        'HzkSizeCombo
        '
        Me.HzkSizeCombo.FormattingEnabled = True
        Me.HzkSizeCombo.Items.AddRange(New Object() {"16", "24", "32", "36", "40", "48", "56", "64"})
        Me.HzkSizeCombo.Location = New System.Drawing.Point(79, 36)
        Me.HzkSizeCombo.Margin = New System.Windows.Forms.Padding(2)
        Me.HzkSizeCombo.Name = "HzkSizeCombo"
        Me.HzkSizeCombo.Size = New System.Drawing.Size(72, 20)
        Me.HzkSizeCombo.TabIndex = 2
        '
        'UpCom
        '
        Me.UpCom.Location = New System.Drawing.Point(149, 6)
        Me.UpCom.Margin = New System.Windows.Forms.Padding(2)
        Me.UpCom.Name = "UpCom"
        Me.UpCom.Size = New System.Drawing.Size(31, 26)
        Me.UpCom.TabIndex = 3
        Me.UpCom.Text = "↑"
        Me.UpCom.UseVisualStyleBackColor = True
        '
        'DownCom
        '
        Me.DownCom.Location = New System.Drawing.Point(184, 6)
        Me.DownCom.Margin = New System.Windows.Forms.Padding(2)
        Me.DownCom.Name = "DownCom"
        Me.DownCom.Size = New System.Drawing.Size(31, 26)
        Me.DownCom.TabIndex = 4
        Me.DownCom.Text = "↓"
        Me.DownCom.UseVisualStyleBackColor = True
        '
        'LeftCom
        '
        Me.LeftCom.Location = New System.Drawing.Point(219, 6)
        Me.LeftCom.Margin = New System.Windows.Forms.Padding(2)
        Me.LeftCom.Name = "LeftCom"
        Me.LeftCom.Size = New System.Drawing.Size(31, 26)
        Me.LeftCom.TabIndex = 5
        Me.LeftCom.Text = "←"
        Me.LeftCom.UseVisualStyleBackColor = True
        '
        'RightCom
        '
        Me.RightCom.Location = New System.Drawing.Point(252, 6)
        Me.RightCom.Margin = New System.Windows.Forms.Padding(2)
        Me.RightCom.Name = "RightCom"
        Me.RightCom.Size = New System.Drawing.Size(31, 26)
        Me.RightCom.TabIndex = 6
        Me.RightCom.Text = "→"
        Me.RightCom.UseVisualStyleBackColor = True
        '
        'ExportAscCom
        '
        Me.ExportAscCom.Location = New System.Drawing.Point(159, 45)
        Me.ExportAscCom.Margin = New System.Windows.Forms.Padding(2)
        Me.ExportAscCom.Name = "ExportAscCom"
        Me.ExportAscCom.Size = New System.Drawing.Size(124, 26)
        Me.ExportAscCom.TabIndex = 7
        Me.ExportAscCom.Text = "导出半角字库"
        Me.ExportAscCom.UseVisualStyleBackColor = True
        '
        'ExportHzkCom
        '
        Me.ExportHzkCom.Location = New System.Drawing.Point(159, 75)
        Me.ExportHzkCom.Margin = New System.Windows.Forms.Padding(2)
        Me.ExportHzkCom.Name = "ExportHzkCom"
        Me.ExportHzkCom.Size = New System.Drawing.Size(124, 26)
        Me.ExportHzkCom.TabIndex = 8
        Me.ExportHzkCom.Text = "导出汉字库"
        Me.ExportHzkCom.UseVisualStyleBackColor = True
        '
        'SampleText
        '
        Me.SampleText.Location = New System.Drawing.Point(79, 60)
        Me.SampleText.Margin = New System.Windows.Forms.Padding(2)
        Me.SampleText.Name = "SampleText"
        Me.SampleText.Size = New System.Drawing.Size(52, 21)
        Me.SampleText.TabIndex = 9
        '
        'HzkFontDialog
        '
        Me.HzkFontDialog.AllowVerticalFonts = False
        '
        'HzkSaveFileDialog
        '
        Me.HzkSaveFileDialog.DefaultExt = "bin"
        Me.HzkSaveFileDialog.Filter = "汉字库二进制数据|*.bin"
        '
        'ExportHzkBGW
        '
        Me.ExportHzkBGW.WorkerReportsProgress = True
        Me.ExportHzkBGW.WorkerSupportsCancellation = True
        '
        'ExportAscBGW
        '
        Me.ExportAscBGW.WorkerReportsProgress = True
        Me.ExportAscBGW.WorkerSupportsCancellation = True
        '
        'HzkExporter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(294, 112)
        Me.Controls.Add(Me.SampleText)
        Me.Controls.Add(Me.ExportHzkCom)
        Me.Controls.Add(Me.ExportAscCom)
        Me.Controls.Add(Me.RightCom)
        Me.Controls.Add(Me.LeftCom)
        Me.Controls.Add(Me.DownCom)
        Me.Controls.Add(Me.UpCom)
        Me.Controls.Add(Me.HzkSizeCombo)
        Me.Controls.Add(Me.SelectFontCom)
        Me.Controls.Add(Me.Canvas)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "HzkExporter"
        Me.Text = "HzkExporter"
        CType(Me.Canvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Canvas As PictureBox
    Friend WithEvents SelectFontCom As Button
    Friend WithEvents HzkSizeCombo As ComboBox
    Friend WithEvents UpCom As Button
    Friend WithEvents DownCom As Button
    Friend WithEvents LeftCom As Button
    Friend WithEvents RightCom As Button
    Friend WithEvents ExportAscCom As Button
    Friend WithEvents ExportHzkCom As Button
    Friend WithEvents SampleText As TextBox
    Friend WithEvents HzkFontDialog As FontDialog
    Friend WithEvents HzkSaveFileDialog As SaveFileDialog
    Friend WithEvents ExportHzkBGW As System.ComponentModel.BackgroundWorker
    Friend WithEvents ExportAscBGW As System.ComponentModel.BackgroundWorker
End Class
