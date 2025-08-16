using Pao.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.ServiceModel;


namespace Pao.Reports.Sample
{
    public partial class Form1 : Form
    {

        public class PrintData
        {
            public int Id { get; set; }
            public string Data { get; set; }
        }




        public Form1()
        {
            InitializeComponent();

            // TLS設定（.NET Framework 4.6以前で必要、4.7以降は冗長だが無害）
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }


        /// <summary>
        /// 実行ボタンクリック（WCF-SOAP版）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {

            // 接続先URL : デフォルトはローカルでデバッグ環境
            string url = "http://localhost:62141/Service1.svc";

            if (radWebApiAzureWin.Checked)
            {
                // Azure Windowsサーバに配置してある WCF-SOAP サービスを使用
                url = "http://paopao-hce5b6hcfwaec0b4.japaneast-01.azurewebsites.net/Service1.svc";
            }


            try
            {
                byte[] data = null;
                System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;
                IReport paoRep;

                if (b.Text == "印刷")
                {
                    paoRep = ReportCreator.GetReport(); // 印刷オブジェクト生成
                }
                else if (b.Text == "プレビュー")
                {
                    paoRep = ReportCreator.GetPreviewWpf(); // プレビュー画面を作成
                }
                else
                {
                    paoRep = ReportCreator.GetPdf(); // PDFオブジェクト生成
                }

                // WCF-SOAPクライアントを動的に作成
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
                {
                    MaxReceivedMessageSize = 10 * 1024 * 1024,   // 10 MB
                    SendTimeout = TimeSpan.FromMinutes(2),
                    ReceiveTimeout = TimeSpan.FromMinutes(2)
                };

                var address = new EndpointAddress(url);

                using (var client = new ServiceReference1.Service1Client(binding, address))
                {

                    // 選択された帳票タイプに応じてWCFサービス呼び出し
                    if (opt単純な印刷データ.Checked) data = client.getReports単純なサンプル();
                    if (opt10の倍数.Checked) data = client.getReports10の倍数();
                    if (opt住所一覧.Checked) data = client.getReports郵便番号();
                    if (opt見積書.Checked) data = client.getReports見積書();
                    if (opt請求書.Checked) data = client.getReports請求書();
                    if (opt商品一覧.Checked) data = client.getReports商品一覧();
                    if (opt広告.Checked) data = client.getReports広告();

                    Output帳票(data);
                }
                //// チャンネル閉じる
                //((ICommunicationObject)client).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Output帳票(byte[] data)
        {

            //IReport インターフェースで宣言(印刷・レポートどちらでも使える入れ物の用意)
            IReport paoRep = null;

            if (radPreview.Checked) //ラジオボタンでプレビューが選択されている場合
            {
                //プレビューオブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPreviewWpf();
            }
            else if (radPrint.Checked) // 印刷が選択されている場合
            {
                //印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport();
            }
            else if (radPDF.Checked) // PDFが選択されている場合
            {
                if (!opt広告.Checked)
                {
                    //PDF出力オブジェクトのインスタンスを獲得
                    paoRep = ReportCreator.GetPdf();
                }
                else
                {
                    //イメージPDF出力オブジェクトのインスタンスを獲得
                    paoRep = ReportCreator.GetImagePdf();
                }
            }
            else
            {
                //印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport();
            }


            paoRep.LoadData(data); //印刷データを読み込む


            if (!radPDF.Checked) //PDF出力が選択されていない場合
            {
                paoRep.Output(); //印刷または、プレビューを実行

            }
            else
            {

                //PDF出力
                saveFileDialog.FileName = "印刷データ";
                saveFileDialog.Filter = "PDF形式 (*.pdf)|*.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    paoRep.SavePDF(saveFileDialog.FileName); //印刷データの保存

                    // *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                    if (MessageBox.Show(this, "PDFを表示しますか？", "PDF の表示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName);
                        startInfo.UseShellExecute = true;
                        System.Diagnostics.Process.Start(startInfo);
                    }
                }

            }

        }

        /// <summary>
        /// フォーム終了処理
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }



    }
}
