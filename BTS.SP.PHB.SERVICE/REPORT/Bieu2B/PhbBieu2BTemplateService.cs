using BTS.SP.PHB.ENTITY.Rp.BIEU2B;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu2B
{
    public interface IPhbBieu2BTemplateService:IBaseService<PHB_BIEU2B_TEMPLATE>
    {
        
    }
    public class PhbBieu2BTemplateService:BaseService<PHB_BIEU2B_TEMPLATE>, IPhbBieu2BTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU2B_TEMPLATE> _repository;
        public PhbBieu2BTemplateService(IRepositoryAsync<PHB_BIEU2B_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
