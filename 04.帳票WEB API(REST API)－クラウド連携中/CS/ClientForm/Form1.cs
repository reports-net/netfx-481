using Pao.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;



namespace WinFormsApp1
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

        private string GetWebApiUrl()
        {
            // 接続先URL : デフォルトはローカルでデバッグ環境
            string url = "https://localhost:44317/";

            if (radWebApiAzureWin.Checked)
            {
                // Azure Windowsサーバに配置してある WEB API を使用
                url = "https://webapimvc-b9ake6c0hwetgbdn.canadacentral-01.azurewebsites.net/";
            }

            return url;
        }



        /// <summary>
        /// GETクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnGet_Click(object sender, EventArgs e)
        {
            // WEB API (REST API) の URL 取得
            string webApiUrl = GetWebApiUrl();

            // ★HTTPS証明書検証をスキップ（開発用）★
            ServicePointManager.ServerCertificateValidationCallback =
                (sender2, certificate, chain, sslPolicyErrors) => true;

            // WEB API (REST API) のURLセット
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                int id = 0;
                if (opt単純な印刷データ.Checked) id = 0;
                if (opt10の倍数.Checked) id = 1;
                if (opt住所一覧.Checked) id = 2;
                if (opt見積書.Checked) id = 3;
                if (opt請求書.Checked) id = 4;
                if (opt商品一覧.Checked) id = 5;
                if (opt広告.Checked) id = 6;


                // GET リクエスト実行
                HttpResponseMessage response = await client.GetAsync("Home/GetReports?Id=" + id.ToString());
                response.EnsureSuccessStatusCode();

                // レスポンスをJSON文字列として取得
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // JSON を PrintData に変換
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                PrintData pd = serializer.Deserialize<PrintData>(jsonResponse);

                // Base64デコードして帳票出力
                byte[] data = Convert.FromBase64String(pd.Data);
                Output帳票(data);
            }
        }

        /// <summary>
        /// POSTクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnPost_Click(object sender, EventArgs e)
        {
            // WEB API (REST API) の URL 取得
            string webApiUrl = GetWebApiUrl();

            // POST用データ作成
            PrintData pdIn = new PrintData();
            if (opt単純な印刷データ.Checked) pdIn.Id = 0;
            if (opt10の倍数.Checked) pdIn.Id = 1;
            if (opt住所一覧.Checked) pdIn.Id = 2;
            if (opt見積書.Checked) pdIn.Id = 3;
            if (opt請求書.Checked) pdIn.Id = 4;
            if (opt商品一覧.Checked) pdIn.Id = 5;
            if (opt広告.Checked) pdIn.Id = 6;

            // WEB API (REST API) のURLセット
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(webApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // JavaScriptSerializer作成
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                // PrintData を JSON に変換
                string jsonContent = serializer.Serialize(pdIn);
                //StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            StringContent content = new StringContent(jsonContent, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");



            // POST リクエスト実行
            HttpResponseMessage response = await client.PostAsync("Home/PostReports", content);
                response.EnsureSuccessStatusCode();

                // レスポンスをJSON文字列として取得
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // JSON を PrintData に変換
                PrintData pdOut = serializer.Deserialize<PrintData>(jsonResponse);

                // Base64デコードして帳票出力
                byte[] data = Convert.FromBase64String(pdOut.Data);
                Output帳票(data);
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


    }
}
