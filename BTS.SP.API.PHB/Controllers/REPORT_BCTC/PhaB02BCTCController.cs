using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B02;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHA_B02_BCTC")]
    [Route("{id?}")]
    public class PhaB02BCTCController : ApiController
    {
        private readonly IPhaB02BCTCService _B02BCTCService;
        private readonly IPhaB02BCTCDetailService _B02BCTCDetailService;
        private readonly IPhaB02BCTCTemplateService _B02BCTCTemplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IAuNguoiDungService _auService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhaB02BCTCController(IPhaB02BCTCService B02BCTCService, IPhaB02BCTCDetailService B02BCTCDetailService, IPhaB02BCTCTemplateService B02BCTCTemplateService,
            IDmDVQHNSService serviceDMDVQHNS, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService, IAuNguoiDungService auService,
            ISysDvqhnsService sysDvqhnsService)
        {
            _B02BCTCService = B02BCTCService;
            _B02BCTCDetailService = B02BCTCDetailService;
            _B02BCTCTemplateService = B02BCTCTemplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _auService = auService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(PHA_B02_BCTCVm.ContentData item)
        {
            var response = new Response<List<PHA_B02_BCTCVm.ContentData>>();
            var result = new List<PHA_B02_BCTCVm.ContentData>();
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
                    var dataCha = _B02BCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHA_B02_BCTCVm.ContentData();
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
                            var data = _B02BCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHA_B02_BCTCVm.ContentData();
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
                    //var dataCha = _B02BCTCService.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _B02BCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHA_B02_BCTCVm.ContentData();
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
                            var data = _B02BCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHA_B02_BCTCVm.ContentData();
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
            Response<List<PHA_B02_BCTC_TEMPLATE>> response = new Response<List<PHA_B02_BCTC_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _B02BCTCTemplateService.Queryable()
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
                        var b02Bcqt = new PHA_B02_BCTC()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };
                        //string maDVSDNS = null;
                        //var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        //if (identity != null && !string.IsNullOrEmpty(identity.Name))
                        //{
                        //    maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                        //}
                        //if (!string.IsNullOrEmpty(maDVSDNS))
                        //{
                        //    var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDVSDNS);
                        //    if (dmDVSDNS != null)
                        //    {
                        //        b02Bcqt.MA_DONVI = maDVSDNS;
                        //        b02Bcqt.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                        //    }

                        //}
                        if (string.IsNullOrEmpty(httpRequest["MA_DONVI"])) return Ok(new Response()
                        {
                            Error = true,
                            Message = "Không có mã đơn vị QHNS."
                        });
                        b02Bcqt.MA_DONVI = httpRequest["MA_DONVI"];

                        //b02Bcqt.CAP_DU_TOAN = httpRequest["MA_QHNS_QL"];
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["NAM"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b02Bcqt.NAM = int.Parse(httpRequest["NAM"]);

                            //check đã có báo cáo chưa
                            var reportCount = _B02BCTCService.Queryable()
                                .Where(report => report.MA_DONVI == b02Bcqt.MA_DONVI && report.NAM == b02Bcqt.NAM)
                                .Count();

                            if (reportCount > 0)
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.EXITS_REPORT;
                                return Ok(response);
                            }

                            _B02BCTCService.Insert(b02Bcqt);

                            int start_Row = 10;
                            int end_Row = 36;
                            int start_Col = 1;
                            int count = 1;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                var obj = new PHA_B02_BCTC_DETAIL()
                                {
                                    PHA_B02_BCTC_REFID = b02Bcqt.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                obj.STT_SAPXEP = count;
                                obj.STT = workSheet.Cells[r, 1].Value?.ToString();
                                obj.CHI_TIEU = workSheet.Cells[r, 2].Value?.ToString();
                                obj.MA_SO = workSheet.Cells[r, 3].Value?.ToString();
                                if (string.IsNullOrEmpty(obj.MA_SO))
                                {
                                    obj.MA_SO = "a";
                                }
                                obj.THUYET_MINH = workSheet.Cells[r, 4].Value?.ToString();
                                obj.SO_NAM_NAY = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.SO_NAM_TRUOC = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                _B02BCTCDetailService.Insert(obj);
                                count++;

                            }


                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b02Bcqt.REFID;
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

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(PHA_B02_BCTCVm.ContentData item)
        {
            var response = new Response<PHA_B02_BCTCVm.ContentData>();
            try
            {

                PHA_B02_BCTC model = new PHA_B02_BCTC();
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
                var reportCount = _B02BCTCService.Queryable()
                    .Where(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _B02BCTCService.Insert(model);

                var data = item.lstDetail.ToList();
                foreach (var v in data)
                {
                    v.PHA_B02_BCTC_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _B02BCTCDetailService.Insert(v);
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


        [Route("Detail/{refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> Detail(string refid)
        {
            var response = new Response<List<PHA_B02_BCTC_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _B02BCTCDetailService.Queryable()
                    .Where(detail => detail.PHA_B02_BCTC_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(List<PHA_B02_BCTC_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHA_B02_BCTC();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHA_B02_BCTC_REFID;
                report = await _B02BCTCService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
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
            _B02BCTCService.Update(report);

            //get list details by refid
            var lstDetail = new List<PHA_B02_BCTC_DETAIL>();
            try
            {
                lstDetail = await _B02BCTCDetailService.Queryable().Where(detail => detail.PHA_B02_BCTC_REFID == report.REFID).ToListAsync();
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
                detail.THUYET_MINH = model.Where(e => e.ID == detail.ID).FirstOrDefault().THUYET_MINH;
                detail.SO_NAM_NAY = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_NAM_NAY;
                detail.SO_NAM_TRUOC = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_NAM_TRUOC;
                detail.ObjectState = ObjectState.Modified;
                _B02BCTCDetailService.Update(detail);
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
            var report = new PHA_B02_BCTC();
            try
            {
                report = await _B02BCTCService.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<PHA_B02_BCTC_DETAIL>();
            try
            {
                lstDetail = await _B02BCTCDetailService.Queryable().Where(detail => detail.PHA_B02_BCTC_REFID == refid).ToListAsync();
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
                _B02BCTCService.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _B02BCTCDetailService.Delete(detail);
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

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            try
            {
                var bc = new PHA_B02_BCTC
                {
                    NAM = model.ReportHeader.ReportYear,
                    MA_DONVI = model.ReportHeader.CompanyID,
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _B02BCTCService
                    .Queryable()
                    .Count(report => report.MA_DONVI == model.ReportHeader.CompanyID && report.NAM == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _B02BCTCService.Insert(bc);

                var details = model.B02BCTCDetail;
                foreach (var t in details)
                {
                    var item = new PHA_B02_BCTC_DETAIL
                    {
                        PHA_B02_BCTC_REFID = bc.REFID,
                        CHI_TIEU = t.ReportItemName,
                        STT = t.ReportItemAlias,
                        STT_SAPXEP = t.ReportItemIndex.GetValueOrDefault(0),
                        MA_SO = t.ReportItemCode.ToString(),
                        SO_NAM_NAY = t.CurrentPeriodAmount,
                        SO_NAM_TRUOC = t.PreviousPeriodAmount
                    };
                    _B02BCTCDetailService.Insert(item);
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
                response.Message = ErrorMessage.ERROR_SYSTEM;
                response.Error = true;
                return Ok(response);
            }

            return Ok(response);
        }

        [Route("ReceiveDataFromService")]
        [HttpPost]
        public async Task<IHttpActionResult> ReceiveDataFromService(List<B02BCTCModel> model)
        {
            var response = new Response<string>();
            if (model == null || model.Count == 0)
            {
                response.Message = "param model Task<IHttpActionResult> ReceiveDataFromService is null or empty";
                return Ok(response);
            }

            using (var context = new PHBContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var rpB02 in model)
                        {
                            string msg = _B02BCTCService.IfExistsRpPeriodThenDelete(rpB02.ReportHeader.CompanyID, rpB02.ReportHeader.ReportPeriod, rpB02.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _B02BCTCService.InsertData(rpB02, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                        response.Message = "";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Message = ex.Message;
                        return Ok(response);
                    }
                    finally
                    {
                        transaction.Dispose();
                        context.Dispose();
                    }
                }
            }
            return Ok(response);
        }
    }
}