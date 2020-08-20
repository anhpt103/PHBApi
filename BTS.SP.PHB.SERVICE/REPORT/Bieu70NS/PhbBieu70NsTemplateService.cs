using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu70NS
{
    public interface IPhbBieu70NsTemplateService:IBaseService<PHB_BIEU70NS_TEMPLATE>
    {
        
    }
    public class PhbBieu70NsTemplateService:BaseService<PHB_BIEU70NS_TEMPLATE>, IPhbBieu70NsTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU70NS_TEMPLATE> _repository;
        public PhbBieu70NsTemplateService(IRepositoryAsync<PHB_BIEU70NS_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
