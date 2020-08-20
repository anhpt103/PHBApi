using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;
using BTS.SP.PHB.SERVICE.REPORT.C_B02aX;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbC_B02AX")]
    [Route("{id?}")]
    public class PhbC_B02aXController:ApiController
    {

        private readonly IB02aXService _B02aXService;
        private readonly IB02aXDetailService _B02aXDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbC_B02aXController(IB02aXService B02aXService,
            IB02aXDetailService B02aXDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _B02aXService = B02aXService;
            _B02aXDetailService = B02aXDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;
            
            string MA_DBHC = httpRequest.Form["MA_DBHC"];
            int NAM_BC = int.Parse(httpRequest.Form["NAM_BC"]);
            int KY_BC = int.Parse(httpRequest.Form["KY_BC"]);
            int count = 0;
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B02AX B02aX = new PHB_C_B02AX()
                        {                          
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            NAM_BC = NAM_BC,
                            MA_DBHC_CHA = firstOrDefault?.Value,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            KY_BC = KY_BC
                        };
                        var REFID = B02aX.REFID;
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {

                            B02aX.MA_DBHC = MA_DBHC;
                            B02aX.MA_CHUONG = "Non";
                            B02aX.MA_QHNS = "None";
                            //if (string.IsNullOrEmpty(TEN_QHNS)) TEN_QHNS = MA_QHNS;
                            //B02aX.TEN_QHNS = TEN_QHNS;

                            int start_Row = 14;

                            _B02aXService.Insert(B02aX);
                            for (int r = start_Row; r <= workSheet.Dimension.End.Row; r++)
                            {
                                var obj = new PHB_C_B02AX_DETAIL() { PHB_C_B02AX_REFID = B02aX.REFID, ObjectState = ObjectState.Added };
                                obj.STT = workSheet.Cells[r, 1].Text;
                                obj.NOI_DUNG = workSheet.Cells[r, 2].Text;
                                obj.MA_SO = workSheet.Cells[r, 3].Text;
                                obj.SAP_XEP = count++;
                                obj.DU_TOAN = workSheet.Cells[r, 4].Value != null ? decimal.Parse(workSheet.Cells[r, 4].Value.ToString()) : 0;
                                obj.TH_TRONG_THANG = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.TH_LUYKE_DN = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                obj.SO_SANH = workSheet.Cells[r, 7].Value != null ? decimal.Parse(workSheet.Cells[r, 7].Value.ToString()) : 0;
                                _B02aXDetailService.Insert(obj);
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = REFID;
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

        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            var httpRequest = HttpContext.Current.Request;

            string MA_DBHC = httpRequest.Form["MA_DBHC"];
            int NAM_BC = int.Parse(httpRequest.Form["NAM_BC"]);
            int KY_BC = int.Parse(httpRequest.Form["KY_BC"]);
            int count = 0;
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            Response<string> response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_C_B02AX B02aX = new PHB_C_B02AX()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            NAM_BC = NAM_BC,
                            MA_DBHC_CHA = firstOrDefault?.Value,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                            KY_BC = KY_BC
                        };
                        var REFID = B02aX.REFID;
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {

                            B02aX.MA_CHUONG = "423";
                            B02aX.MA_QHNS = "1032433";
                            B02aX.MA_DBHC = httpRequest["MA_DBHC"];
                            B02aX.MA_DBHC_CHA = httpRequest["MA_DBHC_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            B02aX.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            B02aX.KY_BC = int.Parse(httpRequest["KY_BC"]);
                            var checkReport = await _B02aXService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(B02aX.MA_DBHC) && x.MA_QHNS.Equals(B02aX.MA_QHNS) &&
                                x.NAM_BC == B02aX.NAM_BC && x.KY_BC == B02aX.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            int start_Row = 14;

                            _B02aXService.Insert(B02aX);
                            for (int r = start_Row; r <= workSheet.Dimension.End.Row; r++)
                            {
                                var obj = new PHB_C_B02AX_DETAIL() { PHB_C_B02AX_REFID = B02aX.REFID, ObjectState = ObjectState.Added };
                                obj.STT = workSheet.Cells[r, 1].Text;
                                obj.NOI_DUNG = workSheet.Cells[r, 2].Text;
                                obj.MA_SO = workSheet.Cells[r, 3].Text;
                                obj.SAP_XEP = count++;
                                obj.DU_TOAN = workSheet.Cells[r, 4].Value != null ? decimal.Parse(workSheet.Cells[r, 4].Value.ToString()) : 0;
                                obj.TH_TRONG_THANG = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.TH_LUYKE_DN = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                obj.SO_SANH = workSheet.Cells[r, 7].Value != null ? decimal.Parse(workSheet.Cells[r, 7].Value.ToString()) : 0;
                                _B02aXDetailService.Insert(obj);
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = REFID;
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

        #region Get Detail
        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            var response = new Response<PHB_C_B02AXVm.DataRes>();
            var res = new PHB_C_B02AXVm.DataRes();
            try
            {
                //lay nam BC
                var ins = _B02aXService.GetAll().FirstOrDefault(x => x.REFID.Equals(refid));
                var data = _B02aXDetailService.GetAll().Where(x => x.PHB_C_B02AX_REFID.Equals(refid)).OrderBy(x => x.SAP_XEP).ToList();

                res.Body = data;
                res.NAM_BC = ins.NAM_BC;
                response.Error = false;
                response.Data = res;
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

        #region Update
        [HttpPut]
        public async Task<IHttpActionResult> Put(PHB_C_B02AXVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var c_b02ax =
                    await _B02aXService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (c_b02ax == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var id in model.LstDelete)
                    {
                        var item = await _B02aXDetailService.FindByIdAsync(id);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _B02aXDetailService.Delete(item);
                        }
                    }
                }

                #endregion

                #region add

                if (model.LstAdd != null && model.LstAdd.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstAdd)
                    {
                        item.ObjectState = ObjectState.Added;
                        item.PHB_C_B02AX_REFID = c_b02ax.REFID;
                        _B02aXDetailService.Insert(item);
                    }
                }

                #endregion

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _B02aXDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.STT = item.STT;
                            detail.SAP_XEP = item.SAP_XEP;
                            detail.NOI_DUNG = item.NOI_DUNG;
                            detail.MA_SO = item.MA_SO;
                            detail.DU_TOAN = item.DU_TOAN;
                            detail.TH_TRONG_THANG = item.TH_TRONG_THANG;
                            detail.TH_LUYKE_DN = item.TH_LUYKE_DN;
                            detail.SO_SANH = item.SO_SANH;
                            _B02aXDetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    c_b02ax.NGAY_SUA = DateTime.Now;
                    c_b02ax.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _B02aXService.Update(c_b02ax);
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
        #endregion

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
            var response = await _B02aXService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.NOI_DUNG == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B02AX_DETAIL detail = await _B02aXDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.NOI_DUNG_OLD = entry.NOI_DUNG;
                                detail.NOI_DUNG = rqmodel.newName;
                                _B02aXDetailService.Update(detail);
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
            var response = await _B02aXService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
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
                var foundLst = await _B02aXDetailService.Queryable()
                    .Where(x => x.NOI_DUNG == rqmodel.NOI_DUNG && x.NOI_DUNG_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B02AX_DETAIL detail = await _B02aXDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.NOI_DUNG = entry.NOI_DUNG_OLD;
                        detail.NOI_DUNG_OLD = null;
                        _B02aXDetailService.Update(detail);
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