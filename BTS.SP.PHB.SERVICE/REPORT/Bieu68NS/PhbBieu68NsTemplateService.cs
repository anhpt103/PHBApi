using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU68NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu68NS
{
    public interface IPhbBieu68NsTemplateService:IBaseService<PHB_BIEU68NS_TEMPLATE>
    {
        
    }
    public class PhbBieu68NsTemplateService:BaseService<PHB_BIEU68NS_TEMPLATE>, IPhbBieu68NsTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU68NS_TEMPLATE> _repository;
        public PhbBieu68NsTemplateService(IRepositoryAsync<PHB_BIEU68NS_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
