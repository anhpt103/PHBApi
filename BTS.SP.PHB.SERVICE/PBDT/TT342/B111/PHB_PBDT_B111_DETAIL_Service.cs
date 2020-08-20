using BTS.SP.PHB.ENTITY.PBDT.B111;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.PBDT.B111
{
    public interface IPHB_PBDT_B111_DETAIL_Service : IBaseService<PHB_PBDT_B111_DETAIL>
    {
    }

    public class PHB_PBDT_B111_DETAIL_Service : BaseService<PHB_PBDT_B111_DETAIL>, IPHB_PBDT_B111_DETAIL_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B111_DETAIL> _repository;
        public PHB_PBDT_B111_DETAIL_Service(IRepositoryAsync<PHB_PBDT_B111_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
