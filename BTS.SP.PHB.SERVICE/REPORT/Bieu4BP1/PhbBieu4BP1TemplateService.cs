using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1
{
    public interface IPhbBieu4BP1TemplateService:IBaseService<PHB_BIEU4BP1_TEMPLATE>
    {
        
    }
    public class PhbBieu4BP1TemplateService:BaseService<PHB_BIEU4BP1_TEMPLATE>, IPhbBieu4BP1TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU4BP1_TEMPLATE> _repository;
        public PhbBieu4BP1TemplateService(IRepositoryAsync<PHB_BIEU4BP1_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
