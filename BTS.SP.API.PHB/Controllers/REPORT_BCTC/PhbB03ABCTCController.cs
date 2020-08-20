using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC;
using OfficeOpenXml;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Security.Claims;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHB_B03A_BCTC")]
    [Route("{id?}")]
    public class PhbB03ABCTCController : ApiController
    {
        private readonly IPhbB03ABCTCService _PhbB03ABCTCService;
        private readonly IPhbB03ABCTCDetailService _PhbB03ABCTCDetailService;
        private readonly IPhbB03ABCTCTemplateService _PhbB03ABCTCTemplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbB03ABCTCController(IPhbB03ABCTCService B03ABCTCService, IPhbB03ABCTCDetailService B03ABCTCDetailService, 
            IPhbB03ABCTCTemplateService B03ABCTCTemplateService, IDmDVQHNSService serviceDMDVQHNS, IUnitOfWorkAsync unitOfWorkAsync, 
            ISysDvqhns_QuanLyService sysDVQHNSQLService, ISysDvqhnsService sysDvqhnsService)
        {
            _PhbB03ABCTCService = B03ABCTCService;
            _PhbB03ABCTCDetailService = B03ABCTCDetailService;
            _PhbB03ABCTCTemplateService = B03ABCTCTemplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(PHB_B03A_BCTCVm.ContentData item)
        {
            var response = new Response<List<PHB_B03A_BCTCVm.ContentData>>();
            var result = new List<PHB_B03A_BCTCVm.ContentData>();
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
                    var dataCha = _PhbB03ABCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B03A_BCTCVm.ContentData();
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
                            var data = _PhbB03ABCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B03A_BCTCVm.ContentData();
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
                    //var dataCha = _PhbB03ABCTCService.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _PhbB03ABCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B03A_BCTCVm.ContentData();
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
                            var data = _PhbB03ABCTCService.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B03A_BCTCVm.ContentData();
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
            Response<List<PHB_B03A_BCTC_TEMPLATE>> response = new Response<List<PHB_B03A_BCTC_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _PhbB03ABCTCTemplateService.Queryable()
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
        public async Task<IHttpActionResult> AddContent(PHB_B03A_BCTCVm.ContentData item)
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<PHB_B03A_BCTCVm.ContentData>();
            try
            {

                PHB_B03A_BCTC model = new PHB_B03A_BCTC();

                string maDVSDNS = null;
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                if (identity != null && !string.IsNullOrEmpty(identity.Name))
                {
                    maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                }
                if (!string.IsNullOrEmpty(maDVSDNS))
                {
                    var dmDVSDNS = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == maDVSDNS);
                    if (dmDVSDNS != null)
                    {
                        model.MA_DONVI = maDVSDNS;
                        model.TEN_DONVI = dmDVSDNS.TEN_DVQHNS.ToString();
                        //model.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                    }

                }

                //if (string.IsNullOrEmpty(httpRequest["MA_DONVI"])) return Ok(new Response()
                //{
                //    Error = true,
                //    Message = "Không có mã đơn vị QHNS."
                //});
                //model.MA_DONVI = httpRequest["MA_DONVI"];
                //model.TEN_DONVI = httpRequest["TEN_DONVI"];
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM = item.NAM;
                model.REFID = Guid.NewGuid().ToString();
                model.ObjectState = ObjectState.Added;

                //check đã có báo cáo chưa
                var reportCount = _PhbB03ABCTCService.Queryable()
                    .Where(re => re.MA_DONVI == model.MA_DONVI && re.NAM == model.NAM)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _PhbB03ABCTCService.Insert(model);

                var data = item.lstDetail.ToList();
                foreach (var v in data)
                {
                    v.PHB_B03A_BCTC_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _PhbB03ABCTCDetailService.Insert(v);
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
                        PHB_B03A_BCTC b03aBctc = new PHB_B03A_BCTC()
                        {
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            NGAY_TAO = DateTime.Now,
                            ObjectState = ObjectState.Added,
                            REFID = Guid.NewGuid().ToString("N")
                        };
                        //if (string.IsNullOrEmpty(httpRequest["MA_DVQHNS"])) return Ok(new Response()
                        //{
                        //    Error = true,
                        //    Message = "Không có đơn vị báo cáo."
                        //});
                        //b03aBctc.MA_DONVI = httpRequest["MA_DVQHNS"];
                        //b03aBctc.TEN_DONVI = httpRequest["TEN_DVQHNS"];

                        if (string.IsNullOrEmpty(httpRequest["MA_DONVI"])) return Ok(new Response()
                        {
                            Error = true,
                            Message = "Không có mã đơn vị QHNS."
                        });
                        b03aBctc.MA_DONVI = httpRequest["MA_DONVI"];
                        b03aBctc.TEN_DONVI = httpRequest["TEN_DONVI"];
                        //string maDVSDNS = null;
                        //string tenDVSDNS = null;
                        //var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        //if (identity != null && !string.IsNullOrEmpty(identity.Name))
                        //{
                        //    maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                        //    tenDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("TEN_DONVI"))?.Value.ToString();
                        //}
                        //if (!string.IsNullOrEmpty(maDVSDNS))
                        //{
                        //    var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDVSDNS);
                        //    if (dmDVSDNS != null)
                        //    {
                        //        b03aBctc.MA_DONVI = maDVSDNS;
                        //        b03aBctc.TEN_DONVI = tenDVSDNS;
                        //        b03aBctc.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                        //    }
                        //}
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["NAM"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b03aBctc.NAM = int.Parse(httpRequest["NAM"]);

                            //check đã có báo cáo chưa
                            var reportCount = _PhbB03ABCTCService.Queryable()
                                .Where(re => re.MA_DONVI == b03aBctc.MA_DONVI && re.NAM == b03aBctc.NAM)
                                .Count();

                            if (reportCount > 0)
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.EXITS_REPORT;
                                return Ok(response);
                            }

                            _PhbB03ABCTCService.Insert(b03aBctc);

                            int start_Row = 11;
                            int end_Row = 39;
                            int start_Col = 1;
                            int count = 1;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                var obj = new PHB_B03A_BCTC_DETAIL()
                                {
                                    PHB_B03A_BCTC_REFID = b03aBctc.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                obj.STT_SAPXEP = count;
                                obj.STT = workSheet.Cells[r, 1].Value?.ToString();
                                obj.CHI_TIEU = workSheet.Cells[r, 2].Value?.ToString();
                                obj.MA_SO = workSheet.Cells[r, 3].Value?.ToString();                                
                                obj.THUYET_MINH = workSheet.Cells[r, 4].Value?.ToString();
                                obj.SO_CUOI_NAM = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.SO_DAU_NAM = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                _PhbB03ABCTCDetailService.Insert(obj);
                                count++;
                            }
                           


                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b03aBctc.REFID;
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
            var response = new Response<List<PHB_B03A_BCTC_DETAIL>>();
            try
            {
                response.Error = false;
                response.Data = _PhbB03ABCTCDetailService.Queryable()
                    .Where(x => x.PHB_B03A_BCTC_REFID == REFID)
                    .OrderBy(x => x.STT_SAPXEP)
                    .ToList();

            }
            catch(Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }

            return Ok(response);
        }

        //[Route("Edit")]
        //[HttpPut]
        //public async Task<IHttpActionResult> Edit(PHB_B03A_BCTCVm.ContentData item)
        //{

        //    var response = new Response<string>();
            
        //    var itemData = await _PhbB03ABCTCService.Queryable().FirstOrDefaultAsync(X => X.REFID == item.REFID);
        //    if(itemData.TRANG_THAI == 1)
        //    {
        //        response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
        //        response.Error = true;
        //        return Ok(response);
        //    }
        //    try
        //    {
        //        if(itemData != null)
        //        {

        //            itemData.NGUOI_SUA = RequestContext.Principal.Identity.Name;
        //            itemData.NGAY_SUA = DateTime.Now;
        //            itemData.ObjectState = ObjectState.Modified;
        //            _PhbB03ABCTCService.Update(itemData);
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //        }

        //        if (item.lstDetail.Count > 0)
        //        {
        //            foreach(var record in item.lstDetail)
        //            {
        //                var itemDetail = await _PhbB03ABCTCDetailService.Queryable().FirstOrDefaultAsync(X => X.STT_SAPXEP == record.STT_SAPXEP);
        //                itemDetail.THUYET_MINH = record.THUYET_MINH;
        //                itemDetail.SO_CUOI_NAM = record.SO_CUOI_NAM;
        //                itemDetail.SO_DAU_NAM = record.SO_DAU_NAM;
        //                itemDetail.ObjectState = ObjectState.Modified;
        //                _PhbB03ABCTCDetailService.Update(itemDetail);
                        
        //            }
        //            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
        //            {
        //                response.Error = false;
        //                response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
        //            }
        //        }
        //        else
        //        {
        //            response.Error = true;
        //            response.Message = ErrorMessage.EMPTY_DATA;
        //        }
        //        //response.Error = false;
        //        //response.Data = item;
        //    }
        //    catch(Exception ex)
        //    {
        //        WriteLogs.LogError(ex);
        //        response.Error = true;
        //        response.Message = ErrorMessage.ERROR_SYSTEM;
        //    }
        //    return Ok(response);

        //}

        [Route("Edit")]
        [HttpPost]
        public async Task<IHttpActionResult> Edit(List<PHB_B03A_BCTC_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_B03A_BCTC();

            //get report by refid of the first detail
            try
            {
                var refid = model.First().PHB_B03A_BCTC_REFID;
                report = await _PhbB03ABCTCService.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();
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
            _PhbB03ABCTCService.Update(report);

            //get list details by refid
            var lstDetail = new List<PHB_B03A_BCTC_DETAIL>();
            try
            {
                lstDetail = await _PhbB03ABCTCDetailService.Queryable().Where(detail => detail.PHB_B03A_BCTC_REFID == report.REFID).ToListAsync();
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
                detail.SO_DAU_NAM = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_DAU_NAM;
                detail.SO_CUOI_NAM = model.Where(e => e.ID == detail.ID).FirstOrDefault().SO_CUOI_NAM;
                detail.ObjectState = ObjectState.Modified;
                _PhbB03ABCTCDetailService.Update(detail);
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
            var report = new PHB_B03A_BCTC();
            try
            {
                report = await _PhbB03ABCTCService.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<PHB_B03A_BCTC_DETAIL>();
            try
            {
                lstDetail = await _PhbB03ABCTCDetailService.Queryable().Where(detail => detail.PHB_B03A_BCTC_REFID == refid).ToListAsync();
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
                _PhbB03ABCTCService.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _PhbB03ABCTCDetailService.Delete(detail);
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