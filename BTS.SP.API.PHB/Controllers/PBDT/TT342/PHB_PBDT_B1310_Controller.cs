using BTS.SP.API.PHB.ViewModels.PBDT.B1310;
using BTS.SP.API.PHB.ViewModels.PBDT.Main;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.PBDT.B1310;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.PBDT.B1310;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.PBDT
{
    [RoutePrefix("api/phb/pbdt/tt342/PHB_PBDT_TT342_B1310")]
    public class PHB_PBDT_B1310_Controller : ApiController
    {
        private readonly IPHB_PBDT_B1310_Service _formService;
        private readonly IPHB_PBDT_B1310_TEMPLATE_Service _templateService;
        private readonly IPHB_PBDT_B1310_DETAIL_Service _detailService;
        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;

        private readonly IUnitOfWorkAsync _unitOfWork;

        public PHB_PBDT_B1310_Controller(
            IPHB_PBDT_B1310_Service service,
            IPHB_PBDT_B1310_TEMPLATE_Service templateService,
            IPHB_PBDT_B1310_DETAIL_Service detailService,
            IAuNguoiDungService auService,
            IDmDVQHNSService dvqhnsService,
            ISysDvqhns_QuanLyService sysDVQHNSQLService,

            IUnitOfWorkAsync unitOfWork)
        {
            _formService = service;
            _templateService = templateService;
            _detailService = detailService;
            _auService = auService;
            _dvqhnsService = dvqhnsService;
            _sysDVQHNSQLService = sysDVQHNSQLService;


            _unitOfWork = unitOfWork;
        }

        [Route("GetListForm/{nam}")]
        [HttpGet]
        public IHttpActionResult GetListForm(int nam)
        {
            var response = new Response<TotalForm_ViewModel>();
            response.Data = new TotalForm_ViewModel();
            response.Data.FormsOfDonVi = new List<FormsOfDonVi_ViewModel>();
            var lstDonVi = new List<SYS_DVQHNS_QUANLY>();

            // get list DonVi which are children of current user 
            // and include current DonVi to list
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var maDVQHNS = _auService.Queryable().Where(user => user.USERNAME == identity.Name).FirstOrDefault().MA_DBHC;
                lstDonVi = _sysDVQHNSQLService.Queryable().Where(dv => dv.MA_DBHC == maDVQHNS).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach(var donVi in lstDonVi)
            {
                var lstForms = new List<PHB_PBDT_B1310>();
                var lstFormViewModel = new List<FormViewModel>();

                //get list forms
                try
                {
                    lstForms = _formService.Queryable().Where(form => form.NAM_HIEN_HANH == nam && form.MA_DONVI == donVi.MA_DVQHNS).ToList();
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    WriteLogs.LogError(ex);
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                    return Ok(response);
                }

                //get list form viewmodel form list forms
                foreach(var form in lstForms)
                {
                    var formViewModel = new FormViewModel
                    {
                        Id = form.ID,
                        Nam = form.NAM_HIEN_HANH,
                        NgaySua = form.NGAY_SUA,
                        NgayTao = form.NGAY_TAO,
                        NguoiSua = form.NGUOI_SUA,
                        NguoiTao = form.NGUOI_TAO,
                        TrangThai = form.TRANG_THAI
                    };

                    lstFormViewModel.Add(formViewModel);
                }

                if(lstFormViewModel.Count > 0)
                {
                    var formsOfDonVi = new FormsOfDonVi_ViewModel
                    {
                        MaDonVi = donVi.MA_DVQHNS,
                        TenDonVi = donVi.TEN_DVQHNS,
                        Forms = lstFormViewModel
                    };

                    response.Data.FormsOfDonVi.Add(formsOfDonVi);
                }
            }

            return Ok(response);
        }

        [Route("GetTemplate")]
        [HttpGet]
        public IHttpActionResult GetTemplate()
        {
            var response = new Response<List<PHB_PBDT_B1310_TEMPLATE>>();

            try
            {
                response.Data = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("GetDetail/{formId}")]
        [HttpGet]
        public IHttpActionResult GetDetail(int formId)
        {
            var response = new Response<List<PHB_PBDT_B1310_DETAIL>>();

            try
            {
                var form = _formService.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
                
                if(form == null)
                {
                    response.Message = ErrorMessage.EMPTY_DATA;
                    response.Error = true;
                    return Ok(response);
                }

                var refId = form.REFID;

                response.Data = _detailService.Queryable()
                    .Where(detail => detail.PHB_PBDT_B1310_REFID == refId)
                    .OrderBy(detail => detail.STT_SAPXEP)
                    .ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("AddContent")]
        [HttpPost]
        public IHttpActionResult AddContent(AddContent_ViewModel model)
        {
            var response = new Response();

            // validate model
            if(model == null || model.Form == null || model.Details == null || model.Details.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            //get informations by current user
            string chuong = "";
            string nguoiTao = "";
            string capDuToan = "";
            string maDonVi = "";
            string donViDuToan = "";
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

            try
            {
                chuong = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == model.Form.MA_DONVI)?.MA_CHUONG;
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            // add form
            var form = new PHB_PBDT_B1310
            {
                CAP_DU_TOAN = capDuToan,
                CHUONG = chuong,
                DON_VI_DT = donViDuToan,
                MA_DONVI = model.Form.MA_DONVI,
                NAM_HIEN_HANH = model.Form.NAM_HIEN_HANH,
                NAM_KE_HOACH = model.Form.NAM_HIEN_HANH + 1,
                NAM_THUC_HIEN = model.Form.NAM_HIEN_HANH - 1,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = nguoiTao,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0
            };

            // check if exist form
            var formCount = _formService.Queryable()
                .Where(f => f.MA_DONVI == form.MA_DONVI && f.NAM_HIEN_HANH == form.NAM_HIEN_HANH)
                .Count();
            if(formCount > 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EXITS_REPORT;
                return Ok(response);
            }

            _formService.Insert(form);

            // add details
            foreach (var detail in model.Details)
            {
                detail.PHB_PBDT_B1310_REFID = form.REFID;
                _detailService.Insert(detail);
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

        [Route("UploadExcel")]
        [HttpPost]
        public IHttpActionResult UploadExcel()
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
            
            //get informations by current user
            string chuong = "";
            string nguoiTao = "";
            string capDuToan = "";
            string maDonVi = "";
            string donViDuToan = "";
            int namHienHanh;
            try
            {
                namHienHanh = int.Parse(httpRequest["Nam"].ToString());

                if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                {
                    Error = true,
                    Message = "Không có mã đơn vị QHNS."
                });
                maDonVi = httpRequest["MA_QHNS"];
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            try
            {
                chuong = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == maDonVi)?.MA_CHUONG;
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            if (String.IsNullOrEmpty(maDonVi))
            {
                response.Error = true;
                response.Message = "Lỗi không tồn tại đơn vị tương ứng với người dùng hiện tại";
                return Ok(response);
            }

            // add form
            var form = new PHB_PBDT_B1310
            {
                CAP_DU_TOAN = capDuToan,
                CHUONG = chuong,
                DON_VI_DT = donViDuToan,
                MA_DONVI = maDonVi,
                NAM_HIEN_HANH = namHienHanh,
                NAM_KE_HOACH = namHienHanh + 1,
                NAM_THUC_HIEN = namHienHanh - 1,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = nguoiTao,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0
            };

            // check if exist form
            var formCount = _formService.Queryable()
                .Where(f => f.MA_DONVI == form.MA_DONVI && f.NAM_HIEN_HANH == form.NAM_HIEN_HANH)
                .Count();
            if (formCount > 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EXITS_REPORT;
                return Ok(response);
            }

            _formService.Insert(form);

            var lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
            var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.CHI_TIEU).ToList();

            //add detail
            using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream)){
                var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if(sheet == null)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                    return Ok(response);
                }

                var startRow = GetStartRow(sheet, lstChiTieuTemplate.FirstOrDefault());
                var endRow = GetEndRow(sheet, lstChiTieuTemplate.LastOrDefault());
                var stt_SapXep = 1;

                for(int row = startRow; row <= endRow; row++)
                {
                    try
                    {
                        var detail = new PHB_PBDT_B1310_DETAIL
                        {
                            STT = sheet.Cells[row, 1].Value?.ToString(),
                            CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),

                            UOC_THUC_HIEN = decimal.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),

                            NAMHH_SO_DOI_TUONG = decimal.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0"),
                            NAMHH_DU_TOAN = decimal.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0"),
                            NAMHH_UOC_THUC_HIEN = decimal.Parse(sheet.Cells[row, 7].Value?.ToString() ?? "0"),

                            NAMKH_SO_DOI_TUONG = decimal.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0"),
                            NAMKH_MUC_TRO_CAP = decimal.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0"),
                            NAMKH_DU_TOAN = decimal.Parse(sheet.Cells[row, 7].Value?.ToString() ?? "0"),


                            IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
                            IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
                            IS_OPTIONAL = 0,
                            PHB_PBDT_B1310_REFID = form.REFID,
                            STT_SAPXEP = stt_SapXep,
                        };

                        if(IsOptional(lstTemplate, detail.CHI_TIEU))
                        {
                            detail.IS_OPTIONAL = 1;
                        }

                        _detailService.Insert(detail);
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

            //save
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

        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult Edit(Edit_ViewModel model)
        {
            var response = new Response();

            var form = new PHB_PBDT_B1310();
            var lstDetail = new List<PHB_PBDT_B1310_DETAIL>();

            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == model.FormId).FirstOrDefault();
                lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_B1310_REFID == form.REFID).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            // check if form has been approved
            if (form.TRANG_THAI == 1)
            {
                response.Error = true;
                response.Message = "Báo cáo đã được duyệt, không thể sửa";
                return Ok(response);
            }

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            form.NGUOI_SUA = identity.Name;
            form.NGAY_SUA = DateTime.Now;
            form.ObjectState = ObjectState.Modified;

            try
            {
                // delete all old details
                foreach (var detail in lstDetail)
                {
                    _detailService.Delete(detail);
                }

                // add new details
                foreach (var detail in model.Details)
                {
                    detail.ID = 0;
                    detail.PHB_PBDT_B1310_REFID = form.REFID;
                    _detailService.Insert(detail);
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        [Route("Delete/{formId}")]
        [HttpGet]
        public IHttpActionResult Delete(int formId)
        {
            var response = new Response<string>();

            //get report by refid
            var form = new PHB_PBDT_B1310();
            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            if (form == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //check if report is already censored or not
            if (form.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể xóa!";
                response.Error = true;
                return Ok(response);
            }

            //get list details by refid
            var lstDetail = new List<PHB_PBDT_B1310_DETAIL>();
            try
            {
                lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_B1310_REFID == form.REFID).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            //delete report and list details
            try
            {
                _formService.Delete(form);
                foreach (var detail in lstDetail)
                {
                    _detailService.Delete(detail);
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        [Route("Approve/{formId}")]
        [HttpGet]
        public IHttpActionResult Approve(int formId)
        {
            var response = new Response<string>();

            //get report by id
            var form = new PHB_PBDT_B1310();
            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            if (form == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //delete report and list details
            try
            {
                form.TRANG_THAI = 1;
                form.ObjectState = ObjectState.Modified;
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        [Route("Reject/{formId}")]
        [HttpGet]
        public IHttpActionResult Reject(int formId)
        {
            var response = new Response<string>();

            //get report by id
            var form = new PHB_PBDT_B1310();
            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            if (form == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //delete report and list details
            try
            {
                form.TRANG_THAI = 0;
                form.ObjectState = ObjectState.Modified;
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        private bool IsOptional(List<PHB_PBDT_B1310_TEMPLATE> lstChiTieuTemplate, string chiTieu)
        {
            if(lstChiTieuTemplate
                .Where(
                    tpl => tpl.IS_OPTIONAL == 0 && 
                    tpl.CHI_TIEU.ToLower().Trim() == chiTieu.ToLower().Trim())
                .Count() > 0)
            {
                return false;
            }

            return true;
        }

        private int GetStartRow(ExcelWorksheet sheet, string firstChiTieuTpl)
        {
            var firstRow = sheet.Dimension.Start.Row;
            var lastRow = sheet.Dimension.End.Row;

            for(int row = firstRow; row <= lastRow; row++)
            {
                if ((sheet.Cells[row, 2]?.Value?.ToString()?? "").ToLower().Trim() == firstChiTieuTpl.Trim().ToLower())
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
    }
}
