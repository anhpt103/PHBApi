using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;
using BTS.SP.API.PHB.Utils;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.REPORT;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.UnitOfWork;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.REPORT.B01B_TT137;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/dsBaoCao")]
    [Route("{id?}")]
    [Authorize]
    public class PhbDsBaoCaoController : ApiController
    {
        private IPhbReportFieldService _phbReportFieldService;
        private readonly IDmDVQHNSService _dmDVQHNSService;
        private ISysDvqhns_QuanLyService _sysDvqhns_QuanLyService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IPhbB01bTT137Service _bmB01bTT137Service;
        private IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbDsBaoCaoController(IPhbReportFieldService phbReportFieldService, IDmDVQHNSService dmDVQHNSService, ISysDvqhns_QuanLyService sysDvqhns_QuanLyService,
            IPhbB01bTT137Service bmB01bTT137Service, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhnsService sysDvqhnsService)
        {
            _phbReportFieldService = phbReportFieldService;
            _dmDVQHNSService = dmDVQHNSService;
            _sysDvqhns_QuanLyService = sysDvqhns_QuanLyService;
            _sysDVQHNSService = sysDvqhnsService;
            _bmB01bTT137Service = bmB01bTT137Service;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region UploadReport
        [Route("UploadReport")]
        [HttpPost]
        public IHttpActionResult UploadReport()
        {
            var result = new Response<object>();
            try
            {
                var httpcontex = HttpContext.Current;
                var username = httpcontex.User.Identity.Name;
                var httpRequest = httpcontex.Request;
                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    string MA_BAO_CAO = httpRequest.Form["MA_BAO_CAO"];
                    string MA_BAO_CAO_XML = MA_BAO_CAO.Replace("PHB_", "");
                    string MA_TEMPLATE = httpRequest.Form["MA_TEMPLATE"];
                    XmlDocument doc = new XmlDocument();
                    doc.Load(file.InputStream);
                    #region getReportHeader
                    XmlNodeList xmlNodeList = doc.GetElementsByTagName("ReportHeader");
                    List<string> Org_Header = new List<string>();
                    List<string> Rep_Header = new List<string>();
                    List<string> Org_Fields = new List<string>();
                    List<string> Rep_Fields = new List<string>();
                    try
                    {
                        var obj =
                            _phbReportFieldService.Queryable().FirstOrDefault(x => x.MA_BAO_CAO.Equals(MA_BAO_CAO));
                        if (obj != null)
                        {
                            Org_Header = obj.HEADER_XML_FIELD.Split(',').ToList();
                            Rep_Header = obj.HEADER_REPORT_FIELD.Split(',').ToList();
                            Org_Fields = obj.DATA_XML_FIELD.Split(',').ToList();
                            Rep_Fields = obj.DATA_REPORT_FIELD.Split(',').ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Rep_Header = null;
                        Org_Header = null;
                        Org_Fields = null;
                        Rep_Fields = null;
                    }
                    if (Org_Header != null && Org_Header.Count > 0)
                    {
                        Dictionary<string, object> dicHeader = new Dictionary<string, object>();
                        if (xmlNodeList.Count > 0)
                        {
                            XmlNode node = xmlNodeList.Item(0);
                            foreach (XmlElement element in node.ChildNodes)
                            {
                                var index = Org_Header.IndexOf(element.Name);
                                if (index != -1)
                                {
                                    dicHeader.Add(Rep_Header[index], "N'" + element.InnerText + "'");
                                }
                            }
                        }
                        XmlNodeList nodeListData = doc.GetElementsByTagName(MA_BAO_CAO_XML + "Data"); // detail
                        List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
                        foreach (XmlNode nodeData in nodeListData)
                        {
                            Dictionary<string, object> data = new Dictionary<string, object>();
                            foreach (XmlElement element in nodeData.ChildNodes)
                            {
                                var index = Org_Fields.IndexOf(element.Name);
                                if (index != -1)
                                {
                                    data.Add(Rep_Fields[index], "N'" + element.InnerText + "'");
                                }
                            }
                            lst.Add(data);
                        }
                        if (lst.Count > 0)
                        {
                            using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                            {
                                connection.Open();
                                var command = connection.CreateCommand();
                                OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                                command.Transaction = transaction;
                                try
                                {
                                    string newId = "'" + Guid.NewGuid().ToString("N").ToUpper() + "'";
                                    Dictionary<string, object>.ValueCollection valueColl = dicHeader.Values;
                                    if (string.IsNullOrEmpty(MA_TEMPLATE))
                                    {
                                        command.CommandText = "INSERT INTO " + MA_BAO_CAO + "(REFID,TRANG_THAI,NGAY_TAO,NGUOI_TAO," + String.Join(",", Rep_Header) + ") VALUES(" + newId + ",0,SYSDATE,N'" + username + "'," + String.Join(",", valueColl) + ")";
                                    }
                                    else
                                    {
                                        command.CommandText = "INSERT INTO " + MA_BAO_CAO + "(REFID,MA_TEMPLATE,TRANG_THAI,NGAY_TAO,NGUOI_TAO," + String.Join(",", Rep_Header) + ") VALUES(" + newId + ",'" + MA_TEMPLATE + "',0,SYSDATE,N'" + username + "'," + String.Join(",", valueColl) + ")";
                                    }
                                    command.ExecuteNonQuery();
                                    #region insert chitiet
                                    string buildQuerry = "";
                                    foreach (var row in lst)
                                    {
                                        Dictionary<string, object>.KeyCollection keyRow = row.Keys;
                                        Dictionary<string, object>.ValueCollection dataRow = row.Values;
                                        buildQuerry += " INTO " + MA_BAO_CAO + "_DETAIL(" + MA_BAO_CAO + "_REFID," + String.Join(",", keyRow) + ") VALUES(" + newId + "," + String.Join(",", dataRow) + ") ";
                                    }
                                    buildQuerry += " SELECT * FROM dual";
                                    command.CommandText = "INSERT ALL " + buildQuerry;
                                    command.Parameters.Clear();
                                    command.ExecuteNonQuery();
                                    #endregion
                                    transaction.Commit();
                                    result.Error = false;
                                    result.Message = "Cập nhật thành công.";
                                    result.Data = "OK";
                                    return Ok(result);
                                }
                                catch (Exception e)
                                {
                                    transaction.Rollback();
                                    result.Error = true;
                                    result.Message = "Lỗi trong quá trình cập nhật.";
                                    result.Data = "ERROR";
                                    return Ok(result);
                                }
                            }
                        }
                    }
                    else
                    {
                        result.Error = true;
                        result.Message = "Cấu hình chưa khai báo.";
                        result.Data = "ERROR";
                        return Ok(result);
                    }
                    #endregion
                }
                result.Error = true;
                result.Message = "Dữ liệu trống.";
                result.Data = "ERROR";
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        #endregion

        #region UploadXml

        [Route("UploadXml")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadXml()
        {
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 0) return Ok(new Response(true, ErrorMessage.EMPTY_DATA));
            var file = httpRequest.Files[0];
            try
            {
                var lstTemplateBc = await _phbReportFieldService.Queryable().Where(x => x.MA_BAO_CAO.Equals("UPLOAD")).ToListAsync();
                if (lstTemplateBc == null || lstTemplateBc.Count == 0)
                    return Ok(new Response(true, ErrorMessage.ERROR_SYSTEM));
                var doc = new XmlDocument();
                doc.Load(file.InputStream);
                var lstResponseRp = new List<Response>();
                #region ImportB01X
                var nodeListDataB01X = doc.GetElementsByTagName("SummaryB01X");
                if (nodeListDataB01X.Count > 0)
                {
                    var lstB01XDetails = (from XmlNode nodeData in nodeListDataB01X
                                          select new B01XImportModel()
                                          {
                                              MA_QHNS = nodeData["BranchCode"]?.InnerText ?? "",
                                              KY_BC = nodeData["ReportPeriod"] != null ? int.Parse(nodeData["ReportPeriod"].InnerText) : 13,
                                              NAM_BC = nodeData["ReportYear"] != null ? int.Parse(nodeData["ReportYear"].InnerText) : 0,
                                              MA_TAIKHOAN = nodeData["AccountNumber"]?.InnerText ?? "",
                                              TEN_TAIKHOAN = nodeData["AccountName"]?.InnerText ?? "",
                                              SDDK_NO = nodeData["OpeningDebit"] != null ? double.Parse(nodeData["OpeningDebit"].InnerText) : 0,
                                              SDDK_CO = nodeData["OpeningCredit"] != null ? double.Parse(nodeData["OpeningCredit"].InnerText) : 0,
                                              PSTK_NO = nodeData["MovementDebit"] != null ? double.Parse(nodeData["MovementDebit"].InnerText) : 0,
                                              PSTK_CO = nodeData["MovementCredit"] != null ? double.Parse(nodeData["MovementCredit"].InnerText) : 0,
                                              LUYKE_NO = nodeData["MovementAccumDebit"] != null ? double.Parse(nodeData["MovementAccumDebit"].InnerText) : 0,
                                              LUYKE_CO = nodeData["MovementAccumCredit"] != null ? double.Parse(nodeData["MovementAccumCredit"].InnerText) : 0,
                                              SDCK_NO = nodeData["ClosingDebit"] != null ? double.Parse(nodeData["ClosingDebit"].InnerText) : 0,
                                              SDCK_CO = nodeData["ClosingCredit"] != null ? double.Parse(nodeData["ClosingCredit"].InnerText) : 0
                                          }).ToList();
                    if (lstB01XDetails.Count > 0)
                    {
                        var machuong = "";
                        var tenqhns = "";
                        var madbhc = "";
                        var madbhccha = "";
                        var groups = lstB01XDetails.GroupBy(x => x.KY_BC);
                        var isCheck = false;
                        var checkMaQhns = false;
                        foreach (var group in groups)
                        {
                            using (var connection =
                                new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"]
                                    .ConnectionString))
                            {
                                await connection.OpenAsync();
                                using (var command = connection.CreateCommand())
                                {
                                    var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                                    command.Transaction = transaction;
                                    var lstResponseB01X = new List<Response>();
                                    var lstDetail = group.ToList();
                                    if (lstDetail.Count > 0)
                                    {
                                        command.CommandType = CommandType.Text;
                                        command.CommandText = "SELECT COUNT(REFID) FROM PHB_C_B01X WHERE MA_QHNS=:maqhns AND NAM_BC=:nambc AND KY_BC=:kybc";
                                        command.Parameters.Clear();
                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value = lstDetail[0].MA_QHNS;
                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value = lstDetail[0].NAM_BC;
                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value = lstDetail[0].KY_BC;
                                        var result = await command.ExecuteScalarAsync();
                                        if (int.Parse(result.ToString()) > 0)
                                        {
                                            //bỏ qua insert
                                            lstResponseB01X.Add(new Response(true, "B01X đã tồn tại báo cáo kỳ " + lstDetail[0].KY_BC));
                                        }
                                        else
                                        {
                                            //check mã dvqhns
                                            if (!isCheck)
                                            {
                                                isCheck = true;
                                                command.CommandText = "SELECT * FROM SYS_DVQHNS WHERE MA_DVQHNS=:maqhns";
                                                command.Parameters.Clear();
                                                command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                    lstDetail[0].MA_QHNS;
                                                using (var dataReader =
                                                    await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                                                {
                                                    if (!dataReader.HasRows)
                                                    {
                                                        checkMaQhns = false;
                                                        lstResponseB01X.Add(new Response(true,
                                                            "B01X có mã DVQHNS " + lstDetail[0].MA_QHNS + " không có trong danh mục"));
                                                    }
                                                    else
                                                    {
                                                        checkMaQhns = true;
                                                        while (dataReader.Read())
                                                        {
                                                            machuong = dataReader["MA_CHUONG"]?.ToString();
                                                            tenqhns = dataReader["TEN_DVQHNS"]?.ToString();
                                                            madbhc = dataReader["MA_XA"]?.ToString();
                                                            madbhccha = dataReader["MA_HUYEN"]?.ToString();
                                                        }
                                                    }

                                                }
                                            }
                                            //insert vào DB
                                            try
                                            {
                                                if (checkMaQhns)
                                                {
                                                    if (!string.IsNullOrEmpty(machuong) && !string.IsNullOrEmpty(tenqhns) &&
                                                    !string.IsNullOrEmpty(madbhc))
                                                    {
                                                        command.CommandText =
                                                            "INSERT INTO PHB_C_B01X(REFID,MA_CHUONG,MA_QHNS,NAM_BC,KY_BC,TRANG_THAI,NGAY_TAO,NGUOI_TAO,MA_DBHC,MA_DBHC_CHA,TEN_QHNS) " +
                                                            "VALUES(:refid,:machuong,:maqhns,:nambc,:kybc,0,SYSDATE,:nguoitao,:madbhc,:madbhccha,:tenqhns)";
                                                        command.Parameters.Clear();
                                                        var refid = Guid.NewGuid().ToString("N");
                                                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 50).Value =
                                                            refid;
                                                        command.Parameters.Add("machuong", OracleDbType.NVarchar2, 3).Value =
                                                            machuong;
                                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                            lstDetail[0].MA_QHNS;
                                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value =
                                                            lstDetail[0].NAM_BC;
                                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value =
                                                            lstDetail[0].KY_BC;
                                                        command.Parameters.Add("nguoitao", OracleDbType.NVarchar2, 150).Value =
                                                            RequestContext.Principal.Identity.Name;
                                                        command.Parameters.Add("madbhc", OracleDbType.NVarchar2, 50).Value =
                                                            madbhc;
                                                        command.Parameters.Add("madbhccha", OracleDbType.NVarchar2, 50).Value =
                                                            madbhccha;
                                                        command.Parameters.Add("tenqhns", OracleDbType.NVarchar2, 50).Value =
                                                            tenqhns;
                                                        await command.ExecuteNonQueryAsync();
                                                        var buildQuerry = "";
                                                        foreach (var detail in lstDetail)
                                                        {
                                                            buildQuerry +=
                                                                "INTO PHB_C_B01X_DETAIL(PHB_C_B01X_REFID,LOAI,MA_TAIKHOAN,TEN_TAIKHOAN,SDDK_NO,SDDK_CO,PSTK_NO,PSTK_CO,LUYKE_NO,LUYKE_CO,SDCK_NO,SDCK_CO) " +
                                                                "VALUES('" + refid + "',";
                                                            if (detail.MA_TAIKHOAN.StartsWith("00"))
                                                            {
                                                                buildQuerry += "2,";
                                                            }
                                                            else
                                                            {
                                                                buildQuerry += "1,";
                                                            }
                                                            buildQuerry +=
                                                            ("N'" + detail.MA_TAIKHOAN + "'" + ",N'" + detail.TEN_TAIKHOAN +
                                                             "'," + detail.SDDK_NO + "," + detail.SDDK_CO + "," +
                                                             detail.PSTK_NO + "," + detail.PSTK_CO + "," + detail.LUYKE_NO +
                                                             "," + detail.LUYKE_CO + "," + detail.SDCK_NO + "," +
                                                             detail.SDCK_CO);
                                                            buildQuerry += ") ";
                                                        }
                                                        buildQuerry += " SELECT * FROM dual";
                                                        command.CommandText = "INSERT ALL " + buildQuerry;
                                                        command.Parameters.Clear();
                                                        await command.ExecuteNonQueryAsync();
                                                        transaction.Commit();
                                                        lstResponseB01X.Add(new Response(false, "B01X báo cáo kỳ " + lstDetail[0].KY_BC + ": thành công"));
                                                    }
                                                    else
                                                    {
                                                        lstResponseB01X.Add(new Response(true, "B01X báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                lstResponseB01X.Add(new Response(true, "B01X báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                            }
                                        }
                                    }
                                    lstResponseRp.AddRange(lstResponseB01X);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region ImportB02AX
                var nodeListDataB02AX = doc.GetElementsByTagName("SummaryB02aX");
                if (nodeListDataB02AX.Count > 0)
                {
                    var lstB02AxDetails = (from XmlNode nodeData in nodeListDataB02AX
                                           select new B02AXImportModel()
                                           {
                                               MA_QHNS = nodeData["BranchCode"]?.InnerText ?? "",
                                               KY_BC = nodeData["ReportPeriod"] != null ? int.Parse(nodeData["ReportPeriod"].InnerText) : 13,
                                               NAM_BC = nodeData["ReportYear"] != null ? int.Parse(nodeData["ReportYear"].InnerText) : 0,
                                               DU_TOAN = decimal.Parse(nodeData["YearPlanAmount"].InnerText),
                                               TH_TRONG_THANG = decimal.Parse(nodeData["MonthActualAmount"].InnerText),
                                               TH_LUYKE_DN = decimal.Parse(nodeData["AccumActualAmount"].InnerText),
                                               MA_SO = nodeData["PlanActivityCode"]?.InnerText ?? "",
                                               NOI_DUNG = nodeData["ActivityName"]?.InnerText ?? "",
                                               SO_SANH = decimal.Parse(nodeData["YearPlanAmount"].InnerText) == 0 ? 0 : (decimal.Parse(nodeData["AccumActualAmount"].InnerText) * 100 / decimal.Parse(nodeData["YearPlanAmount"].InnerText)),
                                               SAP_XEP = nodeData["SortOrder"] != null ? int.Parse(nodeData["SortOrder"].InnerText) : 0,
                                               STT = nodeData["PlanActivityIndex"]?.InnerText ?? ""
                                           }).ToList();
                    if (lstB02AxDetails.Count > 0)
                    {
                        var machuong = "";
                        var tenqhns = "";
                        var madbhc = "";
                        var madbhccha = "";
                        var groups = lstB02AxDetails.GroupBy(x => x.KY_BC);
                        var isCheck = false;
                        var checkMaQhns = false;
                        foreach (var group in groups)
                        {
                            using (var connection =
                                new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"]
                                    .ConnectionString))
                            {
                                await connection.OpenAsync();
                                using (var command = connection.CreateCommand())
                                {
                                    var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                                    command.Transaction = transaction;
                                    var lstResponseB01X = new List<Response>();
                                    var lstDetail = group.ToList();
                                    if (lstDetail.Count > 0)
                                    {
                                        command.CommandType = CommandType.Text;
                                        command.CommandText = "SELECT COUNT(REFID) FROM PHB_C_B02AX WHERE MA_QHNS=:maqhns AND NAM_BC=:nambc AND KY_BC=:kybc";
                                        command.Parameters.Clear();
                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value = lstDetail[0].MA_QHNS;
                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value = lstDetail[0].NAM_BC;
                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value = lstDetail[0].KY_BC;
                                        var result = await command.ExecuteScalarAsync();
                                        if (int.Parse(result.ToString()) > 0)
                                        {
                                            //bỏ qua insert
                                            lstResponseB01X.Add(new Response(true, "B02AX đã tồn tại báo cáo kỳ " + lstDetail[0].KY_BC));
                                        }
                                        else
                                        {
                                            //check mã dvqhns
                                            if (!isCheck)
                                            {
                                                isCheck = true;
                                                command.CommandText = "SELECT * FROM SYS_DVQHNS WHERE MA_DVQHNS=:maqhns";
                                                command.Parameters.Clear();
                                                command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                    lstDetail[0].MA_QHNS;
                                                using (var dataReader =
                                                    await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                                                {
                                                    if (!dataReader.HasRows)
                                                    {
                                                        checkMaQhns = false;
                                                        lstResponseB01X.Add(new Response(true,
                                                            "B02AX có mã DVQHNS " + lstDetail[0].MA_QHNS + " không có trong danh mục"));
                                                    }
                                                    else
                                                    {
                                                        checkMaQhns = true;
                                                        while (dataReader.Read())
                                                        {
                                                            machuong = dataReader["MA_CHUONG"]?.ToString();
                                                            tenqhns = dataReader["TEN_DVQHNS"]?.ToString();
                                                            madbhc = dataReader["MA_XA"]?.ToString();
                                                            madbhccha = dataReader["MA_HUYEN"]?.ToString();
                                                        }
                                                    }

                                                }
                                            }
                                            //insert vào DB
                                            try
                                            {
                                                if (checkMaQhns)
                                                {
                                                    if (!string.IsNullOrEmpty(machuong) && !string.IsNullOrEmpty(tenqhns) &&
                                                    !string.IsNullOrEmpty(madbhc))
                                                    {
                                                        command.CommandText =
                                                            "INSERT INTO PHB_C_B02AX(REFID,MA_CHUONG,MA_QHNS,NAM_BC,KY_BC,TRANG_THAI,NGAY_TAO,NGUOI_TAO,MA_DBHC,MA_DBHC_CHA,TEN_QHNS) " +
                                                            "VALUES(:refid,:machuong,:maqhns,:nambc,:kybc,0,SYSDATE,:nguoitao,:madbhc,:madbhccha,:tenqhns)";
                                                        command.Parameters.Clear();
                                                        var refid = Guid.NewGuid().ToString("N");
                                                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 50).Value =
                                                            refid;
                                                        command.Parameters.Add("machuong", OracleDbType.NVarchar2, 3).Value =
                                                            machuong;
                                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                            lstDetail[0].MA_QHNS;
                                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value =
                                                            lstDetail[0].NAM_BC;
                                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value =
                                                            lstDetail[0].KY_BC;
                                                        command.Parameters.Add("nguoitao", OracleDbType.NVarchar2, 150).Value =
                                                            RequestContext.Principal.Identity.Name;
                                                        command.Parameters.Add("madbhc", OracleDbType.NVarchar2, 50).Value =
                                                            madbhc;
                                                        command.Parameters.Add("madbhccha", OracleDbType.NVarchar2, 50).Value =
                                                            madbhccha;
                                                        command.Parameters.Add("tenqhns", OracleDbType.NVarchar2, 50).Value =
                                                            tenqhns;
                                                        await command.ExecuteNonQueryAsync();
                                                        var buildQuerry = "";
                                                        foreach (var detail in lstDetail)
                                                        {
                                                            buildQuerry +=
                                                                "INTO PHB_C_B02AX_DETAIL(PHB_C_B02AX_REFID,SAP_XEP,STT,NOI_DUNG,MA_SO,DU_TOAN,TH_TRONG_THANG,TH_LUYKE_DN,SO_SANH) " +
                                                                "VALUES('" + refid + "'," + detail.SAP_XEP + ",'" + detail.STT + "',N'" + detail.NOI_DUNG + "','" + detail.MA_SO + "'," + detail.DU_TOAN + "," +
                                                                detail.TH_TRONG_THANG + "," + detail.TH_LUYKE_DN + "," + detail.SO_SANH;
                                                            buildQuerry += ")";
                                                        }
                                                        buildQuerry += " SELECT * FROM dual";
                                                        command.CommandText = "INSERT ALL " + buildQuerry;
                                                        command.Parameters.Clear();
                                                        await command.ExecuteNonQueryAsync();
                                                        transaction.Commit();
                                                        lstResponseB01X.Add(new Response(false, "B02AX báo cáo kỳ " + lstDetail[0].KY_BC + ": thành công"));
                                                    }
                                                    else
                                                    {
                                                        lstResponseB01X.Add(new Response(true, "B02AX báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                lstResponseB01X.Add(new Response(true, "B02AX báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                            }
                                        }
                                    }
                                    lstResponseRp.AddRange(lstResponseB01X);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region ImportB02BX
                var nodeListDataB02Bx = doc.GetElementsByTagName("SummaryB02bX");
                if (nodeListDataB02Bx.Count > 0)
                {
                    var lstB02BxDetails = (from XmlNode nodeData in nodeListDataB02Bx
                                           select new B02BXImportModel()
                                           {
                                               MA_QHNS = nodeData["BranchCode"]?.InnerText ?? "",
                                               KY_BC = nodeData["ReportPeriod"] != null ? int.Parse(nodeData["ReportPeriod"].InnerText) : 13,
                                               NAM_BC = nodeData["ReportYear"] != null ? int.Parse(nodeData["ReportYear"].InnerText) : 0,
                                               DUTOANNAM = double.Parse(nodeData["YearPlanAmount"].InnerText),
                                               TRONGTHANG = double.Parse(nodeData["MonthActualAmount"].InnerText),
                                               LUYKE = double.Parse(nodeData["AccumActualAmount"].InnerText),
                                               MASO = nodeData["PlanActivityCode"] != null ? int.Parse(nodeData["PlanActivityCode"].InnerText) : 0,
                                               NOIDUNG = nodeData["ActivityName"]?.InnerText ?? "",
                                               SOSANH = double.Parse(nodeData["YearPlanAmount"].InnerText) == 0 ? 0 : (double.Parse(nodeData["AccumActualAmount"].InnerText) * 100 / double.Parse(nodeData["YearPlanAmount"].InnerText)),
                                               SAPXEP = nodeData["SortOrder"] != null ? int.Parse(nodeData["SortOrder"].InnerText) : 0,
                                               STT = nodeData["PlanActivityIndex"]?.InnerText ?? ""
                                           }).ToList();
                    if (lstB02BxDetails.Count > 0)
                    {
                        var machuong = "";
                        var tenqhns = "";
                        var madbhc = "";
                        var madbhccha = "";
                        var groups = lstB02BxDetails.GroupBy(x => x.KY_BC);
                        var isCheck = false;
                        var checkMaQhns = false;
                        foreach (var group in groups)
                        {
                            using (var connection =
                                new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"]
                                    .ConnectionString))
                            {
                                await connection.OpenAsync();
                                using (var command = connection.CreateCommand())
                                {
                                    var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                                    command.Transaction = transaction;
                                    var lstResponseB02BX = new List<Response>();
                                    var lstDetail = group.ToList();
                                    if (lstDetail.Count > 0)
                                    {
                                        command.CommandType = CommandType.Text;
                                        command.CommandText = "SELECT COUNT(REFID) FROM PHB_C_B02B_X WHERE MA_QHNS=:maqhns AND NAM_BC=:nambc AND KY_BC=:kybc";
                                        command.Parameters.Clear();
                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value = lstDetail[0].MA_QHNS;
                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value = lstDetail[0].NAM_BC;
                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value = lstDetail[0].KY_BC;
                                        var result = await command.ExecuteScalarAsync();
                                        if (int.Parse(result.ToString()) > 0)
                                        {
                                            //bỏ qua insert
                                            lstResponseB02BX.Add(new Response(true, "B02BX đã tồn tại báo cáo kỳ " + lstDetail[0].KY_BC));
                                        }
                                        else
                                        {
                                            //check mã dvqhns
                                            if (!isCheck)
                                            {
                                                isCheck = true;
                                                command.CommandText = "SELECT * FROM SYS_DVQHNS WHERE MA_DVQHNS=:maqhns";
                                                command.Parameters.Clear();
                                                command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                    lstDetail[0].MA_QHNS;
                                                using (var dataReader =
                                                    await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                                                {
                                                    if (!dataReader.HasRows)
                                                    {
                                                        checkMaQhns = false;
                                                        lstResponseB02BX.Add(new Response(true,
                                                            "B02BX có mã DVQHNS " + lstDetail[0].MA_QHNS + " không có trong danh mục"));
                                                    }
                                                    else
                                                    {
                                                        checkMaQhns = true;
                                                        while (dataReader.Read())
                                                        {
                                                            machuong = dataReader["MA_CHUONG"]?.ToString();
                                                            tenqhns = dataReader["TEN_DVQHNS"]?.ToString();
                                                            madbhc = dataReader["MA_XA"]?.ToString();
                                                            madbhccha = dataReader["MA_HUYEN"]?.ToString();
                                                        }
                                                    }

                                                }
                                            }
                                            //insert vào DB
                                            try
                                            {
                                                if (checkMaQhns)
                                                {
                                                    if (!string.IsNullOrEmpty(machuong) && !string.IsNullOrEmpty(tenqhns) &&
                                                    !string.IsNullOrEmpty(madbhc))
                                                    {
                                                        command.CommandText =
                                                            "INSERT INTO PHB_C_B02B_X(REFID,MA_CHUONG,MA_QHNS,NAM_BC,KY_BC,TRANG_THAI,NGAY_TAO,NGUOI_TAO,MA_DBHC,MA_DBHC_CHA,TEN_QHNS) " +
                                                            "VALUES(:refid,:machuong,:maqhns,:nambc,:kybc,0,SYSDATE,:nguoitao,:madbhc,:madbhccha,:tenqhns)";
                                                        command.Parameters.Clear();
                                                        var refid = Guid.NewGuid().ToString("N");
                                                        command.Parameters.Add("refid", OracleDbType.NVarchar2, 50).Value =
                                                            refid;
                                                        command.Parameters.Add("machuong", OracleDbType.NVarchar2, 3).Value =
                                                            machuong;
                                                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value =
                                                            lstDetail[0].MA_QHNS;
                                                        command.Parameters.Add("nambc", OracleDbType.Int32).Value =
                                                            lstDetail[0].NAM_BC;
                                                        command.Parameters.Add("kybc", OracleDbType.Int32).Value =
                                                            lstDetail[0].KY_BC;
                                                        command.Parameters.Add("nguoitao", OracleDbType.NVarchar2, 150).Value =
                                                            RequestContext.Principal.Identity.Name;
                                                        command.Parameters.Add("madbhc", OracleDbType.NVarchar2, 50).Value =
                                                            madbhc;
                                                        command.Parameters.Add("madbhccha", OracleDbType.NVarchar2, 50).Value =
                                                            madbhccha;
                                                        command.Parameters.Add("tenqhns", OracleDbType.NVarchar2, 50).Value =
                                                            tenqhns;
                                                        await command.ExecuteNonQueryAsync();
                                                        var buildQuerry = "";
                                                        foreach (var detail in lstDetail)
                                                        {
                                                            buildQuerry +=
                                                                "INTO PHB_C_B02B_X_DETAIL(PHB_C_B02B_X_REFID,SAPXEP,STT,NOIDUNG,MASO,DUTOANNAM,TRONGTHANG,LUYKE,SOSANH) " +
                                                                "VALUES('" + refid + "'," + detail.SAPXEP + ",'" + detail.STT + "',N'" + detail.NOIDUNG + "'," + detail.MASO + "," + detail.DUTOANNAM + "," +
                                                                detail.TRONGTHANG + "," + detail.LUYKE + "," + detail.SOSANH;
                                                            buildQuerry += ")";
                                                        }
                                                        buildQuerry += " SELECT * FROM dual";
                                                        command.CommandText = "INSERT ALL " + buildQuerry;
                                                        command.Parameters.Clear();
                                                        await command.ExecuteNonQueryAsync();
                                                        transaction.Commit();
                                                        lstResponseB02BX.Add(new Response(false, "B02BX báo cáo kỳ " + lstDetail[0].KY_BC + ": thành công"));
                                                    }
                                                    else
                                                    {
                                                        lstResponseB02BX.Add(new Response(true, "B02BX báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                lstResponseB02BX.Add(new Response(true, "B02BX báo cáo kỳ " + lstDetail[0].KY_BC + ": bỏ qua"));
                                            }
                                        }
                                    }
                                    lstResponseRp.AddRange(lstResponseB02BX);
                                }
                            }
                        }
                    }
                }
                #endregion

                return Ok(lstResponseRp);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return Ok(new Response(true, ErrorMessage.ERROR_DATA));
            }
        }
        #endregion

        #region GetTotalB02BCTC
        [Route("GetTotalB02BCTC")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTotalB02BCTC(ReceivedReportRQModel model)
        {
            if (model.MA_DVQHNS == "Tất cả") model.MA_DVQHNS = "";
            if (model.TEN_DVQHNS == "Tất cả") model.TEN_DVQHNS = "";
            if (string.IsNullOrEmpty(model.MA_BAO_CAO)) model.MA_BAO_CAO = "-1";
            if (string.IsNullOrEmpty(model.MA_DVQHNS)) model.MA_DVQHNS = "-1";
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_DVQHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_DONVI").Value.ToString();
            var tempDVQHNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS);
            var lstTempDVQHNS = _sysDVQHNSService.Queryable().Where(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS).ToList();
            var result = new Response<List<ReceivedReportRSModel>>();
            try
            {
                if (model.MA_BAO_CAO == "B01B_TT137")
                {
                    var maBaoCaoB02_BCTC = "B02_BCTC";
                    var lst = new List<ReceivedReportRSModel>();
                    var tempDVSDNS = _sysDvqhns_QuanLyService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == model.MA_DVQHNS);
                    var tempResult = new ReceivedReportRSModel();
                    if (tempDVSDNS != null)
                    {
                        tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                    }
                    var checkExits = _bmB01bTT137Service.Queryable().Where(x => x.MA_QHNS == model.MA_DVQHNS && x.NAM_BC == model.NAM_BC && x.KY_BC == model.KY_BC).Count();
                    if (checkExits > 0)
                    {
                        using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "PHB_GET_TOTAL_REPORT";
                                command.Parameters.Clear();
                                command.Parameters.Add("maBaoCao", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                                command.Parameters.Add("maQHNS", OracleDbType.NVarchar2, 100).Value = model.MA_DVQHNS;
                                command.Parameters.Add("namBC", OracleDbType.Int32).Value = model.NAM_BC;
                                command.Parameters.Add("kyBC", OracleDbType.Int32).Value = model.KY_BC;
                                command.Parameters.Add("userName", OracleDbType.NVarchar2, 50).Value = RequestContext.Principal.Identity.Name;
                                command.Parameters.Add("outRef", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                                await command.ExecuteNonQueryAsync();
                                using (var reader = ((OracleRefCursor)command.Parameters["outRef"].Value).GetDataReader())
                                {
                                    while (reader.Read())
                                    {
                                        lst.Add(new ReceivedReportRSModel()
                                        {
                                            KY_BC = int.Parse(reader["KY_BC"].ToString()),
                                            NAM_BC = int.Parse(reader["NAM_BC"].ToString()),
                                            MA_BAO_CAO = reader["MA_BAO_CAO"].ToString(),
                                            MA_CHUONG = reader["MA_CHUONG"].ToString(),
                                            MA_QHNS = reader["MA_QHNS"].ToString(),
                                            TEN_QHNS = reader["TEN_QHNS"].ToString(),
                                            MA_QHNS_QL = tempResult.MA_QHNS_QL,
                                            TRANG_THAI = int.Parse(reader["TRANG_THAI"].ToString()),
                                            TRANG_THAI_GUI = int.Parse(reader["TRANG_THAI_GUI"].ToString()),
                                            ID = int.Parse(reader["ID"].ToString()),
                                            REFID = reader["REFID"].ToString(),
                                            NGUOI_TAO = reader["NGUOI_TAO"].ToString(),
                                            NGAY_TAO = DateTime.Parse(reader["NGAY_TAO"].ToString())
                                        });
                                    }

                                }

                                result.Error = false;
                                result.Data = lst;
                            }
                        }
                    }
                    else
                    {
                        using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "PHB_GET_B02BCTC_REPORT";
                                command.Parameters.Clear();
                                command.Parameters.Add("maBaoCao", OracleDbType.NVarchar2, 100).Value = maBaoCaoB02_BCTC;
                                command.Parameters.Add("maQHNS", OracleDbType.NVarchar2, 100).Value = model.MA_DVQHNS;
                                command.Parameters.Add("namBC", OracleDbType.Int32).Value = model.NAM_BC;
                                command.Parameters.Add("kyBC", OracleDbType.Int32).Value = model.KY_BC;
                                command.Parameters.Add("userName", OracleDbType.NVarchar2, 50).Value = RequestContext.Principal.Identity.Name;
                                command.Parameters.Add("outRef", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                                await command.ExecuteNonQueryAsync();
                                using (var reader = ((OracleRefCursor)command.Parameters["outRef"].Value).GetDataReader())
                                {
                                    while (reader.Read())
                                    {
                                        lst.Add(new ReceivedReportRSModel()
                                        {
                                            ID = int.Parse(reader["ID"].ToString()),
                                            REFID = reader["REFID"].ToString(),
                                            TRANG_THAI = int.Parse(reader["TRANG_THAI"].ToString()),
                                            TRANG_THAI_GUI = int.Parse(reader["TRANG_THAI_GUI"].ToString()),
                                            NGAY_TAO = DateTime.Parse(reader["NGAY_TAO"].ToString()),
                                            NGUOI_TAO = reader["NGUOI_TAO"].ToString(),
                                            MA_BAO_CAO = model.MA_BAO_CAO,
                                            MA_QHNS = reader["MA_DONVI"].ToString(),
                                            DON_VI_DT = reader["DON_VI_DT"].ToString(),
                                            CAP_DU_TOAN = reader["CAP_DU_TOAN"].ToString(),
                                            NAM_BC = int.Parse(reader["NAM"].ToString()),
                                        });
                                    }
                                    foreach (var item in lst)
                                    {
                                        var tempNameDVQHNS = _sysDvqhns_QuanLyService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == item.MA_QHNS);
                                        if (tempNameDVQHNS != null)
                                        {
                                            item.TEN_QHNS = tempNameDVQHNS.TEN_DVQHNS;
                                            item.MA_QHNS_QL = tempNameDVQHNS.MA_DVQHNS_CHA;
                                        }
                                    }
                                }

                                result.Error = false;
                                result.Data = lst;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region GetTotalReport
        [Route("GetTotalReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetReceivedReport(ReceivedReportRQModel model)
        {
            if (model.MA_DVQHNS == "Tất cả") model.MA_DVQHNS = "";
            if (model.TEN_DVQHNS == "Tất cả") model.TEN_DVQHNS = "";
            if (string.IsNullOrEmpty(model.MA_BAO_CAO)) model.MA_BAO_CAO = "-1";
            if (string.IsNullOrEmpty(model.MA_DVQHNS)) model.MA_DVQHNS = "-1";
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_DVQHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_DONVI").Value.ToString();
            var tempDVQHNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS);
            var lstTempDVQHNS = _sysDVQHNSService.Queryable().Where(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS).ToList();
            var result = new Response<List<ReceivedReportRSModel>>();
            try
            {
                var lst = new List<ReceivedReportRSModel>();
                if (model.MA_BAO_CAO == "-1")
                {
                    result.Error = false;
                    result.Data = lst;

                }
                else
                {
                    var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == model.MA_DVQHNS);
                    var tempResult = new ReceivedReportRSModel();
                    if (tempDVSDNS != null)
                    {
                        tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                    }
                    if ((model.MA_DVQHNS == "-1") && tempDVQHNS != null)
                    {
                        using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "PHB_GET_TOTAL_REPORT";
                                command.Parameters.Clear();
                                command.Parameters.Add("maBaoCao", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                                command.Parameters.Add("maQHNS", OracleDbType.NVarchar2, 100).Value = model.MA_DVQHNS;
                                command.Parameters.Add("namBC", OracleDbType.Int32).Value = model.NAM_BC;
                                command.Parameters.Add("kyBC", OracleDbType.Int32).Value = model.KY_BC;
                                command.Parameters.Add("userName", OracleDbType.NVarchar2, 50).Value = RequestContext.Principal.Identity.Name;
                                command.Parameters.Add("outRef", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                                await command.ExecuteNonQueryAsync();
                                using (var reader = ((OracleRefCursor)command.Parameters["outRef"].Value).GetDataReader())
                                {
                                    while (reader.Read())
                                    {
                                        foreach (var item in lstTempDVQHNS)
                                        {
                                            var maDVQHNS = reader["MA_QHNS"].ToString();
                                            if (item.MA_DVQHNS == maDVQHNS || item.MA_DVQHNS_CHA == maDVQHNS)
                                            {
                                                lst.Add(new ReceivedReportRSModel()
                                                {
                                                    KY_BC = int.Parse(reader["KY_BC"].ToString()),
                                                    NAM_BC = int.Parse(reader["NAM_BC"].ToString()),
                                                    MA_BAO_CAO = reader["MA_BAO_CAO"].ToString(),
                                                    MA_CHUONG = reader["MA_CHUONG"].ToString(),
                                                    MA_QHNS = reader["MA_QHNS"].ToString(),
                                                    TEN_QHNS = reader["TEN_QHNS"].ToString(),
                                                    MA_QHNS_QL = tempResult.MA_QHNS_QL,
                                                    TRANG_THAI = int.Parse(reader["TRANG_THAI"].ToString()),
                                                    TRANG_THAI_GUI = int.Parse(reader["TRANG_THAI_GUI"].ToString()),
                                                    ID = int.Parse(reader["ID"].ToString()),
                                                    REFID = reader["REFID"].ToString(),
                                                    NGUOI_TAO = reader["NGUOI_TAO"].ToString(),
                                                    NGAY_TAO = DateTime.Parse(reader["NGAY_TAO"].ToString())
                                                });
                                                break;
                                            }
                                        }
                                    }
                                }

                                result.Error = false;
                                result.Data = lst;
                            }
                        }
                    }
                    else
                    {
                        using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "PHB_GET_TOTAL_REPORT";
                                command.Parameters.Clear();
                                command.Parameters.Add("maBaoCao", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                                command.Parameters.Add("maQHNS", OracleDbType.NVarchar2, 100).Value = model.MA_DVQHNS;
                                command.Parameters.Add("namBC", OracleDbType.Int32).Value = model.NAM_BC;
                                command.Parameters.Add("kyBC", OracleDbType.Int32).Value = model.KY_BC;
                                command.Parameters.Add("userName", OracleDbType.NVarchar2, 50).Value = RequestContext.Principal.Identity.Name;
                                command.Parameters.Add("outRef", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                                await command.ExecuteNonQueryAsync();
                                using (var reader = ((OracleRefCursor)command.Parameters["outRef"].Value).GetDataReader())
                                {
                                    while (reader.Read())
                                    {
                                        lst.Add(new ReceivedReportRSModel()
                                        {
                                            KY_BC = int.Parse(reader["KY_BC"].ToString()),
                                            NAM_BC = int.Parse(reader["NAM_BC"].ToString()),
                                            MA_BAO_CAO = reader["MA_BAO_CAO"].ToString(),
                                            MA_CHUONG = reader["MA_CHUONG"].ToString(),
                                            MA_QHNS = reader["MA_QHNS"].ToString(),
                                            TEN_QHNS = reader["TEN_QHNS"].ToString(),
                                            MA_QHNS_QL = tempResult.MA_QHNS_QL,
                                            TRANG_THAI = int.Parse(reader["TRANG_THAI"].ToString()),
                                            TRANG_THAI_GUI = int.Parse(reader["TRANG_THAI_GUI"].ToString()),
                                            ID = int.Parse(reader["ID"].ToString()),
                                            REFID = reader["REFID"].ToString(),
                                            NGUOI_TAO = reader["NGUOI_TAO"].ToString(),
                                            NGAY_TAO = DateTime.Parse(reader["NGAY_TAO"].ToString())
                                        });
                                    }
                                }
                                result.Error = false;
                                result.Data = lst;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region GetTotalReportXa
        [Route("GetTotalReportXa")]
        [HttpPost]
        public async Task<IHttpActionResult> GetTotalReportXa(ReceivedReportRQModelXa model)
        {
            if (string.IsNullOrEmpty(model.MA_BAO_CAO)) model.MA_BAO_CAO = "-1";
            if (string.IsNullOrEmpty(model.MA_DBHC)) model.MA_DBHC = "-1";
            var result = new Response<List<ReceivedReportRSModelXa>>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var maDBHC = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                maDBHC = firstOrDefault.Value;
                if (maDBHC.Length == 2) maDBHC = "-1";
            }
            try
            {
                var lst = new List<ReceivedReportRSModelXa>();
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_GET_TOTAL_REPORT_XA";
                        command.Parameters.Clear();
                        command.Parameters.Add("MABAOCAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                        command.Parameters.Add("NAMBC", OracleDbType.Int32).Value = model.NAM_BC;
                        command.Parameters.Add("KYBC", OracleDbType.Int32).Value = model.KY_BC;
                        command.Parameters.Add("MADBHC", OracleDbType.NVarchar2, 50).Value = maDBHC;
                        command.Parameters.Add("outRef", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        await command.ExecuteNonQueryAsync();
                        using (var reader = ((OracleRefCursor)command.Parameters["outRef"].Value).GetDataReader())
                        {
                            while (reader.Read())
                            {
                                lst.Add(new ReceivedReportRSModelXa()
                                {
                                    KY_BC = int.Parse(reader["KY_BC"].ToString()),
                                    NAM_BC = int.Parse(reader["NAM_BC"].ToString()),
                                    MA_BAO_CAO = reader["MA_BAO_CAO"].ToString(),
                                    MA_DBHC = reader["MA_DBHC"].ToString(),
                                    MA_CHUONG = reader["MA_CHUONG"].ToString(),
                                    MA_QHNS = reader["MA_QHNS"].ToString(),
                                    TEN_DBHC = reader["TEN_DBHC"].ToString(),
                                    TRANG_THAI = int.Parse(reader["TRANG_THAI"].ToString()),
                                    ID = int.Parse(reader["ID"].ToString()),
                                    REFID = reader["REFID"].ToString(),
                                    NGUOI_TAO = reader["NGUOI_TAO"].ToString(),
                                    NGAY_TAO = DateTime.Parse(reader["NGAY_TAO"].ToString())
                                });
                            }

                        }
                        result.Error = false;
                        result.Data = lst;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region SendReport Gửi
        [Route("SendReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SendReport(ReceivedReportRSModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var result = new Response<string>();
            try
            {
                if (model.MA_BAO_CAO == "B01B_TT137")
                {
                    var checkExits = _bmB01bTT137Service.Queryable().Where(x => x.MA_QHNS == model.MA_QHNS && x.NAM_BC == model.NAM_BC && x.KY_BC == model.KY_BC).Count();
                    if (checkExits > 0)
                    {
                        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            var command = connection.CreateCommand();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "PHB_SEND_REJECT_REPORT";
                            command.Parameters.Clear();
                            command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                            command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                            command.Parameters.Add("TRANG_THAI_GUI", OracleDbType.Int32).Value = 1;
                            await command.ExecuteNonQueryAsync();
                            result.Error = false;
                            result.Data = "SENDREPORT";
                            result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                        }
                    }
                    else
                    {
                        var maBaoCaoB02BCTC = "PHA_B02_BCTC";
                        using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                        {
                            await connection.OpenAsync();
                            var command = connection.CreateCommand();
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = "PHB_SEND_REJECT_REPORT";
                            command.Parameters.Clear();
                            command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = maBaoCaoB02BCTC;
                            command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                            command.Parameters.Add("TRANG_THAI_GUI", OracleDbType.Int32).Value = 1;
                            await command.ExecuteNonQueryAsync();
                            result.Error = false;
                            result.Data = "SENDREPORT";
                            result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                        }
                    }
                }
                else
                {
                    using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                    {
                        await connection.OpenAsync();
                        var command = connection.CreateCommand();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_SEND_REJECT_REPORT";
                        command.Parameters.Clear();
                        command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                        command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                        command.Parameters.Add("TRANG_THAI_GUI", OracleDbType.Int32).Value = 1;
                        await command.ExecuteNonQueryAsync();
                        result.Error = false;
                        result.Data = "SENDREPORT";
                        result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                }

            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region RejectSendReport
        [Route("RejectSendReport")]
        [HttpPost]
        public async Task<IHttpActionResult> RejectSendReport(ReceivedReportRSModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var result = new Response<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PHB_SEND_REJECT_REPORT";
                    command.Parameters.Clear();
                    command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                    command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                    command.Parameters.Add("TRANG_THAI_GUI", OracleDbType.Int32).Value = 2;
                    await command.ExecuteNonQueryAsync();
                    result.Error = false;
                    result.Data = "REJECTED";
                    result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region ApproveReport 
        [Route("ApproveReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ApproveReport(ReceivedReportRSModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var result = new Response<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PHB_APPROVE_REJECT_REPORT";
                    command.Parameters.Clear();
                    command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                    command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                    command.Parameters.Add("TRANG_THAI", OracleDbType.Int32).Value = 1;
                    await command.ExecuteNonQueryAsync();
                    result.Error = false;
                    result.Data = "APPROVED";
                    result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region RejectReport 
        [Route("RejectReport")]
        [HttpPost]
        public async Task<IHttpActionResult> RejectReport(ReceivedReportRSModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var result = new Response<string>();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PHB_APPROVE_REJECT_REPORT";
                    command.Parameters.Clear();
                    command.Parameters.Add("MA_BAO_CAO", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                    command.Parameters.Add("REFID_BC", OracleDbType.NVarchar2, 100).Value = model.REFID;
                    command.Parameters.Add("TRANG_THAI", OracleDbType.Int32).Value = 0;
                    await command.ExecuteNonQueryAsync();
                    result.Error = false;
                    result.Data = "REJECTED";
                    result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(result);
        }
        #endregion

        #region DeleteReport
        [Route("DeleteReport")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteReport(DeleteReportRQModel model)
        {
            if (string.IsNullOrEmpty(model.MA_BAO_CAO) || string.IsNullOrEmpty(model.REFID)) return BadRequest();
            model.MA_BAO_CAO = model.MA_BAO_CAO.ToUpper();
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    var result = new Response<string>();

                    connection.Open();
                    var command = connection.CreateCommand();
                    OracleTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.Transaction = transaction;
                    command.CommandType = CommandType.Text;
                    try
                    {
                        if (model.MA_BAO_CAO != null)
                        {
                            command.CommandText = "DELETE FROM PHB_" + model.MA_BAO_CAO + " WHERE REFID='" + model.REFID + "'";
                            command.ExecuteNonQuery();

                            command.CommandText = "DELETE FROM PHB_" + model.MA_BAO_CAO + "_DETAIL WHERE PHB_" + model.MA_BAO_CAO + "_REFID='" + model.REFID + "'";
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }

                        result.Error = false;
                        result.Message = "Xóa thành công.";
                        return Ok(result);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        WriteLogs.LogError(ex);
                        result.Error = true;
                        result.Message = "Gặp lỗi trong quá trình thực hiện. Thử lại sau.";
                        return Ok(result);
                    }
                }
            }
            catch (Exception e)
            {
                WriteLogs.LogError(e);
                return InternalServerError();
            }
        }
        #endregion

        #region CheckIsExists
        [Route("IsExists")]
        [HttpPost]
        public async Task<IHttpActionResult> IsExists(ReceivedReportRQModel model)
        {
            if (string.IsNullOrEmpty(model.MA_BAO_CAO) || model.NAM_BC < 1 || model.KY_BC < 0) return BadRequest();
            var response = new Response<bool>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PHB_CHECK_ISEXISTS_REPORT";
                        command.Parameters.Clear();
                        command.Parameters.Add("maBaoCao", OracleDbType.NVarchar2, 100).Value = model.MA_BAO_CAO;
                        command.Parameters.Add("maQHNS", OracleDbType.NVarchar2, 100).Value = model.MA_DVQHNS;
                        command.Parameters.Add("namBC", OracleDbType.Int32).Value = model.NAM_BC;
                        command.Parameters.Add("kyBC", OracleDbType.Int32).Value = model.KY_BC;
                        command.Parameters.Add("CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        await command.ExecuteNonQueryAsync();
                        using (var reader = ((OracleRefCursor)command.Parameters["CUR"].Value).GetDataReader())
                        {
                            if (!reader.HasRows)
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.ERROR_SYSTEM;
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    var tmp = int.Parse(reader["SOBAOCAO"].ToString());
                                    response.Error = false;
                                    response.Data = tmp > 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }
        #endregion

        #region UploadReportFile
        [Route("UploadReportFile")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportFile()
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var httpRequest = HttpContext.Current.Request;
            var test = httpRequest.MapPath("~/Upload/" + httpRequest["MA_DBHC"] + "/" + httpRequest["MA_BAO_CAO"] + "/" + httpRequest["NAM_BC"] + "/" + httpRequest["KY_BC"]);
            Directory.CreateDirectory(test);
            if (httpRequest.Files.Count > 0)
            {

                var indexOfDot = httpRequest.Files[0].FileName.LastIndexOf(".");
                string fileName = DateTime.Now.Ticks + "" + httpRequest.Files[0].FileName.Substring(indexOfDot, httpRequest.Files[0].FileName.Length - indexOfDot);
                try
                {
                    httpRequest.Files[0].SaveAs(test + "/" + fileName + "");
                    using (var connection =
                        new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"]
                            .ConnectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = connection.CreateCommand())
                        {
                            string sql = "UPDATE PHB_" + httpRequest["MA_BAO_CAO"] + " SET"
                                + " NGUOI_SUA = :fileName WHERE"
                                + " MA_DBHC = :maqhns AND"
                                + " NAM_BC = :nam AND"
                                + " KY_BC = :ky";
                            command.CommandType = CommandType.Text;
                            command.CommandText = sql;
                            command.Parameters.Clear();
                            command.Parameters.Add("fileName", OracleDbType.NVarchar2, 150).Value = fileName;
                            command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value = httpRequest["MA_DBHC"];
                            command.Parameters.Add("nam", OracleDbType.Int32).Value = httpRequest["NAM_BC"];
                            command.Parameters.Add("ky", OracleDbType.Int32).Value = httpRequest["KY_BC"];
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_DATA;
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }
            }
            else
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
            }
            return Ok(response);
        }
        #endregion

        #region DowloadFileScan
        [HttpPost]
        [Route("DowloadFileScan")]
        public async Task<HttpResponseMessage> DowloadFileScan(DownloadModel rqmodel)
        {

            HttpResponseMessage response = Request.CreateResponse();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var test = httpRequest.MapPath("~/Upload/" + rqmodel.MA_DBHC + "/" + rqmodel.MA_BAO_CAO + "/" + rqmodel.NAM_BC + "/" + rqmodel.KY_BC);

                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"]
                        .ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        string sql = "SELECT NGUOI_SUA FROM  PHB_" + rqmodel.MA_BAO_CAO + " WHERE"
                            + " MA_DBHC = :maqhns AND"
                            + " NAM_BC = :nam AND"
                            + " KY_BC = :ky";
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.Parameters.Clear();
                        command.Parameters.Add("maqhns", OracleDbType.NVarchar2, 10).Value = rqmodel.MA_DBHC;
                        command.Parameters.Add("nam", OracleDbType.Int32).Value = rqmodel.NAM_BC;
                        command.Parameters.Add("ky", OracleDbType.Int32).Value = rqmodel.KY_BC;
                        var result2 = await command.ExecuteScalarAsync();
                        test += "\\" + result2.ToString();
                    }
                }

                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<object>));
                MemoryStream stream = new MemoryStream();
                FileStream a = File.OpenRead(test);
                a.CopyTo(stream);

                //xmlSerializer.Serialize(stream,listData);
                //if (instance.LOAIBAOCAO == "B03A_X")
                //{
                //    stream = CommonService.ExportXML<DTO>(listData);
                //}
                //else if (instance.LOAIBAOCAO == "B02A_X")
                //{
                //    stream = CommonService.ExportXML<DTOB02A>(listData2);
                //}
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ByteArrayContent(stream.GetBuffer());
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "FileScan.pdf" };
                //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            }
            catch (NullReferenceException ex)
            {
                WriteLogs.LogError(ex);
                //response. = true;
                //response.Message = ErrorMessage.ERROR_DATA;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                //response.Error = true;
                //response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return response;
        }

        #endregion

        #region GetDsDvQhnsByMaDBHC
        [Route("GetDsDvQhnsByMaDBHC/{madbhc}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByMaDBHC(string madbhc)
        {
            var response = new List<SysDvqhns_QuanLyVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        if (!string.IsNullOrEmpty(madbhc))
                        {
                            command.CommandText = "select MA_DVQHNS,TEN_DVQHNS,MA_CHUONG, MA_DBHC from sys_dvqhns_quanly  where MA_DBHC like '" + madbhc + "' order by MA_DBHC";
                            command.Parameters.Clear();
                            using (var dataReader = command.ExecuteReaderAsync())
                            {
                                if (dataReader.Result.HasRows)
                                {
                                    while (dataReader.Result.Read())
                                    {
                                        response.Add(new SysDvqhns_QuanLyVm.ViewModel()
                                        {
                                            MA_DVQHNS = dataReader.Result["MA_DVQHNS"].ToString(),
                                            TEN_DVQHNS = dataReader.Result["TEN_DVQHNS"].ToString(),
                                            MA_CHUONG = dataReader.Result["MA_CHUONG"].ToString(),
                                            MA_DBHC = dataReader.Result["MA_DBHC"].ToString(),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
            return Ok(response);
        }
        #endregion

        #region GetByMaDbhc
        [Route("GetByMaDbhc/{maDbhc}")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetByMaDbhc(string maDbhc)
        {
            var response = new Response<List<SysDvqhns_QuanLyVm.ViewModel>>();
            response.Data = new List<SysDvqhns_QuanLyVm.ViewModel>();

            if (maDbhc == null || maDbhc.Trim() == "")
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            var lstDonVi = new List<SYS_DVQHNS_QUANLY>();

            try
            {
                lstDonVi = await _sysDvqhns_QuanLyService.Queryable().Where(x => x.MA_DBHC == maDbhc).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach (var x in lstDonVi)
            {
                var item = new SysDvqhns_QuanLyVm.ViewModel
                {
                    MA_DVQHNS = x.MA_DVQHNS,
                    TEN_DVQHNS = x.TEN_DVQHNS,
                    MA_CHUONG = x.MA_CHUONG,
                    MA_DBHC = x.MA_DBHC
                };
                response.Data.Add(item);
            }

            return Ok(response);
        }
        #endregion

        //get all
        [Route("GetAll")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetAll()
        {
            var response = new Response<List<SysDvqhns_QuanLyVm.ViewModel>>();
            response.Data = new List<SysDvqhns_QuanLyVm.ViewModel>();

            var lstDonVi = new List<PHB_DM_DVQHNS>();

            try
            {
                lstDonVi = await _dmDVQHNSService.Queryable().ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach (var x in lstDonVi)
            {
                var item = new SysDvqhns_QuanLyVm.ViewModel
                {
                    MA_DVQHNS = x.MA_QHNS,
                    TEN_DVQHNS = x.TEN_QHNS,
                    MA_CHUONG = x.MA_CHUONG,
                    MA_DBHC = x.MA_DBHC
                };
                response.Data.Add(item);
            }

            return Ok(response);
        }

        #region GetListChuong
        [Route("GetListChuong")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListChuong()
        {
            var response = new List<DmChuongVm.ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {

                        command.CommandText = "select * from dm_chuong where ngay_het_hl > current_date or ngay_het_hl is null order by ma_chuong";
                        command.Parameters.Clear();
                        using (var dataReader = command.ExecuteReaderAsync())
                        {
                            if (dataReader.Result.HasRows)
                            {
                                while (dataReader.Result.Read())
                                {
                                    response.Add(new DmChuongVm.ViewModel()
                                    {
                                        Value = dataReader.Result["MA_CHUONG"].ToString(),
                                        Text = dataReader.Result["TEN_CHUONG"].ToString()
                                    });
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
            return Ok(response);
        }
        #endregion
    }

    public class ReceivedReportRQModel
    {
        public string MA_DVQHNS { get; set; }
        public string MA_BAO_CAO { get; set; }
        public string MA_CHUONG { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
        public string TEN_DVQHNS { get; set; }
        public bool Is_MA_QHNS_Wrong { get; set; }

    }

    public class ReceivedReportRQModelXa
    {
        public string MA_DBHC { get; set; }
        public string MA_BAO_CAO { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
    }

    public class ReceivedReportRSModel
    {
        public int ID { get; set; }
        public string MA_BAO_CAO { get; set; }
        public string MA_QHNS { get; set; }
        public string TEN_QHNS { get; set; }
        public string MA_QHNS_QL { get; set; }
        public string MA_CHUONG { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
        public int TRANG_THAI { get; set; }
        public int TRANG_THAI_GUI { get; set; }
        public string REFID { get; set; }
        public string NGUOI_TAO { get; set; }
        public string DON_VI_DT { get; set; }
        public string CAP_DU_TOAN { get; set; }
        public DateTime NGAY_TAO { get; set; }
    }

    public class ReceivedReportRSModelXa
    {
        public int ID { get; set; }
        public string MA_BAO_CAO { get; set; }
        public string MA_DBHC { get; set; }
        public string TEN_DBHC { get; set; }
        public string MA_QHNS { get; set; }
        public string MA_CHUONG { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
        public int TRANG_THAI { get; set; }
        public string REFID { get; set; }
        public string NGUOI_TAO { get; set; }
        public DateTime NGAY_TAO { get; set; }
    }

    public class DeleteReportRQModel
    {
        public string MA_BAO_CAO { get; set; }
        public string REFID { get; set; }
        public int NAM_BC { get; set; }
        public int KY_BC { get; set; }
    }

    public class RQReportSummaryModel
    {
        public string MA_CHUONG { get; set; }
        public int KY_BC { get; set; }
        public int NAM_BC { get; set; }
        public List<string> LSTDONVI { get; set; }
    }
}