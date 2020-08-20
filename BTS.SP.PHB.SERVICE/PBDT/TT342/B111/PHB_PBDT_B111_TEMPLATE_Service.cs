using BTS.SP.PHB.ENTITY.PBDT.B111;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.PBDT.B111
{
    public interface IPHB_PBDT_B111_TEMPLATE_Service : IBaseService<PHB_PBDT_B111_TEMPLATE>
    {
    }

    public class PHB_PBDT_B111_TEMPLATE_Service : BaseService<PHB_PBDT_B111_TEMPLATE>, IPHB_PBDT_B111_TEMPLATE_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B111_TEMPLATE> _repository;
        public PHB_PBDT_B111_TEMPLATE_Service(IRepositoryAsync<PHB_PBDT_B111_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
