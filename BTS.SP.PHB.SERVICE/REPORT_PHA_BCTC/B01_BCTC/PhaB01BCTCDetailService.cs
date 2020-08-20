using BTS.SP.PHB.SERVICE.SERVICES;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BCTC;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BCTC
{
    public interface IPhaB01BCTCDetailService : IBaseService<PHA_B01_BCTC_DETAIL>
    {

    }

    public class PhaB01BCTCDetailService : BaseService<PHA_B01_BCTC_DETAIL>, IPhaB01BCTCDetailService
    {
        private readonly IRepositoryAsync<PHA_B01_BCTC_DETAIL> _repository;

        public PhaB01BCTCDetailService(IRepositoryAsync<PHA_B01_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
