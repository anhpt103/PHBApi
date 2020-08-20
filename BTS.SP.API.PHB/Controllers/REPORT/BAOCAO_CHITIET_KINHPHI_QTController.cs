using BTS.SP.PHB.ENTITY.Dm;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/Nv/BAOCAO_CHITIET_QT")]
    [Route("{id?}")]
    [Authorize]
    public class BAOCAO_CHITIET_KINHPHI_QTController : ApiController
    {
        public class ExportParams
        {
            public string MA_CAP { get; set; }
            public string MA_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public string MA_DBHC { get; set; }
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
            public DateTime TUNGAY_KETSO { get; set; }
            public DateTime DENNGAY_KETSO { get; set; }
            public string LST_MA_DVQHNS { get; set; }
        }

        public class ResultItems
        {
            public string MA_DIABAN { get; set; }
            public string MA_CAP { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public string TEN_TIEUMUC { get; set; }
            public string MA_DVQHNS { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
            public string TUCHU { get; set; }
        }

        public class Result1CItems
        {
            public string MA_CHITIEU { get; set; }
            public string SAPXEP { get; set; }
            public string TEN_CHITIEU { get; set; }
            public string STT { get; set; }
            public string CONGTHUC_WHERE { get; set; }
            public int INDAM { get; set; }
            public int INNGHIENG { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
        }
        //lấy file excel
        [Route("_Export1C")]
        [AllowAnonymous]
        public HttpResponseMessage _Export1C(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            file = _CreateExcelFile1C(para);
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                }
            }

            return result;
        }
        //lấy data
        public string _CreateExcelFile1C(ExportParams para)

        {
            //var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = para.MA_DBHC;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var urlExtensionFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BM1C_PI_Extend.txt";
            string extensionStr = System.IO.File.ReadAllText(urlExtensionFile);
            extensionStr = extensionStr.Replace("\r", "");
            extensionStr = extensionStr.Replace("\n", "");
            if (unit.Length == 3)
            {
                extensionStr += " AND MA_KBNN IN (SELECT MA FROM DM_TKKHOBAC WHERE MA_DBHC LIKE '" + unit + "') ";
            }
            var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BC_TINHHINH_KINHPHI.xlsx";
            var urlFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/UploadFile/ExcelTmp/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(urlFileTemplate))
            {
                List<DM_CHITIEU_BAOCAO> items = new List<DM_CHITIEU_BAOCAO>();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                cmd.CommandText = @"SELECT MA_CHITIEU,SAPXEP,TEN_CHITIEU,STT,CONGTHUC_WHERE,INDAM,INNGHIENG FROM DM_CHITIEU_BAOCAO WHERE MA_BAOCAO = 'BM1C_PI'";
                cmd.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DM_CHITIEU_BAOCAO dt = new DM_CHITIEU_BAOCAO
                    {
                        MA_CHITIEU = dr["MA_CHITIEU"].ToString(),
                        SAPXEP = dr["SAPXEP"].ToString(),
                        TEN_CHITIEU = dr["TEN_CHITIEU"].ToString(),
                        STT = dr["STT"].ToString(),
                        CONGTHUC_WHERE = dr["CONGTHUC_WHERE"].ToString(),
                        INDAM = int.Parse(dr["INDAM"].ToString()),
                        INNGHIENG = int.Parse(dr["INNGHIENG"].ToString())
                    };
                    items.Add(dt);
                }
                dr.Close();
                cmd.Connection.Close();

                var itemsF = items.Where(x => x.STT != null).OrderBy(y => int.Parse(y.STT)).ToList();
                List<string> s8 = new List<string>(new[] { "16", "17", "18", "19", "20", "21", "41", "42", "43", "44", "45", "53", "54", "55", "56" });
                List<string> crdr = new List<string>(new[] { "31", "32", "35", "46" });
                //List<string> drcr = new List<string>(new[] { "22", "23", "27" });
                List<PHA_HACHTOAN_CHI> result = new List<PHA_HACHTOAN_CHI>();
                foreach (var t in itemsF)
                {
                    if (!string.IsNullOrEmpty(t.CONGTHUC_WHERE))
                    {

                        var tbl = "PHA_DUTOAN";
                        var gtht = "GIA_TRI_HACH_TOAN";
                        if (s8.Contains(t.STT))
                        {
                            tbl = "PHA_HACHTOAN_CHI";
                        }
                        if (!s8.Contains(t.STT))
                        {
                            gtht = "ACCOUNTED_DR - ACCOUNTED_CR";
                        }
                        if (crdr.Contains(t.STT))
                        {
                            gtht = "ACCOUNTED_CR - ACCOUNTED_DR";
                        }
                        OracleCommand cmdt = new OracleCommand();
                        OracleDataReader drt;
                        cmdt.CommandText = string.Format("SELECT MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS,SUM(" + gtht + ") AS GIA_TRI_HACH_TOAN FROM " + tbl + " WHERE ");
                        if (!string.IsNullOrEmpty(para.MA_CAP))
                        {
                            cmdt.CommandText += "MA_CAP = '" + para.MA_CAP + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_CHUONG))
                        {
                            cmdt.CommandText += "MA_CHUONG = '" + para.MA_CHUONG + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_LOAI))
                        {
                            cmdt.CommandText += "MA_LOAI = '" + para.MA_LOAI + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_NGANHKT))
                        {
                            cmdt.CommandText += "MA_NGANHKT = '" + para.MA_NGANHKT + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_MUC))
                        {
                            cmdt.CommandText += "MA_MUC = '" + para.MA_MUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
                        {
                            cmdt.CommandText += "MA_TIEUMUC = '" + para.MA_TIEUMUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_DVQHNS))
                        {
                            cmdt.CommandText += "MA_DVQHNS IN ('" + para.MA_DVQHNS + "') AND ";
                        }
                        cmdt.CommandText += "TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY') " +
                                            extensionStr.Trim() + " AND ";
                        cmdt.CommandText += t.CONGTHUC_WHERE;
                        cmdt.CommandText += " GROUP BY MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS";
                        cmdt.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                        cmdt.Connection.Open();
                        drt = cmdt.ExecuteReader();
                        while (drt.Read())
                        {
                            PHA_HACHTOAN_CHI tmpItem = new PHA_HACHTOAN_CHI();
                            tmpItem.MA_CAP = drt["MA_CAP"].ToString();
                            tmpItem.MA_CHUONG = drt["MA_CHUONG"].ToString();
                            tmpItem.MA_LOAI = drt["MA_LOAI"].ToString();
                            tmpItem.MA_NGANHKT = drt["MA_NGANHKT"].ToString();
                            tmpItem.MA_MUC = drt["MA_MUC"].ToString();
                            tmpItem.MA_TIEUMUC = drt["MA_TIEUMUC"].ToString();
                            tmpItem.MA_DVQHNS = drt["MA_DVQHNS"].ToString();
                            //Gán STT vào SEG12 để group khi xuất file -- Không ảnh hưởng dữ liệu
                            tmpItem.SEGMENT12 = t.STT;
                            if (drt["GIA_TRI_HACH_TOAN"].ToString() != "")
                            {
                                tmpItem.GIA_TRI_HACH_TOAN = decimal.Parse(drt["GIA_TRI_HACH_TOAN"].ToString());
                            }
                            result.Add(tmpItem);
                        }
                        drt.Close();
                        cmdt.Connection.Close();
                    }
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(urlFileTemplate, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcel1C(workSheet, result, itemsF, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;
        }
        //ghi dữ liệu vào file excel
        public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHA_HACHTOAN_CHI> result, List<DM_CHITIEU_BAOCAO> items, ExportParams para)
        {
            var startRow = 12;
            var startCol = 6;
            for (int i = 0; i < items.Count; i++)
            {
                ws.Cells[startRow + i, 1].Value = items[i].SAPXEP;
                ws.Cells[startRow + i, 2].Value = items[i].TEN_CHITIEU;
                if (items[i].INDAM == 1)
                {
                    ws.Cells[startRow + i, 1].Style.Font.Bold = true;
                    ws.Cells[startRow + i, 2].Style.Font.Bold = true;
                }
                if (items[i].INNGHIENG == 1)
                {
                    ws.Cells[startRow + i, 1].Style.Font.Italic = true;
                    ws.Cells[startRow + i, 2].Style.Font.Italic = true;
                }
                if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                {
                    ws.Cells[startRow + i, 3].Value = result.Where(y => y.SEGMENT12 == items[i].STT).Sum(x => x.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow + i, 3].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }

            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();
            for (int j = 0; j < loaiS.Count; j++)
            {
                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                ws.Cells[7, startCol, 7, startCol + (khoanS.Count * 3) + 2].Merge = true;
                ws.Cells[7, startCol, 7, startCol + (khoanS.Count * 3) + 2].Style.Font.Bold = true;
                ws.Cells[7, startCol, 7, startCol + (khoanS.Count * 3) + 2].Style.WrapText = true;
                ws.Cells[7, startCol, 7, startCol + (khoanS.Count * 3) + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[7, startCol].Value = "Loại" + loaiS[j];

                ws.Cells[8, startCol, 8, startCol + 2].Merge = false;
                ws.Cells[8, startCol, 8, startCol + 2].Merge = true;
                ws.Cells[8, startCol, 8, startCol + 2].Value = "Tổng loại " + loaiS[j];

                ws.Cells[9, startCol].Value = "Số báo cáo";
                ws.Cells[9, startCol + 1].Value = "Số xét duyệt/TĐ";
                ws.Cells[9, startCol + 2].Value = "Chênh lệch";

                ws.Cells[8, startCol, 9, startCol + 2].Style.Font.Bold = true;
                ws.Cells[8, startCol, 9, startCol + 2].Style.WrapText = true;
                ws.Cells[8, startCol, 9, startCol + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (int i = 0; i < items.Count; i++)
                {
                    if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                    {
                        ws.Cells[startRow + i, startCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow + i, startCol].Style.Numberformat.Format = "###,###,###,###,###";
                    }
                }

                for (int k = 0; k < khoanS.Count; k++)
                {
                    var nextCol = startCol + ((k + 1) * 3);
                    ws.Cells[8, nextCol, 8, nextCol + 2].Merge = false;
                    ws.Cells[8, nextCol, 8, nextCol + 2].Merge = true;
                    ws.Cells[8, nextCol, 8, nextCol + 2].Value = "Khoản " + khoanS[k];

                    ws.Cells[9, nextCol].Value = "Số báo cáo";
                    ws.Cells[9, nextCol + 1].Value = "Số xét duyệt/TĐ";
                    ws.Cells[9, nextCol + 2].Value = "Chênh lệch";

                    ws.Cells[8, nextCol, 9, nextCol + 2].Style.Font.Bold = true;
                    ws.Cells[8, nextCol, 9, nextCol + 2].Style.WrapText = true;
                    ws.Cells[8, nextCol, 9, nextCol + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                        {
                            ws.Cells[startRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + i, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }
                }

                //Gán số cột bằng số cột hiện tại
                startCol += (khoanS.Count * 3) + 3;
            }

            var lstDvqhns = result.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Merge = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.Font.Bold = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.WrapText = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            for (int j = 0; j < lstDvqhns.Count; j++)
            {
                ws.Cells[8, startCol + j, 9, startCol + j].Merge = false;
                ws.Cells[8, startCol + j, 9, startCol + j].Merge = true;
                ws.Cells[8, startCol + j, 9, startCol + j].Value = lstDvqhns[j];
                ws.Cells[8, startCol + j, 9, startCol + j].Style.Font.Bold = true;
                ws.Cells[8, startCol + j, 9, startCol + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                for (int i = 0; i < items.Count; i++)
                {
                    ws.Cells[startRow + i, startCol + j].Value = result.Where(x => x.SEGMENT12 == items[i].STT && x.MA_DVQHNS == lstDvqhns[j]).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow + i, startCol + j].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }
            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Ngày hiệu lực từ ngày " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[3, 1].Value = "Ngày kết sổ từ ngày " + para.TUNGAY_KETSO.ToString("d") + " đến ngày " + para.DENNGAY_KETSO.ToString("d");

            ws.SelectedRange[7, 6, 10, startCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 6, 10, startCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 6, 10, startCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 6, 10, startCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        [Route("Export")]
        [AllowAnonymous]
        public HttpResponseMessage Export(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            try
            {

                file = CreateExcelFile(para);
                if (file == "OverLoad")
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                    result.Content = new StringContent("OverLoad");
                    return result;
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (!File.Exists(file))
                    {
                        result = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.OK);
                        result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        private string CreateExcelFile(ExportParams para)

        {
            //var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = para.MA_DBHC;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BC_CHITIET_QUYETTOAN.xlsx";
            var urlFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/UploadFile/ExcelTmp/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(urlFileTemplate))
            {
                List<ResultItems> items = new List<ResultItems>();
                OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BTSTC.PHA_BAOCAO_CHITIETKINHPHIQT";
                command.Parameters.Clear();
                command.Parameters.Add("MA_CAP", OracleDbType.NVarchar2, 100).Value = para.MA_CAP;
                command.Parameters.Add("MA_CHUONG", OracleDbType.NVarchar2, 100).Value = para.MA_CHUONG;
                command.Parameters.Add("MA_LOAI", OracleDbType.NVarchar2, 100).Value = para.MA_LOAI;
                command.Parameters.Add("MA_NGANHKT", OracleDbType.NVarchar2, 100).Value = para.MA_NGANHKT;
                command.Parameters.Add("MA_MUC", OracleDbType.NVarchar2, 100).Value = para.MA_MUC;
                command.Parameters.Add("MA_TIEUMUC", OracleDbType.NVarchar2, 100).Value = para.MA_TIEUMUC;
                command.Parameters.Add("MA_DVQHNS", OracleDbType.NVarchar2, 100).Value = para.MA_DVQHNS;
                command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2, 100).Value = unit;
                command.Parameters.Add("TUNGAY_HIEULUC", OracleDbType.Date).Value = para.TUNGAY_HIEULUC;
                command.Parameters.Add("DENNGAY_HIEULUC", OracleDbType.Date).Value = para.DENNGAY_HIEULUC;
                command.Parameters.Add("TUNGAY_KETSO", OracleDbType.Date).Value = para.TUNGAY_KETSO;
                command.Parameters.Add("DENNGAY_KETSO", OracleDbType.Date).Value = para.DENNGAY_KETSO;
                command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader();
                while (reader.Read())
                {
                    var item = new ResultItems();
                    item.MA_DIABAN = reader["MA_DIABAN"].ToString();
                    item.MA_CAP = reader["MA_CAP"].ToString();
                    item.MA_CHUONG = reader["MA_CHUONG"].ToString();
                    item.MA_LOAI = reader["MA_LOAI"].ToString();
                    item.MA_NGANHKT = reader["MA_NGANHKT"].ToString();
                    item.MA_MUC = reader["MA_MUC"].ToString();
                    item.MA_TIEUMUC = reader["MA_TIEUMUC"].ToString();
                    item.TEN_TIEUMUC = reader["TEN_TIEUMUC"].ToString();
                    item.MA_DVQHNS = reader["MA_DVQHNS"].ToString();
                    item.TUCHU = reader["TUCHU"].ToString();
                    if (reader["GIA_TRI_HACH_TOAN"].ToString() != "")
                    {
                        item.GIA_TRI_HACH_TOAN = decimal.Parse(reader["GIA_TRI_HACH_TOAN"].ToString());
                    }
                    //if (item.MA_DIABAN == unit.Value)
                    //{
                    items.Add(item);
                    //}

                }
                connection.Close();

                if (items.Count > 40000)
                {
                    return "OverLoad";
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(urlFileTemplate, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        BindingDataToExcel(workSheet, items, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;

        }

        private void BindingDataToExcel(ExcelWorksheet ws, List<ResultItems> items, ExportParams para)
        {

            var lst = items.Where(x => x.TUCHU == "1").ToList();
            var lst2 = items.Where(x => x.TUCHU == "2").ToList();
            var startRow = 11;
            var dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS = lst.Select(x => x.MA_LOAI).Distinct().ToList();
            //var dvqhnsS2 = lst2.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS2 = lst2.Select(x => x.MA_LOAI).Distinct().ToList();
            ws.Cells[9, 5].Value = "Tổng cộng";
            ws.Cells[9, 5].Style.Font.Bold = true;
            ws.Cells[9, 6].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 6].Style.Font.Bold = true;
            ws.Cells[9, 9].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 9].Style.Font.Bold = true;

            ws.Cells[10, 5].Value = "I. Kinh phí thường xuyên/tự chủ";
            ws.Cells[10, 5].Style.Font.Bold = true;
            ws.Cells[10, 6].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[10, 6].Style.Font.Bold = true;
            ws.Cells[10, 9].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[10, 9].Style.Font.Bold = true;

            for (int i = 0; i < loaiS.Count; i++)
            {
                var l = loaiS[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 9].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = lst.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 9].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 9].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 9].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 9].Style.Numberformat.Format = "###,###,###,###,###";
                        startRow++;
                        var tieumucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 6].Style.Numberformat.Format = "###,###,###,###,###";
                            ws.Cells[startRow + n, 9].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 9].Style.Numberformat.Format = "###,###,###,###,###";


                            for (int o = 0; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[9, 24 + o].Value = items.Where(x => x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[9, 24 + o].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[9, 24 + o].Style.Font.Bold = true;
                                ws.Cells[7, 24 + o].Value = d;
                                ws.Cells[startRow + n, 24 + o].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[startRow + n, 24 + o].Style.Numberformat.Format = "###,###,###,###,###";
                            }
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            /////////////////////--KHÔNG TỰ CHỦ--//////////////////////////////////////////////
            ws.Cells[startRow, 5].Value = "II. Kinh phí không thường xuyên/không tự chủ";
            ws.Cells[startRow, 5].Style.Font.Bold = true;
            ws.Cells[startRow, 6].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 6].Style.Font.Bold = true;
            ws.Cells[startRow, 9].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 9].Style.Font.Bold = true;
            startRow++;
            for (int i = 0; i < loaiS2.Count; i++)
            {
                var l = loaiS2[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 9].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = lst2.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 9].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    startRow++;
                    var mucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 9].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        startRow++;
                        var tieumucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            var col = 9;
                            if (t == "8954")
                            {
                                col = 16;
                            }
                            if (t == "8955")
                            {
                                col = 13;
                            }
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst2.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, col].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);

                            for (int o = 0; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[7, 24 + o].Value = d;
                                ws.Cells[startRow + n, 24 + o].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                            }
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            var tmp_dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Merge = true;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Font.Bold = true;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Ngày hiệu lực từ ngày " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[3, 1].Value = "Ngày kết sổ từ ngày " + para.TUNGAY_KETSO.ToString("d") + " đến ngày " + para.DENNGAY_KETSO.ToString("d");
        }

        [Route("ExportBM2CP1")]
        [AllowAnonymous]
        public HttpResponseMessage ExportBM2CP1(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            file = _CreateExcelFileBM2CP1(para);
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                }
            }

            return result;
        }

        public string _CreateExcelFileBM2CP1(ExportParams para)

        {
            //var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = para.MA_DBHC;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var urlExtensionFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BM1C_PI_Extend.txt";
            string extensionStr = System.IO.File.ReadAllText(urlExtensionFile);
            extensionStr = extensionStr.Replace("\r", "");
            extensionStr = extensionStr.Replace("\n", "");
            if (unit.Length == 3)
            {
                extensionStr += " AND MA_KBNN IN (SELECT MA FROM DM_TKKHOBAC WHERE MA_DBHC LIKE '" + unit + "') ";
            }
            var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BM2CP1.xlsx";
            var urlFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/UploadFile/ExcelTmp/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(urlFileTemplate))
            {
                List<DM_CHITIEU_BAOCAO> items = new List<DM_CHITIEU_BAOCAO>();
                OracleCommand cmd = new OracleCommand();
                OracleDataReader dr;
                cmd.CommandText = @"SELECT MA_CHITIEU,SAPXEP,TEN_CHITIEU,STT,CONGTHUC_WHERE,INDAM,INNGHIENG FROM DM_CHITIEU_BAOCAO WHERE MA_BAOCAO = 'BM1C_PI'";
                cmd.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                cmd.Connection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DM_CHITIEU_BAOCAO dt = new DM_CHITIEU_BAOCAO
                    {
                        MA_CHITIEU = dr["MA_CHITIEU"].ToString(),
                        SAPXEP = dr["SAPXEP"].ToString(),
                        TEN_CHITIEU = dr["TEN_CHITIEU"].ToString(),
                        STT = dr["STT"].ToString(),
                        CONGTHUC_WHERE = dr["CONGTHUC_WHERE"].ToString(),
                        INDAM = int.Parse(dr["INDAM"].ToString()),
                        INNGHIENG = int.Parse(dr["INNGHIENG"].ToString())
                    };
                    items.Add(dt);
                }
                dr.Close();
                cmd.Connection.Close();

                var itemsF = items.Where(x => x.STT != null).OrderBy(y => int.Parse(y.STT)).ToList();
                List<string> s8 = new List<string>(new[] { "16", "17", "18", "19", "20", "21", "41", "42", "43", "44", "45", "53", "54", "55", "56" });
                List<string> crdr = new List<string>(new[] { "31", "32", "35", "46" });
                //List<string> drcr = new List<string>(new[] { "22", "23", "27" });
                List<PHA_HACHTOAN_CHI> result = new List<PHA_HACHTOAN_CHI>();
                foreach (var t in itemsF)
                {
                    if (!string.IsNullOrEmpty(t.CONGTHUC_WHERE))
                    {

                        var tbl = "PHA_DUTOAN";
                        var gtht = "GIA_TRI_HACH_TOAN";
                        if (s8.Contains(t.STT))
                        {
                            tbl = "PHA_HACHTOAN_CHI";
                        }
                        if (!s8.Contains(t.STT))
                        {
                            gtht = "ACCOUNTED_DR - ACCOUNTED_CR";
                        }
                        if (crdr.Contains(t.STT))
                        {
                            gtht = "ACCOUNTED_CR - ACCOUNTED_DR";
                        }
                        OracleCommand cmdt = new OracleCommand();
                        OracleDataReader drt;
                        cmdt.CommandText = string.Format("SELECT MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS,SUM(" + gtht + ") AS GIA_TRI_HACH_TOAN FROM " + tbl + " WHERE ");
                        if (!string.IsNullOrEmpty(para.MA_CAP))
                        {
                            cmdt.CommandText += "MA_CAP = '" + para.MA_CAP + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_CHUONG))
                        {
                            cmdt.CommandText += "MA_CHUONG = '" + para.MA_CHUONG + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_LOAI))
                        {
                            cmdt.CommandText += "MA_LOAI = '" + para.MA_LOAI + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_NGANHKT))
                        {
                            cmdt.CommandText += "MA_NGANHKT = '" + para.MA_NGANHKT + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_MUC))
                        {
                            cmdt.CommandText += "MA_MUC = '" + para.MA_MUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
                        {
                            cmdt.CommandText += "MA_TIEUMUC = '" + para.MA_TIEUMUC + "' AND ";
                        }
                        if (!string.IsNullOrEmpty(para.MA_DVQHNS))
                        {
                            cmdt.CommandText += "MA_DVQHNS IN ('" + para.MA_DVQHNS + "') AND ";
                        }
                        cmdt.CommandText += "TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_HIEU_LUC,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_HIEULUC.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') >= TO_DATE ('" + para.TUNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY')" +
                                                            "AND TO_DATE (NGAY_KET_SO,'DD-MM-YY') <= TO_DATE ('" + para.DENNGAY_KETSO.ToString("dd'/'MM'/'yyyy") + "','DD-MM-YY') " +
                                            extensionStr.Trim() + " AND ";
                        cmdt.CommandText += t.CONGTHUC_WHERE;
                        cmdt.CommandText += " GROUP BY MA_CAP,MA_CHUONG,MA_LOAI,MA_NGANHKT,MA_MUC,MA_TIEUMUC,MA_DVQHNS";
                        cmdt.Connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                        cmdt.Connection.Open();
                        drt = cmdt.ExecuteReader();
                        while (drt.Read())
                        {
                            PHA_HACHTOAN_CHI tmpItem = new PHA_HACHTOAN_CHI();
                            tmpItem.MA_CAP = drt["MA_CAP"].ToString();
                            tmpItem.MA_CHUONG = drt["MA_CHUONG"].ToString();
                            tmpItem.MA_LOAI = drt["MA_LOAI"].ToString();
                            tmpItem.MA_NGANHKT = drt["MA_NGANHKT"].ToString();
                            tmpItem.MA_MUC = drt["MA_MUC"].ToString();
                            tmpItem.MA_TIEUMUC = drt["MA_TIEUMUC"].ToString();
                            tmpItem.MA_DVQHNS = drt["MA_DVQHNS"].ToString();
                            //Gán STT vào SEG12 để group khi xuất file -- Không ảnh hưởng dữ liệu
                            tmpItem.SEGMENT12 = t.STT;
                            if (drt["GIA_TRI_HACH_TOAN"].ToString() != "")
                            {
                                tmpItem.GIA_TRI_HACH_TOAN = decimal.Parse(drt["GIA_TRI_HACH_TOAN"].ToString());
                            }
                            result.Add(tmpItem);
                        }
                        drt.Close();
                        cmdt.Connection.Close();
                    }
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(urlFileTemplate, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcelBM2CP1(workSheet, result, itemsF, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;
        }

        public void _BindingDataToExcelBM2CP1(ExcelWorksheet ws, List<PHA_HACHTOAN_CHI> result, List<DM_CHITIEU_BAOCAO> items, ExportParams para)
        {
            var startRow = 10;
            var startCol = 4;
            for (int i = 0; i < items.Count; i++)
            {
                ws.Cells[startRow + i, 1].Value = items[i].SAPXEP;
                ws.Cells[startRow + i, 2].Value = items[i].TEN_CHITIEU;
                if (items[i].INDAM == 1)
                {
                    ws.Cells[startRow + i, 1].Style.Font.Bold = true;
                    ws.Cells[startRow + i, 2].Style.Font.Bold = true;
                }
                if (items[i].INNGHIENG == 1)
                {
                    ws.Cells[startRow + i, 1].Style.Font.Italic = true;
                    ws.Cells[startRow + i, 2].Style.Font.Italic = true;
                }
                if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                {
                    ws.Cells[startRow + i, 3].Value = result.Where(y => y.SEGMENT12 == items[i].STT).Sum(x => x.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow + i, 3].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }

            var loaitmp = result.GroupBy(x => x.MA_LOAI).Select(y => y.First()).ToList();
            var loaiS = loaitmp.Select(x => x.MA_LOAI).ToList();
            for (int j = 0; j < loaiS.Count; j++)
            {
                var khoanS = result.Where(x => x.MA_LOAI == loaiS[j]).Select(y => y.MA_NGANHKT).Distinct().ToList();
                ws.Cells[7, startCol, 7, startCol + khoanS.Count].Merge = true;
                ws.Cells[7, startCol, 7, startCol + khoanS.Count].Style.Font.Bold = true;
                ws.Cells[7, startCol, 7, startCol + khoanS.Count].Style.WrapText = true;
                ws.Cells[7, startCol, 7, startCol + khoanS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells[7, startCol].Value = "Loại" + loaiS[j];

                ws.Cells[8, startCol].Value = "Tổng loại " + loaiS[j];

                ws.Cells[8, startCol, 8, startCol + 2].Style.Font.Bold = true;
                ws.Cells[8, startCol, 8, startCol + 2].Style.WrapText = true;
                ws.Cells[8, startCol, 8, startCol + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                for (int i = 0; i < items.Count; i++)
                {
                    if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                    {
                        ws.Cells[startRow + i, startCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow + i, startCol].Style.Numberformat.Format = "###,###,###,###,###";
                    }
                }

                for (int k = 0; k < khoanS.Count; k++)
                {
                    var nextCol = startCol + 1 + k;
                    ws.Cells[8, nextCol].Value = "Khoản " + khoanS[k];
                    ws.Cells[8, nextCol].Style.Font.Bold = true;
                    ws.Cells[8, nextCol].Style.WrapText = true;
                    ws.Cells[8, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(items[i].CONGTHUC_WHERE))
                        {
                            ws.Cells[startRow + i, nextCol].Value = result.Where(x => x.MA_LOAI == loaiS[j] && x.MA_NGANHKT == khoanS[k] && x.SEGMENT12 == items[i].STT).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + i, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                    }
                }

                //Gán số cột bằng số cột hiện tại
                startCol += khoanS.Count + 1;
            }

            var lstDvqhns = result.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Merge = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.Font.Bold = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.WrapText = true;
            ws.Cells[7, startCol, 7, startCol + lstDvqhns.Count - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            for (int j = 0; j < lstDvqhns.Count; j++)
            {
                ws.Cells[8, startCol + j].Merge = false;
                ws.Cells[8, startCol + j].Merge = true;
                ws.Cells[8, startCol + j].Value = lstDvqhns[j];
                ws.Cells[8, startCol + j].Style.Font.Bold = true;
                ws.Cells[8, startCol + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                for (int i = 0; i < items.Count; i++)
                {
                    ws.Cells[startRow + i, startCol + j].Value = result.Where(x => x.SEGMENT12 == items[i].STT && x.MA_DVQHNS == lstDvqhns[j]).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow + i, startCol + j].Style.Numberformat.Format = "###,###,###,###,###";
                }
            }
            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Ngày hiệu lực từ ngày " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[3, 1].Value = "Ngày kết sổ từ ngày " + para.TUNGAY_KETSO.ToString("d") + " đến ngày " + para.DENNGAY_KETSO.ToString("d");

            ws.SelectedRange[7, 4, 10, startCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 4, 10, startCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 4, 10, startCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 4, 10, startCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow + items.Count, startCol + lstDvqhns.Count - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        [Route("ExportBM2CP2")]
        [AllowAnonymous]
        public HttpResponseMessage ExportBM2CP2(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            try
            {

                file = CreateExcelFileBM2CP2(para);
                if (file == "OverLoad")
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                    result.Content = new StringContent("OverLoad");
                    return result;
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (!File.Exists(file))
                    {
                        result = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.OK);
                        result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        private string CreateExcelFileBM2CP2(ExportParams para)

        {
            //var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = para.MA_DBHC;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BM2CP2.xlsx";
            var urlFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/UploadFile/ExcelTmp/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(urlFileTemplate))
            {
                List<ResultItems> items = new List<ResultItems>();
                OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BTSTC.PHA_BAOCAO_CHITIETKINHPHIQT";
                command.Parameters.Clear();
                command.Parameters.Add("MA_CAP", OracleDbType.NVarchar2, 100).Value = para.MA_CAP;
                command.Parameters.Add("MA_CHUONG", OracleDbType.NVarchar2, 100).Value = para.MA_CHUONG;
                command.Parameters.Add("MA_LOAI", OracleDbType.NVarchar2, 100).Value = para.MA_LOAI;
                command.Parameters.Add("MA_NGANHKT", OracleDbType.NVarchar2, 100).Value = para.MA_NGANHKT;
                command.Parameters.Add("MA_MUC", OracleDbType.NVarchar2, 100).Value = para.MA_MUC;
                command.Parameters.Add("MA_TIEUMUC", OracleDbType.NVarchar2, 100).Value = para.MA_TIEUMUC;
                command.Parameters.Add("MA_DVQHNS", OracleDbType.NVarchar2, 100).Value = para.MA_DVQHNS;
                command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2, 100).Value = unit;
                command.Parameters.Add("TUNGAY_HIEULUC", OracleDbType.Date).Value = para.TUNGAY_HIEULUC;
                command.Parameters.Add("DENNGAY_HIEULUC", OracleDbType.Date).Value = para.DENNGAY_HIEULUC;
                command.Parameters.Add("TUNGAY_KETSO", OracleDbType.Date).Value = para.TUNGAY_KETSO;
                command.Parameters.Add("DENNGAY_KETSO", OracleDbType.Date).Value = para.DENNGAY_KETSO;
                command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader();
                while (reader.Read())
                {
                    var item = new ResultItems();
                    item.MA_DIABAN = reader["MA_DIABAN"].ToString();
                    item.MA_CAP = reader["MA_CAP"].ToString();
                    item.MA_CHUONG = reader["MA_CHUONG"].ToString();
                    item.MA_LOAI = reader["MA_LOAI"].ToString();
                    item.MA_NGANHKT = reader["MA_NGANHKT"].ToString();
                    item.MA_MUC = reader["MA_MUC"].ToString();
                    item.MA_TIEUMUC = reader["MA_TIEUMUC"].ToString();
                    item.TEN_TIEUMUC = reader["TEN_TIEUMUC"].ToString();
                    item.MA_DVQHNS = reader["MA_DVQHNS"].ToString();
                    item.TUCHU = reader["TUCHU"].ToString();
                    if (reader["GIA_TRI_HACH_TOAN"].ToString() != "")
                    {
                        item.GIA_TRI_HACH_TOAN = decimal.Parse(reader["GIA_TRI_HACH_TOAN"].ToString());
                    }
                    //if (item.MA_DIABAN == unit.Value)
                    //{
                    items.Add(item);
                    //}

                }
                connection.Close();

                if (items.Count > 40000)
                {
                    return "OverLoad";
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(urlFileTemplate, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        BindingDataToExcelBM2CP2(workSheet, items, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;

        }

        private void BindingDataToExcelBM2CP2(ExcelWorksheet ws, List<ResultItems> items, ExportParams para)
        {
            ws.Cells[5, 1, 7, 1].Merge = true;
            ws.Cells[5, 1].Value = "Loại";
            ws.Cells[5, 1].Style.Font.Bold = true;

            ws.Cells[5, 2, 7, 2].Merge = true;
            ws.Cells[5, 2].Value = "Khoản";
            ws.Cells[5, 2].Style.Font.Bold = true;

            ws.Cells[5, 3, 7, 3].Merge = true;
            ws.Cells[5, 3].Value = "Mục";
            ws.Cells[5, 3].Style.Font.Bold = true;

            ws.Cells[5, 4, 7, 4].Merge = true;
            ws.Cells[5, 4].Value = "Tiểu mục";
            ws.Cells[5, 4].Style.Font.Bold = true;

            ws.Cells[5, 5, 7, 5].Merge = true;
            ws.Cells[5, 5].Value = "Nội dung";
            ws.Cells[5, 5].Style.Font.Bold = true;

            ws.Cells[5, 6, 5, 11].Merge = true;
            ws.Cells[5, 6].Value = "Tổng số";
            ws.Cells[5, 6].Style.Font.Bold = true;

            ws.Cells[6, 6, 7, 6].Merge = true;
            ws.Cells[6, 6].Value = "Tổng số";

            ws.Cells[6, 7, 6, 9].Merge = true;
            ws.Cells[6, 7].Value = "Nguồn NSNN";

            ws.Cells[7, 7].Value = "Ngân sách trong nước";
            ws.Cells[7, 8].Value = "Viện trợ";
            ws.Cells[7, 9].Value = "Vay nợ nước ngoài";

            ws.Cells[6, 10, 7, 10].Merge = true;
            ws.Cells[6, 10].Value = "Phí được khấu trừ, để lại";
            ws.Cells[6, 10, 7, 10].Merge = true;
            ws.Cells[6, 10].Value = "Nguồn hoạt động khác được để lại";

            ws.Cells[8, 1].Value = "A";
            ws.Cells[8, 2].Value = "B";
            ws.Cells[8, 3].Value = "C";
            ws.Cells[8, 4].Value = "D";
            ws.Cells[8, 5].Value = "E";
            ws.Cells[8, 6].Value = "1";
            ws.Cells[8, 7].Value = "2";
            ws.Cells[8, 8].Value = "3";
            ws.Cells[8, 9].Value = "4";
            ws.Cells[8, 10].Value = "5";
            ws.Cells[8, 11].Value = "6";

            ws.SelectedRange[5, 1, 7, 11].Style.WrapText = true;
            ws.SelectedRange[5, 1, 7, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.SelectedRange[5, 1, 7, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.SelectedRange[5, 1, 7, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 1, 7, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 1, 7, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 1, 7, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            var lst = items.Where(x => x.TUCHU == "1").ToList();
            var lst2 = items.Where(x => x.TUCHU == "2").ToList();
            var startRow = 11;
            var dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS = lst.Select(x => x.MA_LOAI).Distinct().ToList();
            //var dvqhnsS2 = lst2.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS2 = lst2.Select(x => x.MA_LOAI).Distinct().ToList();
            ws.Cells[9, 5].Value = "Tổng cộng";
            ws.Cells[9, 5].Style.Font.Bold = true;
            ws.Cells[9, 6].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 6].Style.Font.Bold = true;
            ws.Cells[9, 7].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 7].Style.Font.Bold = true;

            ws.Cells[10, 5].Value = "I. Kinh phí thường xuyên/tự chủ";
            ws.Cells[10, 5].Style.Font.Bold = true;
            ws.Cells[10, 6].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[10, 6].Style.Font.Bold = true;
            ws.Cells[10, 7].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[10, 7].Style.Font.Bold = true;

            for (int i = 0; i < loaiS.Count; i++)
            {
                var l = loaiS[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = lst.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                        startRow++;
                        var tieumucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 6].Style.Numberformat.Format = "###,###,###,###,###";
                            ws.Cells[startRow + n, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 7].Style.Numberformat.Format = "###,###,###,###,###";

                            var nextCol = 12;
                            for (int o = 0; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[9, nextCol].Value = items.Where(x => x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[9, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[9, nextCol].Style.Font.Bold = true;
                                ws.Cells[9, nextCol + 1].Value = items.Where(x => x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[9, nextCol + 1].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[9, nextCol + 1].Style.Font.Bold = true;
                                ws.Cells[5, nextCol, 5, nextCol + 5].Merge = true;
                                ws.Cells[5, nextCol, 5, nextCol + 5].Style.Font.Bold = true;
                                ws.Cells[5, nextCol, 5, nextCol + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[5, nextCol].Value = d;
                                ////////////////////
                                ws.Cells[6, nextCol, 7, nextCol].Merge = true;
                                ws.Cells[6, nextCol, 7, nextCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[6, nextCol, 7, nextCol].Style.WrapText = true;
                                ws.Cells[6, nextCol].Value = "Tổng số";

                                ws.Cells[6, nextCol + 1, 6, nextCol + 3].Merge = true;
                                ws.Cells[6, nextCol + 1, 6, nextCol + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[6, nextCol + 1, 6, nextCol + 3].Style.WrapText = true;
                                ws.Cells[6, nextCol + 1].Value = "Nguồn NSNN";

                                ws.Cells[7, nextCol + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[7, nextCol + 1].Style.WrapText = true;
                                ws.Cells[7, nextCol + 1].Value = "Ngân sách trong nước";
                                ws.Cells[7, nextCol + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[7, nextCol + 2].Style.WrapText = true;
                                ws.Cells[7, nextCol + 2].Value = "Viện trợ";
                                ws.Cells[7, nextCol + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[7, nextCol + 3].Style.WrapText = true;
                                ws.Cells[7, nextCol + 3].Value = "Vay nợ nước ngoài";

                                ws.Cells[6, nextCol + 4, 7, nextCol + 4].Merge = true;
                                ws.Cells[6, nextCol + 4, 7, nextCol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[6, nextCol + 4, 7, nextCol + 4].Style.WrapText = true;
                                ws.Cells[6, nextCol + 4].Value = "Phí được khấu trừ, để lại";

                                ws.Cells[6, nextCol + 5, 7, nextCol + 5].Merge = true;
                                ws.Cells[6, nextCol + 5, 7, nextCol + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[6, nextCol + 5, 7, nextCol + 5].Style.WrapText = true;
                                ws.Cells[6, nextCol + 5].Value = "Nguồn hoạt động khác được để lại";
                                ////////////////////
                                ws.Cells[startRow + n, nextCol].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[startRow + n, nextCol].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[startRow + n, nextCol + 1].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[startRow + n, nextCol + 1].Style.Numberformat.Format = "###,###,###,###,###";
                                nextCol += 6;
                            }
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            /////////////////////--KHÔNG TỰ CHỦ--//////////////////////////////////////////////
            ws.Cells[startRow, 5].Value = "II. Kinh phí không thường xuyên/không tự chủ";
            ws.Cells[startRow, 5].Style.Font.Bold = true;
            ws.Cells[startRow, 6].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 6].Style.Font.Bold = true;
            ws.Cells[startRow, 7].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 7].Style.Font.Bold = true;
            startRow++;
            for (int i = 0; i < loaiS2.Count; i++)
            {
                var l = loaiS2[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = lst2.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    startRow++;
                    var mucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        startRow++;
                        var tieumucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            var col = 9;
                            if (t == "8954")
                            {
                                col = 16;
                            }
                            if (t == "8955")
                            {
                                col = 13;
                            }
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst2.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, col].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);

                            for (int o = 0; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[5, 12 + o].Value = d;
                                ws.Cells[startRow + n, 12 + o].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                            }
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            var tmp_dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.SelectedRange[9, 1, startRow - 1, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[5, 12, 7, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 12, 7, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 12, 7, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[5, 12, 7, 12 + (tmp_dvqhnsS.Count * 6)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Merge = true;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Font.Bold = true;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Ngày hiệu lực từ ngày " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[3, 1].Value = "Ngày kết sổ từ ngày " + para.TUNGAY_KETSO.ToString("d") + " đến ngày " + para.DENNGAY_KETSO.ToString("d");
        }


        [Route("Export3BPII")]
        [AllowAnonymous]
        public HttpResponseMessage Export3BPII(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            try
            {

                file = CreateExcelFileBPII(para);
                if (file == "OverLoad")
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                    result.Content = new StringContent("OverLoad");
                    return result;
                }
                if (!string.IsNullOrEmpty(file))
                {
                    if (!File.Exists(file))
                    {
                        result = Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.OK);
                        result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                        result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        private string CreateExcelFileBPII(ExportParams para)

        {
            //var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = para.MA_DBHC;
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/Template/BC_CHITIET_QUYETTOAN.xlsx";
            var urlFile = "C:/inetpub/wwwroot/wss/VirtualDirectories/8787/UploadFile/ExcelTmp/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(urlFileTemplate))
            {
                List<ResultItems> items = new List<ResultItems>();
                OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BTSTC.PHA_BAOCAO_CHITIETKINHPHIQT";
                command.Parameters.Clear();
                command.Parameters.Add("MA_CAP", OracleDbType.NVarchar2, 100).Value = para.MA_CAP;
                command.Parameters.Add("MA_CHUONG", OracleDbType.NVarchar2, 100).Value = para.MA_CHUONG;
                command.Parameters.Add("MA_LOAI", OracleDbType.NVarchar2, 100).Value = para.MA_LOAI;
                command.Parameters.Add("MA_NGANHKT", OracleDbType.NVarchar2, 100).Value = para.MA_NGANHKT;
                command.Parameters.Add("MA_MUC", OracleDbType.NVarchar2, 100).Value = para.MA_MUC;
                command.Parameters.Add("MA_TIEUMUC", OracleDbType.NVarchar2, 100).Value = para.MA_TIEUMUC;
                command.Parameters.Add("MA_DVQHNS", OracleDbType.NVarchar2, 100).Value = para.MA_DVQHNS;
                command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2, 100).Value = unit;
                command.Parameters.Add("TUNGAY_HIEULUC", OracleDbType.Date).Value = para.TUNGAY_HIEULUC;
                command.Parameters.Add("DENNGAY_HIEULUC", OracleDbType.Date).Value = para.DENNGAY_HIEULUC;
                command.Parameters.Add("TUNGAY_KETSO", OracleDbType.Date).Value = para.TUNGAY_KETSO;
                command.Parameters.Add("DENNGAY_KETSO", OracleDbType.Date).Value = para.DENNGAY_KETSO;
                command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader();
                while (reader.Read())
                {
                    var item = new ResultItems();
                    item.MA_DIABAN = reader["MA_DIABAN"].ToString();
                    item.MA_CAP = reader["MA_CAP"].ToString();
                    item.MA_CHUONG = reader["MA_CHUONG"].ToString();
                    item.MA_LOAI = reader["MA_LOAI"].ToString();
                    item.MA_NGANHKT = reader["MA_NGANHKT"].ToString();
                    item.MA_MUC = reader["MA_MUC"].ToString();
                    item.MA_TIEUMUC = reader["MA_TIEUMUC"].ToString();
                    item.TEN_TIEUMUC = reader["TEN_TIEUMUC"].ToString();
                    item.MA_DVQHNS = reader["MA_DVQHNS"].ToString();
                    item.TUCHU = reader["TUCHU"].ToString();
                    if (reader["GIA_TRI_HACH_TOAN"].ToString() != "")
                    {
                        item.GIA_TRI_HACH_TOAN = decimal.Parse(reader["GIA_TRI_HACH_TOAN"].ToString());
                    }
                    //if (item.MA_DIABAN == unit.Value)
                    //{
                    items.Add(item);
                    //}

                }
                connection.Close();

                if (items.Count > 40000)
                {
                    return "OverLoad";
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(urlFileTemplate, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        BindingDataToExcel3BP2(workSheet, items, para);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }

            }

            else
            {
                filename = "";
            }

            return filename;

        }

        private void BindingDataToExcel3BP2(ExcelWorksheet ws, List<ResultItems> items, ExportParams para)
        {

            
            var startRow = 11;
            var dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS = items.Select(x => x.MA_LOAI).Distinct().ToList();
            //var dvqhnsS2 = lst2.Select(x => x.MA_DVQHNS).Distinct().ToList();
            
            ws.Cells[9, 5].Value = "Tổng cộng";
            ws.Cells[9, 5].Style.Font.Bold = true;
            ws.Cells[9, 6].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 6].Style.Font.Bold = true;
            ws.Cells[9, 9].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[9, 9].Style.Font.Bold = true;


            for (int i = 0; i < loaiS.Count; i++)
            {
                var l = loaiS[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 9].Value = items.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = items.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 9].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 9].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = items.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 9].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 9].Style.Numberformat.Format = "###,###,###,###,###";
                        startRow++;
                        var tieumucS = items.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = items.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 6].Style.Numberformat.Format = "###,###,###,###,###";
                            ws.Cells[startRow + n, 9].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 9].Style.Numberformat.Format = "###,###,###,###,###";


                            for (int o = 0; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[9, 24 + o].Value = items.Where(x => x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[9, 24 + o].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[9, 24 + o].Style.Font.Bold = true;
                                ws.Cells[7, 24 + o].Value = d;
                                ws.Cells[startRow + n, 24 + o].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[startRow + n, 24 + o].Style.Numberformat.Format = "###,###,###,###,###";
                            }
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
           
            var tmp_dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[9, 1, startRow - 1, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[7, 24, 7, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Merge = true;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Font.Bold = true;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[5, 24, 6, 23 + tmp_dvqhnsS.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Ngày hiệu lực từ ngày " + para.TUNGAY_HIEULUC.ToString("d") + " đến ngày " + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[3, 1].Value = "Ngày kết sổ từ ngày " + para.TUNGAY_KETSO.ToString("d") + " đến ngày " + para.DENNGAY_KETSO.ToString("d");
        }
    }
}