using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01A_TT137
{
    public interface IPhbB01aTT137DetailService : IBaseService<PHB_B01A_TT137_DETAIL>
    {
        
    }
    public class PhbB01aTT137DetailService : BaseService<PHB_B01A_TT137_DETAIL>, IPhbB01aTT137DetailService
    {
        private readonly IRepositoryAsync<PHB_B01A_TT137_DETAIL> _repository;
        public PhbB01aTT137DetailService(IRepositoryAsync<PHB_B01A_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
