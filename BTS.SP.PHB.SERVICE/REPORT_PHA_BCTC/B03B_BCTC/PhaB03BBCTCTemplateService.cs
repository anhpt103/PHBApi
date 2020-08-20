using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC
{
    public interface IPhaB03BBCTCTemplateService : IBaseService<PHA_B03B_BCTC_TEMPLATE>
    {

    }
    public class PhaB03BBCTCTemplateService : BaseService<PHA_B03B_BCTC_TEMPLATE>, IPhaB03BBCTCTemplateService
    {
        private readonly IRepositoryAsync<PHA_B03B_BCTC_TEMPLATE> _repository;

        public PhaB03BBCTCTemplateService(IRepositoryAsync<PHA_B03B_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
