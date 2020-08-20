using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B04TT90;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT.B04_TT90;
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
    [RoutePrefix("api/report/phbB04_TT90")]
    [Route("{id?}")]
    public class PhbB04TT90Controller : ApiController
    {
        private readonly IDmLoaiKhoanService _LoaiKhoanservice;
        private readonly IPhbB04_TT90Service _phbB04_TT90Service;
        private readonly IPhbB04_TT90TemplateService _phbB04_TT90TemplateService;
        private readonly IPhbB04_TT90DetailService _phbB04_TT90DetailService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IDmChuongService _dmChuong;

        public PhbB04TT90Controller(IDmLoaiKhoanService LoaiKhoanservice, IPhbB04_TT90Service phbB04_TT90Service, IPhbB04_TT90TemplateService phbB04_TT90TemplateService, IPhbB04_TT90DetailService phbB04_TT90DetailService,
            ISysDvqhns_QuanLyService sysDVQHNSQLService, IUnitOfWorkAsync unitOfWorkAsync, IDM_NGANHKTService dmNganhktService, IDmChuongService dmChuong)
        {
            _LoaiKhoanservice = LoaiKhoanservice;
            _phbB04_TT90Service = phbB04_TT90Service;
            _phbB04_TT90TemplateService = phbB04_TT90TemplateService;
            _phbB04_TT90DetailService = phbB04_TT90DetailService;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _unitOfWorkAsync = unitOfWorkAsync;
            _dmChuong = dmChuong;
            _dmNganhktService = dmNganhktService;
        }

        [Route("PostQuery")]
        [HttpPost]
        public async Task<IHttpActionResult> PostQuery(B04_TT90Vm.ContentData item)
        {
            var response = new Response<List<B04_TT90Vm.ContentData>>();
            List<PHB_B04_TT90> data = new List<PHB_B04_TT90>();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var currentMA_QHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_QHNS_QL").Value.ToString();
            var StrDONVI = currentMA_QHNS.Split(',');
            try
            {
                if (string.IsNullOrEmpty(item.MA_QHNS))
                {
                    data = _phbB04_TT90Service.Queryable().Where(x => x.NAM_BC == item.NAM_BC && StrDONVI.Contains(x.MA_QHNS)).ToList();
                }
                else
                {
                    data = _phbB04_TT90Service.Queryable().Where(x => x.NAM_BC == item.NAM_BC && (x.MA_QHNS == item.MA_QHNS || StrDONVI.Contains(x.MA_QHNS))).ToList();
                }
                var result = new List<B04_TT90Vm.ContentData>();
                if (data.Count > 0)
                {
                    foreach (var dataObj in data)
                    {
                        var tempResult = new B04_TT90Vm.ContentData
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
            Response<List<PHB_B04_TT90_TEMPLATE>> response = new Response<List<PHB_B04_TT90_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _phbB04_TT90TemplateService.Queryable()
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
        public async Task<IHttpActionResult> AddContent(B04_TT90Vm.ContentData item)

        {
            var response = new Response<B04_TT90Vm.ContentData>();
            try
            {
                PHB_B04_TT90 model = new PHB_B04_TT90();

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                model.MA_QHNS = item.MA_QHNS;
                model.TEN_QHNS = item.TEN_DVQHNS;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM_BC = item.NAM_BC;
                model.REFID = Guid.NewGuid().ToString();

                //check đã có báo cáo chưa
                var reportCount = _phbB04_TT90Service.Queryable()
                    .Where(report => report.MA_QHNS == model.MA_QHNS && report.NAM_BC == model.NAM_BC)
                    .Count();

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _phbB04_TT90Service.Insert(model);

                var data = item.lstDetail.ToList();
                foreach (var v in data)
                {
                    v.PHB_B04_TT90_REFID = model.REFID;
                    v.ObjectState = ObjectState.Added;
                    _phbB04_TT90DetailService.Insert(v);
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
            var response = new Response<List<PHB_B04_TT90_DETAIL>>();

            //get all details by refid
            try
            {
                response.Data = await _phbB04_TT90DetailService.Queryable()
                    .Where(detail => detail.PHB_B04_TT90_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(List<B04_TT90Vm.DetailVm> items)
        {

            var response = new Response<string>();
            var refid = items[0].PHB_B04_TT90_REFID;
            var itemData = await _phbB04_TT90Service.Queryable().FirstOrDefaultAsync(X => X.REFID == refid);
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
                    _phbB04_TT90Service.Update(itemData);
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
                        if (!record.isAdd)
                        {
                            var itemDetail = await _phbB04_TT90DetailService.Queryable().FirstOrDefaultAsync(X => X.MA_CHI_TIEU == record.MA_CHI_TIEU);
                            if (itemDetail != null)
                            {
                                itemDetail.SAP_XEP = record.SAP_XEP;
                                itemDetail.CHENH_LECH = record.CHENH_LECH;
                                itemDetail.SOQUYETTOAN_DUOCDUYET = record.SOQUYETTOAN_DUOCDUYET;
                                itemDetail.TONGSOLIEU_BCQT = record.TONGSOLIEU_BCQT;
                                itemDetail.TONGSOLIEU_QT_DUOCDUYET = record.TONGSOLIEU_QT_DUOCDUYET;
                                itemDetail.ObjectState = ObjectState.Modified;
                                _phbB04_TT90DetailService.Update(itemDetail);
                            }
                        }
                        else
                        {
                            var itemDetail = new PHB_B04_TT90_DETAIL
                            {
                                PHB_B04_TT90_REFID = refid,
                                SAP_XEP = record.SAP_XEP,
                                TEN_CHI_TIEU = record.TEN_CHI_TIEU,
                                MA_CHI_TIEU = record.MA_CHI_TIEU,
                                STT_CHI_TIEU = record.STT_CHI_TIEU,
                                CHENH_LECH = record.CHENH_LECH,
                                SOQUYETTOAN_DUOCDUYET = record.SOQUYETTOAN_DUOCDUYET,
                                TONGSOLIEU_BCQT = record.TONGSOLIEU_BCQT,
                                TONGSOLIEU_QT_DUOCDUYET = record.TONGSOLIEU_QT_DUOCDUYET,
                                ObjectState = ObjectState.Added
                            };
                            _phbB04_TT90DetailService.Insert(itemDetail);
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
            var report = new PHB_B04_TT90();
            try
            {
                report = await _phbB04_TT90Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
                var lstDetail = new List<PHB_B04_TT90_DETAIL>();
                lstDetail = await _phbB04_TT90DetailService.Queryable().Where(detail => detail.PHB_B04_TT90_REFID == refid).ToListAsync();
                _phbB04_TT90Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _phbB04_TT90DetailService.Delete(detail);
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
                var bc = new PHB_B04_TT90
                {
                    NAM_BC = model.ReportHeader.ReportYear,
                    MA_QHNS = model.ReportHeader.CompanyID,
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _phbB04_TT90Service
                    .Queryable()
                    .Count(report => report.MA_QHNS == model.ReportHeader.CompanyID && report.NAM_BC == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _phbB04_TT90Service.Insert(bc);

                var details = model.B04TT90Detail;
                foreach (var t in details)
                {
                    var item = new PHB_B04_TT90_DETAIL
                    {
                        PHB_B04_TT90_REFID = bc.REFID,
                        TEN_CHI_TIEU = t.ReportItemName,
                        STT_CHI_TIEU = t.ReportItemAlias,
                        SAP_XEP = t.ReportItemIndex,
                        MA_CHI_TIEU = t.ReportItemCode.ToString(),
                        TONGSOLIEU_BCQT = t.EstimateAmount,
                        TONGSOLIEU_QT_DUOCDUYET = t.ApprovedAmount,
                    };
                    _phbB04_TT90DetailService.Insert(item);
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
        public async Task<IHttpActionResult> ReceiveDataFromService(List<B04TT90Model> model)
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
                        foreach (var rpB04_TT90 in model)
                        {
                            string msg = _phbB04_TT90Service.IfExistsRpPeriodThenDelete(rpB04_TT90.ReportHeader.CompanyID, rpB04_TT90.ReportHeader.ReportPeriod, rpB04_TT90.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _phbB04_TT90Service.InsertData(rpB04_TT90, context);
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