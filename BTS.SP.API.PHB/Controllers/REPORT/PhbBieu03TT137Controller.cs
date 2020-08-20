

using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU03_TT137;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT.BIEU03_TT137;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbBIEU03_TT137")]
    public class PhbBieu03TT137Controller : ApiController
    {
        private readonly IBIEU03_TT137Service _b03Service;
        private readonly IBIEU03_TT137_DETAILService _b03DetailService;
        private readonly IBIEU03_TT137_TEMPLATEService _b03TemplateService;
        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly IUnitOfWorkAsync _unitOfWork;


        public PhbBieu03TT137Controller(
           IBIEU03_TT137Service service,
           IBIEU03_TT137_DETAILService detailService,
           IBIEU03_TT137_TEMPLATEService templateService,
           IAuNguoiDungService auService,
           IDmDVQHNSService dvqhnsService,
           IUnitOfWorkAsync unitOfWork
        )
        {
            _b03Service = service;
            _b03DetailService = detailService;
            _b03TemplateService = templateService;
            _auService = auService;
            _dvqhnsService = dvqhnsService;
            _unitOfWork = unitOfWork;
        }

        //#region UploadReport11
        //[Route("UploadReport11")]
        //[HttpPost]
        //public IHttpActionResult UploadReport11()
        //{
        //    var response = new Response();
        //    var httpRequest = HttpContext.Current.Request;


        //    //validate model
        //    if (httpRequest.Files.Count == 0)
        //    {
        //        response.Error = true;
        //        response.Message = ErrorMessage.EMPTY_DATA;
        //        return Ok(response);
        //    }
        //    //get informations 

        //    string chuong = httpRequest["MA_CHUONG"];
        //    string nguoiTao = "";
        //    string capDuToan = "";
        //    string maDonVi = httpRequest["MA_QHNS"];
        //    string tenDonVi = httpRequest["TEN_QHNS"];
        //    string donViDuToan = "";
        //    int nam = int.Parse(httpRequest["NAM_BC"]);
        //    int ky = int.Parse(httpRequest["KY_BC"]);
        //    try
        //    {


        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        nguoiTao = identity.Name;


        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        WriteLogs.LogError(ex);
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //        return Ok(response);
        //    }

        //    var form = new PHB_BIEU03_TT137
        //    {
        //        CAP_DU_TOAN = capDuToan,
        //        MA_CHUONG = chuong,
        //        DON_VI_DT = donViDuToan,
        //        MA_QHNS = maDonVi,
        //        TEN_QHNS = tenDonVi,
        //        NGAY_SUA = null,
        //        NGUOI_SUA = null,
        //        NGUOI_TAO = nguoiTao,
        //        NGAY_TAO = DateTime.Now,
        //        REFID = Guid.NewGuid().ToString("n"),
        //        TRANG_THAI = 0,
        //        NAM_BC = nam,
        //        KY_BC = ky
        //    };

        //    _b03Service.Insert(form);

        //    var lstTemplate = _b03TemplateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
        //    var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.CHI_TIEU).ToList();

        //    using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
        //    {
        //        var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
        //        if (sheet == null)
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //            return Ok(response);
        //        }

        //        var startRow = GetStartRow(sheet, lstChiTieuTemplate.FirstOrDefault());
        //        var endRow = GetEndRow(sheet, lstChiTieuTemplate.LastOrDefault());
        //        var stt_SapXep = 1;

        //        for (int row = startRow; row <= endRow; row++)
        //        {
        //            try
        //            {
        //                var detail = new PHB_BIEU03_TT137_DETAIL
        //                {
        //                    PHB_BIEU03_TT137_REFID = form.REFID,
        //                    STT_SAPXEP = stt_SapXep,
        //                    STT = sheet.Cells[row, 1].Value?.ToString(),
        //                    CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
        //                    IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
        //                    IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
        //                    DT_NT = decimal.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0"),
        //                    DT_GNN = decimal.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    DT_SDNN = double.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0") + double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    QT_NN = decimal.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0"),
        //                    DG_TUYETDOI = double.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0") - double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    DG_TUONGDOI = double.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0") / double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
        //                    DSD_TUYETDOI = double.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0") - (double.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0") + double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0")),
        //                    DSD_TUONGDOI = double.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0") / (double.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0") + double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0")),
        //                    IS_OPTIONAL = 0
        //                };

        //                if (IsOptional(lstTemplate, detail.CHI_TIEU))
        //                {
        //                    detail.IS_OPTIONAL = 1;
        //                }
        //                _b03DetailService.Insert(detail);

        //            }
        //            catch (Exception ex)
        //            {
        //                response.Error = true;
        //                response.Message = ErrorMessage.ERROR_DATA;
        //                return Ok(response);
        //            }

        //            stt_SapXep++;
        //        }
        //    }
        //    try
        //    {
        //        _unitOfWork.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        WriteLogs.LogError(ex);
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //        return Ok(response);
        //    }

        //    response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //    return Ok(response);

        //}

        //private int GetStartRow(ExcelWorksheet sheet, string firstChiTieuTpl)
        //{
        //    var firstRow = sheet.Dimension.Start.Row;
        //    var lastRow = sheet.Dimension.End.Row;

        //    for (int row = firstRow; row <= lastRow; row++)
        //    {
        //        if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == firstChiTieuTpl.Trim().ToLower())
        //        {
        //            return row;
        //        }
        //    }

        //    throw new Exception();
        //}

        //private int GetEndRow(ExcelWorksheet sheet, string lastChiTieuTpl)
        //{
        //    var firstRow = sheet.Dimension.Start.Row;
        //    var lastRow = sheet.Dimension.End.Row;

        //    for (int row = lastRow; row >= firstRow; row--)
        //    {
        //        if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == lastChiTieuTpl.Trim().ToLower())
        //        {
        //            return row;
        //        }
        //    }

        //    throw new Exception();
        //}
        //private bool IsOptional(List<PHB_BIEU03_TT137_TEMPLATE> lstChiTieuTemplate, string chiTieu)
        //{
        //    if (lstChiTieuTemplate
        //        .Where(
        //            tpl => tpl.IS_OPTIONAL == 0 &&
        //            tpl.CHI_TIEU.ToLower().Trim() == chiTieu.ToLower().Trim())
        //        .Count() > 0)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        //#endregion

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
                        PHB_BIEU03_TT137 bieu03TT137 = new PHB_BIEU03_TT137()
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
                            bieu03TT137.MA_CHUONG = httpRequest["MA_CHUONG"];
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            bieu03TT137.MA_QHNS = httpRequest["MA_QHNS"];
                            bieu03TT137.TEN_QHNS = httpRequest["TEN_QHNS"];
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            bieu03TT137.NAM_BC = int.Parse(httpRequest["NAM_BC"]);
                            if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có kỳ báo cáo."
                            });
                            bieu03TT137.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _b03Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(bieu03TT137.MA_CHUONG) && x.MA_QHNS.Equals(bieu03TT137.MA_QHNS) &&
                                x.NAM_BC == bieu03TT137.NAM_BC && x.KY_BC == bieu03TT137.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _b03Service.Insert(bieu03TT137);

                            var Stt_sapxep = 1;
                            int startRow = 11;
                            while (workSheet.Cells[startRow, 2].Value != null && !string.IsNullOrEmpty(workSheet.Cells[startRow, 2].Value.ToString()))
                            {
                                {
                                    PHB_BIEU03_TT137_DETAIL detail = new PHB_BIEU03_TT137_DETAIL()
                                    {
                                        ObjectState = ObjectState.Added,
                                        PHB_BIEU03_TT137_REFID = bieu03TT137.REFID,
                                    };
                                    detail.STT = workSheet.Cells[startRow, 1].Value != null
                                    ? workSheet.Cells[startRow, 1].Value.ToString()
                                    : null;
                                    detail.CHI_TIEU = workSheet.Cells[startRow, 2].Value != null
                                    ? workSheet.Cells[startRow, 2].Value.ToString()
                                    : null;
                                    detail.STT_SAPXEP = Stt_sapxep;
                                    if (workSheet.Cells[startRow, 3].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_NT = decimal.Parse(workSheet.Cells[startRow, 3].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_NT = 0;
                                    }
                                    if (workSheet.Cells[startRow, 4].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_GNN = decimal.Parse(workSheet.Cells[startRow, 4].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_GNN = 0;
                                    }

                                    if (workSheet.Cells[startRow, 5].Value != null)
                                    {
                                        try
                                        {
                                            detail.DT_SDNN = double.Parse(workSheet.Cells[startRow, 5].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DT_SDNN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 6].Value != null)
                                    {
                                        try
                                        {
                                            detail.QT_NN = decimal.Parse(workSheet.Cells[startRow, 6].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.QT_NN = 0;
                                    }
                                    if (workSheet.Cells[startRow, 7].Value != null)
                                    {
                                        try
                                        {
                                            detail.DG_TUYETDOI = double.Parse(workSheet.Cells[startRow, 7].Value.ToString());

                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DG_TUYETDOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 8].Value != null)
                                    {
                                        try
                                        {
                                            detail.DG_TUONGDOI = double.Parse(workSheet.Cells[startRow, 8].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DG_TUONGDOI = 0;
                                    }

                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.DSD_TUYETDOI = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DSD_TUYETDOI = 0;
                                    }
                                    if (workSheet.Cells[startRow, 9].Value != null)
                                    {
                                        try
                                        {
                                            detail.DSD_TUONGDOI = double.Parse(workSheet.Cells[startRow, 9].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            WriteLogs.LogError(ex);
                                            return Ok(new Response() { Error = true, Message = ErrorMessage.ERROR_DATA });
                                        }
                                    }
                                    else
                                    {
                                        detail.DSD_TUONGDOI = 0;
                                    }
                                    _b03DetailService.Insert(detail);
                                    startRow += 1;
                                    Stt_sapxep += 1;
                                }
                            }

                            if (await _unitOfWork.SaveChangesAsync() > 0)
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

        #region GetSumReport
        [Route("GetSumReport")]
        [HttpPost]
        public IHttpActionResult GetSumReport(BIEU03_TT137Vm.SumPara para)
        {
            var response = new Response<List<PHB_BIEU03_TT137_DETAIL>>();
            List<PHB_BIEU03_TT137_DETAIL> detail = new List<PHB_BIEU03_TT137_DETAIL>();
            List<string> dsRefid = new List<string>();
            List<PHB_BIEU03_TT137_DETAIL> dsDetail = new List<PHB_BIEU03_TT137_DETAIL>();
            //TEST
            List<PHB_BIEU03_TT137_DETAIL> dsDetailTEST = new List<PHB_BIEU03_TT137_DETAIL>();
            int NAM_BC = int.Parse(para.NAM_BC);
            var lstDSDVQHNS = para.DSDVQHNS.Split(',');
            if (para == null || para.DSDVQHNS == null || para.NAM_BC == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }
            //Lấy danh sác Các Báo Cáo
            try
            {
                dsRefid = _b03Service.Queryable().Where(x => para.DSDVQHNS.Contains(x.MA_QHNS) && x.NAM_BC == NAM_BC).Select(x => x.REFID).ToList();
            }
            catch (Exception ex)
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
                    detail = _b03DetailService.Queryable().Where(x => x.PHB_BIEU03_TT137_REFID == REFID).OrderBy(x => x.STT_SAPXEP).ToList();
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                    return Ok(response);
                };
                dsDetail.AddRange(detail);
            }

            //Tính Sum
            List<PHB_BIEU03_TT137_DETAIL> sumDetail = new List<PHB_BIEU03_TT137_DETAIL>();

            foreach (var a in dsDetail.OrderBy(x => x.STT_SAPXEP))
            {
                var y = sumDetail.FirstOrDefault(x => x.CHI_TIEU == a.CHI_TIEU && x.PHB_BIEU03_TT137_REFID != a.PHB_BIEU03_TT137_REFID);

                if (y != null)
                {
                    y.DT_NT += a.DT_NT;
                    y.DT_GNN += a.DT_GNN;
                    y.DT_SDNN = Convert.ToDouble(y.DT_NT) + Convert.ToDouble(y.DT_GNN);
                    y.QT_NN += a.QT_NN;
                    y.DG_TUYETDOI = Convert.ToDouble(y.QT_NN) - Convert.ToDouble(y.DT_GNN);
                    y.DG_TUONGDOI = Convert.ToDouble(y.QT_NN) / Convert.ToDouble(y.DT_GNN);
                    y.DSD_TUYETDOI = Convert.ToDouble(y.QT_NN) - (Convert.ToDouble(y.DT_NT) + Convert.ToDouble(y.DT_GNN));
                    y.DSD_TUONGDOI = Convert.ToDouble(y.QT_NN) / (Convert.ToDouble(y.DT_NT) + Convert.ToDouble(y.DT_GNN));
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
        #endregion

        #region GetTemplate
        [Route("GetTemplate")]
        [HttpGet]
        public IHttpActionResult GetTemplate()
        {
            var Response = new Response<List<PHB_BIEU03_TT137_TEMPLATE>>();
            try
            {
                Response.Data = _b03TemplateService.Queryable().OrderBy(x => x.STT_SAPXEP).ToList();
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = "Lỗi hệ thống";
            }

            Response.Error = false;
            Response.Message = "Lấy dữ liệu thành công";
            return Ok(Response);
        }
        #endregion

        #region GetDataDetail
        [Route("GetDataDetail/{REFID}")]
        [HttpGet]
        public IHttpActionResult GetDataDetail(string REFID)
        {
            var Response = new Response<List<PHB_BIEU03_TT137_DETAIL>>();
            try
            {
                Response.Data = _b03DetailService.Queryable().Where(x => x.PHB_BIEU03_TT137_REFID.Equals(REFID)).OrderBy(x => x.STT_SAPXEP).ToList();
            }
            catch (Exception ex)
            {
                Response.Error = true;
                Response.Message = "Lỗi hệ thống";
            }
            return Ok(Response);
        }
        #endregion

        #region AddNew
        [Route("AddNew")]
        [HttpPost]
        public IHttpActionResult AddNew(BIEU03_TT137Vm.AddModel model)
        {
            var response = new Response();

            if (model.data == null && model.datadetail.Count <= 0)
            {
                response.Error = false;
                response.Message = "Không có dữ liệu";
            }

            var form = new PHB_BIEU03_TT137
            {
                CAP_DU_TOAN = "",
                MA_CHUONG = model.data.MA_CHUONG,
                DON_VI_DT = "",
                MA_QHNS = model.data.MA_QHNS,
                TEN_QHNS = model.data.TEN_QHNS,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGUOI_TAO = model.data.NGUOI_TAO,
                NGAY_TAO = DateTime.Now,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0,
                NAM_BC = model.data.NAM_BC,
                KY_BC = model.data.KY_BC,
            };

            _b03Service.Insert(form);

            foreach (var item in model.datadetail)
            {
                var form_Detail = new PHB_BIEU03_TT137_DETAIL
                {
                    PHB_BIEU03_TT137_REFID = form.REFID,
                    STT_SAPXEP = item.STT_SAPXEP,
                    STT = item.STT,
                    MA_SO = item.MA_SO,
                    MA_CHA = item.MA_CHA,
                    CHI_TIEU = item.CHI_TIEU,
                    DT_NT = item.DT_NT,
                    DT_GNN = item.DT_GNN,
                    DT_SDNN = item.DT_SDNN,
                    QT_NN = item.QT_NN,
                    DG_TUYETDOI = item.DG_TUYETDOI,
                    DG_TUONGDOI = item.DG_TUONGDOI,
                    DSD_TUYETDOI = item.DSD_TUYETDOI,
                    DSD_TUONGDOI = item.DSD_TUONGDOI,
                    IS_BOLD = item.IS_BOLD,
                    IS_ITALIC = item.IS_ITALIC,
                    IS_OPTIONAL = item.IS_OPTIONAL,
                };
                _b03DetailService.Insert(form_Detail);
            }

            try
            {
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "Lỗi hệ thống";
            }

            response.Error = false;
            response.Message = "Thêm mới dữ liệu thành công";

            return Ok(response);

        }
        #endregion

        #region Edit
        [Route("Edit")]
        [HttpPut]
        public async Task<IHttpActionResult> Edit(BIEU03_TT137Vm.EditModel model)
        {
            var response = new Response<string>();
            var itemData = await _b03Service.Queryable().FirstOrDefaultAsync(X => X.REFID == model.data.REFID);
            if (itemData.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
                response.Error = true;
                return Ok(response);
            }
            try
            {
                if (itemData != null)
                {
                    var lstDetail = new List<PHB_BIEU03_TT137_DETAIL>();
                    lstDetail = _b03DetailService.Queryable().Where(detail => detail.PHB_BIEU03_TT137_REFID == itemData.REFID).ToList();
                    if (lstDetail.Count > 0)
                    {
                        foreach (var item in lstDetail)
                        {
                            item.ObjectState = ObjectState.Deleted;
                            _b03DetailService.Delete(item);
                        }
                        if (await _unitOfWork.SaveChangesAsync() > 0)
                        {
                            itemData.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                            itemData.NGAY_SUA = DateTime.Now;
                            itemData.ObjectState = ObjectState.Modified;
                            _b03Service.Update(itemData);
                            foreach (var record in model.datadetail)
                            {
                                PHB_BIEU03_TT137_DETAIL itemDetail = new PHB_BIEU03_TT137_DETAIL();
                                itemDetail.PHB_BIEU03_TT137_REFID = itemData.REFID;
                                itemDetail.STT = record.STT;
                                itemDetail.MA_SO = record.MA_SO;
                                itemDetail.CHI_TIEU = record.CHI_TIEU;
                                itemDetail.MA_CHA = record.MA_CHA;
                                itemDetail.STT_SAPXEP = record.STT_SAPXEP;
                                itemDetail.DT_NT = record.DT_NT;
                                itemDetail.DT_GNN = record.DT_GNN;
                                itemDetail.DT_SDNN = record.DT_SDNN;
                                itemDetail.QT_NN = record.QT_NN;
                                itemDetail.DG_TUYETDOI = record.DG_TUYETDOI;
                                itemDetail.DG_TUONGDOI = record.DG_TUONGDOI;
                                itemDetail.DSD_TUYETDOI = record.DSD_TUYETDOI;
                                itemDetail.DSD_TUONGDOI = record.DSD_TUONGDOI;
                                itemDetail.ObjectState = ObjectState.Added;
                                _b03DetailService.Insert(itemDetail);
                            }
                        }
                        if (await _unitOfWork.SaveChangesAsync() > 0)
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
                        if (await _unitOfWork.SaveChangesAsync() > 0)
                        {
                            itemData.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                            itemData.NGAY_SUA = DateTime.Now;
                            itemData.ObjectState = ObjectState.Modified;
                            _b03Service.Update(itemData);
                            foreach (var record in model.datadetail)
                            {
                                PHB_BIEU03_TT137_DETAIL itemDetail = new PHB_BIEU03_TT137_DETAIL();
                                itemDetail.PHB_BIEU03_TT137_REFID = itemData.REFID;
                                itemDetail.STT = record.STT;
                                itemDetail.MA_SO = record.MA_SO;
                                itemDetail.CHI_TIEU = record.CHI_TIEU;
                                itemDetail.MA_CHA = record.MA_CHA;
                                itemDetail.STT_SAPXEP = record.STT_SAPXEP;
                                itemDetail.DT_NT = record.DT_NT;
                                itemDetail.DT_GNN = record.DT_GNN;
                                itemDetail.DT_SDNN = record.DT_SDNN;
                                itemDetail.QT_NN = record.QT_NN;
                                itemDetail.DG_TUYETDOI = record.DG_TUYETDOI;
                                itemDetail.DG_TUONGDOI = record.DG_TUONGDOI;
                                itemDetail.DSD_TUYETDOI = record.DSD_TUYETDOI;
                                itemDetail.DSD_TUONGDOI = record.DSD_TUONGDOI;
                                itemDetail.ObjectState = ObjectState.Added;
                                _b03DetailService.Insert(itemDetail);
                            }
                        }
                        if (await _unitOfWork.SaveChangesAsync() > 0)
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
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
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

        #endregion
    }
}
