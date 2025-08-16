' 変換した VB.NET コード
Imports Pao.Reports
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Services.Description

Namespace WebApplication1.Controllers
    Public Class HomeController
        Inherits Controller

        Private sqlcon As SqlConnection = Nothing

        Function Index() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Function Index(ReportsKind As String, PdfAction As String) As ActionResult
            If PdfAction.StartsWith("View") Then
                ViewBag.ReportsKind = ReportsKind
                Return View("ShowPdf")
            Else
                Dim pdfBytes As Byte() = GeneratePdfBytes(ReportsKind)
                Return File(pdfBytes, "application/pdf", ReportsKind & ".pdf")
            End If
        End Function

        Function GetPdfStream(ReportsKind As String) As ActionResult
            Dim pdfBytes As Byte() = GeneratePdfBytes(ReportsKind)
            Return File(pdfBytes, "application/pdf")
        End Function

        Private Function GeneratePdfBytes(ReportsKind As String) As Byte()
            sqlcon = New SqlConnection("Server=tcp:fzxu46e9ck.database.windows.net,1433;Initial Catalog=Reports.net.Sample;Persist Security Info=False;User ID=AzureLab;Password=ayakaRk9504w;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")

            Dim paoRep As IReport = ReportCreator.GetPdf()
            Select Case ReportsKind
                Case "simple"
                    makeReports単純なサンプル(paoRep)
                Case "simple10"
                    makeReports10の倍数(paoRep)
                Case "mitsumori"
                    makeReports見積書(paoRep)
                Case "zipcode"
                    makeReports郵便番号(paoRep)
                Case "koukoku"
                    paoRep = ReportCreator.GetImagePdf()
                    makeReports広告(paoRep)
                Case "invoice"
                    makeReports請求書(paoRep)
                Case "itemlist"
                    makeReports商品一覧(paoRep)
            End Select

            Using memoryStream As New MemoryStream()
                paoRep.SavePDF(memoryStream)
                Return memoryStream.ToArray()
            End Using
        End Function

        Protected Sub makeReports単純なサンプル(paoRep As IReport)

            '埋め込みリソース方式で帳票デザインファイル取得＆ストリーム読み込み
            Dim path As String = Assembly.GetExecutingAssembly().GetName().Name & ".simple.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)
            paoRep.LoadDefFile(stream)

            paoRep.PageStart()
            paoRep.Write("Text2", ".NET Framework MVCで作った
印刷データですよ～ん♪")
            paoRep.PageEnd()
        End Sub

        Protected Sub makeReports10の倍数(paoRep As IReport)

            '埋め込みリソース方式で帳票デザインファイル取得＆ストリーム読み込み
            Dim path As String = Assembly.GetExecutingAssembly().GetName().Name & ".sample10.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path)
            paoRep.LoadDefFile(stream)

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

                If ((i + 1) Mod 15) = 0 Then
                    paoRep.PageEnd()
                End If
            Next
        End Sub

        ' その他の makeReports～ メソッド（見積書・請求書・郵便番号・広告・商品一覧）は省略（必要に応じて追加可能）
        Protected Sub makeReports見積書(paoRep As IReport)
            Dim path As String = ""

            Dim sqlda As New SqlDataAdapter("SELECT * FROM 見積ヘッダ ORDER BY 見積番号", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds, "見積ヘッダ")

            sqlda = New SqlDataAdapter("SELECT * FROM 見積明細 ORDER BY 見積番号,行番号", sqlcon)
            sqlda.Fill(ds, "見積明細")

            Dim ht As DataTable = ds.Tables(0)
            For Each hdr As DataRow In ht.Rows

                '外部ファイル方式で帳票デザインファイル取得＆App_Dataフォルダから読み込み
                path = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/mitsumori.head.prepd")
                paoRep.LoadDefFile(path)
                paoRep.PageStart()
                paoRep.Write("お客様名", hdr("お客様名").ToString())
                paoRep.Write("担当者名", hdr("担当者名").ToString())
                paoRep.PageEnd()

                '外部ファイル方式で帳票デザインファイル取得＆App_Dataフォルダから読み込み
                path = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/mitsumori.prepd")
                paoRep.LoadDefFile(path)
                paoRep.PageStart()

                paoRep.Write("見積番号", hdr("見積番号").ToString())
                paoRep.Write("お客様名", hdr("お客様名").ToString())
                paoRep.Write("担当者名", hdr("担当者名").ToString())
                paoRep.Write("見積日", CType(hdr("見積日"), DateTime).ToString("yyyy年M月d日"))
                paoRep.Write("ヘッダ合計", "\ " & String.Format("{0:N0}", hdr("合計金額")))
                paoRep.Write("消費税額", String.Format("{0:N0}", hdr("消費税額")))
                paoRep.Write("フッタ合計", String.Format("{0:N0}", hdr("合計金額")))

                For i As Integer = 0 To 6
                    paoRep.Write("品番白", i + 1)
                    paoRep.Write("品名白", i + 1)
                    paoRep.Write("数量白", i + 1)
                    paoRep.Write("単価白", i + 1)
                    paoRep.Write("金額白", i + 1)
                    paoRep.Write("品番青", i + 1)
                    paoRep.Write("品名青", i + 1)
                    paoRep.Write("数量青", i + 1)
                    paoRep.Write("単価青", i + 1)
                    paoRep.Write("金額青", i + 1)
                Next

                Dim dv As New DataView(ds.Tables("見積明細"))
                dv.RowFilter = "見積番号 = '" & hdr("見積番号").ToString() & "'"

                For i As Integer = 0 To dv.Count - 1
                    paoRep.Write("品番", dv(i)("品番").ToString(), i + 1)
                    paoRep.Write("品名", dv(i)("品名").ToString(), i + 1)
                    paoRep.Write("数量", dv(i)("数量").ToString(), i + 1)
                    paoRep.Write("単価", String.Format("{0:N0}", dv(i)("単価")), i + 1)
                    paoRep.Write("金額", String.Format("{0:N0}", dv(i)("金額")), i + 1)
                Next

                paoRep.PageEnd()
            Next
        End Sub

        ' --- 郵便番号 ---
        Sub makeReports郵便番号(paoRep As IReport)


            '埋め込みリソース方式で帳票デザインファイル取得＆ストリーム方式
            Dim path1 As String = Assembly.GetExecutingAssembly().GetName().Name & ".zipcode1.prepd"
            Dim stream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream(path1)
            Dim path2 As String = Assembly.GetExecutingAssembly().GetName().Name & ".zipcode2.prepd"
            Dim stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(path2)

            Dim sqlda As New SqlDataAdapter("select * from 郵便番号テーブル", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds, "PostTable")
            Dim table As DataTable = ds.Tables(0)

            Dim page As Integer = 0
            Dim line As Integer = 999
            Dim hDate As String = DateTime.Now.ToString()

            paoRep.LoadDefFile(stream1)
            For Each row As DataRow In table.Rows
                line += 1
                If line > 32 Then
                    If page <> 0 Then paoRep.PageEnd()
                    page += 1

                    If page = 6 Then
                        paoRep.LoadDefFile(stream2)
                    End If

                    paoRep.PageStart()
                    paoRep.Write("日時", hDate)
                    paoRep.Write("ページ", "Page-" & page.ToString())

                    If page < 6 Then
                        paoRep.Write("QR", row("郵便番号").ToString() & " " & row("市区町村").ToString() & row("住所").ToString())
                    End If
                    line = 1
                End If

                paoRep.Write("郵便番号", row("郵便番号").ToString(), line)
                paoRep.Write("市区町村", row("市区町村").ToString(), line)
                paoRep.Write("住所", row("住所").ToString(), line)
                paoRep.Write("横罫線", line)

                If page > 5 AndAlso line Mod 2 = 0 Then
                    paoRep.Write("網掛け", line \ 2)
                End If
            Next
            paoRep.PageEnd()
        End Sub

        Sub makeReports広告(paoRep As IReport)

            '外部ファイル方式で帳票デザインファイル取得＆App_Dataフォルダから読み込み
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/koukoku.prepd")
            paoRep.LoadDefFile(path)

            Dim gpath As String = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/img/")

            Dim sqlda As New SqlDataAdapter("select * from 広告情報", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds)
            Dim table As DataTable = ds.Tables(0)

            For Each row As DataRow In table.Rows
                paoRep.PageStart()
                paoRep.Write("製品名", row("製品名").ToString())
                paoRep.Write("キャッチフレーズ", row("キャッチフレーズ").ToString())
                paoRep.Write("商品コード", row("商品コード").ToString())
                paoRep.Write("JANコード", row("商品コード").ToString())
                paoRep.Write("売り文句", row("売り文句").ToString())
                paoRep.Write("説明", row("説明").ToString())
                paoRep.Write("価格", row("価格").ToString())
                paoRep.Write("画像1", System.IO.Path.Combine(gpath, row("画像1").ToString()))
                paoRep.Write("画像2", System.IO.Path.Combine(gpath, row("画像2").ToString()))
                paoRep.Write("QR", row("製品名").ToString() & " " & row("キャッチフレーズ").ToString())
                paoRep.PageEnd()
            Next
        End Sub

        Sub makeReports請求書(paoRep As IReport)

            '外部ファイル方式で帳票デザインファイル取得＆App_Dataフォルダから読み込み
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/invoice.prepd")
            paoRep.LoadDefFile(path)

            ' 会社角印画像ファイルパス
            Dim gpath As String = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/img/")

            Dim ds As New DataSet()
            Dim sqlda As New SqlDataAdapter("select * from 請求ヘッダ ORDER BY 請求番号", sqlcon)
            sqlda.Fill(ds, "請求ヘッダ")
            sqlda = New SqlDataAdapter("select * from 請求明細 ORDER BY 請求番号, 行番号", sqlcon)
            sqlda.Fill(ds, "請求明細")

            Dim arr_w As Single() = {-5, 44, -20, -10, -9}
            Dim ht As DataTable = ds.Tables(0)

            For Each hdr As DataRow In ht.Rows
                paoRep.PageStart()
                paoRep.Write("txtNo", hdr("請求番号").ToString())
                paoRep.Write("txtCustomer", hdr("お客様名").ToString())
                paoRep.Write("txtDate", DateTime.Now.ToString("yyyy年M月d日"))
                paoRep.Write("Image1", gpath & "kakuin.png")

                paoRep.z_Objects.SetObject("hLine")
                Dim maxHLine As Integer = paoRep.z_Objects.z_Line.Repeat - 1
                paoRep.z_Objects.SetObject("vLine")
                Dim maxVLine As Integer = paoRep.z_Objects.z_Line.Repeat - 1

                For i As Integer = 0 To maxHLine - 1
                    paoRep.Write("hLine", i + 1)

                    If i = 0 Then
                        paoRep.z_Objects.SetObject("hLine", i + 1)
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F
                    End If
                    If i = 1 Then
                        paoRep.z_Objects.SetObject("hLine", i + 1)
                        paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double
                    End If

                    paoRep.Write("LineRect", i + 1)
                    paoRep.z_Objects.SetObject("LineRect", i + 1)

                    If i > 0 AndAlso i < maxHLine - 3 Then
                        If i Mod 2 = 1 Then
                            paoRep.z_Objects.z_Square.PaintColor = Color.White
                        Else
                            paoRep.z_Objects.z_Square.PaintColor = Color.LightSkyBlue
                        End If
                    ElseIf i >= maxHLine - 3 Then
                        paoRep.z_Objects.z_Square.PaintColor = Color.FromArgb(255, 255, 180)
                    End If

                    Dim svX As Single = -1
                    For j As Integer = 0 To maxVLine - 1
                        paoRep.z_Objects.SetObject("field" & (j + 1).ToString(), i + 1)
                        paoRep.z_Objects.z_Text.Width += arr_w(j)
                        If j > 0 Then
                            paoRep.z_Objects.z_Text.X = svX
                        End If
                        svX = paoRep.z_Objects.z_Text.X + paoRep.z_Objects.z_Text.Width

                        If i = 0 Then
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = True
                        Else
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = False
                            paoRep.z_Objects.z_Text.z_FontAttr.Size = 12
                            Select Case j + 1
                                Case 1
                                    paoRep.z_Objects.z_Text.TextAlign = PmAlignType.Center
                                Case 2
                                    paoRep.z_Objects.z_Text.TextAlign = PmAlignType.Left
                                Case 3, 4, 5
                                    paoRep.z_Objects.z_Text.TextAlign = PmAlignType.Right
                            End Select
                        End If
                    Next
                Next

                For j As Integer = maxHLine To maxHLine - 3 + 1 Step -1
                    paoRep.z_Objects.SetObject("field4", j)
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 16
                    paoRep.z_Objects.z_Text.TextAlign = PmAlignType.Center
                    paoRep.z_Objects.z_Text.z_FontAttr.Bold = True
                Next

                paoRep.z_Objects.SetObject("vLine")
                Dim baseX As Single = paoRep.z_Objects.z_Line.X
                For j As Integer = 0 To maxVLine
                    paoRep.Write("vLine", j + 1)
                    paoRep.z_Objects.SetObject("vLine", j + 1)

                    For jj As Integer = 1 To j
                        If j < maxVLine Then
                            paoRep.z_Objects.z_Line.IntervalX += arr_w(j - jj)
                        End If
                    Next

                    If j = 0 OrElse j = maxVLine Then
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F
                    End If
                Next

                paoRep.Write("field1", "品番", 1)
                paoRep.Write("field2", "品名", 1)
                paoRep.Write("field3", "数量", 1)
                paoRep.Write("field4", "単価", 1)
                paoRep.Write("field5", "金額", 1)

                Dim dv As New DataView(ds.Tables("請求明細"))
                dv.RowFilter = "請求番号 = '" & hdr("請求番号").ToString() & "'"
                Dim totalAmount As Long = 0

                For ii As Integer = 0 To dv.Count - 1
                    paoRep.Write("field1", dv(ii)("品番").ToString(), ii + 2)
                    paoRep.Write("field2", dv(ii)("品名").ToString(), ii + 2)
                    paoRep.Write("field3", dv(ii)("数量").ToString(), ii + 2)
                    paoRep.Write("field4", String.Format("{0:N0}", dv(ii)("単価")), ii + 2)
                    Dim amount As Long = Convert.ToInt64(dv(ii)("数量")) * Convert.ToInt64(dv(ii)("単価"))
                    paoRep.Write("field5", String.Format("{0:N0}", amount), ii + 2)
                    totalAmount += amount
                Next

                Dim tax As Double = 0.05
                paoRep.Write("field4", "小計", maxHLine - 2)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount), maxHLine - 2)
                paoRep.Write("field4", "消費税", maxHLine - 1)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount * tax), maxHLine - 1)
                paoRep.Write("field4", "合計", maxHLine)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount + totalAmount * tax), maxHLine)

                paoRep.Write("txtTotal", String.Format("{0:N0}", totalAmount + totalAmount * tax))

                paoRep.z_Objects.SetObject("hLine", maxHLine - 2)
                paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double

                paoRep.Write("hLine", maxHLine + 1)
                paoRep.z_Objects.SetObject("hLine", maxHLine + 1)
                paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F

                paoRep.PageEnd()
            Next
        End Sub

        ' --- 商品一覧用構造体 ---
        Class PrintData
            Public s大分類コード As String = ""
            Public s小分類コード As String = ""
            Public s大分類名称 As String = ""
            Public s小分類名称 As String = ""
            Public s品番 As String = ""
            Public s品名 As String = ""
        End Class

        ' --- 商品一覧 ---
        Sub makeReports商品一覧(paoRep As IReport)

            '外部ファイル方式で帳票デザインファイル取得＆App_Dataフォルダから読み込み
            Dim path As String = System.IO.Path.Combine(Server.MapPath("~"), "App_Data/itemlist.prepd")
            paoRep.LoadDefFile(path)

            Dim sql As String = ""
            sql &= " SELECT C.*, A.大分類名称, B.小分類名称 "
            sql &= " FROM M_大分類 AS A, M_小分類 AS B, M_商品 AS C "
            sql &= " WHERE A.大分類コード = B.大分類コード "
            sql &= " AND A.大分類コード = C.大分類コード "
            sql &= " AND B.大分類コード = C.大分類コード "
            sql &= " AND B.小分類コード = C.小分類コード "
            sql &= " ORDER BY C.大分類コード, C.小分類コード"

            Dim sqlda As New SqlDataAdapter(sql, sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds)
            Dim dt As DataTable = ds.Tables(0)

            Dim sv大分類名称 As String = Nothing
            Dim sv小分類名称 As String = Nothing
            Dim cnt大分類 As Integer = 0
            Dim cnt小分類 As Integer = 0
            Dim pds As New List(Of PrintData)()

            For Each dr As DataRow In dt.Rows
                Dim pd As New PrintData()

                If sv小分類名称 IsNot Nothing AndAlso sv小分類名称 <> dr("小分類名称").ToString() Then
                    pd.s小分類コード = " "
                    pd.s小分類名称 = "小分類(" & sv小分類名称 & ")小計"
                    pd.s品番 = cnt小分類.ToString() & " 冊"
                    cnt小分類 = 0
                    pds.Add(pd)
                    pd = New PrintData()
                End If

                If sv大分類名称 IsNot Nothing AndAlso sv大分類名称 <> dr("大分類名称").ToString() Then
                    pd.s大分類コード = " "
                    pd.s小分類名称 = "大分類(" & sv大分類名称 & ")小計"
                    pd.s品番 = cnt大分類.ToString() & " 冊"
                    cnt大分類 = 0
                    pds.Add(pd)
                    pd = New PrintData()
                End If

                If sv大分類名称 <> dr("大分類名称").ToString() Then
                    pd.s大分類名称 = dr("大分類名称").ToString()
                End If
                If sv小分類名称 <> dr("小分類名称").ToString() Then
                    pd.s小分類名称 = dr("小分類名称").ToString()
                End If
                pd.s品番 = dr("品番").ToString()
                pd.s品名 = dr("品名").ToString()

                pds.Add(pd)

                sv大分類名称 = dr("大分類名称").ToString()
                sv小分類名称 = dr("小分類名称").ToString()

                cnt大分類 += 1
                cnt小分類 += 1
            Next

            Dim p As New PrintData()
            p.s小分類コード = " "
            p.s小分類名称 = "小分類(" & sv小分類名称 & ")小計"
            p.s品番 = cnt小分類.ToString() & " 冊"
            pds.Add(p)
            p = New PrintData()
            p.s大分類コード = " "
            p.s小分類名称 = "大分類(" & sv大分類名称 & ")小計"
            p.s品番 = cnt大分類.ToString() & " 冊"
            pds.Add(p)

            paoRep.PageStart()
            Const RecnumInPage As Integer = 20
            paoRep.z_Objects.SetObject("枠_大分類")

            Dim filedNames_枠() As String = {"枠_大分類", "枠_小分類", "枠_品番", "枠_品名"}
            Dim filedNames() As String = {"大分類", "小分類", "品番", "品名"}

            For recno As Integer = 0 To pds.Count - 1
                If recno Mod RecnumInPage = 0 AndAlso recno <> 0 Then
                    paoRep.PageEnd()
                    paoRep.PageStart()
                End If

                Dim lineno As Integer = (recno Mod RecnumInPage) + 1
                paoRep.Write("大分類", pds(recno).s大分類名称, lineno)
                paoRep.Write("小分類", pds(recno).s小分類名称, lineno)
                paoRep.Write("品番", pds(recno).s品番, lineno)
                paoRep.Write("品名", pds(recno).s品名, lineno)

                For Each name In filedNames_枠
                    paoRep.Write(name, lineno)
                Next

                If pds(recno).s小分類コード = " " Then
                    For Each name In filedNames_枠
                        paoRep.z_Objects.SetObject(name, lineno)
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightYellow
                    Next
                ElseIf pds(recno).s大分類コード = " " Then
                    For Each name In filedNames_枠
                        paoRep.z_Objects.SetObject(name, lineno)
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightPink
                    Next
                End If
            Next

            paoRep.PageEnd()
        End Sub

        Function About() As ActionResult
            ViewBag.Message = "Your application description page."
            Return View()
        End Function

        Function Contact() As ActionResult
            ViewBag.Message = "Your contact page."
            Return View()
        End Function

    End Class
End Namespace
