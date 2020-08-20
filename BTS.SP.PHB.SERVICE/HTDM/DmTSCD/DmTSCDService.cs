using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTSCD
{
    public interface IDmTSCDService:IBaseService<PHB_DM_TSCD>
    {
    }
    public class DmTSCDService:BaseService<PHB_DM_TSCD>, IDmTSCDService
    {
        private readonly IRepositoryAsync<PHB_DM_TSCD> _repository;

        public DmTSCDService(IRepositoryAsync<PHB_DM_TSCD> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
