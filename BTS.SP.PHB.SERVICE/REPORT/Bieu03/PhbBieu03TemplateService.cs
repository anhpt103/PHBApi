using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu03
{
    public interface IPhbBieu03TemplateService:IBaseService<PHB_BIEU03_TEMPLATE>
    {
        
    }
    public class PhbBieu03TemplateService:BaseService<PHB_BIEU03_TEMPLATE>, IPhbBieu03TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU03_TEMPLATE> _repository;
        public PhbBieu03TemplateService(IRepositoryAsync<PHB_BIEU03_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
