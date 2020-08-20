using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.REPORT.C_B02B_X;
using OfficeOpenXml;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Rp.C_B02B_X;
using Repository.Pattern.Infrastructure;
using System.Data.Entity;
using BTS.SP.PHB.SERVICE.Models.C_B02B_X;
using BTS.SP.PHB.ENTITY;
using System.Security.Claims;
using BTS.SP.PHB.ENTITY.Rp;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbC_B02B_X")]
    [Route("{id?}")]
    public class C_B02B_XController : ApiController
    {
        private readonly IC_B02B_XService _C_B02B_XService;
        private readonly IC_B02B_XDetailService _C_B02B_XDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public C_B02B_XController(IC_B02B_XService C_B02B_XService,
            IC_B02B_XDetailService C_B02B_XDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _C_B02B_XService = C_B02B_XService;
            _C_B02B_XDetailService = C_B02B_XDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var claimMaDbhcCha = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            int NAM_BC = int.Parse(httpRequest.Form["NAM_BC"]);
            int KY_BC = int.Parse(httpRequest.Form["KY_BC"]);
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B02B_X bieu08 = new PHB_C_B02B_X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            //MA_DBHC = httpRequest["MA_DBHC"],
                            MA_DBHC_CHA = claimMaDbhcCha?.Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã địa bàn hành chính."
                            });
                            bieu08.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu08.NAM_BC = NAM_BC;
                            bieu08.KY_BC = KY_BC;
                            bieu08.MA_CHUONG = "Non";
                            bieu08.MA_QHNS = "None";
                            var checkReport = await _C_B02B_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu08.MA_DBHC) && x.MA_DBHC.Equals(bieu08.MA_DBHC) &&
                                x.NAM_BC == bieu08.NAM_BC && x.KY_BC == bieu08.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B02B_XService.Insert(bieu08);

                            int startRow = 13;
                            int endRow = workSheet.Dimension.End.Row;
                            int sapxep = 1;
                            for (int i = startRow; i <= endRow; i++)
                            {
                                PHB_C_B02B_X_DETAIL detail = new PHB_C_B02B_X_DETAIL()
                                {
                                    PHB_C_B02B_X_REFID = bieu08.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                detail.SAPXEP = sapxep;
                                detail.STT = workSheet.Cells[i, 1].Text.Trim();
                                detail.NOIDUNG = workSheet.Cells[i, 2].Text;
                                detail.MASO = int.Parse(workSheet.Cells[i, 3].Text);
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 4].Text))
                                {
                                    detail.DUTOANNAM = double.Parse(workSheet.Cells[i, 4].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 5].Text))
                                {
                                    detail.TRONGTHANG = double.Parse(workSheet.Cells[i, 5].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 6].Text))
                                {
                                    detail.LUYKE = double.Parse(workSheet.Cells[i, 6].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 7].Text))
                                {
                                    detail.SOSANH = double.Parse(workSheet.Cells[i, 7].Text);
                                }
                                sapxep++;
                                _C_B02B_XDetailService.Insert(detail);
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
            var claimMaDbhcCha = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            int NAM_BC = int.Parse(httpRequest.Form["NAM_BC"]);
            int KY_BC = int.Parse(httpRequest.Form["KY_BC"]);
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B02B_X bieu08 = new PHB_C_B02B_X()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            //MA_DBHC = httpRequest["MA_DBHC"],
                            MA_DBHC_CHA = claimMaDbhcCha?.Value
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            bieu08.MA_CHUONG = "423";
                            bieu08.MA_QHNS = "1032433";
                            bieu08.MA_DBHC = httpRequest["MA_DBHC"];
                            bieu08.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu08.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu08.KY_BC = int.Parse(httpRequest["KY_BC"]);
                            var checkReport = await _C_B02B_XService.Queryable().FirstOrDefaultAsync(x => x.MA_DBHC == bieu08.MA_DBHC && x.NAM_BC == bieu08.NAM_BC && x.KY_BC == bieu08.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _C_B02B_XService.Insert(bieu08);

                            int startRow = 13;
                            int endRow = workSheet.Dimension.End.Row;
                            int sapxep = 1;
                            for (int i = startRow; i <= endRow; i++)
                            {
                                PHB_C_B02B_X_DETAIL detail = new PHB_C_B02B_X_DETAIL()
                                {
                                    PHB_C_B02B_X_REFID = bieu08.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                detail.SAPXEP = sapxep;
                                detail.STT = workSheet.Cells[i, 1].Text.Trim();
                                detail.NOIDUNG = workSheet.Cells[i, 2].Text;
                                detail.MASO = int.Parse(workSheet.Cells[i, 3].Text);
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 4].Text))
                                {
                                    detail.DUTOANNAM = double.Parse(workSheet.Cells[i, 4].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 5].Text))
                                {
                                    detail.TRONGTHANG = double.Parse(workSheet.Cells[i, 5].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 6].Text))
                                {
                                    detail.LUYKE = double.Parse(workSheet.Cells[i, 6].Text);
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[i, 7].Text))
                                {
                                    detail.SOSANH = double.Parse(workSheet.Cells[i, 7].Text);
                                }
                                sapxep++;
                                _C_B02B_XDetailService.Insert(detail);
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
            Response<C_B02B_XVm.ViewModel> response = new Response<C_B02B_XVm.ViewModel>();
            try
            {
                C_B02B_XVm.ViewModel data = new C_B02B_XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _C_B02B_XDetailService.Queryable().Where(x => x.PHB_C_B02B_X_REFID.Equals(refid))
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
        [HttpPut]
        public async Task<IHttpActionResult> Put(C_B02B_XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B02B_X b02bx =
                    await _C_B02B_XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (b02bx == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B02B_X_DETAIL detail = await _C_B02B_XDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.DUTOANNAM = item.DUTOANNAM;
                            detail.TRONGTHANG = item.TRONGTHANG;
                            detail.LUYKE = item.LUYKE;
                            detail.SOSANH = item.SOSANH;
                            _C_B02B_XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    b02bx.NGAY_SUA = DateTime.Now;
                    b02bx.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _C_B02B_XService.Update(b02bx);

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
            var response = await _C_B02B_XService.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
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
            var response = await _C_B02B_XService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.NOIDUNG == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B02B_X_DETAIL detail = await _C_B02B_XDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.NOIDUNG_OLD = entry.NOIDUNG;
                                detail.NOIDUNG = rqmodel.newName;
                                _C_B02B_XDetailService.Update(detail);
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
            var response = await _C_B02B_XService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
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
                var foundLst = await _C_B02B_XDetailService.Queryable()
                    .Where(x => x.NOIDUNG == rqmodel.NOIDUNG && x.NOIDUNG_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B02B_X_DETAIL detail = await _C_B02B_XDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.NOIDUNG = entry.NOIDUNG_OLD;
                        detail.NOIDUNG_OLD = null;
                        _C_B02B_XDetailService.Update(detail);
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
    }
}