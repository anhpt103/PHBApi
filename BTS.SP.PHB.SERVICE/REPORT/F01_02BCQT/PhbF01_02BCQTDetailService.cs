using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT
{
    public interface IPhbF01_02BCQTDetailService : IBaseService<PHB_F01_02BCQT_DETAIL>
    {
        
    }
    public class PhbF01_02BCQTDetailService:BaseService<PHB_F01_02BCQT_DETAIL>,IPhbF01_02BCQTDetailService
    {
        private readonly IRepositoryAsync<PHB_F01_02BCQT_DETAIL> _repository;
        public PhbF01_02BCQTDetailService(IRepositoryAsync<PHB_F01_02BCQT_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
