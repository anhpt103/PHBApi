using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC
{
    
    public interface IPhbB05BCTCTemplateService : IBaseService<PHB_B05_BCTC_TEMPLATE>
    {
    }

    public class PhbB05BCTCTemplateService : BaseService<PHB_B05_BCTC_TEMPLATE>, IPhbB05BCTCTemplateService
    {
        private readonly IRepositoryAsync<PHB_B05_BCTC_TEMPLATE> _repository;

        public PhbB05BCTCTemplateService(IRepositoryAsync<PHB_B05_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
