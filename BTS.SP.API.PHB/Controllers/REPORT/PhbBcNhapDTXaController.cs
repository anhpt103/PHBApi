using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.UTILS;
using Newtonsoft.Json.Linq;
using Repository.Pattern.UnitOfWork;
using System.Data;
using BTS.SP.PHB.SERVICE.REPORT.NHAP_DT_XA;
using BTS.SP.PHB.ENTITY.Rp.NHAP_DT_XA;
using BTS.SP.PHB.SERVICE.HTDM.DmChiTieuBaoCao;
using BTS.SP.PHB.ENTITY;
using Repository.Pattern.Infrastructure;
using BTS.SP.PHB.ENTITY.Dm;
using System.Web;
using System.Security.Claims;
using System.Data.Entity;
using BTS.SP.PHB.SERVICE.Helper;
using BTS.SP.PHB.SERVICE.BuildQuery.Result;
using BTS.SP.PHB.SERVICE.BuildQuery.Implimentations;
using BTS.SP.PHB.SERVICE.BuildQuery;
using BTS.SP.PHB.SERVICE.BuildQuery.Types;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/PhbBcNhapDTXa")]
    [Route("{id?}")]
    public class PhbBcNhapDTXaController : ApiController
    {
        private readonly IBC_NHAPDTXA_DETAILService _detailservice;
        private readonly IDM_CT_BAOCAOService _BAOCAOservice;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public PhbBcNhapDTXaController(
            IBC_NHAPDTXA_DETAILService detailservice,
            IDM_CT_BAOCAOService BAOCAOservice,
            IUnitOfWorkAsync unitOfWorkAsync)
        {
            _detailservice = detailservice;
            _BAOCAOservice = BAOCAOservice;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("GetDuToan")]
        [HttpPost]
        public List<InsertDetails> GetDuToan(InsertObj param)
        {
            try
            {
                var lstChiTieu = _unitOfWorkAsync.Repository<DM_PHC_CHITIEUTHU_CHI>().Queryable().Where(x => x.PHAN_HE == "B" && x.TRANG_THAI == "A" && x.LOAICHITIEU == param.LOAI_DT).ToList();
                var lstSoLieu = _detailservice.Queryable().Where(x=>x.MA_DBHC == param.MA_DBHC && x.NAM == param.NAM_BC && x.LOAI_CHITIEU == param.LOAI_DT).ToList();
                List<InsertDetails> result = new List<InsertDetails>();
                for (int i = 0; i < lstChiTieu.Count; i++)
                {
                    InsertDetails item = new InsertDetails();
                    item.MACHITIEU = lstChiTieu[i].MACHITIEU;
                    item.TENCHITIEU = lstChiTieu[i].TENCHITIEU;
                    item.MACHA = lstChiTieu[i].MACHA;
                    item.SAPXEP = lstChiTieu[i].SAPXEP;
                    var nsnn = lstSoLieu.FirstOrDefault(x=>x.MA_COT == "NSNN" && x.MA_CHITIEU == lstChiTieu[i].MACHITIEU);
                    if (nsnn != null)
                    {
                        item.NSNN = nsnn.GIA_TRI;
                    }
                    var nsx = lstSoLieu.FirstOrDefault(x => x.MA_COT == "NSX" && x.MA_CHITIEU == lstChiTieu[i].MACHITIEU);
                    if (nsx != null)
                    {
                        item.NSX = nsx.GIA_TRI.GetValueOrDefault();
                    }
                    var dtpt = lstSoLieu.FirstOrDefault(x => x.MA_COT == "DTPT" && x.MA_CHITIEU == lstChiTieu[i].MACHITIEU);
                    if (dtpt != null)
                    {
                        item.DTPT = dtpt.GIA_TRI.GetValueOrDefault();
                    }
                    var ctx = lstSoLieu.FirstOrDefault(x => x.MA_COT == "CTX" && x.MA_CHITIEU == lstChiTieu[i].MACHITIEU);
                    if (ctx != null)
                    {
                        item.CTX = ctx.GIA_TRI.GetValueOrDefault();
                    }
                    result.Add(item);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class updateObj
        {
            public string id { get; set; }
            public List<InsertDetails> details { get; set; }
        }

        public class InsertDetails
        {
            public string MA_DBHC { get; set; }
            public string MA_PK { get; set; }
            public string MACHITIEU { get; set; }
            public string MACHA { get; set; }
            public string TENCHITIEU { get; set; }
            public string SAPXEP { get; set; }
            public decimal? NSNN { get; set; }
            public decimal? NSX { get; set; }
            public decimal? DTPT { get; set; }
            public decimal? CTX { get; set; }
        }

        public class InsertObj
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }
            //public string MA_PK { get; set; }
            public string MA_DBHC_CHA { get; set; }
            public int NAM_BC { get; set; }
            public int LOAI_DT { get; set; }
            public DateTime? NGAY_TAO { get; set; }
            public string NGUOI_TAO { get; set; }
            public DateTime? NGAY_SUA { get; set; }
            public string NGUOI_SUA { get; set; }
            public List<InsertDetails> DataDetails { get; set; }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IHttpActionResult> Post(InsertObj model)
        {
            var response = new Response<bool>();
            try
            {
                var currentUser = (HttpContext.Current.User as ClaimsPrincipal);
                    if (model.DataDetails != null)
                    {
                    var lstSoLieu = _detailservice.Queryable().Where(x => x.MA_DBHC == model.MA_DBHC && x.NAM == model.NAM_BC && x.LOAI_CHITIEU == model.LOAI_DT).ToList();
                    foreach (var bm in model.DataDetails)
                        {
                            if (bm.NSNN > 0)
                            {
                                var item1 = new BC_NHAPDT_XA_DETAIL();
                                item1.MA_DBHC = model.MA_DBHC;
                            item1.MA_DBHC_XA = model.MA_DBHC;
                            item1.MA_DBHC_USER = model.MA_DBHC;
                            item1.MA_CHITIEU = bm.MACHITIEU;
                                item1.GIA_TRI = bm.NSNN;
                                item1.MA_COT = "NSNN";
                                item1.LOAI_CHITIEU = model.LOAI_DT;
                                item1.NAM = model.NAM_BC;
                                item1.TRANG_THAI = "B";
                            item1.CAP = 4;
                                item1.ObjectState = ObjectState.Added;
                                var exist = lstSoLieu.FirstOrDefault(x=>x.MA_CHITIEU == bm.MACHITIEU && x.MA_COT == "NSNN");
                                if (exist != null)
                                {
                                    exist.GIA_TRI = bm.NSNN;
                                    _detailservice.Update(exist);
                                }
                                else
                                {
                                    _detailservice.Insert(item1);
                                }
                            }

                            if (bm.NSX > 0)
                            {
                                var item2 = new BC_NHAPDT_XA_DETAIL();
                                item2.MA_DBHC = model.MA_DBHC;
                            item2.MA_DBHC_XA = model.MA_DBHC;
                            item2.MA_DBHC_USER = model.MA_DBHC;
                            item2.MA_CHITIEU = bm.MACHITIEU;
                                item2.GIA_TRI = bm.NSX;
                                item2.MA_COT = "NSX";
                                item2.LOAI_CHITIEU = model.LOAI_DT;
                                item2.NAM = model.NAM_BC;
                                item2.TRANG_THAI = "B";
                            item2.CAP = 4;
                            item2.ObjectState = ObjectState.Added;
                                var exist = lstSoLieu.FirstOrDefault(x => x.MA_CHITIEU == bm.MACHITIEU && x.MA_COT == "NSX");
                                if (exist != null)
                                {
                                    exist.GIA_TRI = bm.NSX;
                                    _detailservice.Update(exist);
                                }
                                else
                                {
                                    _detailservice.Insert(item2);
                                }
                            }

                            if (bm.CTX > 0)
                            {
                                var item3 = new BC_NHAPDT_XA_DETAIL();
                                item3.MA_DBHC = model.MA_DBHC;
                            item3.MA_DBHC_XA = model.MA_DBHC;
                            item3.MA_DBHC_USER = model.MA_DBHC;
                            item3.MA_CHITIEU = bm.MACHITIEU;
                                item3.GIA_TRI = bm.CTX;
                                item3.MA_COT = "CTX";
                                item3.LOAI_CHITIEU = model.LOAI_DT;
                                item3.NAM = model.NAM_BC;
                                item3.TRANG_THAI = "B";
                            item3.CAP = 4;
                                item3.ObjectState = ObjectState.Added;
                                var exist = lstSoLieu.FirstOrDefault(x => x.MA_CHITIEU == bm.MACHITIEU && x.MA_COT == "CTX");
                                if (exist != null)
                                {
                                    exist.GIA_TRI = bm.CTX;
                                    _detailservice.Update(exist);
                                }
                                else
                                {
                                    _detailservice.Insert(item3);
                                }
                            }

                            if (bm.DTPT > 0)
                            {
                                var item4 = new BC_NHAPDT_XA_DETAIL();
                                item4.MA_DBHC = model.MA_DBHC;
                            item4.MA_DBHC_XA = model.MA_DBHC;
                            item4.MA_DBHC_USER = model.MA_DBHC;
                            item4.MA_CHITIEU = bm.MACHITIEU;
                                item4.GIA_TRI = bm.DTPT;
                                item4.MA_COT = "DTPT";
                                item4.LOAI_CHITIEU = model.LOAI_DT;
                                item4.NAM = model.NAM_BC;
                                item4.TRANG_THAI = "B";
                            item4.CAP = 4;
                                item4.ObjectState = ObjectState.Added;
                                var exist = lstSoLieu.FirstOrDefault(x => x.MA_CHITIEU == bm.MACHITIEU && x.MA_COT == "DTPT");
                                if (exist != null)
                                {
                                    exist.GIA_TRI = bm.DTPT;
                                    _detailservice.Update(exist);
                                }
                                else
                                {
                                    _detailservice.Insert(item4);
                                }
                            }
                        }
                    }
                _unitOfWorkAsync.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                response.Message = "Xảy ra lỗi";
                response.Error = true;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

    }
}