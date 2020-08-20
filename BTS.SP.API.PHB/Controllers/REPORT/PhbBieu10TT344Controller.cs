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
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU10TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU10TT344;
using BTS.SP.PHB.SERVICE.REPORT.BIEU10TT344;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbbieu10tt344")]
    [Route("{id?}")]
    public class PhbBieu10TT344Controller : ApiController
    {
        private readonly IPhbBieu10tt344Service _phbBieu10tt344Service;
        private readonly IPhbBieu10tt344DetailService _phbBieu10tt344DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu10TT344Controller(IPhbBieu10tt344Service phbBieu10tt344Service,
            IPhbBieu10tt344DetailService phbBieu10tt344DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phbBieu10tt344Service = phbBieu10tt344Service;
            _phbBieu10tt344DetailService = phbBieu10tt344DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<BIEU10TT344Vm.ViewModel> response = new Response<BIEU10TT344Vm.ViewModel>();
            try
            {
                BIEU10TT344Vm.ViewModel data = new BIEU10TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL =
                    await
                        _phbBieu10tt344DetailService.Queryable()
                            .Where(x => x.PHB_BIEU10TT344_REFID.Equals(refid))
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
            Response<BIEU10TT344Vm.ViewModel> response = new Response<BIEU10TT344Vm.ViewModel>();
            try
            {
                BIEU10TT344Vm.ViewModel data = new BIEU10TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL =
                    await
                        _phbBieu10tt344DetailService.Queryable()
                            .Where(x => x.PHB_BIEU10TT344_REFID.Equals(refid))
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
        public async Task<IHttpActionResult> Post(BIEU10TT344Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_BIEU10TT344 bieu10tt344 = new PHB_BIEU10TT344()
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

                var checkReport = await _phbBieu10tt344Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu10tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu10tt344.MA_QHNS) &&
                    x.NAM_BC == bieu10tt344.NAM_BC && x.KY_BC == bieu10tt344.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phbBieu10tt344Service.Insert(bieu10tt344);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU10TT344_REFID = bieu10tt344.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phbBieu10tt344DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU10TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU10TT344 bieu10tt344 =
                    await _phbBieu10tt344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu10tt344 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU10TT344_DETAIL item = await _phbBieu10tt344DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phbBieu10tt344DetailService.Delete(item);
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
                        item.PHB_BIEU10TT344_REFID = model.REFID;
                        _phbBieu10tt344DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU10TT344_DETAIL detail = await _phbBieu10tt344DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_CHUONG = item.MA_CHUONG;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.DIEN_GIAI = item.DIEN_GIAI;
                            detail.QUYET_TOAN = item.QUYET_TOAN;

                            _phbBieu10tt344DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu10tt344.NGAY_SUA = DateTime.Now;
                    bieu10tt344.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phbBieu10tt344Service.Update(bieu10tt344);

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
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_BIEU10TT344 bieu10tt344 = new PHB_BIEU10TT344()
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
                            bieu10tt344.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu10tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu10tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu10tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu10tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu10tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phbBieu10tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_QHNS.Equals(bieu10tt344.MA_QHNS) && x.NAM_BC == bieu10tt344.NAM_BC && x.KY_BC == bieu10tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phbBieu10tt344Service.Insert(bieu10tt344);

                            #region Insert Detail
                            int startRow = 9;
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()))
                            {
                                var detail = new PHB_BIEU10TT344_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU10TT344_REFID = bieu10tt344.REFID
                                };

                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_MUC = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                detail.DIEN_GIAI = workSheet.Cells[startRow, 4].Value != null
                                ? workSheet.Cells[startRow, 4].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.QUYET_TOAN = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.QUYET_TOAN = 0;
                                }
                                _phbBieu10tt344DetailService.Insert(detail);
                                startRow += 1;

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
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_BIEU10TT344 bieu10tt344 = new PHB_BIEU10TT344()
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
                            bieu10tt344.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu10tt344.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = "1032433";
                            //bieu10tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu10tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu10tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu10tt344.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu10tt344.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu10tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu10tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phbBieu10tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu10tt344.MA_DBHC) && x.NAM_BC == bieu10tt344.NAM_BC && x.KY_BC == bieu10tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phbBieu10tt344Service.Insert(bieu10tt344);

                            #region Insert Detail
                            int startRow = 9;
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()))
                            {
                                var detail = new PHB_BIEU10TT344_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU10TT344_REFID = bieu10tt344.REFID
                                };

                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_MUC = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                detail.DIEN_GIAI = workSheet.Cells[startRow, 4].Value != null
                                ? workSheet.Cells[startRow, 4].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.QUYET_TOAN = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.QUYET_TOAN = 0;
                                }
                                _phbBieu10tt344DetailService.Insert(detail);
                                startRow += 1;

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
            var response = await _phbBieu10tt344Service.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }
    }
}