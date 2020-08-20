using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
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
using System.Security.Claims;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHA_B01_BSTT_1")]
    [Route("{id?}")]
    public class PhaB01BSTT1Controller : ApiController
    {
        private readonly IPhaB01BSTT1Service _B01BSTT1Service;
        private readonly IPhaB01BSTT1DetailService _B01BSTT1DetailService;
        private readonly IPhaB01BSTT1TemplateService _B01BSTT1emplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhaB01BSTT1Controller(IPhaB01BSTT1Service B01BSTT1Service, IPhaB01BSTT1DetailService B01BSTT1DetailService, IPhaB01BSTT1TemplateService B01BSTT1emplateService, IDmDVQHNSService serviceDMDVQHNS, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService, ISysDvqhnsService sysDvqhnsService)
        {
            _B01BSTT1Service = B01BSTT1Service;
            _B01BSTT1DetailService = B01BSTT1DetailService;
            _B01BSTT1emplateService = B01BSTT1emplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        //[Route("PostQuery")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostQuery(PHA_B01_BSTT_1Vm.ContentData item)
        //{
        //    var response = new Response<List<PHA_B01_BSTT_1Vm.ContentData>>();
        //    try
        //    {
        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
        //        var StrDONVI = currentMA_QHNS.Split(',');
        //        var data = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
        //        var result = new List<PHA_B01_BSTT_1Vm.ContentData>();
        //        if (data.Count > 0)
        //        {
        //            foreach (var dataObj in data)
        //            {
        //                var tempResult = new PHA_B01_BSTT_1Vm.ContentData();
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
        public async Task<IHttpActionResult> PostQuery(PHA_B01_BSTT_1Vm.ContentData item)
        {
            var response = new Response<List<PHA_B01_BSTT_1Vm.ContentData>>();
            var result = new List<PHA_B01_BSTT_1Vm.ContentData>();
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
                    var dataCha = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHA_B01_BSTT_1Vm.ContentData();
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
                            var data = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHA_B01_BSTT_1Vm.ContentData();
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
                    //var dataCha = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHA_B01_BSTT_1Vm.ContentData();
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
                            var data = _B01BSTT1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHA_B01_BSTT_1Vm.ContentData();
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
            Response<List<PHA_B01_BSTT_1_TEMPLATE>> response = new Response<List<PHA_B01_BSTT_1_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _B01BSTT1emplateService.Queryable()
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

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(PHA_B01_BSTT_1Vm.ContentData item)
        {
            var response = new Response<PHA_B01_BSTT_1Vm.ContentData>();
            try
            {
                PHA_B01_BSTT_1 model = new PHA_B01_BSTT_1();
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
                        model.MA_DONVI = maDVSDNS;
                        model.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                    }

                }

                model.MA_DONVI = item.MA_DONVI;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM = item.NAM;
                model.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _B01BSTT1Service.Queryable()
                    .Where(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _B01BSTT1Service.Insert(model);

                var data = item.lstDetail.ToList();
                foreach (var v in data)
                {
                    v.PHA_B01_BSTT_1_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _B01BSTT1DetailService.Insert(v);
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
                response.Error = false;
                response.Data = item;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        [Route("UploadReport")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadReport()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    using (var excelPackage = new ExcelPackage(HttpContext.Current.Request.Files[0].InputStream))
                    {
                        var b01btss = new PHA_B01_BSTT_1()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
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
                                b01btss.MA_DONVI = maDVSDNS;
                                b01btss.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                            }

                        }
                        var workSheet = excelPackage.Workbook.Worksheets["Sheet1"];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["NAM"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b01btss.NAM = int.Parse(httpRequest["NAM"]);

                            //check đã có báo cáo chưa
                            var reportCount = _B01BSTT1Service.Queryable()
                                .Where(report => report.MA_DONVI == b01btss.MA_DONVI && report.NAM == b01btss.NAM)
                                .Count();

                            if (reportCount > 0)
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.EXITS_REPORT;
                                return Ok(response);
                            }

                            _B01BSTT1Service.Insert(b01btss);

                            int start_Row = 13;
                            int end_Row = 40;
                            int start_Col = 1;
                            int count = 1;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                var obj = new PHA_B01_BSTT_1_DETAIL()
                                {
                                    PHA_B01_BSTT_1_REFID = b01btss.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                obj.STT_SAPXEP = count;
                                obj.STT = workSheet.Cells[r, 1].Value?.ToString();
                                obj.CHI_TIEU = workSheet.Cells[r, 2].Value?.ToString();
                                obj.MA_SO = workSheet.Cells[r, 3].Value?.ToString();
                                obj.TONG_SO = workSheet.Cells[r, 4].Value != null ? decimal.Parse(workSheet.Cells[r, 4].Value.ToString()) : 0;
                                obj.TRONG_DVKTTG_1 = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.TRONG_DVKTTG_2 = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                obj.TRONG_DVDT_CAP1 = workSheet.Cells[r, 7].Value != null ? decimal.Parse(workSheet.Cells[r, 7].Value.ToString()) : 0;
                                obj.NGOAI_DVDT_CAP1_CUNGTINH = workSheet.Cells[r, 8].Value != null ? decimal.Parse(workSheet.Cells[r, 8].Value.ToString()) : 0;
                                obj.NGOAI_DVDT_CAP1_KHACTINH = workSheet.Cells[r, 9].Value != null ? decimal.Parse(workSheet.Cells[r, 9].Value.ToString()) : 0;
                                obj.NGOAI_NHA_NUOC = workSheet.Cells[r, 10].Value != null ? decimal.Parse(workSheet.Cells[r, 10].Value.ToString()) : 0;
                                _B01BSTT1DetailService.Insert(obj);
                                count++;
                            }



                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b01btss.REFID;
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

        [Route("GetDetailByRefID/{REFID}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailByRefId(string REFID)
        {
            var response = new Response<List<PHA_B01_BSTT_1_DETAIL>>();
            try
            {
                response.Error = false;
                response.Data = _B01BSTT1DetailService.Queryable()
                    .Where(x => x.PHA_B01_BSTT_1_REFID == REFID)
                    .OrderBy(x => x.STT_SAPXEP)
                    .ToList();

            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }

            return Ok(response);
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHA_B01_BSTT_1_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHA_B01_BSTT_1();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHA_B01_BSTT_1_REFID;
                report = await _B01BSTT1Service.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
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
            _B01BSTT1Service.Update(report);

            //get list details by refid
            var lstDetail = new List<PHA_B01_BSTT_1_DETAIL>();
            try
            {
                lstDetail = await _B01BSTT1DetailService.Queryable().Where(detail => detail.PHA_B01_BSTT_1_REFID == report.REFID).ToListAsync();
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
                detail.TONG_SO = model.Where(e => e.ID == detail.ID).FirstOrDefault().TONG_SO;
                detail.TRONG_DVKTTG_1 = model.Where(e => e.ID == detail.ID).FirstOrDefault().TRONG_DVKTTG_1;
                detail.TRONG_DVKTTG_2 = model.Where(e => e.ID == detail.ID).FirstOrDefault().TRONG_DVKTTG_2;
                detail.TRONG_DVDT_CAP1 = model.Where(e => e.ID == detail.ID).FirstOrDefault().TRONG_DVDT_CAP1;
                detail.NGOAI_DVDT_CAP1_CUNGTINH = model.Where(e => e.ID == detail.ID).FirstOrDefault().NGOAI_DVDT_CAP1_CUNGTINH;
                detail.NGOAI_DVDT_CAP1_KHACTINH = model.Where(e => e.ID == detail.ID).FirstOrDefault().NGOAI_DVDT_CAP1_KHACTINH;
                detail.NGOAI_NHA_NUOC = model.Where(e => e.ID == detail.ID).FirstOrDefault().NGOAI_NHA_NUOC;
                detail.ObjectState = ObjectState.Modified;
                _B01BSTT1DetailService.Update(detail);
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
            var report = new PHA_B01_BSTT_1();
            try
            {
                report = await _B01BSTT1Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<PHA_B01_BSTT_1_DETAIL>();
            try
            {
                lstDetail = await _B01BSTT1DetailService.Queryable().Where(detail => detail.PHA_B01_BSTT_1_REFID == refid).ToListAsync();
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
                _B01BSTT1Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _B01BSTT1DetailService.Delete(detail);
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