using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC
{
  

    public interface IPhbB03ABCTCTemplateService : IBaseService<PHB_B03A_BCTC_TEMPLATE>
    {

    }

    public class PhbB03ABCTCTemplateService : BaseService<PHB_B03A_BCTC_TEMPLATE>, IPhbB03ABCTCTemplateService
    {
        private readonly IRepositoryAsync<PHB_B03A_BCTC_TEMPLATE> _repository;

        public PhbB03ABCTCTemplateService(IRepositoryAsync<PHB_B03A_BCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }


    }
}
