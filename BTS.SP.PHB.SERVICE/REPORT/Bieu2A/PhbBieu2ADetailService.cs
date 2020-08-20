using BTS.SP.PHB.ENTITY.Rp.BIEU2A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu2A
{
    public interface IPhbBieu2ADetailService : IBaseService<PHB_BIEU2A_DETAIL>
    {
        
    }
    public class PhbBieu2ADetailService:BaseService<PHB_BIEU2A_DETAIL>,IPhbBieu2ADetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU2A_DETAIL> _repository;
        public PhbBieu2ADetailService(IRepositoryAsync<PHB_BIEU2A_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
