using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.Models.BIEU70NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu70NS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU70NS")]
    [Route("{id?}")]
    public class PhbBieu70NsController : ApiController
    {
        private readonly IPhbBieu70NsService _phb70NsService;
        private readonly IPhbBieu70NsDetailService _phb70NsDetailService;
        private readonly IPhbBieu70NsTemplateService _phb70NsTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu70NsController(IPhbBieu70NsService phb70NsService, IPhbBieu70NsDetailService phb70NsDetailService, IPhbBieu70NsTemplateService phb70NsTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb70NsService = phb70NsService;
            _phb70NsDetailService = phb70NsDetailService;
            _phb70NsTemplateService = phb70NsTemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var phb70Ns = new PHB_BIEU70NS()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            phb70Ns.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            phb70Ns.MA_QHNS = httpRequest["MA_QHNS"];
                            phb70Ns.TEN_QHNS = httpRequest["TEN_QHNS"];
                            phb70Ns.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            phb70Ns.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            phb70Ns.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb70NsService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(phb70Ns.MA_CHUONG) && x.MA_QHNS.Equals(phb70Ns.MA_QHNS) &&
                                x.NAM_BC == phb70Ns.NAM_BC && x.KY_BC == phb70Ns.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _phb70NsService.Insert(phb70Ns);

                            var startRow = 8;
                            var endRow = 14;
                            for (var i = startRow; i <= endRow; i++)
                            {
                                var detail=new PHB_BIEU70NS_DETAIL()
                                {
                                    PHB_BIEU70NS_REFID = phb70Ns.REFID,
                                    ObjectState = ObjectState.Added,
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[i, 1].Value !=null ? workSheet.Cells[i, 1].Value.ToString():null ;
                                detail.MA_CHI_TIEU = detail.STT_CHI_TIEU ?? "";
                                detail.TEN_CHI_TIEU = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : null;
                                if (workSheet.Cells[i, 3].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_TIEN_NT = double.Parse(workSheet.Cells[i, 3].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                if (workSheet.Cells[i, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.SO_TIEN_NBC = double.Parse(workSheet.Cells[i, 4].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                detail.GIAI_TRINH = workSheet.Cells[i, 7].Value != null
                                    ? workSheet.Cells[i, 7].Value.ToString()
                                    : null;
                                _phb70NsDetailService.Insert(detail);
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
            Response<BIEU70NSVm.ViewModel> response = new Response<BIEU70NSVm.ViewModel>();
            try
            {
                BIEU70NSVm.ViewModel data = new BIEU70NSVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _phb70NsDetailService.Queryable().Where(x => x.PHB_BIEU70NS_REFID.Equals(refid))
                    .OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
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
            Response<List<PHB_BIEU70NS_TEMPLATE>> response = new Response<List<PHB_BIEU70NS_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phb70NsTemplateService.Queryable()
                    .OrderBy(x => x.MA_CHI_TIEU)
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

        [HttpPost]
        public async Task<IHttpActionResult> Post(BIEU70NSVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu70Ns = new PHB_BIEU70NS()
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
                var checkReport = await _phb70NsService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu70Ns.MA_CHUONG) && x.MA_QHNS.Equals(bieu70Ns.MA_QHNS) &&
                    x.NAM_BC == bieu70Ns.NAM_BC && x.KY_BC == bieu70Ns.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phb70NsService.Insert(bieu70Ns);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU70NS_REFID = bieu70Ns.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phb70NsDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU70NSVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu70Ns =
                    await _phb70NsService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu70Ns == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _phb70NsDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.SO_TIEN_NBC = item.SO_TIEN_NBC;
                            detail.SO_TIEN_NT = item.SO_TIEN_NT;
                            detail.GIAI_TRINH = item.GIAI_TRINH;
                            _phb70NsDetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    bieu70Ns.NGAY_SUA = DateTime.Now;
                    bieu70Ns.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb70NsService.Update(bieu70Ns);
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

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _phb70NsService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _phb70NsService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU70NS/Template.xlsx");
                string exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU70NS/" + RequestContext.Principal.Identity.Name
                                                                                     + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A2"].Value = sheet.Cells["A2"].Value + " " + rqmodel.NAM_BC;
                        var startRow = 8;
                        sheet.InsertRow(startRow, data.Data.DETAIL.Count);
                        for (var i = 0; i < data.Data.DETAIL.Count; i++)
                        {
                            for (var j = 1; j <= 6; j++)
                            {
                                sheet.Cells[startRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                sheet.Cells[startRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                            }
                            sheet.Cells[startRow + i, 1].Value = data.Data.DETAIL[i].STT_CHI_TIEU;
                            sheet.Cells[startRow + i, 2].Value = data.Data.DETAIL[i].TEN_CHI_TIEU;
                            sheet.Cells[startRow + i, 3].Value = data.Data.DETAIL[i].SO_TIEN_NT;
                            sheet.Cells[startRow + i, 4].Value = data.Data.DETAIL[i].SO_TIEN_NBC;
                            var temp = data.Data.DETAIL[i].SO_TIEN_NBC - data.Data.DETAIL[i].SO_TIEN_NT;
                            sheet.Cells[startRow + i, 5].Value = temp;
                            if (data.Data.DETAIL[i].SO_TIEN_NT > 0)
                            {
                                sheet.Cells[startRow + i, 6].Value = temp / data.Data.DETAIL[i].SO_TIEN_NT;
                            }
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_BIEU70NS.xlsx"
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
    }
}
