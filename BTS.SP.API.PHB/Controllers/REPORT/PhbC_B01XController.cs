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
using BTS.SP.PHB.SERVICE.Models.C_B01X;
using BTS.SP.PHB.SERVICE.REPORT.C_B01X;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbC_B01X")]
    [Route("{id?}")]
    public class PhbC_B01XController : ApiController
    {
        private readonly IPhbC_B01XService _C_B01XService;
        private readonly IPhbC_B01XDetailService _C_B01XDetailService;
        private readonly IPhbC_B01XTemplateService _C_B01XTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbC_B01XController(IPhbC_B01XService C_B01XService, IPhbC_B01XDetailService C_B01XDetailService, IPhbC_B01XTemplateService C_B01XTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _C_B01XService = C_B01XService;
            _C_B01XDetailService = C_B01XDetailService;
            _C_B01XTemplateService = C_B01XTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    Stream stream;
                    if (httpRequest.Files[0].FileName.EndsWith(".xls")){
                        stream = ConvertToXlsx(HttpContext.Current.Request.Files[0].InputStream);
                    }
                    else
                    {
                        stream = HttpContext.Current.Request.Files[0].InputStream;
                    }

                    using (var excelPackage = new ExcelPackage(stream))
                    {
                        PHB_C_B01X C_B01X = new PHB_C_B01X()
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
                            C_B01X.MA_CHUONG = "423";
                            C_B01X.MA_QHNS = "27";
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã địa bàn hành chính"
                            });
                            C_B01X.MA_DBHC = httpRequest["MA_DBHC"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            C_B01X.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            C_B01X.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _C_B01XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(C_B01X.MA_CHUONG) && x.MA_DBHC.Equals(C_B01X.MA_DBHC) &&
                                x.NAM_BC == C_B01X.NAM_BC && x.KY_BC == C_B01X.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B01XService.Insert(C_B01X);

                            #region insert TRONG BẢNG
                            int startRowTB = 14;
                            bool isLoop = true;
                            bool isSDDK_NO, isSDDK_CO, isPSTK_NO, isPSTK_CO, isLUYKE_NO, isLUYKE_CO, isSDCK_NO, isSDCK_CO;
                            double _SDDK_NO, _SDDK_CO, _PSTK_NO, _PSTK_CO, _LUYKE_NO, _LUYKE_CO, _SDCK_NO, _SDCK_CO;
                            while (workSheet.Cells[startRowTB, 1].Value != null && isLoop)
                            {
                                if (workSheet.Cells[startRowTB, 1].Value != null &&
                                    workSheet.Cells[startRowTB, 1].Value.ToString().Trim().Contains("B."))
                                {
                                    isLoop = false;
                                }
                                else
                                {
                                    if (workSheet.Cells[startRowTB, 1].Value != null &&
                                        workSheet.Cells[startRowTB, 1].Value.ToString().Trim().Contains("A."))
                                    {
                                        startRowTB += 1;
                                    }
                                    else
                                    {
                                        PHB_C_B01X_DETAIL detail = new PHB_C_B01X_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_C_B01X_REFID = C_B01X.REFID,
                                            LOAI = 1,
                                        };
                                        detail.MA_TAIKHOAN = workSheet.Cells[startRowTB, 1].Value != null ? workSheet.Cells[startRowTB, 1].Value.ToString() : null;
                                        detail.TEN_TAIKHOAN = workSheet.Cells[startRowTB, 2].Value != null ? workSheet.Cells[startRowTB, 2].Value.ToString() : null;
                                        isSDDK_NO = double.TryParse(workSheet.Cells[startRowTB, 3].Value == null ? "0" : workSheet.Cells[startRowTB, 3].Value.ToString(), out _SDDK_NO);
                                        detail.SDDK_NO = isSDDK_NO ? _SDDK_NO : 0;
                                        isSDDK_CO = double.TryParse(workSheet.Cells[startRowTB, 4].Value == null ? "0" : workSheet.Cells[startRowTB, 4].Value.ToString(), out _SDDK_CO);
                                        detail.SDDK_CO = isSDDK_CO ? _SDDK_CO : 0;
                                        isPSTK_NO = double.TryParse(workSheet.Cells[startRowTB, 5].Value == null ? "0" : workSheet.Cells[startRowTB, 5].Value.ToString(), out _PSTK_NO);
                                        detail.PSTK_NO = isPSTK_NO ? _PSTK_NO : 0;
                                        isPSTK_CO = double.TryParse(workSheet.Cells[startRowTB, 6].Value == null ? "0" : workSheet.Cells[startRowTB, 6].Value.ToString(), out _PSTK_CO);
                                        detail.PSTK_CO = isPSTK_CO ? _PSTK_CO : 0;
                                        isLUYKE_NO = double.TryParse(workSheet.Cells[startRowTB, 7].Value == null ? "0" : workSheet.Cells[startRowTB, 7].Value.ToString(), out _LUYKE_NO);
                                        detail.LUYKE_NO = isLUYKE_NO ? _LUYKE_NO : 0;
                                        isLUYKE_CO = double.TryParse(workSheet.Cells[startRowTB, 8].Value == null ? "0" : workSheet.Cells[startRowTB, 8].Value.ToString(), out _LUYKE_CO);
                                        detail.LUYKE_CO = isLUYKE_CO ? _LUYKE_CO : 0;
                                        isSDCK_NO = double.TryParse(workSheet.Cells[startRowTB, 9].Value == null ? "0" : workSheet.Cells[startRowTB, 9].Value.ToString(), out _SDCK_NO);
                                        detail.SDCK_NO = isSDCK_NO ? _SDCK_NO : 0;
                                        isSDCK_CO = double.TryParse(workSheet.Cells[startRowTB, 10].Value == null ? "0" : workSheet.Cells[startRowTB, 10].Value.ToString(), out _SDCK_CO);
                                        detail.SDCK_CO = isSDCK_CO ? _SDCK_CO : 0;
                                        _C_B01XDetailService.Insert(detail);
                                        startRowTB += 1;
                                    }
                                }
                            }
                            #endregion

                            #region insert NGOÀI BẢNG

                            int startRowNB = startRowTB;
                            if (workSheet.Cells[startRowNB, 1].Value != null &&
                                workSheet.Cells[startRowNB, 1].Value.ToString().Contains("B.")) startRowNB += 1;
                            while (workSheet.Cells[startRowNB, 1].Value != null)
                            {
                                PHB_C_B01X_DETAIL detail = new PHB_C_B01X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B01X_REFID = C_B01X.REFID,
                                    LOAI = 2,
                                };
                                detail.MA_TAIKHOAN = workSheet.Cells[startRowNB, 1].Value != null ? workSheet.Cells[startRowNB, 1].Value.ToString() : null;
                                detail.TEN_TAIKHOAN = workSheet.Cells[startRowNB, 2].Value != null ? workSheet.Cells[startRowNB, 2].Value.ToString() : null;
                                isSDDK_NO = double.TryParse(workSheet.Cells[startRowNB, 3].Value == null ? "0":workSheet.Cells[startRowNB, 3].Value.ToString(), out _SDDK_NO);
                                detail.SDDK_NO = isSDDK_NO ? _SDDK_NO : 0;
                                isSDDK_CO = double.TryParse(workSheet.Cells[startRowNB, 4].Value == null ? "0" : workSheet.Cells[startRowNB, 4].Value.ToString(), out _SDDK_CO);
                                detail.SDDK_CO = isSDDK_CO ? _SDDK_CO : 0;
                                isPSTK_NO = double.TryParse(workSheet.Cells[startRowNB, 5].Value == null ? "0" : workSheet.Cells[startRowNB, 5].Value.ToString(), out _PSTK_NO);
                                detail.PSTK_NO = isPSTK_NO ? _PSTK_NO : 0;
                                isPSTK_CO = double.TryParse(workSheet.Cells[startRowNB, 6].Value == null ? "0" : workSheet.Cells[startRowNB, 6].Value.ToString(), out _PSTK_CO);
                                detail.PSTK_CO = isPSTK_CO ? _PSTK_CO : 0;
                                isLUYKE_NO = double.TryParse(workSheet.Cells[startRowNB, 7].Value == null ? "0" : workSheet.Cells[startRowNB, 7].Value.ToString(), out _LUYKE_NO);
                                detail.LUYKE_NO = isLUYKE_NO ? _LUYKE_NO : 0;
                                isLUYKE_CO = double.TryParse(workSheet.Cells[startRowNB, 8].Value == null ? "0" : workSheet.Cells[startRowNB, 8].Value.ToString(), out _LUYKE_CO);
                                detail.LUYKE_CO = isLUYKE_CO ? _LUYKE_CO : 0;
                                isSDCK_NO = double.TryParse(workSheet.Cells[startRowNB, 9].Value == null ? "0" : workSheet.Cells[startRowNB, 9].Value.ToString(), out _SDCK_NO);
                                detail.SDCK_NO = isSDCK_NO ? _SDCK_NO : 0;
                                isSDCK_CO = double.TryParse(workSheet.Cells[startRowNB, 10].Value == null ? "0" : workSheet.Cells[startRowNB, 10].Value.ToString(), out _SDCK_CO);
                                detail.SDCK_CO = isSDCK_CO ? _SDCK_CO : 0;
                                _C_B01XDetailService.Insert(detail);
                                startRowNB += 1;
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
                    int[] colInd = new int[11];
                    int colABInd;
                    int startRowTB;

                    Stream stream;
                    if (httpRequest.Files[0].FileName.EndsWith(".xls"))
                    {
                        stream = ConvertToXlsx(HttpContext.Current.Request.Files[0].InputStream);
                        colInd[1] = 2;
                        colInd[2] = 4;
                        colInd[3] = 6;
                        colInd[4] = 7;
                        colInd[5] = 9;
                        colInd[6] = 10;
                        colInd[7] = 12;
                        colInd[8] = 14;
                        colInd[9] = 17;
                        colInd[10] = 19;

                        colABInd = 4;
                        startRowTB = 17;
                    }
                    else
                    {
                        stream = HttpContext.Current.Request.Files[0].InputStream;
                        colInd[1] = 1;
                        colInd[2] = 2;
                        colInd[3] = 3;
                        colInd[4] = 4;
                        colInd[5] = 5;
                        colInd[6] = 6;
                        colInd[7] = 7;
                        colInd[8] = 8;
                        colInd[9] = 9;
                        colInd[10] = 10;

                        colABInd = 1;
                        startRowTB = 14;
                    }

                    using (var excelPackage = new ExcelPackage(stream))
                    {
                        PHB_C_B01X C_B01X = new PHB_C_B01X()
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
                            C_B01X.MA_CHUONG = "423";
                            C_B01X.MA_QHNS = "1032433";
                            C_B01X.MA_DBHC = httpRequest["MA_DBHC"];
                            C_B01X.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            C_B01X.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            C_B01X.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _C_B01XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(C_B01X.MA_DBHC) && x.MA_DBHC.Equals(C_B01X.MA_DBHC) &&
                                x.NAM_BC == C_B01X.NAM_BC && x.KY_BC == C_B01X.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B01XService.Insert(C_B01X);

                            #region insert TRONG BẢNG
                            bool isLoop = true;
                            bool isSDDK_NO, isSDDK_CO, isPSTK_NO, isPSTK_CO, isLUYKE_NO, isLUYKE_CO, isSDCK_NO, isSDCK_CO;
                            double _SDDK_NO, _SDDK_CO, _PSTK_NO, _PSTK_CO, _LUYKE_NO, _LUYKE_CO, _SDCK_NO, _SDCK_CO;
                            while (workSheet.Cells[startRowTB, colABInd].Value != null && isLoop)
                            {
                                if (workSheet.Cells[startRowTB, colABInd].Value != null &&
                                    workSheet.Cells[startRowTB, colABInd].Value.ToString().Trim().Contains("B."))
                                {
                                    isLoop = false;
                                }
                                else
                                {
                                    if (workSheet.Cells[startRowTB, colABInd].Value != null &&
                                        workSheet.Cells[startRowTB, colABInd].Value.ToString().Trim().Contains("A."))
                                    {
                                        startRowTB += 1;
                                    }
                                    else
                                    {
                                        PHB_C_B01X_DETAIL detail = new PHB_C_B01X_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_C_B01X_REFID = C_B01X.REFID,
                                            LOAI = 1,
                                        };
                                        detail.MA_TAIKHOAN = workSheet.Cells[startRowTB, colInd[1]].Value != null ? workSheet.Cells[startRowTB, colInd[1]].Value.ToString() : null;
                                        detail.TEN_TAIKHOAN = workSheet.Cells[startRowTB, colInd[2]].Value != null ? workSheet.Cells[startRowTB, colInd[2]].Value.ToString() : null;
                                        isSDDK_NO = double.TryParse(workSheet.Cells[startRowTB, colInd[3]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[3]].Value.ToString(), out _SDDK_NO);
                                        detail.SDDK_NO = isSDDK_NO ? _SDDK_NO : 0;
                                        isSDDK_CO = double.TryParse(workSheet.Cells[startRowTB, colInd[4]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[4]].Value.ToString(), out _SDDK_CO);
                                        detail.SDDK_CO = isSDDK_CO ? _SDDK_CO : 0;
                                        isPSTK_NO = double.TryParse(workSheet.Cells[startRowTB, colInd[5]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[5]].Value.ToString(), out _PSTK_NO);
                                        detail.PSTK_NO = isPSTK_NO ? _PSTK_NO : 0;
                                        isPSTK_CO = double.TryParse(workSheet.Cells[startRowTB, colInd[6]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[6]].Value.ToString(), out _PSTK_CO);
                                        detail.PSTK_CO = isPSTK_CO ? _PSTK_CO : 0;
                                        isLUYKE_NO = double.TryParse(workSheet.Cells[startRowTB, colInd[7]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[7]].Value.ToString(), out _LUYKE_NO);
                                        detail.LUYKE_NO = isLUYKE_NO ? _LUYKE_NO : 0;
                                        isLUYKE_CO = double.TryParse(workSheet.Cells[startRowTB, colInd[8]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[8]].Value.ToString(), out _LUYKE_CO);
                                        detail.LUYKE_CO = isLUYKE_CO ? _LUYKE_CO : 0;
                                        isSDCK_NO = double.TryParse(workSheet.Cells[startRowTB, colInd[9]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[9]].Value.ToString(), out _SDCK_NO);
                                        detail.SDCK_NO = isSDCK_NO ? _SDCK_NO : 0;
                                        isSDCK_CO = double.TryParse(workSheet.Cells[startRowTB, colInd[10]].Value == null ? "0" : workSheet.Cells[startRowTB, colInd[10]].Value.ToString(), out _SDCK_CO);
                                        detail.SDCK_CO = isSDCK_CO ? _SDCK_CO : 0;
                                        _C_B01XDetailService.Insert(detail);
                                        startRowTB += 1;
                                    }
                                }
                            }
                            #endregion

                            #region insert NGOÀI BẢNG

                            int startRowNB = 0;
                            int endRow = workSheet.Dimension.End.Row;

                            for(int i = startRowTB; i <= endRow; i++)
                            {
                                if (workSheet.Cells[i, colABInd].Value != null &&
                                workSheet.Cells[i, colABInd].Value.ToString().Contains("B."))
                                {
                                    startRowNB = i + 1;
                                    break;
                                }
                            }

                            while (workSheet.Cells[startRowNB, colInd[1]].Value != null)
                            {
                                PHB_C_B01X_DETAIL detail = new PHB_C_B01X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B01X_REFID = C_B01X.REFID,
                                    LOAI = 2,
                                };
                                detail.MA_TAIKHOAN = workSheet.Cells[startRowNB, colInd[1]].Value != null ? workSheet.Cells[startRowNB, colInd[1]].Value.ToString() : null;
                                detail.TEN_TAIKHOAN = workSheet.Cells[startRowNB, colInd[2]].Value != null ? workSheet.Cells[startRowNB, colInd[2]].Value.ToString() : null;
                                isSDDK_NO = double.TryParse(workSheet.Cells[startRowNB, colInd[3]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[3]].Value.ToString(), out _SDDK_NO);
                                detail.SDDK_NO = isSDDK_NO ? _SDDK_NO : 0;
                                isSDDK_CO = double.TryParse(workSheet.Cells[startRowNB, colInd[4]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[4]].Value.ToString(), out _SDDK_CO);
                                detail.SDDK_CO = isSDDK_CO ? _SDDK_CO : 0;
                                isPSTK_NO = double.TryParse(workSheet.Cells[startRowNB, colInd[5]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[5]].Value.ToString(), out _PSTK_NO);
                                detail.PSTK_NO = isPSTK_NO ? _PSTK_NO : 0;
                                isPSTK_CO = double.TryParse(workSheet.Cells[startRowNB, colInd[6]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[6]].Value.ToString(), out _PSTK_CO);
                                detail.PSTK_CO = isPSTK_CO ? _PSTK_CO : 0;
                                isLUYKE_NO = double.TryParse(workSheet.Cells[startRowNB, colInd[7]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[7]].Value.ToString(), out _LUYKE_NO);
                                detail.LUYKE_NO = isLUYKE_NO ? _LUYKE_NO : 0;
                                isLUYKE_CO = double.TryParse(workSheet.Cells[startRowNB, colInd[8]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[8]].Value.ToString(), out _LUYKE_CO);
                                detail.LUYKE_CO = isLUYKE_CO ? _LUYKE_CO : 0;
                                isSDCK_NO = double.TryParse(workSheet.Cells[startRowNB, colInd[9]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[9]].Value.ToString(), out _SDCK_NO);
                                detail.SDCK_NO = isSDCK_NO ? _SDCK_NO : 0;
                                isSDCK_CO = double.TryParse(workSheet.Cells[startRowNB, colInd[10]].Value == null ? "0" : workSheet.Cells[startRowNB, colInd[10]].Value.ToString(), out _SDCK_CO);
                                detail.SDCK_CO = isSDCK_CO ? _SDCK_CO : 0;
                                _C_B01XDetailService.Insert(detail);
                                startRowNB += 1;
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

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<PHB_C_B01XVm.ViewModel> response = new Response<PHB_C_B01XVm.ViewModel>();
            try
            {
                PHB_C_B01XVm.ViewModel data = new PHB_C_B01XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL_TRONGBANG = await _C_B01XDetailService.Queryable().Where(x => x.PHB_C_B01X_REFID.Equals(refid))
                    .OrderBy(x => x.MA_TAIKHOAN).ToListAsync();
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHB_C_B01X_TEMPLATE>> response = new Response<List<PHB_C_B01X_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _C_B01XTemplateService.Queryable()
                    .OrderBy(x => x.MA_TAIKHOAN)
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
            var response = await _C_B01XTemplateService.GetTemplateForEdit(refid);
            return Ok(response);
        }

        //[Route("SumReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        //{
        //    var response = await _C_B01XService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    return Ok(response);
        //}

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            //rqmodel.MA_CHUONG = "421";
            var data = await _C_B01XService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && (data.Data.DETAIL_TRONGBANG.Count > 0 || data.Data.DETAIL_TRONGBANG.Count > 0))
            {
                string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/C_B01X/Template.xlsx");
                string exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/C_B01X/" + RequestContext.Principal.Identity.Name
                                                                                     + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A1"].Value = sheet.Cells["A1"].Value + " " + rqmodel.MA_CHUONG;
                        sheet.Cells["A2"].Value = sheet.Cells["A2"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
                        sheet.Cells["A3"].Value = sheet.Cells["A3"].Value + " " + rqmodel.DSDVQHNS;
                        sheet.Cells["A6"].Value = sheet.Cells["A6"].Value + " " + rqmodel.NAM_BC;
                        if (data.Data.DETAIL_TRONGBANG.Count > 0)
                        {
                            sheet.InsertRow(15, data.Data.DETAIL_TRONGBANG.Count);
                            for (int i = 0; i < data.Data.DETAIL_TRONGBANG.Count; i++)
                            {
                                for (int j = 1; j <= 10; j++)
                                {
                                    sheet.Cells[11 + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[11 + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                }

                                sheet.Cells[11 + i, 1].Value = data.Data.DETAIL_TRONGBANG[i].MA_TAIKHOAN;
                                sheet.Cells[11 + i, 2].Value = data.Data.DETAIL_TRONGBANG[i].TEN_TAIKHOAN;
                                sheet.Cells[11 + i, 3].Value = data.Data.DETAIL_TRONGBANG[i].SDDK_NO;
                                sheet.Cells[11 + i, 4].Value = data.Data.DETAIL_TRONGBANG[i].SDDK_CO;
                                sheet.Cells[11 + i, 5].Value = data.Data.DETAIL_TRONGBANG[i].PSTK_NO;
                                sheet.Cells[11 + i, 6].Value = data.Data.DETAIL_TRONGBANG[i].PSTK_CO;
                                sheet.Cells[11 + i, 7].Value = data.Data.DETAIL_TRONGBANG[i].LUYKE_NO;
                                sheet.Cells[11 + i, 8].Value = data.Data.DETAIL_TRONGBANG[i].LUYKE_CO;
                                sheet.Cells[11 + i, 9].Value = data.Data.DETAIL_TRONGBANG[i].SDCK_NO;
                                sheet.Cells[11 + i, 10].Value = data.Data.DETAIL_TRONGBANG[i].SDCK_CO;

                            }
                        }

                        if (data.Data.DETAIL_NGOAIBANG.Count > 0)
                        {
                            int startRow = 15 + data.Data.DETAIL_TRONGBANG.Count;
                            sheet.InsertRow(startRow, data.Data.DETAIL_NGOAIBANG.Count);
                            for (int i = 0; i < data.Data.DETAIL_NGOAIBANG.Count; i++)
                            {
                                for (int j = 1; j <= 10; j++)
                                {
                                    sheet.Cells[startRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[startRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                }

                                sheet.Cells[11 + i, 1].Value = data.Data.DETAIL_TRONGBANG[i].MA_TAIKHOAN;
                                sheet.Cells[11 + i, 2].Value = data.Data.DETAIL_TRONGBANG[i].TEN_TAIKHOAN;
                                sheet.Cells[11 + i, 3].Value = data.Data.DETAIL_TRONGBANG[i].SDDK_NO;
                                sheet.Cells[11 + i, 4].Value = data.Data.DETAIL_TRONGBANG[i].SDDK_CO;
                                sheet.Cells[11 + i, 5].Value = data.Data.DETAIL_TRONGBANG[i].PSTK_NO;
                                sheet.Cells[11 + i, 6].Value = data.Data.DETAIL_TRONGBANG[i].PSTK_CO;
                                sheet.Cells[11 + i, 7].Value = data.Data.DETAIL_TRONGBANG[i].LUYKE_NO;
                                sheet.Cells[11 + i, 8].Value = data.Data.DETAIL_TRONGBANG[i].LUYKE_CO;
                                sheet.Cells[11 + i, 9].Value = data.Data.DETAIL_TRONGBANG[i].SDCK_NO;
                                sheet.Cells[11 + i, 10].Value = data.Data.DETAIL_TRONGBANG[i].SDCK_CO;

                            }
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_C_B01X.xlsx"
                        };
                        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        var response = ResponseMessage(result);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    WriteLogs.LogError(ex);
                    return InternalServerError();
                }
            }
            return Ok(data);
        }

        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(PHB_C_B01XVm.InsertModel model)
        {
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            //if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
            //    model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            if ( model.NAM_BC < 0 || model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_C_B01X C_B01X = new PHB_C_B01X()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = "423",
                    MA_QHNS = "17",
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    TEN_QHNS = "",
                    MA_DBHC = model.MA_DBHC,
                    MA_DBHC_CHA = claimMaDbhc?.Value
                };
                var checkReport = await _C_B01XService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(C_B01X.MA_CHUONG) && x.MA_QHNS.Equals(C_B01X.MA_QHNS) &&
                    x.NAM_BC == C_B01X.NAM_BC && x.KY_BC == C_B01X.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _C_B01XService.Insert(C_B01X);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_C_B01X_REFID = C_B01X.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _C_B01XDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(PHB_C_B01XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B01X C_B01X = await _C_B01XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (C_B01X == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B01X_DETAIL item = await _C_B01XDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _C_B01XDetailService.Delete(item);
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
                        item.PHB_C_B01X_REFID = model.REFID;
                        _C_B01XDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B01X_DETAIL detail = await _C_B01XDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.SDDK_NO = item.SDDK_NO;
                            detail.SDDK_CO = item.SDDK_CO;
                            detail.PSTK_NO = item.PSTK_NO;
                            detail.PSTK_CO = item.PSTK_CO;
                            detail.LUYKE_NO = item.LUYKE_NO;
                            detail.LUYKE_CO = item.LUYKE_CO;
                            detail.SDCK_NO = item.SDCK_NO;
                            detail.SDCK_CO = item.SDCK_CO;
                            _C_B01XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    C_B01X.NGAY_SUA = DateTime.Now;
                    C_B01X.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _C_B01XService.Update(C_B01X);

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
            var response = new Response<List<PHB_C_B01X_DETAIL>>();
            List<PHB_C_B01X_DETAIL> detail = new List<PHB_C_B01X_DETAIL>();
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
                dsRefid = _C_B01XService.Queryable().FirstOrDefault(x => x.KY_BC == KY_BC && x.NAM_BC == NAM_BC && x.MA_DBHC == para.MA_DBHC).REFID;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            };

            try
            {
                detail = _C_B01XDetailService.Queryable().Where(x => x.PHB_C_B01X_REFID == dsRefid).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }




            response.Data = detail;
            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);

        }

    }
}
