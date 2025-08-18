Imports System
Imports System.Windows.Forms
Imports Pao.Reports
Imports System.Drawing
Imports System.Diagnostics


Namespace Sample

    Public Partial Class Form1
        Inherits Form

        Private sharePath_ As String

        Public Sub New()
            InitializeComponent()

            ' C# との共有リソースパス取得
            sharePath_ = System.IO.Path.GetFullPath(System.IO.Directory.GetCurrentDirectory() & "/../../../../")
        End Sub

        Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
            Dim paoRep As IReport = Nothing

            If radPreview.Checked Then
                paoRep = ReportCreator.GetPreviewWpf()
            ElseIf radPrint.Checked Then
                paoRep = ReportCreator.GetReport()
            ElseIf radGetPrintDocument.Checked Then
                paoRep = ReportCreator.GetReport()
                ' ↑ OR ↓(どちらでも可)
                ' paoRep = ReportCreator.GetPreview()
            ElseIf radPDF.Checked Then
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

                    ' ヘッダのセット
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

                If (i + 1) Mod 15 = 0 Then
                    paoRep.PageEnd()
                End If
            Next

            If radPreview.Checked OrElse radPrint.Checked Then
                paoRep.DisplayDialog = True
                paoRep.DocumentName = "10の倍数の印刷ドキュメント"
                paoRep.z_PreviewWindowWpf.z_TitleText = "10の倍数の印刷プレビュー"
                paoRep.z_PreviewWindowWpf.z_Icon = New Icon(sharePath_ & "PreView.ico")
                paoRep.ZoomPreview = 77
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName = "御社製品名"
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName_ForeColor = Color.Blue

                MessageBox.Show("ページ数 : " & paoRep.AllPages.ToString())

                paoRep.Output()

            ElseIf radGetPrintDocument.Checked Then
                printDocument1 = paoRep.GetPrintDocument()
                prevWinForm.Document = printDocument1
                prevWinForm.InvalidatePreview()
                Return

            ElseIf radPDF.Checked Then
                saveFileDialog.FileName = "印刷データ"
                saveFileDialog.Filter = "PDF形式 (*.pdf)|*.pdf"
                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    paoRep.SavePDF(saveFileDialog.FileName)

                    ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                    If MessageBox.Show(Me, "PDFを表示しますか？", "PDF の表示", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim startInfo As New ProcessStartInfo(saveFileDialog.FileName)
                        startInfo.UseShellExecute = True
                        Process.Start(startInfo)
                    End If
                End If

            ElseIf radSVG.Checked Then
                saveFileDialog.FileName = "印刷データ"
                saveFileDialog.Filter = "html形式 (*.html)|*.html"
                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    paoRep.SaveSVGFile(saveFileDialog.FileName)

                    ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                    If MessageBox.Show(Me, "ブラウザで表示しますか？" & Environment.NewLine & "表示する場合、SVGプラグインが必要です。", "SVG の表示", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim startInfo As New ProcessStartInfo(saveFileDialog.FileName)
                        startInfo.UseShellExecute = True
                        Process.Start(startInfo)
                    End If
                End If

            ElseIf radXPS.Checked Then
                saveFileDialog.FileName = "印刷データ"
                saveFileDialog.Filter = "XPS形式 (*.xps)|*.xps"
                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    paoRep.SaveXPS(saveFileDialog.FileName)

                    ' *** 保存が失敗するときはOneDriveへ保存しようとしていると思われます

                    If MessageBox.Show(Me, "XPSを表示しますか？", "XPS の表示", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Dim startInfo As New ProcessStartInfo(saveFileDialog.FileName)
                        startInfo.UseShellExecute = True
                        Process.Start(startInfo)
                    End If
                End If
            End If

            If MessageBox.Show(Me, "続いて、印刷データXMLファイルを保存して再度読み込んでプレビューを行います。", "Save And Reload Print Data", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                Return
            End If

            paoRep.SaveXMLFile("印刷データ.prepe")
            paoRep = ReportCreator.GetPreview()
            paoRep.LoadXMLFile("印刷データ.prepe")
            paoRep.Output()
        End Sub

    End Class

End Namespace
