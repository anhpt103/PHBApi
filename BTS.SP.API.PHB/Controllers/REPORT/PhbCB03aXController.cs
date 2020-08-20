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
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU12TT344;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.REPORT.C_B03A_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03A_X;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.Models.C_B03A_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03B_X;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbc_B03A_X")]
    [Route("{id?}")]
    public class PhbCB03AXController : ApiController
    {
        private readonly IPHB_C_B03A_XService _phb_C_B03A_XService;
        private readonly IPhb_C_B03A_XDetailService _phb_C_B03A_XDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbCB03AXController(IPHB_C_B03A_XService phb_C_B03A_XService, IPhb_C_B03A_XDetailService phb_C_B03A_XDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb_C_B03A_XService = phb_C_B03A_XService;
            _phb_C_B03A_XDetailService = phb_C_B03A_XDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
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
                            PHB_C_B03A_X bieu = new PHB_C_B03A_X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                                MA_DBHC_CHA = firstOrDefault?.Value
                                //MA_DBHC = firstOrDefault?.Value,
                                //TEN_QHNS = "ABC",
                            };
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            //bieu.MA_CHUONG = httpRequest["MA_CHUONG"];
                            bieu.MA_CHUONG = "A";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            //bieu.MA_QHNS = httpRequest["MA_QHNS"];

                            bieu.MA_QHNS = "012345";
                            bieu.MA_DBHC = httpRequest["MA_DBHC"];

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


                            var checkReport = await _phb_C_B03A_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb_C_B03A_XService.Insert(bieu);

                            int startRow = 9;
                            string loai = string.Empty;
                            while (workSheet.Cells[startRow, 3].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 3].Value.ToString()))
                            {
                                PHB_C_B03A_X_DETAIL detail = new PHB_C_B03A_X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B03A_X_REFID = bieu.REFID,
                                };
                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.NOI_DUNG_THU = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_TIEN = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_TIEN = 0;
                                }
                                if (workSheet.Cells[startRow, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.LUY_KE = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.LUY_KE = 0;
                                }
                                if (workSheet.Cells[startRow, 1].Value != null)
                                {
                                    loai = "0";
                                    detail.MA_LOAI = loai;
                                }
                                if (workSheet.Cells[startRow, 2].Value != null && workSheet.Cells[startRow, 1].Value == null)
                                {
                                    loai = "1";
                                    detail.MA_LOAI = loai;
                                }

                                _phb_C_B03A_XDetailService.Insert(detail);
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
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
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
                            PHB_C_B03A_X bieu = new PHB_C_B03A_X()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N"),
                                MA_DBHC_CHA = firstOrDefault?.Value
                            };
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

                            var checkReport = await _phb_C_B03A_XService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu.MA_DBHC) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC && x.MA_DBHC.Equals(bieu.MA_DBHC));
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb_C_B03A_XService.Insert(bieu);

                            int startRow = 9;
                            string loai = string.Empty;
                            while (workSheet.Cells[startRow, 3].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 3].Value.ToString()))
                            {
                                PHB_C_B03A_X_DETAIL detail = new PHB_C_B03A_X_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_C_B03A_X_REFID = bieu.REFID,
                                };
                                detail.MA_CHUONG = workSheet.Cells[startRow, 1].Value != null
                                ? workSheet.Cells[startRow, 1].Value.ToString()
                                : null;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 2].Value != null
                                ? workSheet.Cells[startRow, 2].Value.ToString()
                                : null;
                                detail.NOI_DUNG_THU = workSheet.Cells[startRow, 3].Value != null
                                ? workSheet.Cells[startRow, 3].Value.ToString()
                                : null;
                                if (workSheet.Cells[startRow, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_TIEN = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.SO_TIEN = 0;
                                }
                                if (workSheet.Cells[startRow, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.LUY_KE = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.LUY_KE = 0;
                                }
                                if (workSheet.Cells[startRow, 1].Value != null)
                                {
                                    loai = "0";
                                    detail.MA_LOAI = loai;
                                }
                                if (workSheet.Cells[startRow, 2].Value != null && workSheet.Cells[startRow, 1].Value == null)
                                {
                                    loai = "1";
                                    detail.MA_LOAI = loai;
                                }

                                _phb_C_B03A_XDetailService.Insert(detail);
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
            Response<C_B03A_XVm.ViewModel> response = new Response<C_B03A_XVm.ViewModel>();
            try
            {
                C_B03A_XVm.ViewModel data = new C_B03A_XVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb_C_B03A_XDetailService.Queryable().Where(x => x.PHB_C_B03A_X_REFID.Equals(refid))
                    .OrderBy(x => x.ID).ThenBy(x => x.MA_LOAI)
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
        public async Task<IHttpActionResult> Post(C_B03A_XVm.InsertModel model)
        {
            //if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
            //    model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            //Response<string> response = new Response<string>();
            if (model.NAM_BC < 0 ||
               model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            try
            {
                PHB_C_B03A_X bieu = new PHB_C_B03A_X()
                {
                    //KY_BC = model.KY_BC,
                    //NAM_BC = model.NAM_BC,
                    //MA_CHUONG = model.MA_CHUONG,
                    //MA_QHNS = model.MA_QHNS,
                    //NGAY_TAO = DateTime.Now,
                    //NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    //ObjectState = ObjectState.Added,
                    //TRANG_THAI = 0,
                    //REFID = Guid.NewGuid().ToString("N")
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = "A",
                    MA_QHNS = "012345",
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    MA_DBHC = model.MA_DBHC,
                    MA_DBHC_CHA = firstOrDefault?.Value,
                };

                var checkReport = await _phb_C_B03A_XService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                    x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phb_C_B03A_XService.Insert(bieu);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_C_B03A_X_REFID = bieu.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phb_C_B03A_XDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(C_B03A_XVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_C_B03A_X bieu =
                    await _phb_C_B03A_XService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_C_B03A_X_DETAIL item = await _phb_C_B03A_XDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _phb_C_B03A_XDetailService.Delete(item);
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
                        item.PHB_C_B03A_X_REFID = model.REFID;
                        _phb_C_B03A_XDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_C_B03A_X_DETAIL detail = await _phb_C_B03A_XDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_CHUONG = item.MA_CHUONG;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_THU = item.NOI_DUNG_THU;
                            detail.SO_TIEN = item.SO_TIEN;
                            detail.LUY_KE = item.LUY_KE;
                            _phb_C_B03A_XDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu.NGAY_SUA = DateTime.Now;
                    bieu.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb_C_B03A_XService.Update(bieu);

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
            var response = await _phb_C_B03A_XService.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rqmodel"></param>
        /// <returns></returns>
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
            var response = await _phb_C_B03A_XService.MergeReport(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC, rqmodel.changeList, rqmodel.newName);
            Response<string> msg = new Response<string>();

            try
            {
                if (response.Data.DETAIL.Count > 0)
                {
                    foreach (var item in rqmodel.changeList)
                    {
                        var foundLst = response.Data.DETAIL.Where(x => x.NOI_DUNG_THU == item);

                        foreach (var entry in foundLst)
                        {
                            PHB_C_B03A_X_DETAIL detail = await _phb_C_B03A_XDetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.NOI_DUNG_THU_OLD = entry.NOI_DUNG_THU;
                                detail.NOI_DUNG_THU = rqmodel.newName;
                                _phb_C_B03A_XDetailService.Update(detail);
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
            var response = await _phb_C_B03A_XService.SumReport_HTML(rqmodel.MA_DBHC, madbhc_cha, rqmodel.NAM_BC, rqmodel.KY_BC);
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
                var foundLst = await _phb_C_B03A_XDetailService.Queryable()
                    .Where(x => x.NOI_DUNG_THU == rqmodel.NOI_DUNG_THU && x.NOI_DUNG_THU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_C_B03A_X_DETAIL detail = await _phb_C_B03A_XDetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.NOI_DUNG_THU = entry.NOI_DUNG_THU_OLD;
                        detail.NOI_DUNG_THU_OLD = null;
                        _phb_C_B03A_XDetailService.Update(detail);
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
