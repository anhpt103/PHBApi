using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.PHB.PL42_P1_TT01;
using BTS.SP.PHB.SERVICE.Models.PL42_P1_TT01;
using BTS.SP.PHB.SERVICE.REPORT.PL42_P1_TT01;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.IO;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Net.Http.Headers;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPL42_P1_TT01")]
    [Route("{id?}")]
    public class PhbPL42_P1_TT01Controller : ApiController
    {
        private readonly IPhbPL42_P1_TT01Service _PL42_P1_TT01Service;
        private readonly IPhbPL42_P1_TT01DetailService _PL42_P1_TT01DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbPL42_P1_TT01Controller(IPhbPL42_P1_TT01Service PL42_P1_TT01Service, IPhbPL42_P1_TT01DetailService PL42_P1_TT01DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _PL42_P1_TT01Service = PL42_P1_TT01Service;
            _PL42_P1_TT01DetailService = PL42_P1_TT01DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var response = new Response();
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
                            var bieu = new PHB_PL42_P1_TT01()
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
                            bieu.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
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

                            var checkReport = await _PL42_P1_TT01Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu.MA_CHUONG) && x.MA_QHNS.Equals(bieu.MA_QHNS) &&
                                x.NAM_BC == bieu.NAM_BC && x.KY_BC == bieu.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _PL42_P1_TT01Service.Insert(bieu);

                            var tmp_col = workSheet.Dimension.Columns;
                            var stop_col = 5;
                            for (int i = 6; i < tmp_col; i++)
                            {
                                if (!string.IsNullOrEmpty(workSheet.Cells[7, i].Text))
                                {
                                    stop_col++;
                                }
                            }

                            for (int i = 6; i < stop_col; i++)
                            {

                                for (int j = 9; j < 73; j++)
                                {
                                    PHB_PL42_P1_TT01_DETAIL detail = new PHB_PL42_P1_TT01_DETAIL()
                                    {
                                        PHB_PL42_P1_TT01_REFID = bieu.REFID,
                                        ObjectState = ObjectState.Added
                                    };
                                    detail.MASO = workSheet.Cells[j, 1].Text;
                                    detail.CHITIEU = workSheet.Cells[j, 2].Text;
                                    detail.DON_VI = workSheet.Cells[6, i].Text;
                                    detail.LOAI_KHOAN = workSheet.Cells[7, i].Text;
                                    
                                    double sotien = 0;
                                    if (!string.IsNullOrEmpty(workSheet.Cells[j, i].Text) && double.TryParse(workSheet.Cells[j, i].Text, out sotien))
                                    {
                                        detail.SO_TIEN = decimal.Parse(workSheet.Cells[j, i].Text);
                                    }
                                    else
                                    {
                                        detail.SO_TIEN = 0;
                                    }
                                    _PL42_P1_TT01DetailService.Insert(detail);
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
                            response.Message = ErrorMessage.EMPTY_DATA;
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
            var response = new Response<PL42_P1_TT01Vm.ViewModel>();
            try
            {
                var model = new PL42_P1_TT01Vm.ViewModel();
                model.REFID = refid;
                model.DETAIL = await _PL42_P1_TT01DetailService.Queryable().Where(x => x.PHB_PL42_P1_TT01_REFID.Equals(refid))
                    .OrderBy(x => x.ID)
                    .ToListAsync();
                response.Error = false;
                response.Data = model;
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
        public async Task<IHttpActionResult> Post(PL42_P1_TT01Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var PL42_P1_TT01 = new PHB_PL42_P1_TT01()
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
                var checkReport = await _PL42_P1_TT01Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(PL42_P1_TT01.MA_CHUONG) && x.MA_QHNS.Equals(PL42_P1_TT01.MA_QHNS) &&
                    x.NAM_BC == PL42_P1_TT01.NAM_BC && x.KY_BC == PL42_P1_TT01.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _PL42_P1_TT01Service.Insert(PL42_P1_TT01);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_PL42_P1_TT01_REFID = PL42_P1_TT01.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _PL42_P1_TT01DetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(PL42_P1_TT01Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var PL42_P1_TT01 =
                    await _PL42_P1_TT01Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (PL42_P1_TT01 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete

                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var id in model.LstDelete)
                    {
                        var item = await _PL42_P1_TT01DetailService.FindByIdAsync(id);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _PL42_P1_TT01DetailService.Delete(item);
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
                        item.PHB_PL42_P1_TT01_REFID = PL42_P1_TT01.REFID;
                        _PL42_P1_TT01DetailService.Insert(item);
                    }
                }

                #endregion

                #region edit

                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _PL42_P1_TT01DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.CHITIEU = item.CHITIEU;
                            detail.MASO = item.MASO;
                            detail.LOAI_KHOAN = item.LOAI_KHOAN;
                            detail.DON_VI = item.DON_VI;
                            detail.SO_TIEN = item.SO_TIEN;
                            _PL42_P1_TT01DetailService.Update(detail);
                        }
                    }
                }

                #endregion

                if (hasValue)
                {
                    PL42_P1_TT01.NGAY_SUA = DateTime.Now;
                    PL42_P1_TT01.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _PL42_P1_TT01Service.Update(PL42_P1_TT01);
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
            var response = await _PL42_P1_TT01Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
                string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _PL42_P1_TT01Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC,
                rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/PL42_P1_TT01/PL42_P1_TT01_Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/PL42_P1_TT01/" + RequestContext.Principal.Identity.Name
                    + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["C2"].Value = sheet.Cells["C2"].Value + " " + rqmodel.NAM_BC;

                        var listByLoaiKhoan = data.Data.DETAIL.GroupBy(x => x.LOAI_KHOAN);
                        var startRow = 8;
                        foreach (var list in listByLoaiKhoan)
                        {
                            var listData = list.ToList().OrderByDescending(x => x.MASO);

                        }

                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_PL41.xlsx"
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