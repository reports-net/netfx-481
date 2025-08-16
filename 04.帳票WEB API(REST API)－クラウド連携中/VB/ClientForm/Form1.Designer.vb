<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.radWebApiAzureWin = New System.Windows.Forms.RadioButton()
        Me.radWebApiLocal = New System.Windows.Forms.RadioButton()
        Me.richTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.radPDF = New System.Windows.Forms.RadioButton()
        Me.radPrint = New System.Windows.Forms.RadioButton()
        Me.radPreview = New System.Windows.Forms.RadioButton()
        Me.btnPost = New System.Windows.Forms.Button()
        Me.btnGet = New System.Windows.Forms.Button()
        Me.opt10の倍数 = New System.Windows.Forms.RadioButton()
        Me.opt単純な印刷データ = New System.Windows.Forms.RadioButton()
        Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.opt広告 = New System.Windows.Forms.RadioButton()
        Me.opt商品一覧 = New System.Windows.Forms.RadioButton()
        Me.opt請求書 = New System.Windows.Forms.RadioButton()
        Me.opt見積書 = New System.Windows.Forms.RadioButton()
        Me.opt住所一覧 = New System.Windows.Forms.RadioButton()
        Me.groupBox1.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.radWebApiAzureWin)
        Me.groupBox1.Controls.Add(Me.radWebApiLocal)
        Me.groupBox1.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.groupBox1.ForeColor = System.Drawing.Color.Blue
        Me.groupBox1.Location = New System.Drawing.Point(33, 22)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(1131, 111)
        Me.groupBox1.TabIndex = 21
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "帳票作成 WEB API (REST API) の配置場所選択"
        '
        'radWebApiAzureWin
        '
        Me.radWebApiAzureWin.AutoSize = True
        Me.radWebApiAzureWin.Font = New System.Drawing.Font("Yu Gothic UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.radWebApiAzureWin.Location = New System.Drawing.Point(259, 44)
        Me.radWebApiAzureWin.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radWebApiAzureWin.Name = "radWebApiAzureWin"
        Me.radWebApiAzureWin.Size = New System.Drawing.Size(246, 34)
        Me.radWebApiAzureWin.TabIndex = 17
        Me.radWebApiAzureWin.Tag = "1"
        Me.radWebApiAzureWin.Text = "Azure Windows Server"
        Me.radWebApiAzureWin.UseVisualStyleBackColor = True
        '
        'radWebApiLocal
        '
        Me.radWebApiLocal.AutoSize = True
        Me.radWebApiLocal.Checked = True
        Me.radWebApiLocal.Font = New System.Drawing.Font("Yu Gothic UI", 15.75!, System.Drawing.FontStyle.Bold)
        Me.radWebApiLocal.Location = New System.Drawing.Point(24, 44)
        Me.radWebApiLocal.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radWebApiLocal.Name = "radWebApiLocal"
        Me.radWebApiLocal.Size = New System.Drawing.Size(202, 34)
        Me.radWebApiLocal.TabIndex = 16
        Me.radWebApiLocal.TabStop = True
        Me.radWebApiLocal.Tag = "0"
        Me.radWebApiLocal.Text = "ローカルデバッグ環境"
        Me.radWebApiLocal.UseVisualStyleBackColor = True
        '
        'richTextBox1
        '
        Me.richTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.richTextBox1.BackColor = System.Drawing.Color.Gray
        Me.richTextBox1.Font = New System.Drawing.Font("BIZ UDゴシック", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.richTextBox1.ForeColor = System.Drawing.Color.White
        Me.richTextBox1.Location = New System.Drawing.Point(66, 295)
        Me.richTextBox1.Name = "richTextBox1"
        Me.richTextBox1.ReadOnly = True
        Me.richTextBox1.Size = New System.Drawing.Size(1098, 391)
        Me.richTextBox1.TabIndex = 20
        Me.richTextBox1.Text = resources.GetString("richTextBox1.Text")
        '
        'panel1
        '
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panel1.Controls.Add(Me.radPDF)
        Me.panel1.Controls.Add(Me.radPrint)
        Me.panel1.Controls.Add(Me.radPreview)
        Me.panel1.Controls.Add(Me.btnPost)
        Me.panel1.Controls.Add(Me.btnGet)
        Me.panel1.Location = New System.Drawing.Point(66, 185)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(1098, 104)
        Me.panel1.TabIndex = 19
        '
        'radPDF
        '
        Me.radPDF.AutoSize = True
        Me.radPDF.Font = New System.Drawing.Font("Yu Gothic UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.radPDF.Location = New System.Drawing.Point(383, 30)
        Me.radPDF.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
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
        Me.radPrint.Location = New System.Drawing.Point(228, 30)
        Me.radPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
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
        Me.radPreview.Location = New System.Drawing.Point(57, 30)
        Me.radPreview.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radPreview.Name = "radPreview"
        Me.radPreview.Size = New System.Drawing.Size(116, 36)
        Me.radPreview.TabIndex = 9
        Me.radPreview.TabStop = True
        Me.radPreview.Text = "プレビュー"
        Me.radPreview.UseVisualStyleBackColor = True
        '
        'btnPost
        '
        Me.btnPost.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPost.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnPost.Location = New System.Drawing.Point(552, 12)
        Me.btnPost.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(256, 76)
        Me.btnPost.TabIndex = 2
        Me.btnPost.Text = "実行 (POST)"
        Me.btnPost.UseVisualStyleBackColor = False
        '
        'btnGet
        '
        Me.btnGet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGet.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnGet.Location = New System.Drawing.Point(828, 12)
        Me.btnGet.Margin = New System.Windows.Forms.Padding(7, 7, 7, 7)
        Me.btnGet.Name = "btnGet"
        Me.btnGet.Size = New System.Drawing.Size(236, 76)
        Me.btnGet.TabIndex = 0
        Me.btnGet.Text = "実行 (GET)"
        Me.btnGet.UseVisualStyleBackColor = False
        '
        'opt10の倍数
        '
        Me.opt10の倍数.AutoSize = True
        Me.opt10の倍数.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt10の倍数.Location = New System.Drawing.Point(285, 142)
        Me.opt10の倍数.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt10の倍数.Name = "opt10の倍数"
        Me.opt10の倍数.Size = New System.Drawing.Size(114, 34)
        Me.opt10の倍数.TabIndex = 18
        Me.opt10の倍数.Tag = "1"
        Me.opt10の倍数.Text = "10の倍数"
        Me.opt10の倍数.UseVisualStyleBackColor = True
        '
        'opt単純な印刷データ
        '
        Me.opt単純な印刷データ.AutoSize = True
        Me.opt単純な印刷データ.Checked = True
        Me.opt単純な印刷データ.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt単純な印刷データ.Location = New System.Drawing.Point(72, 143)
        Me.opt単純な印刷データ.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt単純な印刷データ.Name = "opt単純な印刷データ"
        Me.opt単純な印刷データ.Size = New System.Drawing.Size(188, 34)
        Me.opt単純な印刷データ.TabIndex = 17
        Me.opt単純な印刷データ.TabStop = True
        Me.opt単純な印刷データ.Tag = "0"
        Me.opt単純な印刷データ.Text = "単純な印刷データ"
        Me.opt単純な印刷データ.UseVisualStyleBackColor = True
        '
        'opt広告
        '
        Me.opt広告.AutoSize = True
        Me.opt広告.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt広告.Location = New System.Drawing.Point(1051, 143)
        Me.opt広告.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt広告.Name = "opt広告"
        Me.opt広告.Size = New System.Drawing.Size(75, 34)
        Me.opt広告.TabIndex = 24
        Me.opt広告.Tag = "6"
        Me.opt広告.Text = "広告"
        Me.opt広告.UseVisualStyleBackColor = True
        '
        'opt商品一覧
        '
        Me.opt商品一覧.AutoSize = True
        Me.opt商品一覧.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt商品一覧.Location = New System.Drawing.Point(885, 143)
        Me.opt商品一覧.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt商品一覧.Name = "opt商品一覧"
        Me.opt商品一覧.Size = New System.Drawing.Size(141, 34)
        Me.opt商品一覧.TabIndex = 26
        Me.opt商品一覧.Tag = "5"
        Me.opt商品一覧.Text = "商品一覧　"
        Me.opt商品一覧.UseVisualStyleBackColor = True
        '
        'opt請求書
        '
        Me.opt請求書.AutoSize = True
        Me.opt請求書.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt請求書.Location = New System.Drawing.Point(741, 142)
        Me.opt請求書.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt請求書.Name = "opt請求書"
        Me.opt請求書.Size = New System.Drawing.Size(97, 34)
        Me.opt請求書.TabIndex = 25
        Me.opt請求書.Tag = "4"
        Me.opt請求書.Text = "請求書"
        Me.opt請求書.UseVisualStyleBackColor = True
        '
        'opt見積書
        '
        Me.opt見積書.AutoSize = True
        Me.opt見積書.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt見積書.Location = New System.Drawing.Point(602, 143)
        Me.opt見積書.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt見積書.Name = "opt見積書"
        Me.opt見積書.Size = New System.Drawing.Size(97, 34)
        Me.opt見積書.TabIndex = 23
        Me.opt見積書.Tag = "3"
        Me.opt見積書.Text = "見積書"
        Me.opt見積書.UseVisualStyleBackColor = True
        '
        'opt住所一覧
        '
        Me.opt住所一覧.AutoSize = True
        Me.opt住所一覧.Font = New System.Drawing.Font("Yu Gothic UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.opt住所一覧.Location = New System.Drawing.Point(443, 143)
        Me.opt住所一覧.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.opt住所一覧.Name = "opt住所一覧"
        Me.opt住所一覧.Size = New System.Drawing.Size(119, 34)
        Me.opt住所一覧.TabIndex = 22
        Me.opt住所一覧.Tag = "2"
        Me.opt住所一覧.Text = "住所一覧"
        Me.opt住所一覧.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1226, 707)
        Me.Controls.Add(Me.opt広告)
        Me.Controls.Add(Me.opt商品一覧)
        Me.Controls.Add(Me.opt請求書)
        Me.Controls.Add(Me.opt見積書)
        Me.Controls.Add(Me.opt住所一覧)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.richTextBox1)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.opt10の倍数)
        Me.Controls.Add(Me.opt単純な印刷データ)
        Me.Font = New System.Drawing.Font("Yu Gothic UI", 20.25!, System.Drawing.FontStyle.Bold)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MinimumSize = New System.Drawing.Size(1022, 594)
        Me.Name = "Form1"
        Me.Text = "WEB API (REST API) から印刷データを取得して帳票出力するサンプル"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents groupBox1 As GroupBox
    Private WithEvents radWebApiAzureWin As RadioButton
    Private WithEvents radWebApiLocal As RadioButton
    Private WithEvents richTextBox1 As RichTextBox
    Private WithEvents panel1 As Panel
    Private WithEvents radPDF As RadioButton
    Private WithEvents radPrint As RadioButton
    Private WithEvents radPreview As RadioButton
    Private WithEvents btnPost As Button
    Private WithEvents btnGet As Button
    Private WithEvents opt10の倍数 As RadioButton
    Private WithEvents opt単純な印刷データ As RadioButton
    Private WithEvents saveFileDialog As SaveFileDialog
    Private WithEvents opt広告 As RadioButton
    Private WithEvents opt商品一覧 As RadioButton
    Private WithEvents opt請求書 As RadioButton
    Private WithEvents opt見積書 As RadioButton
    Private WithEvents opt住所一覧 As RadioButton
End Class
