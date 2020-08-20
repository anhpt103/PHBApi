using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC;
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
    [RoutePrefix("api/reportbctc/PHB_B05_BCTC")]
    [Route("{id?}")]
    public class PhbB05BCTCController : ApiController
    {
        private readonly IPhbB05BCTCService _phbB05BCTCService;
        private readonly IPhbB05BCTCDetailService _phbB05BCTCDetailService;
        private readonly IPhbB05BCTCTemplateService _phbB05BCTCTemplateService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IPhbB05BCTCWorkService _phbB05BCTCWorkService;



        public PhbB05BCTCController(ISysDvqhnsService sysDvqhnsService, IPhbB05BCTCService B05BCTCService, IPhbB05BCTCDetailService B05BCTCDetailService, IPhbB05BCTCTemplateService B05BCTCTemplateService, IUnitOfWorkAsync UnitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService, IPhbB05BCTCWorkService phbB05BCTCWorkService)
        {
            _phbB05BCTCService = B05BCTCService;
            _phbB05BCTCDetailService = B05BCTCDetailService;
            _phbB05BCTCTemplateService = B05BCTCTemplateService;
            _unitOfWorkAsync = UnitOfWorkAsync;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _phbB05BCTCWorkService = phbB05BCTCWorkService;
        }

        //[Route("PostQuery")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostQuery(PHB_B05_BCTCVm.ContentData item)
        //{
        //    var response = new Response<List<PHB_B05_BCTCVm.ContentData>>();
        //    try
        //    {
        //        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //        var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
        //        var StrDONVI = currentMA_QHNS.Split(',');
        //        var data = _phbB05BCTCService.Queryable().Where(x => x.NAM_BC == item.NAM_BC && (x.MA_QHNS == item.MA_DONVI || StrDONVI.Contains(x.MA_QHNS))).ToList();
        //        var result = new List<PHB_B05_BCTCVm.ContentData>();
        //        if (data.Count > 0)
        //        {
        //            foreach (var dataObj in data)
        //            {
        //                var tempResult = new PHB_B05_BCTCVm.ContentData();
        //                tempResult.MA_DONVI = dataObj.MA_QHNS;
        //                tempResult.REFID = dataObj.REFID;
        //                tempResult.NAM_BC = dataObj.NAM_BC;
        //                tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
        //                tempResult.NGAY_SUA = dataObj.NGAY_SUA;
        //                tempResult.NGAY_TAO = dataObj.NGAY_TAO;
        //                tempResult.TRANG_THAI = dataObj.TRANG_THAI;
        //                tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

        //                var tempDVSDNS = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
        //                if (tempDVSDNS != null)
        //                {
        //                    tempResult.TEN_DONVI = tempDVSDNS.TEN_DVQHNS;
        //                    tempResult.MA_QHNS_QL = tempDVSDNS.MA_DVQHNS_CHA;
        //                }
        //                if (tempResult.MA_QHNS_QL != null)
        //                {
        //                    result.Add(tempResult);
        //                }
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
        public async Task<IHttpActionResult> PostQuery(PHB_B05_BCTCVm.ContentData item)
        {
            var response = new Response<List<PHB_B05_BCTCVm.ContentData>>();
            var result = new List<PHB_B05_BCTCVm.ContentData>();
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
                    var dataCha = _phbB05BCTCService.Queryable().Where(x => x.NAM_BC == item.NAM && x.MA_QHNS == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B05_BCTCVm.ContentData();
                            tempResult.MA_DONVI = dataObj.MA_QHNS;
                            tempResult.REFID = dataObj.REFID;
                            tempResult.NAM = dataObj.NAM_BC;
                            tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                            tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                            tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                            tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                            tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                            tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                            var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
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
                            var data = _phbB05BCTCService.Queryable().Where(x => x.NAM_BC == item.NAM && x.MA_QHNS.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B05_BCTCVm.ContentData();
                                    tempResult.MA_DONVI = dataObj.MA_QHNS;
                                    tempResult.REFID = dataObj.REFID;
                                    tempResult.NAM = dataObj.NAM_BC;
                                    tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                                    tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                                    tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                                    tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                                    tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                                    tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                                    var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
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
                    //var dataCha = _phbB05BCTCService.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _phbB05BCTCService.Queryable().Where(x => x.NAM_BC == item.NAM && x.MA_QHNS == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new PHB_B05_BCTCVm.ContentData();
                            tempResult.MA_DONVI = dataObj.MA_QHNS;
                            tempResult.REFID = dataObj.REFID;
                            tempResult.NAM = dataObj.NAM_BC;
                            tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                            tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                            tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                            tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                            tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                            tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                            var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
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
                            var data = _phbB05BCTCService.Queryable().Where(x => x.NAM_BC == item.NAM && x.MA_QHNS == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new PHB_B05_BCTCVm.ContentData();
                                    tempResult.MA_DONVI = dataObj.MA_QHNS;
                                    tempResult.REFID = dataObj.REFID;
                                    tempResult.NAM = dataObj.NAM_BC;
                                    tempResult.CAP_DU_TOAN = dataObj.CAP_DU_TOAN;
                                    tempResult.NGAY_SUA = dataObj.NGAY_SUA;
                                    tempResult.NGAY_TAO = dataObj.NGAY_TAO;
                                    tempResult.NGUOI_TAO = dataObj.NGUOI_TAO;
                                    tempResult.TRANG_THAI = dataObj.TRANG_THAI;
                                    tempResult.TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI;

                                    var tempDVSDNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
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
            Response<List<PHB_B05_BCTC_TEMPLATE>> response = new Response<List<PHB_B05_BCTC_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phbB05BCTCTemplateService.Queryable()
                    .OrderBy(x => x.STT_SAP_XEP)
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

        [Route("AddNew")]
        [HttpPost]
        public async Task<IHttpActionResult> AddNew(PhbB05BCTCVm.AddConten model)
        {
            var response = new Response();

            if (model == null || model.dataTable.Count() < 0 || model.dataWork == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            string machuong = "";
            try
            {
                machuong = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == model.data.MA_QHNS).MA_CHUONG;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = "Đơn vị không tìm được mã chương";
                return Ok(response);
            }


            try
            {
                PHB_B05_BCTC item = new PHB_B05_BCTC();
                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                item.MA_QHNS = model.data.MA_QHNS;
                item.KY_BC = model.data.KY_BC;
                item.TEN_QHNS = model.data.TEN_QHNS;
                item.TRANG_THAI = 0;
                item.REFID = Guid.NewGuid().ToString();
                item.NGAY_TAO = DateTime.Now;
                item.NGUOI_TAO = identity.Name;
                item.MA_CHUONG = machuong;
                item.NAM_BC = model.data.NAM_BC;


                var reportCount = _phbB05BCTCService.Queryable()
                    .Where(report => report.MA_QHNS == model.data.MA_QHNS && report.NAM_BC == model.data.NAM_BC)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _phbB05BCTCService.Insert(item);

                PHB_B05_BCTC_WORK item_work = new PHB_B05_BCTC_WORK();

                item_work.PHB_B05_BCTC_REFID = item.REFID;
                item_work.DON_VI = model.dataWork.DON_VI;
                item_work.QD_TL_SO = model.dataWork.QD_TL_SO;
                item_work.NGAY = model.dataWork.NGAY;
                item_work.THANG = model.dataWork.THANG;
                item_work.NAM = model.dataWork.NAM;
                item_work.CO_QUAN_CAP = model.dataWork.CO_QUAN_CAP;
                item_work.THUOC_DV = model.dataWork.THUOC_DV;
                item_work.LOAI_HINH_DV = model.dataWork.LOAI_HINH_DV;
                item_work.CHU_TC = model.dataWork.CHU_TC;
                item_work.NV_CHINH_DV = model.dataWork.NV_CHINH_DV;
                item_work.TT_THUYETMINH = model.dataWork.TT_THUYETMINH;

                _phbB05BCTCWorkService.Insert(item_work);

                foreach (var itemDetail in model.dataTable)
                {
                    itemDetail.PHB_B05_BCTC_REFID = item.REFID;
                    itemDetail.ObjectState = ObjectState.Added;

                    _phbB05BCTCDetailService.Insert(itemDetail);
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
                response.Message = "Thêm Mới Thành Công";
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
            }
            return Ok(response);
        }
        [Route("GetDetail/{Refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetail(string Refid)
        {
            var response = new Response<List<PHB_B05_BCTC_DETAIL>>();

            //get all details by refid
            try
            {
                //Get Dữ liệu data trong bảng detail
                response.Data = await _phbB05BCTCDetailService.Queryable()
                    .Where(detail => detail.PHB_B05_BCTC_REFID == Refid)
                    .OrderBy(detail => detail.STT)
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

        [Route("GetDetailWork/{Refid}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDetailWork(string Refid)
        {
            var response = new Response<PHB_B05_BCTC_WORK>();

            //get all details by refid
            try
            {
                //Get Dữ liệu data trong bảng detail
                response.Data = await _phbB05BCTCWorkService.Queryable()
                    .FirstOrDefaultAsync(detail => detail.PHB_B05_BCTC_REFID == Refid);

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

        public class Report
        {
            public string DSDVQHNS { get; set; }
            public int NAM { get; set; }

            public int KY { get; set; }

        }

        [Route("GetDetailWorkforReport")]
        [HttpPost]
        public async Task<IHttpActionResult> GetDetailWorkforReport(Report report)
        {
            var response = new Response<PHB_B05_BCTC_WORK>();

            //get all details by refid
            try
            {
                var refid = _phbB05BCTCService.Queryable().FirstOrDefault(x => x.NAM_BC == report.NAM && x.MA_QHNS == report.DSDVQHNS && x.KY_BC == report.KY).REFID;
                //Get Dữ liệu data trong bảng detail
                response.Data = await _phbB05BCTCWorkService.Queryable()
                    .FirstOrDefaultAsync(detail => detail.PHB_B05_BCTC_REFID == refid);
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
        public async Task<IHttpActionResult> Edit(PhbB05BCTCVm.EditConten model)
        {
            var response = new Response();
            var form = new PHB_B05_BCTC();
            var lstDetail = new List<PHB_B05_BCTC_DETAIL>();
            var formWork = new PHB_B05_BCTC_WORK();
            if (model == null || model.dataTable.Count() < 0 || model.dataWork == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }


            try
            {
                form = _phbB05BCTCService.Queryable().FirstOrDefault(x => x.REFID == model.Formid);
                lstDetail = _phbB05BCTCDetailService.Queryable().Where(x => x.PHB_B05_BCTC_REFID == model.Formid).ToList();
                formWork = _phbB05BCTCWorkService.Queryable().FirstOrDefault(x => x.PHB_B05_BCTC_REFID == model.Formid);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

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

            formWork.DON_VI = model.dataWork.DON_VI;
            formWork.QD_TL_SO = model.dataWork.QD_TL_SO;
            formWork.NGAY = model.dataWork.NGAY;
            formWork.THANG = model.dataWork.THANG;
            formWork.NAM = model.dataWork.NAM;
            formWork.CO_QUAN_CAP = model.dataWork.CO_QUAN_CAP;
            formWork.THUOC_DV = model.dataWork.THUOC_DV;
            formWork.LOAI_HINH_DV = model.dataWork.LOAI_HINH_DV;
            formWork.CHU_TC = model.dataWork.CHU_TC;
            formWork.NV_CHINH_DV = model.dataWork.NV_CHINH_DV;
            formWork.TT_THUYETMINH = model.dataWork.TT_THUYETMINH;
            formWork.ObjectState = ObjectState.Modified;

            try
            {
                // delete all old details
                foreach (var detail in lstDetail)
                {
                    _phbB05BCTCDetailService.Delete(detail);
                }

                // add new details
                foreach (var detail in model.dataTable)
                {
                    detail.ID = 0;
                    detail.PHB_B05_BCTC_REFID = form.REFID;
                    _phbB05BCTCDetailService.Insert(detail);
                }

                _unitOfWorkAsync.SaveChanges();
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
        [HttpPost]
        public IHttpActionResult Delete(string formId)
        {
            var response = new Response<string>();

            //get report by refid
            var form = new PHB_B05_BCTC();
            try
            {
                form = _phbB05BCTCService.Queryable().Where(frm => frm.REFID == formId).FirstOrDefault();
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

            //get list details by refid
            var lstDetail = new List<PHB_B05_BCTC_DETAIL>();
            var formWork = new PHB_B05_BCTC_WORK();
            try
            {
                lstDetail = _phbB05BCTCDetailService.Queryable().Where(detail => detail.PHB_B05_BCTC_REFID == form.REFID).ToList();
                formWork = _phbB05BCTCWorkService.Queryable().Where(frm => frm.PHB_B05_BCTC_REFID == formId).FirstOrDefault();
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
                _phbB05BCTCService.Delete(form);
                _phbB05BCTCWorkService.Delete(formWork);
                foreach (var detail in lstDetail)
                {
                    _phbB05BCTCDetailService.Delete(detail);
                }

                _unitOfWorkAsync.SaveChanges();
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
    }



}