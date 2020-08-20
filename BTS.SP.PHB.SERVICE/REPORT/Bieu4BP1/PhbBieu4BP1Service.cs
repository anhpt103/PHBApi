using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1
{
    public interface IPhbBieu4BP1Service : IBaseService<PHB_BIEU4BP1>
    {
        
    }
    public class PhbBieu4BP1Service:BaseService<PHB_BIEU4BP1>, IPhbBieu4BP1Service
    {
        private readonly IRepositoryAsync<PHB_BIEU4BP1> _repository;
        public PhbBieu4BP1Service(IRepositoryAsync<PHB_BIEU4BP1> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
