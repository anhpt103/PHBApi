using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC
{
    public interface IPhaB04BCTCDetailService : IBaseService<PHA_B04_BCTC_DETAIL>
    {

    }

    public class PhaB04BCTCDetailService : BaseService<PHA_B04_BCTC_DETAIL>, IPhaB04BCTCDetailService
    {
        private readonly IRepositoryAsync<PHA_B04_BCTC_DETAIL> _repository;

        public PhaB04BCTCDetailService(IRepositoryAsync<PHA_B04_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
