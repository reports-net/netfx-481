Imports System
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Windows
Imports System.Windows.Controls
Imports System.Net
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports Pao.Reports

Namespace Sample

    Public Class PrintData
        Public Property Id As Integer
        Public Property Data As String
    End Class

    ''' <summary>
    ''' Window1.xaml の相互作用ロジック
    ''' </summary>
    Partial Public Class Window1
        Inherits Window

        Public Sub New()
            InitializeComponent()
            Me.Height = SystemParameters.PrimaryScreenHeight - 50

            ' TLS設定（.NET Framework 4.6以前で必要、4.7以降は冗長だが無害）
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls

        End Sub

        ''' <summary>
        ''' GETクリック
        ''' </summary>
        Private Async Sub ExecuteGet_Click(sender As Object, e As RoutedEventArgs)
            ' WEB API (REST API) の URL 取得
            Dim webApiUrl As String = GetWebApiUrl()

            ' ★HTTPS証明書検証をスキップ（開発用）★
            ServicePointManager.ServerCertificateValidationCallback =
                Function(sender2, certificate, chain, sslPolicyErrors) True

            ' WEB API (REST API) のURLセット
            Using client As New HttpClient()
                client.BaseAddress = New Uri(webApiUrl)
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(
                    New MediaTypeWithQualityHeaderValue("application/json"))

                Dim id As Integer = GetReportTypeId()

                ' GET リクエスト実行
                Dim response As HttpResponseMessage = Await client.GetAsync("Home/GetReports?Id=" & id.ToString())
                response.EnsureSuccessStatusCode()

                ' レスポンスをJSON文字列として取得
                Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                ' JSON を PrintData に変換
                Dim serializer As New JavaScriptSerializer()
                Dim pd As PrintData = serializer.Deserialize(Of PrintData)(jsonResponse)

                ' Base64デコードして帳票出力
                Dim data() As Byte = Convert.FromBase64String(pd.Data)
                Output帳票(data)
            End Using
        End Sub

        Private Async Sub ExecutePost_Click(sender As Object, e As RoutedEventArgs)
            ' WEB API (REST API) の URL 取得
            Dim webApiUrl As String = GetWebApiUrl()

            Dim pdIn As New PrintData()
            pdIn.Id = GetReportTypeId()

            ' WEB API (REST API) のURLセット
            Using client As New HttpClient()
                client.BaseAddress = New Uri(webApiUrl)
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Accept.Add(
                    New MediaTypeWithQualityHeaderValue("application/json"))

                ' JavaScriptSerializer作成
                Dim serializer As New JavaScriptSerializer()

                ' PrintData を JSON に変換
                Dim jsonContent As String = serializer.Serialize(pdIn)
                Dim content As New StringContent(jsonContent, Encoding.UTF8)
                content.Headers.ContentType = New MediaTypeHeaderValue("application/json")

                ' POST リクエスト実行
                Dim response As HttpResponseMessage = Await client.PostAsync("Home/PostReports", content)
                response.EnsureSuccessStatusCode()

                ' レスポンスをJSON文字列として取得
                Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                ' JSON を PrintData に変換
                Dim pdOut As PrintData = serializer.Deserialize(Of PrintData)(jsonResponse)

                ' Base64デコードして帳票出力
                Dim data() As Byte = Convert.FromBase64String(pdOut.Data)
                Output帳票(data)
            End Using
        End Sub

        ''' <summary>
        ''' レポートタイプIDを取得
        ''' </summary>
        ''' <returns>レポートタイプID</returns>
        Private Function GetReportTypeId() As Integer
            Dim selectedItem As ComboBoxItem = TryCast(cmbReportType.SelectedItem, ComboBoxItem)
            Dim selectedContent As String = If(selectedItem?.Content?.ToString(), String.Empty)

            If selectedContent.Contains("単純な印刷データ") Then
                Return 0
            ElseIf selectedContent.Contains("10の倍数") Then
                Return 1
            ElseIf selectedContent.Contains("郵便番号一覧") Then
                Return 2
            ElseIf selectedContent.Contains("見積書") Then
                Return 3
            ElseIf selectedContent.Contains("請求書") Then
                Return 4
            ElseIf selectedContent.Contains("商品大小分類") Then
                Return 5
            ElseIf selectedContent.Contains("広告") Then
                Return 6
            Else
                Return 0 ' デフォルト
            End If
        End Function

        ''' <summary>
        ''' WEB API URL 取得
        ''' </summary>
        ''' <returns>WEB API URL</returns>
        Private Function GetWebApiUrl() As String
            ' 接続先URL : デフォルトはローカルでデバッグ環境
            Dim url As String = "https://localhost:44317/"

            If radAzureWin.IsChecked = True Then
                ' Azure Windowsサーバに配置してある WEB API を使用
                url = "https://webapimvc-b9ake6c0hwetgbdn.canadacentral-01.azurewebsites.net/"
            End If
            Return url
        End Function

        ''' <summary>
        ''' 帳票出力処理
        ''' </summary>
        ''' <param name="data"></param>
        Private Sub Output帳票(data() As Byte)
            Dim repID As Integer = GetReportTypeId()

            'IReport インターフェースで宣言(印刷・レポートどちらでも使える入れ物の用意)
            Dim paoRep As IReport = Nothing

            If radPreview.IsChecked = True Then 'ラジオボタンでプレビューが選択されている場合
                'プレビューオブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPreview()
            ElseIf radPrint.IsChecked = True Then ' 印刷が選択されている場合
                '印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport()
            ElseIf radPDF.IsChecked = True Then ' PDFが選択されている場合
                If Not cmbReportType.Text.Contains("広告") Then
                    'PDF出力オブジェクトのインスタンスを獲得
                    paoRep = ReportCreator.GetPdf()
                Else
                    'イメージPDF出力オブジェクトのインスタンスを獲得
                    paoRep = ReportCreator.GetImagePdf()
                End If
            Else
                'SVGとWPSは、印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport()
            End If

            paoRep.LoadData(data) '印刷データを読み込む

            If Not radPDF.IsChecked = True Then 'PDF出力が選択されていない場合
                paoRep.Output() '印刷または、プレビューを実行
            Else
                Dim dlg As New Microsoft.Win32.SaveFileDialog()
                dlg.FileName = "印刷データ"
                dlg.DefaultExt = ".pdf"
                dlg.Filter = "PDF Document (.pdf)|*.pdf" ' Filter files by extension
                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

                ' Show save file dialog box
                Dim result As Boolean? = dlg.ShowDialog()

                ' Process save file dialog box results
                If result = False Then Return

                'PDFの保存
                paoRep.SavePDF(dlg.FileName)

                '*** PDFの保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If MessageBox.Show("保存した PDF を表示しますか？", "PDF の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim startInfo = New System.Diagnostics.ProcessStartInfo(dlg.FileName)
                    startInfo.UseShellExecute = True
                    System.Diagnostics.Process.Start(startInfo)
                End If
            End If
        End Sub

    End Class

End Namespace