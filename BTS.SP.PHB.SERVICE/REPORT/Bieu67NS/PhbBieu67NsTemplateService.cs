using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU67NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu67NS
{
    public interface IPhbBieu67NsTemplateService:IBaseService<PHB_BIEU67NS_TEMPLATE>
    {
        
    }
    public class PhbBieu67NsTemplateService:BaseService<PHB_BIEU67NS_TEMPLATE>, IPhbBieu67NsTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU67NS_TEMPLATE> _repository;
        public PhbBieu67NsTemplateService(IRepositoryAsync<PHB_BIEU67NS_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
