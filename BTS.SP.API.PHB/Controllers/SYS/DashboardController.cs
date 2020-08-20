using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using BTS.SP.PHB.SERVICE.PBDT.TT344;
using BTS.SP.PHB.SERVICE.REPORT.C_B01X;
using BTS.SP.PHB.SERVICE.REPORT.C_B04X;
using BTS.SP.PHB.SERVICE.REPORT.C_B06X;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/Dashboard")]
    [Route("{id?}")]
    public class DashboardController :ApiController
    {
        private readonly IDmDBHCService _dbhcService;
        private readonly IPhbC_B01XService _b01Service;
        private readonly IPhbBieuC_B04XService _b04Service;
        private readonly IPhbC_B06XService _b06Service;
        private readonly IPHB_PBDT_TT344_B04_Service _b04TT344Service;
        private readonly IPHB_PBDT_TT344_B05_Service _b05TT344Service;

        public DashboardController(IDmDBHCService dbhcService,IPhbC_B01XService b01Service,IPhbBieuC_B04XService b04Service,IPhbC_B06XService b06Service, IPHB_PBDT_TT344_B04_Service b04TT344Service, IPHB_PBDT_TT344_B05_Service b05TT344Service)
        {
            _dbhcService = dbhcService;
            _b01Service = b01Service;
            _b04Service = b04Service;
            _b06Service = b06Service;
            _b04TT344Service = b04TT344Service;
            _b05TT344Service = b05TT344Service;
        }

        public class DashboardItem
        {
            public string MA_DBHC { get; set; }
            public string TEN_DBHC { get; set; }
            public DateTime? NGAY_BC { get; set; }
            public bool B01X { get; set; }
            public bool B04X { get; set; }
            public bool B06X { get; set; }
            public bool B04TT344 { get; set; }
            public bool B05TT344 { get; set; }
        }

        public class DashboardParam {
            public string MA_HUYEN { get; set; }
            public int NAM { get; set; }
        }

        [Route("Select_Page")]
        [HttpPost]
        public async Task<IHttpActionResult> Select_Page(DashboardParam param)
        {
            var result = new Response<List<DashboardItem>>();
            var data = new List<DashboardItem>();
            try
            {
                var lstDbhc = _dbhcService.Queryable().Where(x=>x.MA_DBHC_CHA == param.MA_HUYEN);
                var lstDbhcB01 = _b01Service.Queryable().Where(x=>x.NAM_BC == param.NAM);
                var lstDbhcB04 = _b04Service.Queryable().Where(x => x.NAM_BC == param.NAM);
                var lstDbhcB06 = _b06Service.Queryable().Where(x => x.NAM_BC == param.NAM);
                var lstDbhcB04TT344 = _b04TT344Service.Queryable().Where(x => x.NAM == param.NAM);
                var lstDbhcB05TT344 = _b05TT344Service.Queryable().Where(x => x.NAM == param.NAM);
                foreach (var db in lstDbhc)
                {
                    var item = new DashboardItem();
                    item.MA_DBHC = db.MA_DBHC;
                    item.TEN_DBHC = db.TEN_DBHC;
                    item.B01X = false;
                    item.B04X = false;
                    item.B06X = false;
                    item.B04TT344 = false;
                    item.B05TT344 = false;
                    var bc01Exist = lstDbhcB01.Where(x=>x.MA_DBHC == db.MA_DBHC);
                    var bc04Exist = lstDbhcB04.Where(x => x.MA_DBHC == db.MA_DBHC);
                    var bc06Exist = lstDbhcB06.Where(x => x.MA_DBHC == db.MA_DBHC);
                    var bc04TT344Exist = lstDbhcB04TT344.Where(x => x.MA_DBHC == db.MA_DBHC);
                    var bc05TT344Exist = lstDbhcB05TT344.Where(x => x.MA_DBHC == db.MA_DBHC);
                    if (bc01Exist.Count() > 0)
                    {
                        item.B01X = true;
                    }
                    if (bc04Exist.Count() > 0)
                    {
                        item.B04X = true;
                    }
                    if (bc06Exist.Count() > 0)
                    {
                        item.B06X = true;
                    }
                    if (bc04TT344Exist.Count() > 0)
                    {
                        item.B04TT344 = true;
                    }
                    if (bc05TT344Exist.Count() > 0)
                    {
                        item.B05TT344 = true;
                    }
                    data.Add(item);
                }
                result.Data = data;
                result.Error = false;
                return Ok(result);
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
                return InternalServerError();
            }
        }
    }
}