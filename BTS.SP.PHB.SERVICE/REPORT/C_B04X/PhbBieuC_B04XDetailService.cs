using BTS.SP.PHB.ENTITY.Rp.C_B04X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
namespace BTS.SP.PHB.SERVICE.REPORT.C_B04X
{
    public interface IPhbBieuC_B04XDetailService : IBaseService<PHB_C_B04X_DETAIL>
    {
    }
    public class PhbBieuC_B04XDetailService : BaseService<PHB_C_B04X_DETAIL>, IPhbBieuC_B04XDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B04X_DETAIL> _repository;
        public PhbBieuC_B04XDetailService(IRepositoryAsync<PHB_C_B04X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
