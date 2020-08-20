using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.SERVICE.Models.C_B06X;
using BTS.SP.PHB.SERVICE.REPORT.C_B06X;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbC_B06X")]
    [Route("{id?}")]
    public class PhbC_B06XController : ApiController
    {
        private readonly IPhbC_B06XService _C_B06XService;
        private readonly IPhbC_B06XDetailService _C_B06XDetailService;
        private readonly IPhbC_B06XTemplateService _C_B06XTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbC_B06XController(IPhbC_B06XService C_B06XService, IPhbC_B06XDetailService C_B06XDetailService, IPhbC_B06XTemplateService C_B06XTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _C_B06XService = C_B06XService;
            _C_B06XDetailService = C_B06XDetailService;
            _C_B06XTemplateService = C_B06XTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            //var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.ToArray();
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B06X C_B06X = new PHB_C_B06X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            MA_DBHC_CHA = madbhc[3].Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            C_B06X.MA_CHUONG = "423";
                            C_B06X.MA_QHNS = "27";
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã địa bàn hành chính"
                            });
                            C_B06X.MA_DBHC = httpRequest["MA_DBHC"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            C_B06X.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            C_B06X.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _C_B06XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(C_B06X.MA_CHUONG) && x.MA_DBHC.Equals(C_B06X.MA_DBHC) &&
                                x.NAM_BC == C_B06X.NAM_BC && x.KY_BC == C_B06X.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B06XService.Insert(C_B06X);

                            #region insert Quỹ công chuyên dùng
                            int startRowQC = 12;
                            bool isSDDK, isTONG_THU, isTONG_CHI, isCON_LAI;
                            double _SDDK, _TONG_THU, _TONG_CHI, _CON_LAI;
                            while (workSheet.Cells[startRowQC, 3].Value != null)
                            {
                                if (workSheet.Cells[startRowQC, 1].Value != null &&
                                    (workSheet.Cells[startRowQC, 1].Value.ToString().Trim().Equals("2") 
                                        || workSheet.Cells[startRowQC, 1].Value.ToString().Trim().Equals("3")))
                                {
                                    break;
                                }
                                else
                                {
                                    if (workSheet.Cells[startRowQC, 1].Value != null &&
                                        workSheet.Cells[startRowQC, 1].Value.ToString().Trim().Equals("1"))
                                    {
                                        startRowQC += 1;
                                    }
                                    else
                                    {
                                        PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_C_B06X_REFID = C_B06X.REFID,
                                            LOAI = 1,
                                        };
                                        detail.STT_CHI_TIEU = workSheet.Cells[startRowQC, 1].Value != null ? workSheet.Cells[startRowQC, 1].Value.ToString() : null;
                                        detail.MA_CHITIEU = workSheet.Cells[startRowQC, 2].Value != null ? workSheet.Cells[startRowQC, 2].Value.ToString() : null;
                                        detail.TEN_CHITIEU = workSheet.Cells[startRowQC, 3].Value != null ? workSheet.Cells[startRowQC, 3].Value.ToString() : null;
                                        isSDDK = double.TryParse(workSheet.Cells[startRowQC, 4].Value == null ? "0" : workSheet.Cells[startRowQC, 4].Value.ToString(), out _SDDK);
                                        detail.SDDK = isSDDK ? _SDDK : 0;
                                        isTONG_THU = double.TryParse(workSheet.Cells[startRowQC, 5].Value == null ? "0" : workSheet.Cells[startRowQC, 5].Value.ToString(), out _TONG_THU);
                                        detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                        isTONG_CHI = double.TryParse(workSheet.Cells[startRowQC, 6].Value == null ? "0" : workSheet.Cells[startRowQC, 6].Value.ToString(), out _TONG_CHI);
                                        detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                        isCON_LAI = double.TryParse(workSheet.Cells[startRowQC, 7].Value == null ? "0" : workSheet.Cells[startRowQC, 7].Value.ToString(), out _CON_LAI);
                                        detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                        _C_B06XDetailService.Insert(detail);
                                        startRowQC += 1;
                                    }
                                }
                            }
                            #endregion
                            #region insert Hoạt động sự nghiệp

                            int startRowHD = startRowQC;
                            while (workSheet.Cells[startRowHD, 3].Value != null)
                            {
                                if (workSheet.Cells[startRowHD, 1].Value != null && workSheet.Cells[startRowHD, 1].Value.ToString().Trim().Equals("3"))
                                    break;
                                if (workSheet.Cells[startRowHD, 1].Value != null &&
                                workSheet.Cells[startRowHD, 1].Value.ToString().Trim().Equals("2")) startRowHD += 1;
                                else
                                {
                                    PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_C_B06X_REFID = C_B06X.REFID,
                                        LOAI = 2,
                                    };
                                    detail.STT_CHI_TIEU = workSheet.Cells[startRowHD, 1].Value != null ? workSheet.Cells[startRowHD, 1].Value.ToString() : null;
                                    detail.MA_CHITIEU = workSheet.Cells[startRowHD, 2].Value != null ? workSheet.Cells[startRowHD, 2].Value.ToString() : null;
                                    detail.TEN_CHITIEU = workSheet.Cells[startRowHD, 3].Value != null ? workSheet.Cells[startRowHD, 3].Value.ToString() : null;
                                    isSDDK = double.TryParse(workSheet.Cells[startRowHD, 4].Value == null ? "0" : workSheet.Cells[startRowHD, 4].Value.ToString(), out _SDDK);
                                    detail.SDDK = isSDDK ? _SDDK : 0;
                                    isTONG_THU = double.TryParse(workSheet.Cells[startRowHD, 5].Value == null ? "0" : workSheet.Cells[startRowHD, 5].Value.ToString(), out _TONG_THU);
                                    detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                    isTONG_CHI = double.TryParse(workSheet.Cells[startRowHD, 6].Value == null ? "0" : workSheet.Cells[startRowHD, 6].Value.ToString(), out _TONG_CHI);
                                    detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                    isCON_LAI = double.TryParse(workSheet.Cells[startRowHD, 7].Value == null ? "0" : workSheet.Cells[startRowHD, 7].Value.ToString(), out _CON_LAI);
                                    detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                    _C_B06XDetailService.Insert(detail);
                                    startRowHD += 1;
                                }
                            }
                            #endregion
                            #region insert Hoạt động tài chính khác
                            int startRowHDK = startRowHD;
                            if (workSheet.Cells[startRowHDK, 1].Value != null &&
                                workSheet.Cells[startRowHDK, 1].Value.ToString().Trim().Equals("3"))
                                startRowHDK += 1;
                            while (workSheet.Cells[startRowHDK, 3].Value != null)
                            {
                                PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B06X_REFID = C_B06X.REFID,
                                    LOAI = 3,
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[startRowHDK, 1].Value != null ? workSheet.Cells[startRowHDK, 1].Value.ToString() : null;
                                detail.MA_CHITIEU = workSheet.Cells[startRowHDK, 2].Value != null ? workSheet.Cells[startRowHDK, 2].Value.ToString() : null;
                                detail.TEN_CHITIEU = workSheet.Cells[startRowHDK, 3].Value != null ? workSheet.Cells[startRowHDK, 3].Value.ToString() : null;
                                isSDDK = double.TryParse(workSheet.Cells[startRowHDK, 4].Value == null ? "0" : workSheet.Cells[startRowHDK, 4].Value.ToString(), out _SDDK);
                                detail.SDDK = isSDDK ? _SDDK : 0;
                                isTONG_THU = double.TryParse(workSheet.Cells[startRowHDK, 5].Value == null ? "0" : workSheet.Cells[startRowHDK, 5].Value.ToString(), out _TONG_THU);
                                detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                isTONG_CHI = double.TryParse(workSheet.Cells[startRowHDK, 6].Value == null ? "0" : workSheet.Cells[startRowHDK, 6].Value.ToString(), out _TONG_CHI);
                                detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                isCON_LAI = double.TryParse(workSheet.Cells[startRowHDK, 7].Value == null ? "0" : workSheet.Cells[startRowHDK, 7].Value.ToString(), out _CON_LAI);
                                detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                _C_B06XDetailService.Insert(detail);
                                startRowHDK += 1;
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

        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B06X C_B06X = new PHB_C_B06X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            MA_DBHC_CHA = claimMaDbhc?.Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            C_B06X.MA_CHUONG = "423";
                            C_B06X.MA_QHNS = "1032433";
                            C_B06X.MA_DBHC = httpRequest["MA_DBHC"];
                            C_B06X.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            C_B06X.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            C_B06X.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _C_B06XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(C_B06X.MA_DBHC) && x.MA_DBHC.Equals(C_B06X.MA_DBHC) &&
                                x.NAM_BC == C_B06X.NAM_BC && x.KY_BC == C_B06X.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B06XService.Insert(C_B06X);

                            #region insert Quỹ công chuyên dùng
                            int startRowQC = 12;
                            bool isSDDK, isTONG_THU, isTONG_CHI, isCON_LAI;
                            double _SDDK, _TONG_THU, _TONG_CHI, _CON_LAI;
                            while (workSheet.Cells[startRowQC, 3].Value != null)
                            {
                                if (workSheet.Cells[startRowQC, 1].Value != null &&
                                    (workSheet.Cells[startRowQC, 1].Value.ToString().Trim().Equals("2")
                                        || workSheet.Cells[startRowQC, 1].Value.ToString().Trim().Equals("3")))
                                {
                                    break;
                                }
                                else
                                {
                                    
                                        PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_C_B06X_REFID = C_B06X.REFID,
                                            LOAI = 1,
                                        };
                                        detail.STT_CHI_TIEU = workSheet.Cells[startRowQC, 1].Value != null ? workSheet.Cells[startRowQC, 1].Value.ToString() : null;
                                        detail.MA_CHITIEU = workSheet.Cells[startRowQC, 2].Value != null ? workSheet.Cells[startRowQC, 2].Value.ToString() : null;
                                        detail.TEN_CHITIEU = workSheet.Cells[startRowQC, 3].Value != null ? workSheet.Cells[startRowQC, 3].Value.ToString() : null;
                                        isSDDK = double.TryParse(workSheet.Cells[startRowQC, 4].Value == null ? "0" : workSheet.Cells[startRowQC, 4].Value.ToString(), out _SDDK);
                                        detail.SDDK = isSDDK ? _SDDK : 0;
                                        isTONG_THU = double.TryParse(workSheet.Cells[startRowQC, 5].Value == null ? "0" : workSheet.Cells[startRowQC, 5].Value.ToString(), out _TONG_THU);
                                        detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                        isTONG_CHI = double.TryParse(workSheet.Cells[startRowQC, 6].Value == null ? "0" : workSheet.Cells[startRowQC, 6].Value.ToString(), out _TONG_CHI);
                                        detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                        isCON_LAI = double.TryParse(workSheet.Cells[startRowQC, 7].Value == null ? "0" : workSheet.Cells[startRowQC, 7].Value.ToString(), out _CON_LAI);
                                        detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                        _C_B06XDetailService.Insert(detail);
                                        startRowQC += 1;
                                    
                                }
                            }
                            #endregion
                            #region insert Hoạt động sự nghiệp

                            int startRowHD = startRowQC;
                            while (workSheet.Cells[startRowHD, 3].Value != null)
                            {
                                if (workSheet.Cells[startRowHD, 1].Value != null && workSheet.Cells[startRowHD, 1].Value.ToString().Trim().Equals("3"))
                                    break;                              
                                else
                                {
                                    PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_C_B06X_REFID = C_B06X.REFID,
                                        LOAI = 2,
                                    };
                                    detail.STT_CHI_TIEU = workSheet.Cells[startRowHD, 1].Value != null ? workSheet.Cells[startRowHD, 1].Value.ToString() : null;
                                    detail.MA_CHITIEU = workSheet.Cells[startRowHD, 2].Value != null ? workSheet.Cells[startRowHD, 2].Value.ToString() : null;
                                    detail.TEN_CHITIEU = workSheet.Cells[startRowHD, 3].Value != null ? workSheet.Cells[startRowHD, 3].Value.ToString() : null;
                                    isSDDK = double.TryParse(workSheet.Cells[startRowHD, 4].Value == null ? "0" : workSheet.Cells[startRowHD, 4].Value.ToString(), out _SDDK);
                                    detail.SDDK = isSDDK ? _SDDK : 0;
                                    isTONG_THU = double.TryParse(workSheet.Cells[startRowHD, 5].Value == null ? "0" : workSheet.Cells[startRowHD, 5].Value.ToString(), out _TONG_THU);
                                    detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                    isTONG_CHI = double.TryParse(workSheet.Cells[startRowHD, 6].Value == null ? "0" : workSheet.Cells[startRowHD, 6].Value.ToString(), out _TONG_CHI);
                                    detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                    isCON_LAI = double.TryParse(workSheet.Cells[startRowHD, 7].Value == null ? "0" : workSheet.Cells[startRowHD, 7].Value.ToString(), out _CON_LAI);
                                    detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                    _C_B06XDetailService.Insert(detail);
                                    startRowHD += 1;
                                }
                            }
                            #endregion
                            #region insert Hoạt động tài chính khác
                            int startRowHDK = startRowHD;
                            while (workSheet.Cells[startRowHDK, 3].Value != null)
                            {
                                PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B06X_REFID = C_B06X.REFID,
                                    LOAI = 3,
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[startRowHDK, 1].Value != null ? workSheet.Cells[startRowHDK, 1].Value.ToString() : null;
                                detail.MA_CHITIEU = workSheet.Cells[startRowHDK, 2].Value != null ? workSheet.Cells[startRowHDK, 2].Value.ToString() : null;
                                detail.TEN_CHITIEU = workSheet.Cells[startRowHDK, 3].Value != null ? workSheet.Cells[startRowHDK, 3].Value.ToString() : null;
                                isSDDK = double.TryParse(workSheet.Cells[startRowHDK, 4].Value == null ? "0" : workSheet.Cells[startRowHDK, 4].Value.ToString(), out _SDDK);
                                detail.SDDK = isSDDK ? _SDDK : 0;
                                isTONG_THU = double.TryParse(workSheet.Cells[startRowHDK, 5].Value == null ? "0" : workSheet.Cells[startRowHDK, 5].Value.ToString(), out _TONG_THU);
                                detail.TONG_THU = isTONG_THU ? _TONG_THU : 0;
                                isTONG_CHI = double.TryParse(workSheet.Cells[startRowHDK, 6].Value == null ? "0" : workSheet.Cells[startRowHDK, 6].Value.ToString(), out _TONG_CHI);
                                detail.TONG_CHI = isTONG_CHI ? _TONG_CHI : 0;
                                isCON_LAI = double.TryParse(workSheet.Cells[startRowHDK, 7].Value == null ? "0" : workSheet.Cells[startRowHDK, 7].Value.ToString(), out _CON_LAI);
                                detail.CON_LAI = isCON_LAI ? _CON_LAI : 0;
                                _C_B06XDetailService.Insert(detail);
                                startRowHDK += 1;
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

        #region upload MiSa
        [Route("UploadReportMisa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportMisa()
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if(httpRequest.Files.Count > 0)
            {
                try
                {
                    Stream stream = ConvertToXlsx(HttpContext.Current.Request.Files[0].InputStream);
                    using (var excelPackage = new ExcelPackage(stream))
                    {
                        PHB_C_B06X C_B06X = new PHB_C_B06X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            REFID = Guid.NewGuid().ToString("N"),
                            MA_DBHC = httpRequest["MA_DBHC"],
                            MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"]
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            C_B06X.MA_DBHC = httpRequest["MA_DBHC"];
                            C_B06X.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            C_B06X.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            C_B06X.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _C_B06XService.Queryable().FirstOrDefaultAsync(x =>
                              x.MA_DBHC.Equals(C_B06X.MA_DBHC) && x.MA_DBHC.Equals(C_B06X.MA_DBHC) &&
                              x.NAM_BC == C_B06X.NAM_BC && x.KY_BC == C_B06X.KY_BC);

                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B06XService.Insert(C_B06X);
                            var startRow = 15;
                            var stt = 1;
                            bool isSDDK, isTONGTHU, isTONGCHI, isCONLAI;
                            double _SDDK, _TONGTHU, _TONGCHI, _CONLAI;
                            while (workSheet.Cells[startRow, 4].Value != null)
                            {
                                PHB_C_B06X_DETAIL detail = new PHB_C_B06X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B06X_REFID = C_B06X.REFID,                
                                };
                                if(workSheet.Cells[startRow, 4].Value == null)
                                {
                                    detail.TEN_CHITIEU = workSheet.Cells[startRow, 2].Value != null ? workSheet.Cells[startRow, 2].Value.ToString() : null;
                                }
                                else
                                {
                                    detail.TEN_CHITIEU = workSheet.Cells[startRow, 4].Value != null ? workSheet.Cells[startRow, 4].Value.ToString() : null;
                                }
                                detail.STT_CHI_TIEU = workSheet.Cells[startRow, 2].Value != null ? workSheet.Cells[startRow, 2].Value.ToString() : null;                                
                                isSDDK = double.TryParse(workSheet.Cells[startRow, 7].Value != null ? workSheet.Cells[startRow, 7].Value.ToString() : "0", out _SDDK);
                                detail.SDDK = isSDDK ? _SDDK : 0;

                                isTONGTHU = double.TryParse(workSheet.Cells[startRow, 10].Value != null ? workSheet.Cells[startRow, 10].Value.ToString() : "0", out _TONGTHU);
                                detail.TONG_THU = isTONGTHU ? _TONGTHU : 0;

                                isTONGCHI = double.TryParse(workSheet.Cells[startRow, 12].Value != null ? workSheet.Cells[startRow, 12].Value.ToString() : "0", out _TONGCHI);
                                detail.TONG_CHI = isTONGCHI ? _TONGCHI : 0;


                                isCONLAI = double.TryParse(workSheet.Cells[startRow, 13].Value != null ? workSheet.Cells[startRow, 13].Value.ToString() : "0", out _CONLAI);
                                detail.CON_LAI = isCONLAI ? _CONLAI : 0;
                                detail.STT_SAP_XEP = stt;
                                _C_B06XDetailService.Insert(detail);
                                startRow += 1;
                                stt += 1;
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
                            response.Message = ErrorMessage.ERROR_DATA;
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
        #endregion

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            //Response<PHB_C_B06XVm.ViewModel> response = new Response<PHB_C_B06XVm.ViewModel>();
            var response = new Response<List<PHB_C_B06X_DETAIL>>();
            try
            {
                //PHB_C_B06XVm.ViewModel data = new PHB_C_B06XVm.ViewModel();
                //data.REFID = refid;
                //data.DETAIL_QC = await _C_B06XDetailService.Queryable().Where(x => x.PHB_C_B06X_REFID.Equals(refid))
                //    .OrderBy(x => x.STT_SAP_XEP).ToListAsync();
                response.Error = false;
                response.Data = await _C_B06XDetailService.Queryable().Where(x => x.PHB_C_B06X_REFID.Equals(refid)).OrderBy(x => x.STT_SAP_XEP).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_C_B06X_TEMPLATE>> response = new Response<List<PHB_C_B06X_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _C_B06XTemplateService.Queryable()
                    .OrderBy(x => x.MA_CHITIEU)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetTemplateForEdit/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplateForEdit(string refid)
        {
            var response = await _C_B06XTemplateService.GetTemplateForEdit(refid);
            return Ok(response);
        }

        //[Route("SumReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        //{
        //    var response = await _C_B06XService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    return Ok(response);
        //}
        public class SumPara
        {
            public string MA_BAO_CAO { get; set; }            
            public string KY_BC { get; set; }
            public string NAM_BC { get; set; }
            public string MA_DBHC { get; set; }


        }


        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(SumPara para)
        {
            var response = new Response<List<PHB_C_B06X_DETAIL>>();
            List<PHB_C_B06X_DETAIL> detail = new List<PHB_C_B06X_DETAIL>();
            string dsRefid;
            int NAM_BC = int.Parse(para.NAM_BC);
            int KY_BC = int.Parse(para.KY_BC);

            if (para == null || para.MA_BAO_CAO == null || para.NAM_BC == null || para.MA_DBHC == null || para.KY_BC == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }

            //Lấy danh sách báo cáo
            try
            {
                dsRefid = _C_B06XService.Queryable().FirstOrDefault(x => x.KY_BC == KY_BC && x.NAM_BC == NAM_BC && x.MA_DBHC == para.MA_DBHC).REFID;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            };

            try
            {
                detail = _C_B06XDetailService.Queryable().Where(x => x.PHB_C_B06X_REFID == dsRefid).ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }




            response.Data = detail;
            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);

        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(PHB_C_B06XVm.InsertModel model)
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_C_B06X C_B06X = new PHB_C_B06X()
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
                    TEN_QHNS = "",
                    MA_DBHC = model.MA_DBHC,
                    MA_DBHC_CHA = claimMaDbhc?.Value
                };
                var checkReport = await _C_B06XService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(C_B06X.MA_CHUONG) && x.MA_QHNS.Equals(C_B06X.MA_QHNS) &&
                    x.NAM_BC == C_B06X.NAM_BC && x.KY_BC == C_B06X.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _C_B06XService.Insert(C_B06X);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_C_B06X_REFID = C_B06X.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _C_B06XDetailService.Insert(detail);
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
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(PHB_C_B06XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B06X C_B06X = await _C_B06XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (C_B06X == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B06X_DETAIL item = await _C_B06XDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _C_B06XDetailService.Delete(item);
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
                        item.PHB_C_B06X_REFID = model.REFID;
                        _C_B06XDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B06X_DETAIL detail = await _C_B06XDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.STT_CHI_TIEU = item.STT_CHI_TIEU;
                            detail.SDDK = item.SDDK;
                            detail.TONG_THU = item.TONG_THU;
                            detail.TONG_CHI = item.TONG_CHI;
                            detail.CON_LAI = item.CON_LAI;
                            _C_B06XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    C_B06X.NGAY_SUA = DateTime.Now;
                    C_B06X.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _C_B06XService.Update(C_B06X);

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
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC_CHA"));
            var madbhc_cha = "256";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc_cha = firstOrDefault.Value;
            }
            var response = await _C_B06XService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.TEN_CHITIEU == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B06X_DETAIL detail = await _C_B06XDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TEN_CHITIEU_OLD = entry.TEN_CHITIEU;
                                detail.TEN_CHITIEU = rqmodel.newName;
                                _C_B06XDetailService.Update(detail);
                            }
                        }
                    }
                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        msg.Error = false;
                        msg.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                    }
                }
                else
                {
                    msg.Error = true;
                    msg.Message = "Không tìm thấy chỉ tiêu!";
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                msg.Error = true;
                msg.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(msg);
        }

        [Route("Sumreport_HTML")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport_HTML(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC_CHA"));
            var madbhc_cha = "256";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc_cha = firstOrDefault.Value;
            }
            var response = await _C_B06XService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
            return Ok(response);
        }

        [Route("MergeReportcomeback")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReportcomeback(ReportRqModelBack rqmodel)
        {
            //var response = await _bieu01BService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.PHAN, rqmodel.CAP);
            Response<string> msg = new Response<string>();

            try
            {
                var foundLst = await _C_B06XDetailService.Queryable()
                    .Where(x => x.TEN_CHITIEU == rqmodel.TEN_CHITIEU && x.TEN_CHITIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B06X_DETAIL detail = await _C_B06XDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.TEN_CHITIEU = entry.TEN_CHITIEU_OLD;
                        detail.TEN_CHITIEU_OLD = null;
                        _C_B06XDetailService.Update(detail);
                    }
                }
                if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                {
                    msg.Error = false;
                    msg.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
                }

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                msg.Error = true;
                msg.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(msg);
        }

        private Stream ConvertToXlsx(Stream inputStream)
        {
            var fileDir = System.Web.Hosting.HostingEnvironment.MapPath(@"~/UploadFile/ExcelTemp");
            var xlsFileName = Guid.NewGuid() + ".xls";
            var xlsxFileName = xlsFileName + "x";
            var xlsFilePath = Path.Combine(fileDir, xlsFileName);
            var xlsxFilePath = Path.Combine(fileDir, xlsxFileName);

            SaveStreamAsFile(fileDir, inputStream, xlsFileName);


            var app = new Microsoft.Office.Interop.Excel.Application();
            var wb = app.Workbooks.Open(xlsFilePath);
            wb.SaveAs(Filename: xlsxFilePath, FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
            wb.Close();
            app.Quit();

            byte[] fileData = File.ReadAllBytes(xlsxFilePath);
            var stream = new MemoryStream(fileData);

            File.Delete(xlsFilePath);
            File.Delete(xlsxFilePath);

            return stream;
        }

        private static void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }
        }
    }
}
