using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU11TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU11TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu11TT344;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbbieu11tt344")]
    [Route("{id?}")]
    public class PhbBIEU11TT344Controller : ApiController
    {
        private readonly IPhbBieu11TT344Service _bieu11tt344Service;
        private readonly IPhbBieu11TT344DetailService _bieu11tt344DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBIEU11TT344Controller(IPhbBieu11TT344Service bieu11tt344Service, IPhbBieu11TT344DetailService bieu11tt344DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu11tt344Service = bieu11tt344Service;
            _bieu11tt344DetailService = bieu11tt344DetailService;
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
                            PHB_BIEU11TT344 bieu11tt344 = new PHB_BIEU11TT344()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu11tt344.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu11tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu11tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu11tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu11tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu11tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);


                            var checkReport = await _bieu11tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu11tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu11tt344.MA_QHNS) &&
                                x.NAM_BC == bieu11tt344.NAM_BC && x.KY_BC == bieu11tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu11tt344Service.Insert(bieu11tt344);

                            int startRow = 9;
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()))
                            {
                                PHB_BIEU11TT344_DETAIL detail = new PHB_BIEU11TT344_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU11TT344_REFID = bieu11tt344.REFID,
                                };

                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_LOAI = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.MA_KHOAN = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                detail.MA_MUC = workSheet.Cells[startRow, 4].Value != null
                                ? workSheet.Cells[startRow, 4].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 5].Value != null
                                ? workSheet.Cells[startRow, 5].Value.ToString()
                                : null;
                                detail.DIEN_GIAI = workSheet.Cells[startRow, 6].Value != null
                                ? workSheet.Cells[startRow, 6].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 7].Value != null)
                                {
                                    try
                                    {
                                        detail.QUYET_TOAN = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
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
                                _bieu11tt344DetailService.Insert(detail);
                                startRow += 1;

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
                            PHB_BIEU11TT344 bieu11tt344 = new PHB_BIEU11TT344()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                            };
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            bieu11tt344.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu11tt344.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = "1032433";
                            //bieu11tt344.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu11tt344.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu11tt344.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu11tt344.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu11tt344.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu11tt344.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu11tt344.KY_BC = int.Parse(httpRequest["KY_BC"]);


                            var checkReport = await _bieu11tt344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu11tt344.MA_DBHC) && x.MA_QHNS.Equals(bieu11tt344.MA_QHNS) &&
                                x.NAM_BC == bieu11tt344.NAM_BC && x.KY_BC == bieu11tt344.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu11tt344Service.Insert(bieu11tt344);

                            int startRow = 9;
                            while (workSheet.Cells[startRow, 1].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()))
                            {
                                PHB_BIEU11TT344_DETAIL detail = new PHB_BIEU11TT344_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_BIEU11TT344_REFID = bieu11tt344.REFID,
                                };

                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_LOAI = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.MA_KHOAN = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                detail.MA_MUC = workSheet.Cells[startRow, 4].Value != null
                                ? workSheet.Cells[startRow, 4].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 5].Value != null
                                ? workSheet.Cells[startRow, 5].Value.ToString()
                                : null;
                                detail.DIEN_GIAI = workSheet.Cells[startRow, 6].Value != null
                                ? workSheet.Cells[startRow, 6].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 7].Value != null)
                                {
                                    try
                                    {
                                        detail.QUYET_TOAN = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
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
                                _bieu11tt344DetailService.Insert(detail);
                                startRow += 1;

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
            Response<BIEU11TT344Vm.ViewModel> response = new Response<BIEU11TT344Vm.ViewModel>();
            try
            {
                BIEU11TT344Vm.ViewModel data = new BIEU11TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _bieu11tt344DetailService.Queryable().Where(x => x.PHB_BIEU11TT344_REFID.Equals(refid))
                    .OrderBy(x => x.MA_CHUONG).ThenBy(x => x.MA_LOAI).ThenBy(x => x.MA_KHOAN).ThenBy(x => x.MA_MUC).ThenBy(x => x.MA_TIEU_MUC)
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
        public async Task<IHttpActionResult> Post(BIEU11TT344Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_BIEU11TT344 bieu11tt344 = new PHB_BIEU11TT344()
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

                var checkReport = await _bieu11tt344Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu11tt344.MA_CHUONG) && x.MA_QHNS.Equals(bieu11tt344.MA_QHNS) &&
                    x.NAM_BC == bieu11tt344.NAM_BC && x.KY_BC == bieu11tt344.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu11tt344Service.Insert(bieu11tt344);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU11TT344_REFID = bieu11tt344.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu11tt344DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU11TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU11TT344 bieu11tt344 =
                    await _bieu11tt344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu11tt344 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU11TT344_DETAIL item = await _bieu11tt344DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu11tt344DetailService.Delete(item);
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
                        item.PHB_BIEU11TT344_REFID = model.REFID;
                        _bieu11tt344DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU11TT344_DETAIL detail = await _bieu11tt344DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.MA_CHUONG = item.MA_CHUONG;
                            detail.DIEN_GIAI = item.DIEN_GIAI;
                            detail.QUYET_TOAN = item.QUYET_TOAN;
                            _bieu11tt344DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu11tt344.NGAY_SUA = DateTime.Now;
                    bieu11tt344.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu11tt344Service.Update(bieu11tt344);

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
           var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc = firstOrDefault.Value;
            }
            var response = await _bieu11tt344Service.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }
    }
}
