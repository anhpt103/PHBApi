using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1
{
    public interface IPhbBieu4BP1DetailService:IBaseService<PHB_BIEU4BP1_DETAIL>
    {
        
    }
    public class PhbBieu4BP1DetailService:BaseService<PHB_BIEU4BP1_DETAIL>, IPhbBieu4BP1DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU4BP1_DETAIL> _repository;
        public PhbBieu4BP1DetailService(IRepositoryAsync<PHB_BIEU4BP1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
