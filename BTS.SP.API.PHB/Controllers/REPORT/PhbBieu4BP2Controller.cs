using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU4BP2;
using BTS.SP.PHB.SERVICE.Models.BIEU4BP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4BP2;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using OfficeOpenXml.Style;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu4bp2")]
    [Route("{id?}")]
    public class PhbBieu4BP2Controller : ApiController
    {
        private readonly IPhbBieu4BP2Service _bieu4Bp2Service;
        private readonly IPhbBieu4BP2DetailService _bieu4Bp2DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDmChuongService _dmChuong;

        public PhbBieu4BP2Controller(IPhbBieu4BP2Service bieu4Bp2Service, IPhbBieu4BP2DetailService bieu4Bp2DetailService, IUnitOfWorkAsync unitOfWorkAsync, IDmChuongService dmChuong)
        {
            _bieu4Bp2Service = bieu4Bp2Service;
            _bieu4Bp2DetailService = bieu4Bp2DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var response = new Response();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            var bieu4BP2 = new PHB_BIEU4BP2()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N")
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu4BP2.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu4BP2.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu4BP2.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu4BP2.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu4BP2.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu4BP2.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu4Bp2Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu4BP2.MA_CHUONG) && x.MA_QHNS.Equals(bieu4BP2.MA_QHNS) &&
                                x.NAM_BC == bieu4BP2.NAM_BC && x.KY_BC == bieu4BP2.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu4Bp2Service.Insert(bieu4BP2);

                            var startRowPhan2 = 14;
                            var inRow = 0;
                            while (workSheet.Cells[startRowPhan2 + inRow, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRowPhan2 + inRow, 5].Value.ToString()))
                            {
                                var temp = startRowPhan2 + inRow;
                                var detail = new PHB_BIEU4BP2_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU4BP2_REFID = bieu4BP2.REFID
                                };
                                detail.MA_LOAI = workSheet.Cells[temp, 1].Value != null
                                    ? workSheet.Cells[temp, 1].Value.ToString()
                                    : null;
                                detail.MA_KHOAN = workSheet.Cells[temp, 2].Value != null
                                    ? workSheet.Cells[temp, 2].Value.ToString()
                                    : null;
                                detail.MA_MUC = workSheet.Cells[temp, 3].Value != null
                                    ? workSheet.Cells[temp, 3].Value.ToString()
                                    : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[temp, 4].Value != null
                                    ? workSheet.Cells[temp, 4].Value.ToString()
                                    : null;
                                detail.NOI_DUNG_CHI = workSheet.Cells[temp, 5].Value != null
                                    ? workSheet.Cells[temp, 5].Value.ToString()
                                    : null;
                                detail.NSTN = workSheet.Cells[temp, 7].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 7].Value.ToString())
                                    : 0;
                                detail.VT = workSheet.Cells[temp, 8].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 8].Value.ToString())
                                    : 0;
                                detail.VN = workSheet.Cells[temp, 9].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 9].Value.ToString())
                                    : 0;
                                detail.PKT = workSheet.Cells[temp, 10].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 10].Value.ToString())
                                    : 0;
                                detail.HDKDL = workSheet.Cells[temp, 11].Value != null
                                    ? double.Parse(workSheet.Cells[temp, 11].Value.ToString())
                                    : 0;
                                _bieu4Bp2DetailService.Insert(detail);
                                inRow++;
                            }


                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Error = false;
                                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = ErrorMessage.EMPTY_DATA;
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    WriteLogs.LogError(ex);
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }
                catch (DbEntityValidationException ex)
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


        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            var response = new Response<BIEU4BP2Vm.ViewModel>();
            try
            {
                var model = new BIEU4BP2Vm.ViewModel();
                model.REFID = refid;
                model.DETAIL = await _bieu4Bp2DetailService.Queryable().Where(x => x.PHB_BIEU4BP2_REFID.Equals(refid))
                    .OrderBy(x=>x.MA_LOAI).ThenBy(x=>x.MA_KHOAN).ThenBy(x=>x.MA_MUC).ThenBy(x=>x.MA_TIEU_MUC)
                    .ToListAsync();
                response.Error = false;
                response.Data = model;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(BIEU4BP2Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu4Bp2 = new PHB_BIEU4BP2()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA
                };
                var checkReport = await _bieu4Bp2Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu4Bp2.MA_CHUONG) && x.MA_QHNS.Equals(bieu4Bp2.MA_QHNS) &&
                    x.NAM_BC == bieu4Bp2.NAM_BC && x.KY_BC == bieu4Bp2.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu4Bp2Service.Insert(bieu4Bp2);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU4BP2_REFID = bieu4Bp2.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu4Bp2DetailService.Insert(detail);
                }
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    response.Error = false;
                    response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                }
            }
            catch (DbEntityValidationException ex)
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
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(BIEU4BP2Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu4Bp2 =
                    await _bieu4Bp2Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu4Bp2 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var id in model.LstDelete)
                    {
                        var item = await _bieu4Bp2DetailService.FindByIdAsync(id);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu4Bp2DetailService.Delete(item);
                        }
                    }
                }

                #endregion

                #region add

                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstAdd)
                    {
                        item.ObjectState = ObjectState.Added;
                        item.PHB_BIEU4BP2_REFID = bieu4Bp2.REFID;
                        _bieu4Bp2DetailService.Insert(item);
                    }
                }

                #endregion

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _bieu4Bp2DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.NSTN = item.NSTN;
                            detail.VN = item.VN;
                            detail.VT = item.VT;
                            detail.PKT = item.PKT;
                            detail.HDKDL = item.HDKDL;
                            _bieu4Bp2DetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    bieu4Bp2.NGAY_SUA = DateTime.Now;
                    bieu4Bp2.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu4Bp2Service.Update(bieu4Bp2);
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Error = false;
                        response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }
            }
            catch (DbEntityValidationException ex)
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
            return Ok(response);
        }

        public class ExportParams
        {
            public string MA_CTMTQG { get; set; }
            public string MA_KBNN { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_CAP { get; set; }
            public string MA_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
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

        [Route("Export1C")]
        public HttpResponseMessage Export1C(ExportParams para)
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return result;
        }

        public string CreateExcelFile(ExportParams para)
        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string Date = now.ToString("dd-MM-yyyy");
            var fileNameInput = "BIEU4BP2.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/BIEU4BP2/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInput;
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + Date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                List<ResultItems> items = new List<ResultItems>();
                OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BTSTC.PHB_B04B_II_TT137";
                command.Parameters.Clear();
                command.Parameters.Add("MA_CAP", OracleDbType.NVarchar2, 100).Value = para.MA_CAP;
                command.Parameters.Add("MA_CHUONG", OracleDbType.NVarchar2, 100).Value = para.MA_CHUONG;
                command.Parameters.Add("MA_LOAI", OracleDbType.NVarchar2, 100).Value = para.MA_LOAI;
                command.Parameters.Add("MA_NGANHKT", OracleDbType.NVarchar2, 100).Value = para.MA_NGANHKT;
                command.Parameters.Add("MA_MUC", OracleDbType.NVarchar2, 100).Value = para.MA_MUC;
                command.Parameters.Add("MA_TIEUMUC", OracleDbType.NVarchar2, 100).Value = para.MA_TIEUMUC;
                command.Parameters.Add("MA_DVQHNS", OracleDbType.NVarchar2, 100).Value = para.MA_DVQHNS;
                command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2, 100).Value = para.MA_DBHC;
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
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
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


            var startRow = 13;

            var loaiS = items.Select(x => x.MA_LOAI).Distinct().ToList();           
            ws.Cells[9, 6, 10, 6].Value = "Tổng Số";
            ws.Cells[9, 6, 10, 6].Merge = true;
            ws.Cells[9, 6, 10, 6].Style.Font.Bold = true;
            ws.Cells[9, 6, 10, 6].Style.WrapText = true;

            ws.Cells[12, 6].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[12, 6].Style.Font.Bold = true;
            ws.Cells[12, 7].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[12, 7].Style.Font.Bold = true;
            var dvqhnsS = items.Select(x => x.MA_DVQHNS).Distinct().ToList();

            for (int i = 0; i < loaiS.Count; i++)
            {
                var l = loaiS[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 7].Value = items.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = items.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 7].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = items.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 7].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
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
                            ws.Cells[startRow + n, 7].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 7].Style.Numberformat.Format = "###,###,###,###,###";

                            for (int o = 1; o < dvqhnsS.Count; o++)
                            {
                                var d = dvqhnsS[o];
                                ws.Cells[12, 11 + o].Value = items.Where(x => x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[12, 11 + o].Style.Numberformat.Format = "###,###,###,###,###";
                                ws.Cells[12, 11 + o].Style.Font.Bold = true;
                                ws.Cells[10, 11 + o].Value = d;
                                ws.Cells[startRow + n, 11 + o].Value = items.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t && x.MA_DVQHNS == d).Sum(y => y.GIA_TRI_HACH_TOAN);
                                ws.Cells[startRow + n, 11 + o].Style.Numberformat.Format = "###,###,###,###,###";
                            }
                        }


                       
                        startRow += tieumucS.Count;
                    }
                }
            }

            ws.Cells[8, 12, 9, 12 + dvqhnsS.Count -1].Merge = true;
            ws.Cells[8, 12, 9, 12 + dvqhnsS.Count -1].Value = "Chi tiết từng đơn vị trực thuộc (nếu có đơn vị trực thuộc)";
            ws.Cells[8, 12, 9, 12 + dvqhnsS.Count -1].Style.Font.Bold = true;
            ws.Cells[8, 12, 9, 12 + dvqhnsS.Count -1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[8, 12, 9, 12 + dvqhnsS.Count - 1].Style.WrapText = true;
            ws.SelectedRange[8, 1, startRow -1, 12 + dvqhnsS.Count -1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 1, startRow -1, 12 + dvqhnsS.Count -1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 1, startRow -1, 12  + dvqhnsS.Count -1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[8, 1, startRow -1,  12+ dvqhnsS.Count -1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



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
            var DVBAOCAO = _dmChuong.Queryable().FirstOrDefault(x => x.MA_CHUONG == para.MA_CHUONG).TEN_CHUONG;
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Đơn vị báo cáo :" + DVBAOCAO;
            ws.Cells[3, 1].Value = "Từ ngày hiệu lực :" + para.TUNGAY_HIEULUC.ToString("d") + "đến ngày hiệu lực" + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[4, 1].Value = "Từ ngày kết sổ :" + para.TUNGAY_KETSO.ToString("d") + "đến ngày kết sổ:" + para.DENNGAY_KETSO.ToString("d");
        }

    }
}
