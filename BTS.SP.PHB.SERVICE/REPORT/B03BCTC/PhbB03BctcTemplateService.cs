using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B03BBCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCTC
{
    public interface IPhbB03BctcTemplateService : IBaseService<PHB_B03BBCTC_TEMPLATE>
    {
    }

    public class PhbB03BctcTemplateService : BaseService<PHB_B03BBCTC_TEMPLATE>, IPhbB03BctcTemplateService
    {
        private readonly IRepositoryAsync<PHB_B03BBCTC_TEMPLATE> _repository;
        public PhbB03BctcTemplateService(IRepositoryAsync<PHB_B03BBCTC_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
