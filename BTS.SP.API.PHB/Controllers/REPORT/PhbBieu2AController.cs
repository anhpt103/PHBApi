using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU2A;
using BTS.SP.PHB.SERVICE.Models.BIEU2A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu2A;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using OfficeOpenXml.Style;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBieu2a")]
    [Route("{id?}")]
    public class PhbBieu2AController : ApiController
    {
        private readonly IPhbBieu2AService _bieu2AService;
        private readonly IPhbBieu2ADetailService _bieu2ADetailService;
        private readonly IPhbBieu2ATemplateService _bieu2ATemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBieu2AController(IPhbBieu2AService bieu2AService, IPhbBieu2ADetailService bieu2ADetailService, IPhbBieu2ATemplateService bieu2ATemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu2AService = bieu2AService;
            _bieu2ADetailService = bieu2ADetailService;
            _bieu2ATemplateService = bieu2ATemplateService;
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
                        var bieu2A = new PHB_BIEU2A()
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
                            bieu2A.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu2A.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu2A.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieu2A.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu2A.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu2A.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu2AService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu2A.MA_CHUONG) && x.MA_QHNS.Equals(bieu2A.MA_QHNS) &&
                                x.NAM_BC == bieu2A.NAM_BC && x.KY_BC == bieu2A.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu2AService.Insert(bieu2A);

                            #region insert PHÍ
                            var startRowPhi = 9;
                            var mamuc = string.Empty;
                            var tenmuc = string.Empty;
                            var isLoop = true;
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
                                        if (workSheet.Cells[startRowPhi, 2].Value != null)
                                        {
                                            mamuc = workSheet.Cells[startRowPhi, 2].Value.ToString();
                                            tenmuc = workSheet.Cells[startRowPhi, 3].Value.ToString();
                                            startRowPhi += 1;
                                        }
                                        else
                                        {
                                            for (var i = 1; i <= 3; i++)
                                            {
                                                var tempRow = startRowPhi + i - 1;
                                                var detail = new PHB_BIEU2A_DETAIL()
                                                {
                                                    ObjectState = ObjectState.Added,
                                                    PHB_BIEU2A_REFID = bieu2A.REFID,
                                                    LOAI = 1
                                                };
                                                detail.STT_CHI_TIEU = workSheet.Cells[tempRow, 1].Value != null ? workSheet.Cells[tempRow, 1].Value.ToString() : null;
                                                detail.MA_NOIDUNGKT = mamuc;
                                                detail.TEN_NOIDUNGKT = tenmuc;
                                                detail.MA_CHI_TIEU = i;
                                                detail.TEN_CHI_TIEU = workSheet.Cells[tempRow, 3].Value != null ? workSheet.Cells[tempRow, 3].Value.ToString() : null;
                                                if (workSheet.Cells[tempRow, 4].Value != null)
                                                {
                                                    try
                                                    {
                                                        detail.DU_TOAN = double.Parse(workSheet.Cells[tempRow, 4].Value.ToString());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        WriteLogs.LogError(ex);
                                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                                    }
                                                }
                                                else
                                                {
                                                    detail.DU_TOAN = 0;
                                                }

                                                if (workSheet.Cells[tempRow, 5].Value != null)
                                                {
                                                    try
                                                    {
                                                        detail.THUC_HIEN = double.Parse(workSheet.Cells[tempRow, 5].Value.ToString());
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        WriteLogs.LogError(ex);
                                                        return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                                    }
                                                }
                                                else
                                                {
                                                    detail.THUC_HIEN = 0;
                                                }

                                                _bieu2ADetailService.Insert(detail);
                                            }
                                            startRowPhi += 3;
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region insert LỆ PHÍ

                            var startRowLePhi = startRowPhi;
                            if (workSheet.Cells[startRowLePhi, 1].Value != null &&
                                workSheet.Cells[startRowLePhi, 1].Value.ToString().Equals("II")) startRowLePhi += 1;
                            while (workSheet.Cells[startRowLePhi, 3].Value != null)
                            {
                                if (workSheet.Cells[startRowLePhi, 2].Value != null)
                                {
                                    mamuc = workSheet.Cells[startRowLePhi, 2].Value.ToString();
                                    tenmuc = workSheet.Cells[startRowLePhi, 3].Value.ToString();
                                    startRowLePhi += 1;
                                }
                                else
                                {
                                    for (var i = 1; i <= 3; i++)
                                    {
                                        var tempRow = startRowLePhi + i - 1;
                                        var detail = new PHB_BIEU2A_DETAIL()
                                        {
                                            ObjectState = ObjectState.Added,
                                            PHB_BIEU2A_REFID = bieu2A.REFID,
                                            LOAI = 2
                                        };
                                        detail.STT_CHI_TIEU = workSheet.Cells[tempRow, 1].Value != null ? workSheet.Cells[tempRow, 1].Value.ToString() : null;
                                        detail.MA_NOIDUNGKT = mamuc;
                                        detail.TEN_NOIDUNGKT = tenmuc;
                                        detail.MA_CHI_TIEU = i;
                                        detail.TEN_CHI_TIEU = workSheet.Cells[tempRow, 3].Value != null ? workSheet.Cells[tempRow, 3].Value.ToString() : null;
                                        if (workSheet.Cells[tempRow, 4].Value != null)
                                        {
                                            try
                                            {
                                                detail.DU_TOAN = double.Parse(workSheet.Cells[tempRow, 4].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.DU_TOAN = 0;
                                        }

                                        if (workSheet.Cells[tempRow, 5].Value != null)
                                        {
                                            try
                                            {
                                                detail.THUC_HIEN = double.Parse(workSheet.Cells[tempRow, 5].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.THUC_HIEN = 0;
                                        }
                                        _bieu2ADetailService.Insert(detail);
                                    }
                                    startRowLePhi += 3;
                                }
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
            var response = new Response<BIEU2AVm.ViewModel>();
            try
            {
                var data = new BIEU2AVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _bieu2ADetailService.Queryable().Where(x => x.PHB_BIEU2A_REFID.Equals(refid))
                    .OrderBy(x => x.MA_NOIDUNGKT).ThenBy(x => x.MA_CHI_TIEU)
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_BIEU2A_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bieu2ATemplateService.Queryable().OrderBy(x => x.MA_NOIDUNGKT).ToListAsync();
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
            var response = await _bieu2ATemplateService.GetTemplateForEdit(refid);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(BIEU2AVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            var response = new Response<string>();
            try
            {
                var bieu2A = new PHB_BIEU2A()
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
                _bieu2AService.Insert(bieu2A);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU2A_REFID = bieu2A.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu2ADetailService.Insert(detail);
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
        public async Task<IHttpActionResult> Put(BIEU2AVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU2A bieu2A = await _bieu2AService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu2A == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU2A_DETAIL item = await _bieu2ADetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu2ADetailService.Delete(item);
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
                        item.PHB_BIEU2A_REFID = model.REFID;
                        _bieu2ADetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU2A_DETAIL detail = await _bieu2ADetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.DU_TOAN = item.DU_TOAN;
                            detail.THUC_HIEN = item.THUC_HIEN;
                            _bieu2ADetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu2A.NGAY_SUA = DateTime.Now;
                    bieu2A.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu2AService.Update(bieu2A);

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
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            var response = await _bieu2AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("SumAllReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumAllReport(ReportRqModel rqmodel)
        {
            var response = await _bieu2AService.SumAllReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            string tendonvi = (rqmodel.TEN_DSDVQHNS.Count == 0) ? "Toàn đơn vị" : rqmodel.TEN_DSDVQHNS[0];
            var data1 = await _bieu2AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            var data2 = await _bieu2AService.SumAllReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (rqmodel.TONG_HOP_BAOCAO == false)
            {
                if (data1 != null && !data1.Error && data1.Data.DETAIL.Count > 0)
                {
                    var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU2A/Template.xlsx");
                    var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU2A/" +
                        RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                    try
                    {
                        using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                        {
                            var sheet = excelPackage.Workbook.Worksheets[1];
                            sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                            sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", tendonvi);
                            string[] lstTenChiTieu = { "- Tổng số", "- Tổng số thu", "- Số phải nộp NSNN", "- Số được khấu trừ để lại" };

                            var listByMaQhns = data1.Data.DETAIL.GroupBy(x => x.MA_QHNS);
                            var startRowInsert = 10;
                            foreach (var list in listByMaQhns)
                            {
                                if (list.Key.Equals("0"))
                                {
                                    // dữ liệu tổng hợp
                                    var lstByLoai = list.ToList().GroupBy(x => x.LOAI);
                                    foreach (var lstLoai in lstByLoai)
                                    {
                                        if (lstLoai.Key.Equals(1))
                                        {
                                            //insert loại PHÍ
                                            sheet.InsertRow(startRowInsert, 1);
                                            sheet.Cells[startRowInsert, 1].Value = "I";
                                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3].Value = "PHÍ";
                                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                                ExcelHorizontalAlignment.Center;
                                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                                ExcelVerticalAlignment.Center;
                                            for (var i = 1; i <= 7; i++)
                                            {
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            startRowInsert += 1;

                                            var lstPhi = lstLoai.ToList();
                                            sheet.InsertRow(startRowInsert, lstPhi.Count);
                                            for (var i = 0; i < lstPhi.Count; i++)
                                            {
                                                for (var j = 1; j <= 7; j++)
                                                {
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                                    if (lstPhi[i].MA_CHI_TIEU == 3)
                                                    {
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                    }
                                                }
                                                sheet.Cells[startRowInsert + i, 1].Value = lstPhi[i].STT_CHI_TIEU;
                                                sheet.Cells[startRowInsert + i, 2].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].MA_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 3].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].TEN_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstPhi[i].MA_CHI_TIEU];
                                                sheet.Cells[startRowInsert + i, 5].Value = lstPhi[i].DU_TOAN;
                                                sheet.Cells[startRowInsert + i, 6].Value = lstPhi[i].THUC_HIEN;
                                                if (lstPhi[i].DU_TOAN > 0 || lstPhi[i].DU_TOAN < 0)
                                                {
                                                    sheet.Cells[startRowInsert + i, 7].Value = lstPhi[i].THUC_HIEN * 100 / lstPhi[i].DU_TOAN;
                                                }
                                            }
                                            startRowInsert += lstPhi.Count;
                                        }
                                        else
                                        {
                                            //Lệ phí
                                            sheet.InsertRow(startRowInsert, 1);
                                            sheet.Cells[startRowInsert, 1].Value = "II";
                                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3].Value = "LỆ PHÍ";
                                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                                ExcelHorizontalAlignment.Center;
                                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                                ExcelVerticalAlignment.Center;
                                            for (var i = 1; i <= 7; i++)
                                            {
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Top.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            startRowInsert += 1;

                                            var lstLePhi = lstLoai.ToList();
                                            sheet.InsertRow(startRowInsert, lstLePhi.Count);
                                            for (var i = 0; i < lstLePhi.Count; i++)
                                            {
                                                for (var j = 1; j <= 7; j++)
                                                {
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                                    if (lstLePhi[i].MA_CHI_TIEU == 3)
                                                    {
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                    }
                                                }
                                                sheet.Cells[startRowInsert + i, 1].Value = lstLePhi[i].STT_CHI_TIEU;
                                                sheet.Cells[startRowInsert + i, 2].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].MA_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 3].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].TEN_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstLePhi[i].MA_CHI_TIEU];
                                                sheet.Cells[startRowInsert + i, 5].Value = lstLePhi[i].DU_TOAN;
                                                sheet.Cells[startRowInsert + i, 6].Value = lstLePhi[i].THUC_HIEN;
                                                if (lstLePhi[i].DU_TOAN > 0 || lstLePhi[i].DU_TOAN < 0)
                                                {
                                                    sheet.Cells[startRowInsert + i, 7].Value = lstLePhi[i].THUC_HIEN * 100 / lstLePhi[i].DU_TOAN;
                                                }
                                            }
                                            startRowInsert += lstLePhi.Count;
                                        }
                                    }
                                }
                                else
                                {
                                    var lst = list.ToList();
                                    var lstByLoai = lst.GroupBy(x => x.LOAI);
                                    sheet.InsertRow(startRowInsert, 1);
                                    startRowInsert += 1;
                                    sheet.Cells[startRowInsert, 3].Value = list.Key + "--" + lst[0].TEN_QHNS;
                                    sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                                    sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                                    startRowInsert += 1;
                                    sheet.InsertRow(startRowInsert, 4);
                                    sheet.Cells[7, 1, 9, 7].Copy(sheet.Cells[startRowInsert, 1, startRowInsert + 2, 7]);
                                    startRowInsert += 3;
                                    foreach (var lstLoai in lstByLoai)
                                    {
                                        if (lstLoai.Key.Equals(1))
                                        {
                                            //insert loại PHÍ
                                            sheet.InsertRow(startRowInsert, 1);
                                            sheet.Cells[startRowInsert, 1].Value = "I";
                                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3].Value = "PHÍ";
                                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                                ExcelHorizontalAlignment.Center;
                                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                                ExcelVerticalAlignment.Center;
                                            for (var i = 1; i <= 7; i++)
                                            {
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            startRowInsert += 1;

                                            var lstPhi = lstLoai.ToList();
                                            sheet.InsertRow(startRowInsert, lstPhi.Count);
                                            for (var i = 0; i < lstPhi.Count; i++)
                                            {
                                                for (var j = 1; j <= 7; j++)
                                                {
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                                    if (lstPhi[i].MA_CHI_TIEU == 3)
                                                    {
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                    }
                                                }
                                                sheet.Cells[startRowInsert + i, 1].Value = lstPhi[i].STT_CHI_TIEU;
                                                sheet.Cells[startRowInsert + i, 2].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].MA_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 3].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].TEN_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstPhi[i].MA_CHI_TIEU];
                                                sheet.Cells[startRowInsert + i, 5].Value = lstPhi[i].DU_TOAN;
                                                sheet.Cells[startRowInsert + i, 6].Value = lstPhi[i].THUC_HIEN;
                                                if (lstPhi[i].DU_TOAN < 0 || lstPhi[i].DU_TOAN > 0)
                                                {
                                                    sheet.Cells[startRowInsert + i, 7].Value = lstPhi[i].THUC_HIEN * 100 / lstPhi[i].DU_TOAN;
                                                }
                                            }
                                            startRowInsert += lstPhi.Count;
                                        }
                                        else
                                        {
                                            //Lệ phí
                                            sheet.InsertRow(startRowInsert, 1);
                                            sheet.Cells[startRowInsert, 1].Value = "II";
                                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3].Value = "LỆ PHÍ";
                                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                                ExcelHorizontalAlignment.Center;
                                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                                ExcelVerticalAlignment.Center;
                                            for (var i = 1; i <= 7; i++)
                                            {
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Top.Color.SetColor(Color.Black);
                                                sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                            }
                                            startRowInsert += 1;

                                            var lstLePhi = lstLoai.ToList();
                                            sheet.InsertRow(startRowInsert, lstLePhi.Count);
                                            for (var i = 0; i < lstLePhi.Count; i++)
                                            {
                                                for (var j = 1; j <= 7; j++)
                                                {
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                                    if (lstLePhi[i].MA_CHI_TIEU == 3)
                                                    {
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                                    }
                                                }
                                                sheet.Cells[startRowInsert + i, 1].Value = lstLePhi[i].STT_CHI_TIEU;
                                                sheet.Cells[startRowInsert + i, 2].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].MA_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 3].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].TEN_NOIDUNGKT : "";
                                                sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstLePhi[i].MA_CHI_TIEU];
                                                sheet.Cells[startRowInsert + i, 5].Value = lstLePhi[i].DU_TOAN;
                                                sheet.Cells[startRowInsert + i, 6].Value = lstLePhi[i].THUC_HIEN;
                                                if (lstLePhi[i].DU_TOAN < 0 || lstLePhi[i].DU_TOAN > 0)
                                                {
                                                    sheet.Cells[startRowInsert + i, 7].Value = lstLePhi[i].THUC_HIEN * 100 / lstLePhi[i].DU_TOAN;
                                                }
                                            }
                                            startRowInsert += lstLePhi.Count;
                                        }
                                    }
                                }
                            }
                            excelPackage.SaveAs(new FileInfo(exportUrlFile));
                            var result = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                            };
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "export_BIEU2A.xlsx"
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
                return Ok(data1);
            }
            else
            {
                if (data2 != null && !data2.Error && data2.Data.DETAIL.Count > 0)
                {
                    var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU2A/Template.xlsx");
                    var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU2A/" +
                        RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                    try
                    {
                        using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                        {
                            var sheet = excelPackage.Workbook.Worksheets[1];
                            sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                            sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", tendonvi);
                            string[] lstTenChiTieu = { "- Tổng số", "- Tổng số thu", "- Số phải nộp NSNN", "- Số được khấu trừ để lại" };

                            var listByMaQhns = data2.Data.DETAIL;
                            var startRowInsert = 10;

                            sheet.InsertRow(startRowInsert, 1);
                            sheet.Cells[startRowInsert, 1].Value = "I";
                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                            sheet.Cells[startRowInsert, 3].Value = "PHÍ";
                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                ExcelHorizontalAlignment.Center;
                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                ExcelVerticalAlignment.Center;

                            foreach (var list in listByMaQhns)
                            {
                                if (list.LOAI == 1)
                                {
                                    //insert loại PHÍ
                                    for (var i = 1; i <= 7; i++)
                                    {
                                        sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                        sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    startRowInsert += 3;
                                    sheet.InsertRow(startRowInsert, 1);
                                    for (int i = startRowInsert - lstTenChiTieu.Count() + 2; i <= startRowInsert; i++)
                                    {
                                        for (var j = 1; j <= 7; j++)
                                        {
                                            sheet.Cells[i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            sheet.Cells[i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            if (i == startRowInsert - lstTenChiTieu.Count() + 4)
                                            {
                                                sheet.Cells[i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                            }
                                        }
                                    }
                                    for (int i = startRowInsert - lstTenChiTieu.Count() + 2; i <= startRowInsert; i++)
                                    {
                                        sheet.Cells[i, 1].Value = list.STT_CHI_TIEU.ToString() + "." + (i - (startRowInsert - lstTenChiTieu.Count() + 2) + 1).ToString();
                                        sheet.Cells[i, 2].Value = (i == startRowInsert - lstTenChiTieu.Count() + 2) ? list.MA_NOIDUNGKT.ToString() : "";
                                        sheet.Cells[i, 3].Value = (i == startRowInsert - lstTenChiTieu.Count() + 2) ? list.TEN_NOIDUNGKT.ToString() : "";
                                        sheet.Cells[i, 4].Value = lstTenChiTieu[i - (startRowInsert - lstTenChiTieu.Count() + 2) + 1];
                                        sheet.Cells[i, 5].Value = list.DU_TOAN;
                                        if (i == startRowInsert - lstTenChiTieu.Count() + 2)
                                        {
                                            sheet.Cells[i, 6].Value = list.TONG_THU;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TONG_THU * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                        else if (i == startRowInsert - lstTenChiTieu.Count() + 3)
                                        {
                                            sheet.Cells[i, 6].Value = list.TIEN_NSNN;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TIEN_NSNN * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                        else
                                        {
                                            sheet.Cells[i, 6].Value = list.TIEN_KHAUTRU;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TIEN_KHAUTRU * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                    }
                                    //startRowInsert += 3;
                                }

                            }
                            startRowInsert += 1;
                            //Lệ phí
                            sheet.InsertRow(startRowInsert, 1);
                            sheet.Cells[startRowInsert, 1].Value = "II";
                            sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
                            sheet.Cells[startRowInsert, 3].Value = "LỆ PHÍ";
                            sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
                            sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
                            sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
                                ExcelHorizontalAlignment.Center;
                            sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
                                ExcelVerticalAlignment.Center;
                            foreach (var list in listByMaQhns)
                            {
                                if (list.LOAI == 2)
                                {
                                    for (var i = 1; i <= 7; i++)
                                    {
                                        sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
                                        sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    startRowInsert += 3;
                                    sheet.InsertRow(startRowInsert, 1);
                                    for (int i = startRowInsert - lstTenChiTieu.Count() + 2; i <= startRowInsert; i++)
                                    {
                                        for (var j = 1; j <= 7; j++)
                                        {
                                            sheet.Cells[i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            sheet.Cells[i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                            if (i == startRowInsert - lstTenChiTieu.Count() + 4)
                                            {
                                                sheet.Cells[i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                sheet.Cells[i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
                                            }
                                        }
                                    }
                                    for (int i = startRowInsert - lstTenChiTieu.Count() + 2; i <= startRowInsert; i++)
                                    {
                                        sheet.Cells[i, 1].Value = list.STT_CHI_TIEU.ToString() + "." + (i - (startRowInsert - lstTenChiTieu.Count() + 2) + 1).ToString();
                                        sheet.Cells[i, 2].Value = (i == startRowInsert - lstTenChiTieu.Count() + 2) ? list.MA_NOIDUNGKT.ToString() : "";
                                        sheet.Cells[i, 3].Value = (i == startRowInsert - lstTenChiTieu.Count() + 2) ? list.TEN_NOIDUNGKT.ToString() : "";
                                        sheet.Cells[i, 4].Value = lstTenChiTieu[i - (startRowInsert - lstTenChiTieu.Count() + 2) + 1];
                                        sheet.Cells[i, 5].Value = list.DU_TOAN;
                                        if (i == startRowInsert - lstTenChiTieu.Count() + 2)
                                        {
                                            sheet.Cells[i, 6].Value = list.TONG_THU;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TONG_THU * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                        else if (i == startRowInsert - lstTenChiTieu.Count() + 3)
                                        {
                                            sheet.Cells[i, 6].Value = list.TIEN_NSNN;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TIEN_NSNN * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                        else
                                        {
                                            sheet.Cells[i, 6].Value = list.TIEN_KHAUTRU;
                                            if (list.DU_TOAN > 0 || list.DU_TOAN < 0)
                                            {
                                                sheet.Cells[i, 7].Value = list.TIEN_KHAUTRU * 100 / list.DU_TOAN;
                                            }
                                            else
                                            {
                                                sheet.Cells[i, 7].Value = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            excelPackage.SaveAs(new FileInfo(exportUrlFile));
                            var result = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                            };
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "export_BIEU2A.xlsx"
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
                return Ok(data2);
            }
        }
    }
}
