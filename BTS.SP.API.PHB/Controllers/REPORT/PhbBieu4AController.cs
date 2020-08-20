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
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.SERVICE.Models.BIEU4A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4A;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3A;
using BTS.SP.PHB.ENTITY.Rp.BIEU3A;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu4a")]
    [Route("{id?}")]
    public class PhbBieu4AController : ApiController
    {
        private readonly IPhbBieu4AService _bieu4AService;
        private readonly IPhbBieu4ADetailService _bieu4ADetailService;
        private readonly IPhbBieu4ATemplateService _bieu4ATemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IPhbBieu3AService _bieu3AService;
        private readonly IPhbBieu3ADetailService _bieu3ADetailService;
        public PhbBieu4AController(IPhbBieu4AService bieu4AService, IPhbBieu4ADetailService bieu4ADetailService, IPhbBieu4ATemplateService bieu4ATemplateService, IUnitOfWorkAsync unitOfWorkAsync, IPhbBieu3AService bieu3AService, IPhbBieu3ADetailService bieu3ADetailService)
        {
            _bieu4AService = bieu4AService;
            _bieu4ADetailService = bieu4ADetailService;
            _bieu4ATemplateService = bieu4ATemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _bieu3AService = bieu3AService;
            _bieu3ADetailService = bieu3ADetailService;
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
                            var bieu4A = new PHB_BIEU4A()
                            {
                                NGAY_TAO = DateTime.Now,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                ObjectState = ObjectState.Added,
                                TRANG_THAI = 0,
                                REFID = Guid.NewGuid().ToString("N")
                            };
                            if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            bieu4A.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu4A.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu4A.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu4A.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu4A.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu4A.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu4AService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu4A.MA_CHUONG) && x.MA_QHNS.Equals(bieu4A.MA_QHNS) &&
                                x.NAM_BC == bieu4A.NAM_BC && x.KY_BC == bieu4A.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu4AService.Insert(bieu4A);

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
                                    var detail = new PHB_BIEU4A_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU4A_REFID = bieu4A.REFID
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
                                    }
                                    detail.STT_CHI_TIEU = machitieu;
                                    detail.MA_CHI_TIEU = machitieu;
                                    detail.MA_CHI_TIEU_HIEN_THI = machitieu;
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.SO_DUTOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.SO_DUTOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.SO_THUCHIEN = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.SO_THUCHIEN = 0;
                                    }
                                    _bieu4ADetailService.Insert(detail);
                                    startRow += 1;
                                }
                                else
                                {
                                    //chỉ tiêu con
                                    var detail = new PHB_BIEU4A_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU4A_REFID = bieu4A.REFID
                                    };
                                    detail.LOAI = 3;
                                    detail.MA_CHI_TIEU = machitieu + "" + (count++);
                                    detail.MA_CHI_TIEU_HIEN_THI = detail.MA_CHI_TIEU;
                                    detail.TEN_CHI_TIEU = workSheet.Cells[startRow, 2].Value.ToString();
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.SO_DUTOAN = double.Parse(workSheet.Cells[startRow, 3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.SO_DUTOAN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.SO_THUCHIEN = double.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.SO_THUCHIEN = 0;
                                    }
                                    _bieu4ADetailService.Insert(detail);
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
            var response = new Response<List<PHB_BIEU4A_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bieu4ATemplateService.Queryable().OrderBy(x => x.MA_CHI_TIEU).ToListAsync();
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
            var response = new Response<BIEU4AVm.ViewModel>();
            try
            {
                var data = new BIEU4AVm.ViewModel
                {
                    REFID = refid,
                    DETAIL = await _bieu4ADetailService.Queryable().Where(x => x.PHB_BIEU4A_REFID.Equals(refid))
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
        public async Task<IHttpActionResult> Post(BIEU4AVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu4A = new PHB_BIEU4A()
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
                var checkReport = await _bieu4AService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu4A.MA_CHUONG) && x.MA_QHNS.Equals(bieu4A.MA_QHNS) &&
                    x.NAM_BC == bieu4A.NAM_BC && x.KY_BC == bieu4A.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu4AService.Insert(bieu4A);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU4A_REFID = bieu4A.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu4ADetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU4AVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            bool hasValue = false;
            try
            {
                var bieu4A =
                    await _bieu4AService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu4A == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU4A_DETAIL item = await _bieu4ADetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu4ADetailService.Delete(item);
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
                        item.PHB_BIEU4A_REFID = model.REFID;
                        _bieu4ADetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU4A_DETAIL detail = await _bieu4ADetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TEN_CHI_TIEU = item.TEN_CHI_TIEU;
                            detail.SO_DUTOAN = item.SO_DUTOAN;
                            detail.SO_THUCHIEN = item.SO_THUCHIEN;
                            _bieu4ADetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu4A.NGAY_SUA = DateTime.Now;
                    bieu4A.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu4AService.Update(bieu4A);
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

        //[Route("SumReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        //{
        //    var response = await _bieu4AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    return Ok(response);
        //}
        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(SumPara para)
        {
            var response = new Response<List<PHB_BIEU3A_DETAIL>>();
            List<string> dsRefid = new List<string>();
            List<PHB_BIEU3A_DETAIL> detail = new List<PHB_BIEU3A_DETAIL>();
            List<PHB_BIEU3A_DETAIL> dsDetail = new List<PHB_BIEU3A_DETAIL>();

            int NAM_BC = int.Parse(para.NAM_BC);
            var lstDSDVQHNS = para.DSDVQHNS.Split(',');

            if(para == null || para.DSDVQHNS == null || para.NAM_BC == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }

            //Lấy danh sách báo cáo
            try
            {
                dsRefid = _bieu3AService.Queryable().Where(x => para.DSDVQHNS.Contains(x.MA_QHNS) && x.NAM_BC == NAM_BC).Select(x => x.REFID).ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            };

            for (int i = 1; i <= dsRefid.Count; i++)
            {
                var REFID = dsRefid[i - 1];
                try
                {
                    detail = _bieu3ADetailService.Queryable().Where(x => x.PHB_BIEU3A_REFID == REFID).OrderBy(x => x.ID).ToList();
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                    return Ok(response);
                };
                dsDetail.AddRange(detail);
            }

            List<PHB_BIEU3A_DETAIL> sumDetail = new List<PHB_BIEU3A_DETAIL>();

            foreach (var a in dsDetail.OrderBy(x => x.ID))
            {
                var y = sumDetail.FirstOrDefault(x => x.TEN_CHI_TIEU == a.TEN_CHI_TIEU && x.PHB_BIEU3A_REFID != a.PHB_BIEU3A_REFID);

                if (y != null)
                {
                    y.DT_SODCKT += a.DT_SODCKT;
                    y.TH_SODCKT += a.TH_SODCKT;
                }
                else
                {
                    sumDetail.Add(a);
                }

            }

            response.Data = sumDetail;
            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);

        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _bieu4AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC,
                rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU4A/BIEU4A_Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU4A/" + RequestContext.Principal.Identity.Name
                    + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                        sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);

                        var listByMaQhns = data.Data.DETAIL.GroupBy(x => x.MA_QHNS);
                        var startRow = 11;
                        foreach (var list in listByMaQhns)
                        {
                            var listData = list.ToList();
                            if (list.Key.Equals("0"))
                            {
                                // dữ liệu tổng hợp
                                sheet.InsertRow(startRow, listData.Count);
                                for (var i = 0; i < listData.Count; i++)
                                {
                                    for (var j = 1; j <= 5; j++)
                                    {
                                        sheet.Cells[startRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    sheet.Cells[startRow + i, 1].Value = listData[i].STT_CHI_TIEU;
                                    sheet.Cells[startRow + i, 2].Value = listData[i].TEN_CHI_TIEU;
                                    sheet.Cells[startRow + i, 3].Value = listData[i].SO_DUTOAN;
                                    sheet.Cells[startRow + i, 4].Value = listData[i].SO_THUCHIEN;

                                    if (Math.Abs(listData[i].SO_DUTOAN) < double.Epsilon)
                                    {
                                        sheet.Cells[startRow + i, 5].Value = "";
                                    }
                                    else
                                    {
                                        var tile = Math.Round(listData[i].SO_THUCHIEN / listData[i].SO_DUTOAN, 2);
                                        sheet.Cells[startRow + i, 5].Value = tile;
                                    }
                                }
                                startRow += listData.Count;
                            }
                            else
                            {
                                sheet.InsertRow(startRow + 1, 2);
                                sheet.Cells[startRow + 2, 2].Value = list.Key +"--"+ listData[0].TEN_QHNS;
                                startRow += 3;
                                sheet.InsertRow(startRow,2);
                                sheet.Cells[9,1,10,5].Copy(sheet.Cells[startRow,1,startRow+1,5]);
                                startRow += 2;
                                sheet.InsertRow(startRow, listData.Count);
                                for (var i = 0; i < listData.Count; i++)
                                {
                                    for (var j = 1; j <= 5; j++)
                                    {
                                        sheet.Cells[startRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    sheet.Cells[startRow + i, 1].Value = listData[i].STT_CHI_TIEU;
                                    sheet.Cells[startRow + i, 2].Value = listData[i].TEN_CHI_TIEU;
                                    sheet.Cells[startRow + i, 3].Value = listData[i].SO_DUTOAN;
                                    sheet.Cells[startRow + i, 4].Value = listData[i].SO_THUCHIEN;

                                    if (Math.Abs(listData[i].SO_DUTOAN) < double.Epsilon)
                                    {
                                        sheet.Cells[startRow + i, 5].Value = "";
                                    }
                                    else
                                    {
                                        var tile = Math.Round(listData[i].SO_THUCHIEN / listData[i].SO_DUTOAN, 2);
                                        sheet.Cells[startRow + i, 5].Value = tile;
                                    }
                                }
                                for (var j = 1; j <= 5; j++)
                                {
                                    sheet.Cells[startRow + listData.Count - 1 , j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[startRow + listData.Count - 1, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                }
                                startRow += listData.Count;
                            }
                        }
                        
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_BIEU4A.xlsx"
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

        [Route("Sumreport_HTML")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport_HTML(ReportRqModel rqmodel)
        {
            var response = await _bieu4AService.SumReport_HTML(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("MergeReport")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReport(ReportRqModel rqmodel)
        {
            var response = await _bieu4AService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName);
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
                            PHB_BIEU4A_DETAIL detail = await _bieu4ADetailService.FindByIdAsync(entry.ID);
                            if (detail != null)
                            {
                                detail.ObjectState = ObjectState.Modified;
                                detail.TEN_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                                detail.TEN_CHI_TIEU = rqmodel.newName;
                                _bieu4ADetailService.Update(detail);
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

        [Route("MergeReportcomeback")]
        [HttpPost]
        public async Task<IHttpActionResult> MergeReportcomeback(ReportRqModelBack rqmodel)
        {
            //var response = await _bieu01BService.MergeReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()), rqmodel.changeList, rqmodel.newName, rqmodel.PHAN, rqmodel.CAP);
            Response<string> msg = new Response<string>();

            try
            {
                var foundLst = await _bieu4ADetailService.Queryable()
                    .Where(x => x.TEN_CHI_TIEU == rqmodel.TEN_CHI_TIEU && x.TEN_CHI_TIEU_OLD != null).ToListAsync();

                foreach (var entry in foundLst)
                {
                    PHB_BIEU4A_DETAIL detail = await _bieu4ADetailService.FindByIdAsync(entry.ID);
                    if (detail != null)
                    {
                        detail.ObjectState = ObjectState.Modified;
                        //detail.MA_CHI_TIEU_OLD = entry.TEN_CHI_TIEU;
                        // detail.TEN_CHI_TIEU = rqmodel.newName;
                        detail.TEN_CHI_TIEU = entry.TEN_CHI_TIEU_OLD;
                        detail.TEN_CHI_TIEU_OLD = null;
                        _bieu4ADetailService.Update(detail);
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
