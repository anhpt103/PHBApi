using BTS.SP.PHB.ENTITY.Rp.PL41;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.PL41
{
    public interface IPhbPL41TemplateService : IBaseService<PHB_PL41_TEMPLATE>
    {
        
    }
    public class PhbPL41TemplateService:BaseService<PHB_PL41_TEMPLATE>, IPhbPL41TemplateService
    {
        private readonly IRepositoryAsync<PHB_PL41_TEMPLATE> _repository;
        public PhbPL41TemplateService(IRepositoryAsync<PHB_PL41_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
