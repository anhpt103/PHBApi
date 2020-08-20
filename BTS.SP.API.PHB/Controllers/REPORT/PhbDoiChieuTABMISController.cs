using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Xml;
using System.Data;
using BTS.SP.API.ENTITY;
using Oracle.ManagedDataAccess.Client;
using BTS.SP.API.ENTITY.Models.Nv.PHC;
using BTS.SP.PHB.SERVICE.REPORT.DoiChieuTABMIS;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.PHB.SERVICE.TienIch;
using BTS.SP.PHB.SERVICE.BuildQuery.Result;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/doichieuTABMIS")]
    [Route("{id?}")]
    [Authorize]
    public class PhbDoiChieuTABMISController : ApiController
    {
        private readonly IDoiChieuTABMISService _service;
        public PhbDoiChieuTABMISController(IDoiChieuTABMISService service)
        {
            _service = service;
        }
        public DoiChieuSoLieuVm.ConvertPeriod ConvertDateByPeriod(int Year,int Month)
        {
            DoiChieuSoLieuVm.ConvertPeriod resultTime = new DoiChieuSoLieuVm.ConvertPeriod();
            resultTime.TUNGAY_HIEULUC = new DateTime(Year, Month, 1);
            resultTime.DENNGAY_HIEULUC = resultTime.TUNGAY_HIEULUC.AddMonths(1).AddDays(-1);
            return resultTime;
        }
        public DoiChieuSoLieuVm.Data_Header_XML ConvertFromFileName(string fileName)
        {
            DoiChieuSoLieuVm.Data_Header_XML data = new DoiChieuSoLieuVm.Data_Header_XML();
            fileName = fileName.Substring(0, fileName.IndexOf('.'));
            string[] words = fileName.Split('_');
            data.MADVQHNS_FILE = words[0];
            if (words[1] is string)
            {
                data.KY_FILE = Int32.Parse(words[2]);
            }
            else
            {
                data.KY_FILE = Int32.Parse(words[1]);
            }
            data.NAM_FILE = Int32.Parse(words[2]);
            return data;
        }
        [Route("Select_Page")]
        [HttpPost]
        public async Task<IHttpActionResult> Select_Page(JObject jsonData)
        {
            var result = new TransferObj();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DoiChieuSoLieuVm.Search>>();
            if (filtered.AdvanceData.LOAIDULIEU == null)
            {
                result.Status = false;
                result.Message = "Chưa chọn loại báo cáo !";
                return Ok(result);
            }
            if (filtered.AdvanceData.KY_BC == null)
            {
                result.Status = false;
                result.Message = "Chưa chọn kỳ báo cáo !";
                return Ok(result);
            }
            if (filtered.AdvanceData.MA_DBHC == null)
            {
                result.Status = false;
                result.Message = "Chưa chọn Mã địa bàn hành chính !";
                return Ok(result);
            }
            if (filtered.AdvanceData.LOAIDULIEU == "0") //THU
            {
                try
                {
                    List<DoiChieuSoLieuVm.ViewModelB03A_X> lst = new List<DoiChieuSoLieuVm.ViewModelB03A_X>();
                    int KyBaoCao = Int32.Parse(filtered.AdvanceData.KY_BC);
                    using (var connection = new OracleConnection(new STCContext().Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.Text;
                            command.CommandText =
                                @"SELECT REFID,MA_CHUONG,MA_QHNS,NAM_BC,KY_BC,NGAY_TAO,TRANG_THAI,MA_DBHC,MA_DBHC_CHA,TEN_QHNS FROM PHB_C_B03A_X WHERE MA_DBHC = '" + filtered.AdvanceData.MA_DBHC + "' AND KY_BC = " + KyBaoCao + " ";
                            using (OracleDataReader oracleDataReader =
                                command.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (!oracleDataReader.HasRows)
                                {
                                    lst = new List<DoiChieuSoLieuVm.ViewModelB03A_X>();
                                }
                                else
                                {
                                    while (oracleDataReader.Read())
                                    {
                                        DoiChieuSoLieuVm.ViewModelB03A_X item = new DoiChieuSoLieuVm.ViewModelB03A_X()
                                        {
                                            REFID = oracleDataReader["REFID"].ToString(),
                                            MA_CHUONG = oracleDataReader["MA_CHUONG"].ToString(),
                                            MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                            NAM_BC = int.Parse(oracleDataReader["NAM_BC"].ToString()),
                                            MA_DBHC = oracleDataReader["MA_DBHC"].ToString(),
                                            MA_DBHC_CHA = oracleDataReader["MA_DBHC_CHA"].ToString(),
                                            TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString(),
                                            KY_BC = int.Parse(oracleDataReader["KY_BC"].ToString()),
                                            LOAIDULIEU = "0"
                                        };
                                        lst.Add(item);
                                    }
                                }
                            }
                        }
                    }
                    result.Status = true;
                    result.Data = lst;
                }
                catch (Exception ex)
                {
                    result.Message = ex.ToString();
                    return InternalServerError();
                }
            }
            else if (filtered.AdvanceData.LOAIDULIEU == "1")//CHI
            {
                try
                {
                    List<DoiChieuSoLieuVm.ViewModelB03B_X> lst = new List<DoiChieuSoLieuVm.ViewModelB03B_X>();
                    using (var connection = new OracleConnection(new STCContext().Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandType = CommandType.Text;
                            command.CommandText =
                                @"SELECT REFID,MA_CHUONG,MA_QHNS,NAM_BC,KY_BC,NGAY_TAO,TRANG_THAI,MA_DBHC,MA_DBHC_CHA,TEN_QHNS FROM PHB_C_B03B_X WHERE MA_DBHC = '" + filtered.AdvanceData.MA_DBHC + "' AND KY_BC = " + filtered.AdvanceData.KY_BC + "";
                            using (OracleDataReader oracleDataReader =
                                command.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (!oracleDataReader.HasRows)
                                {
                                    lst = new List<DoiChieuSoLieuVm.ViewModelB03B_X>();
                                }
                                else
                                {
                                    while (oracleDataReader.Read())
                                    {
                                        DoiChieuSoLieuVm.ViewModelB03B_X item = new DoiChieuSoLieuVm.ViewModelB03B_X()
                                        {
                                            REFID = oracleDataReader["REFID"].ToString(),
                                            MA_CHUONG = oracleDataReader["MA_CHUONG"].ToString(),
                                            MA_QHNS = oracleDataReader["MA_QHNS"].ToString(),
                                            NAM_BC = int.Parse(oracleDataReader["NAM_BC"].ToString()),
                                            MA_DBHC = oracleDataReader["MA_DBHC"].ToString(),
                                            MA_DBHC_CHA = oracleDataReader["MA_DBHC_CHA"].ToString(),
                                            TEN_QHNS = oracleDataReader["TEN_QHNS"].ToString(),
                                            LOAIDULIEU = "1",
                                            KY_BC = int.Parse(oracleDataReader["KY_BC"].ToString()),
                                        };
                                        lst.Add(item);
                                    }
                                }
                            }
                        }
                    }
                    result.Status = true;
                    result.Data = lst;
                }
                catch (Exception ex)
                {
                    result.Message = ex.ToString();
                    return Ok(result);

                }
            }
            else
            {
                result.Status = false;
                result.Data = null;
                return BadRequest();
            }
            return Ok(result);
        }
        public string DonViToDiaBanHanhChinh(string maDonViQuanHe)
        {
            using (var context = new STCContext())
            {
                var data = context.SYS_DVQHNS.FirstOrDefault(x => x.MA_DVQHNS == maDonViQuanHe);
                if (data != null)
                {
                    var obj = context.DM_DBHCs.FirstOrDefault(x => x.MA_T == data.MA_TINH && x.MA_H == data.MA_HUYEN && x.MA_X == data.MA_XA);
                    if (obj != null)
                    {
                        return obj.MA_DBHC;
                    }
                }
                else
                {
                    return "";
                }
            }
            return "";
        }
        //[Route("ReceiveDataMiSa")]
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IHttpActionResult> ReceiveDataMiSa()
        //{
        //    var context = new STCContext();
        //    List<DoiChieuSoLieuVm.DoiChieuSoLieuDetails> listDoiChieuDetails = new List<DoiChieuSoLieuVm.DoiChieuSoLieuDetails>();
        //    string path = _service.XmlMapPath();

        //    HttpRequest request = HttpContext.Current.Request;
        //    if (request.Files.Count > 0)
        //    {
        //        try
        //        {
        //            HttpPostedFile file = request.Files[0];
        //            new FileInfo(path).Directory.Create();
        //            string filename = path + file.FileName + "_(" + DateTime.Now.ToString("dd-MM-yyyy") + ")_MISA" + DateTime.Now.Ticks + ".xml";
        //            file.SaveAs(filename);
        //            if (File.Exists(filename))
        //            {
        //                XmlDocument xmlDoc = new XmlDocument();
        //                xmlDoc.Load(filename);
        //                XmlElement root = xmlDoc.DocumentElement;
        //                XmlNodeList nodeList = root.GetElementsByTagName("SummaryB03abX");
        //                foreach (XmlNode node in nodeList)
        //                {
        //                    try
        //                    {
        //                        DoiChieuSoLieuVm.DoiChieuSoLieuDetails doiChieuDetails = new DoiChieuSoLieuVm.DoiChieuSoLieuDetails();
        //                        if (node["ReportID"].InnerText == "B03a-X") //nếu là chi niên độ B303 - Báo cáo quyết toán chi ngân sách xã theo mục lục NSNN
        //                        {
        //                            doiChieuDetails.DVQHNS = node["BranchCode"].InnerText;
        //                            doiChieuDetails.LOAIDULIEU = "MISA";
        //                            doiChieuDetails.MAQUY = "";
        //                            doiChieuDetails.MATAIKHOAN = "";
        //                            doiChieuDetails.CAP = 4;
        //                            doiChieuDetails.DBHC = DonViToDiaBanHanhChinh(doiChieuDetails.DVQHNS);
        //                            doiChieuDetails.CHUONG = node["BudgetChapterCode"].InnerText;
        //                            doiChieuDetails.LOAI = node["BudgetKindItemCode"].InnerText;
        //                            doiChieuDetails.KHOAN = node["BudgetSubKindItemCode"].InnerText;
        //                            doiChieuDetails.MUC = node["BudgetItemCode"].InnerText;
        //                            doiChieuDetails.TIEUMUC = node["BudgetSubItemCode"].InnerText;
        //                            doiChieuDetails.NHOM = "";
        //                            doiChieuDetails.TIEUNHOM = "";
        //                            doiChieuDetails.CTMT = "";
        //                            doiChieuDetails.KBNN = "";
        //                            doiChieuDetails.MANGUONVON = "";
        //                            doiChieuDetails.SOTIEN = decimal.Parse(node["Amount"].InnerText);
        //                            doiChieuDetails.NAM = Int32.Parse(node["ReportYear"].InnerText);
        //                            doiChieuDetails.KY = Int32.Parse(node["ReportPeriod"].InnerText);
        //                            doiChieuDetails.TUNGAY_HIEULUC = ConvertDateByPeriod(doiChieuDetails.NAM, doiChieuDetails.KY).TUNGAY_HIEULUC;
        //                            doiChieuDetails.DENNGAY_HIEULUC = ConvertDateByPeriod(doiChieuDetails.NAM, doiChieuDetails.KY).DENNGAY_HIEULUC;
        //                            doiChieuDetails.NGAYPHATSINH = DateTime.Now;
        //                            doiChieuDetails.LOAICHUNGTU = "T";
        //                        }
        //                        else if (node["ReportID"].InnerText == "B03b-X") //nếu là thu vay B202 - Báo cáo quyết toán thu ngân sách xã theo mục lục NSNN
        //                        {
        //                            doiChieuDetails.DVQHNS = node["BranchCode"].InnerText;
        //                            doiChieuDetails.LOAIDULIEU = "MISA";
        //                            doiChieuDetails.MAQUY = "";
        //                            doiChieuDetails.MATAIKHOAN = "";
        //                            doiChieuDetails.CAP = 4;
        //                            doiChieuDetails.DBHC = DonViToDiaBanHanhChinh(doiChieuDetails.DVQHNS);
        //                            doiChieuDetails.CHUONG = node["BudgetChapterCode"].InnerText;
        //                            doiChieuDetails.LOAI = node["BudgetKindItemCode"].InnerText;
        //                            doiChieuDetails.KHOAN = node["BudgetSubKindItemCode"].InnerText;
        //                            doiChieuDetails.MUC = node["BudgetItemCode"].InnerText;
        //                            doiChieuDetails.TIEUMUC = node["BudgetSubItemCode"].InnerText;
        //                            doiChieuDetails.NHOM = "";
        //                            doiChieuDetails.TIEUNHOM = "";
        //                            doiChieuDetails.CTMT = "";
        //                            doiChieuDetails.KBNN = "";
        //                            doiChieuDetails.MANGUONVON = "";
        //                            doiChieuDetails.SOTIEN = decimal.Parse(node["Amount"].InnerText);
        //                            doiChieuDetails.NAM = Int32.Parse(node["ReportYear"].InnerText);
        //                            doiChieuDetails.KY = Int32.Parse(node["ReportPeriod"].InnerText);
        //                            doiChieuDetails.TUNGAY_HIEULUC = ConvertDateByPeriod(doiChieuDetails.NAM, doiChieuDetails.KY).TUNGAY_HIEULUC;
        //                            doiChieuDetails.DENNGAY_HIEULUC = ConvertDateByPeriod(doiChieuDetails.NAM, doiChieuDetails.KY).DENNGAY_HIEULUC;
        //                            doiChieuDetails.NGAYPHATSINH = DateTime.Now;
        //                            doiChieuDetails.LOAICHUNGTU = "C";
        //                        }
        //                        listDoiChieuDetails.Add(doiChieuDetails);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                    }
        //                }
        //                if (listDoiChieuDetails.Count > 0)
        //                {
        //                    List<DoiChieuSoLieuVm.DoiChieuSoLieuDto> doiChieuDto = new List<DoiChieuSoLieuVm.DoiChieuSoLieuDto>();
        //                    PHC_DOICHIEUSOLIEUHEADER dataHeader = new PHC_DOICHIEUSOLIEUHEADER();
        //                    var result = new TransferObj<PHC_CHUNGTUDETAILS>();
        //                    try
        //                    {
        //                        await _service.UnitOfWork.SaveAsync();
        //                        var strQuery = @"SELECT A.DVQHNS,A.LOAIDULIEU,SUM(A.SOTIEN) AS SOTIEN FROM PHC_DOICHIEUSOLIEUDETAILS a GROUP BY A.DVQHNS,A.LOAIDULIEU";
        //                        var data = context.Database.SqlQuery<DoiChieuSoLieuVm.DoiChieuSoLieuDto>(strQuery);
        //                        doiChieuDto = data.ToList();
        //                        if (doiChieuDto.Count > 0)
        //                        {
        //                            foreach (var value in doiChieuDto)
        //                            {
        //                                value.TENFILE = file.FileName;
        //                                value.DUONGDAN = path + file.FileName;
        //                                value.KY = ConvertFromFileName(file.FileName).KY_FILE;
        //                                value.NAM = ConvertFromFileName(file.FileName).NAM_FILE;
        //                                if (value.KY == value.NAM)
        //                                {
        //                                    value.TUNGAY_HIEULUC = ConvertDateByPeriod(value.NAM, 1).TUNGAY_HIEULUC;
        //                                    value.DENNGAY_HIEULUC = ConvertDateByPeriod(value.NAM, 12).DENNGAY_HIEULUC;
        //                                }
        //                                else
        //                                {
        //                                    value.TUNGAY_HIEULUC = ConvertDateByPeriod(value.NAM, value.KY).TUNGAY_HIEULUC;
        //                                    value.DENNGAY_HIEULUC = ConvertDateByPeriod(value.NAM, value.KY).DENNGAY_HIEULUC;
        //                                }
        //                                value.NGAYPHATSINH = DateTime.Now;
        //                            }
        //                            await _service.UnitOfWork.SaveAsync();
        //                        }
        //                        result.Status = true;
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        return InternalServerError();
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return InternalServerError();
        //        }
        //    }
        //    return Ok();
        //}
    }
}