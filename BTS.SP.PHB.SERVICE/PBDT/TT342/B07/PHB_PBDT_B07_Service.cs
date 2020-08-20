using BTS.SP.PHB.ENTITY.PBDT.B07;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.PBDT.B07
{
    public interface IPHB_PBDT_B07_Service : IBaseService<PHB_PBDT_B07>
    {
    }

    public class PHB_PBDT_B07_Service : BaseService<PHB_PBDT_B07>, IPHB_PBDT_B07_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B07> _repository;
        public PHB_PBDT_B07_Service(IRepositoryAsync<PHB_PBDT_B07> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
