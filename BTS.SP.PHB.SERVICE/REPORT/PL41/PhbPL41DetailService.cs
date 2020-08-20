using BTS.SP.PHB.ENTITY.Rp.PL41;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.PL41
{
    public interface IPhbPL41DetailService : IBaseService<PHB_PL41_DETAIL>
    {
        
    }
    public class PhbPL41DetailService:BaseService<PHB_PL41_DETAIL>, IPhbPL41DetailService
    {
        private readonly IRepositoryAsync<PHB_PL41_DETAIL> _repository;
        public PhbPL41DetailService(IRepositoryAsync<PHB_PL41_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
