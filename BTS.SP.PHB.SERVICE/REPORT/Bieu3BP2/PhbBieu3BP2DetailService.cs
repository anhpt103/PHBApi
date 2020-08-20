using BTS.SP.PHB.ENTITY.Rp.BIEU3BP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3BP2
{
    public interface IPhbBieu3BP2DetailService:IBaseService<PHB_BIEU3BP2_DETAIL>
    {
        
    }
    public class PhbBieu3BP2DetailService:BaseService<PHB_BIEU3BP2_DETAIL>, IPhbBieu3BP2DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU3BP2_DETAIL> _repository;
        public PhbBieu3BP2DetailService(IRepositoryAsync<PHB_BIEU3BP2_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
