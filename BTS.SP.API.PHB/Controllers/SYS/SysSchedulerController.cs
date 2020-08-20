using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SYS.SysScheduler;
using Repository.Pattern.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/scheduler")]
    [Route("{id?}")]
    [Authorize]
    public class SysSchedulerController : ApiController
    {
        private readonly ISysSchedulerService _schedulerService;

        public SysSchedulerController(ISysSchedulerService schedulerService, IUnitOfWorkAsync unitOfWork)
        {
            _schedulerService = schedulerService;
        }

        [Route("GetScheduler")]
        [HttpPost]
        public async Task<IHttpActionResult> GetScheduler()
        {
            string msg = _schedulerService.GetScheduler(out List<SYS_SCHEDULER> outListScheduler);
            if (msg.Length > 0) return BadRequest(msg);

            return Ok(outListScheduler);
        }
    }
}