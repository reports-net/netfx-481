Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.ServiceModel.Web
Imports System.Text
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports Pao.Reports
Imports System.Drawing

' メモ: コンテキスト メニューの [名前の変更] コマンドを使用すると、コード、svc、および config ファイルで同時にクラス名 "Service1" を変更できます。
' 注意: このサービスをテストするために WCF テスト クライアントを起動するには、ソリューション エクスプローラーで Service1.svc または Service1.svc.vb を選択し、デバッグを開始してください。
Public Class Service1
    Implements IService1

    Public Sub New()
    End Sub

    Public Function GetData(ByVal value As Integer) As String Implements IService1.GetData
        Return String.Format("You entered: {0}", value)
    End Function

    Public Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType Implements IService1.GetDataUsingDataContract
        If composite Is Nothing Then
            Throw New ArgumentNullException("composite")
        End If
        If composite.BoolValue Then
            composite.StringValue &= "Suffix"
        End If
        Return composite
    End Function


#Region "各帳票作成"

    Private sqlcon As New SqlConnection("Server=tcp:fzxu46e9ck.database.windows.net,1433;Initial Catalog=Reports.net.Sample;Persist Security Info=False;User ID=login_user;Password=$Abc123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")

    Public Function getReports単純なサンプル() As Byte() Implements IService1.getReports単純なサンプル

        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()


        '帳票定義体の読み込み
        Dim path As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\simple.prepd")
        paoRep.LoadDefFile(path)

        '帳票編集
        paoRep.PageStart()
        paoRep.Write("Barcode1", "123456789012")
        paoRep.Write("Barcode2", "123456789012")
        paoRep.Write("Barcode3", "123456789012")
        paoRep.Write("Barcode4", "123456789012")
        paoRep.Write("Barcode5", "123456789012")
        paoRep.Write("Barcode6", "123456789012")
        paoRep.Write("Barcode7", "123456789012")
        paoRep.Write("Text1", "文字列")
        paoRep.Write("Text2", "これはローカルの ASP.NET VB.NETで作った" & vbLf & "印刷データですよ～ん♪")
        paoRep.PageEnd()

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    Public Function getReports10の倍数() As Byte() Implements IService1.getReports10の倍数
        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()


        '帳票定義体の読み込み
        Dim path As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\sample10.prepd")
        paoRep.LoadDefFile(path)

        Dim page As Integer = 0
        '頁数を定義
        Dim line As Integer = 0
        '行数を定義
        For i As Integer = 0 To 59
            If i Mod 15 = 0 Then
                '1頁15行で開始
                '頁開始を宣言
                paoRep.PageStart()
                page += 1
                '頁数をインクリメント
                line = 0
                '行数を初期化
                '＊＊＊ヘッダのセット＊＊＊
                '文字列のセット
                paoRep.Write("日付", System.DateTime.Now.ToString())
                paoRep.Write("頁数", "Page - " & page.ToString())

                'オブジェクトの属性変更
                paoRep.z_Objects.SetObject("フォントサイズ")
                paoRep.z_Objects.z_Text.z_FontAttr.Size = 12
                paoRep.Write("フォントサイズ", "フォントサイズ" & Environment.NewLine & " 変更後")

                If page = 2 Then
                    paoRep.Write("Line3", "")
                    '２頁目の線をを消す
                End If
            End If
            line += 1
            '行数をインクリメント
            '＊＊＊明細のセット＊＊＊
            '繰返し文字列のセット
            paoRep.Write("行番号", (i + 1).ToString(), line)
            paoRep.Write("10倍数", ((i + 1) * 10).ToString(), line)
            '繰返し図形(横線)のセット
            paoRep.Write("横線", line)

            If ((i + 1) Mod 15) = 0 Then
                paoRep.PageEnd()
                '1頁15行で終了宣言
            End If
        Next

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    Public Function getReports見積書() As Byte() Implements IService1.getReports見積書
        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()

        ' デザインファイル
        Dim path As String = ""

        ' ヘッダデータ読込
        Dim sqlda As New SqlDataAdapter("SELECT * FROM 見積ヘッダ ORDER BY 見積番号", sqlcon)
        Dim ds As New DataSet()
        sqlda.Fill(ds, "見積ヘッダ")

        sqlda = New SqlDataAdapter("SELECT * FROM 見積明細 ORDER BY 見積番号,行番号", sqlcon)
        sqlda.Fill(ds, "見積明細")


        Dim ht As DataTable = ds.Tables("見積ヘッダ")
        For Each hdr As DataRow In ht.Rows
            '表紙の生成
            path = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\mitsumori.head.prepd")
            paoRep.LoadDefFile(path)
            paoRep.PageStart()
            paoRep.Write("お客様名", DirectCast(hdr("お客様名"), String))
            paoRep.Write("担当者名", DirectCast(hdr("担当者名"), String))
            paoRep.PageEnd()

            '見積書の生成
            path = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\mitsumori.prepd")
            paoRep.LoadDefFile(path)
            paoRep.PageStart()

            paoRep.Write("見積番号", DirectCast(hdr("見積番号"), String))
            paoRep.Write("お客様名", DirectCast(hdr("お客様名"), String))
            paoRep.Write("担当者名", DirectCast(hdr("担当者名"), String))
            paoRep.Write("見積日", CType(hdr("見積日"), DateTime).ToString("yyyy年M月d日"))
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
            dv.RowFilter = "見積番号 = '" & DirectCast(hdr("見積番号"), String) & "'"
            For i As Integer = 0 To dv.Count - 1
                paoRep.Write("品番", DirectCast(dv(i)("品番"), String), i + 1)
                paoRep.Write("品名", DirectCast(dv(i)("品名"), String), i + 1)
                paoRep.Write("数量", dv(i)("数量").ToString(), i + 1)
                paoRep.Write("単価", String.Format("{0:N0}", dv(i)("単価")), i + 1)
                paoRep.Write("金額", String.Format("{0:N0}", dv(i)("金額")), i + 1)
            Next
            paoRep.PageEnd()
        Next

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    Public Function getReports郵便番号() As Byte() Implements IService1.getReports郵便番号

        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()

        ' デザインファイル
        Dim path1 As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\zipcode1.prepd")
        Dim path2 As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\zipcode2.prepd")


        ' ヘッダデータ読込
        Dim sqlda As New SqlDataAdapter("select * from 郵便番号テーブル", sqlcon)
        Dim ds As New DataSet()
        sqlda.Fill(ds, "PostTable")
        Dim table As DataTable = ds.Tables("PostTable")

        Dim page As Integer = 0
        Dim line As Integer = 999
        Dim hDate As String = System.DateTime.Now.ToString()

        paoRep.LoadDefFile(path1)
        For Each row As DataRow In table.Rows
            line += 1
            If line > 32 Then
                ' Head Print
                If page <> 0 Then
                    paoRep.PageEnd()
                End If

                page += 1

                If page = 6 Then
                    paoRep.LoadDefFile(path2)
                End If

                paoRep.PageStart()

                paoRep.Write("日時", hDate)
                paoRep.Write("ページ", "Page-" & page.ToString())

                'QRコード描画
                If page < 6 Then
                    paoRep.Write("QR", row("郵便番号").ToString() & " " & row("市区町村").ToString() & row("住所").ToString())
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

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    Public Function getReports広告() As Byte() Implements IService1.getReports広告
        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetImagePdf()

        ' デザインファイル
        Dim path As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\koukoku.prepd")
        paoRep.LoadDefFile(path)

        ' 画像ファイルパス
        Dim gpath As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\img\")

        ' データ読込
        Dim sqlda As New SqlDataAdapter("select * from 広告情報", sqlcon)
        Dim ds As New DataSet()
        sqlda.Fill(ds, "広告情報")
        Dim table As DataTable = ds.Tables("広告情報")


        For Each row As DataRow In table.Rows

            paoRep.PageStart()

            paoRep.Write("製品名", DirectCast(row("製品名"), String))
            paoRep.Write("キャッチフレーズ", DirectCast(row("キャッチフレーズ"), String))
            paoRep.Write("商品コード", DirectCast(row("商品コード"), String))
            paoRep.Write("JANコード", DirectCast(row("商品コード"), String))
            paoRep.Write("売り文句", DirectCast(row("売り文句"), String))
            paoRep.Write("説明", DirectCast(row("説明"), String))
            paoRep.Write("価格", DirectCast(row("価格"), String))
            paoRep.Write("画像1", gpath & DirectCast(row("画像1"), String))
            paoRep.Write("画像2", gpath & DirectCast(row("画像2"), String))
            paoRep.Write("QR", DirectCast(row("製品名"), String) & " " & DirectCast(row("キャッチフレーズ"), String))


            paoRep.PageEnd()
        Next

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    Public Function getReports請求書() As Byte() Implements IService1.getReports請求書
        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()

        ' デザインファイル
        Dim path As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\invoice.prepd")
        paoRep.LoadDefFile(path)

        ' 画像ファイルパス
        Dim gpath As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\img\")

        ' データ読込
        Dim ds As New DataSet()

        Dim sqlda As New SqlDataAdapter("select * from 請求ヘッダ ORDER BY 請求番号", sqlcon)
        sqlda.Fill(ds, "請求ヘッダ")
        sqlda = New SqlDataAdapter("select * from 請求明細 ORDER BY 請求番号, 行番号", sqlcon)
        sqlda.Fill(ds, "請求明細")

        ' 各列幅調整の配列
        Dim arr_w As Single() = {-5, 44, -20, -10, -9}

        Dim ht As DataTable = ds.Tables("請求ヘッダ")
        For Each hdr As DataRow In ht.Rows

            paoRep.PageStart()

            paoRep.Write("txtNo", DirectCast(hdr("請求番号"), String))
            paoRep.Write("txtCustomer", DirectCast(hdr("お客様名"), String))
            paoRep.Write("txtDate", DateTime.Now.ToString("yyyy年M月d日"))
            paoRep.Write("Image1", gpath & "kakuin.png")

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
                    paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.[Double]
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
                        paoRep.z_Objects.z_Square.PaintColor = System.Drawing.Color.White
                    Else
                        paoRep.z_Objects.z_Square.PaintColor = System.Drawing.Color.LightSkyBlue
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
                                Exit Select
                            Case 2
                                paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Left
                                Exit Select
                            Case 3, 4, 5
                                paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Right
                                Exit Select

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

                '幅調整
                Dim jj As Integer = 1
                While jj <= j AndAlso j < maxVLine
                    Dim baseIntervalX As Single = paoRep.z_Objects.z_Line.IntervalX
                    paoRep.z_Objects.z_Line.IntervalX = baseIntervalX + arr_w(j - jj)
                    jj += 1
                End While

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
            dv.RowFilter = "請求番号 = '" & DirectCast(hdr("請求番号"), String) & "'"
            Dim totalAmount As Long = 0
            Dim ii As Integer = 0
            While ii < dv.Count
                paoRep.Write("field1", DirectCast(dv(ii)("品番"), String), ii + 2)
                paoRep.Write("field2", DirectCast(dv(ii)("品名"), String), ii + 2)
                paoRep.Write("field3", dv(ii)("数量").ToString(), ii + 2)
                paoRep.Write("field4", String.Format("{0:N0}", dv(ii)("単価")), ii + 2)
                Dim amount As Long = Convert.ToInt64(dv(ii)("数量")) * Convert.ToInt64(dv(ii)("単価"))
                paoRep.Write("field5", String.Format("{0:N0}", amount), ii + 2)

                totalAmount += amount
                ii += 1
            End While

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
            paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.[Double]

            ' 最終行を太く
            paoRep.Write("hLine", maxHLine + 1)
            paoRep.z_Objects.SetObject("hLine", maxHLine + 1)
            paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5F



            paoRep.PageEnd()
        Next

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰
    End Function

    ''' <summary>
    ''' 商品マスタ用構造体
    ''' </summary>
    Public Class PrintData
        Friend s大分類コード As String
        Friend s小分類コード As String
        Friend s大分類名称 As String
        Friend s小分類名称 As String
        Friend s品番 As String
        Friend s品名 As String
    End Class
    Public Function getReports商品一覧() As Byte() Implements IService1.getReports商品一覧
        'インスタンスの生成
        Dim paoRep As IReport = ReportCreator.GetReport()

        ' デザインファイル
        Dim path As String = System.IO.Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\itemlist.prepd")
        paoRep.LoadDefFile(path)


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
        sqlda.Fill(ds, "商品一覧")
        Dim dt As DataTable = ds.Tables("商品一覧")


        ' いったん構造体の配列にセット

        Dim sv大分類名称 As String = Nothing
        Dim sv小分類名称 As String = Nothing
        Dim cnt大分類 As Integer = 0
        Dim cnt小分類 As Integer = 0
        Dim pds As New List(Of PrintData)()
        Dim pd As PrintData
        For Each dr As DataRow In dt.Rows
            pd = New PrintData()

            ' キーブレイク処理は、今回は構造体にセットするところでやってみました。
            ' プログラム構造的にもっと汎用的な方法はあります。
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


        ' 商品構造体にセット
        pd = New PrintData()
        pd.s小分類コード = " "
        pd.s小分類名称 = "小分類(" & sv小分類名称 & ")小計"
        pd.s品番 = cnt小分類.ToString() & " 冊"
        pds.Add(pd)
        pd = New PrintData()
        pd.s大分類コード = " "
        pd.s小分類名称 = "大分類(" & sv大分類名称 & ")小計"
        pd.s品番 = cnt大分類.ToString() & " 冊"
        pds.Add(pd)


        '帳票データセット・出力
        paoRep.PageStart()

        Const RecnumInPage As Integer = 20

        paoRep.z_Objects.SetObject("枠_大分類")

        Dim filedNames_枠 As String() = {"枠_大分類", "枠_小分類", "枠_品番", "枠_品名"}
        Dim filedNames As String() = {"大分類", "小分類", "品番", "品名"}

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
                ' 大分類小計行の色替え
            ElseIf pds(recno).s大分類コード = " " Then
                ' 枠描画
                For j As Integer = 0 To filedNames_枠.Length - 1
                    paoRep.z_Objects.SetObject(filedNames_枠(j), lineno)
                    paoRep.z_Objects.z_Square.PaintColor = Color.LightPink

                Next

            End If
        Next

        paoRep.PageEnd()

        Return paoRep.SaveData()
        ' 印刷データを保存 & 復帰


    End Function

#End Region

End Class
