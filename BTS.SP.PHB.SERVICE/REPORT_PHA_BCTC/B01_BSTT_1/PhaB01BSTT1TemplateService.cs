using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1
{
  

    public interface IPhaB01BSTT1TemplateService : IBaseService<PHA_B01_BSTT_1_TEMPLATE>
    {

    }

    public class PhaB01BSTT1TemplateService : BaseService<PHA_B01_BSTT_1_TEMPLATE>, IPhaB01BSTT1TemplateService
    {
        private readonly IRepositoryAsync<PHA_B01_BSTT_1_TEMPLATE> _repository;

        public PhaB01BSTT1TemplateService(IRepositoryAsync<PHA_B01_BSTT_1_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }


    }
}
