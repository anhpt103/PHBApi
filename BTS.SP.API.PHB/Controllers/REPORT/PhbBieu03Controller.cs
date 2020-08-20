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
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.SERVICE.Models.BIEU03;
using BTS.SP.PHB.SERVICE.REPORT.Bieu03;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU03")]
    [Route("{id?}")]
    [Authorize]
    public class PhbBieu03Controller : ApiController
    {
        private readonly IPhbBieu03Service _bieu03Service;
        private readonly IPhbBieu03TemplateService _bieu03TemplateService;
        private readonly IPhbBieu03DetailService _bieu03DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu03Controller(IPhbBieu03Service bieu03Service, IPhbBieu03TemplateService bieu03TemplateService,
            IPhbBieu03DetailService bieu03DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu03Service = bieu03Service;
            _bieu03TemplateService = bieu03TemplateService;
            _bieu03DetailService = bieu03DetailService;
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
                            var bieu03 = new PHB_BIEU03()
                            {
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                NGAY_TAO = DateTime.Now,
                                ObjectState = ObjectState.Added,
                                REFID = Guid.NewGuid().ToString("N"),
                            };
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            //bieu03.MA_CHUONG = httpRequest["MA_CHUONG"];
                            bieu03.MA_CHUONG = "123";
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu03.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu03.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu03.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu03.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu03.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu03Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu03.MA_CHUONG) && x.MA_QHNS.Equals(bieu03.MA_QHNS) &&
                                x.NAM_BC == bieu03.NAM_BC && x.KY_BC == bieu03.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            var startRowI = 12;
                            var endRowI = startRowI + 15;
                            for (var i = startRowI; i <= endRowI; i++)
                            {
                                var detail = new PHB_BIEU03_DETAIL
                                {
                                    PHB_BIEU03_REFID = bieu03.REFID,
                                    ObjectState = ObjectState.Added,
                                    STT_CHI_TIEU =
                                        workSheet.Cells[i, 1].Value != null
                                            ? workSheet.Cells[i, 1].Value.ToString()
                                            : null
                                };
                                var currentMct = i - startRowI;
                                if (currentMct < 10)
                                {
                                    detail.MA_CHI_TIEU = "0" + currentMct;
                                }
                                else
                                {
                                    detail.MA_CHI_TIEU = currentMct.ToString();

                                }
                                if (detail.MA_CHI_TIEU.Equals("00") || detail.MA_CHI_TIEU.Equals("01") || detail.MA_CHI_TIEU.Equals("15"))
                                {
                                    detail.LOAI = 0;
                                }
                                else
                                {
                                    detail.LOAI = 1;
                                }
                                detail.SAPXEP = currentMct;
                                detail.TEN_CHI_TIEU = workSheet.Cells[i, 2].Value.ToString();
                                if (workSheet.Cells[i, 3].Value != null)
                                {
                                    try
                                    {
                                        detail.DU_TOAN_NAM_TRUOC = double.Parse(workSheet.Cells[i, 3].Value.ToString());

                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.DU_TOAN_NAM_TRUOC = 0;
                                }
                                if (workSheet.Cells[i, 4].Value != null)
                                {
                                    try
                                    {
                                        detail.DU_TOAN_DUOC_GIAO = double.Parse(workSheet.Cells[i, 4].Value.ToString());

                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response(){Error = true,Message = ErrorMessage.ERROR_DATA});
                                    }
                                }
                                else
                                {
                                    detail.DU_TOAN_DUOC_GIAO = 0;
                                }
                                if (workSheet.Cells[i, 5].Value != null)
                                {
                                    try
                                    {
                                        detail.DU_TOAN_DUOC_SU_DUNG = double.Parse(workSheet.Cells[i, 5].Value.ToString());

                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                    }
                                }
                                else
                                {
                                    detail.DU_TOAN_DUOC_SU_DUNG = 0;
                                }
                                if (workSheet.Cells[i, 6].Value != null)
                                {
                                    try
                                    {
                                        detail.QUYET_TOAN_NAM = double.Parse(workSheet.Cells[i, 6].Value.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        WriteLogs.LogError(ex);
                                        return Ok(new Response(){Error = true,Message = ErrorMessage.ERROR_DATA});
                                    }
                                }
                                else
                                {
                                    detail.QUYET_TOAN_NAM = 0;
                                }
                                _bieu03DetailService.Insert(detail);
                            }

                            var startRow2 = endRowI + 1;
                            try
                            {
                                while (workSheet.Cells[startRow2, 1].Value != null && workSheet.Cells[startRow2, 2].Value != null)
                                {
                                    var detail = new PHB_BIEU03_DETAIL
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU03_REFID = bieu03.REFID,
                                        LOAI = 1,
                                        SAPXEP = 99,
                                        MA_CHI_TIEU = workSheet.Cells[startRow2, 1].Value.ToString()
                                    };
                                    detail.STT_CHI_TIEU = detail.MA_CHI_TIEU;
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow2, 2].Value.ToString();
                                    if (workSheet.Cells[startRow2, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.DU_TOAN_NAM_TRUOC = double.Parse(workSheet.Cells[startRow2, 3].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response()
                                            {
                                                Error = true,
                                                Message = ErrorMessage.ERROR_DATA
                                            });
                                        }
                                    }
                                    else
                                    {
                                        detail.DU_TOAN_NAM_TRUOC = 0;
                                    }
                                    if (workSheet.Cells[startRow2, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.DU_TOAN_DUOC_GIAO = double.Parse(workSheet.Cells[startRow2, 4].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response()
                                            {
                                                Error = true,
                                                Message = ErrorMessage.ERROR_DATA
                                            });
                                        }
                                    }
                                    else
                                    {
                                        detail.DU_TOAN_DUOC_GIAO = 0;
                                    }
                                    if (workSheet.Cells[startRow2, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.DU_TOAN_DUOC_SU_DUNG = double.Parse(workSheet.Cells[startRow2, 5].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response()
                                            {
                                                Error = true,
                                                Message = ErrorMessage.ERROR_DATA
                                            });
                                        }
                                    }
                                    else
                                    {
                                        detail.DU_TOAN_DUOC_SU_DUNG = 0;
                                    }
                                    if (workSheet.Cells[startRow2, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.QUYET_TOAN_NAM = double.Parse(workSheet.Cells[startRow2, 6].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response()
                                            {
                                                Error = true,
                                                Message = ErrorMessage.ERROR_DATA
                                            });
                                        }
                                    }
                                    else
                                    {
                                        detail.QUYET_TOAN_NAM = 0;
                                    }
                                    _bieu03DetailService.Insert(detail);
                                    startRow2 += 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteLogs.LogError(ex);
                                return Ok(new Response()
                                {
                                    Error = true,
                                    Message = ErrorMessage.ERROR_DATA
                                });
                            }

                            if (workSheet.Cells[startRow2 + 1, 3].Value != null)
                            {
                                bieu03.THUYETMINH_1 = workSheet.Cells[startRow2 + 1, 3].Value.ToString();
                            }
                            if (workSheet.Cells[startRow2 + 2, 3].Value != null)
                            {
                                bieu03.THUYETMINH_2 = workSheet.Cells[startRow2 + 2, 3].Value.ToString();
                            }
                            if (workSheet.Cells[startRow2 + 3, 3].Value != null)
                            {
                                bieu03.THUYETMINH_3 = workSheet.Cells[startRow2 + 3, 3].Value.ToString();
                            }
                            _bieu03Service.Insert(bieu03);

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
            var response = new Response<BIEU03Vm.DetailModel>();
            try
            {
                var item = await _bieu03Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(refid));
                if (item != null)
                {
                    var data = new BIEU03Vm.DetailModel
                    {
                        REFID = refid,
                        THUYETMINH_1 = item.THUYETMINH_1,
                        THUYETMINH_2 = item.THUYETMINH_2,
                        THUYETMINH_3 = item.THUYETMINH_3
                    };
                    var responseDetail = await _bieu03DetailService.GetDetailByRefId(refid);
                    if (responseDetail.Error) return Ok(responseDetail);
                    data.DETAIL = responseDetail.Data;
                    response.Error = false;
                    response.Data = data;
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.NOT_FOUND;
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_BIEU03_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bieu03TemplateService.Queryable().OrderBy(x => x.SAPXEP).ToListAsync();
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
            
            var response = await _bieu03Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET,rqmodel.NAM_BC, rqmodel.KY_BC,string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _bieu03Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET,rqmodel.NAM_BC,rqmodel.KY_BC,string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU03/Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU03/" + 
                    RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                        sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);

                        var listByMaQhns = data.Data.DETAIL.GroupBy(x => x.MA_QHNS);
                        var startRowInsert = 12;
                        foreach (var list in listByMaQhns)
                        {
                            if (list.Key.Equals("0"))
                            {
                                // dữ liệu tổng hợp
                                var lst = list.ToList();
                                sheet.InsertRow(startRowInsert, lst.Count);
                                for (var i = 0; i < lst.Count; i++)
                                {
                                    for (var j = 1; j <= 10; j++)
                                    {
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    if (lst[i].INDAM ==1) sheet.Row(startRowInsert + i).Style.Font.Bold = true;
                                    if (lst[i].INNGHIENG == 1) sheet.Row(startRowInsert + i).Style.Font.Italic = true;
                                    sheet.Cells[startRowInsert + i, 1].Value = lst[i].STT_CHI_TIEU;
                                    sheet.Cells[startRowInsert + i, 2].Value = lst[i].TEN_CHI_TIEU;
                                    sheet.Cells[startRowInsert + i, 3].Value = lst[i].DU_TOAN_NAM_TRUOC;
                                    sheet.Cells[startRowInsert + i, 4].Value = lst[i].DU_TOAN_DUOC_GIAO;
                                    sheet.Cells[startRowInsert + i, 5].Value = lst[i].DU_TOAN_DUOC_SU_DUNG;
                                    sheet.Cells[startRowInsert + i, 6].Value = lst[i].QUYET_TOAN_NAM;
                                    sheet.Cells[startRowInsert + i, 7].Value = lst[i].QUYET_TOAN_NAM - lst[i].DU_TOAN_DUOC_GIAO;
                                    if (lst[i].DU_TOAN_DUOC_GIAO > 0 || lst[i].DU_TOAN_DUOC_GIAO < 0)
                                    {
                                        sheet.Cells[startRowInsert + i, 8].Value = Math.Round(lst[i].QUYET_TOAN_NAM / lst[i].DU_TOAN_DUOC_GIAO,2);
                                    }
                                    sheet.Cells[startRowInsert + i, 9].Value = lst[i].QUYET_TOAN_NAM - lst[i].DU_TOAN_DUOC_SU_DUNG;
                                    if (lst[i].DU_TOAN_DUOC_SU_DUNG > 0 || lst[i].DU_TOAN_DUOC_SU_DUNG < 0)
                                    {
                                        sheet.Cells[startRowInsert + i, 8].Value = Math.Round(lst[i].QUYET_TOAN_NAM / lst[i].DU_TOAN_DUOC_SU_DUNG, 2);
                                    }
                                }
                                startRowInsert += lst.Count;
                            }
                            else
                            {
                                // dữ liệu chi tiết đơn vị
                                var lst = list.ToList();
                                sheet.InsertRow(startRowInsert,1);
                                startRowInsert+=1;
                                sheet.Cells[startRowInsert, 2].Value = list.Key + "--" + lst[0].TEN_QHNS;
                                sheet.Cells[startRowInsert, 2].Style.Font.Bold = true;
                                startRowInsert += 1;
                                sheet.InsertRow(startRowInsert, 4);
                                sheet.Cells[9,1,11,6].Copy(sheet.Cells[startRowInsert, 1, startRowInsert+2, 6]);
                                startRowInsert += 3;
                                sheet.InsertRow(startRowInsert, lst.Count);
                                for (var i = 0; i < lst.Count; i++)
                                {
                                    for (var j = 1; j <= 10; j++)
                                    {
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    if (lst[i].INDAM == 1) sheet.Row(startRowInsert + i).Style.Font.Bold = true;
                                    if (lst[i].INNGHIENG == 1) sheet.Row(startRowInsert + i).Style.Font.Italic = true;
                                    sheet.Cells[startRowInsert + i, 1].Value = lst[i].STT_CHI_TIEU;
                                    sheet.Cells[startRowInsert + i, 2].Value = lst[i].TEN_CHI_TIEU;
                                    sheet.Cells[startRowInsert + i, 3].Value = lst[i].DU_TOAN_NAM_TRUOC;
                                    sheet.Cells[startRowInsert + i, 4].Value = lst[i].DU_TOAN_DUOC_GIAO;
                                    sheet.Cells[startRowInsert + i, 5].Value = lst[i].DU_TOAN_DUOC_SU_DUNG;
                                    sheet.Cells[startRowInsert + i, 6].Value = lst[i].QUYET_TOAN_NAM;
                                    sheet.Cells[startRowInsert + i, 7].Value = lst[i].QUYET_TOAN_NAM - lst[i].DU_TOAN_DUOC_GIAO;
                                    if (lst[i].DU_TOAN_DUOC_GIAO > 0 || lst[i].DU_TOAN_DUOC_GIAO < 0)
                                    {
                                        sheet.Cells[startRowInsert + i, 8].Value = Math.Round(lst[i].QUYET_TOAN_NAM / lst[i].DU_TOAN_DUOC_GIAO, 2);
                                    }
                                    sheet.Cells[startRowInsert + i, 9].Value = lst[i].QUYET_TOAN_NAM - lst[i].DU_TOAN_DUOC_SU_DUNG;
                                    if (lst[i].DU_TOAN_DUOC_SU_DUNG > 0 || lst[i].DU_TOAN_DUOC_SU_DUNG < 0)
                                    {
                                        sheet.Cells[startRowInsert + i, 10].Value = Math.Round(lst[i].QUYET_TOAN_NAM / lst[i].DU_TOAN_DUOC_SU_DUNG, 2);
                                    }
                                }
                                startRowInsert += lst.Count;
                            }
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_BIEU03.xlsx"
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

        [HttpPost]
        public async Task<IHttpActionResult> Post(BIEU03Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS)  || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu03 = new PHB_BIEU03()
                {
                    KY_BC = model.KY_BC,
                    NAM_BC = model.NAM_BC,
                    MA_CHUONG = "123",
                    MA_QHNS = model.MA_QHNS,
                    NGAY_TAO = DateTime.Now,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    ObjectState = ObjectState.Added,
                    TRANG_THAI = 0,
                    REFID = Guid.NewGuid().ToString("N"),
                    THUYETMINH_1 = model.THUYETMINH_1,
                    THUYETMINH_2 = model.THUYETMINH_2,
                    THUYETMINH_3 = model.THUYETMINH_3,
                    TEN_QHNS = model.TEN_QHNS,
                    MA_QHNS_CHA = model.MA_QHNS_CHA
                };
                var checkReport = await _bieu03Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu03.MA_CHUONG) && x.MA_QHNS.Equals(bieu03.MA_QHNS) &&
                    x.NAM_BC == bieu03.NAM_BC && x.KY_BC == bieu03.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                _bieu03Service.Insert(bieu03);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU03_REFID = bieu03.REFID;
                    if (detail.MA_CHI_TIEU.Length > 2) detail.STT_CHI_TIEU = detail.MA_CHI_TIEU;
                    detail.ObjectState = ObjectState.Added;
                    _bieu03DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU03Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var bieu03 = await _bieu03Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu03 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                if (!string.IsNullOrEmpty(model.THUYETMINH_1) || !string.IsNullOrEmpty(model.THUYETMINH_2) || !string.IsNullOrEmpty(model.THUYETMINH_3))
                {
                    hasValue = true;
                    bieu03.THUYETMINH_1 = model.THUYETMINH_1;
                    bieu03.THUYETMINH_2 = model.THUYETMINH_2;
                    bieu03.THUYETMINH_3 = model.THUYETMINH_3;
                }

                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU03_DETAIL item = await _bieu03DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu03DetailService.Delete(item);
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
                        item.PHB_BIEU03_REFID = model.REFID;
                        item.STT_CHI_TIEU = item.MA_CHI_TIEU;
                        _bieu03DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU03_DETAIL detail = await _bieu03DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.DU_TOAN_NAM_TRUOC = item.DU_TOAN_NAM_TRUOC;
                            detail.DU_TOAN_DUOC_GIAO = item.DU_TOAN_DUOC_GIAO;
                            detail.DU_TOAN_DUOC_SU_DUNG = item.DU_TOAN_DUOC_SU_DUNG;
                            detail.QUYET_TOAN_NAM = item.QUYET_TOAN_NAM;
                            _bieu03DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu03.NGAY_SUA = DateTime.Now;
                    bieu03.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu03Service.Update(bieu03);

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
    }
}
