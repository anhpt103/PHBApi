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
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.SERVICE.Models.BIEU09TT344;
using BTS.SP.PHB.SERVICE.REPORT.BIEU09TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU09TT344;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.ENTITY.Rp;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbbieu09tt344")]
    [Route("{id?}")]
    public class PhbBieu09TT344Controller : ApiController
    {
        private readonly IPhbBieu09tt344Service _phbBieu09tt344Service;
        private readonly IPhbBieu09tt344DetailService _phbBieu09tt344DetailService;
        private readonly IPhbBieu09tt344TemplateService _phbBieu09tt344TemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu09TT344Controller(IPhbBieu09tt344Service phbBieu09tt344Service, IPhbBieu09tt344DetailService phbBieu09tt344DetailService, IPhbBieu09tt344TemplateService phbBieu09tt344TemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phbBieu09tt344Service = phbBieu09tt344Service;
            _phbBieu09tt344DetailService = phbBieu09tt344DetailService;
            _phbBieu09tt344TemplateService = phbBieu09tt344TemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            Response<BIEU09TT344Vm.ViewModel> response = new Response<BIEU09TT344Vm.ViewModel>();
            try
            {
                BIEU09TT344Vm.ViewModel data = new BIEU09TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL =
                    await
                        _phbBieu09tt344DetailService.Queryable()
                            .Where(x => x.PHB_BIEU09TT344_REFID.Equals(refid))
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
            Response<BIEU09TT344Vm.ViewModel> response = new Response<BIEU09TT344Vm.ViewModel>();
            try
            {
                BIEU09TT344Vm.ViewModel data = new BIEU09TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL =
                    await
                        _phbBieu09tt344DetailService.Queryable()
                            .Where(x => x.PHB_BIEU09TT344_REFID.Equals(refid))
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
        public async Task<IHttpActionResult> Post(BIEU09TT344Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_BIEU09TT344 bieu09tt344 = new PHB_BIEU09TT344()
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


                var checkReport = await _phbBieu09tt344Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu09tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu09tt344.MA_QHNS) &&
                    x.NAM_BC == bieu09tt344.NAM_BC && x.KY_BC == bieu09tt344.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phbBieu09tt344Service.Insert(bieu09tt344);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU09TT344_REFID = bieu09tt344.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phbBieu09tt344DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU09TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU09TT344 bieu09tt344 =
                    await _phbBieu09tt344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu09tt344 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU09TT344_DETAIL item = await _phbBieu09tt344DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phbBieu09tt344DetailService.Delete(item);
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
                        item.PHB_BIEU09TT344_REFID = model.REFID;
                        _phbBieu09tt344DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU09TT344_DETAIL detail = await _phbBieu09tt344DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;

                            detail.DT_TONGSO = item.DT_TONGSO;
                            detail.DT_DTPT = item.DT_DTPT;
                            detail.DT_TX = item.DT_TX;
                            detail.QT_TONGSO = item.QT_TONGSO;
                            detail.QT_DTPT = item.QT_DTPT;
                            detail.QT_TX = item.QT_TX;
                            detail.SS_TONGSO = item.SS_TONGSO;
                            detail.SS_DTPT = item.SS_DTPT;
                            detail.SS_TX = item.SS_TX;

                            _phbBieu09tt344DetailService.Update(detail);
                        }
                    }
                }
                #endregion




                if (hasValue)
                {
                    bieu09tt344.NGAY_SUA = DateTime.Now;
                    bieu09tt344.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phbBieu09tt344Service.Update(bieu09tt344);

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
                            PHB_BIEU09TT344 bieu09tt344 = new PHB_BIEU09TT344()
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
                            bieu09tt344.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu09tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu09tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu09tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu09tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu09tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phbBieu09tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_QHNS.Equals(bieu09tt344.MA_QHNS) && x.NAM_BC == bieu09tt344.NAM_BC && x.KY_BC == bieu09tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phbBieu09tt344Service.Insert(bieu09tt344);

                            #region Insert Detail
                            int rowStart = 11;
                            int rowEnd = 45;
                            int stt = 0;
                            for (int j = rowStart; j <= rowEnd; j++)
                            {
                                stt++;
                                var detail = new PHB_BIEU09TT344_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU09TT344_REFID = bieu09tt344.REFID,
                                };
                                if (stt < 10) { detail.STT_CHI_TIEU = "0" + stt.ToString();}
                                else { detail.STT_CHI_TIEU = stt.ToString(); }
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.DT_TONGSO = double.Parse(workSheet.Cells[j, 2].Value.ToString());
                                detail.DT_DTPT = double.Parse(workSheet.Cells[j, 3].Value.ToString());
                                detail.DT_TX = double.Parse(workSheet.Cells[j, 4].Value.ToString());
                                detail.QT_TONGSO = double.Parse(workSheet.Cells[j, 5].Value.ToString());
                                detail.QT_DTPT = double.Parse(workSheet.Cells[j, 6].Value.ToString());
                                detail.QT_TX = double.Parse(workSheet.Cells[j, 7].Value.ToString());
                                detail.SS_TONGSO = Math.Round(double.Parse(workSheet.Cells[j, 8].Value.ToString()), 2);
                                detail.SS_DTPT = Math.Round(double.Parse(workSheet.Cells[j, 9].Value.ToString()), 2);
                                detail.SS_TX = Math.Round(double.Parse(workSheet.Cells[j, 10].Value.ToString()), 2);
                                if (j == 11 || j == 12 || j == 22 || j == 28 || j == 38)
                                {
                                    detail.LOAI = 1;
                                }
                                else if(j == 29 )
                                {
                                    detail.LOAI = 3;
                                }
                                else
                                {
                                    detail.LOAI = 2;
                                }
                                if (j == 11) { detail.IN_DAM = 1; }
                                else { detail.IN_DAM = 0; }
                                _phbBieu09tt344DetailService.Insert(detail);
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
                            PHB_BIEU09TT344 bieu09tt344 = new PHB_BIEU09TT344()
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
                            bieu09tt344.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu09tt344.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = "1032433";
                            //bieu09tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu09tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu09tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu09tt344.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu09tt344.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu09tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu09tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phbBieu09tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu09tt344.MA_DBHC) && x.MA_QHNS.Equals(bieu09tt344.MA_QHNS) &&
                                x.NAM_BC == bieu09tt344.NAM_BC && x.KY_BC == bieu09tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phbBieu09tt344Service.Insert(bieu09tt344);

                            #region Insert Detail
                            int rowStart = 11;
                            int rowEnd = 45;
                            int stt = 0;
                            for (int j = rowStart; j <= rowEnd; j++)
                            {
                                stt++;
                                var detail = new PHB_BIEU09TT344_DETAIL
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU09TT344_REFID = bieu09tt344.REFID,
                                };
                                if (stt < 10) { detail.STT_CHI_TIEU = "0" + stt.ToString(); }
                                else { detail.STT_CHI_TIEU = stt.ToString(); }
                                detail.TEN_CHI_TIEU = workSheet.Cells[j, 1].Value.ToString().Trim();
                                detail.DT_TONGSO = double.Parse(workSheet.Cells[j, 2].Value.ToString());
                                detail.DT_DTPT = double.Parse(workSheet.Cells[j, 3].Value.ToString());
                                detail.DT_TX = double.Parse(workSheet.Cells[j, 4].Value.ToString());
                                detail.QT_TONGSO = double.Parse(workSheet.Cells[j, 5].Value.ToString());
                                detail.QT_DTPT = double.Parse(workSheet.Cells[j, 6].Value.ToString());
                                detail.QT_TX = double.Parse(workSheet.Cells[j, 7].Value.ToString());
                                detail.SS_TONGSO = Math.Round(double.Parse(workSheet.Cells[j, 8].Value.ToString()), 2);
                                detail.SS_DTPT = Math.Round(double.Parse(workSheet.Cells[j, 9].Value.ToString()), 2);
                                detail.SS_TX = Math.Round(double.Parse(workSheet.Cells[j, 10].Value.ToString()), 2);
                                if (j == 11 || j == 12 || j == 22 || j == 28 || j == 38)
                                {
                                    detail.LOAI = 1;
                                }
                                else if (j == 29)
                                {
                                    detail.LOAI = 3;
                                }
                                else
                                {
                                    detail.LOAI = 2;
                                }
                                if (j == 11) { detail.IN_DAM = 1; }
                                else { detail.IN_DAM = 0; }
                                _phbBieu09tt344DetailService.Insert(detail);
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
            var response = await _phbBieu09tt344Service.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

    }
}