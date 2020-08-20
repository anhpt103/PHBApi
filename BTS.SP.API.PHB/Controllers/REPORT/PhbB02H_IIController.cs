using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.B02H_II;
using BTS.SP.PHB.SERVICE.Models.B02H_II;
using BTS.SP.PHB.SERVICE.REPORT.B02H_II;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
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

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB02H_II")]
    [Route("{id?}")]
    public class PhbB02H_IIController:ApiController
    {
        private readonly IB02H_IIService _B02H_IIService;
        private readonly IB02H_IIDetailService _B02H_IIDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB02H_IIController(IB02H_IIService B02H_IIService, IB02H_IIDetailService B02H_IIDetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _B02H_IIService = B02H_IIService;
            _B02H_IIDetailService = B02H_IIDetailService;
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
                            PHB_B02H_II bieuF01_02 = new PHB_B02H_II()
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

                            var checkReport = await _B02H_IIService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieuF01_02.MA_CHUONG) && x.MA_QHNS.Equals(bieuF01_02.MA_QHNS) &&
                                x.NAM_BC == bieuF01_02.NAM_BC && x.KY_BC == bieuF01_02.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _B02H_IIService.Insert(bieuF01_02);

                            string noidungchi = string.Empty;
                            int startRow = 12;
                            var loai = "";
                            var khoan = "";
                            var muc = "";
                            while (workSheet.Cells[startRow, 5].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 5].Value.ToString()))
                            {
                                PHB_B02H_II_DETAIL detail = new PHB_B02H_II_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_B02H_II_REFID = bieuF01_02.REFID,
                                    };
                                if (!string.IsNullOrEmpty(workSheet.Cells[startRow, 1].Text))
                                {
                                    loai = workSheet.Cells[startRow, 1].Value.ToString().Trim();
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Text))
                                {
                                    khoan = workSheet.Cells[startRow, 2].Value.ToString().Trim();
                                }
                                if (!string.IsNullOrEmpty(workSheet.Cells[startRow, 3].Text))
                                {
                                    muc = workSheet.Cells[startRow, 3].Value.ToString().Trim();
                                }
                                detail.MA_LOAI = loai;
                                detail.MA_KHOAN = khoan;
                                detail.MA_MUC = muc;
                                detail.MA_TIEU_MUC = workSheet.Cells[startRow, 4].Value != null
                                    ? workSheet.Cells[startRow, 4].Value.ToString()
                                    : null;
                                detail.NOI_DUNG_CHI = workSheet.Cells[startRow, 5].Value != null
                                    ? workSheet.Cells[startRow, 5].Value.ToString()
                                    : null;
                                if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.TONG_SO = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !"  });
                                        }
                                    }
                                    else
                                    {
                                        detail.TONG_SO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_TONG_SO = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !" });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_TONG_SO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_NSNN_GIAO = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !" });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_NSNN_GIAO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 10].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_PHI_LEPHI = double.Parse(workSheet.Cells[startRow, 10].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !" });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_PHI_LEPHI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 11].Value != null)
                                    {
                                        try
                                        {
                                            detail.NSNN_VIEN_TRO = double.Parse(workSheet.Cells[startRow, 11].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !" });
                                        }
                                    }
                                    else
                                    {
                                        detail.NSNN_VIEN_TRO = 0;
                                    }
                                    if (workSheet.Cells[startRow, 12].Value != null)
                                    {
                                        try
                                        {
                                            detail.NGUON_KHAC = double.Parse(workSheet.Cells[startRow, 12].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = "Lỗi số liệu trong file Excel !" });
                                        }
                                    }
                                    else
                                    {
                                        detail.NGUON_KHAC = 0;
                                    }
                                    
                                    _B02H_IIDetailService.Insert(detail);
                                    startRow += 1;
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
            Response<B02H_IIVm.ViewModel> response = new Response<B02H_IIVm.ViewModel>();
            try
            {
                B02H_IIVm.ViewModel data = new B02H_IIVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _B02H_IIDetailService.Queryable().Where(x => x.PHB_B02H_II_REFID.Equals(refid))
                    .OrderBy(x => x.MA_SO).ThenBy(x => x.MA_LOAI).ThenBy(x => x.MA_KHOAN).ThenBy(x => x.MA_MUC).ThenBy(x => x.MA_TIEU_MUC)
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
        public async Task<IHttpActionResult> Post(B02H_IIVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_B02H_II B02H_II = new PHB_B02H_II()
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

                var checkReport = await _B02H_IIService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(B02H_II.MA_CHUONG) && x.MA_QHNS.Equals(B02H_II.MA_QHNS) &&
                    x.NAM_BC == B02H_II.NAM_BC && x.KY_BC == B02H_II.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _B02H_IIService.Insert(B02H_II);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_B02H_II_REFID = B02H_II.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _B02H_IIDetailService.Insert(detail);
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
            var response = await _B02H_IIService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(B02H_IIVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            Response<string> response = new Response<string>();
            bool hasValue = false;
            try
            {
                PHB_B02H_II B02H_II = await _B02H_IIService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (B02H_II == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
                #region Delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        PHB_B02H_II_DETAIL item = await _B02H_IIDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _B02H_IIDetailService.Delete(item);
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
                        item.PHB_B02H_II_REFID = model.REFID;
                        _B02H_IIDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        PHB_B02H_II_DETAIL detail = await _B02H_IIDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.TONG_SO = item.TONG_SO;
                            detail.NSNN_TONG_SO = item.NSNN_TONG_SO;
                            detail.NSNN_NSNN_GIAO = item.NSNN_NSNN_GIAO;
                            detail.NSNN_PHI_LEPHI = item.NSNN_PHI_LEPHI;
                            detail.NSNN_VIEN_TRO = item.NSNN_VIEN_TRO;
                            detail.NGUON_KHAC = item.NGUON_KHAC;
                            _B02H_IIDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    B02H_II.NGAY_SUA = DateTime.Now;
                    B02H_II.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _B02H_IIService.Update(B02H_II);

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
            var data = await _B02H_IIService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
            {
                var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/B02H_II/Template.xlsx");
                var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/B02H_II/" +
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
                                    sheet.Cells[startRowInsert + i, 1].Value = lst[i].MA_SO;
                                    sheet.Cells[startRowInsert + i, 2].Value = lst[i].MA_KHOAN;
                                    sheet.Cells[startRowInsert + i, 3].Value = lst[i].MA_MUC;
                                    sheet.Cells[startRowInsert + i, 4].Value = lst[i].MA_TIEU_MUC;
                                    sheet.Cells[startRowInsert + i, 5].Value = lst[i].NOI_DUNG_CHI;
                                    //sheet.Cells[startRowInsert + i, 6].Value = lst[i].TS_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 7].Value = lst[i].TS_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 8].Value = lst[i].TS_SOXDTD - lst[i].TS_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 9].Value = lst[i].NSTN_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 10].Value = lst[i].NSTN_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 11].Value = lst[i].NSTN_SOXDTD - lst[i].NSTN_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 12].Value = lst[i].VT_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 13].Value = lst[i].VT_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 14].Value = lst[i].VT_SOXDTD - lst[i].VT_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 15].Value = lst[i].VNNN_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 16].Value = lst[i].VNNN_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 17].Value = lst[i].VNNN_SOXDTD - lst[i].VNNN_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 18].Value = lst[i].PDKTL_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 19].Value = lst[i].PDKTL_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 20].Value = lst[i].PDKTL_SOXDTD - lst[i].PDKTL_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 21].Value = lst[i].NHDKDL_SOBAOCAO;
                                    //sheet.Cells[startRowInsert + i, 22].Value = lst[i].NHDKDL_SOXDTD;
                                    //sheet.Cells[startRowInsert + i, 23].Value = lst[i].NHDKDL_SOXDTD - lst[i].NHDKDL_SOBAOCAO;
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
                            FileName = "export_B02H_II.xlsx"
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