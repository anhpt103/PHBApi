using BTS.SP.PHB.ENTITY.Rp.BIEU4BP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4BP2
{
    public interface IPhbBieu4BP2Service : IBaseService<PHB_BIEU4BP2>
    {
        
    }
    public class PhbBieu4BP2Service:BaseService<PHB_BIEU4BP2>, IPhbBieu4BP2Service
    {
        private readonly IRepositoryAsync<PHB_BIEU4BP2> _repository;
        public PhbBieu4BP2Service(IRepositoryAsync<PHB_BIEU4BP2> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
