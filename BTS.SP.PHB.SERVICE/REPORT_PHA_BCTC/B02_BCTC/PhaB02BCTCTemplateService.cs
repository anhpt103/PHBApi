using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC
{
    public interface IPhaB02BCTCTemplateService : IBaseService<PHA_B02_BCTC_TEMPLATE>
    {

    }

    public class PhaB02BCTCTemplateService : BaseService<PHA_B02_BCTC_TEMPLATE>, IPhaB02BCTCTemplateService
    {
        private readonly IRepositoryAsync<PHA_B02_BCTC_TEMPLATE> _repository;

        public PhaB02BCTCTemplateService(IRepositoryAsync<PHA_B02_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
