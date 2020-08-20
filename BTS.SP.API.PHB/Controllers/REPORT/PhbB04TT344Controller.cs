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
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.SERVICE.Models.BIEU01A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01A;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.PBDT.TT344;
using BTS.SP.PHB.ENTITY.PBDT.TT344;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB04_TT344")]
    [Route("{id?}")]
    public class PhbB04TT344Controller : ApiController
    {
        private readonly IPHB_PBDT_TT344_B04_Service _bieu01AService;
        private readonly IPHB_PBDT_TT344_B04_DETAIL_Service _bieu01ADetailService;
        private readonly IPHB_PBDT_TT344_B04_TEMPLATE_Service _bieu01ATemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB04TT344Controller(IPHB_PBDT_TT344_B04_Service bieu01AService, IPHB_PBDT_TT344_B04_DETAIL_Service bieu01ADetailService, IPHB_PBDT_TT344_B04_TEMPLATE_Service bieu01ATemplateService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bieu01AService = bieu01AService;
            _bieu01ADetailService = bieu01ADetailService;
            _bieu01ATemplateService = bieu01ATemplateService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("UploadReportxa")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            var response = new Response<string>();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                try
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var bieu01A = new PHB_PBDT_TT344_B04()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            //if (string.IsNullOrEmpty(httpRequest["MA_CHUONG"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có mã chương."
                            //});
                            //bieu01A.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_DBHC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu01A.MA_DBHC = httpRequest["MA_DBHC"];
                            //bieu01A.TEN_QHNS = httpRequest["TEN_QHNS"];
                            //bieu01A.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu01A.NAM = int.Parse(httpRequest["NAM_BC"]);
                            //if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có kỳ báo cáo."
                            //});
                            //bieu01A.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bieu01AService.Queryable().FirstOrDefaultAsync(x => x.MA_DBHC.Equals(bieu01A.MA_DBHC) &&
                                x.NAM == bieu01A.NAM);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

                            _bieu01AService.Insert(bieu01A);

                            int start_Row = 13;
                            int end_Row = 66;
                            int start_Col = 3;
                            int count = 1;
                            int sTT_Cha = 0;
                            string currentLama = "";
                            string currentSo = "";
                            string currentSoHai = "";

                            for (int r = start_Row; r <= int.MaxValue; r++)
                            {
                                if( string.IsNullOrEmpty(workSheet.Cells[r, 1].Text) || workSheet.Cells[r, 1].Text.Equals("Ghi chú: (1) Theo phân cấp của cấp tỉnh"))
                                {
                                    break;
                                }
                                else
                                {
                                    var obj = new PHB_PBDT_TT344_B04_DETAIL()
                                    {
                                        PHB_PBDT_TT344_B04_REFID = bieu01A.REFID,
                                        ObjectState = ObjectState.Added,
                                        IS_BOLD = 0,
                                        IS_ITALIC = 0,
                                        IS_OPTIONAL = 0,

                                    };
                                    var tongso = 0;
                                    obj.STT_SAPXEP = count;
                                    obj.CHI_TIEU = workSheet.Cells[r, 1].Text;
                                    obj.THOIGIAN_KCHT = workSheet.Cells[r, 5].Text;

                                    int.TryParse(workSheet.Cells[r, 6].Text.ToString(), out tongso);
                                    obj.TONG_DU_TOAN_TONGSO = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 7].Text.ToString(), out tongso);
                                    obj.TONG_DU_TOAN_DAN_DONG_GOP = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 8].Text.ToString(), out tongso);
                                    obj.GIATRI_THUCHIEN = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 9].Text.ToString(), out tongso);
                                    obj.GIATRI_THANHTOAN = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 10].Text.ToString(), out tongso);
                                    obj.DT_TONGSO = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 11].Text.ToString(), out tongso);
                                    obj.DT_NAMTRUOC = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 12].Text.ToString(), out tongso);
                                    obj.DT_CANDOI_NGANSACH = tongso;
                                    tongso = 0;
                                    int.TryParse(workSheet.Cells[r, 13].Text.ToString(), out tongso);
                                    obj.DT_TONGSO1 = tongso;
                                    tongso = 0;
                                    _bieu01ADetailService.Insert(obj);
                                    count++;
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

        //[Route("GetDetailByRefId/{refid}")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetDetailByRefId(string refid)
        //{
        //    if (string.IsNullOrEmpty(refid)) return BadRequest();
        //    var response = new Response<BIEU01AVm.ViewModel>();
        //    try
        //    {
        //        var data = new BIEU01AVm.ViewModel
        //        {
        //            REFID = refid,
        //            DETAIL = await _bieu01ADetailService.Queryable().Where(x => x.PHB_BIEU01A_REFID.Equals(refid))
        //                .OrderBy(x => x.MA_NOIDUNGKT).ThenBy(x => x.MA_CHI_TIEU).ToListAsync()
        //        };
        //        response.Error = false;
        //        response.Data = data;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}

        //[Route("GetTemplate")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetTemplate()
        //{
        //    var response = new Response<List<PHB_BIEU01A_TEMPLATE>>();
        //    try
        //    {
        //        response.Error = false;
        //        response.Data = await _bieu01ATemplateService.Queryable().OrderBy(x => x.MA_NOIDUNGKT).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}

        //[Route("GetTemplateForEdit/{refid}")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetTemplateForEdit(string refid)
        //{
        //    var response = await _bieu01ATemplateService.GetTemplateForEdit(refid);
        //    return Ok(response);
        //}

        //[Route("SumReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        //{
            
        //    var response = await _bieu01AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    return Ok(response);
        //}

        //[Route("SumAllReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> SumAllReport(ReportRqModel rqmodel)
        //{
        //    var response = await _bieu01AService.SumAllReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC,
        //      string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    return Ok(response);
        //}

        //[Route("ExportReport")]
        //[HttpPost]
        //public async Task<IHttpActionResult> ExportReport(ReportRqModel rqmodel)
        //{
        //    var data = await _bieu01AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET,rqmodel.NAM_BC, rqmodel.KY_BC,
        //        string.Join(",", rqmodel.DSDVQHNS.ToArray()));
        //    if (data != null && !data.Error && data.Data.DETAIL.Count > 0)
        //    {
        //        var urlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Template/BIEU01A/Template.xlsx");
        //        var exportUrlFile = System.Web.Hosting.HostingEnvironment.MapPath("~/Export/BIEU01A/" +
        //            RequestContext.Principal.Identity.Name + "_" + rqmodel.NAM_BC + "_" + rqmodel.KY_BC + "_" + DateTime.Now.Ticks + ".xlsx");
        //        try
        //        {
        //            using (var excelPackage = new ExcelPackage(new FileInfo(urlFile)))
        //            {
        //                var sheet = excelPackage.Workbook.Worksheets[1];
        //                sheet.Cells["A4"].Value = sheet.Cells["A4"].Value + " " + rqmodel.NAM_BC;
        //                sheet.Cells["A5"].Value = sheet.Cells["A5"].Value + " " + string.Join(",", rqmodel.TEN_DSDVQHNS);
        //                string[] lstTenChiTieu = { "- Tổng số", "- Tổng số thu", "- Số phải nộp NSNN", "- Số được khấu trừ để lại" };

        //                var listByMaQhns = data.Data.DETAIL.GroupBy(x => x.MA_QHNS);
        //                var startRowInsert = 10;
        //                foreach (var list in listByMaQhns)
        //                {
        //                    if (list.Key.Equals("0"))
        //                    {
        //                        // dữ liệu tổng hợp
        //                        var lstByLoai = list.ToList().GroupBy(x => x.LOAI);
        //                        foreach (var lstLoai in lstByLoai)
        //                        {
        //                            if (lstLoai.Key.Equals(1))
        //                            {
        //                                //insert loại PHÍ
        //                                sheet.InsertRow(startRowInsert, 1);
        //                                sheet.Cells[startRowInsert, 1].Value = "I";
        //                                sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3].Value = "PHÍ";
        //                                sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
        //                                sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
        //                                    ExcelHorizontalAlignment.Center;
        //                                sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
        //                                    ExcelVerticalAlignment.Center;
        //                                for (var i = 1; i <= 10; i++)
        //                                {
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style =ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
        //                                }
        //                                startRowInsert += 1;

        //                                var lstPhi = lstLoai.ToList();
        //                                sheet.InsertRow(startRowInsert, lstPhi.Count);
        //                                for (var i = 0; i < lstPhi.Count; i++)
        //                                {
        //                                    for (var j = 1; j <= 10; j++)
        //                                    {
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
        //                                        if (lstPhi[i].MA_CHI_TIEU == 3)
        //                                        {
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                        }
        //                                    }
        //                                    sheet.Cells[startRowInsert + i, 1].Value = lstPhi[i].STT_CHI_TIEU;
        //                                    sheet.Cells[startRowInsert + i, 2].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].MA_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 3].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].TEN_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstPhi[i].MA_CHI_TIEU];
        //                                    sheet.Cells[startRowInsert + i, 5].Value = lstPhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 6].Value = lstPhi[i].DT_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 7].Value = lstPhi[i].DT_SOXDTD - lstPhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 8].Value = lstPhi[i].TH_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 9].Value = lstPhi[i].TH_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 10].Value = lstPhi[i].TH_SOXDTD - lstPhi[i].TH_SOBAOCAO;
        //                                }
        //                                startRowInsert += lstPhi.Count;
        //                            }
        //                            else
        //                            {
        //                                //Lệ phí
        //                                sheet.InsertRow(startRowInsert, 1);
        //                                sheet.Cells[startRowInsert, 1].Value = "II";
        //                                sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3].Value = "LỆ PHÍ";
        //                                sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
        //                                sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
        //                                    ExcelHorizontalAlignment.Center;
        //                                sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
        //                                    ExcelVerticalAlignment.Center;
        //                                for (var i = 1; i <= 10; i++)
        //                                {
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Top.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
        //                                }
        //                                startRowInsert += 1;

        //                                var lstLePhi = lstLoai.ToList();
        //                                sheet.InsertRow(startRowInsert, lstLePhi.Count);
        //                                for (var i = 0; i < lstLePhi.Count; i++)
        //                                {
        //                                    for (var j = 1; j <= 10; j++)
        //                                    {
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
        //                                        if (lstLePhi[i].MA_CHI_TIEU == 3)
        //                                        {
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                        }
        //                                    }
        //                                    sheet.Cells[startRowInsert + i, 1].Value = lstLePhi[i].STT_CHI_TIEU;
        //                                    sheet.Cells[startRowInsert + i, 2].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].MA_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 3].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].TEN_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstLePhi[i].MA_CHI_TIEU];
        //                                    sheet.Cells[startRowInsert + i, 5].Value = lstLePhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 6].Value = lstLePhi[i].DT_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 7].Value = lstLePhi[i].DT_SOXDTD - lstLePhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 8].Value = lstLePhi[i].TH_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 9].Value = lstLePhi[i].TH_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 10].Value = lstLePhi[i].TH_SOXDTD - lstLePhi[i].TH_SOBAOCAO;
        //                                }
        //                                startRowInsert += lstLePhi.Count;
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        var lst = list.ToList();
        //                        var lstByLoai = lst.GroupBy(x => x.LOAI);
        //                        sheet.InsertRow(startRowInsert, 1);
        //                        startRowInsert += 1;
        //                        sheet.Cells[startRowInsert, 3].Value = list.Key + "--"+lst[0].TEN_QHNS;
        //                        sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
        //                        sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
        //                        startRowInsert += 1;
        //                        sheet.InsertRow(startRowInsert, 4);
        //                        sheet.Cells[7, 1, 9, 10].Copy(sheet.Cells[startRowInsert, 1, startRowInsert + 2, 10]);
        //                        startRowInsert += 3;
        //                        foreach (var lstLoai in lstByLoai)
        //                        {
        //                            if (lstLoai.Key.Equals(1))
        //                            {
        //                                //insert loại PHÍ
        //                                sheet.InsertRow(startRowInsert, 1);
        //                                sheet.Cells[startRowInsert, 1].Value = "I";
        //                                sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3].Value = "PHÍ";
        //                                sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
        //                                sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
        //                                    ExcelHorizontalAlignment.Center;
        //                                sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
        //                                    ExcelVerticalAlignment.Center;
        //                                for (var i = 1; i <= 10; i++)
        //                                {
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
        //                                }
        //                                startRowInsert += 1;

        //                                var lstPhi = lstLoai.ToList();
        //                                sheet.InsertRow(startRowInsert, lstPhi.Count);
        //                                for (var i = 0; i < lstPhi.Count; i++)
        //                                {
        //                                    for (var j = 1; j <= 10; j++)
        //                                    {
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
        //                                        if (lstPhi[i].MA_CHI_TIEU == 3)
        //                                        {
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                        }
        //                                    }
        //                                    sheet.Cells[startRowInsert + i, 1].Value = lstPhi[i].STT_CHI_TIEU;
        //                                    sheet.Cells[startRowInsert + i, 2].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].MA_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 3].Value = lstPhi[i].MA_CHI_TIEU == 1 ? lstPhi[i].TEN_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstPhi[i].MA_CHI_TIEU];
        //                                    sheet.Cells[startRowInsert + i, 5].Value = lstPhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 6].Value = lstPhi[i].DT_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 7].Value = lstPhi[i].DT_SOXDTD - lstPhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 8].Value = lstPhi[i].TH_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 9].Value = lstPhi[i].TH_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 10].Value = lstPhi[i].TH_SOXDTD - lstPhi[i].TH_SOBAOCAO;
        //                                }
        //                                startRowInsert += lstPhi.Count;
        //                            }
        //                            else
        //                            {
        //                                //Lệ phí
        //                                sheet.InsertRow(startRowInsert, 1);
        //                                sheet.Cells[startRowInsert, 1].Value = "II";
        //                                sheet.Cells[startRowInsert, 1].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3].Value = "LỆ PHÍ";
        //                                sheet.Cells[startRowInsert, 3].Style.Font.Bold = true;
        //                                sheet.Cells[startRowInsert, 3, startRowInsert, 4].Merge = true;
        //                                sheet.Cells[startRowInsert, 3].Style.HorizontalAlignment =
        //                                    ExcelHorizontalAlignment.Center;
        //                                sheet.Cells[startRowInsert, 3].Style.VerticalAlignment =
        //                                    ExcelVerticalAlignment.Center;
        //                                for (var i = 1; i <= 10; i++)
        //                                {
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Top.Color.SetColor(Color.Black);
        //                                    sheet.Cells[startRowInsert, i].Style.Border.Right.Color.SetColor(Color.Black);
        //                                }
        //                                startRowInsert += 1;

        //                                var lstLePhi = lstLoai.ToList();
        //                                sheet.InsertRow(startRowInsert, lstLePhi.Count);
        //                                for (var i = 0; i < lstLePhi.Count; i++)
        //                                {
        //                                    for (var j = 1; j <= 10; j++)
        //                                    {
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //                                        sheet.Cells[startRowInsert + i, j].Style.Border.Right.Color.SetColor(Color.Black);
        //                                        if (lstLePhi[i].MA_CHI_TIEU == 3)
        //                                        {
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //                                            sheet.Cells[startRowInsert + i, j].Style.Border.Bottom.Color.SetColor(Color.Black);
        //                                        }
        //                                    }
        //                                    sheet.Cells[startRowInsert + i, 1].Value = lstLePhi[i].STT_CHI_TIEU;
        //                                    sheet.Cells[startRowInsert + i, 2].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].MA_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 3].Value = lstLePhi[i].MA_CHI_TIEU == 1 ? lstLePhi[i].TEN_NOIDUNGKT : "";
        //                                    sheet.Cells[startRowInsert + i, 4].Value = lstTenChiTieu[lstLePhi[i].MA_CHI_TIEU];
        //                                    sheet.Cells[startRowInsert + i, 5].Value = lstLePhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 6].Value = lstLePhi[i].DT_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 7].Value = lstLePhi[i].DT_SOXDTD - lstLePhi[i].DT_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 8].Value = lstLePhi[i].TH_SOBAOCAO;
        //                                    sheet.Cells[startRowInsert + i, 9].Value = lstLePhi[i].TH_SOXDTD;
        //                                    sheet.Cells[startRowInsert + i, 10].Value = lstLePhi[i].TH_SOXDTD - lstLePhi[i].TH_SOBAOCAO;
        //                                }
        //                                startRowInsert += lstLePhi.Count;
        //                            }
        //                        }
        //                    }
        //                }
        //                excelPackage.SaveAs(new FileInfo(exportUrlFile));
        //                var result = new HttpResponseMessage(HttpStatusCode.OK)
        //                {
        //                    Content = new ByteArrayContent(excelPackage.GetAsByteArray())
        //                };
        //                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //                {
        //                    FileName = "export_BIEU01A.xlsx"
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

        //[HttpPost]
        //public async Task<IHttpActionResult> Post(BIEU01AVm.InsertModel model)
        //{
        //    if (string.IsNullOrEmpty(model.MA_QHNS) || string.IsNullOrEmpty(model.MA_CHUONG) || model.NAM_BC < 0 ||
        //        model.KY_BC < 0 || model.DETAIL.Count < 1) return BadRequest();
        //    var response = new Response<string>();
        //    try
        //    {
        //        var bieu01A = new PHB_BIEU01A()
        //        {
        //            KY_BC = model.KY_BC,
        //            NAM_BC = model.NAM_BC,
        //            MA_CHUONG = model.MA_CHUONG,
        //            MA_QHNS = model.MA_QHNS,
        //            NGAY_TAO = DateTime.Now,
        //            NGUOI_TAO = RequestContext.Principal.Identity.Name,
        //            ObjectState = ObjectState.Added,
        //            TRANG_THAI = 0,
        //            REFID = Guid.NewGuid().ToString("N"),
        //            TEN_QHNS = model.TEN_QHNS,
        //            MA_QHNS_CHA = model.MA_QHNS_CHA
        //        };
        //        var checkReport = await _bieu01AService.Queryable().FirstOrDefaultAsync(x =>
        //            x.MA_CHUONG.Equals(bieu01A.MA_CHUONG) && x.MA_QHNS.Equals(bieu01A.MA_QHNS) &&
        //            x.NAM_BC == bieu01A.NAM_BC && x.KY_BC == bieu01A.KY_BC);
        //        if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });

        //        _bieu01AService.Insert(bieu01A);
        //        foreach (var detail in model.DETAIL)
        //        {
        //            detail.PHB_BIEU01A_REFID = bieu01A.REFID;
        //            detail.ObjectState = ObjectState.Added;
        //            _bieu01ADetailService.Insert(detail);
        //        }
        //        if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //        {
        //            response.Error = false;
        //            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.ERROR_UPDATE_DATA;
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.EMPTY_DATA;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_DATA;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}

        //[HttpPut]
        //public async Task<IHttpActionResult> Put(BIEU01AVm.EditModel model)
        //{
        //    if (string.IsNullOrEmpty(model.REFID)) return BadRequest();
        //    var response = new Response<string>();
        //    var hasValue = false;
        //    try
        //    {
        //        var bieu01A = await _bieu01AService.Queryable().FirstOrDefaultAsync(x => x.REFID.Equals(model.REFID));
        //        if (bieu01A == null) return Ok(new Response() { Error = true, Message = ErrorMessage.NOT_FOUND });
        //        #region Delete
        //        if (model.LstDelete != null && model.LstDelete.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var itemId in model.LstDelete)
        //            {
        //                PHB_BIEU01A_DETAIL item = await _bieu01ADetailService.FindByIdAsync(itemId);
        //                if (item != null)
        //                {
        //                    item.ObjectState = ObjectState.Deleted;
        //                    _bieu01ADetailService.Delete(item);
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Add
        //        if (model.LstAdd != null && model.LstAdd.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var item in model.LstAdd)
        //            {
        //                item.ObjectState = ObjectState.Added;
        //                item.PHB_BIEU01A_REFID = model.REFID;
        //                _bieu01ADetailService.Insert(item);
        //            }
        //        }
        //        #endregion

        //        #region Edit
        //        if (model.LstEdit != null && model.LstEdit.Count > 0)
        //        {
        //            hasValue = true;
        //            foreach (var item in model.LstEdit)
        //            {
        //                PHB_BIEU01A_DETAIL detail = await _bieu01ADetailService.FindByIdAsync(item.ID);
        //                if (detail != null)
        //                {
        //                    detail.ObjectState = ObjectState.Modified;
        //                    detail.DT_SOBAOCAO = item.DT_SOBAOCAO;
        //                    detail.DT_SOXDTD = item.DT_SOXDTD;
        //                    detail.TH_SOBAOCAO = item.TH_SOBAOCAO;
        //                    detail.TH_SOXDTD = item.TH_SOXDTD;
        //                    _bieu01ADetailService.Update(detail);
        //                }
        //            }
        //        }
        //        #endregion

        //        if (hasValue)
        //        {
        //            bieu01A.NGAY_SUA = DateTime.Now;
        //            bieu01A.NGUOI_SUA = RequestContext.Principal.Identity.Name;
        //            _bieu01AService.Update(bieu01A);

        //            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //            {
        //                response.Error = false;
        //                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //            }
        //            else
        //            {
        //                response.Error = true;
        //                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
        //            }
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.EMPTY_DATA;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_DATA;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}
    }
}
