using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Helper;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity;
using BTS.SP.PHB.SERVICE.REPORT.B01BDG_TT137;
using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;
using BTS.SP.PHB.SERVICE.REPORT.Bm48TT342;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using BTS.SP.API.PHB.ViewModels.REPORT.B01BDG_TT137;
using BTS.SP.PHB.ENTITY;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB01BDG_TT137")]
    [Route("{id?}")]
    public class PhbB01bdgTT137Controller : ApiController
    {
        private readonly IPhbBm48TT342Service _bm48TT342Service_2;
        private readonly IPhbBm48TT342DetailService _bm48TT342DetailService_2;

        private readonly IPhbB01bdgTT137Service _bmB01bdgTT137Service;
        private readonly IPhbB01bdgTT137TemplateService _bmB01bdgTT137TemplateService;
        private readonly IPhbB01bdgTT137DetailService _bmB01bdgTT137DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB01bdgTT137Controller(IPhbBm48TT342Service bm48TT342Service_2, IPhbBm48TT342DetailService bm48TT342DetailService_2, 
            IPhbB01bdgTT137Service bmB01bdgTT137Service,
            IPhbB01bdgTT137TemplateService bmB01bdgTT137TemplateService,
            IPhbB01bdgTT137DetailService bmB01bdgTT137DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bm48TT342DetailService_2 = bm48TT342DetailService_2;
            _bm48TT342Service_2 = bm48TT342Service_2;
            _bmB01bdgTT137Service = bmB01bdgTT137Service;
            _bmB01bdgTT137TemplateService = bmB01bdgTT137TemplateService;
            _bmB01bdgTT137DetailService = bmB01bdgTT137DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region Upload
        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var b01Bcqt = new PHB_B01BDG_TT137()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };

                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            b01Bcqt.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b01Bcqt.MA_QHNS = httpRequest["MA_QHNS"];
                            b01Bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
                            //b01Bcqt.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01Bcqt.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            //if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có kỳ báo cáo."
                            //});
                            b01Bcqt.KY_BC = 0;

                            var checkReport = await _bmB01bdgTT137Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(b01Bcqt.MA_CHUONG) && x.MA_QHNS.Equals(b01Bcqt.MA_QHNS) &&
                                x.NAM_BC == b01Bcqt.NAM_BC && x.KY_BC == b01Bcqt.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bmB01bdgTT137Service.Insert(b01Bcqt);

                            int start_Row = 8;
                            int end_Row = 12;
                            int count = 1;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                if (string.IsNullOrEmpty(workSheet.Cells[r, 2].Text))
                                {
                                    //break;
                                }
                                else
                                {
                                    PHB_B01BDG_TT137_DETAIL obj = new PHB_B01BDG_TT137_DETAIL() { PHB_B01BDG_TT137_REFID = b01Bcqt.REFID, ObjectState = ObjectState.Added };
                                    double doichieu = 0;
                                    double thuchien = 0;

                                    obj.STT = count;
                                    obj.MA_CHI_TIEU = workSheet.Cells[r, 1].Value?.ToString();
                                    obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                    obj.STT_CHI_TIEU = workSheet.Cells[r, 3].Value?.ToString();
                                    double.TryParse(workSheet.Cells[r, 4].Text.ToString(), out thuchien);
                                    double.TryParse(workSheet.Cells[r, 5].Text.ToString(), out doichieu);
                                    obj.SO_DOI_CHIEU = doichieu;
                                    obj.THUC_HIEN = thuchien;
                                    obj.CHENH_LECH = doichieu - thuchien;

                                    _bmB01bdgTT137DetailService.Insert(obj);
                                    count++;
                                }
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b01Bcqt.REFID;
                                response.Error = false;
                                response.Message = "Cập nhật thành công.";
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = "Lỗi cập nhật dữ liệu.";
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = "Lỗi định dạng dữ liệu.";
                        }
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Không có dữ liệu upload.";
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        #endregion

        //#region UploadReport
        //[Route("UploadReport")]
        //[HttpPost]
        //public IHttpActionResult UploadReport()
        //{
        //    var response = new Response();
        //    var httpRequest = HttpContext.Current.Request;


        //    //validate model
        //    if (httpRequest.Files.Count == 0)
        //    {
        //        response.Error = true;
        //        response.Message = ErrorMessage.EMPTY_DATA;
        //        return Ok(response);
        //    }
        //    //get informations 

        //    string chuong = httpRequest["MA_CHUONG"];
        //    string nguoiTao = "";
        //    string maDonVi = httpRequest["MA_QHNS"];
        //    string tenDonVi = httpRequest["TEN_QHNS"];
        //    int nam = int.Parse(httpRequest["NAM_BC"]);
        //    int ky = 0;
        //    try
        //    {
        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        nguoiTao = identity.Name;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        WriteLogs.LogError(ex);
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //        return Ok(response);
        //    }

        //    var form = new PHB_B01BDG_TT137
        //    {
        //        MA_CHUONG = chuong,
        //        MA_QHNS = maDonVi,
        //        TEN_QHNS = tenDonVi,
        //        NGAY_SUA = null,
        //        NGUOI_SUA = null,
        //        NGUOI_TAO = nguoiTao,
        //        NGAY_TAO = DateTime.Now,
        //        REFID = Guid.NewGuid().ToString("n"),
        //        TRANG_THAI = 0,
        //        NAM_BC = nam,
        //        KY_BC = ky
        //    };

        //    _bmB01bdgTT137Service.Insert(form);

        //    var lstTemplate = _bmB01bdgTT137TemplateService.Queryable().OrderBy(tpl => tpl.STT).ToList();
        //    var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.TEN_CHI_TIEU).ToList();

        //    using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
        //    {
        //        var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
        //        if (sheet == null)
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //            return Ok(response);
        //        }

        //        var startRow = GetStartRow(sheet, lstChiTieuTemplate.FirstOrDefault());
        //        var endRow = GetEndRow(sheet, lstChiTieuTemplate.LastOrDefault());
        //        var stt_SapXep = 1;

        //        for (int row = startRow; row <= endRow; row++)
        //        {
        //            try
        //            {
        //                var detail = new PHB_B01BDG_TT137_DETAIL
        //                {
        //                    MA_CHI_TIEU = sheet.Cells[row, 1].Value?.ToString(),
        //                    TEN_CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
        //                    IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
        //                    IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
        //                    THUC_HIEN = double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    SO_DOI_CHIEU = double.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0"),
        //                    CHENH_LECH = double.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0") - double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    PHB_B01BDG_TT137_REFID = form.REFID,
        //                    STT = stt_SapXep,
        //                };
        //                _bmB01bdgTT137DetailService.Insert(detail);

        //            }
        //            catch (Exception ex)
        //            {
        //                response.Error = true;
        //                response.Message = ErrorMessage.ERROR_DATA;
        //                return Ok(response);
        //            }

        //            stt_SapXep++;
        //        }
        //    }
        //    try
        //    {
        //        _unitOfWorkAsync.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        WriteLogs.LogError(ex);
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //        return Ok(response);
        //    }

        //    response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //    return Ok(response);

        //}

        //private int GetStartRow(ExcelWorksheet sheet, string firstChiTieuTpl)
        //{
        //    var firstRow = sheet.Dimension.Start.Row;
        //    var lastRow = sheet.Dimension.End.Row;

        //    for (int row = firstRow; row <= lastRow; row++)
        //    {
        //        if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == firstChiTieuTpl.Trim().ToLower())
        //        {
        //            return row;
        //        }
        //    }

        //    throw new Exception();
        //}

        //private int GetEndRow(ExcelWorksheet sheet, string lastChiTieuTpl)
        //{
        //    var firstRow = sheet.Dimension.Start.Row;
        //    var lastRow = sheet.Dimension.End.Row;

        //    for (int row = lastRow; row >= firstRow; row--)
        //    {
        //        if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == lastChiTieuTpl.Trim().ToLower())
        //        {
        //            return row;
        //        }
        //    }

        //    throw new Exception();
        //}
        //private bool IsOptional(List<PHB_B01BDG_TT137_TEMPLATE> lstChiTieuTemplate, string chiTieu)
        //{
        //    if (lstChiTieuTemplate
        //        .Where(
        //            tpl => tpl.TEN_CHI_TIEU.ToLower().Trim() == chiTieu.ToLower().Trim())
        //        .Count() > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //#endregion

        public class ViewModel
        {
            public string MA_SO { get; set; }
            public string COT_1 { get; set; }
        }

        [Route("GetColunmSoBC/{NamBc}/{MaDvQhNs}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDsDvQhnsByUser(string NamBc, string MaDvQhNs)
        {
            var response = new List<ViewModel>();
            try
            {
                using (var connection =
                    new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {

                        command.CommandType = CommandType.Text;
                        command.Parameters.Clear();
                        command.CommandText = "select MA_SO, COT_1 from PHB_B05_BCTC_DETAIL " +
                        "where (MA_SO like '60' or MA_SO like '61' or MA_SO like '62' or MA_SO like '63' or MA_SO like '65'" +
                                " or MA_SO like '66' or MA_SO like '67' or MA_SO like '68' or MA_SO like '69' or "+
                                "MA_SO like '70' or MA_SO like '71' or MA_SO like '72' or MA_SO like '73') " +
                                "and PHB_B05_BCTC_REFID in (select REFID from PHB_B05_BCTC where MA_QHNS like '"+ MaDvQhNs+"' and NAM_BC like '" + NamBc+ "') " +
                                "ORDER BY phb_b05_bctc_detail.ma_so ASC ";
                        command.Parameters.Clear();
                        using (var dataReader = command.ExecuteReaderAsync())
                        {
                            if (dataReader.Result.HasRows)    
                            {
                                while (dataReader.Result.Read())
                                {
                                    response.Add(new ViewModel()
                                    {
                                        MA_SO = dataReader.Result["MA_SO"].ToString(),
                                        COT_1 = dataReader.Result["COT_1"].ToString(),
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

        public class ExportParams
        {
            public string MA_DVQHNS { get; set; }
            public int NAM_BC { get; set; }

        }

        #region get template
        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTeamplate()
        {
            var response = new Response<List<PHB_B01BDG_TT137_TEMPLATE>>();
            try
            {
                response.Data = _bmB01bdgTT137TemplateService.Queryable().ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            response.Error = false;
            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }
        #endregion


        #region thêm mới
        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(B01BDG_TT137ViewModel.AddModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;


            var data = new PHB_B01BDG_TT137
            {
                REFID = Guid.NewGuid().ToString("n"),
                MA_CHUONG = model.data.MA_CHUONG,
                MA_QHNS = model.data.MA_QHNS,
                TEN_QHNS = model.data.TEN_QHNS,
                MA_QHNS_CHA = "",
                MA_DV_SDNS = "",
                MA_SO_SDNS = "",
                MA_KBNN = "",
                NAM_BC = model.data.NAM_BC,
                KY_BC = model.data.KY_BC,
                TRANG_THAI = 0,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = identity.Name,
                NGUOI_SUA = null,
                NGAY_SUA = null
            };
            _bmB01bdgTT137Service.Insert(data);

            foreach (var item in model.datadetail)
            {
                item.PHB_B01BDG_TT137_REFID = data.REFID;
                _bmB01bdgTT137DetailService.Insert(item);
            }

            var checkExits = _bmB01bdgTT137Service.Queryable().Where(x => x.MA_QHNS == data.MA_QHNS && x.NAM_BC == data.NAM_BC && x.KY_BC == data.KY_BC).Count();
            if (checkExits > 0)
            {
                respone.Error = true;
                respone.Message = ErrorMessage.EXITS_REPORT;
                return Ok(respone);
            }

            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }
        #endregion

        #region Get Detail
        [Route("getDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> getDetailByRefId(string refid)
        {
            var result = new Response<List<PHB_B01BDG_TT137_DETAIL>>();
            try
            {
                result.Data = _bmB01bdgTT137DetailService.Queryable().Where(x => x.PHB_B01BDG_TT137_REFID == refid).OrderBy(x => x.STT).ToList();
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ErrorMessage.ERROR_SYSTEM;
            }
            result.Error = false;
            result.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(result);
        }
        #endregion

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHB_B01BDG_TT137_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_B01BDG_TT137();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHB_B01BDG_TT137_REFID;
                report = await _bmB01bdgTT137Service.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }
            if (report == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //check if report is already censored or not
            if (report.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
                response.Error = true;
                return Ok(response);
            }

            //add informations about editing user and editing date
            report.NGAY_SUA = DateTime.Now;
            report.NGUOI_SUA = RequestContext.Principal.Identity.Name;
            report.ObjectState = ObjectState.Modified;
            _bmB01bdgTT137Service.Update(report);

            //get list details by refid
            var lstDetail = new List<PHB_B01BDG_TT137_DETAIL>();
            try
            {
                lstDetail = await _bmB01bdgTT137DetailService.Queryable().Where(detail => detail.PHB_B01BDG_TT137_REFID == report.REFID).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            //loop to edit each detail
            foreach (var detail in lstDetail)
            {
                detail.STT_CHI_TIEU = model.Where(e => e.ID == detail.ID).FirstOrDefault().STT_CHI_TIEU;
                detail.MA_CHI_TIEU = model.Where(e => e.ID == detail.ID).FirstOrDefault().MA_CHI_TIEU;
                detail.TEN_CHI_TIEU = model.Where(e => e.ID == detail.ID).FirstOrDefault().TEN_CHI_TIEU;
                detail.SO_DOI_CHIEU = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_DOI_CHIEU;
                detail.THUC_HIEN = model.Where(e => e.ID == detail.ID).FirstOrDefault().THUC_HIEN;
                detail.CHENH_LECH = model.Where(e => e.ID == detail.ID).FirstOrDefault().CHENH_LECH;
                detail.ObjectState = ObjectState.Modified;
                _bmB01bdgTT137DetailService.Update(detail);
            }

            try
            {
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        public class EditModel
        {
            public string refid { get; set; }
            public List<PHB_B01BDG_TT137_DETAIL> datadetail { get; set; }
        }

        #region ApproveReport 
        [Route("ApproveReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ApproveReport(EditModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var form = new PHB_B01BDG_TT137();
            var listDetail = new List<PHB_B01BDG_TT137_DETAIL>();

            if (model.datadetail.Count <= 0)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }
            try
            {
                form = _bmB01bdgTT137Service.Queryable().FirstOrDefault(x => x.REFID == model.refid);
                listDetail = _bmB01bdgTT137DetailService.Queryable().Where(x => x.PHB_B01BDG_TT137_REFID == form.REFID).ToList();

            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            form.TRANG_THAI = 1;
            form.NGUOI_SUA = identity.Name;
            form.NGAY_SUA = DateTime.Now;
            form.ObjectState = ObjectState.Modified;

            try
            {
                foreach (var item in listDetail)
                {
                    _bmB01bdgTT137DetailService.Delete(item);
                }

                foreach (var item in model.datadetail)
                {
                    item.ID = 0;
                    item.PHB_B01BDG_TT137_REFID = form.REFID;
                    _bmB01bdgTT137DetailService.Insert(item);
                }
            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            try
            {
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }
        #endregion

        [Route("ExportExcel")]

        public HttpResponseMessage ExportExcel(ExportParams model)
        {
            var lstPhi = new List<PHB_BM48_TT342_DETAIL>();
            var lstLePhi = new List<PHB_BM48_TT342_DETAIL>();

            HttpResponseMessage result = null;
            string file = null;
            var tempObj = _bm48TT342Service_2.Queryable().FirstOrDefault(x => x.MA_QHNS == model.MA_DVQHNS && x.NAM_BC == model.NAM_BC);
            if (tempObj == null)
            {
                //result.Error = true;
                //result.Message = "Chưa nhập báo cáo Biểu 48 - TT342";
            }
            else
            {
                lstPhi.AddRange(_bm48TT342DetailService_2.Queryable().Where(x => x.PHB_BM48_TT342_REFID == tempObj.REFID && x.MA_CHI_TIEU == "I-1-1.2").OrderBy(x => x.STT).ToList());
                lstLePhi.AddRange(_bm48TT342DetailService_2.Queryable().Where(x => x.PHB_BM48_TT342_REFID == tempObj.REFID && x.MA_CHI_TIEU == "I-3-3.1").OrderBy(x => x.STT).ToList());
            }

            file = _CreateExcelFile1C(lstPhi, lstLePhi);
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

        public string _CreateExcelFile1C(List<PHB_BM48_TT342_DETAIL> lstPhi, List<PHB_BM48_TT342_DETAIL> lstLePhi)

        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU4BP1/BIEU4B1TT137.xlsx";
            var fileNameInPut = "B01A_TT137_Template.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/B01A_TT137/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInPut;
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcel1C(workSheet, lstPhi, lstLePhi);
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

        public void _BindingDataToExcel1C(ExcelWorksheet ws, List<PHB_BM48_TT342_DETAIL> lstPhi, List<PHB_BM48_TT342_DETAIL> lstLePhi)
        {
            var starRow = 9;
            var starCol = 1;
            var STT = 2;
            var nextSTT = 0;
            int count = 1;
            ws.Cells[8, 1].Value = "I";
            ws.Cells[8, 2].Value = "PHÍ";


            foreach (var item in lstPhi)
            {
                ws.Cells[starRow, 1].Value = count;
                ws.Cells[starRow, 2].Value = item.TEN_CHI_TIEU;
                starRow++;
                ws.Cells[starRow, 2].Value = "- Tổng số thu";
                starRow++;
                ws.Cells[starRow, 2].Value = "- Số phải nộp NSNN";
                starRow++;
                ws.Cells[starRow, 2].Value = "- Số được khấu trừ hoặc để lại";
                starRow++;
                count++;

            }
            ws.Cells[starRow, 1].Value = "II";
            ws.Cells[starRow, 2].Value = "LỆ PHÍ";
            starRow++;
            count = 1;
            foreach (var item in lstLePhi)
            {
                ws.Cells[starRow, 1].Value = count;
                ws.Cells[starRow, 2].Value = item.TEN_CHI_TIEU;
                starRow++;
                count++;
            }
            starRow++;
            starRow++;

            //ws.Cells[starRow, 1, starRow+1, 4].Merge = true;
            ws.Cells[starRow, 1].Value = "Ghi chú: Số liệu xét duyệt, thẩm định biểu này trên cơ sở Thuyết minh báo cáo quyết toán theo Mẫu số 03/BCQT ban hành kèm theo Thông tư số 107/2017/TT-BTC";

        }

    }
}
