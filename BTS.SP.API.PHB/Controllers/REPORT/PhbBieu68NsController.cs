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
using BTS.SP.PHB.ENTITY.Rp.BIEU68NS;
using BTS.SP.PHB.SERVICE.Models.BIEU68NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu68NS;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Security.Claims;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU68NS")]
    [Route("{id?}")]
    public class PhbBieu68NsController : ApiController
    {
        private readonly IPhbBieu68NsService _phb68NsService;
        private readonly IPhbBieu68NsDetailService _phb68NsDetailService;
        private readonly IPhbBieu68NsTemplateService _phb68NsTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu68NsController(IPhbBieu68NsService phb68NsService, IPhbBieu68NsDetailService phb68NsDetailService, IPhbBieu68NsTemplateService phb68NsTemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _phb68NsService = phb68NsService;
            _phb68NsDetailService = phb68NsDetailService;
            _phb68NsTemplateService = phb68NsTemplateService;
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
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            var phb68Ns = new PHB_BIEU68NS()
                            {
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                NGAY_TAO = DateTime.Now,
                                ObjectState = ObjectState.Added,
                                REFID = Guid.NewGuid().ToString("N"),
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            phb68Ns.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            phb68Ns.MA_QHNS = httpRequest["MA_QHNS"];
                            phb68Ns.TEN_QHNS = httpRequest["TEN_QHNS"];
                            phb68Ns.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            phb68Ns.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            phb68Ns.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _phb68NsService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(phb68Ns.MA_CHUONG) && x.MA_QHNS.Equals(phb68Ns.MA_QHNS) &&
                                x.NAM_BC == phb68Ns.NAM_BC && x.KY_BC == phb68Ns.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _phb68NsService.Insert(phb68Ns);

                            var startRow = 9;
                            var endRow = 26;
                            var j = 0;
                            for (var i = startRow; i <= endRow; i++)
                            {
                                j++;
                                var detail=new PHB_BIEU68NS_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,PHB_BIEU68NS_REFID = phb68Ns.REFID,
                                };
                                if (j < 10)
                                {
                                    detail.MA_CHI_TIEU = "0" + j;
                                }
                                else
                                {
                                    detail.MA_CHI_TIEU = j.ToString();
                                }
                                detail.STT_CHI_TIEU= workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : null;
                                detail.TEN_CHI_TIEU= workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : null;
                                if (workSheet.Cells[i, 3].Value != null)
                                {
                                    try
                                    {
                                        detail.TONG_SO = double.Parse(workSheet.Cells[i, 3].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.TONG_SO = 0;
                                }
                                if (workSheet.Cells[i, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.DU_PHONG = double.Parse(workSheet.Cells[i, 4].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.DU_PHONG = 0;
                                }

                                if (workSheet.Cells[i, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.TANG_THU = double.Parse(workSheet.Cells[i, 5].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.TANG_THU = 0;
                                }

                                if (workSheet.Cells[i, 6].Value != null)
                                {
                                    try
                                    {
                                        detail.THUONG_VUOT_DTTHU = double.Parse(workSheet.Cells[i, 6].Value.ToString());
                                    }
                                    catch (Exception)
                                    {
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.THUONG_VUOT_DTTHU = 0;
                                }
                                detail.GHI_CHU = workSheet.Cells[i, 7].Value != null ? workSheet.Cells[i, 7].Value.ToString() : null;
                                _phb68NsDetailService.Insert(detail);
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
            var response = new Response<BIEU68NSVm.DetailModel>();
            try
            {
                var data = new BIEU68NSVm.DetailModel {REFID = refid};

                var templates = _phb68NsTemplateService.Queryable();
                var details = _phb68NsDetailService.Queryable().Where(x => x.PHB_BIEU68NS_REFID.Equals(refid));
                var temp = from detail in details
                    join template in templates on detail.MA_CHI_TIEU equals template.MA_CHI_TIEU orderby template.MA_CHI_TIEU select new BIEU68NSVm.DetailModel.Item()
                    {
                        ID = detail.ID,
                        PHB_BIEU68NS_REFID = refid,
                        STT_CHI_TIEU = template.STT_CHI_TIEU,
                        MA_CHI_TIEU = detail.MA_CHI_TIEU,
                        TEN_CHI_TIEU = template.TEN_CHI_TIEU,
                        TONG_SO = detail.TONG_SO,
                        DU_PHONG = detail.DU_PHONG,
                        TANG_THU = detail.TANG_THU,
                        THUONG_VUOT_DTTHU = detail.THUONG_VUOT_DTTHU,
                        GHI_CHU = detail.GHI_CHU,
                        LOAI = template.LOAI,
                        INDAM = template.INDAM,
                        INNGHIENG = template.INNGHIENG,
                    };
                 
                data.DETAIL = await temp.ToListAsync();
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
            var response = new Response<List<PHB_BIEU68NS_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phb68NsTemplateService.Queryable()
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
        public async Task<IHttpActionResult> Post(BIEU68NSVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu68Ns = new PHB_BIEU68NS()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = model.MA_CHUONG,
                    MA_QHNS = model.MA_QHNS,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA
                };
                var checkReport = await _phb68NsService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu68Ns.MA_CHUONG) && x.MA_QHNS.Equals(bieu68Ns.MA_QHNS) &&
                    x.NAM_BC == bieu68Ns.NAM_BC && x.KY_BC == bieu68Ns.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _phb68NsService.Insert(bieu68Ns);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU68NS_REFID = bieu68Ns.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _phb68NsDetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU68NSVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu68Ns = await _phb68NsService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu68Ns == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _phb68NsDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TONG_SO = item.TONG_SO;
                            detail.TANG_THU = item.TANG_THU;
                            detail.DU_PHONG = item.DU_PHONG;
                            detail.THUONG_VUOT_DTTHU = item.THUONG_VUOT_DTTHU;
                            detail.GHI_CHU = item.GHI_CHU;
                            _phb68NsDetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    bieu68Ns.NGAY_SUA = DateTime.Now;
                    bieu68Ns.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _phb68NsService.Update(bieu68Ns);
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
            var firstOrDefault = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            var madbhc = "-1";
            if (!string.IsNullOrEmpty(firstOrDefault?.Value))
            {
                madbhc = firstOrDefault.Value;
            }
            var response = await _phb68NsService.SumReport(madbhc, rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        //[Route("ExportReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        //{
        //    var data = await _phb68NsService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
        //    {
        //        string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU68NS/Template.xlsx");
        //        string exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU68NS/" + RequestContext.Principal.Identity.Name
        //                                                                             + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
        //        try
        //        {
        //            using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
        //            {
        //                ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];
        //                sheet.Cells["A3"].Value = sheet.Cells["A3"].Value + " " + rqmodel.NAM_BC;
        //                sheet.InsertRow(9, data.Data.DETAIL.Count);
        //                for (var i = 0; i < data.Data.DETAIL.Count; i++)
        //                {
        //                    for (var j = 1; j <= 6; j++)
        //                    {
        //                        sheet.Cells[9 + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                        sheet.Cells[9 + i, j].Style.Border.Right.Color.SetColor(Color.Black);
        //                    }
        //                    sheet.Cells[9 + i, 1].Value = data.Data.DETAIL[i].STT_CHI_TIEU;
        //                    sheet.Cells[9 + i, 2].Value = data.Data.DETAIL[i].TEN_CHI_TIEU;
        //                    sheet.Cells[9 + i, 3].Value = data.Data.DETAIL[i].TONG_SO;
        //                    sheet.Cells[9 + i, 4].Value = data.Data.DETAIL[i].DU_PHONG;
        //                    sheet.Cells[9 + i, 5].Value = data.Data.DETAIL[i].TANG_THU;
        //                    sheet.Cells[9 + i, 6].Value = data.Data.DETAIL[i].THUONG_VUOT_DTTHU;
        //                }
        //                excelPackage.SaveAs(new FileInfo(exportUrlFile));
        //                var result = new HttpResponseMessage(HttpStatusCode.OK)
        //                {
        //                    Content = new ByteArrayContent(excelPackage.GetAsByteArray())
        //                };
        //                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //                {
        //                    FileName = "export_BIEU68NS.xlsx"
        //                };
        //                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //                var response = ResponseMessage(result);
        //                return response;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            WriteLogs.LogError(ex);
        //            return InternalServerError();
        //        }
        //    }
        //    return Ok(data);
        //}
    }
}
