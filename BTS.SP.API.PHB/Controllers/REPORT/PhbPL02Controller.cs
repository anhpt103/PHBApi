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
using BTS.SP.PHB.SERVICE.REPORT.Bieu4A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu2A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu2B;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1;
using BTS.SP.PHB.SERVICE.REPORT.Bieu69NS;
using BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPL02")]
    [Route("{id?}")]
    public class PhbPL02Controller : ApiController
    {
        private readonly IPhbBieu4AService _bieu4AService;
        private readonly IPhbBieu2AService _bieu2AService;
        private readonly IPhbBieu2BService _bieu2BService;
        private readonly IPhbBieu4BP1Service _bieu4BService;
        private readonly IPhbBIEU2CP1Service _bieu2CService;
        private readonly IPhbBieu69NsService _bieu69NsService;

        public PhbPL02Controller(IPhbBieu4AService _bieu4AService, IPhbBieu2AService _bieu2AService,
            IPhbBieu2BService _bieu2BService, IPhbBieu4BP1Service _bieu4BService,
            IPhbBIEU2CP1Service _bieu2CService, IPhbBieu69NsService _bieu69NsService)
        {
            this._bieu4AService = _bieu4AService;
            this._bieu2AService = _bieu2AService;
            this._bieu2BService = _bieu2BService;
            this._bieu4BService = _bieu4BService;
            this._bieu2CService = _bieu2CService;
            this._bieu69NsService = _bieu69NsService;
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            //var claimMaDbhc = ((ClaimsIdentity)RequestContext.Principal.Identity).Claims.FirstOrDefault(x => x.Type.Equals("MA_DBHC"));
            //var maDbhc = "-1";
            //if (!string.IsNullOrEmpty(claimMaDbhc?.Value))
            //{
            //    maDbhc = claimMaDbhc.Value;
            //}
            var response = new Response<DtoPL02>();
            DtoPL02 dataResponse = new DtoPL02();
            // Biểu 2B - Mục 2:
            if (rqmodel.NAM_BC <= 2017)
            {
                 dataResponse.DONG21 = 0;
                 dataResponse.DONG22 = 0;
                 dataResponse.DONG23 = 0;
                 dataResponse.DONG24 = 0;

            }
            else
            {
                var objBieu2B = await _bieu2BService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu2B = objBieu2B.Data;
                if (dataBieu2B != null)
                    foreach (var item in dataBieu2B.DETAIL)
                    {
                        dataResponse.DONG21 += item.STT_CHI_TIEU == "21" ? item.SO_TIEN : 0;
                        dataResponse.DONG22 += item.STT_CHI_TIEU == "22" ? item.SO_TIEN : 0;
                        dataResponse.DONG23 += item.STT_CHI_TIEU == "23" ? item.SO_TIEN : 0;
                        dataResponse.DONG24 += item.STT_CHI_TIEU == "24" ? item.SO_TIEN : 0;

                    }
            }
            // Biểu 69 - Mục 5:
            var objBieu69Ns = await _bieu69NsService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()),rqmodel.CHI_TIET);
            var dataBieu69Ns = objBieu69Ns.Data;
            if (dataBieu69Ns != null)
            foreach (var item in dataBieu69Ns.DETAIL)
            {
                    dataResponse.DONG52 += (item.PHAN.StartsWith("1") || item.PHAN.StartsWith("2")) ? item.SXL_KIEMTOAN : 0;
                    dataResponse.DONG53 += (item.PHAN.StartsWith("1") || item.PHAN.StartsWith("2")) ? item.SCXL_KIEMTOAN : 0;
            }
            dataResponse.DONG51 = dataResponse.DONG52 + dataResponse.DONG53;
            if (rqmodel.NAM_BC <=2017) // lay Bieu 3A va 3B
            {
                // Biểu 4A - Mục 1 :
                var objBieu4A = await _bieu4AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET,rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu4A = objBieu4A.Data;
                if (dataBieu4A != null)
                    foreach (var item in dataBieu4A.DETAIL)
                {
                    dataResponse.DONG11 += item.MA_CHI_TIEU == "02" ? item.SO_THUCHIEN : 0;
                    dataResponse.DONG12 += item.MA_CHI_TIEU == "05" ? item.SO_THUCHIEN : 0;
                }
                dataResponse.DONG13 = dataResponse.DONG11 - dataResponse.DONG12;
                // Biểu 4B - Mục 4 :
                //var objBieu4B = await _bieu4BService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                //var dataBieu4B = objBieu4B.Data;
                //foreach (var item in dataBieu4B.DETAIL)
                //{
                //    dataResponse.DONG41 += item.STT_CHI_TIEU == "1" ? item.SO_XDTD : 0;
                //    dataResponse.DONG42 += item.MA_CHI_TIEU == "9" ? item.SO_XDTD : 0;
                //    dataResponse.DONG43 += item.MA_CHI_TIEU == "21" ? item.SO_XDTD : 0;
                //    dataResponse.DONG44 += item.MA_CHI_TIEU == "27" ? item.SO_XDTD : 0;
                //    dataResponse.DONG45 += item.MA_CHI_TIEU == "33" ? item.SO_XDTD : 0;
                //    dataResponse.DONG46 += item.MA_CHI_TIEU == "49" ? item.SO_XDTD : 0;
                //    dataResponse.DONG461 += item.MA_CHI_TIEU == "52" ? item.SO_XDTD : 0;
                //    dataResponse.DONG462 += item.MA_CHI_TIEU == "53" ? item.SO_XDTD : 0;

                //}
            }
            else // lay bieu 2A vaf 2C
            {
               // Biểu 2A - Mục 1 :
               var objBieu2A = await _bieu2AService.SumReport(RequestContext.Principal.Identity.Name, rqmodel.LOAI_BC, rqmodel.CHI_TIET, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                var dataBieu2A = objBieu2A.Data;
                foreach (var item in dataBieu2A.DETAIL)
                {
                    dataResponse.DONG11 += item.MA_CHI_TIEU == 1 ? item.THUC_HIEN : 0;
                    dataResponse.DONG12 += item.MA_CHI_TIEU == 2 ? item.THUC_HIEN : 0;
                    dataResponse.DONG13 += item.MA_CHI_TIEU == 3 ? item.THUC_HIEN : 0;
                }
                // Biểu 2C - Mục 4 :
                //var objBieu01C = await _bieu01CService.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC, string.Join(",", rqmodel.DSDVQHNS.ToArray()));
                //var dataBieu01C = objBieu01C.Data;
            }
            response.Data = dataResponse;
            return Ok(response);
        }

        public class DtoPL02
        {
            public double DONG11 { get; set; }
            public double DONG12 { get; set; }
            public double DONG13 { get; set; }
            public double DONG20 { get; set; }
            public double DONG21 { get; set; }
            public double DONG22 { get; set; }
            public double DONG23 { get; set; }
            public double DONG24 { get; set; }
            public double DONG31 { get; set; }
            public double DONG32 { get; set; }
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