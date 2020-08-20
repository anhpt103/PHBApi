using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using BTS.SP.PHB.SERVICE.Models.B03BCQT_BII1;
using BTS.SP.PHB.SERVICE.REPORT.B03BCQT_BII1;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB03BCQT_BII1")]
    [Route("{id?}")]
    public class PhbB03BCQT_BII1Controller : ApiController
    {
        private readonly IPhbB03BCQT_BII1Service _B03BCQT_BII1Service;
        private readonly IPhbB03BCQT_BII1DetailService _B03BCQT_BII1DetailService;
        private readonly IPhbB03BCQT_BII1TemplateService _B03BCQT_BII1TemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB03BCQT_BII1Controller(IPhbB03BCQT_BII1Service B03BCQT_BII1Service, IPhbB03BCQT_BII1DetailService B03BCQT_BII1DetailService, IPhbB03BCQT_BII1TemplateService B03BCQT_BII1TemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _B03BCQT_BII1Service = B03BCQT_BII1Service;
            _B03BCQT_BII1DetailService = B03BCQT_BII1DetailService;
            _B03BCQT_BII1TemplateService = B03BCQT_BII1TemplateService;
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
                        var B03BCQT_BII1 = new PHB_B03BCQT_BII1()
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
                            B03BCQT_BII1.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            B03BCQT_BII1.MA_QHNS = httpRequest["MA_QHNS"];
                            B03BCQT_BII1.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            B03BCQT_BII1.TEN_QHNS = httpRequest["TEN_QHNS"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            B03BCQT_BII1.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            B03BCQT_BII1.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _B03BCQT_BII1Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(B03BCQT_BII1.MA_CHUONG) && x.MA_QHNS.Equals(B03BCQT_BII1.MA_QHNS) &&
                                x.NAM_BC == B03BCQT_BII1.NAM_BC && x.KY_BC == B03BCQT_BII1.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _B03BCQT_BII1Service.Insert(B03BCQT_BII1);

                            #region insert PHÍ
                            int startRowPhi = 13;
                            bool isLoop = true;
                            bool isTongThu, isTienNSNN, isTienKhauTru;
                            double tongThu, tienNSNN, tienKhauTru;
                            while (workSheet.Cells[startRowPhi, 3].Value != null && isLoop)
                            {
                                if (workSheet.Cells[startRowPhi, 1].Value != null &&
                                    workSheet.Cells[startRowPhi, 1].Value.ToString().Equals("II"))
                                {
                                    isLoop = false;
                                }
                                else
                                {
                                    if (workSheet.Cells[startRowPhi, 1].Value != null &&
                                        workSheet.Cells[startRowPhi, 1].Value.ToString().Equals("I"))
                                    {
                                        startRowPhi += 1;
                                    }
                                    else
                                    {
                                        PHB_B03BCQT_BII1_DETAIL detail = new PHB_B03BCQT_BII1_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_B03BCQT_BII1_REFID = B03BCQT_BII1.REFID,
                                            LOAI = 1,
                                        };
                                        detail.STT_CHI_TIEU = workSheet.Cells[startRowPhi, 1].Value != null ? workSheet.Cells[startRowPhi, 1].Value.ToString() : null;
                                        detail.MA_NOIDUNGKT = workSheet.Cells[startRowPhi, 2].Value != null ? workSheet.Cells[startRowPhi, 2].Value.ToString() : null;
                                        detail.TEN_NOIDUNGKT = workSheet.Cells[startRowPhi, 3].Value != null ? workSheet.Cells[startRowPhi, 3].Value.ToString() : null;

                                        isTongThu = double.TryParse(workSheet.Cells[startRowPhi, 4].Value == null ? "0" : workSheet.Cells[startRowPhi, 4].Value.ToString(), out tongThu);
                                        detail.TONG_THU = isTongThu ? tongThu : 0;
                                        isTienNSNN = double.TryParse(workSheet.Cells[startRowPhi, 5].Value == null ? "0" : workSheet.Cells[startRowPhi, 5].Value.ToString(), out tienNSNN);
                                        detail.TIEN_NSNN = isTienNSNN ? tienNSNN : 0;
                                        isTienKhauTru = double.TryParse(workSheet.Cells[startRowPhi, 6].Value == null ? "0" : workSheet.Cells[startRowPhi, 6].Value.ToString(), out tienKhauTru);
                                        detail.TIEN_KHAUTRU = isTienKhauTru ? tienKhauTru : 0;
                                        detail.GHI_CHU = workSheet.Cells[startRowPhi, 7].Value != null ? workSheet.Cells[startRowPhi, 7].Value.ToString() : null;

                                        _B03BCQT_BII1DetailService.Insert(detail);
                                        startRowPhi += 1;
                                    }
                                }
                            }
                            #endregion

                            #region insert LỆ PHÍ

                            int startRowLePhi = startRowPhi;
                            if (workSheet.Cells[startRowLePhi, 1].Value != null &&
                                workSheet.Cells[startRowLePhi, 1].Value.ToString().Equals("II")) startRowLePhi += 1;
                            while (workSheet.Cells[startRowLePhi, 3].Value != null)
                            {
                                PHB_B03BCQT_BII1_DETAIL detail = new PHB_B03BCQT_BII1_DETAIL()
                                {
                                    ObjectState = ObjectState.Added,
                                    PHB_B03BCQT_BII1_REFID = B03BCQT_BII1.REFID,
                                    LOAI = 2,
                                };
                                detail.STT_CHI_TIEU = workSheet.Cells[startRowLePhi, 1].Value != null ? workSheet.Cells[startRowLePhi, 1].Value.ToString() : null;
                                detail.MA_NOIDUNGKT = workSheet.Cells[startRowLePhi, 2].Value != null ? workSheet.Cells[startRowLePhi, 2].Value.ToString() : null;
                                detail.TEN_NOIDUNGKT = workSheet.Cells[startRowLePhi, 3].Value != null ? workSheet.Cells[startRowLePhi, 3].Value.ToString() : null;

                                isTongThu = double.TryParse(workSheet.Cells[startRowLePhi, 4].Value == null ? "0" : workSheet.Cells[startRowLePhi, 4].Value.ToString(), out tongThu);
                                detail.TONG_THU = isTongThu ? tongThu : 0;
                                isTienNSNN = double.TryParse(workSheet.Cells[startRowLePhi, 5].Value == null ? "0" : workSheet.Cells[startRowLePhi, 5].Value.ToString(), out tienNSNN);
                                detail.TIEN_NSNN = isTienNSNN ? tienNSNN : 0;
                                isTienKhauTru = double.TryParse(workSheet.Cells[startRowLePhi, 6].Value == null ? "0" : workSheet.Cells[startRowLePhi, 6].Value.ToString(), out tienKhauTru);
                                detail.TIEN_KHAUTRU = isTienKhauTru ? tienKhauTru : 0;
                                detail.GHI_CHU = workSheet.Cells[startRowLePhi, 7].Value != null ? workSheet.Cells[startRowLePhi, 7].Value.ToString() : null;

                                _B03BCQT_BII1DetailService.Insert(detail);
                                startRowLePhi += 1;
                            }

                            #endregion

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
            Response<B03BCQT_BII1Vm.ViewModel> response = new Response<B03BCQT_BII1Vm.ViewModel>();
            try
            {
                B03BCQT_BII1Vm.ViewModel data = new B03BCQT_BII1Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL_PHI = await _B03BCQT_BII1DetailService.Queryable().Where(x => x.PHB_B03BCQT_BII1_REFID.Equals(refid))
                    .OrderBy(x => x.MA_NOIDUNGKT).ToListAsync();
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
            Response<List<PHB_B03BCQT_BII1_TEMPLATE>> response = new Response<List<PHB_B03BCQT_BII1_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _B03BCQT_BII1TemplateService.Queryable()
                    .OrderBy(x => x.MA_NOIDUNGKT)
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

        [Route("GetTemplateForEdit/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplateForEdit(string refid)
        {
            var response = await _B03BCQT_BII1TemplateService.GetTemplateForEdit(refid);
            return Ok(response);
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            
            var response = await _B03BCQT_BII1Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            //rqmodel.MA_CHUONG = "421";
            var data = await _B03BCQT_BII1Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && (data.Data.DETAIL_PHI.Count > 0 || data.Data.DETAIL_LEPHI.Count > 0))
            {
                string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/B03BCQT_BII1/Template.xlsx");
                string exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/B03BCQT_BII1/" + RequestContext.Principal.Identity.Name
                                                                                     + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A1"].Value = sheet.Cells["A1"].Value + " " + rqmodel.MA_CHUONG;
                        sheet.Cells["A2"].Value = sheet.Cells["A2"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
                        sheet.Cells["A3"].Value = sheet.Cells["A3"].Value + " " + rqmodel.DSDVQHNS;
                        sheet.Cells["A6"].Value = sheet.Cells["A6"].Value + " " + rqmodel.NAM_BC;
                        if (data.Data.DETAIL_PHI.Count > 0)
                        {
                            sheet.InsertRow(14, data.Data.DETAIL_PHI.Count);
                            for (int i = 0; i < data.Data.DETAIL_PHI.Count; i++)
                            {
                                for (int j = 1; j <= 7; j++)
                                {
                                    sheet.Cells[11 + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[11 + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                }

                                sheet.Cells[11 + i, 1].Value = data.Data.DETAIL_PHI[i].STT_CHI_TIEU;
                                sheet.Cells[11 + i, 2].Value = data.Data.DETAIL_PHI[i].MA_NOIDUNGKT;
                                sheet.Cells[11 + i, 3].Value = data.Data.DETAIL_PHI[i].LOAI == 0 ? data.Data.DETAIL_PHI[i].TEN_NOIDUNGKT : "";
                                sheet.Cells[11 + i, 4].Value = data.Data.DETAIL_PHI[i].TONG_THU;
                                sheet.Cells[11 + i, 5].Value = data.Data.DETAIL_PHI[i].TIEN_NSNN;
                                sheet.Cells[11 + i, 6].Value = data.Data.DETAIL_PHI[i].TIEN_KHAUTRU;
                                sheet.Cells[11 + i, 7].Value = data.Data.DETAIL_PHI[i].GHI_CHU;
                            }
                        }

                        if (data.Data.DETAIL_LEPHI.Count > 0)
                        {
                            int startRow = 15 + data.Data.DETAIL_PHI.Count;
                            sheet.InsertRow(startRow, data.Data.DETAIL_LEPHI.Count);
                            for (int i = 0; i < data.Data.DETAIL_LEPHI.Count; i++)
                            {
                                for (int j = 1; j <= 7; j++)
                                {
                                    sheet.Cells[startRow + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    sheet.Cells[startRow + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                }

                                sheet.Cells[11 + i, 1].Value = data.Data.DETAIL_LEPHI[i].STT_CHI_TIEU;
                                sheet.Cells[11 + i, 2].Value = data.Data.DETAIL_LEPHI[i].MA_NOIDUNGKT;
                                sheet.Cells[11 + i, 3].Value = data.Data.DETAIL_LEPHI[i].LOAI == 0 ? data.Data.DETAIL_LEPHI[i].TEN_NOIDUNGKT : "";
                                sheet.Cells[11 + i, 4].Value = data.Data.DETAIL_LEPHI[i].TONG_THU;
                                sheet.Cells[11 + i, 5].Value = data.Data.DETAIL_LEPHI[i].TIEN_NSNN;
                                sheet.Cells[11 + i, 6].Value = data.Data.DETAIL_LEPHI[i].TIEN_KHAUTRU;
                                sheet.Cells[11 + i, 7].Value = data.Data.DETAIL_LEPHI[i].GHI_CHU;
                            }
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_B03BCQT_BII1.xlsx"
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
        public async Task<IHttpActionResult> Post(B03BCQT_BII1Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_B03BCQT_BII1 B03BCQT_BII1 = new PHB_B03BCQT_BII1()
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
                var checkReport = await _B03BCQT_BII1Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(B03BCQT_BII1.MA_CHUONG) && x.MA_QHNS.Equals(B03BCQT_BII1.MA_QHNS) &&
                    x.NAM_BC == B03BCQT_BII1.NAM_BC && x.KY_BC == B03BCQT_BII1.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _B03BCQT_BII1Service.Insert(B03BCQT_BII1);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_B03BCQT_BII1_REFID = B03BCQT_BII1.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _B03BCQT_BII1DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(B03BCQT_BII1Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_B03BCQT_BII1 B03BCQT_BII1 = await _B03BCQT_BII1Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (B03BCQT_BII1 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_B03BCQT_BII1_DETAIL item = await _B03BCQT_BII1DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _B03BCQT_BII1DetailService.Delete(item);
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
                        item.PHB_B03BCQT_BII1_REFID = model.REFID;
                        _B03BCQT_BII1DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_B03BCQT_BII1_DETAIL detail = await _B03BCQT_BII1DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.TONG_THU = item.TONG_THU;
                            detail.TIEN_NSNN = item.TIEN_NSNN;
                            detail.TIEN_KHAUTRU = item.TIEN_KHAUTRU;
                            detail.GHI_CHU = item.GHI_CHU;
                            _B03BCQT_BII1DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    B03BCQT_BII1.NGAY_SUA = DateTime.Now;
                    B03BCQT_BII1.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _B03BCQT_BII1Service.Update(B03BCQT_BII1);

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
