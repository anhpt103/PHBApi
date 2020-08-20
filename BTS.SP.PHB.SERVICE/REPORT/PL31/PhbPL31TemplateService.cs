using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.PL3_1;

namespace BTS.SP.PHB.SERVICE.REPORT.PL31
{
    public interface IPhbPL31TemplateService : IBaseService<PHB_PL31_TEMPLATE>
    {
        
    }
    public class PhbPL31TemplateService:BaseService<PHB_PL31_TEMPLATE>, IPhbPL31TemplateService
    {
        private readonly IRepositoryAsync<PHB_PL31_TEMPLATE> _repository;
        public PhbPL31TemplateService(IRepositoryAsync<PHB_PL31_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
