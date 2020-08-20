using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC;
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

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPHA_B03B_BCTC")]
    [Route("{id?}")]
    public class phbB03B_BCTCController : ApiController
    {
        private readonly IPhaB03BBCTCService _PhaB03BBCTCService;
        private readonly IPhaB03BBCTCDetailService _PhaB03BBCTCDetailService;
        private readonly IPhaB03BBCTCTemplateService _PhaB03BBCTCTemplateService;
        private readonly IDmDVQHNSService _serviceDMDVQHNS;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public phbB03B_BCTCController(IPhaB03BBCTCService B03BBCTCService, IPhaB03BBCTCDetailService B03BBCTCDetailService, IPhaB03BBCTCTemplateService B03BBCTCTemplateService, IDmDVQHNSService serviceDMDVQHNS, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _PhaB03BBCTCService = B03BBCTCService;
            _PhaB03BBCTCDetailService = B03BBCTCDetailService;
            _PhaB03BBCTCTemplateService = B03BBCTCTemplateService;
            _serviceDMDVQHNS = serviceDMDVQHNS;

            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(PHA_B03B_BCTCVm.ContentData item)
        {
            var response = new Response<List<PHA_B03B_BCTCVm.ContentData>>();
            try
            {
                List<string> lstDVSDNS = new List<string>();
                using (var connection = new OracleConnection(ConfigurationManager.ConnectionStrings["STCConnection"].ConnectionString))
                {
                    string maDVSDNS = "";
                    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    if (identity != null && !string.IsNullOrEmpty(identity.Name))
                    {
                        maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
                    }

                    connection.Open();
                    using (var commandDepartment = connection.CreateCommand())
                    {
                        commandDepartment.CommandType = CommandType.Text;
                        commandDepartment.CommandText = string.Format(@"Select MA_QHNS,TEN_QHNS, MA_CHA, 
                                                                                Prior TEN_QHNS
                                                                                From   PHB_DM_DVQHNS
                                                                                WHERE TRANG_THAI = {0}
                                                                                Connect By Prior MA_QHNS = MA_CHA
                                                                                Start  With MA_QHNS = '{1}'", "'A'", maDVSDNS);
                        using (var dataReaderDeparment = commandDepartment.ExecuteReader())
                        {
                            if (dataReaderDeparment.HasRows)
                            {
                                while (dataReaderDeparment.Read())
                                {
                                    var value = dataReaderDeparment["MA_QHNS"] != null ? dataReaderDeparment["MA_QHNS"].ToString() : "";
                                    lstDVSDNS.Add(value);
                                }
                            }
                        }

                    }

                }
                var data = _PhaB03BBCTCService.Queryable().Where(x => x.NAM == item.NAM && lstDVSDNS.Contains(x.MA_DONVI)).ToList();
                var result = new List<PHA_B03B_BCTCVm.ContentData>();
                if (data.Count > 0)
                {
                    foreach (var dataObj in data)
                    {
                        var tempResult = new PHA_B03B_BCTCVm.ContentData();
                        tempResult.MA_DONVI = dataObj.MA_DONVI;
                        tempResult.REFID = dataObj.REFID;
                        tempResult.NAM = dataObj.NAM;
                        tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                        tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                        tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                        tempResult.TRANG_THAI = item.TRANG_THAI;

                        var tempDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == dataObj.MA_DONVI);
                        if (tempDVSDNS != null)
                        {
                            tempResult.TEN_DONVI = tempDVSDNS.TEN_QHNS;
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
            Response<List<PHA_B03B_BCTC_TEMPLATE>> response = new Response<List<PHA_B03B_BCTC_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _PhaB03BBCTCTemplateService.Queryable()
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
        public async Task<IHttpActionResult> AddContent(PHA_B03B_BCTCVm.ContentData item)
        {
            var response = new Response<PHA_B03B_BCTCVm.ContentData>();
            try
            {

                PHA_B03B_BCTC model = new PHA_B03B_BCTC();
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

                model.NAM = item.NAM;
                model.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _PhaB03BBCTCService.Queryable()
                    .Where(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _PhaB03BBCTCService.Insert(model);

                var data = item.lstDetail.ToList();

                foreach (var v in data)
                {
                    v.PHA_B03B_BCTC_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _PhaB03BBCTCDetailService.Insert(v);
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

        #region Upload
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
                        PHA_B03B_BCTC b03bbctc = new PHA_B03B_BCTC()
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
                            maDVSDNS = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_QHNS"))?.Value.ToString();
                        }
                        if (!string.IsNullOrEmpty(maDVSDNS))
                        {
                            var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDVSDNS);
                            if (dmDVSDNS != null)
                            {
                                b03bbctc.MA_DONVI = maDVSDNS;
                                b03bbctc.CAP_DU_TOAN = dmDVSDNS.CAP_DU_TOAN.ToString();
                            }

                        }
                        var workSheet = excelPackage.Workbook.Worksheets[1];
                        if (workSheet != null)
                        {
                            if (string.IsNullOrEmpty(httpRequest["NAM_BC"])) return Ok(new Response()
                            {
                                Error = true,
                                Message = "Không có năm báo cáo."
                            });
                            b03bbctc.NAM = int.Parse(httpRequest["NAM_BC"]);

                            //check đã có báo cáo chưa
                            var reportCount = _PhaB03BBCTCService.Queryable()
                                .Where(report => report.MA_DONVI == b03bbctc.MA_DONVI && report.NAM == b03bbctc.NAM)
                                .Count();

                            if (reportCount > 0)
                            {
                                response.Error = true;
                                response.Message = ErrorMessage.EXITS_REPORT;
                                return Ok(response);
                            }

                            _PhaB03BBCTCService.Insert(b03bbctc);

                            int start_Row = 11;
                            int end_Row = 39;
                            int start_Col = 1;
                            int count = 1;

                            for (int r = start_Row; r <= end_Row; r++)
                            {
                                var obj = new PHA_B03B_BCTC_DETAIL()
                                {
                                    PHA_B03B_BCTC_REFID = b03bbctc.REFID,
                                    ObjectState = ObjectState.Added
                                };
                                obj.STT_SAPXEP = count;
                                obj.STT = workSheet.Cells[r, 1].Value?.ToString();
                                obj.CHI_TIEU = workSheet.Cells[r, 2].Value?.ToString();
                                obj.MA_SO = workSheet.Cells[r, 3].Value?.ToString();
                                obj.THUYET_MINH = workSheet.Cells[r, 4].Value?.ToString();
                                obj.SO_NAM_NAY = workSheet.Cells[r, 5].Value != null ? decimal.Parse(workSheet.Cells[r, 5].Value.ToString()) : 0;
                                obj.SO_NAM_TRUOC = workSheet.Cells[r, 6].Value != null ? decimal.Parse(workSheet.Cells[r, 6].Value.ToString()) : 0;
                                _PhaB03BBCTCDetailService.Insert(obj);
                                count++;
                            }



                            if (await _unitOfWorkAsync.SaveChangesAsync() > 0)
                            {
                                response.Data = b03bbctc.REFID;
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
            var response = new Response<List<PHA_B03B_BCTC_DETAIL>>();
            try
            {
                response.Error = false;
                response.Data = _PhaB03BBCTCDetailService.Queryable()
                    .Where(x => x.PHA_B03B_BCTC_REFID == REFID)
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
        [HttpPut]
        public async Task<IHttpActionResult> Edit(PHA_B03B_BCTCVm.ContentData item)
        {

            var response = new Response<string>();

            var itemData = await _PhaB03BBCTCService.Queryable().FirstOrDefaultAsync(X => X.REFID == item.REFID);
            if (itemData.TRANG_THAI == 1)
            {
                response.Message = "Báo cáo đã được duyệt, không thể chỉnh sửa!";
                response.Error = true;
                return Ok(response);
            }
            try
            {
                if (itemData != null)
                {

                    itemData.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                    itemData.NGAY_TAO = DateTime.Now;
                    itemData.ObjectState = ObjectState.Modified;
                    _PhaB03BBCTCService.Update(itemData);
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }

                if (item.lstDetail.Count > 0)
                {
                    foreach (var record in item.lstDetail)
                    {
                        var itemDetail = await _PhaB03BBCTCDetailService.Queryable().FirstOrDefaultAsync(X => X.STT_SAPXEP == record.STT_SAPXEP);
                        itemDetail.THUYET_MINH = record.THUYET_MINH;
                        itemDetail.SO_NAM_NAY = record.SO_NAM_NAY;
                        itemDetail.SO_NAM_TRUOC = record.SO_NAM_TRUOC;
                        itemDetail.ObjectState = ObjectState.Modified;
                        _PhaB03BBCTCDetailService.Update(itemDetail);

                    }
                    await _unitOfWorkAsync.SaveChangesAsync();
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
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

        [Route("Delete/{refid}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(string refid)
        {
            var response = new Response<string>();

            //get report by refid
            var report = new PHA_B03B_BCTC();
            try
            {
                report = await _PhaB03BBCTCService.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<PHA_B03B_BCTC_DETAIL>();
            try
            {
                lstDetail = await _PhaB03BBCTCDetailService.Queryable().Where(detail => detail.PHA_B03B_BCTC_REFID == refid).ToListAsync();
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
                _PhaB03BBCTCService.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _PhaB03BBCTCDetailService.Delete(detail);
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

        #endregion
    }
}
