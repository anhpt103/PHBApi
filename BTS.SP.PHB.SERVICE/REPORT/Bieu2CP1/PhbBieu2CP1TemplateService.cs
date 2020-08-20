using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP1;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1
{
    public interface IPhbBIEU2CP1TemplateService:IBaseService<PHB_BIEU2CP1_TEMPLATE>
    {
        
    }
    public class PhbBIEU2CP1TemplateService:BaseService<PHB_BIEU2CP1_TEMPLATE>, IPhbBIEU2CP1TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU2CP1_TEMPLATE> _repository;
        public PhbBIEU2CP1TemplateService(IRepositoryAsync<PHB_BIEU2CP1_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
