' -----------------------------------------------------------------------------
'   Form1.Designer.vb   (.NET Framework / VB.NET)      ★C#サイズに統一・下部余白縮小★
' -----------------------------------------------------------------------------
Imports System

Namespace Sample
    Partial Class Form1
        Private components As System.ComponentModel.IContainer = Nothing

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
            Me.btnExecute = New System.Windows.Forms.Button()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.radXPS = New System.Windows.Forms.RadioButton()
            Me.radSVG = New System.Windows.Forms.RadioButton()
            Me.radPDF = New System.Windows.Forms.RadioButton()
            Me.radPrint = New System.Windows.Forms.RadioButton()
            Me.radPreview = New System.Windows.Forms.RadioButton()
            Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
            Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
            Me.grpDesign = New System.Windows.Forms.GroupBox()
            Me.radDesign2 = New System.Windows.Forms.RadioButton()
            Me.radDesign1 = New System.Windows.Forms.RadioButton()
            Me.richTextBox2 = New System.Windows.Forms.RichTextBox()
            Me.cmbReportType = New System.Windows.Forms.ComboBox()
            Me.label2 = New System.Windows.Forms.Label()
            Me.pnl = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.grpDesign.SuspendLayout()
            Me.pnl.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnExecute
            '
            Me.btnExecute.BackColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(131, Byte), Integer))
            Me.btnExecute.Font = New System.Drawing.Font("Yu Gothic UI", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.btnExecute.ForeColor = System.Drawing.Color.White
            Me.btnExecute.Location = New System.Drawing.Point(751, 5)
            Me.btnExecute.Margin = New System.Windows.Forms.Padding(5, 6, 5, 6)
            Me.btnExecute.Name = "btnExecute"
            Me.btnExecute.Size = New System.Drawing.Size(188, 56)
            Me.btnExecute.TabIndex = 2
            Me.btnExecute.Text = "実行"
            Me.btnExecute.UseVisualStyleBackColor = False
            '
            'panel1
            '
            Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.panel1.Controls.Add(Me.radXPS)
            Me.panel1.Controls.Add(Me.radSVG)
            Me.panel1.Controls.Add(Me.radPDF)
            Me.panel1.Controls.Add(Me.radPrint)
            Me.panel1.Controls.Add(Me.radPreview)
            Me.panel1.Controls.Add(Me.btnExecute)
            Me.panel1.Location = New System.Drawing.Point(14, 4)
            Me.panel1.Margin = New System.Windows.Forms.Padding(2)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(968, 64)
            Me.panel1.TabIndex = 11
            '
            'radXPS
            '
            Me.radXPS.AutoSize = True
            Me.radXPS.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
            Me.radXPS.Location = New System.Drawing.Point(596, 17)
            Me.radXPS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radXPS.Name = "radXPS"
            Me.radXPS.Size = New System.Drawing.Size(122, 36)
            Me.radXPS.TabIndex = 13
            Me.radXPS.Text = "XPS出力"
            Me.radXPS.UseVisualStyleBackColor = True
            '
            'radSVG
            '
            Me.radSVG.AutoSize = True
            Me.radSVG.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
            Me.radSVG.Location = New System.Drawing.Point(448, 19)
            Me.radSVG.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radSVG.Name = "radSVG"
            Me.radSVG.Size = New System.Drawing.Size(125, 36)
            Me.radSVG.TabIndex = 12
            Me.radSVG.Text = "SVG出力"
            Me.radSVG.UseVisualStyleBackColor = True
            '
            'radPDF
            '
            Me.radPDF.AutoSize = True
            Me.radPDF.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
            Me.radPDF.Location = New System.Drawing.Point(301, 17)
            Me.radPDF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radPDF.Name = "radPDF"
            Me.radPDF.Size = New System.Drawing.Size(123, 36)
            Me.radPDF.TabIndex = 11
            Me.radPDF.Text = "PDF出力"
            Me.radPDF.UseVisualStyleBackColor = True
            '
            'radPrint
            '
            Me.radPrint.AutoSize = True
            Me.radPrint.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
            Me.radPrint.Location = New System.Drawing.Point(192, 19)
            Me.radPrint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radPrint.Name = "radPrint"
            Me.radPrint.Size = New System.Drawing.Size(80, 36)
            Me.radPrint.TabIndex = 10
            Me.radPrint.Text = "印刷"
            Me.radPrint.UseVisualStyleBackColor = True
            '
            'radPreview
            '
            Me.radPreview.AutoSize = True
            Me.radPreview.Checked = True
            Me.radPreview.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
            Me.radPreview.Location = New System.Drawing.Point(47, 19)
            Me.radPreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radPreview.Name = "radPreview"
            Me.radPreview.Size = New System.Drawing.Size(116, 36)
            Me.radPreview.TabIndex = 9
            Me.radPreview.TabStop = True
            Me.radPreview.Text = "プレビュー"
            Me.radPreview.UseVisualStyleBackColor = True
            '
            'richTextBox1
            '
            Me.richTextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(253, Byte), Integer), CType(CType(231, Byte), Integer))
            Me.richTextBox1.Font = New System.Drawing.Font("BIZ UDゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
            Me.richTextBox1.Location = New System.Drawing.Point(14, 80)
            Me.richTextBox1.Margin = New System.Windows.Forms.Padding(2)
            Me.richTextBox1.Name = "richTextBox1"
            Me.richTextBox1.ReadOnly = True
            Me.richTextBox1.Size = New System.Drawing.Size(990, 138)
            Me.richTextBox1.TabIndex = 13
            Me.richTextBox1.Text = "" & Global.Microsoft.VisualBasic.ChrW(10) & "  このプログラムは Windows Form のプログラムですが、" & Global.Microsoft.VisualBasic.ChrW(10) & "  WPFのプロジェクトをスタートアップに設定すれば 同じ機能のWPF版を起動することがで" &
    "きます。" & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(10) & "  このサンプルプログラムでは、手軽にお試しいただける目的で、Excelファイルをデータベースとして使用しております。" & Global.Microsoft.VisualBasic.ChrW(10) & "  そのため、Office " &
    "64bit版がインストールされている必要があります。"
            '
            'grpDesign
            '
            Me.grpDesign.Controls.Add(Me.radDesign2)
            Me.grpDesign.Controls.Add(Me.radDesign1)
            Me.grpDesign.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.grpDesign.ForeColor = System.Drawing.Color.Blue
            Me.grpDesign.Location = New System.Drawing.Point(41, 84)
            Me.grpDesign.Margin = New System.Windows.Forms.Padding(2)
            Me.grpDesign.Name = "grpDesign"
            Me.grpDesign.Padding = New System.Windows.Forms.Padding(2)
            Me.grpDesign.Size = New System.Drawing.Size(384, 58)
            Me.grpDesign.TabIndex = 16
            Me.grpDesign.TabStop = False
            Me.grpDesign.Text = "デザインを選択してください"
            '
            'radDesign2
            '
            Me.radDesign2.AutoSize = True
            Me.radDesign2.Font = New System.Drawing.Font("Yu Gothic UI", 14.0!, System.Drawing.FontStyle.Bold)
            Me.radDesign2.Location = New System.Drawing.Point(217, 27)
            Me.radDesign2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radDesign2.Name = "radDesign2"
            Me.radDesign2.Size = New System.Drawing.Size(99, 29)
            Me.radDesign2.TabIndex = 17
            Me.radDesign2.Tag = "1"
            Me.radDesign2.Text = "デザイン2"
            Me.radDesign2.UseVisualStyleBackColor = True
            '
            'radDesign1
            '
            Me.radDesign1.AutoSize = True
            Me.radDesign1.Checked = True
            Me.radDesign1.Font = New System.Drawing.Font("Yu Gothic UI", 14.0!, System.Drawing.FontStyle.Bold)
            Me.radDesign1.Location = New System.Drawing.Point(17, 27)
            Me.radDesign1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
            Me.radDesign1.Name = "radDesign1"
            Me.radDesign1.Size = New System.Drawing.Size(96, 29)
            Me.radDesign1.TabIndex = 16
            Me.radDesign1.TabStop = True
            Me.radDesign1.Tag = "0"
            Me.radDesign1.Text = "デザイン1"
            Me.radDesign1.UseVisualStyleBackColor = True
            '
            'richTextBox2
            '
            Me.richTextBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(254, Byte), Integer))
            Me.richTextBox2.Font = New System.Drawing.Font("BIZ UDゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.richTextBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
            Me.richTextBox2.Location = New System.Drawing.Point(14, 226)
            Me.richTextBox2.Margin = New System.Windows.Forms.Padding(2)
            Me.richTextBox2.Name = "richTextBox2"
            Me.richTextBox2.ReadOnly = True
            Me.richTextBox2.Size = New System.Drawing.Size(990, 270)
            Me.richTextBox2.TabIndex = 18
            Me.richTextBox2.Text = resources.GetString("richTextBox2.Text")
            '
            'cmbReportType
            '
            Me.cmbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbReportType.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.cmbReportType.FormattingEnabled = True
            Me.cmbReportType.Items.AddRange(New Object() {"見積書 (四角で表を作成する手法・表紙と明細別デザイン・ハンコの印影出力)", "郵便番号一覧 (罫線で表を作成する手法・大量ページ・一覧のデザインを途中で変更・QRコード出力)", "請求書 (ほぼコードのみで帳票作成)", "商品大小分類 (グループごとに中間小計)", "いつでもデザイン変更 (データ再設定なしでデザイン変更)", "広告 (イメージPDF出力・装飾文字使用・画像出力)"})
            Me.cmbReportType.Location = New System.Drawing.Point(36, 40)
            Me.cmbReportType.Margin = New System.Windows.Forms.Padding(2)
            Me.cmbReportType.Name = "cmbReportType"
            Me.cmbReportType.Size = New System.Drawing.Size(1016, 38)
            Me.cmbReportType.TabIndex = 19
            '
            'label2
            '
            Me.label2.AutoSize = True
            Me.label2.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
            Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(102, Byte), Integer))
            Me.label2.Location = New System.Drawing.Point(30, 11)
            Me.label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(217, 21)
            Me.label2.TabIndex = 20
            Me.label2.Text = "サンプル帳票を選択してください。"
            '
            'pnl
            '
            Me.pnl.Controls.Add(Me.panel1)
            Me.pnl.Controls.Add(Me.richTextBox1)
            Me.pnl.Controls.Add(Me.richTextBox2)
            Me.pnl.Location = New System.Drawing.Point(30, 152)
            Me.pnl.Margin = New System.Windows.Forms.Padding(2)
            Me.pnl.Name = "pnl"
            Me.pnl.Size = New System.Drawing.Size(1016, 503)
            Me.pnl.TabIndex = 21
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.AutoScroll = True
            Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(235, Byte), Integer))
            Me.ClientSize = New System.Drawing.Size(1082, 668)
            Me.Controls.Add(Me.pnl)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.cmbReportType)
            Me.Controls.Add(Me.grpDesign)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.MinimumSize = New System.Drawing.Size(1024, 600)
            Me.Name = "Form1"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "WEB API (REST API) から印刷データを取得して帳票出力するサンプル"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.grpDesign.ResumeLayout(False)
            Me.grpDesign.PerformLayout()
            Me.pnl.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        ' ─── デザイナ保持フィールド ───
        Private WithEvents btnExecute As System.Windows.Forms.Button
        Private panel1 As System.Windows.Forms.Panel
        Private radPDF As System.Windows.Forms.RadioButton
        Private radPrint As System.Windows.Forms.RadioButton
        Private radPreview As System.Windows.Forms.RadioButton
        Private saveFileDialog As System.Windows.Forms.SaveFileDialog
        Private richTextBox1 As System.Windows.Forms.RichTextBox
        Private grpDesign As System.Windows.Forms.GroupBox
        Private radDesign2 As System.Windows.Forms.RadioButton
        Private radDesign1 As System.Windows.Forms.RadioButton
        Private WithEvents richTextBox2 As System.Windows.Forms.RichTextBox
        Private WithEvents cmbReportType As System.Windows.Forms.ComboBox
        Private label2 As System.Windows.Forms.Label
        Private radXPS As System.Windows.Forms.RadioButton
        Private radSVG As System.Windows.Forms.RadioButton
        Private pnl As System.Windows.Forms.Panel
    End Class
End Namespace