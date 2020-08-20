using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BCQT
{
    public interface IPhbB01BCQTDetailService : IBaseService<PHB_B01BCQT_DETAIL>
    {
        
    }
    public class PhbB01BCQTDetailService:BaseService<PHB_B01BCQT_DETAIL>,IPhbB01BCQTDetailService
    {
        private readonly IRepositoryAsync<PHB_B01BCQT_DETAIL> _repository;
        public PhbB01BCQTDetailService(IRepositoryAsync<PHB_B01BCQT_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
