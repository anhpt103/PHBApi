using BTS.SP.PHB.ENTITY.PBDT.B07;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.PBDT.B07
{
    public interface IPHB_PBDT_B07_TEMPLATE_Service : IBaseService<PHB_PBDT_B07_TEMPLATE>
    {
    }

    public class PHB_PBDT_B07_TEMPLATE_Service : BaseService<PHB_PBDT_B07_TEMPLATE>, IPHB_PBDT_B07_TEMPLATE_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B07_TEMPLATE> _repository;
        public PHB_PBDT_B07_TEMPLATE_Service(IRepositoryAsync<PHB_PBDT_B07_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
