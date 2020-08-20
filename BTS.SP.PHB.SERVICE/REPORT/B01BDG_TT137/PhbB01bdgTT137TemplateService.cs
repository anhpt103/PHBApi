using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BDG_TT137
{
    public interface IPhbB01bdgTT137TemplateService : IBaseService<PHB_B01BDG_TT137_TEMPLATE>
    {
        
    }
    public class PhbB01bdgTT137TemplateService : BaseService<PHB_B01BDG_TT137_TEMPLATE>, IPhbB01bdgTT137TemplateService
    {
        private readonly IRepositoryAsync<PHB_B01BDG_TT137_TEMPLATE> _repository;
        public PhbB01bdgTT137TemplateService(IRepositoryAsync<PHB_B01BDG_TT137_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
