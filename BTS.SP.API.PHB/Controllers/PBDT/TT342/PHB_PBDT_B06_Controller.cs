using BTS.SP.API.PHB.ViewModels.PBDT.B06;
using BTS.SP.API.PHB.ViewModels.PBDT.Main;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.PBDT.B06;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.PBDT.B06;
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
    [RoutePrefix("api/phb/pbdt/tt342/PHB_PBDT_TT342_B06")]
    public class PHB_PBDT_B06_Controller : ApiController
    {
        private readonly IPHB_PBDT_B06_Service _formService;
        private readonly IPHB_PBDT_B06_TEMPLATE_Service _templateService;
        private readonly IPHB_PBDT_B06_DETAIL_Service _detailService;
        private readonly IPHB_PBDT_B06_DATA_Service _dataService;
        private readonly IPHB_PBDT_B06_DONVI_Service _donviService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;

        private readonly IAuNguoiDungService _auService;
        private readonly IDmDVQHNSService _dvqhnsService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public PHB_PBDT_B06_Controller(
            IPHB_PBDT_B06_Service service,
            IPHB_PBDT_B06_TEMPLATE_Service templateService,
            IPHB_PBDT_B06_DETAIL_Service detailService,
            IPHB_PBDT_B06_DATA_Service dataService,
            IPHB_PBDT_B06_DONVI_Service donviService,

            IAuNguoiDungService auService,
            IDmDVQHNSService dvqhnsService,
            ISysDvqhns_QuanLyService sysDVQHNSQLService,

            IUnitOfWorkAsync unitOfWork)
        {
            _formService = service;
            _templateService = templateService;
            _detailService = detailService;
            _dataService = dataService;
            _donviService = donviService;

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
                var lstForms = new List<PHB_PBDT_B06>();
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
            var response = new Response<Detail_ViewModel>();
            response.Data = new Detail_ViewModel
            {
                DonVis = new List<PHB_PBDT_B06_DONVI>(),
                Rows = new List<Row_ViewModel>()
            };

            try
            {
                var lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
                var donvi = new PHB_PBDT_B06_DONVI
                {
                    DONVI_REFID = Guid.NewGuid().ToString(),
                    STT = 1,
                    TEN_DON_VI = "Đơn vị"
                };

                response.Data.DonVis.Add(donvi);

                foreach(var template in lstTemplate)
                {
                    var detail = new PHB_PBDT_B06_DETAIL
                    {
                        CHI_TIEU = template.CHI_TIEU,
                        IS_BOLD = template.IS_BOLD,
                        IS_ITALIC = template.IS_ITALIC,
                        IS_OPTIONAL = template.IS_OPTIONAL,
                        MA_CHA = template.MA_CHA,
                        MA_SO = template.MA_SO,
                        STT = template.STT,
                        STT_SAPXEP = template.STT_SAPXEP,

                        DETAIL_REFID = Guid.NewGuid().ToString()
                    };

                    var row_ViewModel = new Row_ViewModel
                    {
                        Detail = detail,
                        Datas = new List<PHB_PBDT_B06_DATA>
                        {
                            new PHB_PBDT_B06_DATA
                            {
                                DETAIL_REFID = detail.DETAIL_REFID,
                                DONVI_REFID = donvi.DONVI_REFID
                            }
                        }
                    };

                    response.Data.Rows.Add(row_ViewModel);
                }
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
            response.Data = new Detail_ViewModel
            {
                DonVis = new List<PHB_PBDT_B06_DONVI>(),
                Rows = new List<Row_ViewModel>()
            };

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

                // get list details
                var lstDetail = _detailService.Queryable()
                    .Where(detail => detail.PHB_PBDT_B06_REFID == refId)
                    .OrderBy(detail => detail.STT_SAPXEP)
                    .ToList();

                // get list donvis
                var lstDonvi = _donviService.Queryable()
                    .Where(donvi => donvi.PHB_PBDT_B06_REFID == form.REFID)
                    .OrderBy(donvi => donvi.STT)
                    .ToList();
                response.Data.DonVis = lstDonvi;

                // get list datas of each detail
                foreach(var detail in lstDetail)
                {
                    var lstData = new List<PHB_PBDT_B06_DATA>();
                    foreach (var donvi in lstDonvi)
                    {
                        var data = _dataService.Queryable()
                            .Where(d => d.DETAIL_REFID == detail.DETAIL_REFID && d.DONVI_REFID == donvi.DONVI_REFID).FirstOrDefault();

                        lstData.Add(data);
                    }

                    var row = new Row_ViewModel
                    {
                        Detail = detail,
                        Datas = lstData
                    };

                    response.Data.Rows.Add(row);
                }
                
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
            if(model?.Form == null || model.Detail_ViewModel?.Rows == null || model.Detail_ViewModel.Rows.Count == 0)
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
            var form = new PHB_PBDT_B06
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

            // add don vi
            var lstDonvi = model.Detail_ViewModel.DonVis;
            foreach(var donvi in lstDonvi)
            {
                donvi.DONVI_REFID = Guid.NewGuid().ToString();
                donvi.PHB_PBDT_B06_REFID = form.REFID;

                _donviService.Insert(donvi);
            }

            // add details
            foreach (var row in model.Detail_ViewModel.Rows)
            {
                var detail = row.Detail;
                detail.PHB_PBDT_B06_REFID = form.REFID;
                detail.DETAIL_REFID = Guid.NewGuid().ToString();

                _detailService.Insert(row.Detail);

                //add data for each detail
                var indexLoop = 0;
                foreach (var data in row.Datas)
                {
                    data.DETAIL_REFID = detail.DETAIL_REFID;
                    data.DONVI_REFID = lstDonvi[indexLoop].DONVI_REFID;

                    _dataService.Insert(data);
                    indexLoop++;
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

            // create form
            var form = new PHB_PBDT_B06
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

            // add form
            _formService.Insert(form);

            var lstTemplate = _templateService.Queryable().OrderBy(tpl => tpl.STT_SAPXEP).ToList();
            var lstChiTieuTemplate = lstTemplate.Select(tpl => tpl.CHI_TIEU).ToList();
            
            // read file excel
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
                var endCol = sheet.Dimension.End.Column;
                var startCol_Donvi = 5;
                var stt_SapXep = 1;

                // read don vi
                var lstDonvi = new List<PHB_PBDT_B06_DONVI>();
                var stt = 1;
                for (var col = startCol_Donvi; col < endCol; col += 2)
                {
                    if (sheet.Cells[7, col].Value?.ToString().Trim().ToLower().StartsWith("đơn vị")?? false)
                    {
                        lstDonvi.Add(new PHB_PBDT_B06_DONVI
                        {
                            PHB_PBDT_B06_REFID = form.REFID,
                            DONVI_REFID = Guid.NewGuid().ToString(),
                            STT = stt,
                            TEN_DON_VI = sheet.Cells[7, col].Value?.ToString().Trim()
                        });

                        stt++;
                    }
                }
                //add don vi
                _donviService.InsertRange(lstDonvi);

                for(int row = startRow; row <= endRow; row++)
                {
                    try
                    {
                        // insert detail
                        var detail = new PHB_PBDT_B06_DETAIL
                        {
                            STT = sheet.Cells[row, 1].Value?.ToString(),
                            CHI_TIEU = sheet.Cells[row, 2].Value?.ToString(),
                            IS_BOLD = sheet.Cells[row, 2].Style.Font.Bold ? 1 : 0,
                            IS_ITALIC = sheet.Cells[row, 2].Style.Font.Italic ? 1 : 0,

                            TONGSO_UOC_THUC_HIEN = decimal.Parse(sheet.Cells[row, 3].Value?.ToString() ?? "0"),
                            TONGSO_DU_TOAN = decimal.Parse(sheet.Cells[row, 4].Value?.ToString() ?? "0"),

                            PHB_PBDT_B06_REFID = form.REFID,
                            STT_SAPXEP = stt_SapXep,
                            IS_OPTIONAL = 0,

                            DETAIL_REFID = Guid.NewGuid().ToString()
                        };

                        _detailService.Insert(detail);

                        if (IsOptional(lstTemplate, detail.CHI_TIEU))
                        {
                            detail.IS_OPTIONAL = 1;
                        }

                        var indexDonvi = 0;
                        foreach(var donvi in lstDonvi)
                        {
                            var data = new PHB_PBDT_B06_DATA
                            {
                                DETAIL_REFID = detail.DETAIL_REFID,
                                DONVI_REFID = donvi.DONVI_REFID,
                                UOC_THUC_HIEN = decimal.Parse(sheet.Cells[row, startCol_Donvi + indexDonvi].Value?.ToString() ?? "0"),
                                DU_TOAN = decimal.Parse(sheet.Cells[row, startCol_Donvi + indexDonvi + 1].Value?.ToString() ?? "0"),
                            };

                            _dataService.Insert(data);

                            indexDonvi += 2;
                        }
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

            var form = new PHB_PBDT_B06();
            var lstDetail = new List<PHB_PBDT_B06_DETAIL>();

            try
            {
                form = _formService.Queryable().Where(frm => frm.ID == model.FormId).FirstOrDefault();
                lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_B06_REFID == form.REFID).ToList();
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

            // remove all old donvi
            var lstOldDonvi = _donviService.Queryable().Where(donvi => donvi.PHB_PBDT_B06_REFID == form.REFID).ToList();
            foreach(var donvi in lstOldDonvi)
            {
                _donviService.Delete(donvi);
            }

            // add all new don vi
            var lstDonvi = model.Detail_ViewModel.DonVis;
            foreach(var donvi in lstDonvi)
            {
                donvi.ObjectState = ObjectState.Added;
                donvi.ID = 0;
                donvi.PHB_PBDT_B06_REFID = form.REFID;
                donvi.DONVI_REFID = Guid.NewGuid().ToString();

                _donviService.Insert(donvi);
            }

            try
            {
                // delete all old details
                foreach (var detail in lstDetail)
                {
                    // delete all old detail_donvi
                    var lstDetailDonVi = _dataService.Queryable()
                        .Where(d => d.DETAIL_REFID == detail.DETAIL_REFID).ToList();

                    foreach(var detailDonVi in lstDetailDonVi)
                    {
                        _dataService.Delete(detailDonVi);
                    }

                    _detailService.Delete(detail);
                }

                // add new details
                foreach (var row in model.Detail_ViewModel.Rows)
                {
                    var detail = row.Detail;

                    detail.PHB_PBDT_B06_REFID = form.REFID;
                    detail.DETAIL_REFID = Guid.NewGuid().ToString();
                    detail.ObjectState = ObjectState.Added;
                    detail.ID = 0;

                    _detailService.Insert(detail);

                    //add new data
                    var dataIndex = 0;
                    foreach (var d in row.Datas)
                    {
                        var data = new PHB_PBDT_B06_DATA
                        {
                            DETAIL_REFID = row.Detail.DETAIL_REFID,
                            DONVI_REFID = lstDonvi[dataIndex].DONVI_REFID,
                            DU_TOAN = d?.DU_TOAN,
                            UOC_THUC_HIEN = d?.UOC_THUC_HIEN,
                            ID = 0
                        };

                        _dataService.Insert(data);
                        dataIndex++;
                    }
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
            var form = new PHB_PBDT_B06();
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

            //delete report 
            try
            {
                // delete list don vi
                var lstDonvi = _donviService.Queryable().Where(donvi => donvi.PHB_PBDT_B06_REFID == form.REFID);
                foreach(var donvi in lstDonvi)
                {
                    _donviService.Delete(donvi);
                }

                var lstDetail = _detailService.Queryable().Where(detail => detail.PHB_PBDT_B06_REFID == form.REFID).ToList();
                //delete list details
                foreach (var detail in lstDetail)
                {
                    //delete list detail_donvi
                    var lstData = _dataService.Queryable().Where(detailDv => detailDv.DETAIL_REFID == detail.DETAIL_REFID).ToList();
                    foreach(var data in lstData)
                    {
                        _dataService.Delete(data);
                    }

                    _detailService.Delete(detail);
                }

                _formService.Delete(form);

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
            var form = new PHB_PBDT_B06();
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
            var form = new PHB_PBDT_B06();
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

        private bool IsOptional(List<PHB_PBDT_B06_TEMPLATE> lstChiTieuTemplate, string chiTieu)
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
