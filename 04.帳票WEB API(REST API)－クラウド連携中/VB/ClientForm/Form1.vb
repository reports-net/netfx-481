Imports Pao.Reports
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports System.Threading.Tasks
Imports System.Web.Script.Serialization
Imports System.Windows.Forms



Partial Public Class Form1
        Inherits Form

        Public Class PrintData
            Public Property Id As Integer
            Public Property Data As String
        End Class

        Public Sub New()
        InitializeComponent()

        ' TLS設定（.NET Framework 4.6以前で必要、4.7以降は冗長だが無害）
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
    End Sub

    Private Function GetWebApiUrl() As String
            ' 接続先URL : デフォルトはローカルでデバッグ環境
            Dim url As String = "https://localhost:44317/"

            If radWebApiAzureWin.Checked Then
                ' Azure Windowsサーバに配置してある WEB API を使用
                url = "https://webapimvc-b9ake6c0hwetgbdn.canadacentral-01.azurewebsites.net/"
            End If

            Return url
        End Function

    ''' <summary>
    ''' GETクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click
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

            Dim id As Integer = 0
            If opt単純な印刷データ.Checked Then id = 0
            If opt10の倍数.Checked Then id = 1
            If opt住所一覧.Checked Then id = 2
            If opt見積書.Checked Then id = 3
            If opt請求書.Checked Then id = 4
            If opt商品一覧.Checked Then id = 5
            If opt広告.Checked Then id = 6

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

    ''' <summary>
    ''' POSTクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        ' WEB API (REST API) の URL 取得
        Dim webApiUrl As String = GetWebApiUrl()

        ' POST用データ作成
        Dim pdIn As New PrintData()
        If opt単純な印刷データ.Checked Then pdIn.Id = 0
        If opt10の倍数.Checked Then pdIn.Id = 1
        If opt住所一覧.Checked Then pdIn.Id = 2
        If opt見積書.Checked Then pdIn.Id = 3
        If opt請求書.Checked Then pdIn.Id = 4
        If opt商品一覧.Checked Then pdIn.Id = 5
        If opt広告.Checked Then pdIn.Id = 6

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

    Private Sub Output帳票(data() As Byte)
            'IReport インターフェースで宣言(印刷・レポートどちらでも使える入れ物の用意)
            Dim paoRep As IReport = Nothing

            If radPreview.Checked Then 'ラジオボタンでプレビューが選択されている場合
                'プレビューオブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPreviewWpf()
            ElseIf radPrint.Checked Then ' 印刷が選択されている場合
                '印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport()
            ElseIf radPDF.Checked Then ' PDFが選択されている場合
            If Not opt広告.Checked Then
                'PDF出力オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPdf()
            Else
                'イメージPDF出力オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetImagePdf()
                End If
            Else
                '印刷オブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetReport()
            End If

            paoRep.LoadData(data) '印刷データを読み込む

            If Not radPDF.Checked Then 'PDF出力が選択されていない場合
                paoRep.Output() '印刷または、プレビューを実行
            Else
            'PDF出力
            saveFileDialog.FileName = "印刷データ"
            saveFileDialog.Filter = "PDF形式 (*.pdf)|*.pdf"

            If saveFileDialog.ShowDialog() = DialogResult.OK Then
                paoRep.SavePDF(saveFileDialog.FileName) '印刷データの保存

                '*** PDFの保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If MessageBox.Show(Me, "PDFを表示しますか？", "PDF の表示", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim startInfo As New System.Diagnostics.ProcessStartInfo(saveFileDialog.FileName)
                    startInfo.UseShellExecute = True
                    System.Diagnostics.Process.Start(startInfo)
                End If
            End If
        End If
        End Sub

    End Class

