using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.MisaModel.B04;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107;
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
    [RoutePrefix("api/reportbctc/BC04_BCTC_TT107")]
    [Route("{id?}")]
    public class BC04BctcTT107Controller : ApiController
    {
        private readonly IBc04BCTCTT107Service _bc04Bctctt107Service;
        private readonly IBc04BCTCTT107DetailService _bc04Bctctt107DetailService;
        private readonly IBc04BCTCTT107TemplateService _bc04Bctctt107TemplateService;
        private readonly ISysDvqhns_QuanLyService _sysDVQHNSQLService;
        private readonly ISysDvqhnsService _sysDVQHNSService;
        private readonly IDM_NGANHKTService _dmNganhktService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public BC04BctcTT107Controller(IBc04BCTCTT107Service bc04Bctctt107Service, IBc04BCTCTT107DetailService bc04Bctctt107DetailService, IBc04BCTCTT107TemplateService bc04Bctctt107TemplateService, IUnitOfWorkAsync unitOfWorkAsync, ISysDvqhns_QuanLyService sysDVQHNSQLService, IDM_NGANHKTService dmNganhktService, ISysDvqhnsService sysDvqhnsService)
        {
            _bc04Bctctt107Service = bc04Bctctt107Service;
            _bc04Bctctt107DetailService = bc04Bctctt107DetailService;
            _bc04Bctctt107TemplateService = bc04Bctctt107TemplateService;
            _sysDVQHNSQLService = sysDVQHNSQLService;
            _sysDVQHNSService = sysDvqhnsService;
            _dmNganhktService = dmNganhktService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        //[Route("PostQuery")]
        //[HttpPost]
        //public async Task<IHttpActionResult> PostQuery(BC04_BCTC_TT107Vm.ContentData item)
        //{
        //    var response = new Response<List<BC04_BCTC_TT107Vm.ContentData>>();
        //    List<BC04_BCTC_TT107> data = new List<BC04_BCTC_TT107>();
        //    var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
        //    var currentMA_DVQHNS = identity.Claims.FirstOrDefault(c => c.Type == "MA_DONVI").Value.ToString();
        //    var tempDVQHNS = _sysDVQHNSService.Queryable().FirstOrDefault(x => x.MA_DVQHNS_CHA == currentMA_DVQHNS);
        //    try
        //    {
        //        if (string.IsNullOrEmpty(item.MA_DONVI))
        //        {
        //            data = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
        //        }
        //        else
        //        {
        //            data = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
        //        }
        //        var result = new List<BC04_BCTC_TT107Vm.ContentData>();
        //        if (data.Count > 0)
        //        {
        //            foreach (var dataObj in data)
        //            {
        //                var tempResult = new BC04_BCTC_TT107Vm.ContentData
        //                {
        //                    MA_DONVI = dataObj.MA_DONVI,
        //                    REFID = dataObj.REFID,
        //                    NAM = dataObj.NAM,
        //                    CAP_DU_TOAN = dataObj.CAP_DU_TOAN,
        //                    NGAY_SUA = dataObj.NGAY_SUA,
        //                    NGAY_TAO = dataObj.NGAY_TAO,
        //                    TRANG_THAI = dataObj.TRANG_THAI,
        //                    BCTC_NGAY_PHEDUYET = dataObj.BCTC_NGAY_PHEDUYET,
        //                    CHUCNANG_NHIEMVU = dataObj.CHUCNANG_NHIEMVU,
        //                    BCTC_PHEDUYET = dataObj.BCTC_PHEDUYET,
        //                    DON_VI_DT = dataObj.DON_VI_DT,
        //                    MA_LOAIHINH = dataObj.MA_LOAIHINH,
        //                    NGAY_QD_GIAO = dataObj.NGAY_QD_GIAO,
        //                    NGAY_QD_THANHLAP = dataObj.NGAY_QD_THANHLAP,
        //                    SO_QD_GIAO = dataObj.SO_QD_GIAO,
        //                    SO_QD_THANHLAP = dataObj.SO_QD_THANHLAP,
        //                    THUOC_DONVI_CAP1 = dataObj.THUOC_DONVI_CAP1,
        //                    TEN_DONVI_CAPTREN = dataObj.TEN_DONVI_CAPTREN,
        //                    TEN_LOAIHINH = dataObj.TEN_LOAIHINH,
        //                    TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI
        //                };
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
        public async Task<IHttpActionResult> PostQuery(BC04_BCTC_TT107Vm.ContentData item)
        {
            var response = new Response<List<BC04_BCTC_TT107Vm.ContentData>>();
            var result = new List<BC04_BCTC_TT107Vm.ContentData>();
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
                    var dataCha = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == tempDVQHNS.MA_DVQHNS_CHA).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new BC04_BCTC_TT107Vm.ContentData
                            {
                                MA_DONVI = dataObj.MA_DONVI,
                                REFID = dataObj.REFID,
                                NAM = dataObj.NAM,
                                CAP_DU_TOAN = dataObj.CAP_DU_TOAN,
                                NGAY_SUA = dataObj.NGAY_SUA,
                                NGUOI_TAO = dataObj.NGUOI_TAO,
                                NGAY_TAO = dataObj.NGAY_TAO,
                                TRANG_THAI = dataObj.TRANG_THAI,
                                BCTC_NGAY_PHEDUYET = dataObj.BCTC_NGAY_PHEDUYET,
                                CHUCNANG_NHIEMVU = dataObj.CHUCNANG_NHIEMVU,
                                BCTC_PHEDUYET = dataObj.BCTC_PHEDUYET,
                                DON_VI_DT = dataObj.DON_VI_DT,
                                MA_LOAIHINH = dataObj.MA_LOAIHINH,
                                NGAY_QD_GIAO = dataObj.NGAY_QD_GIAO,
                                NGAY_QD_THANHLAP = dataObj.NGAY_QD_THANHLAP,
                                SO_QD_GIAO = dataObj.SO_QD_GIAO,
                                SO_QD_THANHLAP = dataObj.SO_QD_THANHLAP,
                                THUOC_DONVI_CAP1 = dataObj.THUOC_DONVI_CAP1,
                                TEN_DONVI_CAPTREN = dataObj.TEN_DONVI_CAPTREN,
                                TEN_LOAIHINH = dataObj.TEN_LOAIHINH,
                                TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI
                            };

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
                            var data = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI.Contains(dvqhns.MA_DVQHNS)).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new BC04_BCTC_TT107Vm.ContentData
                                    {
                                        MA_DONVI = dataObj.MA_DONVI,
                                        REFID = dataObj.REFID,
                                        NAM = dataObj.NAM,
                                        CAP_DU_TOAN = dataObj.CAP_DU_TOAN,
                                        NGAY_SUA = dataObj.NGAY_SUA,
                                        NGUOI_TAO = dataObj.NGUOI_TAO,
                                        NGAY_TAO = dataObj.NGAY_TAO,
                                        TRANG_THAI = dataObj.TRANG_THAI,
                                        BCTC_NGAY_PHEDUYET = dataObj.BCTC_NGAY_PHEDUYET,
                                        CHUCNANG_NHIEMVU = dataObj.CHUCNANG_NHIEMVU,
                                        BCTC_PHEDUYET = dataObj.BCTC_PHEDUYET,
                                        DON_VI_DT = dataObj.DON_VI_DT,
                                        MA_LOAIHINH = dataObj.MA_LOAIHINH,
                                        NGAY_QD_GIAO = dataObj.NGAY_QD_GIAO,
                                        NGAY_QD_THANHLAP = dataObj.NGAY_QD_THANHLAP,
                                        SO_QD_GIAO = dataObj.SO_QD_GIAO,
                                        SO_QD_THANHLAP = dataObj.SO_QD_THANHLAP,
                                        THUOC_DONVI_CAP1 = dataObj.THUOC_DONVI_CAP1,
                                        TEN_DONVI_CAPTREN = dataObj.TEN_DONVI_CAPTREN,
                                        TEN_LOAIHINH = dataObj.TEN_LOAIHINH,
                                        TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI
                                    };

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
                    //var dataCha = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && (x.MA_DONVI == item.MA_DONVI || StrDONVI.Contains(x.MA_DONVI))).ToList();
                    var dataCha = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == item.MA_DONVI).ToList();
                    if (dataCha.Count > 0)
                    {
                        foreach (var dataObj in dataCha)
                        {
                            var tempResult = new BC04_BCTC_TT107Vm.ContentData
                            {
                                MA_DONVI = dataObj.MA_DONVI,
                                REFID = dataObj.REFID,
                                NAM = dataObj.NAM,
                                CAP_DU_TOAN = dataObj.CAP_DU_TOAN,
                                NGAY_SUA = dataObj.NGAY_SUA,
                                NGUOI_TAO = dataObj.NGUOI_TAO,
                                NGAY_TAO = dataObj.NGAY_TAO,
                                TRANG_THAI = dataObj.TRANG_THAI,
                                BCTC_NGAY_PHEDUYET = dataObj.BCTC_NGAY_PHEDUYET,
                                CHUCNANG_NHIEMVU = dataObj.CHUCNANG_NHIEMVU,
                                BCTC_PHEDUYET = dataObj.BCTC_PHEDUYET,
                                DON_VI_DT = dataObj.DON_VI_DT,
                                MA_LOAIHINH = dataObj.MA_LOAIHINH,
                                NGAY_QD_GIAO = dataObj.NGAY_QD_GIAO,
                                NGAY_QD_THANHLAP = dataObj.NGAY_QD_THANHLAP,
                                SO_QD_GIAO = dataObj.SO_QD_GIAO,
                                SO_QD_THANHLAP = dataObj.SO_QD_THANHLAP,
                                THUOC_DONVI_CAP1 = dataObj.THUOC_DONVI_CAP1,
                                TEN_DONVI_CAPTREN = dataObj.TEN_DONVI_CAPTREN,
                                TEN_LOAIHINH = dataObj.TEN_LOAIHINH,
                                TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI
                            };

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
                            var data = _bc04Bctctt107Service.Queryable().Where(x => x.NAM == item.NAM && x.MA_DONVI == dvqhns.MA_DVQHNS).ToList();
                            if (data.Count > 0)
                            {
                                foreach (var dataObj in data)
                                {
                                    var tempResult = new BC04_BCTC_TT107Vm.ContentData
                                    {
                                        MA_DONVI = dataObj.MA_DONVI,
                                        REFID = dataObj.REFID,
                                        NAM = dataObj.NAM,
                                        CAP_DU_TOAN = dataObj.CAP_DU_TOAN,
                                        NGAY_SUA = dataObj.NGAY_SUA,
                                        NGUOI_TAO = dataObj.NGUOI_TAO,
                                        NGAY_TAO = dataObj.NGAY_TAO,
                                        TRANG_THAI = dataObj.TRANG_THAI,
                                        BCTC_NGAY_PHEDUYET = dataObj.BCTC_NGAY_PHEDUYET,
                                        CHUCNANG_NHIEMVU = dataObj.CHUCNANG_NHIEMVU,
                                        BCTC_PHEDUYET = dataObj.BCTC_PHEDUYET,
                                        DON_VI_DT = dataObj.DON_VI_DT,
                                        MA_LOAIHINH = dataObj.MA_LOAIHINH,
                                        NGAY_QD_GIAO = dataObj.NGAY_QD_GIAO,
                                        NGAY_QD_THANHLAP = dataObj.NGAY_QD_THANHLAP,
                                        SO_QD_GIAO = dataObj.SO_QD_GIAO,
                                        SO_QD_THANHLAP = dataObj.SO_QD_THANHLAP,
                                        THUOC_DONVI_CAP1 = dataObj.THUOC_DONVI_CAP1,
                                        TEN_DONVI_CAPTREN = dataObj.TEN_DONVI_CAPTREN,
                                        TEN_LOAIHINH = dataObj.TEN_LOAIHINH,
                                        TRANG_THAI_GUI = dataObj.TRANG_THAI_GUI
                                    };

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
            Response<List<BC04_BCTC_TT107_TEMPLATE>> response = new Response<List<BC04_BCTC_TT107_TEMPLATE>>();
            try
            {
                response.Error = false;
                response.Data = await _bc04Bctctt107TemplateService.Queryable()
                    .OrderBy(x => x.SAP_XEP)
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

        public class InsertObject_BC04
        {
            public BC04_BCTC_TT107 model { get; set; }
            public List<BC04_BCTC_TT107_DETAILS> detail { get; set; }
        }

        [Route("AddContent")]
        [HttpPost]
        public async Task<IHttpActionResult> AddContent(InsertObject_BC04 instance)
        {
            var response = new Response<string>();
            try
            {
                BC04_BCTC_TT107 model = new BC04_BCTC_TT107();

                var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

                model.MA_DONVI = instance.model.MA_DONVI;
                model.NGUOI_TAO = RequestContext.Principal.Identity.Name;
                model.NGAY_TAO = DateTime.Now;
                model.NAM = instance.model.NAM;
                model.CAP_DU_TOAN = instance.model.CAP_DU_TOAN;
                model.NGAY_SUA = instance.model.NGAY_SUA;
                model.BCTC_NGAY_PHEDUYET = instance.model.BCTC_NGAY_PHEDUYET;
                model.CHUCNANG_NHIEMVU = instance.model.CHUCNANG_NHIEMVU;
                model.BCTC_PHEDUYET = instance.model.BCTC_PHEDUYET;
                model.DON_VI_DT = instance.model.DON_VI_DT;
                model.MA_LOAIHINH = instance.model.MA_LOAIHINH;
                model.NGAY_QD_GIAO = instance.model.NGAY_QD_GIAO;
                model.NGAY_QD_THANHLAP = instance.model.NGAY_QD_THANHLAP;
                model.SO_QD_GIAO = instance.model.SO_QD_GIAO;
                model.SO_QD_THANHLAP = instance.model.SO_QD_THANHLAP;
                model.THUOC_DONVI_CAP1 = instance.model.THUOC_DONVI_CAP1;
                model.TEN_DONVI_CAPTREN = instance.model.TEN_DONVI_CAPTREN;
                model.TEN_LOAIHINH = instance.model.TEN_LOAIHINH;
                model.REFID = Guid.NewGuid().ToString();
                model.TRANG_THAI = instance.model.TRANG_THAI;
                model.TRANG_THAI_GUI = instance.model.TRANG_THAI_GUI;

                //check đã có báo cáo chưa
                var reportCount = _bc04Bctctt107Service
                    .Queryable()
                    .Count(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                _bc04Bctctt107Service.Insert(model);

                foreach (var item in instance.detail)
                {
                    item.BC04_BCTC_TT107_REFID = model.REFID;
                    _bc04Bctctt107DetailService.Insert(item);
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
            var response = new Response<InsertObject_BC04>();

            //get all details by refid
            try
            {
                response.Data = new InsertObject_BC04();
                var data = _bc04Bctctt107Service.Queryable().FirstOrDefault(x => x.REFID == refid);
                response.Data.model = data;
                response.Data.detail = await _bc04Bctctt107DetailService.Queryable()
                    .Where(detail => detail.BC04_BCTC_TT107_REFID == refid)
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
        public async Task<IHttpActionResult> Edit(InsertObject_BC04 instance)
        {
            var response = new Response<string>();

            if (instance.model == null || instance.detail.Count == 0)
            {
                response.Message = ErrorMessage.EMPTY_DATA;
                response.Error = true;
                return Ok(response);
            }

            var report = new BC04_BCTC_TT107();
            try
            {
                var refid = instance.model.REFID;
                report = await _bc04Bctctt107Service.Queryable().Where(r => r.REFID == refid).FirstOrDefaultAsync();

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
                report.MA_DONVI = instance.model.MA_DONVI;
                report.NAM = instance.model.NAM;
                report.CAP_DU_TOAN = instance.model.CAP_DU_TOAN;
                report.NGAY_SUA = instance.model.NGAY_SUA;
                report.BCTC_NGAY_PHEDUYET = instance.model.BCTC_NGAY_PHEDUYET;
                report.CHUCNANG_NHIEMVU = instance.model.CHUCNANG_NHIEMVU;
                report.BCTC_PHEDUYET = instance.model.BCTC_PHEDUYET;
                report.DON_VI_DT = instance.model.DON_VI_DT;
                report.MA_LOAIHINH = instance.model.MA_LOAIHINH;
                report.NGAY_QD_GIAO = instance.model.NGAY_QD_GIAO;
                report.NGAY_QD_THANHLAP = instance.model.NGAY_QD_THANHLAP;
                report.SO_QD_GIAO = instance.model.SO_QD_GIAO;
                report.SO_QD_THANHLAP = instance.model.SO_QD_THANHLAP;
                report.THUOC_DONVI_CAP1 = instance.model.THUOC_DONVI_CAP1;
                report.TEN_DONVI_CAPTREN = instance.model.TEN_DONVI_CAPTREN;
                report.TEN_LOAIHINH = instance.model.TEN_LOAIHINH;
                report.TRANG_THAI = instance.model.TRANG_THAI;
                report.TRANG_THAI_GUI = instance.model.TRANG_THAI_GUI;

                report.NGAY_SUA = DateTime.Now;
                report.NGUOI_SUA = RequestContext.Principal.Identity.Name;
                report.ObjectState = ObjectState.Modified;
                _bc04Bctctt107Service.Update(report);

                var lstDetail = await _bc04Bctctt107DetailService.Queryable().Where(detail => detail.BC04_BCTC_TT107_REFID == report.REFID).ToListAsync();

                //loop to edit each detail
                foreach (var detail in lstDetail)
                {
                    var first = instance.detail.FirstOrDefault(x => x.ID == detail.ID);
                    if (first != null)
                    {
                        detail.AMOUNT1 = first.AMOUNT1;
                        detail.AMOUNT2 = first.AMOUNT2;
                        detail.AMOUNT3 = first.AMOUNT3;
                        detail.AMOUNT4 = first.AMOUNT4;
                        detail.AMOUNT5 = first.AMOUNT5;
                        detail.AMOUNT6 = first.AMOUNT6;
                        detail.AMOUNT7 = first.AMOUNT7;
                        detail.AMOUNT8 = first.AMOUNT8;
                        detail.AMOUNT9 = first.AMOUNT9;
                        detail.AMOUNT10 = first.AMOUNT10;
                        detail.AMOUNT11 = first.AMOUNT11;
                        detail.AMOUNT12 = first.AMOUNT12;
                        detail.ObjectState = ObjectState.Modified;
                        _bc04Bctctt107DetailService.Update(detail);
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
            var report = new BC04_BCTC_TT107();
            try
            {
                report = await _bc04Bctctt107Service.Queryable().Where(re => re.REFID == refid).FirstOrDefaultAsync();
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
            var lstDetail = new List<BC04_BCTC_TT107_DETAILS>();
            try
            {
                lstDetail = await _bc04Bctctt107DetailService.Queryable().Where(detail => detail.BC04_BCTC_TT107_REFID == refid).ToListAsync();
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
                _bc04Bctctt107Service.Delete(report);
                foreach (var detail in lstDetail)
                {
                    _bc04Bctctt107DetailService.Delete(detail);
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

        public class LoaiHinhDv
        {
            public string MA_LH { get; set; }
            public string TEN_LH { get; set; }
        }

        [Route("ImportXML")]
        [HttpPost]
        public async Task<IHttpActionResult> ImportXML(XmlViewModel.InsertObj model)
        {
            var response = new Response<string>();
            var lstLH = new List<LoaiHinhDv>();
            try
            {
                lstLH.Add(new LoaiHinhDv() { MA_LH = "1", TEN_LH = "Đơn vị SNCL tự chủ chi thường xuyên và đầu tư" });
                lstLH.Add(new LoaiHinhDv() { MA_LH = "2", TEN_LH = "Đơn vị SNCL tự chủ chi thường xuyên" });
                lstLH.Add(new LoaiHinhDv() { MA_LH = "3", TEN_LH = "Đơn vị SNCL tự chủ một phần chi thường xuyên" });
                lstLH.Add(new LoaiHinhDv() { MA_LH = "4", TEN_LH = "Đơn vị SNCL do NSNN cấp kinh phí" });
                lstLH.Add(new LoaiHinhDv() { MA_LH = "5", TEN_LH = "Đơn vị hành chính được giao tự chủ kinh phí" });
                lstLH.Add(new LoaiHinhDv() { MA_LH = "6", TEN_LH = "Đơn vị hành chính không được giao tự chủ kinh phí" });

                var bc = new BC04_BCTC_TT107
                {
                    NAM = model.ReportHeader.ReportYear,
                    MA_DONVI = model.ReportHeader.CompanyID,
                    NGAY_TAO = DateTime.Now,
                    TRANG_THAI = 0,
                    NGUOI_TAO = RequestContext.Principal.Identity.Name,
                    REFID = Guid.NewGuid().ToString()
                };

                //check đã có báo cáo chưa
                var reportCount = _bc04Bctctt107Service
                    .Queryable()
                    .Count(report => report.MA_DONVI == model.ReportHeader.CompanyID && report.NAM == model.ReportHeader.ReportYear);

                if (reportCount > 0)
                {
                    response.Error = true;
                    response.Message = ErrorMessage.EXITS_REPORT;
                    return Ok(response);
                }

                var details = model.B04BCTCDetail;
                var tenDonVi = details.FirstOrDefault(x => x.ReportItemCode == "I.01")?.ContentString;
                string soQdNgayQdThanhLap = details.FirstOrDefault(x => x.ReportItemCode == "I.02")?.ContentString;
                string thuocDvCap1 = details.FirstOrDefault(x => x.ReportItemCode == "I.04")?.ContentString;
                string loaiHinhDv = details.FirstOrDefault(x => x.ReportItemCode == "I.05")?.ContentString;
                string qdGiaoTuChuTC = details.FirstOrDefault(x => x.ReportItemCode == "I.06")?.ContentString;
                string chucNangNv = details.FirstOrDefault(x => x.ReportItemCode == "I.07")?.ContentString;
                string bctcPheDuyet = details.FirstOrDefault(x => x.ReportItemCode == "II")?.ContentString;

                //tách số và ngày QĐ
                if (soQdNgayQdThanhLap != null)
                {
                    int firstPos = soQdNgayQdThanhLap.IndexOf("Số", StringComparison.Ordinal);
                    int secondPos = soQdNgayQdThanhLap.IndexOf("ngày", StringComparison.Ordinal);
                    bc.SO_QD_THANHLAP = soQdNgayQdThanhLap.Substring(firstPos, secondPos).Replace("Số", "");
                    var ngay = soQdNgayQdThanhLap.Substring(secondPos, soQdNgayQdThanhLap.Length - secondPos).Replace("ngày", "");
                    if (DateTime.TryParse(ngay, out _))
                    {
                        bc.NGAY_QD_THANHLAP = DateTime.Parse(ngay);
                    }
                }
                //tách số, ngày, của QĐ
                if (qdGiaoTuChuTC != null)
                {
                    var replaced = qdGiaoTuChuTC.Replace("Số", ";").Replace("ngày", ";").Replace("của", ";");
                    var splited = replaced.Split(';');
                    bc.SO_QD_GIAO = splited[0];
                    if (DateTime.TryParse(splited[1], out _))
                    {
                        bc.NGAY_QD_GIAO = DateTime.Parse(splited[1]);
                    }
                }
                //tách bctc phê duyệt
                if (bctcPheDuyet != null)
                {
                    var replaced = bctcPheDuyet.Replace("Báo cáo tài chính của đơn vị đã được", ";").Replace("phê duyệt để phát hành ngày", ";");
                    var splited = replaced.Split(';');
                    bc.BCTC_PHEDUYET = splited[0];
                    if (DateTime.TryParse(splited[1], out _))
                    {
                        bc.BCTC_NGAY_PHEDUYET = DateTime.Parse(splited[1]);
                    }
                }

                bc.TEN_DONVI = tenDonVi;
                bc.TEN_DONVI_CAPTREN = details.FirstOrDefault(x => x.ReportItemCode == "I.03")?.ContentString;

                if (!string.IsNullOrEmpty(thuocDvCap1))
                {
                    bc.THUOC_DONVI_CAP1 = true;
                }
                bc.THUOC_DONVI_CAP1 = false;

                var lh = lstLH.FirstOrDefault(x => x.TEN_LH == loaiHinhDv);
                if (lh != null)
                {
                    bc.MA_LOAIHINH = lh.MA_LH;
                    bc.TEN_LOAIHINH = lh.TEN_LH;
                }

                bc.CHUCNANG_NHIEMVU = chucNangNv;

                _bc04Bctctt107Service.Insert(bc);

                var detailsAmount = details.Where(x => x.ReportItemCode.Count(f => f == '.') == 2);
                foreach (var t in detailsAmount)
                {
                    var splited = t.ReportItemCode.Split('.');
                    var item = new BC04_BCTC_TT107_DETAILS
                    {
                        BC04_BCTC_TT107_REFID = bc.REFID,
                        CHI_TIEU = t.ReportItemName,
                        PHAN = int.Parse(splited[1])
                    };
                    if (item.PHAN >= 1 && item.PHAN <= 14 && item.PHAN != 4)
                    {
                        if (t.Amount1 != null) item.AMOUNT1 = t.Amount1.Value;
                        if (t.Amount2 != null) item.AMOUNT2 = t.Amount2.Value;
                    }

                    if (item.PHAN == 4)
                    {
                        if (t.Amount3 != null) item.AMOUNT3 = t.Amount3.Value;
                        if (t.Amount4 != null) item.AMOUNT4 = t.Amount4.Value;
                        if (t.Amount5 != null) item.AMOUNT5 = t.Amount5.Value;
                    }

                    if (item.PHAN == 15)
                    {
                        if (t.Amount6 != null) item.AMOUNT6 = t.Amount6.Value;
                        if (t.Amount7 != null) item.AMOUNT7 = t.Amount7.Value;
                        if (t.Amount8 != null) item.AMOUNT8 = t.Amount8.Value;
                        if (t.Amount9 != null) item.AMOUNT9 = t.Amount9.Value;
                        if (t.Amount10 != null) item.AMOUNT10 = t.Amount10.Value;
                        if (t.Amount11 != null) item.AMOUNT11 = t.Amount11.Value;
                        if (t.Amount12 != null) item.AMOUNT12 = t.Amount12.Value;
                    }
                    _bc04Bctctt107DetailService.Insert(item);
                }

                var detailsAmount2 = details.Where(x => x.ReportItemCode.StartsWith("IV"));
                foreach (var t in detailsAmount2)
                {
                    var item = new BC04_BCTC_TT107_DETAILS
                    {
                        BC04_BCTC_TT107_REFID = bc.REFID,
                        CHI_TIEU = t.ReportItemName,
                        PHAN = 16
                    };
                    if (t.Amount1 != null) item.AMOUNT1 = t.Amount1.Value;
                    if (t.Amount2 != null) item.AMOUNT2 = t.Amount2.Value;
                    _bc04Bctctt107DetailService.Insert(item);
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
        public async Task<IHttpActionResult> ReceiveDataFromService(List<B04BCTCModel> model)
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
                        foreach (var rpB04_BCTC in model)
                        {
                            string msg = _bc04Bctctt107Service.IfExistsRpPeriodThenDelete(rpB04_BCTC.ReportHeader.CompanyID, rpB04_BCTC.ReportHeader.ReportPeriod, rpB04_BCTC.ReportHeader.ReportYear, context);
                            if (msg.Length > 0)
                            {
                                transaction.Rollback();
                                response.Message = msg;
                                return Ok(response);
                            }

                            msg = _bc04Bctctt107Service.InsertData(rpB04_BCTC, context);
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