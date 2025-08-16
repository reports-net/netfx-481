Imports Pao.Reports
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Mvc

Namespace WebApi.Controllers

    Public Class HomeController
        Inherits Controller

        ' 既存のMVCアクション
        Public Function Index() As ActionResult
            ViewBag.Title = "Home Page"
            Return View()
        End Function

        ' データベース接続文字列
        ReadOnly sqlcon As String = System.Configuration.ConfigurationManager.ConnectionStrings("ApiCon").ConnectionString

        Public Class PrintData
            Public Property Id As Integer
            Public Property Data As String
        End Class

        ' Web APIアクション
        <HttpGet>
        Public Function GetReports(Optional Id As Integer = 0) As JsonResult

            'インスタンスの生成
            Dim paoRep As IReport = ReportCreator.GetReport()

            Dim pd As New PrintData()

            Select Case Id
                Case 1
                    pd.Data = makeReports10の倍数(paoRep)
                Case 2
                    pd.Data = makeReports郵便番号(paoRep)
                Case 3
                    pd.Data = makeReports見積書(paoRep)
                Case 4
                    pd.Data = makeReports請求書(paoRep)
                Case 5
                    pd.Data = makeReports商品一覧(paoRep)
                Case 6
                    pd.Data = makeReports広告(paoRep)
                Case Else

                    '帳票デザインファイルの読み込み（Azure環境で動作しない）
                    'Dim path As String = Server.MapPath("~/App_Data/simple.prepd")
                    'paoRep.LoadDefFile(path)

                    ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
                    Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".simple.prepd"
                    Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
                    paoRep.LoadDefFile(stream)

                    '帳票編集
                    paoRep.PageStart()
                    Dim text As String = "ローカル開発環境のIIS" & vbCrLf & "で作った印刷データですよ～ん♪"
                    'Dim text As String = "Azure Winows Server" & vbCrLf & "で作った印刷データですよ～ん♪"
                    text += vbCrLf & "REST API で変更！！ (GET)"
                    paoRep.Write("Text2", text)
                    paoRep.PageEnd()
                    pd.Data = Convert.ToBase64String(paoRep.SaveData())

            End Select

            Return Json(pd, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        <AllowAnonymous>
        Public Function PostReports(pd As PrintData) As JsonResult
            ' デバッグ出力
            System.Diagnostics.Debug.WriteLine("POST received: " & If(pd?.Id, -999).ToString())

            'インスタンスの生成
            Dim paoRep As IReport = ReportCreator.GetReport()

            Select Case pd.Id
                Case 1
                    pd.Data = makeReports10の倍数(paoRep)
                Case 2
                    pd.Data = makeReports郵便番号(paoRep)
                Case 3
                    pd.Data = makeReports見積書(paoRep)
                Case 4
                    pd.Data = makeReports請求書(paoRep)
                Case 5
                    pd.Data = makeReports商品一覧(paoRep)
                Case 6
                    pd.Data = makeReports広告(paoRep)
                Case Else

                    '帳票デザインファイルの読み込み（Azure環境で動作しない）
                    'Dim path As String = Server.MapPath("~/App_Data/simple.prepd")
                    'paoRep.LoadDefFile(path)

                    ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
                    Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".simple.prepd"
                    Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
                    paoRep.LoadDefFile(stream)

                    '帳票編集
                    paoRep.PageStart()
                    Dim text As String = "ローカル開発環境のIIS" & vbCrLf & "で作った印刷データですよ～ん♪"
                    'Dim text As String = "Azure Winows Server" & vbCrLf & "で作った印刷データですよ～ん♪"
                    text += vbCrLf & "REST API で変更！！(POST)"
                    paoRep.Write("Text2", text)
                    paoRep.PageEnd()
                    pd.Data = Convert.ToBase64String(paoRep.SaveData())

            End Select

            Return Json(pd)
        End Function

        Protected Function makeReports10の倍数(paoRep As IReport) As String

            ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".sample10.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
            paoRep.LoadDefFile(stream)

            Dim page As Integer = 0 '頁数を定義
            Dim line As Integer = 0 '行数を定義

            For i As Integer = 0 To 59
                If i Mod 15 = 0 Then '1頁15行で開始
                    '頁開始を宣言
                    paoRep.PageStart()
                    page += 1 '頁数をインクリメント
                    line = 0 '行数を初期化

                    '＊＊＊ヘッダのセット＊＊＊
                    '文字列のセット
                    paoRep.Write("日付", System.DateTime.Now.ToString())
                    paoRep.Write("頁数", "Page - " & page.ToString())

                    'オブジェクトの属性変更
                    paoRep.z_Objects.SetObject("フォントサイズ")
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12
                    paoRep.Write("フォントサイズ", "フォントサイズ" & Environment.NewLine & " 変更後")

                    If page = 2 Then
                        paoRep.Write("Line3", "") '２頁目の線をを消す
                    End If

                End If
                line += 1 '行数をインクリメント

                '＊＊＊明細のセット＊＊＊
                '繰返し文字列のセット
                paoRep.Write("行番号", (i + 1).ToString(), line)
                paoRep.Write("10倍数", ((i + 1) * 10).ToString(), line)
                '繰返し図形(横線)のセット
                paoRep.Write("横線", line)

                If ((i + 1) Mod 15) = 0 Then
                    paoRep.PageEnd() '1頁15行で終了宣言
                End If
            Next

            Return Convert.ToBase64String(paoRep.SaveData())
        End Function

        Protected Function makeReports見積書(paoRep As IReport) As String
            ' デザインファイル（Azure環境で動作しない）
            'Dim path As String = ""

            ' ヘッダデータ読込
            Dim sqlda As New SqlDataAdapter("SELECT * FROM 見積ヘッダ ORDER BY 見積番号", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds, "見積ヘッダ")

            sqlda = New SqlDataAdapter("SELECT * FROM 見積明細 ORDER BY 見積番号,行番号", sqlcon)
            sqlda.Fill(ds, "見積明細")

            Dim ht As DataTable = ds.Tables(0)
            For Each hdr As DataRow In ht.Rows

                '' 表紙デザインファイルの読み込み ✅ 埋め込みリソース方式:NG リソースのキャッシュがクリアされない
                'Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".mitsumori.head.prepd"
                'Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
                'paoRep.LoadDefFile(stream)

                ' 表紙デザインファイルの読み込み ✅ 物理パス方式【お勧め】
                Dim repPath As String
                repPath = Path.Combine(Server.MapPath("~"), "App_Data/mitsumori.head.prepd")
                paoRep.LoadDefFile(repPath)

                paoRep.PageStart()
                paoRep.Write("お客様名", CStr(hdr("お客様名")))
                paoRep.Write("担当者名", CStr(hdr("担当者名")))
                paoRep.PageEnd()

                '' 見積書デザインファイルの読み込み ✅ 埋め込みリソース方式:NG リソースのキャッシュがクリアされない
                'resourcePath = Assembly.GetExecutingAssembly().GetName().Name & ".mitsumori.prepd"
                'stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
                'paoRep.LoadDefFile(stream)

                ' 見積書デザインファイルの読み込み ✅ 物理パス方式【お勧め】
                repPath = Path.Combine(Server.MapPath("~"), "App_Data/mitsumori.prepd")
                paoRep.LoadDefFile(repPath)

                paoRep.PageStart()

                paoRep.Write("見積番号", CStr(hdr("見積番号")))
                paoRep.Write("お客様名", CStr(hdr("お客様名")))
                paoRep.Write("担当者名", CStr(hdr("担当者名")))
                paoRep.Write("見積日", CDate(hdr("見積日")).ToString("yyyy年M月d日"))
                paoRep.Write("ヘッダ合計", "\ " & String.Format("{0:N0}", hdr("合計金額")))
                paoRep.Write("消費税額", String.Format("{0:N0}", hdr("消費税額")))
                paoRep.Write("フッタ合計", String.Format("{0:N0}", hdr("合計金額")))

                '明細の背景作成
                For i As Integer = 0 To 6
                    paoRep.Write("品番白", i + 1)
                    paoRep.Write("品番白", i + 1)
                    paoRep.Write("数量白", i + 1)
                    paoRep.Write("単価白", i + 1)
                    paoRep.Write("金額白", i + 1)
                    paoRep.Write("品番青", i + 1)
                    paoRep.Write("品名青", i + 1)
                    paoRep.Write("数量青", i + 1)
                    paoRep.Write("単価青", i + 1)
                    paoRep.Write("金額青", i + 1)
                Next

                '明細の作成
                Dim dv As New DataView(ds.Tables("見積明細"))
                dv.RowFilter = "見積番号 = '" & CStr(hdr("見積番号")) & "'"
                For i As Integer = 0 To dv.Count - 1
                    paoRep.Write("品番", CStr(dv(i)("品番")), i + 1)
                    paoRep.Write("品名", CStr(dv(i)("品名")), i + 1)
                    paoRep.Write("数量", dv(i)("数量").ToString(), i + 1)
                    paoRep.Write("単価", String.Format("{0:N0}", dv(i)("単価")), i + 1)
                    paoRep.Write("金額", String.Format("{0:N0}", dv(i)("金額")), i + 1)
                Next
                paoRep.PageEnd()
            Next

            Return Convert.ToBase64String(paoRep.SaveData())
        End Function

        Protected Function makeReports郵便番号(paoRep As IReport) As String

            ' デザインファイル（Azure環境で動作しない）

            Dim resourcePath1 As String = Assembly.GetExecutingAssembly().GetName().Name & ".zipcode1.prepd"
            Dim stream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath1)

            Dim resourcePath2 As String = Assembly.GetExecutingAssembly().GetName().Name & ".zipcode2.prepd"
            Dim stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath2)

            ' ヘッダデータ読込
            Dim sqlda As New SqlDataAdapter("select * from 郵便番号テーブル", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds, "PostTable")
            Dim table As DataTable = ds.Tables(0)

            Dim page As Integer = 0
            Dim line As Integer = 999
            Dim hDate As String = System.DateTime.Now.ToString()

            paoRep.LoadDefFile(stream1)
            For Each row As DataRow In table.Rows
                line += 1
                If line > 32 Then ' Head Print
                    If page <> 0 Then
                        paoRep.PageEnd()
                    End If

                    page += 1

                    If page = 6 Then
                        paoRep.LoadDefFile(stream2)
                    End If

                    paoRep.PageStart()

                    paoRep.Write("日時", hDate)
                    paoRep.Write("ページ", "Page-" & page.ToString())

                    'QRコード描画
                    If page < 6 Then
                        'paoRep.Write("QR", row("郵便番号").ToString() & " " & row("市区町村").ToString() & row("住所").ToString())
                    End If

                    line = 1

                End If

                'Body Print
                paoRep.Write("郵便番号", row("郵便番号").ToString(), line)
                paoRep.Write("市区町村", row("市区町村").ToString(), line)
                paoRep.Write("住所", row("住所").ToString(), line)
                paoRep.Write("横罫線", line)

                If page > 5 AndAlso line Mod 2 = 0 Then
                    paoRep.Write("網掛け", line \ 2)
                End If

            Next
            paoRep.PageEnd()

            Return Convert.ToBase64String(paoRep.SaveData())
        End Function

        Protected Function makeReports広告(paoRep As IReport) As String
            ' デザインファイル（Azure環境で動作しない）
            'Dim path As String = Server.MapPath("~/App_Data/koukoku.prepd")
            'paoRep.LoadDefFile(path)

            ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".koukoku.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
            paoRep.LoadDefFile(stream)

            ' 画像ファイルパス ❌ ファイル方式（Azure環境で動作しない）
            ' Dim gpath As String = Server.MapPath("~/App_Data/img/")

            ' データ読込
            Dim sqlda As New SqlDataAdapter("select * from 広告情報", sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds)
            Dim table As DataTable = ds.Tables(0)

            For Each row As DataRow In table.Rows

                paoRep.PageStart()

                paoRep.Write("製品名", CStr(row("製品名")))
                paoRep.Write("キャッチフレーズ", CStr(row("キャッチフレーズ")))
                paoRep.Write("商品コード", CStr(row("商品コード")))
                paoRep.Write("JANコード", CStr(row("商品コード")))
                paoRep.Write("売り文句", CStr(row("売り文句")))
                paoRep.Write("説明", CStr(row("説明")))
                paoRep.Write("価格", CStr(row("価格")))

                ' 画像ファイルパス(gpath + DB内の画像ファイル名) ❌ ファイル方式（Azure環境で動作しない）
                'paoRep.Write("画像1", gpath & CStr(row("画像1")))
                'paoRep.Write("画像2", gpath & CStr(row("画像2")))

                ' 🖼️ 画像処理：埋め込みリソースから読み込み
                Dim image1Path As String = GetImageFromEmbeddedResource(CStr(row("画像1")))
                Dim image2Path As String = GetImageFromEmbeddedResource(CStr(row("画像2")))
                paoRep.Write("画像1", image1Path)
                paoRep.Write("画像2", image2Path)

                paoRep.Write("QR", CStr(row("製品名")) & " " & CStr(row("キャッチフレーズ")))

                paoRep.PageEnd()

            Next

            Return Convert.ToBase64String(paoRep.SaveData())
        End Function

        ' 🖼️ 埋め込みリソースから画像を取得するヘルパーメソッド
        Private Function GetImageFromEmbeddedResource(imageName As String) As String

            Dim resourceNames() As String = Assembly.GetExecutingAssembly().GetManifestResourceNames()
            For Each name As String In resourceNames
                If name.Contains("kakuin") Then
                    System.Diagnostics.Debug.WriteLine("Found kakuin resource: " & name)
                End If
                If name.Contains("image") Then
                    System.Diagnostics.Debug.WriteLine("Found image resource: " & name)
                End If
            Next

            Try
                Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & "." & imageName
                Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)

                If stream Is Nothing Then Return "" ' 画像が見つからない場合

                ' 一時ファイルに書き出し（Pao.Reportsがファイルパスを期待する場合）
                Dim tempPath As String = Path.GetTempPath()
                Dim tempFile As String = Path.Combine(tempPath, imageName)

                Using fileStream = System.IO.File.Create(tempFile)
                    stream.CopyTo(fileStream)
                End Using

                Return tempFile
            Catch
                Return "" ' エラー時は空文字
            End Try
        End Function

        Protected Function makeReports請求書(paoRep As IReport) As String
            ' デザインファイル（Azure環境で動作しない）
            'Dim path As String = Server.MapPath("~/App_Data/invoice.prepd")
            'paoRep.LoadDefFile(path)

            ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".invoice.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
            paoRep.LoadDefFile(stream)

            ' データ読込
            Dim ds As New DataSet()

            Dim sqlda As New SqlDataAdapter("select * from 請求ヘッダ ORDER BY 請求番号", sqlcon)
            sqlda.Fill(ds, "請求ヘッダ")
            sqlda = New SqlDataAdapter("select * from 請求明細 ORDER BY 請求番号, 行番号", sqlcon)
            sqlda.Fill(ds, "請求明細")

            ' 各列幅調整の配列
            Dim arr_w() As Single = {-5, 44, -20, -10, -9}

            Dim ht As DataTable = ds.Tables(0)
            For Each hdr As DataRow In ht.Rows

                paoRep.PageStart()

                paoRep.Write("txtNo", CStr(hdr("請求番号")))
                paoRep.Write("txtCustomer", CStr(hdr("お客様名")))
                paoRep.Write("txtDate", DateTime.Now.ToString("yyyy年M月d日"))
                paoRep.Write("Image1", GetImageFromEmbeddedResource("kakuin.png"))

                ' デザイン時の行数・列数取得
                paoRep.z_Objects.SetObject("hLine")
                Dim maxHLine As Integer = paoRep.z_Objects.z_Line.Repeat - 1
                paoRep.z_Objects.SetObject("vLine")
                Dim maxVLine As Integer = paoRep.z_Objects.z_Line.Repeat - 1

                '空の表を作成
                For i As Integer = 0 To maxHLine - 1
                    ' 「横罫線」描画
                    paoRep.Write("hLine", i + 1)

                    ' 外枠の上を太く
                    If i = 0 Then
                        paoRep.z_Objects.SetObject("hLine", i + 1)
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F
                    End If

                    ' 行ヘッダの下を二重線
                    If i = 1 Then
                        paoRep.z_Objects.SetObject("hLine", i + 1)
                        paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double
                    End If

                    ' 「行の背景」描画
                    paoRep.Write("LineRect", i + 1)
                    paoRep.z_Objects.SetObject("LineRect", i + 1)

                    If i = 0 Then
                        ' 行ヘッダはデザイン通り
                    ElseIf i < maxHLine - 3 Then
                        ' 明細行
                        ' 白・青の順番で背景色をセット
                        If i Mod 2 = 1 Then
                            paoRep.z_Objects.z_Square.PaintColor = Color.White
                        Else
                            paoRep.z_Objects.z_Square.PaintColor = Color.LightSkyBlue
                        End If
                    Else
                        ' 集計行
                        paoRep.z_Objects.z_Square.PaintColor = Color.FromArgb(255, 255, 180)
                    End If

                    ' 次回のXの位置
                    Dim svX As Single = -1

                    For j As Integer = 0 To maxVLine - 1

                        ' 文字列項目の属性(幅/Font/Align/)変更
                        paoRep.z_Objects.SetObject("field" & (j + 1).ToString(), i + 1)

                        ' 幅(TextBox)
                        paoRep.z_Objects.z_Text.Width = paoRep.z_Objects.z_Text.Width + arr_w(j)

                        ' 位置(TextBox)
                        If j > 0 Then
                            paoRep.z_Objects.z_Text.X = svX
                        End If
                        svX = paoRep.z_Objects.z_Text.X + paoRep.z_Objects.z_Text.Width

                        ' 行ヘッダの場合
                        If i = 0 Then
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = True
                        Else
                            ' 明細行の場合
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = False
                            paoRep.z_Objects.z_Text.z_FontAttr.Size = 12

                            ' 文字位置(Text Align)
                            Select Case j + 1
                                Case 1
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Center
                                Case 2
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Left
                                Case 3, 4, 5
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Right
                            End Select

                        End If
                    Next
                    '集計行の文字設定
                    For j As Integer = maxHLine To maxHLine - 2 Step -1
                        paoRep.z_Objects.SetObject("field4", j)
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 16
                        paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Center
                        paoRep.z_Objects.z_Text.z_FontAttr.Bold = True
                    Next

                Next

                ' 縦罫線描画
                paoRep.z_Objects.SetObject("vLine")
                Dim baseX As Single = paoRep.z_Objects.z_Line.X
                For j As Integer = 0 To maxVLine
                    paoRep.Write("vLine", j + 1)

                    paoRep.z_Objects.SetObject("vLine", j + 1)

                    ' 幅調整
                    For jj As Integer = 1 To j
                        If j < maxVLine Then
                            Dim baseIntervalX As Single = paoRep.z_Objects.z_Line.IntervalX
                            paoRep.z_Objects.z_Line.IntervalX = baseIntervalX + arr_w(j - jj)
                        End If
                    Next

                    ' 外枠を太線にする
                    If j = 0 OrElse j = maxVLine Then
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F
                    End If

                Next

                ' 見出し文字入れ
                paoRep.Write("field1", "品番", 1)
                paoRep.Write("field2", "品名", 1)
                paoRep.Write("field3", "数量", 1)
                paoRep.Write("field4", "単価", 1)
                paoRep.Write("field5", "金額", 1)

                '明細の作成
                Dim dv As New DataView(ds.Tables("請求明細"))
                dv.RowFilter = "請求番号 = '" & CStr(hdr("請求番号")) & "'"
                Dim totalAmount As Long = 0
                Dim ii As Integer = 0
                For ii = 0 To dv.Count - 1
                    paoRep.Write("field1", CStr(dv(ii)("品番")), ii + 2)
                    paoRep.Write("field2", CStr(dv(ii)("品名")), ii + 2)
                    paoRep.Write("field3", dv(ii)("数量").ToString(), ii + 2)
                    paoRep.Write("field4", String.Format("{0:N0}", dv(ii)("単価")), ii + 2)
                    Dim amount As Long = Convert.ToInt64(dv(ii)("数量")) * Convert.ToInt64(dv(ii)("単価"))
                    paoRep.Write("field5", String.Format("{0:N0}", amount), ii + 2)
                    totalAmount += amount
                Next

                Dim tax As Double = 0.05

                paoRep.Write("field4", "小計", maxHLine - 2)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount), maxHLine - 2)
                ii += 1
                paoRep.Write("field4", "消費税", maxHLine - 1)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount * tax), maxHLine - 1)
                ii += 1
                paoRep.Write("field4", "合計", maxHLine)
                paoRep.Write("field5", String.Format("{0:N0}", totalAmount + (totalAmount * tax)), maxHLine)

                paoRep.Write("txtTotal", String.Format("{0:N0}", totalAmount + (totalAmount * tax)))

                ' 小計の上を二重線
                paoRep.z_Objects.SetObject("hLine", maxHLine - 2)
                paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double

                ' 最終行を太く
                paoRep.Write("hLine", maxHLine + 1)
                paoRep.z_Objects.SetObject("hLine", maxHLine + 1)
                paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F

                paoRep.PageEnd()

            Next

            Return Convert.ToBase64String(paoRep.SaveData())

        End Function

        ''' <summary>
        ''' 商品マスタ用構造体
        ''' </summary>
        Protected Class PrintData商品
            Friend s大分類コード As String = ""
            Friend s小分類コード As String = ""
            Friend s大分類名称 As String = ""
            Friend s小分類名称 As String = ""
            Friend s品番 As String = ""
            Friend s品名 As String = ""
        End Class

        Protected Function makeReports商品一覧(paoRep As IReport) As String
            ' デザインファイル（Azure環境で動作しない）
            'Dim path As String = Server.MapPath("~/App_Data/itemlist.prepd")
            'paoRep.LoadDefFile(path)

            ' 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            Dim resourcePath As String = Assembly.GetExecutingAssembly().GetName().Name & ".itemlist.prepd"
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath)
            paoRep.LoadDefFile(stream)

            ' データ読込
            Dim sql As String = ""
            sql += " SELECT C.*, A.大分類名称, B.小分類名称 "
            sql += " FROM "
            sql += "   M_大分類 AS A"
            sql += " , M_小分類 AS B"
            sql += " , M_商品 AS C"
            sql += " WHERE"
            sql += " A.大分類コード = B.大分類コード"
            sql += " AND"
            sql += " A.大分類コード = C.大分類コード"
            sql += " AND"
            sql += " B.大分類コード = C.大分類コード"
            sql += " AND"
            sql += " B.小分類コード = C.小分類コード"
            sql += " ORDER BY C.大分類コード, C.小分類コード"

            Dim sqlda As New SqlDataAdapter(sql, sqlcon)
            Dim ds As New DataSet()
            sqlda.Fill(ds)
            Dim dt As DataTable = ds.Tables(0)

            ' いったん構造体の配列にセット

            Dim sv大分類名称 As String = Nothing
            Dim sv小分類名称 As String = Nothing

            Dim cnt大分類 As Integer = 0
            Dim cnt小分類 As Integer = 0
            Dim pds As New List(Of PrintData商品)()
            Dim pd As PrintData商品
            For Each dr As DataRow In dt.Rows
                pd = New PrintData商品()

                ' キーブレイク処理は、今回は構造体にセットするところでやってみました。
                ' プログラム構造的にもっと汎用的な方法はあります。
                If sv小分類名称 IsNot Nothing AndAlso sv小分類名称 <> dr("小分類名称").ToString() Then
                    pd.s小分類コード = " "
                    pd.s小分類名称 = "小分類(" & sv小分類名称 & ")小計"
                    pd.s品番 = cnt小分類.ToString() & " 冊"
                    cnt小分類 = 0
                    pds.Add(pd)
                    pd = New PrintData商品()
                End If
                If sv大分類名称 IsNot Nothing AndAlso sv大分類名称 <> dr("大分類名称").ToString() Then
                    pd.s大分類コード = " "
                    pd.s小分類名称 = "大分類(" & sv大分類名称 & ")小計"
                    pd.s品番 = cnt大分類.ToString() & " 冊"
                    cnt大分類 = 0
                    pds.Add(pd)
                    pd = New PrintData商品()
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

            ' 商品構造体にセット
            pd = New PrintData商品()
            pd.s小分類コード = " "
            pd.s小分類名称 = "小分類(" & sv小分類名称 & ")小計"
            pd.s品番 = cnt小分類.ToString() & " 冊"
            pds.Add(pd)
            pd = New PrintData商品()
            pd.s大分類コード = " "
            pd.s小分類名称 = "大分類(" & sv大分類名称 & ")小計"
            pd.s品番 = cnt大分類.ToString() & " 冊"
            pds.Add(pd)

            '帳票データセット・出力
            paoRep.PageStart()

            Const RecnumInPage As Integer = 20

            paoRep.z_Objects.SetObject("枠_大分類")

            Dim filedNames_枠() As String = {"枠_大分類", "枠_小分類", "枠_品番", "枠_品名"}
            Dim filedNames() As String = {"大分類", "小分類", "品番", "品名"}

            For recno As Integer = 0 To pds.Count - 1

                If recno Mod RecnumInPage = 0 Then
                    If recno <> 0 Then
                        paoRep.PageEnd()
                        paoRep.PageStart()
                    End If
                End If

                ' 値セット
                Dim lineno As Integer = (recno Mod RecnumInPage) + 1
                paoRep.Write("大分類", pds(recno).s大分類名称, lineno)
                paoRep.Write("小分類", pds(recno).s小分類名称, lineno)
                paoRep.Write("品番", pds(recno).s品番, lineno)
                paoRep.Write("品名", pds(recno).s品名, lineno)

                ' 枠描画
                For j As Integer = 0 To filedNames_枠.Length - 1
                    paoRep.Write(filedNames_枠(j), lineno)
                Next

                ' 小分類小計行の色替え
                If pds(recno).s小分類コード = " " Then
                    ' 枠描画
                    For j As Integer = 0 To filedNames_枠.Length - 1
                        paoRep.z_Objects.SetObject(filedNames_枠(j), lineno)
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightYellow
                    Next

                ElseIf pds(recno).s大分類コード = " " Then
                    ' 大分類小計行の色替え
                    ' 枠描画
                    For j As Integer = 0 To filedNames_枠.Length - 1
                        paoRep.z_Objects.SetObject(filedNames_枠(j), lineno)
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightPink
                    Next

                End If

            Next

            paoRep.PageEnd()

            Return Convert.ToBase64String(paoRep.SaveData())
        End Function
    End Class
End Namespace
