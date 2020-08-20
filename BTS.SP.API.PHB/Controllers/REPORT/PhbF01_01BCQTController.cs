using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_01;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.Models.F01_01BCQT;
using BTS.SP.PHB.SERVICE.REPORT.F01_01BCQT;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbF01_01BCQT")]
    [Route("{id?}")]
    public class PhbF01_01BCQTController : ApiController
    {
        private readonly IF01_01BCQTService _f0101BcqtService;
        private readonly IF01_01BCQTDetailService _f0101BcqtDetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDmChuongService _dmChuong;

        public PhbF01_01BCQTController(IF01_01BCQTService f0101BcqtService, IF01_01BCQTDetailService f0101BcqtDetailService, IUnitOfWorkAsync unitOfWorkAsync, IDmChuongService dmChuong)
        {
            _f0101BcqtService = f0101BcqtService;
            _f0101BcqtDetailService = f0101BcqtDetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
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
                        PHB_F01_01BCQT bieuF01_01 = new PHB_F01_01BCQT()
                        {
                            NGAY_TAO = DateTime.Now,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            ObjectState = ObjectState.Added,
                            TRANG_THAI = 0,
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
                            bieuF01_01.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieuF01_01.MA_QHNS = httpRequest["MA_QHNS"];
                            bieuF01_01.TEN_QHNS = httpRequest["TEN_QHNS"];
                            bieuF01_01.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieuF01_01.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieuF01_01.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _f0101BcqtService.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieuF01_01.MA_CHUONG) && x.MA_QHNS.Equals(bieuF01_01.MA_QHNS) &&
                                x.NAM_BC == bieuF01_01.NAM_BC && x.KY_BC == bieuF01_01.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _f0101BcqtService.Insert(bieuF01_01);

                            string noidungchi = string.Empty;
                            int startRow = 14;
                            var loai = 1;
                            while (!string.IsNullOrEmpty(workSheet.Cells[startRow, 5].Value.ToString()) && workSheet.Cells[startRow, 5].Value.ToString() != "TỔNG CỘNG")
                            {
                                {
                                    //chi tieu danh mục
                                    noidungchi = workSheet.Cells[startRow, 5].Value.ToString();
                                    PHB_F01_01BCQT_DETAIL detail = new PHB_F01_01BCQT_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_F01_01BCQT_REFID = bieuF01_01.REFID,
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
                                    if (workSheet.Cells[startRow, 5].Value.ToString() != "TỔNG CỘNG")
                                    {
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
                                                detail.TONG_SO = decimal.Parse(workSheet.Cells[startRow, 6].Value.ToString());

                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.TONG_SO = 0;
                                        }
                                        if (workSheet.Cells[startRow, 7].Value != null)
                                        {
                                            try
                                            {
                                                detail.NSNN_TRONGNUOC = decimal.Parse(workSheet.Cells[startRow, 7].Value.ToString());

                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.NSNN_TRONGNUOC = 0;
                                        }
                                        if (workSheet.Cells[startRow, 8].Value != null)
                                        {
                                            try
                                            {
                                                detail.VIEN_TRO = decimal.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.VIEN_TRO = 0;
                                        }
                                        if (workSheet.Cells[startRow, 9].Value != null)
                                        {
                                            try
                                            {
                                                detail.VAYNO_NUOCNGOAI = decimal.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.VAYNO_NUOCNGOAI = 0;
                                        }
                                        if (workSheet.Cells[startRow, 10].Value != null)
                                        {
                                            try
                                            {
                                                detail.NP_DELAI = decimal.Parse(workSheet.Cells[startRow, 10].Value.ToString());
                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.NP_DELAI = 0;
                                        }
                                        if (workSheet.Cells[startRow, 11].Value != null)
                                        {
                                            try
                                            {
                                                detail.NHD_DELAI = decimal.Parse(workSheet.Cells[startRow, 11].Value.ToString());

                                            }
                                            catch (Exception ex)
                                            {
                                                WriteLogs.LogError(ex);
                                                return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                            }
                                        }
                                        else
                                        {
                                            detail.NHD_DELAI = 0;
                                        }

                                        _f0101BcqtDetailService.Insert(detail);
                                        startRow += 1;
                                    }
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
            Response<F01_01BCQTVm.ViewModel> response = new Response<F01_01BCQTVm.ViewModel>();
            try
            {
                F01_01BCQTVm.ViewModel data = new F01_01BCQTVm.ViewModel();
                data.REFID = refid;
                data.DETAIL = await _f0101BcqtDetailService.Queryable().Where(x => x.PHB_F01_01BCQT_REFID.Equals(refid))
                    .OrderBy(x => x.LOAI).ThenBy(x => x.MA_LOAI).ThenBy(x => x.MA_KHOAN).ThenBy(x => x.MA_MUC).ThenBy(x => x.MA_TIEU_MUC)
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
        public async Task<IHttpActionResult> Post(F01_01BCQTVm.InsertModel model)
        {
            if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
                model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
            Response<string> response = new Response<string>();
            try
            {
                PHB_F01_01BCQT f0101Bcqt = new PHB_F01_01BCQT()
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

                var checkReport = await _f0101BcqtService.Queryable().FirstOrDefaultAsync(x =>
                    x.MA_CHUONG.Equals(f0101Bcqt.MA_CHUONG) && x.MA_QHNS.Equals(f0101Bcqt.MA_QHNS) &&
                    x.NAM_BC == f0101Bcqt.NAM_BC && x.KY_BC == f0101Bcqt.KY_BC);
                if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                _f0101BcqtService.Insert(f0101Bcqt);
                foreach (var detail in model.DETAIL)
                {
                    detail.PHB_F01_01BCQT_REFID = f0101Bcqt.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _f0101BcqtDetailService.Insert(detail);
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

        [HttpPut]
        public async Task<IHttpActionResult> Put(F01_01BCQTVm.EditModel model)
        {
            if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
            var response = new Response<string>();
            var hasValue = false;
            try
            {
                var f0101Bcqt = await _f0101BcqtService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
                if (f0101Bcqt == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });

                #region delete
                if (model.LstDelete != null && model.LstDelete.Count > 0)
                {
                    hasValue = true;
                    foreach (var itemId in model.LstDelete)
                    {
                        var item = await _f0101BcqtDetailService.FindByIdAsync(itemId);
                        if (item != null)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _f0101BcqtDetailService.Delete(item);
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
                        item.PHB_F01_01BCQT_REFID = model.REFID;
                        _f0101BcqtDetailService.Insert(item);
                    }
                }
                #endregion

                #region Edit
                if (model.LstEdit != null && model.LstEdit.Count > 0)
                {
                    hasValue = true;
                    foreach (var item in model.LstEdit)
                    {
                        var detail = await _f0101BcqtDetailService.FindByIdAsync(item.ID);
                        if (detail != null)
                        {
                            detail.ObjectState = ObjectState.Modified;
                            detail.MA_LOAI = item.MA_LOAI;
                            detail.MA_KHOAN = item.MA_KHOAN;
                            detail.MA_MUC = item.MA_MUC;
                            detail.MA_TIEU_MUC = item.MA_TIEU_MUC;
                            detail.NOI_DUNG_CHI = item.NOI_DUNG_CHI;
                            detail.NHD_DELAI = item.NHD_DELAI;
                            detail.NP_DELAI = item.NP_DELAI;
                            detail.NSNN_TRONGNUOC = item.NSNN_TRONGNUOC;
                            detail.VAYNO_NUOCNGOAI = item.VAYNO_NUOCNGOAI;
                            detail.VIEN_TRO = item.VIEN_TRO;
                            detail.TONG_SO = item.TONG_SO;
                            _f0101BcqtDetailService.Update(detail);
                        }
                    }
                }
                #endregion

                if (hasValue)
                {
                    f0101Bcqt.NGAY_SUA = DateTime.Now;
                    f0101Bcqt.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    _f0101BcqtService.Update(f0101Bcqt);

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
            var data = await _f0101BcqtService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            if (data != null && !data.Error && (data.Data.DETAIL.Count > 0 || data.Data.DETAIL.Count > 0))
            {
                string urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEUF01-01BCQT/Template.xlsx");
                string exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEUF01-01BCQT/" + RequestContext.Principal.Identity.Name
                                                                                     + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
                try
                {
                    using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
                    {
                        ExcelWorksheet sheet = excelPackage.Workbook.Worksheets[1];
                        sheet.Cells[1, 1].Value = sheet.Cells[1, 1].Value + " " + rqmodel.MA_CHUONG;
                        sheet.Cells[2, 1].Value = sheet.Cells[2, 1].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
                        sheet.Cells[3, 1].Value = sheet.Cells[3, 1].Value + " " + rqmodel.DSDVQHNS;
                        sheet.Cells[6, 1].Value = sheet.Cells[6, 1].Value + " " + rqmodel.NAM_BC;
                        if (data.Data.DETAIL.Count > 0)
                        {
                            var loai1 = data.Data.DETAIL.Where(x => x.LOAI == 1).ToList();
                            var loai2 = data.Data.DETAIL.Where(x => x.LOAI == 2).ToList();

                            int loai2StartRow = 14 + loai1.Count;
                            int loai2_Row = loai2StartRow + 1;
                            int totalRow = 14 + loai1.Count + loai2.Count + 1;
                            decimal tongso = 0;
                            decimal nsnn = 0;
                            decimal vientro = 0;
                            decimal vayno = 0;
                            decimal np_delai = 0;
                            decimal nhd_delai = 0;

                            for (int i = 0; i < loai1.Count; i++)
                            {
                                sheet.Cells[14 + i, 1].Value = loai1[i].MA_LOAI;
                                sheet.Cells[14 + i, 2].Value = loai1[i].MA_KHOAN;
                                sheet.Cells[14 + i, 3].Value = loai1[i].MA_MUC;
                                sheet.Cells[14 + i, 4].Value = loai1[i].MA_TIEU_MUC;
                                sheet.Cells[14 + i, 5].Value = loai1[i].NOI_DUNG_CHI;
                                sheet.Cells[14 + i, 6].Value = loai1[i].TONG_SO;
                                sheet.Cells[14 + i, 7].Value = loai1[i].NSNN_TRONGNUOC;
                                sheet.Cells[14 + i, 8].Value = loai1[i].VIEN_TRO;
                                sheet.Cells[14 + i, 9].Value = loai1[i].VAYNO_NUOCNGOAI;
                                sheet.Cells[14 + i, 10].Value = loai1[i].NP_DELAI;
                                sheet.Cells[14 + i, 11].Value = loai1[i].NHD_DELAI;

                                if (loai1[1].TONG_SO > 0) { tongso += loai1[1].TONG_SO; }
                                if (loai1[1].NSNN_TRONGNUOC > 0) { nsnn += loai1[1].NSNN_TRONGNUOC; }
                                if (loai1[1].VIEN_TRO > 0) { vientro += loai1[1].VIEN_TRO; }
                                if (loai1[1].VAYNO_NUOCNGOAI > 0) { vayno += loai1[1].VAYNO_NUOCNGOAI; }
                                if (loai1[1].NP_DELAI > 0) { np_delai += loai1[1].NP_DELAI; }
                                if (loai1[1].NHD_DELAI > 0) { nhd_delai += loai1[1].NHD_DELAI; }
                            }

                            sheet.Cells[loai2StartRow, 5].Value = "II. Kinh phí không thường xuyên/không tự chủ";
                            sheet.Cells[loai2StartRow, 5].Style.Font.Bold = true;


                            for (int i = 0; i < loai1.Count; i++)
                            {
                                sheet.Cells[loai2_Row + i, 1].Value = loai2[i].MA_LOAI;
                                sheet.Cells[loai2_Row + i, 2].Value = loai2[i].MA_KHOAN;
                                sheet.Cells[loai2_Row + i, 3].Value = loai2[i].MA_MUC;
                                sheet.Cells[loai2_Row + i, 4].Value = loai2[i].MA_TIEU_MUC;
                                sheet.Cells[loai2_Row + i, 5].Value = loai2[i].NOI_DUNG_CHI;
                                sheet.Cells[loai2_Row + i, 6].Value = loai2[i].TONG_SO;
                                sheet.Cells[loai2_Row + i, 7].Value = loai2[i].NSNN_TRONGNUOC;
                                sheet.Cells[loai2_Row + i, 8].Value = loai2[i].VIEN_TRO;
                                sheet.Cells[loai2_Row + i, 9].Value = loai2[i].VAYNO_NUOCNGOAI;
                                sheet.Cells[loai2_Row + i, 10].Value = loai2[i].NP_DELAI;
                                sheet.Cells[loai2_Row + i, 11].Value = loai2[i].NHD_DELAI;

                                if (loai2[1].TONG_SO > 0) { tongso += loai2[1].TONG_SO; }
                                if (loai2[1].NSNN_TRONGNUOC > 0) { nsnn += loai2[1].NSNN_TRONGNUOC; }
                                if (loai2[1].VIEN_TRO > 0) { vientro += loai2[1].VIEN_TRO; }
                                if (loai2[1].VAYNO_NUOCNGOAI > 0) { vayno += loai2[1].VAYNO_NUOCNGOAI; }
                                if (loai2[1].NP_DELAI > 0) { np_delai += loai2[1].NP_DELAI; }
                                if (loai2[1].NHD_DELAI > 0) { nhd_delai += loai2[1].NHD_DELAI; }
                            }

                            sheet.Cells[totalRow, 5].Value = "TỔNG SỐ";
                            sheet.Cells[totalRow, 5].Style.Font.Bold = true;
                            sheet.Cells[totalRow, 6].Value = tongso;
                            sheet.Cells[totalRow, 7].Value = nsnn;
                            sheet.Cells[totalRow, 8].Value = vientro;
                            sheet.Cells[totalRow, 9].Value = vayno;
                            sheet.Cells[totalRow, 10].Value = np_delai;
                            sheet.Cells[totalRow, 11].Value = nhd_delai;

                            sheet.Cells[14, 1, totalRow, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[14, 1, totalRow, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[14, 1, totalRow, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[14, 1, totalRow, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
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

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> Sumreport(ReportRqModel rqmodel)
        {
            var response = await _f0101BcqtService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

        public class ExportParams
        {
            public string MA_CTMTQG { get; set; }
            public string MA_KBNN { get; set; }
            public string MA_DBHC { get; set; }
            public string MA_CAP { get; set; }
            public string MA_DVQHNS { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public DateTime TUNGAY_HIEULUC { get; set; }
            public DateTime DENNGAY_HIEULUC { get; set; }
            public DateTime TUNGAY_KETSO { get; set; }
            public DateTime DENNGAY_KETSO { get; set; }
            public string LST_MA_DVQHNS { get; set; }
        }

        public class ResultItems
        {
            public string MA_DIABAN { get; set; }
            public string MA_CAP { get; set; }
            public string MA_CHUONG { get; set; }
            public string MA_LOAI { get; set; }
            public string MA_NGANHKT { get; set; }
            public string MA_MUC { get; set; }
            public string MA_TIEUMUC { get; set; }
            public string TEN_TIEUMUC { get; set; }
            public string MA_DVQHNS { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
            public string TUCHU { get; set; }
        }

        public class Result1CItems
        {
            public string MA_CHITIEU { get; set; }
            public string SAPXEP { get; set; }
            public string TEN_CHITIEU { get; set; }
            public string STT { get; set; }
            public string CONGTHUC_WHERE { get; set; }
            public int INDAM { get; set; }
            public int INNGHIENG { get; set; }
            public decimal GIA_TRI_HACH_TOAN { get; set; }
        }

        [Route("Export1C")]
        public HttpResponseMessage Export1C(ExportParams para)
        {
            HttpResponseMessage result = null;
            string file = null;

            try
            {
                file = CreateExcelFile(para);
                if (file == "OverLoad")
                {
                    result = Request.CreateResponse(HttpStatusCode.NoContent);
                    result.Content = new StringContent("OverLoad");
                    return result;
                }
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
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return result;
        }

        public string CreateExcelFile(ExportParams para)
        {
            var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
            var unit = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Country);
            DateTime now = DateTime.Now;
            string Date = now.ToString("dd-MM-yyyy");
            var fileNameInput = "f01-01.xlsx";
            string folderServer = @"\Template\";
            string filePathResult = HttpContext.Current.Server.MapPath(folderServer);
            if (!Directory.Exists(filePathResult))
            {
                Directory.CreateDirectory(filePathResult);
            }
            string resourceTemplate = HttpContext.Current.Server.MapPath(folderServer + "/F01_01BCQT/");
            if (!Directory.Exists(resourceTemplate))
            {
                Directory.CreateDirectory(resourceTemplate);
            }
            string filePathSource = resourceTemplate + fileNameInput;
            var urlFile = "C:/ExportOutPut/";
            var filename = urlFile + "BaoCao" + "_(" + Date + ")_TM" + now.Ticks + ".xls";
            if (System.IO.File.Exists(filePathSource))
            {
                List<ResultItems> items = new List<ResultItems>();
                OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString);
                connection.Open();
                OracleCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "BTSTC.PHA_BAOCAO_CHITIETKINHPHIQT";
                command.Parameters.Clear();
                command.Parameters.Add("MA_CAP", OracleDbType.NVarchar2, 100).Value = para.MA_CAP;
                command.Parameters.Add("MA_CHUONG", OracleDbType.NVarchar2, 100).Value = para.MA_CHUONG;
                command.Parameters.Add("MA_LOAI", OracleDbType.NVarchar2, 100).Value = para.MA_LOAI;
                command.Parameters.Add("MA_NGANHKT", OracleDbType.NVarchar2, 100).Value = para.MA_NGANHKT;
                command.Parameters.Add("MA_MUC", OracleDbType.NVarchar2, 100).Value = para.MA_MUC;
                command.Parameters.Add("MA_TIEUMUC", OracleDbType.NVarchar2, 100).Value = para.MA_TIEUMUC;
                command.Parameters.Add("MA_DVQHNS", OracleDbType.NVarchar2, 100).Value = para.MA_DVQHNS;
                command.Parameters.Add("MA_DBHC", OracleDbType.NVarchar2, 100).Value = para.MA_DBHC;
                command.Parameters.Add("TUNGAY_HIEULUC", OracleDbType.Date).Value = para.TUNGAY_HIEULUC;
                command.Parameters.Add("DENNGAY_HIEULUC", OracleDbType.Date).Value = para.DENNGAY_HIEULUC;
                command.Parameters.Add("TUNGAY_KETSO", OracleDbType.Date).Value = para.TUNGAY_KETSO;
                command.Parameters.Add("DENNGAY_KETSO", OracleDbType.Date).Value = para.DENNGAY_KETSO;
                command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                OracleDataReader reader = ((OracleRefCursor)command.Parameters["cur"].Value).GetDataReader();
                while (reader.Read())
                {
                    var item = new ResultItems();
                    item.MA_DIABAN = reader["MA_DIABAN"].ToString();
                    item.MA_CAP = reader["MA_CAP"].ToString();
                    item.MA_CHUONG = reader["MA_CHUONG"].ToString();
                    item.MA_LOAI = reader["MA_LOAI"].ToString();
                    item.MA_NGANHKT = reader["MA_NGANHKT"].ToString();
                    item.MA_MUC = reader["MA_MUC"].ToString();
                    item.MA_TIEUMUC = reader["MA_TIEUMUC"].ToString();
                    item.TEN_TIEUMUC = reader["TEN_TIEUMUC"].ToString();
                    item.MA_DVQHNS = reader["MA_DVQHNS"].ToString();
                    item.TUCHU = reader["TUCHU"].ToString();
                    if (reader["GIA_TRI_HACH_TOAN"].ToString() != "")
                    {
                        item.GIA_TRI_HACH_TOAN = decimal.Parse(reader["GIA_TRI_HACH_TOAN"].ToString());
                    }
                    //if (item.MA_DIABAN == unit.Value)
                    //{
                    items.Add(item);
                    //}

                }
                connection.Close();

                if (items.Count > 40000)
                {
                    return "OverLoad";
                }

                using (var excelPackage = new ExcelPackage())
                {
                    using (FileStream filetemplate = new FileStream(filePathSource, FileMode.Open))
                    {
                        excelPackage.Load(filetemplate);
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        BindingDataToExcel(workSheet, items, para);
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

        private void BindingDataToExcel(ExcelWorksheet ws, List<ResultItems> items, ExportParams para)
        {

            var lst = items.Where(x => x.TUCHU == "1").ToList();
            var lst2 = items.Where(x => x.TUCHU == "2").ToList();
            var startRow = 13;

            var loaiS = lst.Select(x => x.MA_LOAI).Distinct().ToList();
            //var dvqhnsS2 = lst2.Select(x => x.MA_DVQHNS).Distinct().ToList();
            var loaiS2 = lst2.Select(x => x.MA_LOAI).Distinct().ToList();


            ws.Cells[11, 5].Value = "I. Kinh phí thường xuyên/tự chủ";
            ws.Cells[11, 5].Style.Font.Bold = true;
            ws.Cells[11, 6].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[11, 6].Style.Numberformat.Format = "###,###,###,###,###";
            ws.Cells[11, 6].Style.Font.Bold = true;
            ws.Cells[11, 7].Value = lst.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[11, 7].Style.Numberformat.Format = "###,###,###,###,###";
            ws.Cells[11, 7].Style.Font.Bold = true;

            for (int i = 0; i < loaiS.Count; i++)
            {
                var l = loaiS[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                startRow++;
                var khoanS = lst.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                        startRow++;
                        var tieumucS = lst.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 6].Style.Numberformat.Format = "###,###,###,###,###";
                            ws.Cells[startRow + n, 7].Value = lst.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 7].Style.Numberformat.Format = "###,###,###,###,###";
                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            /////////////////////--KHÔNG TỰ CHỦ--//////////////////////////////////////////////
            ws.Cells[startRow, 5].Value = "II. Kinh phí không thường xuyên/không tự chủ";
            ws.Cells[startRow, 5].Style.Font.Bold = true;
            ws.Cells[startRow, 6].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
            ws.Cells[startRow, 6].Style.Font.Bold = true;
            ws.Cells[startRow, 7].Value = lst2.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
            ws.Cells[startRow, 7].Style.Font.Bold = true;
            startRow++;
            for (int i = 0; i < loaiS2.Count; i++)
            {
                var l = loaiS2[i];
                ws.Cells[startRow, 1].Value = l;
                ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l).Sum(y => y.GIA_TRI_HACH_TOAN);
                ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                startRow++;
                var khoanS = lst2.Where(y => y.MA_LOAI == l).OrderBy(o => o.MA_NGANHKT).Select(x => x.MA_NGANHKT).Distinct().ToList();
                for (int j = 0; j < khoanS.Count; j++)
                {
                    var k = khoanS[j];
                    ws.Cells[startRow, 2].Value = k;
                    ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                    ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k).Sum(y => y.GIA_TRI_HACH_TOAN);
                    ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                    startRow++;
                    var mucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k).OrderBy(o => o.MA_MUC).Select(x => x.MA_MUC).Distinct().ToList();
                    for (int p = 0; p < mucS.Count; p++)
                    {
                        var m = mucS[p];
                        ws.Cells[startRow, 3].Value = m;
                        ws.Cells[startRow, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
                        ws.Cells[startRow, 7].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m).Sum(y => y.GIA_TRI_HACH_TOAN);
                        ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
                        startRow++;
                        var tieumucS = lst2.Where(y => y.MA_LOAI == l && y.MA_NGANHKT == k && y.MA_MUC == m).OrderBy(o => o.MA_TIEUMUC).Select(x => x.MA_TIEUMUC).Distinct().ToList();
                        for (int n = 0; n < tieumucS.Count; n++)
                        {
                            var t = tieumucS[n];
                            var col = 6;
                            if (t == "8954")
                            {
                                col = 16;
                            }
                            if (t == "8955")
                            {
                                col = 13;
                            }
                            ws.Cells[startRow + n, 4].Value = t;
                            var firstOrDefault = lst2.FirstOrDefault(x => x.MA_TIEUMUC == t);
                            if (firstOrDefault != null)
                                ws.Cells[startRow + n, 5].Value = firstOrDefault.TEN_TIEUMUC;
                            ws.Cells[startRow + n, 6].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, 6].Style.Numberformat.Format = "###,###,###,###,###";
                            ws.Cells[startRow + n, col].Value = lst2.Where(x => x.MA_LOAI == l && x.MA_NGANHKT == k && x.MA_MUC == m && x.MA_TIEUMUC == t).Sum(y => y.GIA_TRI_HACH_TOAN);
                            ws.Cells[startRow + n, col].Style.Numberformat.Format = "###,###,###,###,###";

                        }
                        startRow += tieumucS.Count;
                    }
                }
            }
            ws.Cells[startRow, 5].Value = "TỔNG CỘNG";
            ws.Cells[startRow, 5].Style.Font.Bold = true;
            ws.Cells[startRow, 6].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 6].Style.Font.Bold = true;
            ws.Cells[startRow, 6].Style.Numberformat.Format = "###,###,###,###,###";
            ws.Cells[startRow, 7].Value = items.Sum(x => x.GIA_TRI_HACH_TOAN);
            ws.Cells[startRow, 7].Style.Font.Bold = true;
            ws.Cells[startRow, 7].Style.Numberformat.Format = "###,###,###,###,###";
            ws.SelectedRange[11, 1, startRow, 11].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow, 11].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow, 11].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.SelectedRange[11, 1, startRow, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;



            var dk = "Điều kiện lọc:";
            if (!string.IsNullOrEmpty(para.MA_CAP))
            {
                dk += "Cấp " + para.MA_CAP + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_CHUONG))
            {
                dk += "Chương " + para.MA_CHUONG + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_LOAI))
            {
                dk += "Loại " + para.MA_LOAI + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_NGANHKT))
            {
                dk += "Khoản " + para.MA_NGANHKT + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_MUC))
            {
                dk += "Mục " + para.MA_MUC + ";";
            }
            if (!string.IsNullOrEmpty(para.MA_TIEUMUC))
            {
                dk += "Tiểu mục " + para.MA_TIEUMUC + ";";
            }
            var DVBAOCAO = _dmChuong.Queryable().FirstOrDefault(x => x.MA_CHUONG == para.MA_CHUONG).TEN_CHUONG;
            ws.Cells[1, 1].Value = dk;
            ws.Cells[2, 1].Value = "Đơn vị báo cáo :" + DVBAOCAO;
            ws.Cells[3, 1].Value = "Từ ngày hiệu lực :" + para.TUNGAY_HIEULUC.ToString("d") + "đến ngày hiệu lực" + para.DENNGAY_HIEULUC.ToString("d");
            ws.Cells[4, 1].Value = "Từ ngày kết sổ :" + para.TUNGAY_KETSO.ToString("d") + "đến ngày kết sổ:" + para.DENNGAY_KETSO.ToString("d");
        }

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            try
            {
                var bc = new PHB_F01_01BCQT
                {
                    NAM_BC = model.ReportHeader.ReportYear,
                    MA_QHNS = model.ReportHeader.CompanyID,
                    MA_CHUONG = model.ReportHeader.BudgetChapterID.ToString(),
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _f0101BcqtService
                    .Queryable()
                    .Count(report => report.MA_QHNS == model.ReportHeader.CompanyID && report.NAM_BC == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _f0101BcqtService.Insert(bc);

                var details = model.F0101BCQTDetail;
                foreach (var t in details)
                {
                    var item = new PHB_F01_01BCQT_DETAIL
                    {
                        PHB_F01_01BCQT_REFID = bc.REFID,
                        NOI_DUNG_CHI = t.BudgetSubItemName,
                        MA_LOAI = t.BudgetKindItemID.ToString(),
                        MA_KHOAN = t.BudgetSubKindItemID.ToString(),
                        MA_MUC = t.BudgetItemID.ToString(),
                        MA_TIEU_MUC = t.BudgetSubItemID.ToString(),
                        NSNN_TRONGNUOC = t.BudgetSourceAmount.GetValueOrDefault(0),
                        VIEN_TRO = t.AidAmount.GetValueOrDefault(0),
                        VAYNO_NUOCNGOAI = t.DebitAmount.GetValueOrDefault(0),
                        NP_DELAI = t.DeductAmount.GetValueOrDefault(0),
                        NHD_DELAI = t.OtherAmount.GetValueOrDefault()

                    };
                    _f0101BcqtDetailService.Insert(item);
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
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("ReceiveDataFromService")]
        [HttpPost]
        public async Task<IHttpActionResult> ReceiveDataFromService(List<F01_01BCQTModel> model)
        {
            var response = new Response<string>();
            if (model == null || model.Count == 0)
            {
                response.Message = "param model Task<IHttpActionResult> ReceiveDataFromService is null or empty";
                return Ok(response);
            }

            using (var context = new PHBContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rpF01_01 in model)
                        {
                            string msg = _f0101BcqtService.IfExistsRpPeriodThenDelete(rpF01_01.ReportHeader.CompanyID, rpF01_01.ReportHeader.ReportPeriod, rpF01_01.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _f0101BcqtService.InsertData(rpF01_01, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        response.Message = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Message = ex.Message;
                        return Ok(response);
                    }
                    finally
                    {
                        transaction.Dispose();
                        context.Dispose();
                    }
                }
            }
            return Ok(response);
        }
    }
}
