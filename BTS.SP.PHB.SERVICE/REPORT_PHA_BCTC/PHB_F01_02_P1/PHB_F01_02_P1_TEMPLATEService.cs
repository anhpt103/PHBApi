using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1
{
    public interface IPhbF01_02_P1TemplateService : IBaseService<PHB_F01_02_P1_TEMPLATE>
    {

    }

    public class PhbF01_02_P1TemplateService : BaseService<PHB_F01_02_P1_TEMPLATE>, IPhbF01_02_P1TemplateService
    {
        private readonly IRepositoryAsync<PHB_F01_02_P1_TEMPLATE> _repository;

        public PhbF01_02_P1TemplateService(IRepositoryAsync<PHB_F01_02_P1_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
