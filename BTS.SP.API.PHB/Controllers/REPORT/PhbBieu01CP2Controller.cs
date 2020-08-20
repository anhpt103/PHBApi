using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU01CP2;
using BTS.SP.PHB.SERVICE.Models.BIEU01CP2;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01CP2;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using System.Web;
using OfficeOpenXml;
using BTS.SP.PHB.ENTITY.Rp;
using System.IO;
using System.Net.Http.Headers;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Security.Claims;


namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU01CP2")]
    [Route("{id?}")]
    public class PhbBIEU01CP2Controller : ApiController
    {
        private readonly IBIEU01CP2Service _bieu01cp2Service;
        private readonly IBIEU01CP2DetailService _bieu01cp2DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBIEU01CP2Controller(IBIEU01CP2Service bieu01cp2Service, IBIEU01CP2DetailService bieu01cp2DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu01cp2Service = bieu01cp2Service;
            _bieu01cp2DetailService = bieu01cp2DetailService;
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
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            PHB_BIEU01CP2 bieuF01_02 = new PHB_BIEU01CP2()
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
                            bieuF01_02.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieuF01_02.MA_QHNS = httpRequest["MA_QHNS"];
                            bieuF01_02.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieuF01_02.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieuF01_02.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieuF01_02.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu01cp2Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieuF01_02.MA_CHUONG) && x.MA_QHNS.Equals(bieuF01_02.MA_QHNS) &&
                                x.NAM_BC == bieuF01_02.NAM_BC && x.KY_BC == bieuF01_02.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bieu01cp2Service.Insert(bieuF01_02);

                            string noidungchi = string.Empty;
                            int startRow =13;
                            var loai = 1;
                            while (workSheet.Cells[startRow, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 5].Value.ToString()))
                            {
                                {
                                    //chi tieu danh mục
                                    noidungchi = workSheet.Cells[startRow, 5].Value.ToString();
                                    PHB_BIEU01CP2_DETAIL detail = new PHB_BIEU01CP2_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU01CP2_REFID = bieuF01_02.REFID,
                                        LOAI = loai,
                                    };
                                    switch (noidungchi)
                                    {
                                        case "I. Kinh phí thường xuyên/ tự chủ":
                                            startRow += 1;
                                            break;
                                        case "II. Kinh phí không thường xuyên/không tự chủ":
                                            startRow += 1;
                                            loai++;
                                            detail.LOAI = loai;
                                            break;
                                    }
                                    detail.NOI_DUNG_CHI = workSheet.Cells[startRow, 5].Value != null
                                    ? workSheet.Cells[startRow, 5].Value.ToString()
                                    : null;
                                    detail.MA_LOAI = workSheet.Cells[startRow, 1].Value != null
                                    ? workSheet.Cells[startRow, 1].Value.ToString()
                                    : null;
                                    detail.MA_KHOAN = workSheet.Cells[startRow, 2].Value != null
                                    ? workSheet.Cells[startRow, 2].Value.ToString()
                                    : null;
                                    detail.MA_MUC = workSheet.Cells[startRow, 3].Value != null
                                    ? workSheet.Cells[startRow, 3].Value.ToString()
                                    : null;
                                    detail.MA_TIEU_MUC = workSheet.Cells[startRow, 4].Value != null
                                    ? workSheet.Cells[startRow, 4].Value.ToString()
                                    : null;
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.TS_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 6].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TS_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.TS_SOXDTD = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.TS_SOXDTD = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSTN_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSTN_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 10].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSTN_SOXDTD = double.Parse(workSheet.Cells[startRow, 10].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSTN_SOXDTD = 0;
                                    }
                                    if (workSheet.Cells[startRow, 12].Value != null)
                                    {
                                        try
                                        {
                                            detail.VT_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 12].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VT_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 13].Value != null)
                                    {
                                        try
                                        {
                                            detail.VT_SOXDTD = double.Parse(workSheet.Cells[startRow, 13].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VT_SOXDTD = 0;
                                    }
                                    if (workSheet.Cells[startRow, 15].Value != null)
                                    {
                                        try
                                        {
                                            detail.VNNN_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 15].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VNNN_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 16].Value != null)
                                    {
                                        try
                                        {
                                            detail.VNNN_SOXDTD = double.Parse(workSheet.Cells[startRow, 16].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.VNNN_SOXDTD = 0;
                                    }
                                    if (workSheet.Cells[startRow, 18].Value != null)
                                    {
                                        try
                                        {
                                            detail.PDKTL_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 18].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.PDKTL_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 19].Value != null)
                                    {
                                        try
                                        {
                                            detail.PDKTL_SOXDTD = double.Parse(workSheet.Cells[startRow, 19].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.PDKTL_SOXDTD = 0;
                                    }
                                    if (workSheet.Cells[startRow, 21].Value != null)
                                    {
                                        try
                                        {
                                            detail.NHDKDL_SOBAOCAO = double.Parse(workSheet.Cells[startRow, 21].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NHDKDL_SOBAOCAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 22].Value != null)
                                    {
                                        try
                                        {
                                            detail.NHDKDL_SOXDTD = double.Parse(workSheet.Cells[startRow, 22].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.NHDKDL_SOXDTD = 0;
                                    }
                                    _bieu01cp2DetailService.Insert(detail);
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
            Response<BIEU01CP2Vm.ViewModel> response = new Response<BIEU01CP2Vm.ViewModel>();
            try
            {
                BIEU01CP2Vm.ViewModel data = new BIEU01CP2Vm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _bieu01cp2DetailService.Queryable().Where(x => x.PHB_BIEU01CP2_REFID.Equals(refid))
                    .OrderBy(x => x.LOAI).ThenBy(x=>x.MA_LOAI).ThenBy(x=>x.MA_KHOAN).ThenBy(x=>x.MA_MUC).ThenBy(x=>x.MA_TIEU_MUC)
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
        [Route("Post")]
        public async Task<IHttpActionResult> Post(BIEU01CP2Vm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_BIEU01CP2 bieu01cp2 = new PHB_BIEU01CP2()
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

                var checkReport = await _bieu01cp2Service.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(bieu01cp2.MA_CHUONG) && x.MA_QHNS.Equals(bieu01cp2.MA_QHNS) &&
                    x.NAM_BC == bieu01cp2.NAM_BC && x.KY_BC == bieu01cp2.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _bieu01cp2Service.Insert(bieu01cp2);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_BIEU01CP2_REFID = bieu01cp2.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _bieu01cp2DetailService.Insert(detail);
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

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
             var response = await _bieu01cp2Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(BIEU01CP2Vm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_BIEU01CP2 bieu01cp2 = await _bieu01cp2Service.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (bieu01cp2 == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_BIEU01CP2_DETAIL item = await _bieu01cp2DetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _bieu01cp2DetailService.Delete(item);
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
                        item.PHB_BIEU01CP2_REFID = model.REFID;
                        _bieu01cp2DetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_BIEU01CP2_DETAIL detail = await _bieu01cp2DetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.TS_SOBAOCAO = item.TS_SOBAOCAO;
                            detail.TS_SOXDTD = item.TS_SOXDTD;
                            detail.NSTN_SOBAOCAO = item.NSTN_SOBAOCAO;
                            detail.NSTN_SOXDTD = item.NSTN_SOXDTD;
                            detail.VT_SOBAOCAO = item.VT_SOBAOCAO;
                            detail.VT_SOXDTD = item.VT_SOXDTD;
                            detail.VNNN_SOBAOCAO = item.VNNN_SOBAOCAO;
                            detail.VNNN_SOXDTD = item.VNNN_SOXDTD;
                            detail.PDKTL_SOBAOCAO = item.PDKTL_SOBAOCAO;
                            detail.PDKTL_SOXDTD = item.PDKTL_SOXDTD;
                            detail.NHDKDL_SOBAOCAO = item.NHDKDL_SOBAOCAO;
                            detail.NHDKDL_SOXDTD = item.NHDKDL_SOXDTD;
                            _bieu01cp2DetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    bieu01cp2.NGAY_SUA = DateTime.Now;
                    bieu01cp2.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _bieu01cp2Service.Update(bieu01cp2);

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

        [Route("ExportReport")]
        [HttpPost]
        public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        {
            var data = await _bieu01cp2Service.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU01CP2/Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU01CP2/" +
                    RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        var sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
                        sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);

                        var listByMaQhns = data.Data.DETAIL.GroupBy(x => x.MA_LOAI);
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
                                    for (var j = 1; j <= 6; j++)
                                    {
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                                    }
                                    //if (lst[i].INDAM == 1) sheet.Row(startRowInsert + i).Style.Font.Bold = true;
                                    //if (lst[i].INNGHIENG == 1) sheet.Row(startRowInsert + i).Style.Font.Italic = true;
                                    sheet.Cells[startRowInsert + i, 1].Value = lst[i].LOAI;
                                    sheet.Cells[startRowInsert + i, 2].Value = lst[i].MA_KHOAN;
                                    sheet.Cells[startRowInsert + i, 3].Value = lst[i].MA_MUC;
                                    sheet.Cells[startRowInsert + i, 4].Value = lst[i].MA_TIEU_MUC;
                                    sheet.Cells[startRowInsert + i, 5].Value = lst[i].NOI_DUNG_CHI;
                                    sheet.Cells[startRowInsert + i, 6].Value = lst[i].TS_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 7].Value = lst[i].TS_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 8].Value = lst[i].TS_SOXDTD - lst[i].TS_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 9].Value = lst[i].NSTN_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 10].Value = lst[i].NSTN_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 11].Value = lst[i].NSTN_SOXDTD - lst[i].NSTN_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 12].Value = lst[i].VT_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 13].Value = lst[i].VT_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 14].Value = lst[i].VT_SOXDTD - lst[i].VT_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 15].Value = lst[i].VNNN_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 16].Value = lst[i].VNNN_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 17].Value = lst[i].VNNN_SOXDTD - lst[i].VNNN_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 18].Value = lst[i].PDKTL_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 19].Value = lst[i].PDKTL_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 20].Value = lst[i].PDKTL_SOXDTD - lst[i].PDKTL_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 21].Value = lst[i].NHDKDL_SOBAOCAO;
                                    sheet.Cells[startRowInsert + i, 22].Value = lst[i].NHDKDL_SOXDTD;
                                    sheet.Cells[startRowInsert + i, 23].Value = lst[i].NHDKDL_SOXDTD - lst[i].NHDKDL_SOBAOCAO;
                                }
                                startRowInsert += lst.Count;
                            }
                            //else
                            //{
                            //    // dữ liệu chi tiết đơn vị
                            //    var lst = list.ToList();
                            //    sheet.InsertRow(startRowInsert, 1);
                            //    startRowInsert += 1;
                            //    sheet.Cells[startRowInsert, 2].Value = list.Key + "--" + lst[0].TEN_QHNS;
                            //    sheet.Cells[startRowInsert, 2].Style.Font.Bold = true;
                            //    startRowInsert += 1;
                            //    sheet.InsertRow(startRowInsert, 4);
                            //    sheet.Cells[9, 1, 11, 6].Copy(sheet.Cells[startRowInsert, 1, startRowInsert + 2, 6]);
                            //    startRowInsert += 3;
                            //    sheet.InsertRow(startRowInsert, lst.Count);
                            //    for (var i = 0; i < lst.Count; i++)
                            //    {
                            //        for (var j = 1; j <= 6; j++)
                            //        {
                            //            sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            //            sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
                            //        }
                            //        //if (lst[i].INDAM == 1) sheet.Row(startRowInsert + i).Style.Font.Bold = true;
                            //        //if (lst[i].INNGHIENG == 1) sheet.Row(startRowInsert + i).Style.Font.Italic = true;
                            //        sheet.Cells[startRowInsert + i, 1].Value = lst[i].STT_CHI_TIEU;
                            //        sheet.Cells[startRowInsert + i, 2].Value = lst[i].TEN_CHI_TIEU;
                            //        sheet.Cells[startRowInsert + i, 3].Value = lst[i].DU_TOAN_DUOC_GIAO;
                            //        sheet.Cells[startRowInsert + i, 4].Value = lst[i].QUYET_TOAN_NAM;
                            //        sheet.Cells[startRowInsert + i, 5].Value = lst[i].QUYET_TOAN_NAM - lst[i].DU_TOAN_DUOC_GIAO;
                            //        if (lst[i].DU_TOAN_DUOC_GIAO > 0 || lst[i].DU_TOAN_DUOC_GIAO < 0)
                            //        {
                            //            sheet.Cells[startRowInsert + i, 6].Value = Math.Round(lst[i].QUYET_TOAN_NAM / lst[i].DU_TOAN_DUOC_GIAO, 2);
                            //        }
                            //    }
                            //    startRowInsert += lst.Count;
                            //}
                        }
                        excelPackage.SaveAs(new FileInfo(exportUrlFile));
                        var result = new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new ByteArrayContent(excelPackage.GetAsByteArray())
                        };
                        result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "export_BIEU01CP2.xlsx"
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

        public class BAOCAO_PARAM
        {
            public int NAM_BC { get; set; }
            public string lst_DVQHNS { get; set; }
            public string TEN_DVQHNS { get; set; }
            public int KY_BC { get; set; }
            public int DonViTien { get; set; }
            public string MA_CHUONG { get; set; }
        }

        [Route("BAOCAODAURA")]
        public HttpResponseMessage BAOCAODAURA(BAOCAO_PARAM para)
        {
            HttpResponseMessage result = null;
            string file = null;

            file = _CreateExcelFileBaoCao(para);
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new StreamContent(new FileStream(file, FileMode.Open, FileAccess.Read));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = "Export_(" + DateTime.Now.ToString("dd-MM-yyyy") + ").xls";
                }
            }
            return result;
        }
        public string _CreateExcelFileBaoCao(BAOCAO_PARAM para)
        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string date = now.ToString("dd-MM-yyyy");
            var fileNameInPut = "BIEU01CP2_DAURA.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/BIEU01CP2/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInPut;
            //var urlFileTemplate = "C:/inetpub/wwwroot/wss/VirtualDirectories/API_PHB/Template/BIEU3BP1/template3b_PI.xlsx";
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                string[] refid = _bieu01cp2Service.Queryable().Where(x => x.NAM_BC == para.NAM_BC && x.KY_BC == para.KY_BC && para.lst_DVQHNS.Contains(x.MA_QHNS)).Select(y => y.REFID).ToArray();
                List<PHB_BIEU01CP2_DETAIL> result = _bieu01cp2DetailService.Queryable().Where(x => refid.Contains(x.PHB_BIEU01CP2_REFID)).ToList();
                string[] lstDVQHNS = _bieu01cp2Service.Queryable().Where(x => x.NAM_BC == para.NAM_BC && x.KY_BC == para.KY_BC && para.lst_DVQHNS.Contains(x.MA_QHNS)).Select(y => y.TEN_QHNS).ToArray();
                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        _BindingDataToExcelDauRa(workSheet, result, para, lstDVQHNS);
                        FileStream stream = new FileStream(filename, FileMode.Create);
                        excelPackage.SaveAs(stream);
                        stream.Close();
                    }
                }
            }
            else
            {
                filename = "";
            }
            return filename;
        }
        public void _BindingDataToExcelDauRa(ExcelWorksheet ws, List<PHB_BIEU01CP2_DETAIL> result, BAOCAO_PARAM para,string[] lstDVQHNS)
        {
            var startRow = 13;
            var startCol = 11;

            var ndcTemp_TX = result.Where(x => x.LOAI == 1).Select(x => x.NOI_DUNG_CHI).ToList();
            var ndcTemp_KTX = result.Where(x => x.LOAI == 2).Select(x => x.NOI_DUNG_CHI).ToList();

            ws.Cells[11, 5].Value = "Tổng số";
            ws.Cells[11, 6].Value = result.Sum(y => y.TS_CHENHLECH);
            ws.Cells[11, 7].Value = result.Sum(y => y.NSTN_CHENHLECH);
            ws.Cells[11, 8].Value = result.Sum(y => y.VT_CHENHLECH);
            ws.Cells[11, 9].Value = result.Sum(y => y.VNNN_CHENHLECH);
            ws.Cells[11, 10].Value = result.Sum(y => y.PDKTL_CHENHLECH);
            ws.Cells[11, 11].Value = result.Sum(y => y.NHDKDL_CHENHLECH);
            ws.Cells[12, 5].Value = "I. Kinh phí thường xuyên/tư chủ";
            for (int i = 0; i < ndcTemp_TX.Count; i++)
            {
                ws.Cells[startRow + i, 1].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.LOAI == 1).MA_LOAI;
                ws.Cells[startRow + i, 2].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.LOAI == 1).MA_KHOAN;
                ws.Cells[startRow + i, 3].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.LOAI == 1).MA_MUC;
                ws.Cells[startRow + i, 4].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.LOAI == 1).MA_TIEU_MUC;
                ws.Cells[startRow + i, 5].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.LOAI == 1).NOI_DUNG_CHI;
                ws.Cells[startRow + i, 6].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.TS_CHENHLECH);
                ws.Cells[startRow + i, 7].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.NSTN_CHENHLECH);
                ws.Cells[startRow + i, 8].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.VT_CHENHLECH);
                ws.Cells[startRow + i, 9].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.VNNN_CHENHLECH);
                ws.Cells[startRow + i, 10].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.PDKTL_CHENHLECH);
                ws.Cells[startRow + i, 11].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_TX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 1).Sum(y => y.NHDKDL_CHENHLECH);
            }
            startRow = startRow + ndcTemp_TX.Count;
            ws.Cells[startRow, 5].Value = "II. Kinh phí không thường xuyên/không tự chủ";
            startRow = startRow + 1;
            for (int i = 0; i < ndcTemp_KTX.Count; i++)
            {
                ws.Cells[startRow  + i, 1].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.LOAI == 2).MA_LOAI;
                ws.Cells[startRow + i, 2].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.LOAI == 2).MA_KHOAN;
                ws.Cells[startRow  + i, 3].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.LOAI == 2).MA_MUC;
                ws.Cells[startRow  + i, 4].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.LOAI == 2).MA_TIEU_MUC;
                ws.Cells[startRow  + i, 5].Value = result.FirstOrDefault(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.LOAI == 2).NOI_DUNG_CHI;
                ws.Cells[startRow  + i, 6].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.TS_CHENHLECH);
                ws.Cells[startRow  + i, 7].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.NSTN_CHENHLECH);
                ws.Cells[startRow  + i, 8].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.VT_CHENHLECH);
                ws.Cells[startRow  + i, 9].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.VNNN_CHENHLECH);
                ws.Cells[startRow  + i, 10].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.PDKTL_CHENHLECH);
                ws.Cells[startRow + i, 11].Value = result.Where(x => x.NOI_DUNG_CHI == ndcTemp_KTX[i] && x.MA_LOAI == ws.Cells[startRow + i, 1].Value.ToString() && x.MA_KHOAN == ws.Cells[startRow + i, 2].Value.ToString() && x.MA_MUC == ws.Cells[startRow + i, 3].Value.ToString() && x.MA_TIEU_MUC == ws.Cells[startRow + i, 4].Value.ToString() && x.LOAI == 2).Sum(y => y.NHDKDL_CHENHLECH);
            }

            ws.Cells[1, 1].Value = "Mã Chương: " + para.MA_CHUONG;
            ws.Cells[2, 1].Value = "Mã Đơn Vị Báo Cáo: " + para.lst_DVQHNS;
            ws.Cells[3, 1].Value = "Tên Đơn Vị Báo Cáo: " + para.TEN_DVQHNS;
            
            ws.SelectedRange[11, 1, 11 + ndcTemp_TX.Count + ndcTemp_KTX.Count + 2, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, 11 + ndcTemp_TX.Count + ndcTemp_KTX.Count + 2, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, 11 + ndcTemp_TX.Count + ndcTemp_KTX.Count + 2, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, 11 + ndcTemp_TX.Count + ndcTemp_KTX.Count + 2, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
          
        }
    }
}
