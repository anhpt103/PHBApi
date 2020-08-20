using BTS.SP.PHB.ENTITY.Rp.BIEU4BP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4BP2
{
    public interface IPhbBieu4BP2DetailService:IBaseService<PHB_BIEU4BP2_DETAIL>
    {
        
    }
    public class PhbBieu4BP2DetailService:BaseService<PHB_BIEU4BP2_DETAIL>, IPhbBieu4BP2DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU4BP2_DETAIL> _repository;
        public PhbBieu4BP2DetailService(IRepositoryAsync<PHB_BIEU4BP2_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
