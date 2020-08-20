using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC
{
 
    public interface IPhbB05BCTCWorkService : IBaseService<PHB_B05_BCTC_WORK>
    {
    }

    public class PhbB05BCTCWorkService : BaseService<PHB_B05_BCTC_WORK>, IPhbB05BCTCWorkService
    {
        private readonly IRepositoryAsync<PHB_B05_BCTC_WORK> _repository;

        public PhbB05BCTCWorkService(IRepositoryAsync<PHB_B05_BCTC_WORK> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
