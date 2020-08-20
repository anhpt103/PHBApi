using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using word = Microsoft.Office.Interop.Word;
using excel = Microsoft.Office.Interop.Excel;
using System.IO;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHA_B04_BCTC")]
    [Route("{id?}")]
    public class PhaB04BCTCController : ApiController
    {
        private readonly IPhaB04BCTCService _B04BCTCService;
        private readonly IPhaB04BCTCDetailService _B04BCTCDetailService;
        private readonly IPhaB04BCTCTemplateService _B04BCTCTemplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        public PhaB04BCTCController(IPhaB04BCTCService B04BCTCService, IPhaB04BCTCDetailService B04BCTCDetailService, IPhaB04BCTCTemplateService B04BCTCTemplateService, IDmDVQHNSService serviceDMDVQHNS, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService)
        {
            _B04BCTCService = B04BCTCService;
            _B04BCTCDetailService = B04BCTCDetailService;
            _B04BCTCTemplateService = B04BCTCTemplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _unitOfWorkAsync = unitOfWorkAsync;
            _sysDVQHNSQLService = sysDVQHNSQLService;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(PHA_B04_BCTCVm.ContentData item)
        {
            var response = new Response<List<PHA_B04_BCTCVm.ContentData>>();
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
                var StrDONVI = currentMA_QHNS.Split(',');
                var data = _B04BCTCService.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                var result = new List<PHA_B04_BCTCVm.ContentData>();
                if (data.Count > 0)
                {
                    foreach (var dataObj in data)
                    {
                        var tempResult = new PHA_B04_BCTCVm.ContentData();
                        tempResult.MA_DONVI = dataObj.MA_DONVI;
                        tempResult.REFID = dataObj.REFID;
                        tempResult.NAM = dataObj.NAM;
                        tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                        tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                        tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                        tempResult.TRANG_THAI = item.TRANG_THAI;
                        tempResult.TRANG_THAI_GUI = item.TRANG_THAI_GUI;

                        var tempDVSDNS = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
                        if (tempDVSDNS != null)
                        {
                            tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
                        }
                        result.Add(tempResult);
                    }
                }
                response.Data = result;
                response.Error = false;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            Response<List<PHA_B04_BCTC_TEMPLATE>> response = new Response<List<PHA_B04_BCTC_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _B04BCTCTemplateService.Queryable()
                    .OrderBy(x => x.STT_SAPXEP)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("Detail/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(string refid)
        {
            var response = new Response<List<PHA_B04_BCTC_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _B04BCTCDetailService.Queryable()
                    .Where(detail => detail.PHA_B04_BCTC_REFID == refid)
                    .OrderBy(detail => detail.STT_SAPXEP)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_UPDATE_DATA;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHA_B04_BCTC_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHA_B04_BCTC();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHA_B04_BCTC_REFID;
                report = await _B04BCTCService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }
            if (report == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //check if report is already censored or not
            if (report.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
                response.Error = true;
                return Ok(response);
            }

            //add informations about editing user and editing date
            report.NGAY_SUA = DateTime.Now;
            report.NGUOI_SUA = RequestContext.Principal.Identity.Name;
            report.ObjectState = ObjectState.Modified;
            _B04BCTCService.Update(report);

            //get list details by refid
            var lstDetail = new List<PHA_B04_BCTC_DETAIL>();
            try
            {
                lstDetail = await _B04BCTCDetailService.Queryable().Where(detail => detail.PHA_B04_BCTC_REFID == report.REFID).ToListAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            //loop to edit each detail
            foreach (var detail in lstDetail)
            {
                detail.SO_DAU_NAM = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_DAU_NAM;
                detail.SO_CUOI_NAM = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_CUOI_NAM;
                detail.TONG_CONG = model.Where(e => e.ID == detail.ID).FirstOrDefault().TONG_CONG;
                detail.TSCD_HUU_HINH = model.Where(e => e.ID == detail.ID).FirstOrDefault().TSCD_HUU_HINH;
                detail.TSCD_VO_HINH = model.Where(e => e.ID == detail.ID).FirstOrDefault().TSCD_VO_HINH;
                detail.NGUON_VON_KD = model.Where(e => e.ID == detail.ID).FirstOrDefault().NGUON_VON_KD;
                detail.CHENH_LECH_TY_GIA = model.Where(e => e.ID == detail.ID).FirstOrDefault().CHENH_LECH_TY_GIA;
                detail.THANG_DU_LUY_KE = model.Where(e => e.ID == detail.ID).FirstOrDefault().THANG_DU_LUY_KE;
                detail.CAC_QUY = model.Where(e => e.ID == detail.ID).FirstOrDefault().CAC_QUY;
                detail.CAI_CACH_TIEN_LUON = model.Where(e => e.ID == detail.ID).FirstOrDefault().CAI_CACH_TIEN_LUON;
                detail.KHAC = model.Where(e => e.ID == detail.ID).FirstOrDefault().KHAC;
                detail.CONG = model.Where(e => e.ID == detail.ID).FirstOrDefault().CONG;
                detail.NAM_NAY = model.Where(e => e.ID == detail.ID).FirstOrDefault().NAM_NAY;
                detail.NAM_TRUOC = model.Where(e => e.ID == detail.ID).FirstOrDefault().NAM_TRUOC;

                detail.ObjectState = ObjectState.Modified;
                _B04BCTCDetailService.Update(detail);
            }

            try
            {
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

        [Route("Delete/{refid}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(string refid)
        {
            var response = new Response<string>();

            //get report by refid
            var report = new PHA_B04_BCTC();
            try
            {
                report = await _B04BCTCService.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }
            if (report == null)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            //check if report is already censored or not
            if (report.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể xóa!";
                response.Error = true;
                return Ok(response);
            }

            //get list details by refid
            var lstDetail = new List<PHA_B04_BCTC_DETAIL>();
            try
            {
                lstDetail = await _B04BCTCDetailService.Queryable().Where(detail => detail.PHA_B04_BCTC_REFID == refid).ToListAsync();
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
                _B04BCTCService.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _B04BCTCDetailService.Delete(detail);
                }

                await _unitOfWorkAsync.SaveChangesAsync();
                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            return Ok();
        }

    }
}