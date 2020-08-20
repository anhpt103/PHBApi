using BTS.SP.PHB.ENTITY.Rp.BIEU2B;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu2B
{
    public interface IPhbBieu2BDetailService : IBaseService<PHB_BIEU2B_DETAIL>
    {
        
    }
    public class PhbBieu2BDetailService:BaseService<PHB_BIEU2B_DETAIL>, IPhbBieu2BDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU2B_DETAIL> _repository;
        public PhbBieu2BDetailService(IRepositoryAsync<PHB_BIEU2B_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
