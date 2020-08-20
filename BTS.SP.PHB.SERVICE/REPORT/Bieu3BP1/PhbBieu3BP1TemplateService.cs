using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1
{
    public interface IPhbBieu3BP1TemplateService:IBaseService<PHB_BIEU3BP1_TEMPLATE>
    {
        
    }
    public class PhbBieu3BP1TemplateService:BaseService<PHB_BIEU3BP1_TEMPLATE>, IPhbBieu3BP1TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU3BP1_TEMPLATE> _repository;
        public PhbBieu3BP1TemplateService(IRepositoryAsync<PHB_BIEU3BP1_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
