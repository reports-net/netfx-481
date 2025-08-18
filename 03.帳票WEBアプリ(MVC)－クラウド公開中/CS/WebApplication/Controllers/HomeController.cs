using Pao.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection sqlcon = null;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string ReportsKind, string PdfAction)
        {
            if (PdfAction.StartsWith("View"))
            {
                ViewBag.ReportsKind = ReportsKind;
                return View("ShowPdf");
            }
            else
            {
                byte[] pdfBytes = GeneratePdfBytes(ReportsKind);
                return File(pdfBytes, "application/pdf", ReportsKind + ".pdf");
            }
        }

        public ActionResult GetPdfStream(string ReportsKind)
        {
            byte[] pdfBytes = GeneratePdfBytes(ReportsKind);
            return File(pdfBytes, "application/pdf");
        }


        // PDF生成を共通化
        private byte[] GeneratePdfBytes(string ReportsKind)
        {
            #region 接続文字列
            sqlcon = new SqlConnection("Server=tcp:fzxu46e9ck.database.windows.net,1433;Initial Catalog=Reports.net.Sample;Persist Security Info=False;User ID=AzureLab;Password=ayakaRk9504w;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            #endregion

            IReport paoRep = ReportCreator.GetPdf();
            if (ReportsKind == "simple")
            {
                makeReports単純なサンプル(paoRep);
            }
            else if (ReportsKind == "simple10")
            {
                makeReports10の倍数(paoRep);
            }
            else if (ReportsKind == "mitsumori")
            {
                makeReports見積書(paoRep);
            }
            else if (ReportsKind == "zipcode")
            {
                makeReports郵便番号(paoRep);
            }
            else if (ReportsKind == "koukoku")
            {
                paoRep = ReportCreator.GetImagePdf();
                makeReports広告(paoRep);
            }
            else if (ReportsKind == "invoice")
            {
                makeReports請求書(paoRep);
            }
            else if (ReportsKind == "itemlist")
            {
                makeReports商品一覧(paoRep);
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                paoRep.SavePDF(memoryStream);
                return memoryStream.ToArray();
            }
        }

        protected void makeReports単純なサンプル(IReport paoRep)
        {
            string path = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.simple.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            paoRep.LoadDefFile(stream);

            paoRep.PageStart();
            paoRep.Write("Text2", ".NET Framework MVCで作った\n印刷データですよ～ん♪");
            paoRep.PageEnd();
        }

        protected void makeReports10の倍数(IReport paoRep)
        {
            string path = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.sample10.prepd";
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            paoRep.LoadDefFile(stream);

            int page = 0;
            int line = 0;

            for (int i = 0; i < 60; i++)
            {
                if (i % 15 == 0)
                {
                    paoRep.PageStart();
                    page++;
                    line = 0;

                    paoRep.Write("日付", System.DateTime.Now.ToString());
                    paoRep.Write("頁数", "Page - " + page.ToString());

                    paoRep.z_Objects.SetObject("フォントサイズ");
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                    paoRep.Write("フォントサイズ", "フォントサイズ" + Environment.NewLine + " 変更後");

                    if (page == 2)
                        paoRep.Write("Line3", "");
                }
                line++;

                paoRep.Write("行番号", (i + 1).ToString(), line);
                paoRep.Write("10倍数", ((i + 1) * 10).ToString(), line);
                paoRep.Write("横線", line);

                if (((i + 1) % 15) == 0) paoRep.PageEnd();
            }
        }

        protected void makeReports見積書(IReport paoRep)
        {
            string path = "";

            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM 見積ヘッダ ORDER BY 見積番号", sqlcon);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "見積ヘッダ");

            sqlda = new SqlDataAdapter("SELECT * FROM 見積明細 ORDER BY 見積番号,行番号", sqlcon);
            sqlda.Fill(ds, "見積明細");

            DataTable ht = ds.Tables[0];
            foreach (DataRow hdr in ht.Rows)
            {
                path = Path.Combine(Server.MapPath("~"), @"App_Data/mitsumori.head.prepd");
                paoRep.LoadDefFile(path);
                paoRep.PageStart();
                paoRep.Write("お客様名", (string)hdr["お客様名"]);
                paoRep.Write("担当者名", (string)hdr["担当者名"]);
                paoRep.PageEnd();

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
        }

        protected void makeReports郵便番号(IReport paoRep)
        {
            string path1 = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.zipcode1.prepd";
            var stream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream(path1);
            string path2 = Assembly.GetExecutingAssembly().GetName().Name + ".App_Data.zipcode2.prepd";
            var stream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(path2);

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
                {
                    if (page != 0) paoRep.PageEnd();
                    page++;

                    if (page == 6)
                    {
                        paoRep.LoadDefFile(stream2);
                    }

                    paoRep.PageStart();
                    paoRep.Write("日時", hDate);
                    paoRep.Write("ページ", "Page-" + page.ToString());

                    if (page < 6)
                    {
                        paoRep.Write("QR", row["郵便番号"].ToString() + " " + row["市区町村"].ToString() + row["住所"].ToString());
                    }
                    line = 1;
                }

                paoRep.Write("郵便番号", row["郵便番号"].ToString(), line);
                paoRep.Write("市区町村", row["市区町村"].ToString(), line);
                paoRep.Write("住所", row["住所"].ToString(), line);
                paoRep.Write("横罫線", line);

                if (page > 5 && line % 2 == 0)
                    paoRep.Write("網掛け", line / 2);
            }
            paoRep.PageEnd();
        }

        protected void makeReports広告(IReport paoRep)
        {
            string path = Path.Combine(Server.MapPath("~"), @"App_Data/koukoku.prepd");
            paoRep.LoadDefFile(path);

            string gpath = Path.Combine(Server.MapPath("~"), @"App_Data/img\");

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
                paoRep.Write("画像1", gpath + (string)row["画像1"]);
                paoRep.Write("画像2", gpath + (string)row["画像2"]);
                paoRep.Write("QR", (string)row["製品名"] + " " + (string)row["キャッチフレーズ"]);
                paoRep.PageEnd();
            }
        }

        protected void makeReports請求書(IReport paoRep)
        {
            string path = Path.Combine(Server.MapPath("~"), @"App_Data/invoice.prepd");
            paoRep.LoadDefFile(path);

            // 会社角印画像ファイルパス
            string gpath = System.IO.Path.Combine(Server.MapPath("~"), @"App_Data/img\");

            DataSet ds = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter("select * from 請求ヘッダ ORDER BY 請求番号", sqlcon);
            sqlda.Fill(ds, "請求ヘッダ");
            sqlda = new SqlDataAdapter("select * from 請求明細 ORDER BY 請求番号, 行番号", sqlcon);
            sqlda.Fill(ds, "請求明細");

            float[] arr_w = { -5, 44, -20, -10, -9 };

            DataTable ht = ds.Tables[0];
            foreach (DataRow hdr in ht.Rows)
            {
                paoRep.PageStart();
                paoRep.Write("txtNo", (string)hdr["請求番号"]);
                paoRep.Write("txtCustomer", (string)hdr["お客様名"]);
                paoRep.Write("txtDate", DateTime.Now.ToString("yyyy年M月d日"));
                paoRep.Write("Image1", gpath + "kakuin.png");

                paoRep.z_Objects.SetObject("hLine");
                int maxHLine = paoRep.z_Objects.z_Line.Repeat - 1;
                paoRep.z_Objects.SetObject("vLine");
                int maxVLine = paoRep.z_Objects.z_Line.Repeat - 1;

                for (int i = 0; i < maxHLine; i++)
                {
                    paoRep.Write("hLine", i + 1);

                    if (i == 0)
                    {
                        paoRep.z_Objects.SetObject("hLine", i + 1);
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;
                    }

                    if (i == 1)
                    {
                        paoRep.z_Objects.SetObject("hLine", i + 1);
                        paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double;
                    }

                    paoRep.Write("LineRect", i + 1);
                    paoRep.z_Objects.SetObject("LineRect", i + 1);

                    if (i == 0)
                    {
                    }
                    else if (i < maxHLine - 3)
                    {
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
                    {
                        paoRep.z_Objects.z_Square.PaintColor = Color.FromArgb(255, 255, 180);
                    }

                    float svX = -1;

                    for (int j = 0; j < maxVLine; j++)
                    {
                        paoRep.z_Objects.SetObject("field" + (j + 1).ToString(), i + 1);
                        paoRep.z_Objects.z_Text.Width = paoRep.z_Objects.z_Text.Width + arr_w[j];

                        if (j > 0)
                        {
                            paoRep.z_Objects.z_Text.X = svX;
                        }
                        svX = paoRep.z_Objects.z_Text.X + paoRep.z_Objects.z_Text.Width;

                        if (i == 0)
                        {
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = true;
                        }
                        else
                        {
                            paoRep.z_Objects.z_Text.z_FontAttr.Bold = false;
                            paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;

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

                    for (int j = maxHLine; j > maxHLine - 3; j--)
                    {
                        paoRep.z_Objects.SetObject("field4", j);
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 16;
                        paoRep.z_Objects.z_Text.TextAlign = Pao.Reports.PmAlignType.Center;
                        paoRep.z_Objects.z_Text.z_FontAttr.Bold = true;
                    }
                }

                paoRep.z_Objects.SetObject("vLine");
                float baseX = paoRep.z_Objects.z_Line.X;
                for (int j = 0; j <= maxVLine; j++)
                {
                    paoRep.Write("vLine", j + 1);
                    paoRep.z_Objects.SetObject("vLine", j + 1);

                    for (int jj = 1; jj <= j && j < maxVLine; jj++)
                    {
                        float baseIntervalX = paoRep.z_Objects.z_Line.IntervalX;
                        paoRep.z_Objects.z_Line.IntervalX = baseIntervalX + arr_w[j - jj];
                    }

                    if (j == 0 || j == maxVLine)
                    {
                        paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;
                    }
                }

                paoRep.Write("field1", "品番", 1);
                paoRep.Write("field2", "品名", 1);
                paoRep.Write("field3", "数量", 1);
                paoRep.Write("field4", "単価", 1);
                paoRep.Write("field5", "金額", 1);

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

                paoRep.z_Objects.SetObject("hLine", maxHLine - 2);
                paoRep.z_Objects.z_Line.z_LineAttr.Type = PmLineType.Double;

                paoRep.Write("hLine", maxHLine + 1);
                paoRep.z_Objects.SetObject("hLine", maxHLine + 1);
                paoRep.z_Objects.z_Line.z_LineAttr.Width = 0.5f;

                paoRep.PageEnd();
            }
        }

        protected class PrintData
        {
            internal string s大分類コード = "";
            internal string s小分類コード = "";
            internal string s大分類名称 = "";
            internal string s小分類名称 = "";
            internal string s品番 = "";
            internal string s品名 = "";
        }

        protected void makeReports商品一覧(IReport paoRep)
        {
            string path = Path.Combine(Server.MapPath("~"), @"App_Data/itemlist.prepd");
            paoRep.LoadDefFile(path);

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

            string sv大分類名称 = null;
            string sv小分類名称 = null;
            int cnt大分類 = 0;
            int cnt小分類 = 0;
            List<PrintData> pds = new List<PrintData>();
            PrintData pd;
            foreach (DataRow dr in dt.Rows)
            {
                pd = new PrintData();

                if (sv小分類名称 != null && sv小分類名称 != dr["小分類名称"].ToString())
                {
                    pd.s小分類コード = " ";
                    pd.s小分類名称 = "小分類(" + sv小分類名称 + ")小計";
                    pd.s品番 = cnt小分類.ToString() + " 冊";
                    cnt小分類 = 0;
                    pds.Add(pd);
                    pd = new PrintData();
                }
                if (sv大分類名称 != null && sv大分類名称 != dr["大分類名称"].ToString())
                {
                    pd.s大分類コード = " ";
                    pd.s小分類名称 = "大分類(" + sv大分類名称 + ")小計";
                    pd.s品番 = cnt大分類.ToString() + " 冊";
                    cnt大分類 = 0;
                    pds.Add(pd);
                    pd = new PrintData();
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

            pd = new PrintData();
            pd.s小分類コード = " ";
            pd.s小分類名称 = "小分類(" + sv小分類名称 + ")小計";
            pd.s品番 = cnt小分類.ToString() + " 冊";
            pds.Add(pd);
            pd = new PrintData();
            pd.s大分類コード = " ";
            pd.s小分類名称 = "大分類(" + sv大分類名称 + ")小計";
            pd.s品番 = cnt大分類.ToString() + " 冊";
            pds.Add(pd);

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

                int lineno = (recno % RecnumInPage) + 1;
                paoRep.Write("大分類", pds[recno].s大分類名称, lineno);
                paoRep.Write("小分類", pds[recno].s小分類名称, lineno);
                paoRep.Write("品番", pds[recno].s品番, lineno);
                paoRep.Write("品名", pds[recno].s品名, lineno);

                for (int j = 0; j < filedNames_枠.Length; j++)
                {
                    paoRep.Write(filedNames_枠[j], lineno);
                }

                if (pds[recno].s小分類コード == " ")
                {
                    for (int j = 0; j < filedNames_枠.Length; j++)
                    {
                        paoRep.z_Objects.SetObject(filedNames_枠[j], lineno);
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightYellow;
                    }
                }
                else if (pds[recno].s大分類コード == " ")
                {
                    for (int j = 0; j < filedNames_枠.Length; j++)
                    {
                        paoRep.z_Objects.SetObject(filedNames_枠[j], lineno);
                        paoRep.z_Objects.z_Square.PaintColor = Color.LightPink;
                    }
                }
            }

            paoRep.PageEnd();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}