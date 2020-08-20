using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu4A
{
    public interface IPhbBieu4ADetailService : IBaseService<PHB_BIEU4A_DETAIL>
    {
        
    }
    public class PhbBieu4ADetailService:BaseService<PHB_BIEU4A_DETAIL>, IPhbBieu4ADetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU4A_DETAIL> _repository;
        public PhbBieu4ADetailService(IRepositoryAsync<PHB_BIEU4A_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
