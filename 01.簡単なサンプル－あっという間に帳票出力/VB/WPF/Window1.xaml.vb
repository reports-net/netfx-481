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

            paoRep.LoadDefFile(sharePath_ & "���|�[�g��`�t�@�C��.prepd")

            Dim page As Integer = 0
            Dim line As Integer = 0
            For i As Integer = 0 To 59
                If i Mod 15 = 0 Then
                    paoRep.PageStart()
                    page += 1
                    line = 0
                    paoRep.Write("���t", DateTime.Now.ToString())
                    paoRep.Write("�Ő�", "Page - " & page.ToString())
                    paoRep.z_Objects.SetObject("�t�H���g�T�C�Y")
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12
                    paoRep.Write("�t�H���g�T�C�Y", "�t�H���g�T�C�Y" & Environment.NewLine & " �ύX��")
                    If page = 2 Then
                        paoRep.Write("Line3", "")
                    End If
                End If
                line += 1
                paoRep.Write("�s�ԍ�", (i + 1).ToString(), line)
                paoRep.Write("10�{��", ((i + 1) * 10).ToString(), line)
                paoRep.Write("����", line)
                If (i + 1) Mod 15 = 0 Then paoRep.PageEnd()
            Next

            If radPreview.IsChecked = True OrElse radPrint.IsChecked = True Then
                paoRep.DisplayDialog = True
                paoRep.DocumentName = "10�̔{���̈���h�L�������g"
                paoRep.z_PreviewWindowWpf.z_TitleText = "10�̔{���̈���v���r���["
                paoRep.z_PreviewWindowWpf.z_Icon = New System.Drawing.Icon(sharePath_ & "PreView.ico")
                paoRep.ZoomPreview = 77
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName = "��А��i��"
                paoRep.z_PreviewWindowWpf.z_VersionWindow.ProductName_ForeColor = System.Drawing.Color.Blue
                MessageBox.Show("�y�[�W�� : " & paoRep.AllPages.ToString())
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

                ' *** �ۑ������s����Ƃ���OneDrive�֕ۑ����悤�Ƃ��Ă���Ǝv���܂�

                If MessageBox.Show(Me, "PDF��\�����܂����H", "PDF �̕\��", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            ElseIf radSVG.IsChecked = True Then
                Dim saveFileName As String = ShowSaveDialog("html")
                If saveFileName = "" Then Return
                paoRep.SaveSVGFile(saveFileName)

                ' *** �ۑ������s����Ƃ���OneDrive�֕ۑ����悤�Ƃ��Ă���Ǝv���܂�

                If MessageBox.Show(Me, "�u���E�U�ŕ\�����܂����H" & Environment.NewLine & "�\������ꍇ�ASVG�v���O�C�����K�v�ł��B", "SVG �̕\��", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            ElseIf radXPS.IsChecked = True Then
                Dim saveFileName As String = ShowSaveDialog("xps")
                If saveFileName = "" Then Return
                paoRep.SaveXPS(saveFileName)

                ' *** �ۑ������s����Ƃ���OneDrive�֕ۑ����悤�Ƃ��Ă���Ǝv���܂�

                If MessageBox.Show(Me, "XPS��\�����܂����H", "XPS �̕\��", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                    Dim psi As New System.Diagnostics.ProcessStartInfo(saveFileName)
                    psi.UseShellExecute = True
                    System.Diagnostics.Process.Start(psi)
                End If
            End If

            If MessageBox.Show(Me, "�����āA����f�[�^XML�t�@�C����ۑ����čēx�ǂݍ���Ńv���r���[���s���܂��B", "Save And Reload Print Data", MessageBoxButton.OKCancel, MessageBoxImage.Question) = MessageBoxResult.Cancel Then Return
            paoRep.SaveXMLFile("����f�[�^.prepe")
            paoRep = ReportCreator.GetPreview()
            paoRep.LoadXMLFile("����f�[�^.prepe")
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
            dlg.FileName = "����f�[�^"
            dlg.DefaultExt = "." & type.ToLower()
            dlg.Filter = type.ToUpper() & " Document (." & type.ToLower() & ")|*." & type.ToLower()
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Dim result? As Boolean = dlg.ShowDialog()
            If result = True Then Return dlg.FileName Else Return ""
        End Function

        Private Sub OpenSaveFile(filePath As String)
            Dim type As String = System.IO.Path.GetExtension(filePath)?.TrimStart("."c).ToUpperInvariant()
            If MessageBox.Show("�ۑ����� " & type & " ��\�����܂����H", type & " �̕\��", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                Dim psi As New System.Diagnostics.ProcessStartInfo(filePath)
                psi.UseShellExecute = True
                System.Diagnostics.Process.Start(psi)
            End If
        End Sub

    End Class
End Namespace