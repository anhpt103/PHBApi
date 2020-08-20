using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4A
{
    public interface IPhbBieu4ATemplateService : IBaseService<PHB_BIEU4A_TEMPLATE>
    {
        
    }
    public class PhbBieu4ATemplateService:BaseService<PHB_BIEU4A_TEMPLATE>, IPhbBieu4ATemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU4A_TEMPLATE> _repository;
        public PhbBieu4ATemplateService(IRepositoryAsync<PHB_BIEU4A_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
