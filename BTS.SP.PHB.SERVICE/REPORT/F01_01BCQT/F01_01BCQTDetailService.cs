using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_01BCQT
{
    public interface IF01_01BCQTDetailService : IBaseService<PHB_F01_01BCQT_DETAIL>
    {
        
    }
    public class F01_01BCQTDetailService:BaseService<PHB_F01_01BCQT_DETAIL>, IF01_01BCQTDetailService
    {
        private readonly IRepositoryAsync<PHB_F01_01BCQT_DETAIL> _repository;
        public F01_01BCQTDetailService(IRepositoryAsync<PHB_F01_01BCQT_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
