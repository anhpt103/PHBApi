using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.C_B03C_X;
using BTS.SP.PHB.SERVICE.Models.C_B03C_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03C_X;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbc_b03C_X")]
    [Route("{id?}")]
    public class PhbCB03cXController : ApiController
    {
        private readonly IPhb_C_B03C_XService _phb_C_B03C_XService;
        private readonly IPhb_C_B03C_XDetailService _phb_C_B03C_XDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbCB03cXController(IPhb_C_B03C_XService phb_C_B03C_XService, IPhb_C_B03C_XDetailService phb_C_B03C_XDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb_C_B03C_XService = phb_C_B03C_XService;
            _phb_C_B03C_XDetailService = phb_C_B03C_XDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B03C_X bieu = new PHB_C_B03C_X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            bieu.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu.MA_DBHC_CHA = claimMaDbhc?.Value;
                            bieu.MA_CHUONG = "";
                            bieu.MA_QHNS = "";
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb_C_B03C_XService.Queryable().FirstOrDefaultAsync(x =>
                                                           x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                                           x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phb_C_B03C_XService.Insert(bieu);

                            int startRow = 16;
                            int endRow = 41 - startRow;
                            int sapxep = 1;
                            int phan = 0;
                            bool big = false;
                            double DT_TNSNN,DT_TNSX,QT_TNSNN,QT_TNSX;
                            for (int i = 0; i <= endRow; i++)
                            {
                                DT_TNSNN = 0;
                                DT_TNSX = 0;
                                QT_TNSNN = 0;
                                QT_TNSX = 0;
                                PHB_C_B03C_X_DETAIL detail = new PHB_C_B03C_X_DETAIL()
                                {
                                    PHB_C_B03C_X_REFID = bieu.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                if (workSheet.Cells[startRow + i, 1].Style.Font.Bold
                                    || workSheet.Cells[startRow + i, 1].Text.StartsWith("1.")
                                    || workSheet.Cells[startRow + i, 1].Text.StartsWith("2."))
                                {
                                    phan++;
                                    detail.PHAN = phan;
                                    if (big)
                                    {
                                        phan++;
                                        detail.PHAN = phan;
                                        big = false;
                                    }
                                }
                                else
                                {
                                    detail.PHAN = phan + 1;
                                    big = true;
                                }
                                detail.SAPXEP = sapxep;
                                detail.STT_CHI_TIEU = workSheet.Cells[startRow + i, 1].Text;
                                detail.TEN_CHI_TIEU = workSheet.Cells[startRow + i, 2].Text;
                                detail.MA_CHI_TIEU = workSheet.Cells[startRow + i, 3].Text;
                                double.TryParse(workSheet.Cells[startRow + i, 4].Text.ToString(),out DT_TNSNN);
                                detail.DT_TNSNN = DT_TNSNN;
                                double.TryParse(workSheet.Cells[startRow + i, 5].Text.ToString(), out DT_TNSX);
                                detail.DT_TNSX = DT_TNSX;
                                double.TryParse(workSheet.Cells[startRow + i, 6].Text.ToString(), out QT_TNSNN);
                                detail.QT_TNSNN = QT_TNSNN;
                                double.TryParse(workSheet.Cells[startRow + i, 7].Text.ToString(), out QT_TNSX);
                                detail.QT_TNSX = QT_TNSX;
                                sapxep++;
                                _phb_C_B03C_XDetailService.Insert(detail);
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
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ex.Message;
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
            var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B03C_X bieu = new PHB_C_B03C_X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            bieu.MA_CHUONG = "423";
                            bieu.MA_QHNS = "1032433";
                            bieu.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb_C_B03C_XService.Queryable().FirstOrDefaultAsync(x =>
                                                           x.MA_DBHC.Equals(bieu.MA_DBHC) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                                           x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phb_C_B03C_XService.Insert(bieu);

                            int startRow = 16;
                            int endRow = 41 - startRow;
                            int sapxep = 1;
                            int phan = 0;
                            bool big = false;
                            double DT_TNSNN, DT_TNSX, QT_TNSNN, QT_TNSX;
                            for (int i = 0; i <= endRow; i++)
                            {
                                DT_TNSNN = 0;
                                DT_TNSX = 0;
                                QT_TNSNN = 0;
                                QT_TNSX = 0;
                                PHB_C_B03C_X_DETAIL detail = new PHB_C_B03C_X_DETAIL()
                                {
                                    PHB_C_B03C_X_REFID = bieu.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                if (workSheet.Cells[startRow + i, 1].Style.Font.Bold
                                    || workSheet.Cells[startRow + i, 1].Text.StartsWith("1.")
                                    || workSheet.Cells[startRow + i, 1].Text.StartsWith("2."))
                                {
                                    phan++;
                                    detail.PHAN = phan;
                                    if (big)
                                    {
                                        phan++;
                                        detail.PHAN = phan;
                                        big = false;
                                    }
                                }
                                else
                                {
                                    detail.PHAN = phan + 1;
                                    big = true;
                                }
                                detail.SAPXEP = sapxep;
                                detail.STT_CHI_TIEU = workSheet.Cells[startRow + i, 1].Text;
                                detail.TEN_CHI_TIEU = workSheet.Cells[startRow + i, 2].Text;
                                detail.MA_CHI_TIEU = workSheet.Cells[startRow + i, 3].Text;
                                double.TryParse(workSheet.Cells[startRow + i, 4].Text.ToString(), out DT_TNSNN);
                                detail.DT_TNSNN = DT_TNSNN;
                                double.TryParse(workSheet.Cells[startRow + i, 5].Text.ToString(), out DT_TNSX);
                                detail.DT_TNSX = DT_TNSX;
                                double.TryParse(workSheet.Cells[startRow + i, 6].Text.ToString(), out QT_TNSNN);
                                detail.QT_TNSNN = QT_TNSNN;
                                double.TryParse(workSheet.Cells[startRow + i, 7].Text.ToString(), out QT_TNSX);
                                detail.QT_TNSX = QT_TNSX;
                                sapxep++;
                                _phb_C_B03C_XDetailService.Insert(detail);
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
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ex.Message;
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
            Response<C_B03C_XVm.ViewModel> response = new Response<C_B03C_XVm.ViewModel>();
            try
            {
                C_B03C_XVm.ViewModel data = new C_B03C_XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb_C_B03C_XDetailService.Queryable().Where(x => x.PHB_C_B03C_X_REFID.Equals(refid))
                    .OrderBy(x => x.SAPXEP)
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
            Response<C_B03C_XVm.ViewModel> response = new Response<C_B03C_XVm.ViewModel>();
            try
            {
                C_B03C_XVm.ViewModel data = new C_B03C_XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb_C_B03C_XDetailService.Queryable().Where(x => x.PHB_C_B03C_X_REFID.Equals(refid))
                    .OrderBy(x => x.PHAN).ThenBy(x => x.STT_CHI_TIEU).ThenBy(x => x.MA_CHI_TIEU)
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
            var response = await _phb_C_B03C_XService.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
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
            var response = await _phb_C_B03C_XService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.TEN_CHI_TIEU == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B03C_X_DETAIL detail = await _phb_C_B03C_XDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TEN_CHI_TIEU = entry.TEN_CHI_TIEU;
                                detail.TEN_CHI_TIEU = rqmodel.newName;
                                _phb_C_B03C_XDetailService.Update(detail);
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

        [HttpPut]
        public async Task<IHttpActionResult> Put(C_B03C_XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B03C_X cb03cx =
                    await _phb_C_B03C_XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (cb03cx == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B03C_X_DETAIL item = await _phb_C_B03C_XDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phb_C_B03C_XDetailService.Delete(item);
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
                        item.PHB_C_B03C_X_REFID = model.REFID;
                        _phb_C_B03C_XDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B03C_X_DETAIL detail = await _phb_C_B03C_XDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;
                            detail.DT_TNSNN = item.DT_TNSNN;
                            detail.DT_TNSX = item.DT_TNSX;
                            detail.QT_TNSNN = item.QT_TNSNN;
                            detail.QT_TNSX = item.QT_TNSX;
                            _phb_C_B03C_XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    cb03cx.NGAY_SUA = DateTime.Now;
                    cb03cx.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb_C_B03C_XService.Update(cb03cx);

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
    }
}