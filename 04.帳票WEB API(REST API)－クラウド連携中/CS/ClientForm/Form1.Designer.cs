namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnGet = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.opt広告 = new System.Windows.Forms.RadioButton();
            this.opt見積書 = new System.Windows.Forms.RadioButton();
            this.opt住所一覧 = new System.Windows.Forms.RadioButton();
            this.opt10の倍数 = new System.Windows.Forms.RadioButton();
            this.opt単純な印刷データ = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radPDF = new System.Windows.Forms.RadioButton();
            this.radPrint = new System.Windows.Forms.RadioButton();
            this.radPreview = new System.Windows.Forms.RadioButton();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.opt請求書 = new System.Windows.Forms.RadioButton();
            this.opt商品一覧 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radWebApiAzureWin = new System.Windows.Forms.RadioButton();
            this.radWebApiLocal = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.btnGet.Location = new System.Drawing.Point(828, 12);
            this.btnGet.Margin = new System.Windows.Forms.Padding(7);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(236, 76);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "実行 (GET)";
            this.btnGet.UseVisualStyleBackColor = false;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnPost
            // 
            this.btnPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.btnPost.Location = new System.Drawing.Point(552, 12);
            this.btnPost.Margin = new System.Windows.Forms.Padding(7);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(256, 76);
            this.btnPost.TabIndex = 2;
            this.btnPost.Text = "実行 (POST)";
            this.btnPost.UseVisualStyleBackColor = false;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // opt広告
            // 
            this.opt広告.AutoSize = true;
            this.opt広告.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt広告.Location = new System.Drawing.Point(1044, 133);
            this.opt広告.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt広告.Name = "opt広告";
            this.opt広告.Size = new System.Drawing.Size(75, 34);
            this.opt広告.TabIndex = 10;
            this.opt広告.Tag = "6";
            this.opt広告.Text = "広告";
            this.opt広告.UseVisualStyleBackColor = true;
            // 
            // opt見積書
            // 
            this.opt見積書.AutoSize = true;
            this.opt見積書.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt見積書.Location = new System.Drawing.Point(595, 133);
            this.opt見積書.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt見積書.Name = "opt見積書";
            this.opt見積書.Size = new System.Drawing.Size(97, 34);
            this.opt見積書.TabIndex = 9;
            this.opt見積書.Tag = "3";
            this.opt見積書.Text = "見積書";
            this.opt見積書.UseVisualStyleBackColor = true;
            // 
            // opt住所一覧
            // 
            this.opt住所一覧.AutoSize = true;
            this.opt住所一覧.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt住所一覧.Location = new System.Drawing.Point(436, 133);
            this.opt住所一覧.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt住所一覧.Name = "opt住所一覧";
            this.opt住所一覧.Size = new System.Drawing.Size(119, 34);
            this.opt住所一覧.TabIndex = 8;
            this.opt住所一覧.Tag = "2";
            this.opt住所一覧.Text = "住所一覧";
            this.opt住所一覧.UseVisualStyleBackColor = true;
            // 
            // opt10の倍数
            // 
            this.opt10の倍数.AutoSize = true;
            this.opt10の倍数.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt10の倍数.Location = new System.Drawing.Point(291, 132);
            this.opt10の倍数.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt10の倍数.Name = "opt10の倍数";
            this.opt10の倍数.Size = new System.Drawing.Size(114, 34);
            this.opt10の倍数.TabIndex = 7;
            this.opt10の倍数.Tag = "1";
            this.opt10の倍数.Text = "10の倍数";
            this.opt10の倍数.UseVisualStyleBackColor = true;
            // 
            // opt単純な印刷データ
            // 
            this.opt単純な印刷データ.AutoSize = true;
            this.opt単純な印刷データ.Checked = true;
            this.opt単純な印刷データ.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt単純な印刷データ.Location = new System.Drawing.Point(78, 133);
            this.opt単純な印刷データ.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt単純な印刷データ.Name = "opt単純な印刷データ";
            this.opt単純な印刷データ.Size = new System.Drawing.Size(188, 34);
            this.opt単純な印刷データ.TabIndex = 6;
            this.opt単純な印刷データ.TabStop = true;
            this.opt単純な印刷データ.Tag = "0";
            this.opt単純な印刷データ.Text = "単純な印刷データ";
            this.opt単純な印刷データ.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.radPDF);
            this.panel1.Controls.Add(this.radPrint);
            this.panel1.Controls.Add(this.radPreview);
            this.panel1.Controls.Add(this.btnPost);
            this.panel1.Controls.Add(this.btnGet);
            this.panel1.Location = new System.Drawing.Point(72, 175);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 104);
            this.panel1.TabIndex = 11;
            // 
            // radPDF
            // 
            this.radPDF.AutoSize = true;
            this.radPDF.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold);
            this.radPDF.Location = new System.Drawing.Point(383, 30);
            this.radPDF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPDF.Name = "radPDF";
            this.radPDF.Size = new System.Drawing.Size(123, 36);
            this.radPDF.TabIndex = 11;
            this.radPDF.Text = "PDF出力";
            this.radPDF.UseVisualStyleBackColor = true;
            // 
            // radPrint
            // 
            this.radPrint.AutoSize = true;
            this.radPrint.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold);
            this.radPrint.Location = new System.Drawing.Point(228, 30);
            this.radPrint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPrint.Name = "radPrint";
            this.radPrint.Size = new System.Drawing.Size(80, 36);
            this.radPrint.TabIndex = 10;
            this.radPrint.Text = "印刷";
            this.radPrint.UseVisualStyleBackColor = true;
            // 
            // radPreview
            // 
            this.radPreview.AutoSize = true;
            this.radPreview.Checked = true;
            this.radPreview.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Bold);
            this.radPreview.Location = new System.Drawing.Point(57, 30);
            this.radPreview.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPreview.Name = "radPreview";
            this.radPreview.Size = new System.Drawing.Size(116, 36);
            this.radPreview.TabIndex = 9;
            this.radPreview.TabStop = true;
            this.radPreview.Text = "プレビュー";
            this.radPreview.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 37);
            this.label1.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.richTextBox1.BackColor = System.Drawing.Color.Gray;
            this.richTextBox1.Font = new System.Drawing.Font("BIZ UDゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(72, 285);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1098, 415);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // opt請求書
            // 
            this.opt請求書.AutoSize = true;
            this.opt請求書.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt請求書.Location = new System.Drawing.Point(733, 132);
            this.opt請求書.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt請求書.Name = "opt請求書";
            this.opt請求書.Size = new System.Drawing.Size(97, 34);
            this.opt請求書.TabIndex = 14;
            this.opt請求書.Tag = "4";
            this.opt請求書.Text = "請求書";
            this.opt請求書.UseVisualStyleBackColor = true;
            // 
            // opt商品一覧
            // 
            this.opt商品一覧.AutoSize = true;
            this.opt商品一覧.Font = new System.Drawing.Font("Yu Gothic UI", 16F, System.Drawing.FontStyle.Bold);
            this.opt商品一覧.Location = new System.Drawing.Point(878, 133);
            this.opt商品一覧.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.opt商品一覧.Name = "opt商品一覧";
            this.opt商品一覧.Size = new System.Drawing.Size(141, 34);
            this.opt商品一覧.TabIndex = 15;
            this.opt商品一覧.Tag = "5";
            this.opt商品一覧.Text = "商品一覧　";
            this.opt商品一覧.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radWebApiAzureWin);
            this.groupBox1.Controls.Add(this.radWebApiLocal);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(39, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1131, 111);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帳票作成 WEB API (REST API) の配置場所選択";
            // 
            // radWebApiAzureWin
            // 
            this.radWebApiAzureWin.AutoSize = true;
            this.radWebApiAzureWin.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.radWebApiAzureWin.Location = new System.Drawing.Point(259, 44);
            this.radWebApiAzureWin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radWebApiAzureWin.Name = "radWebApiAzureWin";
            this.radWebApiAzureWin.Size = new System.Drawing.Size(246, 34);
            this.radWebApiAzureWin.TabIndex = 17;
            this.radWebApiAzureWin.Tag = "1";
            this.radWebApiAzureWin.Text = "Azure Windows Server";
            this.radWebApiAzureWin.UseVisualStyleBackColor = true;
            // 
            // radWebApiLocal
            // 
            this.radWebApiLocal.AutoSize = true;
            this.radWebApiLocal.Checked = true;
            this.radWebApiLocal.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.radWebApiLocal.Location = new System.Drawing.Point(24, 44);
            this.radWebApiLocal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radWebApiLocal.Name = "radWebApiLocal";
            this.radWebApiLocal.Size = new System.Drawing.Size(202, 34);
            this.radWebApiLocal.TabIndex = 16;
            this.radWebApiLocal.TabStop = true;
            this.radWebApiLocal.Tag = "0";
            this.radWebApiLocal.Text = "ローカルデバッグ環境";
            this.radWebApiLocal.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(238)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(1221, 740);
            this.Controls.Add(this.opt広告);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.opt商品一覧);
            this.Controls.Add(this.opt請求書);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.opt見積書);
            this.Controls.Add(this.opt住所一覧);
            this.Controls.Add(this.opt10の倍数);
            this.Controls.Add(this.opt単純な印刷データ);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WEB API (REST API) から印刷データを取得して帳票出力するサンプル";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.RadioButton opt広告;
        private System.Windows.Forms.RadioButton opt見積書;
        private System.Windows.Forms.RadioButton opt住所一覧;
        private System.Windows.Forms.RadioButton opt10の倍数;
        private System.Windows.Forms.RadioButton opt単純な印刷データ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radPDF;
        private System.Windows.Forms.RadioButton radPrint;
        private System.Windows.Forms.RadioButton radPreview;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RadioButton opt請求書;
        private System.Windows.Forms.RadioButton opt商品一覧;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radWebApiAzureWin;
        private System.Windows.Forms.RadioButton radWebApiLocal;
    }
}