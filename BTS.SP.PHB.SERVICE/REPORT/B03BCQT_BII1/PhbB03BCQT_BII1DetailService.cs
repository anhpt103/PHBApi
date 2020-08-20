using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCQT_BII1
{
    public interface IPhbB03BCQT_BII1DetailService : IBaseService<PHB_B03BCQT_BII1_DETAIL>
    {
        
    }
    public class PhbB03BCQT_BII1DetailService:BaseService<PHB_B03BCQT_BII1_DETAIL>, IPhbB03BCQT_BII1DetailService
    {
        private readonly IRepositoryAsync<PHB_B03BCQT_BII1_DETAIL> _repository;
        public PhbB03BCQT_BII1DetailService(IRepositoryAsync<PHB_B03BCQT_BII1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
