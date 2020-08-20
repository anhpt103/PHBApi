using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU12TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu12TT344;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbbieu12tt344")]
    [Route("{id?}")]
    public class PhbBIEU12TT344Controller : ApiController
    {
        private readonly IPhbBieu12TT344Service _bieu12tt344Service;
        private readonly IPhbBieu12TT344DetailService _bieu12tt344DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBIEU12TT344Controller(IPhbBieu12TT344Service bieu12tt344Service, IPhbBieu12TT344DetailService bieu12tt344DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu12tt344Service = bieu12tt344Service;
            _bieu12tt344DetailService = bieu12tt344DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
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
                            PHB_BIEU12TT344 bieu12tt344 = new PHB_BIEU12TT344()
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
                            bieu12tt344.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu12tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu12tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu12tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu12tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu12tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);


                            var checkReport = await _bieu12tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu12tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu12tt344.MA_QHNS) &&
                                x.NAM_BC == bieu12tt344.NAM_BC && x.KY_BC == bieu12tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu12tt344Service.Insert(bieu12tt344);

                            int startRow = 10;
                            int loai = 0;
                            string phan = string.Empty;
                            string tencongtrinh = string.Empty;
                            #region Tong
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "1. Công trình chuyển tiếp")
                            {
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "2.Công trình khởi công mới")
                                {
                                    PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                    };
                                    tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                    switch (tencongtrinh)
                                    {
                                        case "Tổng số":
                                            loai = 0;
                                            phan = "0";
                                            detail.PHAN = phan;
                                            detail.LOAI = loai;
                                            loai++;
                                            break;
                                    }
                                    detail.PHAN = phan;
                                    detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_DU_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TD_NDG = 0;
                                    }
                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.GIA_TRI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_THANH_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.KHOI_LUONG_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_CAN_DOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NDG = 0;
                                    }
                                    _bieu12tt344DetailService.Insert(detail);
                                    startRow += 1;
                                }
                            }
                            #endregion
                            #region P1
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "2. Công trình khởi công mới")
                            {
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "Tổng số")
                                {
                                    PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                    };
                                    tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                    switch (tencongtrinh)
                                    {
                                        case "1. Công trình chuyển tiếp":
                                            loai = 0;
                                            phan = "1.1";
                                            detail.PHAN = phan;
                                            detail.LOAI = loai;
                                            loai++;
                                            break;

                                        case "Trong đó: hoàn thành trong năm":
                                            loai = 0;
                                            detail.LOAI = loai;
                                            phan = "1.2";
                                            detail.PHAN = phan;
                                            loai++;
                                            break;
                                        default:
                                            loai = 1;
                                            detail.LOAI = loai;
                                            break;
                                    }
                                    detail.PHAN = phan;
                                    detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_DU_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TD_NDG = 0;
                                    }
                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.GIA_TRI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_THANH_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.KHOI_LUONG_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_CAN_DOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NDG = 0;
                                    }
                                    _bieu12tt344DetailService.Insert(detail);
                                    startRow += 1;
                                }
                            }
                                #endregion
                                #region p2
                                while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "3")
                                {
                                    //phan 2
                                    if (workSheet.Cells[startRow, 1].Value?.ToString() != "1. Công trình chuyển tiếp")
                                    {
                                        PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                        };
                                        tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                        switch (tencongtrinh)
                                        {
                                            case "2. Công trình khởi công mới":
                                                loai = 0;
                                                phan = "2.1";
                                                detail.LOAI = loai;
                                                detail.PHAN = phan;
                                                loai++;
                                                break;

                                            case "Trong đó: hoàn thành trong năm":
                                                loai = 0;
                                                phan = "2.2";
                                                detail.LOAI = loai;
                                                detail.PHAN = phan;
                                                loai++;
                                                break;
                                            default:
                                                loai = 1;
                                                detail.LOAI = loai;
                                                break;
                                    }
                                        detail.PHAN = phan;
                                        detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                        detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                        if (workSheet.Cells[startRow, 3].Value != null)
                                        {
                                            try
                                            {
                                                detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.TONG_SO_DU_TOAN = 0;
                                        }
                                        if (workSheet.Cells[startRow, 4].Value != null)
                                        {
                                            try
                                            {
                                                detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.TD_NDG = 0;
                                        }
                                        if (workSheet.Cells[startRow, 5].Value != null)
                                        {
                                            try
                                            {
                                                detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.GIA_TRI = 0;
                                        }
                                        if (workSheet.Cells[startRow, 6].Value != null)
                                        {
                                            try
                                            {
                                                detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.TONG_SO_THANH_TOAN = 0;
                                        }
                                        if (workSheet.Cells[startRow, 7].Value != null)
                                        {
                                            try
                                            {
                                                detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = 0;
                                        }
                                        if (workSheet.Cells[startRow, 8].Value != null)
                                        {
                                            try
                                            {
                                                detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.NGUON_CAN_DOI = 0;
                                        }
                                        if (workSheet.Cells[startRow, 9].Value != null)
                                        {
                                            try
                                            {
                                                detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.NDG = 0;
                                        }
                                        _bieu12tt344DetailService.Insert(detail);
                                        startRow += 1;
                                    }
                                }
                                #endregion
                            


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
                            response.Message = ErrorMessage.ERROR_DATA;
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


        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            Response<string> response = new Response<string>();
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
                            PHB_BIEU12TT344 bieu12tt344 = new PHB_BIEU12TT344()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N")
                            };
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            bieu12tt344.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu12tt344.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = "1032433";
                            //bieu12tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu12tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu12tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu12tt344.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu12tt344.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu12tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu12tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);


                            var checkReport = await _bieu12tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu12tt344.MA_DBHC) && x.MA_QHNS.Equals(bieu12tt344.MA_QHNS) &&
                                x.NAM_BC == bieu12tt344.NAM_BC && x.KY_BC == bieu12tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu12tt344Service.Insert(bieu12tt344);

                            int startRow = 10;
                            int loai = 0;
                            string phan = string.Empty;
                            string tencongtrinh = string.Empty;
                            #region Tong
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "1. Công trình chuyển tiếp")
                            {
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "2.Công trình khởi công mới")
                                {
                                    PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                    };
                                    tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                    switch (tencongtrinh)
                                    {
                                        case "Tổng số":
                                            loai = 0;
                                            phan = "0";
                                            detail.PHAN = phan;
                                            detail.LOAI = loai;
                                            loai++;
                                            break;
                                    }
                                    detail.PHAN = phan;
                                    detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_DU_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TD_NDG = 0;
                                    }
                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.GIA_TRI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_THANH_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.KHOI_LUONG_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_CAN_DOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NDG = 0;
                                    }
                                    _bieu12tt344DetailService.Insert(detail);
                                    startRow += 1;
                                }
                            }
                            #endregion
                            #region P1
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "2. Công trình khởi công mới")
                            {
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "Tổng số")
                                {
                                    PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                    };
                                    tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                    switch (tencongtrinh)
                                    {
                                        case "1. Công trình chuyển tiếp":
                                            loai = 0;
                                            phan = "1.1";
                                            detail.PHAN = phan;
                                            detail.LOAI = loai;
                                            loai++;
                                            break;

                                        case "Trong đó: hoàn thành trong năm":
                                            loai = 0;
                                            detail.LOAI = loai;
                                            phan = "1.2";
                                            detail.PHAN = phan;
                                            loai++;
                                            break;
                                        default:
                                            loai = 1;
                                            detail.LOAI = loai;
                                            break;
                                    }
                                    detail.PHAN = phan;
                                    detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_DU_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TD_NDG = 0;
                                    }
                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.GIA_TRI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_THANH_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.KHOI_LUONG_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_CAN_DOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NDG = 0;
                                    }
                                    _bieu12tt344DetailService.Insert(detail);
                                    startRow += 1;
                                }
                            }
                            #endregion
                            #region p2
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()) && workSheet.Cells[startRow, 1].Value?.ToString() != "3")
                            {
                                //phan 2
                                if (workSheet.Cells[startRow, 1].Value?.ToString() != "1. Công trình chuyển tiếp")
                                {
                                    PHB_BIEU12TT344_DETAIL detail = new PHB_BIEU12TT344_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU12TT344_REFID = bieu12tt344.REFID,
                                    };
                                    tencongtrinh = workSheet.Cells[startRow, 1].Value?.ToString();
                                    switch (tencongtrinh)
                                    {
                                        case "2. Công trình khởi công mới":
                                            loai = 0;
                                            phan = "2.1";
                                            detail.LOAI = loai;
                                            detail.PHAN = phan;
                                            loai++;
                                            break;

                                        case "Trong đó: hoàn thành trong năm":
                                            loai = 0;
                                            phan = "2.2";
                                            detail.LOAI = loai;
                                            detail.PHAN = phan;
                                            loai++;
                                            break;
                                        default:
                                            loai = 1;
                                            detail.LOAI = loai;
                                            break;
                                    }
                                    detail.PHAN = phan;
                                    detail.TEN_CONG_TRINH = workSheet.Cells[startRow, 1].Value?.ToString();
                                    detail.THOI_GIAN = DateTime.Parse(workSheet.Cells[startRow, 2].Value.ToString());
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_DU_TOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_DU_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.TD_NDG = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TD_NDG = 0;
                                    }
                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.GIA_TRI = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.GIA_TRI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO_THANH_TOAN = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO_THANH_TOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.KHOI_LUONG_NAM_TRUOC = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.KHOI_LUONG_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_CAN_DOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_CAN_DOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NDG = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NDG = 0;
                                    }
                                    _bieu12tt344DetailService.Insert(detail);
                                    startRow += 1;
                                }
                            }
                            #endregion



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
                            response.Message = ErrorMessage.ERROR_DATA;
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
        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<BIEU12TT344Vm.ViewModel> response = new Response<BIEU12TT344Vm.ViewModel>();
            try
            {
                BIEU12TT344Vm.ViewModel data = new BIEU12TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _bieu12tt344DetailService.Queryable().Where(x => x.PHB_BIEU12TT344_REFID.Equals(refid))
                    .OrderBy(x => x.PHAN).ThenBy(x => x.LOAI)
                    .ToListAsync();
                response.Error = false;
                response.Data = data;
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
        public async Task<IHttpActionResult> Post(BIEU12TT344Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_BIEU12TT344 bieu12tt344 = new PHB_BIEU12TT344()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N")
                };
                var checkReport = await _bieu12tt344Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu12tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu12tt344.MA_QHNS) &&
                    x.NAM_BC == bieu12tt344.NAM_BC && x.KY_BC == bieu12tt344.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu12tt344Service.Insert(bieu12tt344);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU12TT344_REFID = bieu12tt344.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu12tt344DetailService.Insert(detail);
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put(BIEU12TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU12TT344 bieu12tt344 =
                    await _bieu12tt344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu12tt344 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU12TT344_DETAIL item = await _bieu12tt344DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu12tt344DetailService.Delete(item);
                        }
                    }
                }
                #endregion

                #region Add

                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstAdd)
                    {
                        item.ObjectState = ObjectState.Added;
                        item.PHB_BIEU12TT344_REFID = model.REFID;
                        _bieu12tt344DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU12TT344_DETAIL detail = await _bieu12tt344DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CONG_TRINH = item.TEN_CONG_TRINH;
                            detail.THOI_GIAN = item.THOI_GIAN;
                            detail.TONG_SO_DU_TOAN = item.TONG_SO_DU_TOAN;
                            detail.TD_NDG = item.TD_NDG;
                            detail.GIA_TRI = item.GIA_TRI;
                            detail.TONG_SO_THANH_TOAN = item.TONG_SO_THANH_TOAN;
                            detail.KHOI_LUONG_NAM_TRUOC = item.KHOI_LUONG_NAM_TRUOC;
                            detail.NGUON_CAN_DOI = item.NGUON_CAN_DOI;
                            detail.NDG = item.NDG;
                            _bieu12tt344DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu12tt344.NGAY_SUA = DateTime.Now;
                    bieu12tt344.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu12tt344Service.Update(bieu12tt344);

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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _bieu12tt344Service.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }
    }
}
