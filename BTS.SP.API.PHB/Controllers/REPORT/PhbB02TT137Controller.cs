using BTS.SP.API.PHB.ViewModels.REPORT.B02_TT137;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B02_TT137;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT.B02_TT137;
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
    [RoutePrefix("api/report/phbB02_TT137")]
    public class PhbB02TT137Controller : ApiController
    {
        private readonly IPhbB02TT137Service _b02Service;
        private readonly IPhbB02TT137DetailService _b02DetailService;
        private readonly IPhbB02TT137TemplateService _b02TemplateService;
        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly IUnitOfWorkAsync _unitOfWork;


        public PhbB02TT137Controller(
           IPhbB02TT137Service service,
           IPhbB02TT137DetailService detailService,
           IPhbB02TT137TemplateService templateService,
           IAuNguoiDungService auService,
           IDmDVQHNSService dvqhnsService,
           IUnitOfWorkAsync unitOfWork
        )
        {
            _b02Service = service;
            _b02DetailService = detailService;
            _b02TemplateService = templateService;
            _auService = auService;
            _dvqhnsService = dvqhnsService;
            _unitOfWork = unitOfWork;
        }

        #region excle
        [Route("UploadReport")]
        [HttpPost]
        public IHttpActionResult UploadReport()
        {
            var response = new Response();
            var httpRequest = HttpContext.Current.Request;


            //validate model
            if (httpRequest.Files.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            //get informations 

            string chuong = httpRequest["MA_CHUONG"];
            string nguoiTao = "";
            string capDuToan = "";
            string maDonVi = httpRequest["MA_QHNS"];
            string tenDonVi = httpRequest["TEN_QHNS"];
            string donViDuToan = "";
            int nam = int.Parse(httpRequest["NAM_BC"]);
            int ky = int.Parse(httpRequest["KY_BC"]);
            try
            {


                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                nguoiTao = identity.Name;


            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            var form = new PHB_B02_TT137
            {
                CAP_DU_TOAN = capDuToan,
                MA_CHUONG = chuong,
                DON_VI_DT = donViDuToan,
                MA_QHNS = maDonVi,
                TEN_QHNS = tenDonVi,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGUOI_TAO = nguoiTao,
                NGAY_TAO = DateTime.Now,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0,
                NAM_BC = nam,
                KY_BC = ky
            };

            _b02Service.Insert(form);

            var lstTemplate = _b02TemplateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
            var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.CHI_TIEU).ToList();

            using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
            {
                var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (sheet == null)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                    return Ok(response);
                }

                var startRow = GetStartRow(sheet, lstChiTieuTemplate.FirstOrDefault());
                var endRow = GetEndRow(sheet, lstChiTieuTemplate.LastOrDefault());
                var stt_SapXep = 1;

                for (int row = startRow; row <= endRow; row++)
                {
                    try
                    {
                        var detail = new PHB_B02_TT137_DETAIL
                        {
                            STT = sheet.Cells[row, 1].Value?.ToString(),
                            CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
                            IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
                            IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
                            NAM_KE_HOACH = decimal.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0"),
                            THUC_HIEN = decimal.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
                            SOSANH = double.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0") / double.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),
                            PHB_B02_TT137_REFID = form.REFID,
                            STT_SAPXEP = stt_SapXep,
                            IS_OPTIONAL = 0
                        };

                        if (IsOptional(lstTemplate, detail.CHI_TIEU))
                        {
                            detail.IS_OPTIONAL = 1;
                        }
                        _b02DetailService.Insert(detail);

                    }
                    catch (Exception ex)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.ERROR_DATA;
                        return Ok(response);
                    }

                    stt_SapXep++;
                }
            }
            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);

        }

        private int GetStartRow(ExcelWorksheet sheet, string firstChiTieuTpl)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var lastRow = sheet.Dimension.End.Row;

            for (int row = firstRow; row <= lastRow; row++)
            {
                if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == firstChiTieuTpl.Trim().ToLower())
                {
                    return row;
                }
            }

            throw new Exception();
        }

        private int GetEndRow(ExcelWorksheet sheet, string lastChiTieuTpl)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var lastRow = sheet.Dimension.End.Row;

            for (int row = lastRow; row >= firstRow; row--)
            {
                if ((sheet.Cells[row, 2]?.Value?.ToString() ?? "").ToLower().Trim() == lastChiTieuTpl.Trim().ToLower())
                {
                    return row;
                }
            }

            throw new Exception();
        }
        private bool IsOptional(List<PHB_B02_TT137_TEMPLATE> lstChiTieuTemplate, string chiTieu)
        {
            if (lstChiTieuTemplate
                .Where(
                    tpl => tpl.IS_OPTIONAL == 0 &&
                    tpl.CHI_TIEU.ToLower().Trim() == chiTieu.ToLower().Trim())
                .Count() > 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        [Route("GetSumReport")]
        [HttpPost]
        public IHttpActionResult GetSumReport(B02TT137_ViewModel.SumPara para)
        {
            var response = new Response<List<PHB_B02_TT137_DETAIL>>();
            List<PHB_B02_TT137_DETAIL> detail = new List<PHB_B02_TT137_DETAIL>();
            List<string> dsRefid = new List<string>();
            List<PHB_B02_TT137_DETAIL> dsDetail = new List<PHB_B02_TT137_DETAIL>();
            //TEST
            List<PHB_B02_TT137_DETAIL> dsDetailTEST = new List<PHB_B02_TT137_DETAIL>();
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
                dsRefid = _b02Service.Queryable().Where(x => para.DSDVQHNS.Contains(x.MA_QHNS) && x.NAM_BC == NAM_BC).Select(x => x.REFID).ToList();
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
                    detail = _b02DetailService.Queryable().Where(x => x.PHB_B02_TT137_REFID == REFID).OrderBy(x => x.STT_SAPXEP).ToList();
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
            List<PHB_B02_TT137_DETAIL> sumDetail = new List<PHB_B02_TT137_DETAIL>();

            foreach (var a in dsDetail.OrderBy(x => x.STT_SAPXEP))
            {
                var y = sumDetail.FirstOrDefault(x => x.CHI_TIEU == a.CHI_TIEU && x.PHB_B02_TT137_REFID != a.PHB_B02_TT137_REFID);

                if (y != null)
                {
                    y.THUC_HIEN += a.THUC_HIEN;
                    y.NAM_KE_HOACH += a.NAM_KE_HOACH;
                    y.SOSANH = Convert.ToDouble(y.THUC_HIEN) / Convert.ToDouble(y.NAM_KE_HOACH);
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

        [Route("GetTemplate")]
        [HttpGet]
        public IHttpActionResult GetTemplate()
        {
            var Response = new Response<List<PHB_B02_TT137_TEMPLATE>>();
            try
            {
                Response.Data = _b02TemplateService.Queryable().OrderBy(x => x.STT_SAPXEP).ToList();
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

        [Route("GetDataDetail/{REFID}")]
        [HttpGet]
        public IHttpActionResult GetDataDetail(string REFID)
        {
            var Response = new Response<List<PHB_B02_TT137_DETAIL>>();
            try
            {
                Response.Data = _b02DetailService.Queryable().Where(x => x.PHB_B02_TT137_REFID.Equals(REFID)).OrderBy(x => x.STT_SAPXEP).ToList();
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

        [Route("AddNew")]
        [HttpPost]
        public IHttpActionResult AddNew(B02TT137_ViewModel.AddModel model)
        {
            var response = new Response();

            if (model.data == null && model.datadetail.Count <= 0)
            {
                response.Error = false;
                response.Message = "Không có dữ liệu";
            }

            var form = new PHB_B02_TT137
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
                KY_BC = model.data.KY_BC
            };

            _b02Service.Insert(form);

            foreach (var item in model.datadetail)
            {
                var form_Detail = new PHB_B02_TT137_DETAIL
                {
                    PHB_B02_TT137_REFID = form.REFID,
                    STT_SAPXEP = item.STT_SAPXEP,
                    STT = item.STT,
                    MA_SO = item.MA_SO,
                    MA_CHA = item.MA_CHA,
                    CHI_TIEU = item.CHI_TIEU,
                    THUC_HIEN = item.THUC_HIEN,
                    NAM_KE_HOACH = item.NAM_KE_HOACH,
                    SOSANH = double.Parse(item.THUC_HIEN.ToString()) / double.Parse(item.NAM_KE_HOACH.ToString()),
                    IS_BOLD = item.IS_BOLD,
                    IS_ITALIC = item.IS_ITALIC,
                    IS_OPTIONAL = item.IS_OPTIONAL,
                };
                _b02DetailService.Insert(form_Detail);
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

        #region edit
        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(B02TT137_ViewModel.EditModel model)
        {
            var respone = new Response();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var data = new PHB_B02_TT137();
            var listDetail = new List<PHB_B02_TT137_DETAIL>();

            if (model.datadetail.Count <= 0)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            try
            {
                data = _b02Service.Queryable().FirstOrDefault(x => x.REFID == model.refid);
                listDetail = _b02DetailService.Queryable().Where(x => x.PHB_B02_TT137_REFID == data.REFID).ToList();

            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            data.NGUOI_SUA = identity.Name;
            data.NGAY_SUA = DateTime.Now;
            data.ObjectState = ObjectState.Modified;

            try
            {
                foreach (var item in listDetail)
                {
                    _b02DetailService.Delete(item);
                }

                if (model.datadetail.Count > 0)
                {
                    foreach (var item in model.datadetail)
                    {
                        item.ID = 0;
                        item.PHB_B02_TT137_REFID = data.REFID;
                        _b02DetailService.Insert(item);
                    }
                }
            }
            catch (Exception ex)
            {
                respone.Error = true;
                respone.Message = "Lỗi hệ thống";
            }

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                respone.Error = true;
                WriteLogs.LogError(ex);
                respone.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(respone);
            }

            respone.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(respone);
        }

        #endregion

    }
}