using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Pao.Reports;

namespace Sample
{


    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {

        public class PrintData
        {
            public int id { get; set; }
            public string Data { get; set; }
        }

        public Window1()
        {

            InitializeComponent();

            this.Height = SystemParameters.PrimaryScreenHeight - 50;

            // TLS設定（.NET Framework 4.6以前で必要、4.7以降は冗長だが無害）
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


        }

        /// <summary>
        /// GETクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ExecuteGet_Click(object sender, RoutedEventArgs e)
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

                int id = GetReportTypeId();

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

        private async void ExecutePost_Click(object sender, RoutedEventArgs e)
        {
            // WEB API (REST API) の URL 取得
            string webApiUrl = GetWebApiUrl();

            PrintData pdIn = new PrintData();
            pdIn.id = GetReportTypeId();

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

        /// <summary>
        /// レポートタイプIDを取得
        /// </summary>
        /// <returns>レポートタイプID</returns>
        private int GetReportTypeId()
        {
            ComboBoxItem selectedItem = cmbReportType.SelectedItem as ComboBoxItem;
            string selectedContent = selectedItem?.Content?.ToString() ?? string.Empty;

            if (selectedContent.Contains("単純な印刷データ"))
                return 0;
            else if (selectedContent.Contains("10の倍数"))
                return 1;
            else if (selectedContent.Contains("郵便番号一覧"))
                return 2;
            else if (selectedContent.Contains("見積書"))
                return 3;
            else if (selectedContent.Contains("請求書"))
                return 4;
            else if (selectedContent.Contains("商品大小分類"))
                return 5;
            else if (selectedContent.Contains("広告"))
                return 6;
            else
                return 0; // デフォルト
        }

        /// <summary>
        /// WEB API URL 取得
        /// </summary>
        /// <returns>WEB API URL</returns>
        private string GetWebApiUrl()
        {
            // 接続先URL : デフォルトはローカルでデバッグ環境
            string url = "https://localhost:44317/";

            if (radAzureWin.IsChecked == true)
            {
                // Azure Windowsサーバに配置してある WEB API を使用
                url = "https://webapimvc-b9ake6c0hwetgbdn.canadacentral-01.azurewebsites.net/";
            }
            return url;
        }

        /// <summary>
        /// 帳票出力処理
        /// </summary>
        /// <param name="data"></param>
        private void Output帳票(byte[] data)
        {
            int repID = GetReportTypeId();

            //IReport インターフェースで宣言(印刷・レポートどちらでも使える入れ物の用意)
            IReport paoRep = null;

            if (radPreview.IsChecked == true) //ラジオボタンでプレビューが選択されている場合
            {
                //プレビューオブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPreview();
            }
            else if (radPrint.IsChecked == true) // 印刷が選択されている場合
            {
                //印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport();
            }
            else if (radPDF.IsChecked == true) // PDFが選択されている場合
            {
                if (!cmbReportType.Text.Contains("広告"))
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
                //SVGとWPSは、印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport();
            }

            paoRep.LoadData(data); //印刷データを読み込む


            if (!radPDF.IsChecked == true) //PDF出力が選択されていない場合
            {
                paoRep.Output(); //印刷または、プレビューを実行

            }
            else
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "印刷データ";
                dlg.DefaultExt = ".pdf";
                dlg.Filter = "PDF Document (.pdf)|*.pdf"; // Filter files by extension
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == false) return;


                //PDFの保存
                paoRep.SavePDF(dlg.FileName);

                // *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                if (System.Windows.MessageBox.Show("保存した PDF を表示しますか？", "PDF の表示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo(dlg.FileName);
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
            }

        }

    }
}
