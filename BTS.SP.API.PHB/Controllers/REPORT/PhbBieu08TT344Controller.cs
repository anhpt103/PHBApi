using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;
using BTS.SP.PHB.SERVICE.Models.BIEU_08TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu08TT344;
using BTS.SP.PHB.SERVICE.UTILS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU08TT344")]
    [Route("{id?}")]
    public class PhbBieu08TT344Controller : ApiController
    {
        private readonly IPhbBieu08TT344Service _bieu08TT344Service;
        private readonly IPhbBieu08TT344TemplateService _bieu08TT344TemplateService;
        private readonly IPhbBieu08TT344DetailService _bieu08TT344DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu08TT344Controller(IPhbBieu08TT344Service bieu08TT344Service, IPhbBieu08TT344TemplateService bieu08TT344TemplateService,
            IPhbBieu08TT344DetailService bieu08TT344DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu08TT344Service = bieu08TT344Service;
            _bieu08TT344TemplateService = bieu08TT344TemplateService;
            _bieu08TT344DetailService = bieu08TT344DetailService;
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
                        PHB_BIEU08TT344 bieu08 = new PHB_BIEU08TT344()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu08.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu08.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu08.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu08.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
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

                            var checkReport = await _bieu08TT344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu08.MA_CHUONG) && x.MA_QHNS.Equals(bieu08.MA_QHNS) &&
                                x.NAM_BC == bieu08.NAM_BC && x.KY_BC == bieu08.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu08TT344Service.Insert(bieu08);

                            int startRow = 11;
                            int endRow = workSheet.Dimension.End.Row - startRow;
                            int sapxep = 1;
                            int phan = 0;
                            bool big = false;
                            for (int i = 0; i <= endRow; i++)
                            {
                                PHB_BIEU08TT344_DETAIL detail = new PHB_BIEU08TT344_DETAIL()
                                {
                                    PHB_BIEU08TT344_REFID = bieu08.REFID,
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
                                detail.NOIDUNG = workSheet.Cells[startRow + i, 1].Text;
                                detail.DUTOAN_NSNN = double.Parse(workSheet.Cells[startRow + i, 2].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 2].Text.Trim());
                                detail.DUTOAN_NSX = double.Parse(workSheet.Cells[startRow + i, 3].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 3].Text.Trim());
                                detail.QUYETTOAN_NSNN = double.Parse(workSheet.Cells[startRow + i, 4].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 4].Text.Trim());
                                detail.QUYETTOAN_NSX = double.Parse(workSheet.Cells[startRow + i, 5].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 5].Text.Trim());
                                detail.SOSANH_NSNN = double.Parse(workSheet.Cells[startRow + i, 6].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 6].Text.Trim());
                                detail.SOSANH_NSX = double.Parse(workSheet.Cells[startRow + i, 7].Text.Trim() == "" ? "" : workSheet.Cells[startRow + i, 7].Text.Trim());
                                sapxep++;
                                _bieu08TT344DetailService.Insert(detail);
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
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        PHB_BIEU08TT344 bieu08 = new PHB_BIEU08TT344()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N"),
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            bieu08.MA_CHUONG = "423";
                            //if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã đơn vị QHNS."
                            //});
                            bieu08.MA_QHNS = "1032433";
                            //bieu07.MA_QHNS = "1032433";
                            //bieu08.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu08.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu08.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            bieu08.MA_DBHC= httpRequest["MA_DBHC"];
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

                            var checkReport = await _bieu08TT344Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_DBHC.Equals(bieu08.MA_DBHC) && x.MA_QHNS.Equals(bieu08.MA_QHNS) &&
                                x.NAM_BC == bieu08.NAM_BC && x.KY_BC == bieu08.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu08TT344Service.Insert(bieu08);

                            int startRow = 11;
                            int endRow = workSheet.Dimension.End.Row - startRow;
                            int sapxep = 1;
                            int phan = 0;
                            bool big = false;
                            for (int i = 0; i <= endRow; i++)
                            {
                                PHB_BIEU08TT344_DETAIL detail = new PHB_BIEU08TT344_DETAIL()
                                {
                                    PHB_BIEU08TT344_REFID = bieu08.REFID,
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
                                detail.NOIDUNG = workSheet.Cells[startRow + i, 1].Text;
                                detail.DUTOAN_NSNN = double.Parse(workSheet.Cells[startRow + i, 2].Text.Trim());
                                detail.DUTOAN_NSX = double.Parse(workSheet.Cells[startRow + i, 3].Text.Trim());
                                detail.QUYETTOAN_NSNN = double.Parse(workSheet.Cells[startRow + i, 4].Text.Trim());
                                detail.QUYETTOAN_NSX = double.Parse(workSheet.Cells[startRow + i, 5].Text.Trim());
                                detail.SOSANH_NSNN = double.Parse(workSheet.Cells[startRow + i, 6].Text.Trim());
                                detail.SOSANH_NSX = double.Parse(workSheet.Cells[startRow + i, 7].Text.Trim());
                                sapxep++;
                                _bieu08TT344DetailService.Insert(detail);
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
            Response<BIEU08TT344Vm.ViewModel> response = new Response<BIEU08TT344Vm.ViewModel>();
            try
            {
                BIEU08TT344Vm.ViewModel data = new BIEU08TT344Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _bieu08TT344DetailService.Queryable().Where(x => x.PHB_BIEU08TT344_REFID.Equals(refid))
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
        public async Task<IHttpActionResult> Put(BIEU08TT344Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU08TT344 b02bctc =
                    await _bieu08TT344Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (b02bctc == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU08TT344_DETAIL item = await _bieu08TT344DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu08TT344DetailService.Delete(item);
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
                        item.PHB_BIEU08TT344_REFID = model.REFID;
                        _bieu08TT344DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU08TT344_DETAIL detail = await _bieu08TT344DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.NOIDUNG = item.NOIDUNG;
                            detail.DUTOAN_NSNN = item.DUTOAN_NSNN;
                            detail.DUTOAN_NSX = item.DUTOAN_NSX;
                            detail.QUYETTOAN_NSNN = item.QUYETTOAN_NSNN;
                            detail.QUYETTOAN_NSX = item.QUYETTOAN_NSX;
                            detail.SOSANH_NSNN = item.SOSANH_NSNN;
                            detail.SOSANH_NSX = item.SOSANH_NSX;
                            _bieu08TT344DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    b02bctc.NGAY_SUA = DateTime.Now;
                    b02bctc.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu08TT344Service.Update(b02bctc);

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
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            var response = await _bieu08TT344Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var response = await _bieu08TT344Service.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName);
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
                            PHB_BIEU08TT344_DETAIL detail = await _bieu08TT344DetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.NOIDUNG_OLD = entry.NOIDUNG;
                                detail.NOIDUNG = rqmodel.newName;
                                _bieu08TT344DetailService.Update(detail);
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
    }
}