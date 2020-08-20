using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.SERVICE.Models.BIEU01A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01A;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using System.Security.Claims;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01B;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01C;
using BTS.SP.PHB.SERVICE.REPORT.Bieu69NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPL01")]
    [Route("{id?}")]
    public class PhbPL01Controller : ApiController
    {
        private readonly IPhbBieu01AService _bieu01AService;
        private readonly IPhbBieu3AService _bieu3AService;
        private readonly IPhbBieu01BService _bieu01BService;
        private readonly IPhbBieu3BP1Service _bieu3BService;
        private readonly IPhbBIEU01CService _bieu01CService;
        private readonly IPhbBieu69NsService _bieu69NsService;

        public PhbPL01Controller(IPhbBieu01AService _bieu01AService, IPhbBieu3AService _bieu3AService,
            IPhbBieu01BService _bieu01BService, IPhbBieu3BP1Service _bieu3BService,
            IPhbBIEU01CService _bieu01CService, IPhbBieu69NsService _bieu69NsService)
        {
            this._bieu01AService = _bieu01AService;
            this._bieu3AService = _bieu3AService;
            this._bieu01BService = _bieu01BService;
            this._bieu3BService = _bieu3BService;
            this._bieu01CService = _bieu01CService;
            this._bieu69NsService = _bieu69NsService;
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            //var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            //if (string.IsNullOrEmpty(claimMaDbhc?.Value)) return InternalServerError();
            var response = new Response<DtoPL01>();
            var dataResponse = new DtoPL01();
            //// Biểu 01B - Mục 2:
            //var objBieu01B = await _bieu01BService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            //var dataBieu01B = objBieu01B.Data;
            //if(dataBieu01B != null)
            //foreach(var item in dataBieu01B.DETAIL)
            //{
            //    dataResponse.DONG21 += item.STT_CHI_TIEU == "21" ? item.SO_DCKT : 0;
            //    dataResponse.DONG22 += item.STT_CHI_TIEU == "22" ? item.SO_DCKT : 0;
            //    dataResponse.DONG23 += item.STT_CHI_TIEU == "23" ? item.SO_DCKT : 0;
            //    dataResponse.DONG24 += item.STT_CHI_TIEU == "24" ? item.SO_DCKT : 0;

            //}
            // Biểu 69 - Mục 3:
            var objBieu69Ns = await _bieu69NsService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()),rqmodel.CHI_TIET);
            var dataBieu69Ns= objBieu69Ns.Data;
            if (dataBieu69Ns != null)
            foreach (var item in dataBieu69Ns.DETAIL)
            {
                dataResponse.DONG31 +=( item.STT_CHI_TIEU.Equals("I") || item.STT_CHI_TIEU.Equals("II") )? item.SKN_KIEMTOAN : 0;
                dataResponse.DONG32 += (item.STT_CHI_TIEU.Equals("I") || item.STT_CHI_TIEU.Equals("II")) ? item.SXL_KIEMTOAN : 0;
                dataResponse.DONG33 += (item.STT_CHI_TIEU.Equals("I") || item.STT_CHI_TIEU.Equals("II")) ? item.SCXL_KIEMTOAN : 0;

            }
            if (rqmodel.NAM_BC <=2017) // lay Bieu 3A va 3B
            {
                // Biểu 3A - Mục 1 :
                var objBieu3A = await _bieu3AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu3A = objBieu3A.Data;
                if (dataBieu3A != null)
                foreach (var item in dataBieu3A.DETAIL)
                {
                    dataResponse.DONG11 += item.MA_CHI_TIEU == "02" ? item.TH_SOBC : 0;
                    dataResponse.DONG12 += item.MA_CHI_TIEU == "05" ? item.TH_SOBC : 0;
                }
                dataResponse.DONG13 = dataResponse.DONG11 - dataResponse.DONG12;
                // Biểu 3B - Mục 4 :
                var objBieu3B = await _bieu3BService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu3B = objBieu3B.Data;
                if (dataBieu3B != null)
                foreach (var item in dataBieu3B.DETAIL)
                {
                    dataResponse.DONG41 += item.STT_CHI_TIEU == "1" ? item.SO_XDTD : 0;
                    dataResponse.DONG42 += item.STT_CHI_TIEU == "9" ? item.SO_XDTD : 0;
                    dataResponse.DONG43 += item.STT_CHI_TIEU == "21" ? item.SO_XDTD : 0;
                    dataResponse.DONG44 += item.STT_CHI_TIEU == "27" ? item.SO_XDTD : 0;
                    dataResponse.DONG45 += item.STT_CHI_TIEU == "33" ? item.SO_XDTD : 0;
                    dataResponse.DONG46 += item.STT_CHI_TIEU == "49" ? item.SO_XDTD : 0;
                    dataResponse.DONG461 += item.STT_CHI_TIEU == "52" ? item.SO_XDTD : 0;
                    dataResponse.DONG462 += item.STT_CHI_TIEU == "53" ? item.SO_XDTD : 0;

                }
            }
            else // lay bieu 01A vaf 01C
            {
                // Biểu 01A - Mục 2a :
                var objBieu01A = await _bieu01AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC,rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu01A = objBieu01A.Data;
                if (dataBieu01A != null)
                    foreach (var item in dataBieu01A.DETAIL)
                {
                    dataResponse.DONG11 += item.MA_CHI_TIEU == 1 ? item.TH_SOXDTD : 0;
                    dataResponse.DONG12 += item.MA_CHI_TIEU == 2 ? item.TH_SOXDTD : 0;
                    dataResponse.DONG13 += item.MA_CHI_TIEU == 3 ? item.TH_SOXDTD : 0;
                }
                // Biểu 01C - Mục 2b :
                var objBieu01C = await _bieu01CService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu01C = objBieu01C.Data;
                if (dataBieu01C != null)
                    foreach (var item in dataBieu01C.DETAILS)
                    {
                        dataResponse.DONG21 += item.MA_SO == "01" ? Double.Parse(item.GIA_TRI_DUYET+"") : 0;
                        dataResponse.DONG21 += item.MA_SO == "36" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG21 += item.MA_SO == "44" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG21 += item.MA_SO == "61" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG21 += item.MA_SO == "79" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG22 += item.MA_SO == "08" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG22 += item.MA_SO == "37" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG22 += item.MA_SO == "47" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG22 += item.MA_SO == "64" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG22 += item.MA_SO == "82" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG25 += item.MA_SO == "14" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG26 += item.MA_SO == "17" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG26 += item.MA_SO == "42" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG26 += item.MA_SO == "52" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG26 += item.MA_SO == "73" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG26 += item.MA_SO == "91" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG27 += item.MA_SO == "20" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG27 += item.MA_SO == "53" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG28 += item.MA_SO == "29" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG28 += item.MA_SO == "43" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG28 += item.MA_SO == "57" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG28 += item.MA_SO == "76" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG28 += item.MA_SO == "94" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG29 += item.MA_SO == "31" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG29 += item.MA_SO == "34" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;

                        dataResponse.DONG210 += item.MA_SO == "35" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;
                        dataResponse.DONG210 += item.MA_SO == "59" ? Double.Parse(item.GIA_TRI_DUYET + "") : 0;



                    }

            }
            response.Data = dataResponse;
            return Ok(response);
        }

        public class DtoPL01
        {
            public double DONG11 { get; set; }
            public double DONG12 { get; set; }
            public double DONG13 { get; set; }
            public double DONG20 { get; set; }
            public double DONG21 { get; set; }
            public double DONG22 { get; set; }
            public double DONG23 { get; set; }
            public double DONG24 { get; set; }
            public double DONG25 { get; set; }
            public double DONG26 { get; set; }
            public double DONG27 { get; set; }
            public double DONG28 { get; set; }
            public double DONG29 { get; set; }
            public double DONG210 { get; set; }

            public double DONG31 { get; set; }
            public double DONG32 { get; set; }
            public double DONG33 { get; set; }
            public double DONG41 { get; set; }
            public double DONG42 { get; set; }
            public double DONG421 { get; set; }
            public double DONG422 { get; set; }
            public double DONG43 { get; set; }
            public double DONG44 { get; set; }
            public double DONG45 { get; set; }
            public double DONG46 { get; set; }
            public double DONG461 { get; set; }
            public double DONG462 { get; set; }
            public double DONG51 { get; set; }
            public double DONG52 { get; set; }
            public double DONG53 { get; set; }
        }

    }

}