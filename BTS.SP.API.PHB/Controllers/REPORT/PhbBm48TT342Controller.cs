using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.SERVICE.REPORT.Bm48TT342;
using BTS.SP.API.PHB.ViewModels.PBDT.Main;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.SERVICE.REPORT.B01BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Types;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/phb/pbdt/tt342/BM48_TT342")]
    [Route("{id?}")]
    public class PhbBm48TT342Controller : ApiController
    {

        private readonly IPhbBm48TT342Service _bm48TT342Service;
        private readonly IPhbBm48TT342TemplateService _bm48TT342TemplateService;
        private readonly IPhbBm48TT342DetailService _bm48TT342DetailService;
        private readonly IAuNguoiDungService _auService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbBm48TT342Controller(IPhbBm48TT342Service bm48TT342Service, IPhbBm48TT342TemplateService bm48TT342TemplateService,
            IPhbBm48TT342DetailService bm48TT342DetailService, IAuNguoiDungService auService, ISysDvqhns_QuanLyService sysDVQHNSQLService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _bm48TT342Service = bm48TT342Service;
            _bm48TT342TemplateService = bm48TT342TemplateService;
            _bm48TT342DetailService = bm48TT342DetailService;
            _auService = auService;
            _sysDVQHNSQLService = sysDVQHNSQLService;

            _unitOfWorkAsync = unitOfWorkAsync;
        }

        #region Upload
        [Route("UploadExcel")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReportxa()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var lstLaMa = new List<string> { "I", "II", "III", "IV" };
                        var lstSo = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                        var lstSoThuHai = new List<string> { "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9" ,
                                                            "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9",
                                                            "3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9" ,
                                                            "4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9" ,
                                                            "5.1", "5.2", "5.3", "5.4", "5.5", "5.6", "5.7", "5.8", "5.9" ,
                                                            "6.1", "6.2", "6.3", "6.4", "6.5", "6.6", "6.7", "6.8", "6.9" ,
                                                            "7.1", "7.2", "7.3", "7.4", "7.5", "7.6", "7.7", "7.8", "7.9" ,
                                                            "8.1", "8.2", "8.3", "8.4", "8.5", "8.6", "8.7", "8.8", "8.9" ,
                                                            "9.1", "9.2", "9.3", "9.4", "9.5", "9.6", "9.7", "9.8", "9.9" ,
                                                            "10.1", "10.2", "10.3", "10.4", "10.5", "10.6", "10.7", "10.8", "10.9" ,
                                                            "11.1", "11.2", "11.3", "11.4", "11.5", "11.6", "11.7", "11.8", "11.9" ,
                                                            "12.1", "12.2", "12.3", "12.4", "12.5", "12.6", "12.7", "12.8", "12.9" };


                        var b01Bcqt = new PHB_BM48_TT342()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };

                        var workSheet = excelPackage.Workbook.Worksheets["BM48TT342"];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(workSheet.Cells[2, 2].Text)) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã chương."
                            });
                            
                            b01Bcqt.MA_CHUONG = workSheet.Cells[2, 2].Text;
                            if (string.IsNullOrEmpty(httpRequest["MA_QHNS"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có mã đơn vị QHNS."
                            });
                            b01Bcqt.MA_QHNS = httpRequest["MA_QHNS"];
                            //b01Bcqt.TEN_QHNS = httpRequest["TEN_QHNS"];
                            //b01Bcqt.MA_QHNS_CHA = httpRequest["MA_QHNS_CHA"];
                            if (string.IsNullOrEmpty(httpRequest["Nam"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01Bcqt.NAM_BC = int.Parse(httpRequest["Nam"]);
                            //if (string.IsNullOrEmpty(httpRequest["KY_BC"])) return Ok(new Response()
                            //{
                            //    Error = true,
                            //    Message = "Không có kỳ báo cáo."
                            //});
                            //b01Bcqt.KY_BC = int.Parse(httpRequest["KY_BC"]);

                            var checkReport = await _bm48TT342Service.Queryable().FirstOrDefaultAsync(x =>
                                x.MA_CHUONG.Equals(b01Bcqt.MA_CHUONG) && x.MA_QHNS.Equals(b01Bcqt.MA_QHNS) &&
                                x.NAM_BC == b01Bcqt.NAM_BC && x.KY_BC == b01Bcqt.KY_BC);
                            if (checkReport != null) return Ok(new Response() { Error = true, Message = ErrorMessage.EXITS_REPORT });
                            _bm48TT342Service.Insert(b01Bcqt);

                            int start_Row = 9;
                            int end_Row = 66;
                            int start_Col = 3;
                            int count = 1;
                            int sTT_Cha = 0;
                            string currentLama = "";
                            string currentSo = "";
                            string currentSoHai = "";

                            for (int r = start_Row ; r <= end_Row; r++)
                            {
                                var obj = new PHB_BM48_TT342_DETAIL() { PHB_BM48_TT342_REFID = b01Bcqt.REFID, ObjectState = ObjectState.Added };
                                var tongso = 0;
                                obj.STT = count;
                                obj.STT_CHI_TIEU = workSheet.Cells[r, 1].Value?.ToString();
                                if (lstLaMa.Contains(obj.STT_CHI_TIEU))
                                {
                                    currentLama = obj.STT_CHI_TIEU;
                                    obj.MA_CHI_TIEU = "";

                                }
                                else if (lstSo.Contains(obj.STT_CHI_TIEU))
                                {
                                    currentSo = obj.STT_CHI_TIEU;
                                    obj.MA_CHI_TIEU = currentLama;
                                }
                                else if (lstSoThuHai.Contains(obj.STT_CHI_TIEU))
                                {
                                    currentSoHai = obj.STT_CHI_TIEU;
                                    obj.MA_CHI_TIEU = currentLama + "-" + currentSo;
                                }
                                else 
                                {
                                    obj.MA_CHI_TIEU = currentLama + "-" + currentSo + "-" + currentSoHai;
                                }

                                obj.TEN_CHI_TIEU = workSheet.Cells[r, 2].Text;
                                int.TryParse(workSheet.Cells[r, 3].Text.ToString(), out tongso);
                                obj.TONG_SO = tongso;
                                _bm48TT342DetailService.Insert(obj);
                                count++;
                            }

                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b01Bcqt.REFID;
                                response.Error = false;
                                response.Message = "Cập nhật thành công.";
                            }
                            else
                            {
                                response.Error = true;
                                response.Message = "Lỗi cập nhật dữ liệu.";
                            }
                        }
                        else
                        {
                            response.Error = true;
                            response.Message = "Lỗi định dạng dữ liệu.";
                        }
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "Không có dữ liệu upload.";
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        #endregion


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

            foreach (var donVi in lstDonVi)
            {
                var lstForms = new List<PHB_BM48_TT342>();
                var lstFormViewModel = new List<FormViewModel>();

                //get list forms
                try
                {
                    lstForms = _bm48TT342Service.Queryable().Where(form => form.NAM_BC == nam && form.MA_QHNS == donVi.MA_DVQHNS).ToList();
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
                        Nam = form.NAM_BC,
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
    }
}