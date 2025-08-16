Namespace Sample

    Partial Class Form1
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.radGetPrintDocument = New System.Windows.Forms.RadioButton()
            Me.radXPS = New System.Windows.Forms.RadioButton()
            Me.radSVG = New System.Windows.Forms.RadioButton()
            Me.radPDF = New System.Windows.Forms.RadioButton()
            Me.radPrint = New System.Windows.Forms.RadioButton()
            Me.radPreview = New System.Windows.Forms.RadioButton()
            Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.prevWinForm = New System.Windows.Forms.PrintPreviewControl()
            Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
            Me.printDocument1 = New System.Drawing.Printing.PrintDocument()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)

            Me.panel1.SuspendLayout()
            Me.SuspendLayout()

            '
            ' btnExecute
            '
            Me.btnExecute.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.btnExecute.BackColor = System.Drawing.Color.FromArgb(75, 95, 131)
            Me.btnExecute.Font = New System.Drawing.Font("MS UI Gothic", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnExecute.ForeColor = System.Drawing.Color.White
            Me.btnExecute.Location = New System.Drawing.Point(934, 20)
            Me.btnExecute.Margin = New System.Windows.Forms.Padding(7, 10, 7, 10)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(269, 111)
            Me.btnExecute.TabIndex = 2
            Me.btnExecute.Text = "実行"
            Me.btnExecute.UseVisualStyleBackColor = False

            '
            ' panel1
            '
            Me.panel1.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel1.Controls.Add(Me.radGetPrintDocument)
            Me.panel1.Controls.Add(Me.radXPS)
            Me.panel1.Controls.Add(Me.radSVG)
            Me.panel1.Controls.Add(Me.radPDF)
            Me.panel1.Controls.Add(Me.radPrint)
            Me.panel1.Controls.Add(Me.radPreview)
            Me.panel1.Controls.Add(Me.btnExecute)
            Me.panel1.Location = New System.Drawing.Point(12, 8)
            Me.panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(1229, 155)
            Me.panel1.TabIndex = 20

            '
            ' radGetPrintDocument
            '
            Me.radGetPrintDocument.AutoSize = True
            Me.radGetPrintDocument.Checked = True
            Me.radGetPrintDocument.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radGetPrintDocument.Location = New System.Drawing.Point(71, 95)
            Me.radGetPrintDocument.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radGetPrintDocument.Name = "radGetPrintDocument"
            Me.radGetPrintDocument.Size = New System.Drawing.Size(243, 36)
            Me.radGetPrintDocument.TabIndex = 14
            Me.radGetPrintDocument.TabStop = True
            Me.radGetPrintDocument.Text = "独自プレビュー ↓↓↓"
            Me.radGetPrintDocument.UseVisualStyleBackColor = True

            '
            ' radXPS
            '
            Me.radXPS.AutoSize = True
            Me.radXPS.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radXPS.Location = New System.Drawing.Point(733, 32)
            Me.radXPS.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radXPS.Name = "radXPS"
            Me.radXPS.Size = New System.Drawing.Size(122, 36)
            Me.radXPS.TabIndex = 13
            Me.radXPS.Text = "XPS出力"
            Me.radXPS.UseVisualStyleBackColor = True

            '
            ' radSVG
            '
            Me.radSVG.AutoSize = True
            Me.radSVG.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radSVG.Location = New System.Drawing.Point(555, 34)
            Me.radSVG.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radSVG.Name = "radSVG"
            Me.radSVG.Size = New System.Drawing.Size(125, 36)
            Me.radSVG.TabIndex = 12
            Me.radSVG.Text = "SVG出力"
            Me.radSVG.UseVisualStyleBackColor = True

            '
            ' radPDF
            '
            Me.radPDF.AutoSize = True
            Me.radPDF.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radPDF.Location = New System.Drawing.Point(369, 32)
            Me.radPDF.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radPDF.Name = "radPDF"
            Me.radPDF.Size = New System.Drawing.Size(123, 36)
            Me.radPDF.TabIndex = 11
            Me.radPDF.Text = "PDF出力"
            Me.radPDF.UseVisualStyleBackColor = True

            '
            ' radPrint
            '
            Me.radPrint.AutoSize = True
            Me.radPrint.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radPrint.Location = New System.Drawing.Point(234, 34)
            Me.radPrint.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radPrint.Name = "radPrint"
            Me.radPrint.Size = New System.Drawing.Size(80, 36)
            Me.radPrint.TabIndex = 10
            Me.radPrint.Text = "印刷"
            Me.radPrint.UseVisualStyleBackColor = True

            '
            ' radPreview
            '
            Me.radPreview.AutoSize = True
            Me.radPreview.Checked = True
            Me.radPreview.Font = New System.Drawing.Font("Yu Gothic UI", 18!, System.Drawing.FontStyle.Bold)
            Me.radPreview.Location = New System.Drawing.Point(71, 34)
            Me.radPreview.Margin = New System.Windows.Forms.Padding(4, 7, 4, 7)
            Me.radPreview.Name = "radPreview"
            Me.radPreview.Size = New System.Drawing.Size(116, 36)
            Me.radPreview.TabIndex = 9
            Me.radPreview.TabStop = True
            Me.radPreview.Text = "プレビュー"
            Me.radPreview.UseVisualStyleBackColor = True

            '
            ' prevWinForm
            '
            Me.prevWinForm.Anchor = CType(
                 System.Windows.Forms.AnchorStyles.Top Or
                 System.Windows.Forms.AnchorStyles.Bottom Or
                 System.Windows.Forms.AnchorStyles.Left Or
                 System.Windows.Forms.AnchorStyles.Right,
                 System.Windows.Forms.AnchorStyles
             )
            Me.prevWinForm.AutoZoom = False
            Me.prevWinForm.Location = New System.Drawing.Point(12, 171)
            Me.prevWinForm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.prevWinForm.Name = "prevWinForm"
            Me.prevWinForm.Size = New System.Drawing.Size(1229, 527)
            Me.prevWinForm.TabIndex = 21
            Me.prevWinForm.Zoom = 1R

            '
            ' toolTip1
            '
            Me.toolTip1.IsBalloon = True
            Me.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
            Me.toolTip1.ToolTipTitle = "Windows10/11でXPSビューワーを使う方法"

            '
            ' Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.AutoScroll = True
            Me.BackColor = System.Drawing.Color.FromArgb(252, 238, 235)
            Me.ClientSize = New System.Drawing.Size(1255, 711)
            Me.Controls.Add(Me.prevWinForm)
            Me.Controls.Add(Me.panel1)
            Me.MinimumSize = New System.Drawing.Size(1024, 600)
            Me.Name = "Form1"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Reports.net かんたんなサンプル！"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.ResumeLayout(False)
        End Sub

        Private WithEvents btnExecute As System.Windows.Forms.Button
        Private panel1 As System.Windows.Forms.Panel
        Private radPDF As System.Windows.Forms.RadioButton
        Private radPrint As System.Windows.Forms.RadioButton
        Private radPreview As System.Windows.Forms.RadioButton
        Private saveFileDialog As System.Windows.Forms.SaveFileDialog
        Private radXPS As System.Windows.Forms.RadioButton
        Private radSVG As System.Windows.Forms.RadioButton
        Private prevWinForm As System.Windows.Forms.PrintPreviewControl
        Private radGetPrintDocument As System.Windows.Forms.RadioButton
        Private saveFileDialog1 As System.Windows.Forms.SaveFileDialog
        Private printDocument1 As System.Drawing.Printing.PrintDocument
        Private toolTip1 As System.Windows.Forms.ToolTip

#End Region
    End Class

End Namespace
