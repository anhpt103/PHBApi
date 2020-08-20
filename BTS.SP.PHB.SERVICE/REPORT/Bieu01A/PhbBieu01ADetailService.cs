using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01A
{
    public interface IPhbBieu01ADetailService : IBaseService<PHB_BIEU01A_DETAIL>
    {
        
    }
    public class PhbBieu01ADetailService:BaseService<PHB_BIEU01A_DETAIL>,IPhbBieu01ADetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU01A_DETAIL> _repository;
        public PhbBieu01ADetailService(IRepositoryAsync<PHB_BIEU01A_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
