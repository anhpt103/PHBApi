using System.Threading.Tasks;
using System.Web.Http;
using BTS.SP.PHB.SERVICE;
using BTS.SP.PHB.SERVICE.AUTH.Shared;
using BTS.SP.PHB.SERVICE.UTILS;
using Repository.Pattern.UnitOfWork;

namespace BTS.SP.API.PHB.Controllers.SHARED
{
    [RoutePrefix("api/Shared")]
    [Route("{id?}")]
    [Authorize]
    public class SharedController : ApiController
    {
        private readonly ISharedService _sharedService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SharedController(ISharedService sharedService,IUnitOfWorkAsync unitOfWorkAsync)
        {
            _sharedService = sharedService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpGet]
        [Route("GetAccesslist/{machucnang}")]
        public async Task<RoleState> GetAccesslist(string machucnang)
        {
            if (RequestContext.Principal.Identity.Name.Equals("admin"))
            {
                return new RoleState()
                {
                    Approve = true,Delete = true,Add = true,STATE = "ALL",Edit = true,View = true
                };
            }
            var roleState = await _sharedService.GetRoleStateByMaChucNang("B",RequestContext.Principal.Identity.Name, machucnang);
            return roleState;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
