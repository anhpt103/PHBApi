using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmBaoCao;
using BTS.SP.PHB.SERVICE.UTILS;
using BTS.SP.TOOLS.BuildQuery.Implimentations;
using BTS.SP.TOOLS.BuildQuery.Result;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BCTC;
using System.Web;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2;
using System.IO;
using System.Xml.Linq;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.DmBaoCao;
using BTS.SP.API.PHB.Services;
using BTS.SP.API.PHB.ViewModels;
using BTS.SP.API.PHB.Controllers.REPORT_BCTC;
using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using BTS.SP.PHB.SERVICE.REPORT.B04_TT90;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;
using BTS.SP.PHB.SERVICE.REPORT.B01BCQT;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;
using BTS.SP.PHB.SERVICE.REPORT.F01_01BCQT;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;
using BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using BTS.SP.PHB.SERVICE.REPORT.B03_TT90;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;

namespace BTS.SP.API.PHB.Controllers.HTDM
{
    [RoutePrefix("api/dm/dmBaoCao")]
    [Route("{id?}")]
    [AllowAnonymous]
    public class DmBaoCaoController : ApiController
    {
        private readonly IDmBaocaoService _service;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        private readonly IPhbB04_TT90Service _phbB04_TT90Service;
        private readonly IPhbB04_TT90DetailService _phbB04_TT90DetailService;

        private readonly IPhbB01BCQTService _b01BcqtService;
        private readonly IPhbB01BCQTDetailService _b01BcqtDetailService;

        private readonly IPhaB01BCTCService _B01BCTCService;
        private readonly IPhaB01BCTCDetailService _B01BCTCDetailService;

        private readonly IF01_01BCQTService _f0101BcqtService;
        private readonly IF01_01BCQTDetailService _f0101BcqtDetailService;

        private readonly IPhbF01_02BCQTService _F01_02BCQTService;
        private readonly IPhbF01_02BCQTDetailService _F01_02BCQTDetailService;

        private readonly IBc04BCTCTT107Service _bc04Bctctt107Service;
        private readonly IBc04BCTCTT107DetailService _bc04Bctctt107DetailService;

        private readonly IPhaB01BSTT1Service _B01BSTT1Service;
        private readonly IPhaB01BSTT1DetailService _B01BSTT1DetailService;

        private readonly IPhbB01BSTT2Service _PhbB01BSTT2Service;
        private readonly IPhbB01BSTT2DetailService _PhbB01BSTT2DetailService;

        private readonly IPhaB02BCTCService _B02BCTCService;
        private readonly IPhaB02BCTCDetailService _B02BCTCDetailService;

        private readonly IPhaB03BBCTCService _PhaB03BBCTCService;
        private readonly IPhaB03BBCTCDetailService _PhaB03BBCTCDetailService;

        private readonly IPhbB03_TT90Service _phbB03_TT90Service;
        private readonly IPhbB03_TT90DetailService _phbB03_TT90DetailService;

        private readonly IPhaB04BCTCService _B04BCTCService;
        private readonly IPhaB04BCTCDetailService _B04BCTCDetailService;

        private readonly IPhbB05BCTCService _phbB05BCTCService;
        private readonly IPhbB05BCTCDetailService _phbB05BCTCDetailService;
        private readonly IPhbB05BCTCWorkService _phbB05BCTCWorkService;

        private readonly IDmDVQHNSService _serviceDMDVQHNS;
        private readonly IXmlService_Ver001 _xmlServiceVer001;

        private const string GENERAL_ELEMENT_NAME = "TTChung";

        #region OLD

        public DmBaoCaoController(
            IDmBaocaoService service,
            IUnitOfWorkAsync unitOfWorkAsync,
            IDmDVQHNSService serviceDMDVQHNS,
            IXmlService_Ver001 xmlServiceVer001,
            IPhbB04_TT90Service phbB04_TT90Service,
            IPhbB04_TT90DetailService phbB04_TT90DetailService,
            IPhbB01BCQTService b01BcqtService,
            IPhbB01BCQTDetailService b01BcqtDetailService,
            IPhaB01BCTCService B01BCTCService,
            IPhaB01BCTCDetailService B01BCTCDetailService,

            IF01_01BCQTService f0101BcqtService,
            IF01_01BCQTDetailService f0101BcqtDetailService,

            IPhbF01_02BCQTService F01_02BCQTService,
            IPhbF01_02BCQTDetailService F01_02BCQTDetailService,

            IBc04BCTCTT107Service bc04Bctctt107Service,
            IBc04BCTCTT107DetailService bc04Bctctt107DetailService,

            IPhaB01BSTT1Service B01BSTT1Service,
            IPhaB01BSTT1DetailService B01BSTT1DetailService,

            IPhbB01BSTT2Service PhbB01BSTT2Service,
            IPhbB01BSTT2DetailService PhbB01BSTT2DetailService,

            IPhaB02BCTCService B02BCTCService,
            IPhaB02BCTCDetailService B02BCTCDetailService,

            IPhaB03BBCTCService PhaB03BBCTCService,
            IPhaB03BBCTCDetailService PhaB03BBCTCDetailService,

            IPhbB03_TT90Service phbB03_TT90Service,
            IPhbB03_TT90DetailService phbB03_TT90DetailService,

            IPhaB04BCTCService B04BCTCService,
            IPhaB04BCTCDetailService B04BCTCDetailService,

            IPhbB05BCTCService phbB05BCTCService,
            IPhbB05BCTCDetailService phbB05BCTCDetailService,
            IPhbB05BCTCWorkService phbB05BCTCWorkService)
        {
            _service = service;
            _unitOfWorkAsync = unitOfWorkAsync;
            _serviceDMDVQHNS = serviceDMDVQHNS;

            _phbB04_TT90Service = phbB04_TT90Service;
            _phbB04_TT90DetailService = phbB04_TT90DetailService;

            _b01BcqtService = b01BcqtService;
            _b01BcqtDetailService = b01BcqtDetailService;

            _B01BCTCService = B01BCTCService;
            _B01BCTCDetailService = B01BCTCDetailService;

            _f0101BcqtService = f0101BcqtService;
            _f0101BcqtDetailService = f0101BcqtDetailService;

            _F01_02BCQTService = F01_02BCQTService;
            _F01_02BCQTDetailService = F01_02BCQTDetailService;

            _bc04Bctctt107Service = bc04Bctctt107Service;
            _bc04Bctctt107DetailService = bc04Bctctt107DetailService;

            _B01BSTT1Service = B01BSTT1Service;
            _B01BSTT1DetailService = B01BSTT1DetailService;

            _PhbB01BSTT2Service = PhbB01BSTT2Service;
            _PhbB01BSTT2DetailService = PhbB01BSTT2DetailService;

            _B02BCTCService = B02BCTCService;
            _B02BCTCDetailService = B02BCTCDetailService;

            _PhaB03BBCTCService = PhaB03BBCTCService;
            _PhaB03BBCTCDetailService = PhaB03BBCTCDetailService;

            _phbB03_TT90Service = phbB03_TT90Service;
            _phbB03_TT90DetailService = phbB03_TT90DetailService;

            _B04BCTCService = B04BCTCService;
            _B04BCTCDetailService = B04BCTCDetailService;

            _phbB05BCTCService = phbB05BCTCService;
            _phbB05BCTCDetailService = phbB05BCTCDetailService;
            _phbB05BCTCWorkService = phbB05BCTCWorkService;

            _xmlServiceVer001 = xmlServiceVer001;
        }

        [Route("paging")]
        [HttpPost]
        [CustomAuthorize(Method = "XEM", State = "phb_dmBaoCao")]
        public async Task<IHttpActionResult> Paging(JObject jsonData)
        {
            var result = new Response<PagedObj<PHB_DM_BAOCAO>>();
            var postData = ((dynamic)jsonData);
            var filtered = ((JObject)postData.filtered).ToObject<FilterObj<DmBaoCaoVm.Search>>();
            var paged = ((JObject)postData.paged).ToObject<PagedObj<PHB_DM_BAOCAO>>();
            var query = new QueryBuilder
            {
                Take = paged.itemsPerPage,
                Skip = paged.fromItem - 1
            };
            try
            {
                var filterResult = await _service.FilterAsync(filtered, query);
                if (!filterResult.WasSuccessful)
                {
                    return NotFound();
                }
                result.Data = filterResult.Value;
                result.Error = false;
                return Ok(result);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [Route("LoaiBaoCao")]
        [HttpGet]
        public IList<string> GetLoaiBaoCaO()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("I") && x.BC_THEO == "TAPMIS").Select(x => x.MO_TA).Distinct().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("GetSelectData/{loaibaocao}")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectData(string loaibaocao)
        {
            try
            {
                if (loaibaocao == "all")
                {
                    return _service.Queryable().Where(x => x.TRANG_THAI.Equals("I") && x.BC_THEO == "TAPMIS").OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
                }
                else
                {
                    return _service.Queryable().Where(x => x.TRANG_THAI.Equals("I") && x.BC_THEO == "TAPMIS" && x.MO_TA == loaibaocao).OrderBy(x => x.MA_BAO_CAO)
                   .ToList().Select(x => new ChoiceObj()
                   {
                       Value = x.MA_BAO_CAO,
                       Text = x.TEN_BAO_CAO,
                       ExtendValue = x.TEMPLATE.ToString(),
                       Id = x.ID
                   }).ToList();
                }
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetSelectDataBCTC")]
        [HttpGet]
        public IList<ChoiceObj> GetSelectDataBCTC()
        {
            try
            {
                return _service.Queryable().Where(x => x.BC_THEO == "BCTC" && x.TRANG_THAI == "I").OrderBy(x => x.MA_BAO_CAO)
                    .OrderBy(x => x.TEN_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDSBCTC")]
        [HttpGet]
        public IList<ChoiceObj> GetDSBCTC()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("B") && x.BC_THEO == "BCTC").OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDSBCDTL")]
        [HttpGet]
        public IList<ChoiceObj> GetDSBCDTL()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("L") && x.TEMPLATE < 3).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetListPBDT_TT344")]
        [HttpGet]
        public IList<ChoiceObj> GetListPBDT_TT344()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("H")).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("GetListPBDT_TT342")]
        [HttpGet]
        public IList<ChoiceObj> GetListPBDT_TT342()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("G")).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetListPBDT_QLDT")]
        [HttpGet]
        public IList<ChoiceObj> GetListPBDT_QLDT()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("K")).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDS_BaoCaoXa")]
        [HttpGet]
        public IList<ChoiceObj> GetDS_BaoCaoXa()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && x.TEMPLATE == 3).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDS_BaoCaoXa_TT334")]
        [HttpGet]
        public IList<ChoiceObj> GetDS_BaoCaoXa_TT334()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("I") && x.BC_THEO == "BaoCaoXa").OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("GetDS_BaoCaoXa_TT146")]
        [HttpGet]
        [AllowAnonymous]
        public IList<ChoiceObj> GetDS_BaoCaoXa_TT146()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && (x.BC_THEO == "BaoCaoXa")).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [Route("GetBaoCao_HCSN_TAPMIS")]
        [HttpGet]
        public IList<ChoiceObj> GetBaoCao_HCSN_TAPMIS()
        {
            try
            {
                return _service.Queryable().Where(x => x.TRANG_THAI.Equals("A") && (x.BC_THEO == "TAPMIS")).OrderBy(x => x.MA_BAO_CAO)
                    .ToList().Select(x => new ChoiceObj()
                    {
                        Value = x.MA_BAO_CAO,
                        Text = x.TEN_BAO_CAO,
                        ExtendValue = x.TEMPLATE.ToString(),
                        Id = x.ID
                    }).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("AddContent")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> AddContent(PHB_DM_BAOCAO model)
        {
            var response = new Response<PHB_DM_BAOCAO>();

            _service.Insert(model);

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

            return Ok(response);
        }

        [Route("UploadXml")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadXml()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();

            //get file
            if (httpRequest.Files == null || httpRequest.Files.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            var document = new XDocument();
            try
            {
                document = XDocument.Load(httpRequest.Files[0].InputStream);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }

            //get general informations
            string mauBC = "";
            PhienBan phienBan;
            int nam = 0;
            DateTime ngayTao;
            string nguoiTao = "";
            string maDonVi = "";
            string capDuToan = "";

            //validate mauBC and get general information
            try
            {
                mauBC = ReadXml_TTChung_MauBC(document);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Mẫu báo cáo không đúng. Yêu cầu mẫu BCTC_107_GT hoặc BCTC_107_TT";
                return Ok(response);
            }

            try
            {
                phienBan = ReadXml_TTChung_PhienBan(document);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Không có phiên bản hoặc phiên bản không được hỗ trợ.";
                return Ok(response);
            }

            try
            {
                nam = ReadXml_TTChung_Nam(document);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Không có năm báo cáo hoặc năm báo cáo lỗi";
                return Ok(response);
            }

            try
            {
                ngayTao = ReadXml_TTChung_NgayLapBC(document);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Không có ngày tạo báo cáo hoặc ngày tạo báo cáo lỗi";
                return Ok(response);
            }

            try
            {
                maDonVi = ReadXml_TTChung_MaDonVi(document);
            }
            catch (Exception)
            {
                response.Error = true;
                response.Message = "Không có mã đơn vị hoặc mã đơn vị lỗi";
                return Ok(response);
            }

            var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDonVi);
            if (dmDVSDNS == null)
            {
                response.Error = true;
                response.Message = "Mã đơn vị không hợp lệ: không tồn tại mã đơn vị trong hệ thống";
                return Ok(response);
            }

            capDuToan = dmDVSDNS.CAP_DU_TOAN.ToString();

            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            nguoiTao = identity.Name;

            try
            {
                await _xmlServiceVer001.ReadXml(document, nguoiTao, maDonVi, capDuToan, nam, ngayTao);
            }
            catch (ArgumentNullException)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;

                if (ex.Message.Contains("Đã tồn tại"))
                {
                    response.Message = ex.Message;
                }
                else
                {
                    response.Message = ErrorMessage.ERROR_SYSTEM;
                }

                return Ok(response);
            }

            response.Message = SuccessMessage.SUCCESS_UPDATE_DATA;
            return Ok(response);
        }

        [Route("ExportXml")]
        [HttpPost]
        public IHttpActionResult ExportXml(ExportXmlViewModel model)
        {
            var response = new Response<PHB_ExportXml_ViewModel>();

            //validate model
            if (model == null)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }

            if (model.Nam == 0)
            {
                response.Error = true;
                response.Message = "Không có năm báo cáo";
                return Ok(response);
            }

            if (model.TenBC == null || model.TenBC.Trim() == "")
            {
                response.Error = true;
                response.Message = "Không có tên báo cáo";
                return Ok(response);
            }

            string maDonVi = "";
            string tenDonVi = "";
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            if (identity != null && !string.IsNullOrEmpty(identity.Name))
            {
                maDonVi = identity.Claims.FirstOrDefault(x => x.Type.Equals("MA_DONVI"))?.Value.ToString();
            }

            if (!string.IsNullOrEmpty(maDonVi))
            {
                var dmDVSDNS = _serviceDMDVQHNS.Queryable().FirstOrDefault(x => x.MA_QHNS == maDonVi);
                if (dmDVSDNS != null)
                {
                    tenDonVi = dmDVSDNS.TEN_QHNS;
                }
            }

            var content = "";
            try
            {
                switch (model.PhienBan)
                {
                    case PhienBan.Ver_001:
                        content = _xmlServiceVer001.WriteXml(model, maDonVi, tenDonVi);
                        break;
                    default:
                        response.Error = true;
                        response.Message = "phiên bản không được hỗ trợ";
                        return Ok(response);
                }
            }
            catch (ArgumentNullException)
            {
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Error = true;
                response.Message = ErrorMessage.ERROR_SYSTEM;
                return Ok(response);
            }

            response.Data = new PHB_ExportXml_ViewModel();
            response.Data.Content = content;
            response.Data.FileName = WriteXml_FileName(model.Nam.ToString(), "BCTC_99", maDonVi, DateTime.Now);
            return Ok(response);
        }

        //Lấy các báo cáo tổng hợp đơn vị hcsn lấy dữ liêu từ tapmis

        private string WriteXml_FileName(string nam, string mauBC, string maDonVi, DateTime ngayLapBC)
        {
            var ngayLap = ngayLapBC.ToString("dd/MM/yyyy");
            return $"{nam}-{mauBC}-{maDonVi}-{ngayLap}";
        }

        private string ReadXml_TTChung_MauBC(XDocument document)
        {
            var mauBC = "";
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("mauBC").FirstOrDefault();
                mauBC = element.Value.Trim();
            }
            catch (Exception)
            {
                throw;
            }

            if (mauBC != "BCTC_107_GT" && mauBC != "BCTC_107_TT")
            {
                throw new Exception();
            }

            return mauBC;
        }

        private PhienBan ReadXml_TTChung_PhienBan(XDocument document)
        {
            PhienBan phienBan;
            string phienBanStr = "";
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("phienBan").FirstOrDefault();
                phienBanStr = element == null ? "" : element.Value.Trim();
            }
            catch (Exception)
            {
                throw;
            }

            if (phienBanStr == "")
            {
                throw new Exception();
            }

            switch (phienBanStr)
            {
                case "001":
                    phienBan = PhienBan.Ver_001;
                    break;
                default:
                    throw new Exception();
            }

            return phienBan;
        }

        private int ReadXml_TTChung_Nam(XDocument document)
        {
            var nam = 0;
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("kyBC").FirstOrDefault();
                nam = int.Parse(element.Value);
            }
            catch (Exception)
            {
                throw;
            }

            if (nam == 0)
            {
                throw new Exception();
            }

            return nam;
        }

        private string ReadXml_TTChung_MaDonVi(XDocument document)
        {
            var maDV = "";
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("maDonvi").FirstOrDefault();
                maDV = element.Value.Trim();
            }
            catch (Exception)
            {
                throw;
            }

            if (maDV == "")
            {
                throw new Exception();
            }

            return maDV;
        }

        private string ReadXml_TTChung_NguoiLapBC(XDocument document)
        {
            var nguoiLapBC = "";
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("nguoiLapBC").FirstOrDefault();
                nguoiLapBC = element.Value.Trim();
            }
            catch (Exception)
            {
                throw;
            }

            return nguoiLapBC;
        }

        private DateTime ReadXml_TTChung_NgayLapBC(XDocument document)
        {
            var ngayLapBC = new DateTime();
            try
            {
                var element = document.Descendants(GENERAL_ELEMENT_NAME).FirstOrDefault().Descendants("ngayLapBC").FirstOrDefault();

                if (DateTime.TryParse(element.Value.Trim(), out ngayLapBC))
                {
                    return ngayLapBC;
                }

                CultureInfo enUS = new CultureInfo("en-US");
                var lstFormat = new List<string>
                {
                    "dd/MM/yyyy",
                    "dd-MM-yyyy"
                };

                foreach (var format in lstFormat)
                {
                    if (DateTime.TryParseExact(element.Value.Trim(), format, enUS, DateTimeStyles.None, out ngayLapBC))
                    {
                        return ngayLapBC;
                    }
                }

                throw new Exception();

            }
            catch (Exception)
            {
                throw;
            }

            return ngayLapBC;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }


        #endregion

        public class LoaiHinhDv
        {
            public string MA_LH { get; set; }
            public string TEN_LH { get; set; }
        }

        [Route("UploadXmlTotal")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadXmlTotal()
        {
            var httpRequest = HttpContext.Current.Request;
            var response = new Response<string>();

            //get file
            if (httpRequest.Files == null || httpRequest.Files.Count == 0)
            {
                response.Error = true;
                response.Message = ErrorMessage.EMPTY_DATA;
                return Ok(response);
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(httpRequest.Files[0].InputStream);
                string jsonText = JsonConvert.SerializeXmlNode(doc.LastChild, Formatting.None, true);
                XmlViewModel.XmlTotalObj Jobject = JsonConvert.DeserializeObject<XmlViewModel.XmlTotalObj>(jsonText);

                var refid = Guid.NewGuid().ToString();
                foreach (var header in Jobject.ReportHeader)
                {
                    if (header.ReportID == "B04TT90")
                    {
                        var bc = new PHB_B04_TT90
                        {
                            NAM_BC = header.ReportYear,
                            MA_QHNS = header.CompanyID,
                            MA_CHUONG = header.BudgetChapterID.ToString(),
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = refid
                        };
                        var reportCount = _phbB04_TT90Service
                            .Queryable()
                            .Count(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _phbB04_TT90Service.Insert(bc);

                        foreach (var t in Jobject.B04TT90Detail)
                        {
                            var item = new PHB_B04_TT90_DETAIL
                            {
                                PHB_B04_TT90_REFID = refid,
                                TEN_CHI_TIEU = t.ReportItemName,
                                STT_CHI_TIEU = t.ReportItemAlias,
                                SAP_XEP = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_CHI_TIEU = t.ReportItemCode,
                                TONGSOLIEU_BCQT = t.EstimateAmount.GetValueOrDefault(0),
                                TONGSOLIEU_QT_DUOCDUYET = t.ApprovedAmount.GetValueOrDefault(0),
                            };
                            _phbB04_TT90DetailService.Insert(item);
                        }
                    }

                    if (header.ReportID == "B01BCQT")
                    {
                        var bc = new PHB_B01BCQT
                        {
                            NAM_BC = header.ReportYear,
                            MA_QHNS = header.CompanyID,
                            MA_CHUONG = header.BudgetChapterID.ToString(),
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };
                        var reportCount = _b01BcqtService
                            .Queryable()
                            .Count(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear);
                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }
                        _b01BcqtService.Insert(bc);
                        var details = Jobject.B01BCQTDetail;
                        foreach (var t in details)
                        {
                            var item = new PHB_B01BCQT_DETAIL
                            {
                                PHB_B01BCQT_REFID = bc.REFID,
                                TEN_CHI_TIEU = t.ReportItemName,
                                MA_CHI_TIEU = t.ReportItemCode,
                                STT_CHI_TIEU = t.ReportItemAlias,
                                SAP_XEP = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_SO = t.ReportItemCode,
                                GIA_TRI = t.Amount.GetValueOrDefault(0)
                            };
                            _b01BcqtDetailService.Insert(item);
                        }
                    }

                    if (header.ReportID == "B03bBCTC")
                    {
                        var bc = new PHA_B03B_BCTC
                        {
                            NAM = header.ReportYear,
                            MA_DONVI = header.CompanyID,
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };

                        //check đã có báo cáo chưa
                        var reportCount = _PhaB03BBCTCService
                            .Queryable()
                            .Count(report => report.MA_DONVI == header.CompanyID && report.NAM == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _PhaB03BBCTCService.Insert(bc);

                        var details = Jobject.B03bBCTCDetail;
                        foreach (var t in details)
                        {
                            var item = new PHA_B03B_BCTC_DETAIL
                            {
                                PHA_B03B_BCTC_REFID = bc.REFID,
                                CHI_TIEU = t.ReportItemName,
                                STT = t.ReportItemAlias,
                                STT_SAPXEP = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_SO = t.ReportItemCode,
                                SO_NAM_NAY = t.Amount,
                                SO_NAM_TRUOC = t.PrevAmount
                            };
                            _PhaB03BBCTCDetailService.Insert(item);
                        }
                    }

                    if (header.ReportID == "F0101BCQT")
                    {
                        var bc = new PHB_F01_01BCQT
                        {
                            NAM_BC = header.ReportYear,
                            MA_QHNS = header.CompanyID,
                            MA_CHUONG = header.BudgetChapterID.ToString(),
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };

                        //check đã có báo cáo chưa
                        var reportCount = _f0101BcqtService
                            .Queryable()
                            .Count(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _f0101BcqtService.Insert(bc);

                        var details = Jobject.F0101BCQTDetail;
                        foreach (var t in details)
                        {
                            var item = new PHB_F01_01BCQT_DETAIL
                            {
                                PHB_F01_01BCQT_REFID = bc.REFID,
                                NOI_DUNG_CHI = t.BudgetSubItemName,
                                MA_LOAI = t.BudgetKindItemID.ToString(),
                                MA_KHOAN = t.BudgetSubKindItemID.ToString(),
                                MA_MUC = t.BudgetItemID.ToString(),
                                MA_TIEU_MUC = t.BudgetSubItemID.ToString(),
                                NSNN_TRONGNUOC = t.BudgetSourceAmount.GetValueOrDefault(0),
                                VIEN_TRO = t.AidAmount.GetValueOrDefault(0),
                                VAYNO_NUOCNGOAI = t.DebitAmount.GetValueOrDefault(0),
                                NP_DELAI = t.DeductAmount.GetValueOrDefault(0),
                                NHD_DELAI = t.OtherAmount.GetValueOrDefault()

                            };
                            _f0101BcqtDetailService.Insert(item);
                        }
                    }

                    if (header.ReportID == "F0102BCQTP1")
                    {
                        var project = Jobject.Project.Where(x => x.IsDetail == true);
                        foreach (var p in project)
                        {
                            var bc = new PHB_F01_02BCQT
                            {
                                NAM_BC = header.ReportYear,
                                MA_QHNS = header.CompanyID,
                                MA_CHUONG = header.BudgetChapterID.ToString(),
                                NGAY_TAO = DateTime.Now,
                                TRANG_THAI = 0,
                                NGUOI_TAO = RequestContext.Principal.Identity.Name,
                                REFID = Guid.NewGuid().ToString()
                            };

                            //check đã có báo cáo chưa
                            var reportCount = _F01_02BCQTService
                                .Queryable()
                                .Count(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear);

                            if (reportCount > 0)
                            {
                                response.Error = true;
                                response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                                return Ok(response);
                            }

                            _F01_02BCQTService.Insert(bc);

                            var details = Jobject.F0102BCQTP1Detail.Where(x => x.ProjectID == p.ProjectID);
                            foreach (var t in details)
                            {
                                var itemNN = new PHB_F01_02BCQT_DETAIL
                                {
                                    PHB_F01_02BCQT_REFID = bc.REFID,
                                    TEN_CHI_TIEU = t.ReportItemName,
                                    MA_SO = t.ReportItemCode,
                                    GIA_TRI_LK = t.AccAmount.GetValueOrDefault(0),
                                    GIA_TRI_PS = t.Amount.GetValueOrDefault(0),
                                    MA_KHOAN = t.BudgetSubKindItemID,
                                    MA_LOAI = t.BudgetKindItemID
                                };
                                _F01_02BCQTDetailService.Insert(itemNN);

                                var itemLK = new PHB_F01_02BCQT_DETAIL
                                {
                                    PHB_F01_02BCQT_REFID = bc.REFID,
                                    TEN_CHI_TIEU = t.ReportItemName,
                                    MA_SO = t.ReportItemCode,
                                    GIA_TRI_LK = t.AccAmount.GetValueOrDefault(0),
                                    GIA_TRI_PS = t.Amount.GetValueOrDefault(0),
                                    MA_KHOAN = t.BudgetSubKindItemID,
                                    MA_LOAI = t.BudgetKindItemID
                                };
                                _F01_02BCQTDetailService.Insert(itemLK);
                            }
                        }
                    }

                    if (header.ReportID == "B02BCTC")
                    {
                        var bc = new PHA_B02_BCTC
                        {
                            NAM = header.ReportYear,
                            MA_DONVI = header.CompanyID,
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };

                        //check đã có báo cáo chưa
                        var reportCount = _B02BCTCService
                            .Queryable()
                            .Count(report => report.MA_DONVI == header.CompanyID && report.NAM == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _B02BCTCService.Insert(bc);

                        var details = Jobject.B02BCTCDetail;
                        foreach (var t in details)
                        {
                            var item = new PHA_B02_BCTC_DETAIL
                            {
                                PHA_B02_BCTC_REFID = bc.REFID,
                                CHI_TIEU = t.ReportItemName,
                                STT = t.ReportItemAlias,
                                STT_SAPXEP = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_SO = t.ReportItemCode,
                                SO_NAM_NAY = t.CurrentPeriodAmount,
                                SO_NAM_TRUOC = t.PreviousPeriodAmount
                            };
                            _B02BCTCDetailService.Insert(item);
                        }
                    }

                    if (header.ReportID == "B03BCQT")
                    {

                    }

                    if (header.ReportID == "B04BCTC")
                    {
                        var lstLH = new List<LoaiHinhDv>
                        {
                            new LoaiHinhDv() {MA_LH = "1", TEN_LH = "Đơn vị SNCL tự chủ chi thường xuyên và đầu tư"},
                            new LoaiHinhDv() {MA_LH = "2", TEN_LH = "Đơn vị SNCL tự chủ chi thường xuyên"},
                            new LoaiHinhDv() {MA_LH = "3", TEN_LH = "Đơn vị SNCL tự chủ một phần chi thường xuyên"},
                            new LoaiHinhDv() {MA_LH = "4", TEN_LH = "Đơn vị SNCL do NSNN cấp kinh phí"},
                            new LoaiHinhDv() {MA_LH = "5", TEN_LH = "Đơn vị hành chính được giao tự chủ kinh phí"},
                            new LoaiHinhDv() {MA_LH = "6", TEN_LH = "Đơn vị hành chính không được giao tự chủ kinh phí"}
                        };

                        var bc = new BC04_BCTC_TT107
                        {
                            NAM = header.ReportYear,
                            MA_DONVI = header.CompanyID,
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };

                        //check đã có báo cáo chưa
                        var reportCount = _bc04Bctctt107Service
                            .Queryable()
                            .Count(report => report.MA_DONVI == header.CompanyID && report.NAM == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        var details = Jobject.B04BCTCDetail;
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
                    }

                    if (header.ReportID == "B05BCTC")
                    {
                        PHB_B05_BCTC item = new PHB_B05_BCTC();
                        var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
                        item.MA_QHNS = header.CompanyID;
                        item.KY_BC = header.ReportPeriod.GetValueOrDefault(0);
                        item.TEN_QHNS = header.CompanyName;
                        item.TRANG_THAI = 0;
                        item.REFID = Guid.NewGuid().ToString();
                        item.NGAY_TAO = DateTime.Now;
                        item.NGUOI_TAO = identity.Name;
                        item.MA_CHUONG = header.BudgetChapterID.ToString();
                        item.NAM_BC = header.ReportYear;


                        var reportCount = _phbB05BCTCService.Queryable()
                            .Where(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear)
                            .Count();

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _phbB05BCTCService.Insert(item);

                        PHB_B05_BCTC_WORK item_work = new PHB_B05_BCTC_WORK();

                        item_work.PHB_B05_BCTC_REFID = item.REFID;
                        item_work.DON_VI = Jobject.B05BCTCDetailGeneral[0].Value;
                        item_work.QD_TL_SO = Jobject.B05BCTCDetailGeneral[1].Value;
                        //item_work.NGAY = Jobject.B05BCTCDetailGeneral[2].Value;
                        //item_work.THANG = model.dataWork.THANG;
                        //item_work.NAM = model.dataWork.NAM;
                        item_work.CO_QUAN_CAP = Jobject.B05BCTCDetailGeneral[3].Value;
                        item_work.THUOC_DV = Jobject.B05BCTCDetailGeneral[4].Value;
                        item_work.LOAI_HINH_DV = Jobject.B05BCTCDetailGeneral[5].Value;
                        item_work.CHU_TC = Jobject.B05BCTCDetailGeneral[6].Value;
                        item_work.NV_CHINH_DV = Jobject.B05BCTCDetailGeneral[7].Value;
                        item_work.TT_THUYETMINH = Jobject.B05BCTCDetailGeneral[9].Value;

                        _phbB05BCTCWorkService.Insert(item_work);

                        foreach (var t in Jobject.B05BCTCDetailBudget)
                        {
                            var ct = new PHB_B05_BCTC_DETAIL
                            {
                                PHB_B05_BCTC_REFID = item.REFID,
                                STT = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_TABLE = t.Type.ToString(),
                                MA_CHI_TIEU = t.ReportItemAlias,
                                TEN_CHI_TIEU = t.ReportItemName,
                                COT_1 = (double) t.Amount1,
                                COT_2 = (double) t.Amount2
                            };
                            _phbB05BCTCDetailService.Insert(ct);
                        }
                    }

                    if (header.ReportID == "B01BSTT")
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


                        model.NAM = header.ReportYear;
                        model.REFID = Guid.NewGuid().ToString();

                        //check đã có báo cáo chưa
                        var reportCount = _B01BSTT1Service.Queryable()
                            .Where(report => report.MA_DONVI == model.MA_DONVI && report.NAM == model.NAM)
                            .Count();

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _B01BSTT1Service.Insert(model);

                        foreach (var t in Jobject.B01BSTTDetail)
                        {
                            var obj = new PHA_B01_BSTT_1_DETAIL();
                            obj.PHA_B01_BSTT_1_REFID = model.REFID;
                            obj.STT_SAPXEP = t.ReportItemIndex.GetValueOrDefault(0);
                            obj.STT = t.ReportItemAlias;
                            obj.CHI_TIEU = t.ReportItemName;
                            obj.MA_SO = t.ReportItemCode;
                            obj.TONG_SO = t.Amount;
                            obj.TRONG_DVKTTG_1 = t.EstimateUnit1Amount;
                            obj.TRONG_DVKTTG_2 = t.EstimateUnitOut1Amount;
                            obj.TRONG_DVDT_CAP1 = t.MediateUnit1Amount;
                            obj.NGOAI_DVDT_CAP1_CUNGTINH = t.MediateUnit2Amount;
                            obj.NGOAI_DVDT_CAP1_KHACTINH = t.NationalOutAmount;
                            obj.NGOAI_NHA_NUOC = t.OherEstimateUnitOut1Amount;
                            _B01BSTT1DetailService.Insert(obj);
                        }
                    }

                    if (header.ReportID == "B03TT90")
                    {
                        var bc = new PHB_B03_TT90
                        {
                            NAM_BC = header.ReportYear,
                            MA_QHNS = header.CompanyID,
                            MA_CHUONG = header.BudgetChapterID.ToString(),
                            NGAY_TAO = DateTime.Now,
                            TRANG_THAI = 0,
                            NGUOI_TAO = RequestContext.Principal.Identity.Name,
                            REFID = Guid.NewGuid().ToString()
                        };

                        //check đã có báo cáo chưa
                        var reportCount = _phbB03_TT90Service
                            .Queryable()
                            .Count(report => report.MA_QHNS == header.CompanyID && report.NAM_BC == header.ReportYear);

                        if (reportCount > 0)
                        {
                            response.Error = true;
                            response.Message = header.ReportID + "--" + ErrorMessage.EXITS_REPORT;
                            return Ok(response);
                        }

                        _phbB03_TT90Service.Insert(bc);

                        var details = Jobject.B03TT90Detail;
                        foreach (var t in details)
                        {
                            var item = new PHB_B03_TT90_DETAIL
                            {
                                PHB_B03_TT90_REFID = bc.REFID,
                                TEN_CHI_TIEU = t.ReportItemName,
                                STT_CHI_TIEU = t.ReportItemAlias,
                                SAP_XEP = t.ReportItemIndex.GetValueOrDefault(0),
                                MA_CHI_TIEU = t.ReportItemCode,
                                DU_TOAN_NAM = t.EstimateAmount.GetValueOrDefault(0),
                                UOC_THUC_HIEN = t.ApprovedAmount.GetValueOrDefault(0),
                                UOC_THUC_HIEN_DU_TOAN = t.OtherAmount.GetValueOrDefault(0)
                            };
                            _phbB03_TT90DetailService.Insert(item);
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
                response.Error = true;
                response.Message = ErrorMessage.ERROR_DATA;
                return Ok(response);
            }
            return Ok(response);
        }
    }
}