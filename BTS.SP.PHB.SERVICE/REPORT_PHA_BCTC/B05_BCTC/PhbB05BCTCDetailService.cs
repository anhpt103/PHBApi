using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC
{

    public interface IPhbB05BCTCDetailService : IBaseService<PHB_B05_BCTC_DETAIL>
    {
    }

    public class PhbB05BCTCDetailService : BaseService<PHB_B05_BCTC_DETAIL>, IPhbB05BCTCDetailService
    {
        private readonly IRepositoryAsync<PHB_B05_BCTC_DETAIL> _repository;

        public PhbB05BCTCDetailService(IRepositoryAsync<PHB_B05_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
