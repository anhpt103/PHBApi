using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC
{
    public interface IPhaB02BCTCDetailService : IBaseService<PHA_B02_BCTC_DETAIL>
    {

    }
    public class PhaB02BCTCDetailService : BaseService<PHA_B02_BCTC_DETAIL>, IPhaB02BCTCDetailService
    {
        private readonly IRepositoryAsync<PHA_B02_BCTC_DETAIL> _repository;

        public PhaB02BCTCDetailService(IRepositoryAsync<PHA_B02_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
