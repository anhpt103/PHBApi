using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu69NS
{
    public interface IPhbBieu69NsTemplateService:IBaseService<PHB_BIEU69NS_TEMPLATE>
    {
        
    }
    public class PhbBieu69NsTemplateService:BaseService<PHB_BIEU69NS_TEMPLATE>, IPhbBieu69NsTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU69NS_TEMPLATE> _repository;
        public PhbBieu69NsTemplateService(IRepositoryAsync<PHB_BIEU69NS_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
