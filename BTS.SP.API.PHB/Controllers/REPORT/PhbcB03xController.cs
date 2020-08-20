using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;
using BTS.SP.PHB.SERVICE.Models.C_B03X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03X;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.ENTITY.Rp;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbc_B03x")]
    [Route("{id?}")]
    public class PhbcB03xController : ApiController
    {
        private readonly IPhbcB03xService _B03xService;
        private readonly IPhbcB03xDetailService _B03xDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbcB03xController(IPhbcB03xService B03xService, IPhbcB03xDetailService B03xDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _B03xService = B03xService;
            _B03xDetailService = B03xDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<C_B03XVm.ViewModel> response = new Response<C_B03XVm.ViewModel>();
            try
            {
                C_B03XVm.ViewModel data = new C_B03XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _B03xDetailService.Queryable().Where(x => x.PHB_C_B03X_REFID.Equals(refid))
                    .OrderBy(x => x.ID)
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

        [Route("GetDetailByRefIdForEdit/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefIdForEdit(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<C_B03XVm.ViewModel> response = new Response<C_B03XVm.ViewModel>();
            try
            {
                C_B03XVm.ViewModel data = new C_B03XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _B03xDetailService.Queryable().Where(x => x.PHB_C_B03X_REFID.Equals(refid))
                    .OrderBy(x => x.ID)
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
        public async Task<IHttpActionResult> Post(C_B03XVm.InsertModel model)
        {
            if (model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                PHB_C_B03X b03x = new PHB_C_B03X()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = "A",
                    MA_QHNS = "123",
                    MA_DBHC = model.MA_DBHC,
                    MA_DBHC_CHA = claimMaDbhc?.Value,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N")
                };

               

                var checkReport = await _B03xService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(b03x.MA_CHUONG) && x.MA_QHNS.Equals(b03x.MA_QHNS) &&
                    x.NAM_BC == b03x.NAM_BC && x.KY_BC == b03x.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _B03xService.Insert(b03x);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_C_B03X_REFID = b03x.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _B03xDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(C_B03XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B03X b03x  =
                    await _B03xService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (b03x == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B03X_DETAIL item = await _B03xDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _B03xDetailService.Delete(item);
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
                        item.PHB_C_B03X_REFID = model.REFID;
                        _B03xDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B03X_DETAIL detail = await _B03xDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;

                            detail.THU_DUTOAN_TONG = item.THU_DUTOAN_TONG;
                            detail.THU_THUCHIEN_TONG = item.THU_THUCHIEN_TONG;
                            detail.THU_DUTOAN_XAHUONG100 = item.THU_DUTOAN_XAHUONG100;
                            detail.THU_THUCHIEN_XAHUONG100 = item.THU_THUCHIEN_XAHUONG100;
                            detail.THU_DUTOAN_PHANCHIATYLE = item.THU_DUTOAN_PHANCHIATYLE;
                            detail.THU_DUTOAN_THUBOSUNG = item.THU_DUTOAN_THUBOSUNG;
                            detail.THU_THUCHIEN_THUBOSUNG = item.THU_THUCHIEN_THUBOSUNG;
                            detail.THU_DUTOAN_BOSUNGCANDOI = item.THU_DUTOAN_BOSUNGCANDOI;
                            detail.THU_THUCHIEN_BOSUNGCANDOI = item.THU_THUCHIEN_BOSUNGCANDOI;
                            detail.THU_DUTOAN_BOSUNGCOMUCTIEU = item.THU_DUTOAN_BOSUNGCOMUCTIEU;
                            detail.THU_THUCHIEN_BOSUNGCOMUCTIEU = item.THU_THUCHIEN_BOSUNGCOMUCTIEU;
                            detail.THU_DUTOAN_THUCHUYENNGUON = item.THU_DUTOAN_THUCHUYENNGUON;
                            detail.THU_THUCHIEN_THUCHUYENNGUON = item.THU_THUCHIEN_THUCHUYENNGUON;

                            detail.CHI_DUTOAN_TONG = item.CHI_DUTOAN_TONG;
                            detail.CHI_THUCHIEN_TONG = item.CHI_THUCHIEN_TONG;
                            detail.CHI_DUTOAN_DTPT = item.CHI_DUTOAN_DTPT;
                            detail.CHI_THUCHIEN_DTPT = item.CHI_THUCHIEN_DTPT;
                            detail.CHI_DUTOAN_CTX = item.CHI_DUTOAN_CTX;
                            detail.CHI_THUCHIEN_CTX = item.CHI_THUCHIEN_CTX;
                            detail.CHI_DUTOAN_CHICHUYENNGUON = item.CHI_DUTOAN_CHICHUYENNGUON;
                            detail.CHI_THUCHIEN_CHICHUYENNGUON = item.CHI_THUCHIEN_CHICHUYENNGUON;

                            detail.KETDU_NGANSACH = item.KETDU_NGANSACH;

       
                            
                            _B03xDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    b03x.NGAY_SUA = DateTime.Now;
                    b03x.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _B03xService.Update(b03x);

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

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_C_B03X b03x = new PHB_C_B03X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                MA_DBHC_CHA = claimMaDbhc?.Value,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                MA_CHUONG = "123",
                                MA_QHNS = "123",
                                REFID = Guid.NewGuid().ToString("N")
                            };
                            
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã địa bàn."
                            });
                            b03x.MA_DBHC = httpRequest["MA_DBHC"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b03x.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            b03x.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _B03xService.Queryable().FirstOrDefaultAsync(x =>
                                x.NAM_BC == b03x.NAM_BC && x.KY_BC == b03x.KY_BC && x.MA_DBHC.Equals(b03x.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _B03xService.Insert(b03x);

                            //chi tiết
                            PHB_C_B03X_DETAIL detail = new PHB_C_B03X_DETAIL()
                            {
                                PHB_C_B03X_REFID = b03x.REFID,
                                ObjectState = ObjectState.Added
                            };

                            detail.THU_DUTOAN_TONG = double.Parse(workSheet.Cells[10, 2].Text.Trim());
                            detail.THU_THUCHIEN_TONG = double.Parse(workSheet.Cells[10, 3].Text.Trim());
                            detail.THU_DUTOAN_XAHUONG100 = double.Parse(workSheet.Cells[11, 2].Text.Trim());
                            detail.THU_THUCHIEN_XAHUONG100 = double.Parse(workSheet.Cells[11, 3].Text.Trim());
                            detail.THU_DUTOAN_PHANCHIATYLE = double.Parse(workSheet.Cells[12, 2].Text.Trim());
                            detail.THU_THUCHIEN_PHANCHIATYLE = double.Parse(workSheet.Cells[12, 3].Text.Trim());
                            detail.THU_DUTOAN_THUBOSUNG = double.Parse(workSheet.Cells[13, 2].Text.Trim());
                            detail.THU_THUCHIEN_THUBOSUNG = double.Parse(workSheet.Cells[13, 3].Text.Trim());
                            detail.THU_DUTOAN_BOSUNGCANDOI = double.Parse(workSheet.Cells[14, 2].Text.Trim());
                            detail.THU_THUCHIEN_BOSUNGCANDOI = double.Parse(workSheet.Cells[14, 3].Text.Trim());
                            detail.THU_DUTOAN_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[15, 2].Text.Trim());
                            detail.THU_THUCHIEN_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[15, 3].Text.Trim());
                            detail.THU_DUTOAN_THUCHUYENNGUON = double.Parse(workSheet.Cells[16, 2].Text.Trim());
                            detail.THU_THUCHIEN_THUCHUYENNGUON = double.Parse(workSheet.Cells[16, 3].Text.Trim());

                            detail.CHI_DUTOAN_TONG = double.Parse(workSheet.Cells[10, 6].Text.Trim());
                            detail.CHI_THUCHIEN_TONG = double.Parse(workSheet.Cells[10, 7].Text.Trim());
                            detail.CHI_DUTOAN_DTPT = double.Parse(workSheet.Cells[11, 6].Text.Trim());
                            detail.CHI_THUCHIEN_DTPT = double.Parse(workSheet.Cells[11, 7].Text.Trim());
                            detail.CHI_DUTOAN_CTX = double.Parse(workSheet.Cells[12, 6].Text.Trim());
                            detail.CHI_THUCHIEN_CTX = double.Parse(workSheet.Cells[12, 7].Text.Trim());
                            detail.CHI_DUTOAN_CHICHUYENNGUON = double.Parse(workSheet.Cells[13, 6].Text.Trim());
                            detail.CHI_THUCHIEN_CHICHUYENNGUON = double.Parse(workSheet.Cells[13, 7].Text.Trim());

                            detail.KETDU_NGANSACH = double.Parse(workSheet.Cells[17, 7].Text.Trim());

                            _B03xDetailService.Insert(detail);

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

        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_C_B03X b03x = new PHB_C_B03X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                MA_DBHC_CHA = claimMaDbhc?.Value,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                MA_CHUONG = "123",
                                MA_QHNS = "123",
                                REFID = Guid.NewGuid().ToString("N")
                            };

                            b03x.MA_CHUONG = "423";
                            b03x.MA_QHNS = "1032433";
                            b03x.MA_DBHC = httpRequest["MA_DBHC"];
                            b03x.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b03x.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            b03x.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _B03xService.Queryable().FirstOrDefaultAsync(x =>
                                x.NAM_BC == b03x.NAM_BC && x.KY_BC == b03x.KY_BC && x.MA_DBHC.Equals(b03x.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _B03xService.Insert(b03x);

                            //chi tiết
                            PHB_C_B03X_DETAIL detail = new PHB_C_B03X_DETAIL()
                            {
                                PHB_C_B03X_REFID = b03x.REFID,
                                ObjectState = ObjectState.Added
                            };

                            detail.THU_DUTOAN_TONG = double.Parse(workSheet.Cells[10, 2].Text.Trim());
                            detail.THU_THUCHIEN_TONG = double.Parse(workSheet.Cells[10, 3].Text.Trim());
                            detail.THU_DUTOAN_XAHUONG100 = double.Parse(workSheet.Cells[11, 2].Text.Trim());
                            detail.THU_THUCHIEN_XAHUONG100 = double.Parse(workSheet.Cells[11, 3].Text.Trim());
                            detail.THU_DUTOAN_PHANCHIATYLE = double.Parse(workSheet.Cells[12, 2].Text.Trim());
                            detail.THU_THUCHIEN_PHANCHIATYLE = double.Parse(workSheet.Cells[12, 3].Text.Trim());
                            detail.THU_DUTOAN_THUBOSUNG = double.Parse(workSheet.Cells[13, 2].Text.Trim());
                            detail.THU_THUCHIEN_THUBOSUNG = double.Parse(workSheet.Cells[13, 3].Text.Trim());
                            detail.THU_DUTOAN_BOSUNGCANDOI = double.Parse(workSheet.Cells[14, 2].Text.Trim());
                            detail.THU_THUCHIEN_BOSUNGCANDOI = double.Parse(workSheet.Cells[14, 3].Text.Trim());
                            detail.THU_DUTOAN_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[15, 2].Text.Trim());
                            detail.THU_THUCHIEN_BOSUNGCOMUCTIEU = double.Parse(workSheet.Cells[15, 3].Text.Trim());
                            detail.THU_DUTOAN_THUCHUYENNGUON = double.Parse(workSheet.Cells[16, 2].Text.Trim());
                            detail.THU_THUCHIEN_THUCHUYENNGUON = double.Parse(workSheet.Cells[16, 3].Text.Trim());

                            detail.CHI_DUTOAN_TONG = double.Parse(workSheet.Cells[10, 6].Text.Trim());
                            detail.CHI_THUCHIEN_TONG = double.Parse(workSheet.Cells[10, 7].Text.Trim());
                            detail.CHI_DUTOAN_DTPT = double.Parse(workSheet.Cells[11, 6].Text.Trim());
                            detail.CHI_THUCHIEN_DTPT = double.Parse(workSheet.Cells[11, 7].Text.Trim());
                            detail.CHI_DUTOAN_CTX = double.Parse(workSheet.Cells[12, 6].Text.Trim());
                            detail.CHI_THUCHIEN_CTX = double.Parse(workSheet.Cells[12, 7].Text.Trim());
                            detail.CHI_DUTOAN_CHICHUYENNGUON = double.Parse(workSheet.Cells[13, 6].Text.Trim());
                            detail.CHI_THUCHIEN_CHICHUYENNGUON = double.Parse(workSheet.Cells[13, 7].Text.Trim());

                            detail.KETDU_NGANSACH = double.Parse(workSheet.Cells[17, 7].Text.Trim());

                            _B03xDetailService.Insert(detail);

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
        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc = firstOrDefault.Value;
            }
            var response = await _B03xService.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        #region Add Content
        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(C_B03XVm.ContentData item)
        {
            var response = new Response<C_B03XVm.ContentData>();
            try
            {
                //item.lstDetail.ForEach(x =>
                //{
                //    x.MA_LOAI = item.MA_LOAI;
                //    x.MA_KHOAN = item.MA_KHOAN;
                //});
                var data = item.lstDetail.ToList();

                foreach (var v in data)
                {
                    v.ObjectState = ObjectState.Added;
                    _B03xDetailService.Insert(v);
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
                response.Error = false;
                response.Data = item;
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
    }
}