using Pao.Reports;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;

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
        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            // SOAPサービス の URL 取得
            string url = GetWebApiUrl();

            try
            {
                byte[] data = null;
                System.Windows.Controls.Button b = (System.Windows.Controls.Button)sender;
                IReport paoRep;

                if (radPrint.IsChecked == true)
                {
                    paoRep = ReportCreator.GetReport(); // 印刷オブジェクト生成
                }
                else if (radPreview.IsChecked == true)
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
                using (var client = new ClientWpf.ServiceReference1.Service1Client(binding, address))
                {


                    // 選択された帳票タイプに応じてWCFサービス呼び出し
                    // コンボボックスの選択項目の文字列で判定
                    var selectedItem = ((ComboBoxItem)cmbReportType.SelectedItem)?.Content?.ToString();
                    switch (selectedItem)
                    {
                        case "単純な印刷データ":
                            data = client.getReports単純なサンプル();
                            break;
                        case "10の倍数":
                            data = client.getReports10の倍数();
                            break;
                        case "郵便番号一覧":
                            data = client.getReports郵便番号();
                            break;
                        case "見積書":
                            data = client.getReports見積書();
                            break;
                        case "請求書":
                            data = client.getReports請求書();
                            break;
                        case "商品大小分類":
                            data = client.getReports商品一覧();
                            break;
                        case "広告":
                            data = client.getReports広告();
                            break;
                        default:
                            data = client.getReports単純なサンプル();
                            break;
                    }
                    Output帳票(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                                MessageBoxButton.OK, MessageBoxImage.Error);
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
            string url = "http://localhost:62141/Service1.svc";

            if (radAzureWin.IsChecked == true)
            {
                // Azure Windowsサーバに配置してある WEB API を使用
                url = "http://paopao-hce5b6hcfwaec0b4.japaneast-01.azurewebsites.net/Service1.svc";
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
                paoRep = ReportCreator.GetPreviewWpf();
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
