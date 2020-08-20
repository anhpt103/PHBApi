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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.PL3_1;
using BTS.SP.PHB.SERVICE.Models.PL31;
using BTS.SP.PHB.SERVICE.REPORT.PL31;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPL31")]
    [Route("{id?}")]
    public class PhbPL31Controller : ApiController
    {
        private readonly IPhbPL31Service _PL31Service;
        private readonly IPhbPL31DetailService _PL31DetailService;
        private readonly IPhbPL31TemplateService _PL31TemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbPL31Controller(IPhbPL31Service PL31Service, IPhbPL31DetailService PL31DetailService, IPhbPL31TemplateService PL31TemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _PL31Service = PL31Service;
            _PL31DetailService = PL31DetailService;
            _PL31TemplateService = PL31TemplateService;
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
                            var PL31 = new PHB_PL31()
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
                            PL31.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            PL31.MA_QHNS = httpRequest["MA_QHNS"];
                            PL31.TEN_QHNS = httpRequest["TEN_QHNS"];
                            PL31.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            PL31.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            PL31.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _PL31Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(PL31.MA_CHUONG) && x.MA_QHNS.Equals(PL31.MA_QHNS) &&
                                x.NAM_BC == PL31.NAM_BC && x.KY_BC == PL31.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _PL31Service.Insert(PL31);

                            var machitieu = string.Empty;
                            var startRow = 11;
                            var count = 0;
                            while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()))
                            {
                                if (workSheet.Cells[startRow, 1].Value != null &&
                                    !string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Value.ToString()))
                                {
                                    //chi tieu danh mục
                                    count = 0;
                                    machitieu = workSheet.Cells[startRow, 1].Value.ToString();
                                    var detail = new PHB_PL31_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_PL31_REFID = PL31.REFID
                                    };
                                    switch (machitieu)
                                    {
                                        case "01":
                                        case "06":
                                        case "07":
                                            detail.LOAI = 1;
                                            break;
                                        case "02":
                                        case "03":
                                        case "05":
                                        case "08":
                                            detail.LOAI = 2;
                                            break;
                                        case "04":
                                        case "09":
                                            detail.LOAI = 0;
                                            break;
                                        default:
                                            detail.LOAI = 1;
                                            break;
                                    }
                                    detail.STT_CHI_TIEU = machitieu;
                                    detail.MA_CHI_TIEU = machitieu;
                                    detail.MA_CHI_TIEU_HIEN_THI = machitieu;
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_SOBC = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_SOBC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_SODCKT = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_SODCKT = 0;
                                    }

                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TH_SOBC = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TH_SOBC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.TH_SODCKT = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TH_SODCKT = 0;
                                    }
                                    _PL31DetailService.Insert(detail);
                                    startRow += 1;
                                }
                                else
                                {
                                    //chỉ tiêu con
                                    var detail = new PHB_PL31_DETAIL
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_PL31_REFID = PL31.REFID,
                                        LOAI = 3,
                                        MA_CHI_TIEU = machitieu + "" + (count++)
                                    };
                                    detail.MA_CHI_TIEU_HIEN_THI = detail.MA_CHI_TIEU;
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_SOBC = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_SOBC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_SODCKT = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_SODCKT = 0;
                                    }

                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TH_SOBC = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TH_SOBC = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.TH_SODCKT = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TH_SODCKT = 0;
                                    }
                                    _PL31DetailService.Insert(detail);
                                    startRow += 1;
                                }
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_PL31_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _PL31TemplateService.Queryable().OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetDetailByRefId/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        {
            if (string.IsNullOrEmpty(refid)) return BadRequest();
            var response = new Response<PL31Vm.ViewModel>();
            try
            {
                var data = new PL31Vm.ViewModel
                {
                    REFID = refid,
                    DETAIL = await _PL31DetailService.Queryable().Where(x => x.PHB_PL31_REFID.Equals(refid))
                        .OrderBy(x => x.MA_CHI_TIEU).ToListAsync()
                };
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
        public async Task<IHttpActionResult> Post(PL31Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var PL31 = new PHB_PL31()
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

                var checkReport = await _PL31Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(PL31.MA_CHUONG) && x.MA_QHNS.Equals(PL31.MA_QHNS) &&
                    x.NAM_BC == PL31.NAM_BC && x.KY_BC == PL31.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _PL31Service.Insert(PL31);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_PL31_REFID = PL31.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _PL31DetailService.Insert(detail);
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
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(PL31Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var PL31 =
                    await _PL31Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (PL31 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        var item = await _PL31DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _PL31DetailService.Delete(item);
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
                        item.LOAI = 3;
                        item.PHB_PL31_REFID = model.REFID;
                        _PL31DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _PL31DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;
                            detail.DT_SOBC = item.DT_SOBC;
                            detail.DT_SODCKT = item.DT_SODCKT;
                            detail.TH_SOBC = item.TH_SOBC;
                            detail.TH_SODCKT = item.TH_SODCKT;
                            _PL31DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    PL31.NGAY_SUA = DateTime.Now;
                    PL31.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _PL31Service.Update(PL31);

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
            return Ok(response);
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _PL31Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
           
            var data = await _PL31Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC,
                rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/PL31/PL31_Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/PL31/" +
                    RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                        sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
                        var startInsertRow = 11;
                        var titleRow = startInsertRow;
                        sheet.InsertRow(startInsertRow, data.Data.DETAIL.Count);

                        var tempGr = from c in data.Data.DETAIL
                                   group c by new
                                   {
                                       c.MA_QHNS,
                                       c.TEN_QHNS
                                   } into gcs
                                   select new PL31Vm.GroupExcel()
                                   {
                                       MA_QHNS = gcs.Key.MA_QHNS,
                                       TEN_QHNS = gcs.Key.TEN_QHNS,
                                       lstData = gcs.ToList()
                                   };
                        var dataExcel = tempGr.ToList();
                        
                        for ( var i = 0; i< dataExcel.Count;i++)
                        {
                            var cur_Row = titleRow + 1;
                            sheet.Cells[titleRow , 1].Value = dataExcel[i].MA_QHNS + " - " + dataExcel[i].TEN_QHNS;
                            sheet.Cells[titleRow , 1, titleRow , 8].Merge = true;
                            sheet.Cells[titleRow , 1, titleRow , 8].Style.Font.Bold = true;
                            for ( var j = 0; j < dataExcel[i].lstData.Count; j++)
                            {
                                for (var k = 1; k <= 8; k++)
                                {
                                    sheet.Cells[cur_Row + j, k].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[cur_Row + j, k].Style.Border.Right.Color.SetColor(Color.Black);
                                }
                                sheet.Cells[cur_Row + j, 1].Value = data.Data.DETAIL[j].STT_CHI_TIEU;
                                sheet.Cells[cur_Row + j, 2].Value = data.Data.DETAIL[j].TEN_CHI_TIEU;
                                sheet.Cells[cur_Row + j, 3].Value = data.Data.DETAIL[j].DT_SOBC;
                                sheet.Cells[cur_Row + j, 4].Value = data.Data.DETAIL[j].DT_SODCKT;
                                sheet.Cells[cur_Row + j, 5].Value = data.Data.DETAIL[j].DT_SODCKT - data.Data.DETAIL[j].DT_SOBC;
                                sheet.Cells[cur_Row + j, 6].Value = data.Data.DETAIL[j].TH_SOBC;
                                sheet.Cells[cur_Row + j, 7].Value = data.Data.DETAIL[j].TH_SODCKT;
                                sheet.Cells[cur_Row + j, 8].Value = data.Data.DETAIL[j].TH_SODCKT - data.Data.DETAIL[j].TH_SOBC;
                            }
                            titleRow += dataExcel[i].lstData.Count + 2;
                        }
                        //for (var i = 0; i < data.Data.DETAIL.Count; i++)
                        //{
                        //    for (var j = 1; j <= 8; j++)
                        //    {
                        //        sheet.Cells[startInsertRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        //        sheet.Cells[startInsertRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                        //    }
                        //    sheet.Cells[startInsertRow + i, 1].Value = data.Data.DETAIL[i].STT_CHI_TIEU;
                        //    sheet.Cells[startInsertRow + i, 2].Value = data.Data.DETAIL[i].TEN_CHI_TIEU;
                        //    sheet.Cells[startInsertRow + i, 3].Value = data.Data.DETAIL[i].DT_SOBC;
                        //    sheet.Cells[startInsertRow + i, 4].Value = data.Data.DETAIL[i].DT_SODCKT;
                        //    sheet.Cells[startInsertRow + i, 5].Value = data.Data.DETAIL[i].DT_SODCKT - data.Data.DETAIL[i].DT_SOBC;
                        //    sheet.Cells[startInsertRow + i, 6].Value = data.Data.DETAIL[i].TH_SOBC;
                        //    sheet.Cells[startInsertRow + i, 7].Value = data.Data.DETAIL[i].TH_SODCKT;
                        //    sheet.Cells[startInsertRow + i, 8].Value = data.Data.DETAIL[i].TH_SODCKT - data.Data.DETAIL[i].TH_SOBC;
                        //}
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_PL31.xlsx"
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

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var response = await _PL31Service.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName);
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
                            PHB_PL31_DETAIL detail = await _PL31DetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TEN_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                                detail.TEN_CHI_TIEU = rqmodel.newName;
                                _PL31DetailService.Update(detail);
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
            var response = await _PL31Service.SumReport_HTML(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
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
                var foundLst = await _PL31DetailService.Queryable()
                    .Where(x => x.TEN_CHI_TIEU == rqmodel.TEN_CHI_TIEU && x.TEN_CHI_TIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_PL31_DETAIL detail = await _PL31DetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.TEN_CHI_TIEU = entry.TEN_CHI_TIEU_OLD;
                        detail.TEN_CHI_TIEU_OLD = null;
                        _PL31DetailService.Update(detail);
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
