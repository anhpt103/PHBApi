using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU3A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3A
{
    public interface IPhbBieu3ATemplateService : IBaseService<PHB_BIEU3A_TEMPLATE>
    {
        
    }
    public class PhbBieu3ATemplateService:BaseService<PHB_BIEU3A_TEMPLATE>, IPhbBieu3ATemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU3A_TEMPLATE> _repository;
        public PhbBieu3ATemplateService(IRepositoryAsync<PHB_BIEU3A_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
