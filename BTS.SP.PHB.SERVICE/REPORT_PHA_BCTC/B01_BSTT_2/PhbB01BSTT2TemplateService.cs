using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2
{
  

    public interface IPhbB01BSTT2TemplateService : IBaseService<PHB_B01_BSTT_2_TEMPLATE>
    {

    }

    public class PhbB01BSTT2TemplateService : BaseService<PHB_B01_BSTT_2_TEMPLATE>, IPhbB01BSTT2TemplateService
    {
        private readonly IRepositoryAsync<PHB_B01_BSTT_2_TEMPLATE> _repository;

        public PhbB01BSTT2TemplateService(IRepositoryAsync<PHB_B01_BSTT_2_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }


    }
}
