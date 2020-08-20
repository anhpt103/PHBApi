using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT_PII
{
    public interface IF01_02BCQT_PIIDetailService : IBaseService<PHB_F01_02BCQT_PII_DETAIL>
    {
        
    }
    public class F01_02BCQT_PIIDetailService:BaseService<PHB_F01_02BCQT_PII_DETAIL>, IF01_02BCQT_PIIDetailService
    {
        private readonly IRepositoryAsync<PHB_F01_02BCQT_PII_DETAIL> _repository;
        public F01_02BCQT_PIIDetailService(IRepositoryAsync<PHB_F01_02BCQT_PII_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
