using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B03TT90;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT.B03_TT90;
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

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbB03_TT90")]
    [Route("{id?}")]
    public class PhbB03TT90Controller : ApiController
    {
        private readonly IDmLoaiKhoanService _LoaiKhoanservice;
        private readonly IPhbB03_TT90Service _phbB03_TT90Service;
        private readonly IPhbB03_TT90TemplateService _phbB03_TT90TemplateService;
        private readonly IPhbB03_TT90DetailService _phbB03_TT90DetailService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IDmChuongService _dmChuong;

        public PhbB03TT90Controller(IDmLoaiKhoanService LoaiKhoanservice, IPhbB03_TT90Service phbB03_TT90Service, IPhbB03_TT90TemplateService phbB03_TT90TemplateService, IPhbB03_TT90DetailService phbB03_TT90DetailService,
            ISysDvqhns_QuanLyService sysDVQHNSQLService, IUnitOfWorkAsync unitOfWorkAsync, IDM_NGANHKTService dmNganhktService, IDmChuongService dmChuong)
        {
            _LoaiKhoanservice = LoaiKhoanservice;
            _phbB03_TT90Service = phbB03_TT90Service;
            _phbB03_TT90TemplateService = phbB03_TT90TemplateService;
            _phbB03_TT90DetailService = phbB03_TT90DetailService;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
            _dmNganhktService = dmNganhktService;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(B03_TT90Vm.ContentData item)
        {
            var response = new Response<List<B03_TT90Vm.ContentData>>();
            List<PHB_B03_TT90> data = new List<PHB_B03_TT90>();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
            var StrDONVI = currentMA_QHNS.Split(',');
            try
            {
                if (string.IsNullOrEmpty(item.MA_QHNS))
                {
                    data = _phbB03_TT90Service.Queryable().Where(x => x.NAM_BC == item.NAM_BC && StrDONVI.Contains(x.MA_QHNS)).ToList();
                }
                else
                {
                    data = _phbB03_TT90Service.Queryable().Where(x => x.NAM_BC == item.NAM_BC && (x.MA_QHNS == item.MA_QHNS || StrDONVI.Contains(x.MA_QHNS))).ToList();
                }
                var result = new List<B03_TT90Vm.ContentData>();
                if (data.Count > 0)
                {
                    foreach (var dataObj in data)
                    {
                        var tempResult = new B03_TT90Vm.ContentData
                        {
                            MA_QHNS = dataObj.MA_QHNS,
                            REFID = dataObj.REFID,
                            NAM_BC = dataObj.NAM_BC,
                            NGAY_SUA = dataObj.NGAY_SUA,
                            NGAY_TAO = dataObj.NGAY_TAO,
                            TRANG_THAI = item.TRANG_THAI
                        };
                        var tempDVSDNS = _sysDVQHNSQLService.Queryable().FirstOrDefault(x => x.MA_DVQHNS == dataObj.MA_QHNS);
                        if (tempDVSDNS != null)
                        {
                            tempResult.TEN_DVQHNS = tempDVSDNS.TEN_DVQHNS;
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
            Response<List<PHB_B03_TT90_TEMPLATE>> response = new Response<List<PHB_B03_TT90_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phbB03_TT90TemplateService.Queryable()
                    .OrderBy(x => x.SAPXEP)
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
        public async Task<IHttpActionResult> AddContent(B03_TT90Vm.ContentData item)

        {
            var response = new Response<B03_TT90Vm.ContentData>();
            try
            {
                PHB_B03_TT90 model = new PHB_B03_TT90();

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                model.MA_QHNS = item.MA_QHNS;
                model.TEN_QHNS = item.TEN_DVQHNS;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM_BC = item.NAM_BC;
                model.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _phbB03_TT90Service.Queryable()
                    .Where(report => report.MA_QHNS == model.MA_QHNS && report.NAM_BC == model.NAM_BC)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _phbB03_TT90Service.Insert(model);

                var data = item.lstDetail.ToList();
                foreach (var v in data)
                {
                    v.PHB_B03_TT90_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _phbB03_TT90DetailService.Insert(v);
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
            var response = new Response<List<PHB_B03_TT90_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _phbB03_TT90DetailService.Queryable()
                    .Where(detail => detail.PHB_B03_TT90_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(List<B03_TT90Vm.DetailVm> items)
        {

            var response = new Response<string>();
            var refid = items[0].PHB_B03_TT90_REFID;
            var itemData = await _phbB03_TT90Service.Queryable().FirstOrDefaultAsync(X => X.REFID == refid);
            if (itemData != null && itemData.TRANG_THAI == 1)
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
                    _phbB03_TT90Service.Update(itemData);
                }
                else
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EMPTY_DATA;
                }

                if (items.Count > 0)
                {
                    foreach (var record in items)
                    {
                        if (!record.isAss)
                        {
                            var itemDetail = await _phbB03_TT90DetailService.Queryable().FirstOrDefaultAsync(X => X.MA_CHI_TIEU == record.MA_CHI_TIEU);
                            if (itemDetail != null)
                            {
                                itemDetail.DU_TOAN_NAM = record.DU_TOAN_NAM;
                                itemDetail.UOC_THUC_HIEN = record.UOC_THUC_HIEN;
                                itemDetail.UOC_THUC_HIEN_DU_TOAN = record.UOC_THUC_HIEN_DU_TOAN;
                                itemDetail.UOC_THUC_HIEN_CUNG_KY = record.UOC_THUC_HIEN_CUNG_KY;
                                itemDetail.ObjectState = ObjectState.Modified;
                                _phbB03_TT90DetailService.Update(itemDetail);
                            }
                        }
                        else
                        {
                            var itemDetail = new PHB_B03_TT90_DETAIL
                            {
                                PHB_B03_TT90_REFID = refid,
                                TEN_CHI_TIEU = record.TEN_CHI_TIEU,
                                STT_CHI_TIEU = record.STT_CHI_TIEU,
                                SAP_XEP = record.SAP_XEP,
                                MA_CHI_TIEU = record.MA_CHI_TIEU,
                                DU_TOAN_NAM = record.DU_TOAN_NAM,
                                UOC_THUC_HIEN = record.UOC_THUC_HIEN,
                                UOC_THUC_HIEN_DU_TOAN = record.UOC_THUC_HIEN_DU_TOAN,
                                UOC_THUC_HIEN_CUNG_KY = record.UOC_THUC_HIEN_CUNG_KY,
                                ObjectState = ObjectState.Added
                            };
                            _phbB03_TT90DetailService.Insert(itemDetail);
                        }
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
            var report = new PHB_B03_TT90();
            try
            {
                report = await _phbB03_TT90Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
                var lstDetail = new List<PHB_B03_TT90_DETAIL>();
                lstDetail = await _phbB03_TT90DetailService.Queryable().Where(detail => detail.PHB_B03_TT90_REFID == refid).ToListAsync();
                _phbB03_TT90Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _phbB03_TT90DetailService.Delete(detail);
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
                var bc = new PHB_B03_TT90
                {
                    NAM_BC = model.ReportHeader.ReportYear,
                    MA_QHNS = model.ReportHeader.CompanyID,
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _phbB03_TT90Service
                    .Queryable()
                    .Count(report => report.MA_QHNS == model.ReportHeader.CompanyID && report.NAM_BC == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _phbB03_TT90Service.Insert(bc);

                var details = model.B03TT90Detail;
                foreach (var t in details)
                {
                    var item = new PHB_B03_TT90_DETAIL
                    {
                        PHB_B03_TT90_REFID = bc.REFID,
                        TEN_CHI_TIEU = t.ReportItemName,
                        STT_CHI_TIEU = t.ReportItemAlias,
                        SAP_XEP = t.ReportItemIndex,
                        MA_CHI_TIEU = t.ReportItemCode.ToString(),
                        DU_TOAN_NAM = t.EstimateAmount,
                        UOC_THUC_HIEN = t.ApprovedAmount,
                        UOC_THUC_HIEN_DU_TOAN = t.OtherAmount
                    };
                    _phbB03_TT90DetailService.Insert(item);
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
        public async Task<IHttpActionResult> ReceiveDataFromService(List<B03TT90Model> model)
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
                        foreach (var rpB03_TT90 in model)
                        {
                            string msg = _phbB03_TT90Service.IfExistsRpPeriodThenDelete(rpB03_TT90.ReportHeader.CompanyID, rpB03_TT90.ReportHeader.ReportPeriod, rpB03_TT90.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _phbB03_TT90Service.InsertData(rpB03_TT90, context);
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