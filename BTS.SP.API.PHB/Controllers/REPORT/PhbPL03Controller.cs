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
using BTS.SP.PHB.SERVICE.REPORT.Bieu3A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01B;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01C;
using BTS.SP.PHB.SERVICE.REPORT.Bieu69NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1;
using BTS.SP.PHB.SERVICE.REPORT.PL03;

namespace BTS.SP.API.PHB.Controllers.REPORT
{
    [RoutePrefix("api/report/phbPL03")]
    [Route("{id?}")]
    public class PhbPL03Controller : ApiController
    {
        private readonly IPL03Service _pl03Service;
        private readonly IPL03DetailService _pl03DetailService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        public PhbPL03Controller(IPL03Service pl03Service, IPL03DetailService pl03DetailService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _pl03Service = pl03Service;
            _pl03DetailService = pl03DetailService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [Route("SumReport")]
        [HttpPost]
        public async Task<IHttpActionResult> SumReport(ReportRqModel rqmodel)
        {
            var response = await _pl03Service.SumReport(rqmodel.MA_CHUONG, rqmodel.NAM_BC, rqmodel.KY_BC,
               string.Join(",", rqmodel.DSDVQHNS.ToArray()));
            return Ok(response);
        }

    }

}