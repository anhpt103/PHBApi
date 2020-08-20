using BTS.SP.API.PHB.ViewModels.PBDT.QLDT.Main;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using BTS.SP.PHB.SERVICE.PBDT.QLDT;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using BTS.SP.API.PHB.ViewModels.PBDT.QLDT.B01;

namespace BTS.SP.API.PHB.Controllers.PBDT
{
    [RoutePrefix("api/phb/pbdt/QLDT/PHB_PBDT_QLDT_B01")]
    public class PHB_PBDT_QLDT_B01_Controller : ApiController
    {
        private readonly IPHB_PBDT_QLDT_B01_Service _formService;
        private readonly IPHB_PBDT_QLDT_B01_TEMPLATE_Service _templateService;
        private readonly IPHB_PBDT_QLDT_B01_DETAIL_Service _detailService;

        private readonly IAuNguoiDungService _auService;
        private readonly IDmDBHCService _dbhcService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public PHB_PBDT_QLDT_B01_Controller(
            IPHB_PBDT_QLDT_B01_Service service,
            IPHB_PBDT_QLDT_B01_TEMPLATE_Service templateService,
            IPHB_PBDT_QLDT_B01_DETAIL_Service detailService,

            IAuNguoiDungService auService,
            IDmDBHCService dbhcService,
            IUnitOfWorkAsync unitOfWork)
        {
            _formService = service;
            _templateService = templateService;
            _detailService = detailService;
            _auService = auService;
            _dbhcService = dbhcService;

            _unitOfWork = unitOfWork;
        }

        [Route("GetListForm/{nam}")]
        [HttpGet]
        public IHttpActionResult GetListForm(int nam)
        {
            var response = new Response<TotalForm_ViewModel>();
            response.Data = new TotalForm_ViewModel();
            response.Data.FormsOfDBHC = new List<FormsOfDBHC_ViewModel>();
            var lstDBHC = new List<DM_DBHC>();

            // get list DonVi which are children of current user 
            // and include current DonVi to list
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var maDBHC = _auService.Queryable().Where(user => user.USERNAME == identity.Name).FirstOrDefault().MA_DBHC;
                lstDBHC = _dbhcService.Queryable().Where(dv => dv.MA_DBHC == maDBHC || dv.MA_DBHC_CHA == maDBHC).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            foreach (var donVi in lstDBHC)
            {
                var lstForms = new List<PHB_PBDT_QLDT_B01>();
                var lstFormViewModel = new List<FormViewModel>();

                //get list forms
                try
                {
                    lstForms = _formService.Queryable().Where(form => form.NAM == nam && form.MA_DBHC == donVi.MA_DBHC).ToList();
                }
                catch (Exception ex)
                {
                    response.Error = true;
                    WriteLogs.LogError(ex);
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                    return Ok(response);
                }

                //get list form viewmodel form list forms
                foreach (var form in lstForms)
                {
                    var formViewModel = new FormViewModel
                    {
                        Id = form.ID,
                        Nam = form.NAM,
                        Thang = form.THANG,
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
                    var FormsOfDBHC = new FormsOfDBHC_ViewModel
                    {
                        MaDBHC = donVi.MA_DBHC,
                        TenDBHC = donVi.TEN_DBHC,
                        Forms = lstFormViewModel
                    };

                    response.Data.FormsOfDBHC.Add(FormsOfDBHC);
                }
            }

            return Ok(response);
        }

        [Route("GetTemplate")]
        [HttpGet]
        public IHttpActionResult GetTemplate()
        {
            var response = new Response<Template_ViewModel>();

            //get informations by current user
            string user = "";
            string maDBHC = "";
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                user = identity.Name;
                maDBHC = _auService.Queryable().Where(u => u.USERNAME == user).FirstOrDefault()?.MA_DBHC;
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            var dbhc = _dbhcService.Queryable().Where(db => db.MA_DBHC == maDBHC).FirstOrDefault();
            if(dbhc == null)
            {
                response.Error = true;
                response.Message = "Không có địa bàn hành chính ứng với tài khoản hiện tại";
                return Ok(response);
            }

            if(dbhc.LOAI != 4)
            {
                response.Error = true;
                response.Message = "Tài khoản hiện tại không phải xã/phường/thị trấn";
                return Ok(response);
            }

            var tinh = "";
            var huyen = "";
            var xa = dbhc.TEN_DBHC;
            List<PHB_PBDT_QLDT_B01_TEMPLATE> lstTemplate;
            try
            {
                tinh = _dbhcService.Queryable().Where(db => db.MA_DBHC == dbhc.MA_T).FirstOrDefault().TEN_DBHC;
                huyen = _dbhcService.Queryable().Where(db => db.MA_DBHC == dbhc.MA_H).FirstOrDefault().TEN_DBHC;
                lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Không có thông tin tỉnh, huyện tương ứng với tài khoản hiện tại";
                return Ok(response);
            }

            try
            {
                response.Data = new Template_ViewModel
                {
                    Tinh = tinh,
                    Huyen = huyen,
                    Xa = xa,
                    Details = lstTemplate
                };
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
            var response = new Response<Detail_ViewModel>();

            try
            {
                var form = _formService.Queryable().Where(frm => frm.ID == formId).FirstOrDefault();

                if (form == null)
                {
                    response.Message = ErrorMessage.EMPTY_DATA;
                    response.Error = true;
                    return Ok(response);
                }

                var refId = form.REFID;

                var tinh = "";
                var huyen = "";
                var xa = "";
                List<PHB_PBDT_QLDT_B01_DETAIL> lstDetail;
                try
                {
                    var dbhc = _dbhcService.Queryable().Where(db => db.MA_DBHC == form.MA_DBHC).FirstOrDefault();
                    tinh = _dbhcService.Queryable().Where(db => db.MA_DBHC == dbhc.MA_T).FirstOrDefault().TEN_DBHC;
                    huyen = _dbhcService.Queryable().Where(db => db.MA_DBHC == dbhc.MA_H).FirstOrDefault().TEN_DBHC;
                    xa = dbhc.TEN_DBHC;

                    lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_QLDT_B01_REFID == refId).OrderBy(tpl => tpl.STT_SAPXEP).ToList();
                }
                catch (Exception)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                    return Ok(response);
                }

                response.Data = new Detail_ViewModel
                {
                    Tinh = tinh,
                    Huyen = huyen,
                    Xa = xa,
                    Details = lstDetail
                };
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
            if (model == null || model.Form == null || model.Details == null || model.Details.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            //get informations by current user
            string nguoiTao = "";
            string maDBHC = "";
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                nguoiTao = identity.Name;
                maDBHC = _auService.Queryable().Where(u => u.USERNAME == nguoiTao).FirstOrDefault()?.MA_DBHC;
            }
            catch (Exception ex)
            {
                response.Error = true;
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            if (String.IsNullOrEmpty(maDBHC))
            {
                response.Error = true;
                response.Message = "Lỗi không tồn tại đơn vị tương ứng với người dùng hiện tại";
                return Ok(response);
            }

            // add form
            var form = new PHB_PBDT_QLDT_B01
            {
                MA_DBHC = maDBHC,
                NAM = model.Form.NAM,
                THANG = model.Form.THANG,
                NGAY_SUA = null,
                NGUOI_SUA = null,
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = nguoiTao,
                REFID = Guid.NewGuid().ToString("n"),
                TRANG_THAI = 0
            };

            // check if exist form
            var formCount = _formService.Queryable()
                .Where(f => f.MA_DBHC == form.MA_DBHC && f.NAM == form.NAM && f.THANG == form.THANG)
                .Count();
            if (formCount > 0)
            {
                response.Error = true;
                response.Message = $"Đã có báo cáo cho tháng {form.THANG} năm {form.NAM}";
                return Ok(response);
            }

            _formService.Insert(form);

            // add details
            foreach (var detail in model.Details)
            {
                detail.PHB_PBDT_QLDT_B01_REFID = form.REFID;
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

        //[Route("UploadExcel")]
        //[HttpPost]
        //public IHttpActionResult UploadExcel()
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

        //    //get informations by current user
        //    string nguoiTao = "";
        //    string maDBHC = "";
        //    int nam;
        //    try
        //    {
        //        nam = int.Parse(httpRequest["Nam"].ToString());

        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        nguoiTao = identity.Name;

        //        maDBHC = _auService.Queryable().Where(u => u.USERNAME == nguoiTao).FirstOrDefault()?.MA_DBHC;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Error = true;
        //        WriteLogs.LogError(ex);
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //        return Ok(response);
        //    }

        //    if (String.IsNullOrEmpty(maDBHC))
        //    {
        //        response.Error = true;
        //        response.Message = "Lỗi không tồn tại đơn vị tương ứng với người dùng hiện tại";
        //        return Ok(response);
        //    }

        //    // add form
        //    var form = new PHB_PBDT_QLDT_B01
        //    {
        //        MA_DBHC = maDBHC,
        //        NAM = nam,
        //        NGAY_SUA = null,
        //        NGUOI_SUA = null,
        //        NGAY_TAO = DateTime.Now,
        //        NGUOI_TAO = nguoiTao,
        //        REFID = Guid.NewGuid().ToString("n"),
        //        TRANG_THAI = 0
        //    };

        //    // check if exist form
        //    var formCount = _formService.Queryable()
        //        .Where(f => f.MA_DBHC == form.MA_DBHC && f.NAM == form.NAM)
        //        .Count();
        //    if (formCount > 0)
        //    {
        //        response.Error = true;
        //        response.Message = ErrorMessage.EXITS_REPORT;
        //        return Ok(response);
        //    }

        //    _formService.Insert(form);

        //    var lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
        //    var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.CHI_TIEU).ToList();

        //    //add detail
        //    using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
        //    {
        //        var sheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
        //        if (sheet == null)
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //            return Ok(response);
        //        }

        //        var startRowThu = 10;
        //        var endRowThu = 16;
        //        var stt_SapXep = 1;

        //        for (int row = startRowThu; row <= endRowThu; row++)
        //        {
        //            try
        //            {
        //                var detail = new PHB_PBDT_QLDT_B01_DETAIL
        //                {
        //                    STT = sheet.Cells[row, 1].Value?.ToString(),
        //                    CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
        //                    IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
        //                    IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,
        //                    PHB_PBDT_QLDT_B01_REFID = form.REFID,
        //                    STT_SAPXEP = stt_SapXep,
        //                    IS_OPTIONAL = 0,

        //                    DU_TOAN = decimal.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0")
        //                };

        //                _detailService.Insert(detail);
        //            }
        //            catch (Exception ex)
        //            {
        //                response.Error = true;
        //                response.Message = ErrorMessage.ERROR_DATA;
        //                return Ok(response);
        //            }

        //            stt_SapXep++;
        //        }

        //        var startRowChi = 10;
        //        var endRowChi = 13;

        //        for (int row = startRowChi; row <= endRowChi; row++)
        //        {
        //            try
        //            {
        //                var detail = new PHB_PBDT_QLDT_B01_DETAIL
        //                {
        //                    STT = sheet.Cells[row, 4].Value?.ToString(),
        //                    CHI_TIEU = sheet.Cells[row, 5].Value?.ToString(),
        //                    IS_BOLD = sheet.Cells[row, 5].Style.Font.Bold ? 1 : 0,
        //                    IS_ITALIC = sheet.Cells[row, 5].Style.Font.Italic ? 1 : 0,
        //                    PHB_PBDT_QLDT_B01_REFID = form.REFID,
        //                    STT_SAPXEP = stt_SapXep,
        //                    IS_OPTIONAL = 0,

        //                    DU_TOAN = decimal.Parse(sheet.Cells[row, 6].Value?.ToString() ?? "0")
        //                };

        //                _detailService.Insert(detail);
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

        //    //save
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

        [Route("Edit")]
        [HttpPost]
        public IHttpActionResult Edit(Edit_ViewModel model)
        {
            var response = new Response();

            var form = new PHB_PBDT_QLDT_B01();
            var lstDetail = new List<PHB_PBDT_QLDT_B01_DETAIL>();

            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == model.FormId).FirstOrDefault();
                lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_QLDT_B01_REFID == form.REFID).ToList();
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
                    detail.PHB_PBDT_QLDT_B01_REFID = form.REFID;
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
            var form = new PHB_PBDT_QLDT_B01();
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
            var lstDetail = new List<PHB_PBDT_QLDT_B01_DETAIL>();
            try
            {
                lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_QLDT_B01_REFID == form.REFID).ToList();
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
            var form = new PHB_PBDT_QLDT_B01();
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
            var form = new PHB_PBDT_QLDT_B01();
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
    }
}
