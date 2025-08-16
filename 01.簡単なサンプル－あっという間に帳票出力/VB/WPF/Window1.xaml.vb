Imports System
Imports System.Windows
Imports Pao.Reports

Namespace Sample

    Public Partial Class Window1
        Inherits Window

        Private printDocument1 As System.Drawing.Printing.PrintDocument = Nothing
        Private sharePath_ As String

        Public Sub New()
            InitializeComponent()
            Me.Height = SystemParameters.PrimaryScreenHeight - 50
            sharePath_ = System.IO.Path.GetFullPath(System.IO.Directory.GetCurrentDirectory() & "/../../../../")
        End Sub

        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Dim paoRep As IReport

            If radPreview.IsChecked = True Then
                paoRep = ReportCreator.GetPreviewWpf()
            ElseIf radPrint.IsChecked = True OrElse radXPS.IsChecked = True Then
                paoRep = ReportCreator.GetReport()
            ElseIf radPDF.IsChecked = True Then
                paoRep = ReportCreator.GetPdf()
            Else
                paoRep = ReportCreator.GetReport()
            End If

            paoRep.LoadDefFile(sharePath_ & "レポート定義ファイル.prepd")

            Dim page As Integer = 0
            Dim line As Integer = 0
            For i As Integer = 0 To 59
                If i Mod 15 = 0 Then
                    paoRep.PageStart()
                    page += 1
                    line = 0
                    paoRep.Write("日付", DateTime.Now.ToString())
                    paoRep.Write("頁数", "Page - " & page.ToString())
                    paoRep.z_Objects.SetObject("フォントサイズ")
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12
                    paoRep.Write("フォントサイズ", "フォントサイズ" & Environment.NewLine & " 変更後")
                    If page = 2 Then
                        paoRep.Write("Line3", "")
                    End If
                End If
                line += 1
                paoRep.Write("行番号", (i + 1).ToString(), line)
                paoRep.Write("10倍数", ((i + 1) * 10).ToString(), line)
                paoRep.Write("横線", line)
                If (i + 1) Mod 15 = 0 Then paoRep.PageEnd()
            Next

            If radPreview.IsChecked = True OrElse radPrint.IsChecked = True Then
                paoRep.DisplayDialog = True
                paoRep.DocumentName = "10の倍数の印刷ドキュメント"
                paoRep.z_PreviewWindowWpf.z_TitleText = "10の倍数の印刷プレビュー"
                paoRep.z_PreviewWindowWpf.z_Icon = New System.Drawing.Icon(sharePath_ & "PreView.ico")
                paoRep.ZoomPreview = 77
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName = "御社製品名"
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName_ForeColor = System.Drawing.Color.Blue
                MessageBox.Show("ページ数 : " & paoRep.AllPages.ToString())
                paoRep.Output()
            ElseIf radGetPrintDocument.IsChecked = True Then
                printDocument1 = paoRep.GetPrintDocument()
                prevWinForm.Document = printDocument1
                prevWinForm.InvalidatePreview()
                Return
            ElseIf radPDF.IsChecked = True Then
                Dim saveFileName As String = ShowSaveDialog("pdf")
                If saveFileName = "" Then Return
                paoRep.SavePDF(saveFileName)

                ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If MessageBox.Show(Me, "PDFを表示しますか？", "PDF の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            ElseIf radSVG.IsChecked = True Then
                Dim saveFileName As String = ShowSaveDialog("html")
                If saveFileName = "" Then Return
                paoRep.SaveSVGFile(saveFileName)

                ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If MessageBox.Show(Me, "ブラウザで表示しますか？" & Environment.NewLine & "表示する場合、SVGプラグインが必要です。", "SVG の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            ElseIf radXPS.IsChecked = True Then
                Dim saveFileName As String = ShowSaveDialog("xps")
                If saveFileName = "" Then Return
                paoRep.SaveXPS(saveFileName)

                ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                If MessageBox.Show(Me, "XPSを表示しますか？", "XPS の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            End If

            If MessageBox.Show(Me, "続いて、印刷データXMLファイルを保存して再度読み込んでプレビューを行います。", "Save And Reload Print Data", MessageBoxButton.OKCancel, MessageBoxImage.Question) = MessageBoxResult.Cancel Then Return
            paoRep.SaveXMLFile("印刷データ.prepe")
            paoRep = ReportCreator.GetPreview()
            paoRep.LoadXMLFile("印刷データ.prepe")
            paoRep.Output()
        End Sub

        Private Sub Hyperlink_RequestNavigate(sender As Object, e As System.Windows.Navigation.RequestNavigateEventArgs)
            Dim psi As New System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri)
            psi.UseShellExecute = True
            System.Diagnostics.Process.Start(psi)
            e.Handled = True
        End Sub

        Private Function ShowSaveDialog(type As String) As String
            Dim dlg As New Microsoft.Win32.SaveFileDialog()
            dlg.FileName = "印刷データ"
            dlg.DefaultExt = "." & type.ToLower()
            dlg.Filter = type.ToUpper() & " Document (." & type.ToLower() & ")|*." & type.ToLower()
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Dim result? As Boolean = dlg.ShowDialog()
            If result = True Then Return dlg.FileName Else Return ""
        End Function

        Private Sub OpenSaveFile(filePath As String)
            Dim type As String = System.IO.Path.GetExtension(filePath)?.TrimStart("."c).ToUpperInvariant()
            If MessageBox.Show("保存した " & type & " を表示しますか？", type & " の表示", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                Dim psi As New System.Diagnostics.ProcessStartInfo(filePath)
                psi.UseShellExecute = True
                System.Diagnostics.Process.Start(psi)
            End If
        End Sub

    End Class
End Namespace