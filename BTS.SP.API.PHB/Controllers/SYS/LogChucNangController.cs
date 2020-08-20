using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.BuildQuery.Result;
using BTS.SP.PHB.SERVICE.SYS.SysChucNang;
using BTS.SP.PHB.SERVICE.SYS.sysLogChucNang;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.API.PHB.ViewModels.SYS.sysLogChucNang;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/log")]
    [Route("{id?}")]
    [Authorize]
    public class LogChucNangController : ApiController
    {
        private readonly ISysLogChucNangService _logService;
        private readonly IDmDBHCService _dbhcService;
        private readonly IAuNguoiDungService _auService;
        private readonly ISysChucNangService _sysChucNangservice;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public LogChucNangController(
            ISysLogChucNangService logService,
            IDmDBHCService dbhcService,
            IAuNguoiDungService auService,
            ISysChucNangService sysChucNangservice,
            IUnitOfWorkAsync unitOfWork)
        {
            _logService = logService;
            _dbhcService = dbhcService;
            _sysChucNangservice = sysChucNangservice;
            _unitOfWork = unitOfWork;
            _auService = auService;
        }

        [Route("Search")]
        [HttpPost]
        public IHttpActionResult Search(SearchViewModel model)
        {
            var response = new Response<PagedObj<LogSigninViewModel>>();
            IQueryable<PHB_SYS_LOG_CHUCNANG> queryLog = _logService.Queryable().Where(log => log.ThoiGianTruyCap >= model.FromDate && log.ThoiGianTruyCap <= model.ToDate);

            //lấy hết chức năng 
            //lấy hết địa bàn hành chính
            var lstChucNang = new List<SYS_CHUCNANG>();
            try
            {
                lstChucNang = _sysChucNangservice.GetAll();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            //lấy mã tỉnh của user đang đăng nhập
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentUser = _auService.Queryable().Where(u => u.USERNAME == identity.Name).FirstOrDefault();
            if (currentUser == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            var dbhc = _dbhcService.Queryable().Where(diaBan => diaBan.MA_DBHC == currentUser.MA_DBHC).FirstOrDefault();
            if (dbhc == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            var maTinh = dbhc.MA_DBHC_CHA ?? dbhc.MA_DBHC;

            //lọc logs trong cùng tỉnh
            queryLog = queryLog.Where(log => log.DBHC_Cha == maTinh);

            //tìm kiếm logs theo user name
            if (!string.IsNullOrEmpty(model.Search_UserName))
            {
                queryLog = queryLog.Where(log => log.Username.ToLower().Trim().Contains(model.Search_UserName.Trim().ToLower()));
            }

            //tìm kiếm logs theo ip
            if (!string.IsNullOrEmpty(model.Search_IP))
            {
                queryLog = queryLog.Where(log => log.DiaChiMay.ToLower().Trim().Contains(model.Search_IP.Trim().ToLower()));
            }

            //tìm kiếm theo chức năng
            if (!string.IsNullOrEmpty(model.Search_Action))
            {
                var lstMaChucNang = lstChucNang.Where(chucNang => chucNang.TENCHUCNANG.Trim().ToLower().Contains(model.Search_Action.Trim().ToLower())).Select(chucNang => chucNang.MACHUCNANG).ToList();
                queryLog = queryLog.Where(log => lstMaChucNang.Contains(log.ChucNang));
            }

            //tìm kiếm theo mã địa bàn
            if (!string.IsNullOrEmpty(model.Search_DBHC.Trim()))
            {
                queryLog = queryLog.Where(log => log.DBHC == model.Search_DBHC);
            }

            switch (model.SortBy)
            {
                case SortBy.Time:
                    queryLog = queryLog.OrderByDescending(log => log.ThoiGianTruyCap);
                    break;
                case SortBy.UserName:
                    queryLog = queryLog.OrderBy(log => log.Username);
                    break;
                case SortBy.IP:
                    queryLog = queryLog.OrderBy(log => log.DiaChiMay);
                    break;
                case SortBy.Action:
                    queryLog = queryLog.OrderBy(log => log.ChucNang);
                    break;
                default:
                    queryLog = queryLog.OrderByDescending(log => log.ThoiGianTruyCap);
                    break;
            }

            var logCount = queryLog.Count();

            queryLog = queryLog.Skip((model.Page.currentPage - 1) * model.Page.itemsPerPage).Take(model.Page.itemsPerPage);

            List<PHB_SYS_LOG_CHUCNANG> lstLogs = new List<PHB_SYS_LOG_CHUCNANG>();
            try
            {
                lstLogs = queryLog.ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = "Lỗi hệ thống";
                return Ok(response);
            }

            var pagedObj = new PagedObj<LogSigninViewModel>()
            {
                Data = new List<LogSigninViewModel>(),
                currentPage = model.Page.currentPage,
                itemsPerPage = model.Page.itemsPerPage,
                totalItems = logCount,
                takeAll = model.Page.takeAll
            };

            foreach (var item in lstLogs)
            {
                var chucNang = lstChucNang.Where(cn => cn.MACHUCNANG == item.ChucNang).FirstOrDefault();
                var chucNangTitle = chucNang == null ? item.ChucNang : chucNang.TENCHUCNANG;

                var user = _auService.Queryable().Where(u => u.USERNAME == item.Username).FirstOrDefault();

                var itemVm = new LogSigninViewModel
                {
                    Username = item.Username,
                    ChucNang = chucNangTitle,
                    DiaChiMay = item.DiaChiMay,
                    DBHC = item.Ten_DBHC,
                    ThoiGianTruyCap = item.ThoiGianTruyCap
                };

                pagedObj.Data.Add(itemVm);
            }

            response.Data = pagedObj;
            return Ok(response);
        }

        [Route("LogChucNang")]
        [HttpPost]
        public void LogChucNang([FromBody]dynamic data)
        {
            string chucNang = data.chucNang;
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            if (chucNang == null || chucNang.Trim() == "")
            {
                return;
            }

            var userName = "";
            var diaChiMay = "";
            try
            {
                userName = identity.Name;
                diaChiMay = HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception)
            {
                return;
            }
            
            var currentMA_DBHC = identity.Claims.FirstOrDefault(c => c.Type == "MA_DBHC").Value.ToString();
            var dbhc = _dbhcService.Queryable().Where(diaBan => diaBan.MA_DBHC == currentMA_DBHC).FirstOrDefault();
            if (dbhc == null)
            {
                return;
            }

            var maDBHC = dbhc.MA_DBHC;
            var tenDBHC = dbhc.TEN_DBHC;
            var maDBHC_Cha = dbhc.MA_DBHC_CHA ?? dbhc.MA_DBHC;

            var log = new PHB_SYS_LOG_CHUCNANG
            {
                Username = userName,
                ThoiGianTruyCap = DateTime.Now,
                ChucNang = chucNang,
                DiaChiMay = diaChiMay,
                DBHC = maDBHC,
                Ten_DBHC = tenDBHC,
                DBHC_Cha = maDBHC_Cha
            };

            try
            {
                _logService.Insert(log);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
        }

        [Route("GetListDBHC")]
        [HttpGet]
        public IHttpActionResult GetListDBHC()
        {
            var response = new Response<List<DM_DBHC>>();

            //lấy mã tỉnh của user đang đăng nhập
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentUser = _auService.Queryable().Where(u => u.USERNAME == identity.Name).FirstOrDefault();
            if (currentUser == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            var dbhc = _dbhcService.Queryable().Where(diaBan => diaBan.MA_DBHC == currentUser.MA_DBHC).FirstOrDefault();
            if (dbhc == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            var maTinh = dbhc.MA_DBHC_CHA ?? dbhc.MA_DBHC;

            var lstDBHC = new List<DM_DBHC>();

            try
            {
                lstDBHC = _dbhcService.Queryable().Where(diaBan => diaBan.MA_DBHC_CHA == maTinh || diaBan.MA_DBHC == maTinh).ToList();
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Data = lstDBHC;
            return Ok(response);
        }
    }
}