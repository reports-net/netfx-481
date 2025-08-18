Imports System.Net
Imports Pao.Reports
Imports System.ServiceModel


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
        Dim url As String = "https://localhost:62141/"

        If radWebApiAzureWin.Checked Then
                ' Azure Windowsサーバに配置してある WEB API を使用
                url = "https://webapimvc-b9ake6c0hwetgbdn.canadacentral-01.azurewebsites.net/"
            End If

            Return url
        End Function

    ''' <summary>
    ''' 実行ボタンクリック（WCF-SOAP版）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        ' 接続先URL : デフォルトはローカルでデバッグ環境
        Dim url As String = "http://localhost:62141/Service1.svc"
        If radWebApiAzureWin.Checked Then
            ' Azure Windowsサーバに配置してある WCF-SOAP サービスを使用
            url = "http://paopao-hce5b6hcfwaec0b4.japaneast-01.azurewebsites.net/Service1.svc"
        End If
        Try
            Dim data As Byte() = Nothing
            Dim b As System.Windows.Forms.Button = DirectCast(sender, System.Windows.Forms.Button)
            Dim paoRep As IReport
            If b.Text = "印刷" Then
                paoRep = ReportCreator.GetReport() ' 印刷オブジェクト生成
            ElseIf b.Text = "プレビュー" Then
                paoRep = ReportCreator.GetPreviewWpf() ' プレビュー画面を作成
            Else
                paoRep = ReportCreator.GetPdf() ' PDFオブジェクト生成
            End If
            ' WCF-SOAPクライアントを動的に作成
            Dim binding As New BasicHttpBinding(BasicHttpSecurityMode.None) With {
                    .MaxReceivedMessageSize = 10 * 1024 * 1024,   ' 10 MB
                    .SendTimeout = TimeSpan.FromMinutes(2),
                    .ReceiveTimeout = TimeSpan.FromMinutes(2)
                }
            Dim address As New EndpointAddress(url)
            Using client As New ServiceReference1.Service1Client(binding, address)
                ' 選択された帳票タイプに応じてWCFサービス呼び出し
                If opt単純な印刷データ.Checked Then data = client.getReports単純なサンプル()
                If opt10の倍数.Checked Then data = client.getReports10の倍数()
                If opt住所一覧.Checked Then data = client.getReports郵便番号()
                If opt見積書.Checked Then data = client.getReports見積書()
                If opt請求書.Checked Then data = client.getReports請求書()
                If opt商品一覧.Checked Then data = client.getReports商品一覧()
                If opt広告.Checked Then data = client.getReports広告()
                Output帳票(data)
            End Using
            '' チャンネル閉じる
            'DirectCast(client, ICommunicationObject).Close()
        Catch ex As Exception
            MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    ''' <summary>
    ''' フォーム終了処理
    ''' </summary>
    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        MyBase.OnFormClosed(e)
    End Sub
End Class

