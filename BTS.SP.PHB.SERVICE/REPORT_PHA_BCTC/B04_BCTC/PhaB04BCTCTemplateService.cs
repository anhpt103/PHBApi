using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC
{
    public interface IPhaB04BCTCTemplateService : IBaseService<PHA_B04_BCTC_TEMPLATE>
    {

    }

    public class PhaB04BCTCTemplateService : BaseService<PHA_B04_BCTC_TEMPLATE>, IPhaB04BCTCTemplateService
    {
        private readonly IRepositoryAsync<PHA_B04_BCTC_TEMPLATE> _repository;

        public PhaB04BCTCTemplateService(IRepositoryAsync<PHA_B04_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
