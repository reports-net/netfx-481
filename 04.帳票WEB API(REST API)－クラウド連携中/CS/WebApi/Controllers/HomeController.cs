using Pao.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        // 既存のMVCアクション
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        // データベース接続文字列
        readonly string sqlcon = System.Configuration.ConfigurationManager.ConnectionStrings["ApiCon"].ConnectionString;

        public class PrintData
        {
            public int Id { get; set; }

            public string Data { get; set; }
        }

        // Web APIアクション
        [HttpGet]
        public JsonResult GetReports(int Id = 0)
        {

            //インスタンスの生成
            IReport paoRep = ReportCreator.GetReport();


            PrintData pd = new PrintData();

            switch (Id)
            {
                case 1:
                    pd.Data = makeReports10の倍数(paoRep);
                    break;
                case 2:
                    pd.Data = makeReports郵便番号(paoRep);
                    break;
                case 3:
                    pd.Data = makeReports見積書(paoRep);
                    break;
                case 4:
                    pd.Data = makeReports請求書(paoRep);
                    break;
                case 5:
                    pd.Data = makeReports商品一覧(paoRep);
                    break;
                case 6:
                    pd.Data = makeReports広告(paoRep);
                    break;
                case 0:
                default:

                    ////帳票デザインファイルの読み込み（Azure環境で動作しない）
                    //string path = Server.MapPath("~/App_Data/simple.prepd");
                    //paoRep.LoadDefFile(path);

                    // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
                    string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.simple.prepd";
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
                    paoRep.LoadDefFile(stream);

                    //帳票編集
                    paoRep.PageStart();
                    string text = "ローカル開発環境のIIS\n\rで作った印刷データですよ～ん♪";
                    //string text = "Azure Winows Server\n\rで作った印刷データですよ～ん♪";
                    text += "\n\rREST API で変更！！ (GET)";
                    paoRep.Write("Text2", text);
                    paoRep.PageEnd();
                    pd.Data = Convert.ToBase64String(paoRep.SaveData());
                    break;


            }


            return Json(pd, JsonRequestBehavior.AllowGet); ;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult PostReports(PrintData pd)
        {
            // デバッグ出力
            System.Diagnostics.Debug.WriteLine("POST received: " + (pd?.Id ?? -999));

            //インスタンスの生成
            IReport paoRep = ReportCreator.GetReport();


            switch (pd.Id)
            {
                case 1:
                    pd.Data = makeReports10の倍数(paoRep);
                    break;
                case 2:
                    pd.Data = makeReports郵便番号(paoRep);
                    break;
                case 3:
                    pd.Data = makeReports見積書(paoRep);
                    break;
                case 4:
                    pd.Data = makeReports請求書(paoRep);
                    break;
                case 5:
                    pd.Data = makeReports商品一覧(paoRep);
                    break;
                case 6:
                    pd.Data = makeReports広告(paoRep);
                    break;
                case 0:
                default:


                    ////帳票デザインファイルの読み込み（Azure環境で動作しない）
                    //string path = Server.MapPath("~/App_Data/simple.prepd");
                    //paoRep.LoadDefFile(path);

                    // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
                    string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.simple.prepd";
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
                    paoRep.LoadDefFile(stream);


                    //帳票編集
                    paoRep.PageStart();
                    string text = "ローカル開発環境のIIS\n\rで作った印刷データですよ～ん♪";
                    //string text = "Azure Winows Server\n\rで作った印刷データですよ～ん♪";
                    text += "\n\rREST API で変更！！(POST)";
                    paoRep.Write("Text2", text);
                    paoRep.PageEnd();
                    pd.Data = Convert.ToBase64String(paoRep.SaveData());
                    break;


            }

            return Json(pd);
        }



        protected string makeReports10の倍数(IReport paoRep)
        {

            // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.sample10.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            paoRep.LoadDefFile(stream);


            int page = 0; //頁数を定義
            int line = 0; //行数を定義

            for (int i = 0; i < 60; i++)
            {
                if (i % 15 == 0) //1頁15行で開始
                {
                    //頁開始を宣言
                    paoRep.PageStart();
                    page++;		//頁数をインクリメント
                    line = 0;	//行数を初期化

                    //＊＊＊ヘッダのセット＊＊＊
                    //文字列のセット
                    paoRep.Write("日付", System.DateTime.Now.ToString());
                    paoRep.Write("頁数", "Page - " + page.ToString());

                    //オブジェクトの属性変更
                    paoRep.z_Objects.SetObject("フォントサイズ");
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                    paoRep.Write("フォントサイズ", "フォントサイズ" + Environment.NewLine + " 変更後");

                    if (page == 2)
                        paoRep.Write("Line3", "");　 //２頁目の線をを消す

                }
                line++; //行数をインクリメント

                //＊＊＊明細のセット＊＊＊
                //繰返し文字列のセット
                paoRep.Write("行番号", (i + 1).ToString(), line);
                paoRep.Write("10倍数", ((i + 1) * 10).ToString(), line);
                //繰返し図形(横線)のセット
                paoRep.Write("横線", line);

                if (((i + 1) % 15) == 0) paoRep.PageEnd(); //1頁15行で終了宣言
            }

            return Convert.ToBase64String(paoRep.SaveData());
        }

        protected string makeReports見積書(IReport paoRep)
        {
            //// デザインファイル（Azure環境で動作しない）
            //string path = "";

            // ヘッダデータ読込
            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM 見積ヘッダ ORDER BY 見積番号", sqlcon);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "見積ヘッダ");

            sqlda = new SqlDataAdapter("SELECT * FROM 見積明細 ORDER BY 見積番号,行番号", sqlcon);
            sqlda.Fill(ds, "見積明細");


            DataTable ht = ds.Tables[0];
            foreach (DataRow hdr in ht.Rows)
            {

                // 見積書デザインファイルの読み込み ✅ 物理パス方式【お勧め】
                string path = Path.Combine(Server.MapPath("~"), @"App_Data/mitsumori.head.prepd");
                paoRep.LoadDefFile(path);

                paoRep.PageStart();
                paoRep.Write("お客様名", (string)hdr["お客様名"]);
                paoRep.Write("担当者名", (string)hdr["担当者名"]);
                paoRep.PageEnd();

                // 見積書デザインファイルの読み込み ✅ 物理パス方式【お勧め】
                path = Path.Combine(Server.MapPath("~"), @"App_Data/mitsumori.prepd");
                paoRep.LoadDefFile(path);

                paoRep.PageStart();

                paoRep.Write("見積番号", (string)hdr["見積番号"]);
                paoRep.Write("お客様名", (string)hdr["お客様名"]);
                paoRep.Write("担当者名", (string)hdr["担当者名"]);
                paoRep.Write("見積日", ((DateTime)hdr["見積日"]).ToString("yyyy年M月d日"));
                paoRep.Write("ヘッダ合計", "\\ " + string.Format("{0:N0}", hdr["合計金額"]));
                paoRep.Write("消費税額", string.Format("{0:N0}", hdr["消費税額"]));
                paoRep.Write("フッタ合計", string.Format("{0:N0}", hdr["合計金額"]));


                //明細の背景作成
                for (int i = 0; i < 7; i++)
                {
                    paoRep.Write("品番白", i + 1);
                    paoRep.Write("品番白", i + 1);
                    paoRep.Write("数量白", i + 1);
                    paoRep.Write("単価白", i + 1);
                    paoRep.Write("金額白", i + 1);
                    paoRep.Write("品番青", i + 1);
                    paoRep.Write("品名青", i + 1);
                    paoRep.Write("数量青", i + 1);
                    paoRep.Write("単価青", i + 1);
                    paoRep.Write("金額青", i + 1);
                }

                //明細の作成
                DataView dv = new DataView(ds.Tables["見積明細"]);
                dv.RowFilter = "見積番号 = '" + (string)hdr["見積番号"] + "'";
                for (int i = 0; i < dv.Count; i++)
                {
                    paoRep.Write("品番", (string)dv[i]["品番"], i + 1);
                    paoRep.Write("品名", (string)dv[i]["品名"], i + 1);
                    paoRep.Write("数量", dv[i]["数量"].ToString(), i + 1);
                    paoRep.Write("単価", string.Format("{0:N0}", dv[i]["単価"]), i + 1);
                    paoRep.Write("金額", string.Format("{0:N0}", dv[i]["金額"]), i + 1);
                }
                paoRep.PageEnd();
            }

            return Convert.ToBase64String(paoRep.SaveData());


        }
        protected string makeReports郵便番号(IReport paoRep)
        {

            // デザインファイル（Azure環境で動作しない）
            //string path1 = Server.MapPath("~/App_Data/zipcode1.prepd");
            //string path2 = Server.MapPath("~/App_Data/zipcode2.prepd");

            string resourcePath1 = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.zipcode1.prepd";
            var stream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath1);

            string resourcePath2 = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.zipcode2.prepd";
            var stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath2);


            // ヘッダデータ読込
            SqlDataAdapter sqlda = new SqlDataAdapter("select * from 郵便番号テーブル", sqlcon);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "PostTable");
            DataTable table = ds.Tables[0];

            int page = 0;
            int line = 999;
            string hDate = System.DateTime.Now.ToString();

            paoRep.LoadDefFile(stream1);
            foreach (DataRow row in table.Rows)
            {
                line++;
                if (line > 32)
                { // Head Print
                    if (page != 0) paoRep.PageEnd();

                    page++;

                    if (page == 6)
                    {
                        paoRep.LoadDefFile(stream2);
                    }

                    paoRep.PageStart();

                    paoRep.Write("日時", hDate);
                    paoRep.Write("ページ", "Page-" + page.ToString());

                    //QRコード描画
                    if (page < 6)
                    {
                        //paoRep.Write("QR", row["郵便番号"].ToString() + " " + row["市区町村"].ToString() + row["住所"].ToString());
                    }

                    line = 1;

                }

                //Body Print
                paoRep.Write("郵便番号", row["郵便番号"].ToString(), line);
                paoRep.Write("市区町村", row["市区町村"].ToString(), line);
                paoRep.Write("住所", row["住所"].ToString(), line);
                paoRep.Write("横罫線", line);


                if (page > 5 && line % 2 == 0)
                    paoRep.Write("網掛け", line / 2);

            }
            paoRep.PageEnd();

            return Convert.ToBase64String(paoRep.SaveData());
        }

        protected string makeReports広告(IReport paoRep)
        {
            // デザインファイル（Azure環境で動作しない）
            //string path = Server.MapPath("~/App_Data/koukoku.prepd");
            //paoRep.LoadDefFile(path);

            // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.koukoku.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            paoRep.LoadDefFile(stream);

            // 画像ファイルパス ❌ ファイル方式（Azure環境で動作しない）
            // string gpath = Server.MapPath("~/App_Data/img/");

            // データ読込
            SqlDataAdapter sqlda = new SqlDataAdapter("select * from 広告情報", sqlcon);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);
            DataTable table = ds.Tables[0];


            foreach (DataRow row in table.Rows)
            {

                paoRep.PageStart();

                paoRep.Write("製品名", (string)row["製品名"]);
                paoRep.Write("キャッチフレーズ", (string)row["キャッチフレーズ"]);
                paoRep.Write("商品コード", (string)row["商品コード"]);
                paoRep.Write("JANコード", (string)row["商品コード"]);
                paoRep.Write("売り文句", (string)row["売り文句"]);
                paoRep.Write("説明", (string)row["説明"]);
                paoRep.Write("価格", (string)row["価格"]);

                // 画像ファイルパス(gpath + DB内の画像ファイル名) ❌ ファイル方式（Azure環境で動作しない）
                //paoRep.Write("画像1", gpath + (string)row["画像1"]);
                //paoRep.Write("画像2", gpath + (string)row["画像2"]);

                // 🖼️ 画像処理：埋め込みリソースから読み込み
                string image1Path = GetImageFromEmbeddedResource((string)row["画像1"]);
                string image2Path = GetImageFromEmbeddedResource((string)row["画像2"]);
                paoRep.Write("画像1", image1Path);
                paoRep.Write("画像2", image2Path);

                paoRep.Write("QR", (string)row["製品名"] + " " + (string)row["キャッチフレーズ"]);

                paoRep.PageEnd();

            }

            return Convert.ToBase64String(paoRep.SaveData());
        }

        // 🖼️ 埋め込みリソースから画像を取得するヘルパーメソッド
        private string GetImageFromEmbeddedResource(string imageName)
        {
            try
            {
                string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.img." + imageName;
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

                if (stream == null) return ""; // 画像が見つからない場合

                // 一時ファイルに書き出し（Pao.Reportsがファイルパスを期待する場合）
                string tempPath = Path.GetTempPath();
                string tempFile = Path.Combine(tempPath, imageName);

                using (var fileStream = System.IO.File.Create(tempFile))
                {
                    stream.CopyTo(fileStream);
                }

                return tempFile;
            }
            catch
            {
                return ""; // エラー時は空文字
            }
        }

        protected string makeReports請求書(IReport paoRep)
        {
            //// デザインファイル（Azure環境で動作しない）
            //string path = Server.MapPath("~/App_Data/invoice.prepd");
            //paoRep.LoadDefFile(path);

            // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.invoice.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            paoRep.LoadDefFile(stream);


            // データ読込
            DataSet ds = new DataSet();

            SqlDataAdapter sqlda = new SqlDataAdapter("select * from 請求ヘッダ ORDER BY 請求番号", sqlcon);
            sqlda.Fill(ds, "請求ヘッダ");
            sqlda = new SqlDataAdapter("select * from 請求明細 ORDER BY 請求番号, 行番号", sqlcon);
            sqlda.Fill(ds, "請求明細");

            // 各列幅調整の配列
            float[] arr_w = { -5, 44, -20, -10, -9 };

            DataTable ht = ds.Tables[0];
            foreach (DataRow hdr in ht.Rows)
            {

                paoRep.PageStart();

                paoRep.Write("txtNo", (string)hdr["請求番号"]);
                paoRep.Write("txtCustomer", (string)hdr["お客様名"]);
                paoRep.Write("txtDate", DateTime.Now.ToString("yyyy年M月d日"));
                paoRep.Write("Image1", GetImageFromEmbeddedResource("kakuin.png"));
                
                // デザイン時の行数・列数取得
                paoRep.z_Objects.SetObject("hLine");
                int maxHLine = paoRep.z_Objects.z_Line.Repeat - 1;
                paoRep.z_Objects.SetObject("vLine");
                int maxVLine = paoRep.z_Objects.z_Line.Repeat - 1;

                //空の表を作成
                for (int i = 0; i < maxHLine; i++)
                {
                    // 「横罫線」描画
                    paoRep.Write("hLine", i + 1);

                    // 外枠の上を太く
                    if (i == 0)
                    {
                        paoRep.z_Objects.SetObject("hLine", i + 1);
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;
                    }

                    // 行ヘッダの下を二重線
                    if (i == 1)
                    {
                        paoRep.z_Objects.SetObject("hLine", i + 1);
                        paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double;
                    }

                    // 「行の背景」描画
                    paoRep.Write("LineRect", i + 1);
                    paoRep.z_Objects.SetObject("LineRect", i + 1);

                    if (i == 0)
                    // 行ヘッダはデザイン通り
                    {
                    }
                    else if (i < maxHLine - 3)
                    // 明細行
                    {
                        // 白・青の順番で背景色をセット
                        if (i % 2 == 1)
                        {
                            paoRep.z_Objects.z_Square.PaintColor = Color.White;
                        }
                        else
                        {
                            paoRep.z_Objects.z_Square.PaintColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    // 集計行
                    {
                        paoRep.z_Objects.z_Square.PaintColor = Color.FromArgb(255, 255, 180);
                    }


                    // 次回のXの位置
                    float svX = -1;

                    for (int j = 0; j < maxVLine; j++)
                    {

                        // 文字列項目の属性(幅/Font/Align/)変更
                        paoRep.z_Objects.SetObject("field" + (j + 1).ToString(), i + 1);

                        // 幅(TextBox)
                        paoRep.z_Objects.z_Text.Width = paoRep.z_Objects.z_Text.Width + arr_w[j];

                        // 位置(TextBox)
                        if (j > 0)
                        {
                            paoRep.z_Objects.z_Text.X = svX;
                        }
                        svX = paoRep.z_Objects.z_Text.X + paoRep.z_Objects.z_Text.Width;

                        // 行ヘッダの場合
                        if (i == 0)
                        {
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = true;
                        }
                        // 明細行の場合
                        else
                        {
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = false;
                            paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;

                            // 文字位置(Text Align)
                            switch (j + 1)
                            {
                                case 1:
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Center;
                                    break;
                                case 2:
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Left;
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                    paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Right;
                                    break;
                            }

                        }
                    }
                    //集計行の文字設定
                    for (int j = maxHLine; j > maxHLine - 3; j--)
                    {
                        paoRep.z_Objects.SetObject("field4", j);
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 16;
                        paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Center;
                        paoRep.z_Objects.z_Text.z_FontAttr.Bold = true;
                    }


                }

                // 縦罫線描画
                paoRep.z_Objects.SetObject("vLine");
                float baseX = paoRep.z_Objects.z_Line.X;
                for (int j = 0; j <= maxVLine; j++)
                {
                    paoRep.Write("vLine", j + 1);

                    paoRep.z_Objects.SetObject("vLine", j + 1);

                    //// 幅調整
                    for (int jj = 1; jj <= j && j < maxVLine; jj++)
                    {
                        float baseIntervalX = paoRep.z_Objects.z_Line.IntervalX;
                        paoRep.z_Objects.z_Line.IntervalX = baseIntervalX + arr_w[j - jj];
                    }

                    // 外枠を太線にする
                    if (j == 0 || j == maxVLine)
                    {
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;
                    }

                }


                // 見出し文字入れ
                paoRep.Write("field1", "品番", 1);
                paoRep.Write("field2", "品名", 1);
                paoRep.Write("field3", "数量", 1);
                paoRep.Write("field4", "単価", 1);
                paoRep.Write("field5", "金額", 1);

                //明細の作成
                DataView dv = new DataView(ds.Tables["請求明細"]);
                dv.RowFilter = "請求番号 = '" + (string)hdr["請求番号"] + "'";
                long totalAmount = 0;
                int ii = 0;
                for (; ii < dv.Count; ii++)
                {
                    paoRep.Write("field1", (string)dv[ii]["品番"], ii + 2);
                    paoRep.Write("field2", (string)dv[ii]["品名"], ii + 2);
                    paoRep.Write("field3", dv[ii]["数量"].ToString(), ii + 2);
                    paoRep.Write("field4", string.Format("{0:N0}", dv[ii]["単価"]), ii + 2);
                    long amount = Convert.ToInt64(dv[ii]["数量"]) * Convert.ToInt64(dv[ii]["単価"]);
                    paoRep.Write("field5", string.Format("{0:N0}", amount), ii + 2);
                    totalAmount += amount;

                }

                double tax = 0.05;

                paoRep.Write("field4", "小計", maxHLine - 2);
                paoRep.Write("field5", string.Format("{0:N0}", totalAmount), maxHLine - 2);
                ii++;
                paoRep.Write("field4", "消費税", maxHLine - 1);
                paoRep.Write("field5", string.Format("{0:N0}", totalAmount * tax), maxHLine - 1);
                ii++;
                paoRep.Write("field4", "合計", maxHLine);
                paoRep.Write("field5", string.Format("{0:N0}", totalAmount + (totalAmount * tax)), maxHLine);

                paoRep.Write("txtTotal", string.Format("{0:N0}", totalAmount + (totalAmount * tax)));


                // 小計の上を二重線
                paoRep.z_Objects.SetObject("hLine", maxHLine - 2);
                paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double;

                // 最終行を太く
                paoRep.Write("hLine", maxHLine + 1);
                paoRep.z_Objects.SetObject("hLine", maxHLine + 1);
                paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;


                paoRep.PageEnd();

            }

            return Convert.ToBase64String(paoRep.SaveData());

        }

        /// <summary>
        /// 商品マスタ用構造体
        /// </summary>
        protected class PrintData商品
        {
            internal string s大分類コード = "";
            internal string s小分類コード = "";
            internal string s大分類名称 = "";
            internal string s小分類名称 = "";
            internal string s品番 = "";
            internal string s品名 = "";
        }
        protected string makeReports商品一覧(IReport paoRep)
        {
            //// デザインファイル（Azure環境で動作しない）
            //string path = Server.MapPath("~/App_Data/itemlist.prepd");
            //paoRep.LoadDefFile(path);

            // 帳票デザインファイルの読み込み ✅ 埋め込みリソース方式
            string resourcePath = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.itemlist.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            paoRep.LoadDefFile(stream);


            // データ読込
            string sql = "";
            sql += " SELECT C.*, A.大分類名称, B.小分類名称 ";
            sql += " FROM ";
            sql += "   M_大分類 AS A";
            sql += " , M_小分類 AS B";
            sql += " , M_商品 AS C";
            sql += " WHERE";
            sql += " A.大分類コード = B.大分類コード";
            sql += " AND";
            sql += " A.大分類コード = C.大分類コード";
            sql += " AND";
            sql += " B.大分類コード = C.大分類コード";
            sql += " AND";
            sql += " B.小分類コード = C.小分類コード";
            sql += " ORDER BY C.大分類コード, C.小分類コード";

            SqlDataAdapter sqlda = new SqlDataAdapter(sql, sqlcon);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);
            DataTable dt = ds.Tables[0];


            // いったん構造体の配列にセット

            string sv大分類名称 = null;
            string sv小分類名称 = null;

            int cnt大分類 = 0;
            int cnt小分類 = 0;
            List<PrintData商品> pds = new List<PrintData商品>();
            PrintData商品 pd;
            foreach (DataRow dr in dt.Rows)
            {
                pd = new PrintData商品();

                // キーブレイク処理は、今回は構造体にセットするところでやってみました。
                // プログラム構造的にもっと汎用的な方法はあります。
                if (sv小分類名称 != null && sv小分類名称 != dr["小分類名称"].ToString())
                {
                    pd.s小分類コード = " ";
                    pd.s小分類名称 = "小分類(" + sv小分類名称 + ")小計";
                    pd.s品番 = cnt小分類.ToString() + " 冊";
                    cnt小分類 = 0;
                    pds.Add(pd);
                    pd = new PrintData商品();
                }
                if (sv大分類名称 != null && sv大分類名称 != dr["大分類名称"].ToString())
                {
                    pd.s大分類コード = " ";
                    pd.s小分類名称 = "大分類(" + sv大分類名称 + ")小計";
                    pd.s品番 = cnt大分類.ToString() + " 冊";
                    cnt大分類 = 0;
                    pds.Add(pd);
                    pd = new PrintData商品();
                }

                if (sv大分類名称 != dr["大分類名称"].ToString())
                {
                    pd.s大分類名称 = dr["大分類名称"].ToString();
                }
                if (sv小分類名称 != dr["小分類名称"].ToString())
                {
                    pd.s小分類名称 = dr["小分類名称"].ToString();
                }
                pd.s品番 = dr["品番"].ToString();
                pd.s品名 = dr["品名"].ToString();

                pds.Add(pd);

                sv大分類名称 = dr["大分類名称"].ToString();
                sv小分類名称 = dr["小分類名称"].ToString();

                cnt大分類++;
                cnt小分類++;
            }


            // 商品構造体にセット
            pd = new PrintData商品();
            pd.s小分類コード = " ";
            pd.s小分類名称 = "小分類(" + sv小分類名称 + ")小計";
            pd.s品番 = cnt小分類.ToString() + " 冊";
            pds.Add(pd);
            pd = new PrintData商品();
            pd.s大分類コード = " ";
            pd.s小分類名称 = "大分類(" + sv大分類名称 + ")小計";
            pd.s品番 = cnt大分類.ToString() + " 冊";
            pds.Add(pd);


            //帳票データセット・出力
            paoRep.PageStart();

            const int RecnumInPage = 20;

            paoRep.z_Objects.SetObject("枠_大分類");

            string[] filedNames_枠 = { "枠_大分類", "枠_小分類", "枠_品番", "枠_品名" };
            string[] filedNames = { "大分類", "小分類", "品番", "品名" };

            for (int recno = 0; recno < pds.Count; recno++)
            {

                if (recno % RecnumInPage == 0)
                {
                    if (recno != 0)
                    {
                        paoRep.PageEnd();
                        paoRep.PageStart();
                    }
                }

                // 値セット
                int lineno = (recno % RecnumInPage) + 1;
                paoRep.Write("大分類", pds[recno].s大分類名称, lineno);
                paoRep.Write("小分類", pds[recno].s小分類名称, lineno);
                paoRep.Write("品番", pds[recno].s品番, lineno);
                paoRep.Write("品名", pds[recno].s品名, lineno);

                // 枠描画
                for (int j = 0; j < filedNames_枠.Length; j++)
                {
                    paoRep.Write(filedNames_枠[j], lineno);
                }

                // 小分類小計行の色替え
                if (pds[recno].s小分類コード == " ")
                {
                    // 枠描画
                    for (int j = 0; j < filedNames_枠.Length; j++)
                    {
                        paoRep.z_Objects.SetObject(filedNames_枠[j], lineno);
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightYellow;
                    }

                }
                // 大分類小計行の色替え
                else if (pds[recno].s大分類コード == " ")
                {
                    // 枠描画
                    for (int j = 0; j < filedNames_枠.Length; j++)
                    {
                        paoRep.z_Objects.SetObject(filedNames_枠[j], lineno);
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightPink;
                    }

                }

            }

            paoRep.PageEnd();

            return Convert.ToBase64String(paoRep.SaveData());
        }

    }
}