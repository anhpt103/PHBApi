using BTS.SP.API.PHB.ViewModels.PBDT.B123;
using BTS.SP.API.PHB.ViewModels.PBDT.Main;
using BTS.SP.API.PHB.ViewModels.PBDT.TT342.B123;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.PBDT.B123;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.PBDT.B123;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace BTS.SP.API.PHB.Controllers.PBDT
{
    [RoutePrefix("api/phb/pbdt/tt342/PHB_PBDT_TT342_B123")]
    public class PHB_PBDT_B123_Controller : ApiController
    {
        private readonly IPHB_PBDT_B123_Service _B123Service;
        private readonly IPHB_PBDT_B123_DETAIL_Service _detailService;
        private readonly IPHB_PBDT_B123_TEMPLATE_Service _templateService;
        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;

        private readonly IUnitOfWorkAsync _unitOfWork;

        public PHB_PBDT_B123_Controller(
            IPHB_PBDT_B123_Service service,
            IPHB_PBDT_B123_DETAIL_Service detailService,
            IPHB_PBDT_B123_TEMPLATE_Service templateService,
            IAuNguoiDungService auService,
            IDmDVQHNSService dvqhnsService,
            ISysDvqhns_QuanLyService sysDVQHNSQLService,

            IUnitOfWorkAsync unitOfWork
        )
        {
            _B123Service = service;
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

            foreach (var donVi in lstDonVi)
            {
                var lstForms = new List<PHB_PBDT_B123>();
                var lstFormViewModel = new List<FormViewModel>();

                try
                {
                    lstForms = _B123Service.Queryable().Where(x => x.NAM_HIEN_HANH == nam && x.MA_DONVI == donVi.MA_DVQHNS).ToList();
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    WriteLogs.LogError(ex);
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                    return Ok(response);
                }

                foreach (var form in lstForms)
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

                if (lstFormViewModel.Count > 0)
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

        [Route("Gettemplate")]
        [HttpGet]
        public IHttpActionResult Gettemplate()
        {
            var response = new Response<List<PHB_PBDT_B123_TEMPLATE>>();

            try
            {
                response.Data = _templateService.Queryable().OrderBy(x => x.STT_SAPXEP).ToList();
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
            if (model == null || model.Form == null || model.Details == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            string chuong = "";
            string capDuToan = "";
            string maDonVi = "";
            string nguoiTao = "";
            string donViDutoan = "";


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


            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                nguoiTao = identity.Name;
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                return Ok(response);
            }

            var form = new PHB_PBDT_B123
            {
                REFID = Guid.NewGuid().ToString("n"),
                CHUONG = chuong,
                CAP_DU_TOAN = capDuToan,
                NGUOI_TAO = nguoiTao,
                NGAY_TAO = DateTime.Now,
                NGUOI_SUA = null,
                NGAY_SUA = null,
                MA_DONVI = model.Form.MA_DONVI,
                DON_VI_DT = donViDutoan,
                NAM_HIEN_HANH = model.Form.NAM_HIEN_HANH,
                NAM_KE_HOACH = model.Form.NAM_HIEN_HANH + 1,
                NAM_THUC_HIEN = model.Form.NAM_HIEN_HANH - 1,
                
            };

            var formCount = _B123Service.Queryable().Where(x => x.MA_DONVI == form.MA_DONVI && x.NAM_HIEN_HANH == form.NAM_HIEN_HANH).Count();

            if (formCount > 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EXITS_REPORT;
                return Ok(response);
            }

            _B123Service.Insert(form);

            foreach (var detail in model.Details)
            {
                detail.PHB_PBDT_B123_REFID = form.REFID;
                _detailService.Insert(detail);
            }

            try
            {
                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);

            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
         }


        [Route("GetDetail/{id}")]
        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            var response = new Response<List<PHB_PBDT_B123_DETAIL>>();
            try
            {
                var REDID = _B123Service.Queryable().FirstOrDefault(x => x.ID.Equals(id)).REFID;
                response.Data = _detailService.Queryable().Where(x => x.PHB_PBDT_B123_REFID == REDID).OrderBy( x => x.STT_SAPXEP).ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            return Ok(response);
        }

        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult Edit(Edit_ViewModel model)
        {
            var response = new Response();
            var form = new PHB_PBDT_B123();
            var lstDetail = new List<PHB_PBDT_B123_DETAIL>();
            if (model == null || model.FormId == null || model.Details == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            try
            {
                 form = _B123Service.Queryable().FirstOrDefault(x => x.ID.Equals(model.FormId));
                 lstDetail = _detailService.Queryable().Where(x => x.PHB_PBDT_B123_REFID == form.REFID).ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
           
            var Identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            form.NGUOI_SUA = Identity.Name;
            form.NGAY_SUA = DateTime.Now;
            form.ObjectState = ObjectState.Modified;

            try
            {
                foreach (var detail in lstDetail)
                {
                    _detailService.Delete(detail);
                }

                foreach(var details in model.Details)
                {
                    details.ID = new int();
                    details.PHB_PBDT_B123_REFID = form.REFID;
                    _detailService.Insert(details);
                }

                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);


        }
        [Route("Delete/{id}")]
        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            var response = new Response();
            if(id == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            var form = new PHB_PBDT_B123();
            var lstDetail = new List<PHB_PBDT_B123_DETAIL>();
            try
            {
                form = _B123Service.Queryable().FirstOrDefault(x => x.ID == id);
                lstDetail = _detailService.Queryable().Where(x => x.PHB_PBDT_B123_REFID == form.REFID).ToList();
            }
            catch(Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }

            try
            {
                _B123Service.Delete(form);
                foreach(var detail in lstDetail)
                {
                    _detailService.Delete(detail);
                }
                _unitOfWork.SaveChanges();
            }
            catch(Exception ex)
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

            if (httpRequest.Files.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }


            string chuong = "";
            string nguoitao = "";
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


            var form = new PHB_PBDT_B123
            {
                CAP_DU_TOAN = capDuToan,
                CHUONG = chuong,
                DON_VI_DT = donViDuToan,
                MA_DONVI = maDonVi,
                NAM_HIEN_HANH = namHienHanh,
                NAM_THUC_HIEN = namHienHanh - 1,
                NAM_KE_HOACH = namHienHanh + 1,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = nguoitao,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0,
               
            };

            var formCount = _B123Service.Queryable()
                   .Where(x => x.MA_DONVI == form.MA_DONVI && x.NAM_HIEN_HANH == form.NAM_HIEN_HANH).Count();

            if (formCount > 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EXITS_REPORT;
                return Ok(response);
            }

            _B123Service.Insert(form);
            var lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
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
                        var detail = new PHB_PBDT_B123_DETAIL
                        {
                            STT = sheet.Cells[row, 1].Value?.ToString(),
                            CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
                            IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
                            IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
                            NAM_THUC_HIEN = decimal.Parse(sheet.Cells[row, 3].Value?.ToString()?? "0"),
                            NAM_HIEN_HANH_DU_TOAN = decimal.Parse(sheet.Cells[row, 4].Value?.ToString()?? "0"),
                            NAM_HIEN_HANH_UOC_THUC_HIEN = decimal.Parse(sheet.Cells[row, 5].Value?.ToString() ?? "0"),
                            NAM_KE_HOACH = decimal.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0"),
                            PHB_PBDT_B123_REFID = form.REFID,
                            STT_SAPXEP = stt_SapXep
                        };

                        _detailService.Insert(detail);
                    }
                    catch (Exception ex)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.EMPTY_DATA;
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

        [Route("Approve/{formId}")]
        [HttpGet]
        public IHttpActionResult Approve(int formId)
        {
            var response = new Response<string>();

            //get report by id
            var form = new PHB_PBDT_B123();
            try
            {
                form = _B123Service.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
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
            var form = new PHB_PBDT_B123();
            try
            {
                form = _B123Service.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();
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

    }
}