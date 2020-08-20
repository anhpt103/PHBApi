using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;

namespace BTS.SP.PHB.SERVICE.REPORT.Bm48TT342
{
    public interface IPhbBm48TT342DetailService : IBaseService<PHB_BM48_TT342_DETAIL>
    {
        
    }
    public class PhbBm48TT342DetailService : BaseService<PHB_BM48_TT342_DETAIL>, IPhbBm48TT342DetailService
    {
        private readonly IRepositoryAsync<PHB_BM48_TT342_DETAIL> _repository;
        public PhbBm48TT342DetailService(IRepositoryAsync<PHB_BM48_TT342_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
