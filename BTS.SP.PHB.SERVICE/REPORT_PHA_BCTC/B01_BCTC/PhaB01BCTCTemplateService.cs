using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BCTC;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BCTC
{
    public interface IPhaB01BCTCTemplateService : IBaseService<PHA_B01_BCTC_TEMPLATE>
    {

    }

    public class PhaB01BCTCTemplateService : BaseService<PHA_B01_BCTC_TEMPLATE>, IPhaB01BCTCTemplateService
    {
        private readonly IRepositoryAsync<PHA_B01_BCTC_TEMPLATE> _repository;

        public PhaB01BCTCTemplateService(IRepositoryAsync<PHA_B01_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
