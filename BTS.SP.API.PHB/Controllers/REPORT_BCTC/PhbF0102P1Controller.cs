using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.F01_02;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
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

namespace BTS.SP.API.PHB.Controllers.REPORT_BCTC
{
    [RoutePrefix("api/reportbctc/PHB_F01_02_P1")]
    [Route("{id?}")]
    public class PhbF0102P1Controller : ApiController
    {
        private readonly IPhbF01_02_P1Service _f0102P1Service;
        private readonly IPhbF01_02DetailService _f0102P1DetailService;
        private readonly IPhbF01_02_P1TemplateService _f0102P1TemplateService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public PhbF0102P1Controller(ISysDvqhnsService sysDvqhnsService, IPhbF01_02_P1Service f0102P1Service, IPhbF01_02DetailService f0102P1DetailService, IPhbF01_02_P1TemplateService f0102P1TemplateService, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService, IDM_NGANHKTService dmNganhktService)
        {
            _f0102P1Service = f0102P1Service;
            _f0102P1DetailService = f0102P1DetailService;
            _f0102P1TemplateService = f0102P1TemplateService;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _dmNganhktService = dmNganhktService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        //[Route("PostQuery")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostQuery(PHB_F01_02_P1Vm.ContentData item)
        //{
        //    var response = new Response<List<PHB_F01_02_P1Vm.ContentData>>();
        //    List<PHB_F01_02_P1> data = new List<PHB_F01_02_P1>();
        //    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //    var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
        //    var StrDONVI = currentMA_QHNS.Split(',');
        //    try
        //    {
        //        if (string.IsNullOrEmpty(item.MA_DONVI))
        //        {
        //            data = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && StrDONVI.Contains(x.MA_DONVI)).ToList();
        //        }
        //        else
        //        {
        //            data = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
        //        }
        //        var result = new List<PHB_F01_02_P1Vm.ContentData>();
        //        if (data.Count > 0)
        //        {
        //            foreach (var dataObj in data)
        //            {
        //                var tempResult = new PHB_F01_02_P1Vm.ContentData();
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
        public async Task<IHttpActionResult> PostQuery(PHB_F01_02_P1Vm.ContentData item)
        {
            var response = new Response<List<PHB_F01_02_P1Vm.ContentData>>();
            var result = new List<PHB_F01_02_P1Vm.ContentData>();
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
                    var dataCha = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_F01_02_P1Vm.ContentData();
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
                            var data = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_F01_02_P1Vm.ContentData();
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
                    //var dataCha = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_F01_02_P1Vm.ContentData();
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
                            var data = _f0102P1Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_F01_02_P1Vm.ContentData();
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
            Response<List<PHB_F01_02_P1_TEMPLATE>> response = new Response<List<PHB_F01_02_P1_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _f0102P1TemplateService.Queryable()
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

        [Route("GetDMLoaiKhoan")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDMLoaiKhoan()
        {
            Response<List<DM_NGANHKT>> response = new Response<List<DM_NGANHKT>>();
            try
            {
                var toDay = DateTime.Today;
                var result = await _dmNganhktService.Queryable().Where(x => x.NGAY_HET_HL == null || x.NGAY_HET_HL >= toDay).OrderBy(x => x.MA_LOAI).ToListAsync();
                response.Error = false;
                response.Data = result;
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }

        public class InsertObject_F0102P1
        {
            public PHB_F01_02_P1 PHB_F01_02_P1 { get; set; }
            public List<PHB_F01_02_P1_DETAIL> DETAILS { get; set; }
        }

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(InsertObject_F0102P1 instance)
        {
            var response = new Response<string>();
            try
            {
                PHB_F01_02_P1 model = new PHB_F01_02_P1();

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                model.MA_DONVI = instance.PHB_F01_02_P1.MA_DONVI;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM = instance.PHB_F01_02_P1.NAM;
                model.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _f0102P1Service
                    .Queryable()
                    .Count(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _f0102P1Service.Insert(model);

                foreach (var item in instance.DETAILS)
                {
                    item.PHB_F01_02_P1_REFID = model.REFID;
                    _f0102P1DetailService.Insert(item);
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
                response.Data = "Thêm thành công!";
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
            var response = new Response<List<PHB_F01_02_P1_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _f0102P1DetailService.Queryable()
                    .Where(detail => detail.PHB_F01_02_P1_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(List<PHB_F01_02_P1_DETAIL> model)
        {
            var response = new Response<string>();

            if (model == null || model.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new PHB_F01_02_P1();
            try
            {
                var refid = model.First().PHB_F01_02_P1_REFID;
                report = await _f0102P1Service.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();

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
                _f0102P1Service.Update(report);

                var lstDetail = await _f0102P1DetailService.Queryable().Where(detail => detail.PHB_F01_02_P1_REFID == report.REFID).ToListAsync();

                //loop to edit each detail
                foreach (var detail in lstDetail)
                {
                    PHB_F01_02_P1_DETAIL first = null;
                    foreach (var e in model)
                    {
                        if (e.NN_LK != null && (e.MA_SO == detail.MA_SO && e.MA_LOAI == detail.MA_LOAI && e.MA_KHOAN == detail.MA_KHOAN && e.NN_LK == detail.NN_LK))
                        {
                            first = e;
                            detail.GIA_TRI = first.GIA_TRI;
                            detail.ObjectState = ObjectState.Modified;
                            _f0102P1DetailService.Update(detail);
                            break;
                        }
                    }
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

        [Route("Delete/{refid}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(string refid)
        {
            var response = new Response<string>();

            //get report by refid
            var report = new PHB_F01_02_P1();
            try
            {
                report = await _f0102P1Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
                var lstDetail = new List<PHB_F01_02_P1_DETAIL>();
                lstDetail = await _f0102P1DetailService.Queryable().Where(detail => detail.PHB_F01_02_P1_REFID == refid).ToListAsync();
                _f0102P1Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _f0102P1DetailService.Delete(detail);
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

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            try
            {
                var project = model.Project.Where(x => x.IsDetail == true);
                foreach (var p in project)
                {
                    var bc = new PHB_F01_02_P1
                    {
                        NAM = model.ReportHeader.ReportYear,
                        MA_DONVI = model.ReportHeader.CompanyID,
                        NGAY_TAO = DateTime.Now,
                        TRANG_THAI = 0,
                        NGUOI_TAO = RequestContext.Principal.Identity.Name,
                        REFID = Guid.NewGuid().ToString()
                    };

                    //check đã có báo cáo chưa
                    var reportCount = _f0102P1Service
                        .Queryable()
                        .Count(report => report.MA_DONVI == model.ReportHeader.CompanyID && report.NAM == model.ReportHeader.ReportYear);

                    if (reportCount > 0)
                    {
                        response.Error = true;
                        response.Message = ErrorMessage.EXITS_REPORT;
                        return Ok(response);
                    }

                    _f0102P1Service.Insert(bc);

                    var details = model.F0102BCQTP1Detail.Where(x => x.ProjectID == p.ProjectID);
                    foreach (var t in details)
                    {
                        var itemNN = new PHB_F01_02_P1_DETAIL
                        {
                            PHB_F01_02_P1_REFID = bc.REFID,
                            CHI_TIEU = t.ReportItemName,
                            MA_SO = t.ReportItemCode.ToString(),
                            GIA_TRI = t.Amount.GetValueOrDefault(0),
                            MA_KHOAN = t.BudgetSubKindItemID.Replace("K", ""),
                            MA_LOAI = t.BudgetKindItemID.Replace("L", ""),
                            NN_LK = "NN"
                        };
                        _f0102P1DetailService.Insert(itemNN);

                        var itemLK = new PHB_F01_02_P1_DETAIL
                        {
                            PHB_F01_02_P1_REFID = bc.REFID,
                            CHI_TIEU = t.ReportItemName,
                            MA_SO = t.ReportItemCode.ToString(),
                            GIA_TRI = t.AccAmount.GetValueOrDefault(0),
                            MA_KHOAN = t.BudgetSubKindItemID.Replace("K", ""),
                            MA_LOAI = t.BudgetKindItemID.Replace("L", ""),
                            NN_LK = "LK"
                        };
                        _f0102P1DetailService.Insert(itemLK);
                    }
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
        public async Task<IHttpActionResult> ReceiveDataFromService(List<F01_02_P1BCQTModel> model)
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
                        foreach (var rpF01_01_P1 in model)
                        {
                            string msg = _f0102P1Service.IfExistsRpPeriodThenDelete(rpF01_01_P1.ReportHeader.CompanyID, rpF01_01_P1.ReportHeader.ReportPeriod, rpF01_01_P1.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _f0102P1Service.InsertData(rpF01_01_P1, context);
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