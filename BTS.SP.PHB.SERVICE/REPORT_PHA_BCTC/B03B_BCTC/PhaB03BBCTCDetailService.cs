using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC
{
    public interface IPhaB03BBCTCDetailService : IBaseService<PHA_B03B_BCTC_DETAIL>
    {

    }
    public class PhaB03BBCTCDetailService : BaseService<PHA_B03B_BCTC_DETAIL>, IPhaB03BBCTCDetailService
    {
        private readonly IRepositoryAsync<PHA_B03B_BCTC_DETAIL> _repository;

        public PhaB03BBCTCDetailService(IRepositoryAsync<PHA_B03B_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
