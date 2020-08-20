using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BDG_TT137
{
    public interface IPhbB01bdgTT137DetailService : IBaseService<PHB_B01BDG_TT137_DETAIL>
    {
        
    }
    public class PhbB01bdgTT137DetailService : BaseService<PHB_B01BDG_TT137_DETAIL>, IPhbB01bdgTT137DetailService
    {
        private readonly IRepositoryAsync<PHB_B01BDG_TT137_DETAIL> _repository;
        public PhbB01bdgTT137DetailService(IRepositoryAsync<PHB_B01BDG_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
