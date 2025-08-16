Imports Pao.Reports
Imports System.Net
Imports System.ServiceModel

Namespace Sample

    ''' <summary>
    ''' Window1.xaml の相互作用ロジック
    ''' </summary>
    Partial Public Class Window1
        Inherits Window

        Public Class PrintData
            Public Property id As Integer
            Public Property Data As String
        End Class

        Public Sub New()
            InitializeComponent()

            Me.Height = SystemParameters.PrimaryScreenHeight - 50

            ' TLS設定（.NET Framework 4.6以前で必要、4.7以降は冗長だが無害）
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
        End Sub

        ''' <summary>
        ''' GETクリック
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub Execute_Click(sender As Object, e As RoutedEventArgs)
            ' SOAPサービス の URL 取得
            Dim url As String = GetWebApiUrl()

            Try
                Dim data As Byte() = Nothing
                Dim b As System.Windows.Controls.Button = DirectCast(sender, System.Windows.Controls.Button)
                Dim paoRep As IReport

                If radPrint.IsChecked Then
                    paoRep = ReportCreator.GetReport() ' 印刷オブジェクト生成
                ElseIf radPreview.IsChecked Then
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
                    ' コンボボックスの選択項目の文字列で判定
                    Dim selectedItem As String = DirectCast(cmbReportType.SelectedItem, ComboBoxItem)?.Content?.ToString()
                    Select Case selectedItem
                        Case "単純な印刷データ"
                            data = client.getReports単純なサンプル()
                        Case "10の倍数"
                            data = client.getReports10の倍数()
                        Case "郵便番号一覧"
                            data = client.getReports郵便番号()
                        Case "見積書"
                            data = client.getReports見積書()
                        Case "請求書"
                            data = client.getReports請求書()
                        Case "商品大小分類"
                            data = client.getReports商品一覧()
                        Case "広告"
                            data = client.getReports広告()
                        Case Else
                            data = client.getReports単純なサンプル()
                    End Select
                    Output帳票(data)
                End Using
            Catch ex As Exception
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                                MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
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
            Dim url As String = "http://localhost:62141/Service1.svc"

            If radAzureWin.IsChecked = True Then
                ' Azure Windowsサーバに配置してある WEB API を使用
                url = "http://paopao-hce5b6hcfwaec0b4.japaneast-01.azurewebsites.net/Service1.svc"
            End If
            Return url
        End Function

        ''' <summary>
        ''' 帳票出力処理
        ''' </summary>
        ''' <param name="data"></param>
        Private Sub Output帳票(data As Byte())
            Dim repID As Integer = GetReportTypeId()

            'IReport インターフェースで宣言(印刷・レポートどちらでも使える入れ物の用意)
            Dim paoRep As IReport = Nothing

            If radPreview.IsChecked = True Then 'ラジオボタンでプレビューが選択されている場合
                'プレビューオブジェクトのインスタンスを獲得
                paoRep = ReportCreator.GetPreviewWpf()
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
                Dim result As Nullable(Of Boolean) = dlg.ShowDialog()

                ' Process save file dialog box results
                If result = False Then Return

                'PDFの保存
                paoRep.SavePDF(dlg.FileName)

                ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If System.Windows.MessageBox.Show("保存した PDF を表示しますか？", "PDF の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim startInfo As New System.Diagnostics.ProcessStartInfo(dlg.FileName)
                    startInfo.UseShellExecute = True
                    System.Diagnostics.Process.Start(startInfo)
                End If
            End If
        End Sub

    End Class
End Namespace