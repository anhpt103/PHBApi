using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SYS.SysLogSyncMisa;
using Repository.Pattern.UnitOfWork;
using System.Threading.Tasks;
using System.Web.Http;

namespace BTS.SP.API.PHB.Controllers.SYS
{
    [RoutePrefix("api/sys/logSyncMisa")]
    [Route("{id?}")]
    [Authorize]
    public class SysLogSyncMisaController : ApiController
    {
        private readonly ISysLogSyncMisaService _logService;

        public SysLogSyncMisaController(ISysLogSyncMisaService logService)
        {
            _logService = logService;
        }

        [Route("WriteLog")]
        [HttpPost]
        public async Task<IHttpActionResult> WriteLog([FromBody] SYS_LOG_SYNC_MISA log)
        {
            _logService.WriteLogDatabase(log);
            return Ok(log);
        }
    }
}