using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Repository.Pattern.Infrastructure;
using OfficeOpenXml;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using System.Security.Claims;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHB_B01_BSTT_2")]
    [Route("{id?}")]
    public class PhbB01BSTT2Controller : ApiController
    {
        private readonly IPhbB01BSTT2Service _PhbB01BSTT2Service;
        private readonly IPhbB01BSTT2DetailService _PhbB01BSTT2DetailService;
        private readonly IPhbB01BSTT2TemplateService _PhbB01BSTT2TemplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB01BSTT2Controller(
            IPhbB01BSTT2Service PhbB01BSTT2Service,
            IPhbB01BSTT2DetailService PhbB01BSTT2DetailService,
            IPhbB01BSTT2TemplateService PhbB01BSTT2TemplateService,
            IDmDVQHNSService serviceDMDVQHNS,
            ISysDvqhns_QuanLyService sysDVQHNSQLService,
            ISysDvqhnsService sysDvqhnsService,
        IUnitOfWorkAsync unitOfWorkAsync)
        {
            _PhbB01BSTT2Service = PhbB01BSTT2Service;
            _PhbB01BSTT2DetailService = PhbB01BSTT2DetailService;
            _PhbB01BSTT2TemplateService = PhbB01BSTT2TemplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        //[Route("PostQuery")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostQuery(PHB_B01_BSTT_2Vm.ContentData item)
        //{
        //    var response = new Response<List<PHB_B01_BSTT_2Vm.ContentData>>();
        //    try
        //    {
        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
        //        var StrDONVI = currentMA_QHNS.Split(',');
        //        var data = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
        //        var result = new List<PHB_B01_BSTT_2Vm.ContentData>();
        //        if (data.Count > 0)
        //        {
        //            foreach (var dataObj in data)
        //            {
        //                var tempResult = new PHB_B01_BSTT_2Vm.ContentData();
        //                tempResult.MA_DONVI = dataObj.MA_DONVI;
        //                tempResult.REFID = dataObj.REFID;
        //                tempResult.NAM = dataObj.NAM;
        //                tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
        //                tempResult.NGAY_SUA = dataObj.NGAY_SUA;
        //                tempResult.NGAY_TAO = dataObj.NGAY_TAO;
        //                tempResult.TRANG_THAI = item.TRANG_THAI;
        //                tempResult.TRANG_THAI_GUI = item.TRANG_THAI_GUI;

        //                var tempDVSDNS = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
        //                if (tempDVSDNS != null)
        //                {
        //                    tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
        //                }
        //                result.Add(tempResult);
        //            }
        //        }
        //        response.Data = result;
        //        response.Error = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);
        //}

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(PHB_B01_BSTT_2Vm.ContentData item)
        {
            var response = new Response<List<PHB_B01_BSTT_2Vm.ContentData>>();
            var result = new List<PHB_B01_BSTT_2Vm.ContentData>();
            try
            {
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
                var currentMA_DVQHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_DONVI").Value.ToString();
                var StrDONVI = currentMA_QHNS.Split(',');
                var tempDVQHNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS);
                var lstTempDVQHNS = _sysDVQHNSService.Queryable().Where(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS).ToList();

                if ((item.MA_DONVI == "Tất cả" || item.MA_DONVI == "") && tempDVQHNS != null)
                {
                    var dataCha = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B01_BSTT_2Vm.ContentData();
                            tempResult.MA_DONVI = dataObj.MA_DONVI;
                            tempResult.REFID = dataObj.REFID;
                            tempResult.NAM = dataObj.NAM;
                            tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                            tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                            tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                            tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                            tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                            tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                            var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
                            if (tempDVSDNS != null)
                            {
                                tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
                                tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                            }

                            result.Add(tempResult);
                        }
                    }

                    if (lstTempDVQHNS.Count > 0)
                    {
                        foreach (var dvqhns in lstTempDVQHNS)
                        {
                            var data = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B01_BSTT_2Vm.ContentData();
                                    tempResult.MA_DONVI = dataObj.MA_DONVI;
                                    tempResult.REFID = dataObj.REFID;
                                    tempResult.NAM = dataObj.NAM;
                                    tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                                    tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                                    tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                                    tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                                    tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                                    tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                                    var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
                                    if (tempDVSDNS != null)
                                    {
                                        tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
                                        tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                                    }

                                    result.Add(tempResult);
                                }
                            }
                        }
                    }

                    response.Data = result;
                    response.Error = false;
                }
                else
                {
                    //var dataCha = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B01_BSTT_2Vm.ContentData();
                            tempResult.MA_DONVI = dataObj.MA_DONVI;
                            tempResult.REFID = dataObj.REFID;
                            tempResult.NAM = dataObj.NAM;
                            tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                            tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                            tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                            tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                            tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                            tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                            var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
                            if (tempDVSDNS != null)
                            {
                                tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
                                tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                            }

                            result.Add(tempResult);
                        }
                    }
                    if (item.MA_DONVI == currentMA_DVQHNS && lstTempDVQHNS.Count > 0)
                    {
                        foreach (var dvqhns in lstTempDVQHNS)
                        {
                            var data = _PhbB01BSTT2Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B01_BSTT_2Vm.ContentData();
                                    tempResult.MA_DONVI = dataObj.MA_DONVI;
                                    tempResult.REFID = dataObj.REFID;
                                    tempResult.NAM = dataObj.NAM;
                                    tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                                    tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                                    tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                                    tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                                    tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                                    tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                                    var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_DONVI);
                                    if (tempDVSDNS != null)
                                    {
                                        tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
                                        tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
                                    }
                                    result.Add(tempResult);
                                }
                            }
                        }
                    }

                    response.Data = result;
                    response.Error = false;
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

        [Route("GetTemplate")]
        [HttpGet]
        public async Task<IHttpActionResult> GetTemplate()
        {
            var response = new Response<List<PHB_B01_BSTT_2_TEMPLATE>>();

            try
            {
                response.Data = await _PhbB01BSTT2TemplateService.Queryable()
                    .OrderBy(e => e.STT_SAPXEP)
                    .ToListAsync();
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

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(PHB_B01_BSTT_2Vm.ContentData item)
        {
            var response = new Response<PHB_B01_BSTT_2Vm>();

            try
            {
                var report = new PHB_B01_BSTT_2()
                {
                    //NOTE: miss some informations from current login user...
                    MA_DONVI = item.MA_DONVI,
                    REFID = Guid.NewGuid().ToString(),
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    NGAY_TAO = DateTime.Now,
                    NAM = item.NAM,
                    ObjectState = ObjectState.Added
                };
                string maDVSDNS = null;
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                if (identity != null && !string.IsNullOrEmpty(identity.Name))
                {
                    maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                }
                if (!string.IsNullOrEmpty(maDVSDNS))
                {
                    var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDVSDNS);
                    if (dmDVSDNS != null)
                    {
                        report.MA_DONVI = maDVSDNS;
                        report.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                    }

                }

                //check đã có báo cáo chưa
                var reportCount = _PhbB01BSTT2Service.Queryable()
                    .Where(re => re.MA_DONVI == report.MA_DONVI && report.NAM == report.NAM)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _PhbB01BSTT2Service.Insert(report);

                var lstDetail = item.LstDetail.ToList();
                foreach (var detail in lstDetail)
                {
                    detail.PHB_B01_BSTT_2_REFID = report.REFID;
                    detail.ObjectState = ObjectState.Added;
                    _PhbB01BSTT2DetailService.Insert(detail);
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
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }

            return Ok(response);
        }

        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();

            try
            {
                if (httpRequest.Files.Count == 0)
                {
                    response.Error = true;
                    response.Message = "Không có dữ liệu upload.";
                    return Ok(response);
                }

                using (var excelPackage = new ExcelPackage(httpRequest.Files[0].InputStream))
                {
                    var report = new PHB_B01_BSTT_2
                    {
                        //NOTE: miss some informations from current login user...
                        REFID = Guid.NewGuid().ToString(),
                        NGUOI_TAO = RequestContext.Principal.Identity.Name,
                        NGAY_TAO = DateTime.Now,
                        ObjectState = ObjectState.Added
                    };
                    string maDVSDNS = null;
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    if (identity != null && !string.IsNullOrEmpty(identity.Name))
                    {
                        maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                    }
                    if (!string.IsNullOrEmpty(maDVSDNS))
                    {
                        var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDVSDNS);
                        if (dmDVSDNS != null)
                        {
                            report.MA_DONVI = maDVSDNS;
                            report.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                        }

                    }
                    var workSheet = excelPackage.Workbook.Worksheets["Sheet1"];

                    if (workSheet == null)
                    {
                        response.Error = true;
                        response.Message = "Lỗi định dạng dữ liệu.";
                        return Ok(response);
                    }

                    if (string.IsNullOrEmpty(httpRequest["NAM"]))
                    {
                        response.Error = true;
                        response.Message = "Lỗi định dạng dữ liệu.";
                        return Ok(response);
                    }

                    report.NAM = int.Parse(httpRequest["NAM"]);

                    //check đã có báo cáo chưa
                    var reportCount = _PhbB01BSTT2Service.Queryable()
                        .Where(re => re.MA_DONVI == report.MA_DONVI && report.NAM == report.NAM)
                        .Count();

                    if (reportCount > 0)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.EXITS_REPORT;
                        return Ok(response);
                    }

                    _PhbB01BSTT2Service.Insert(report);

                    int start_Row = 6;
                    int end_Row = 41;
                    int count = 1;
                    for (int r = start_Row; r <= end_Row; r++)
                    {
                        var detail = new PHB_B01_BSTT_2_DETAIL
                        {
                            PHB_B01_BSTT_2_REFID = report.REFID,
                            ObjectState = ObjectState.Added,
                            STT_SAPXEP = count,
                            STT = workSheet.Cells[r, 1].Value?.ToString(),
                            CHI_TIEU = workSheet.Cells[r, 2].Value?.ToString(),
                            NAM_NAY = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0
                        };
                        _PhbB01BSTT2DetailService.Insert(detail);

                        count++;
                    }

                    if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                    {
                        response.Data = report.REFID;
                        response.Error = false;
                        response.Message = "Cập nhật thành công.";
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Lỗi cập nhật dữ liệu.";
                    }
                }

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                throw;
            }

            return Ok(response);
        }

        [Route("Detail/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(string refid)
        {
            var response = new Response<List<PHB_B01_BSTT_2_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _PhbB01BSTT2DetailService.Queryable()
                    .Where(detail => detail.PHB_B01_BSTT_2_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(List<PHB_B01_BSTT_2_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_B01_BSTT_2();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHB_B01_BSTT_2_REFID;
                report = await _PhbB01BSTT2Service.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
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
            _PhbB01BSTT2Service.Update(report);

            //get list details by refid
            var lstDetail = new List<PHB_B01_BSTT_2_DETAIL>();
            try
            {
                lstDetail = await _PhbB01BSTT2DetailService.Queryable().Where(detail => detail.PHB_B01_BSTT_2_REFID == report.REFID).ToListAsync();
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
                detail.NAM_NAY = model.Where(e => e.ID == detail.ID).FirstOrDefault().NAM_NAY;
                detail.ObjectState = ObjectState.Modified;
                _PhbB01BSTT2DetailService.Update(detail);
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
            var report = new PHB_B01_BSTT_2();
            try
            {
                report = await _PhbB01BSTT2Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<PHB_B01_BSTT_2_DETAIL>();
            try
            {
                lstDetail = await _PhbB01BSTT2DetailService.Queryable().Where(detail => detail.PHB_B01_BSTT_2_REFID == refid).ToListAsync();
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
                _PhbB01BSTT2Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _PhbB01BSTT2DetailService.Delete(detail);
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

            return Ok(response);
        }
    }
}
