using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BM16_TT344
{
  
    public interface IPhbBm16TT344DetailService : IBaseService<PHB_BM16_TT344_DETAIL>
    {

    }
    public class PhbBm16TT344DetailService : BaseService<PHB_BM16_TT344_DETAIL>, IPhbBm16TT344DetailService
    {
        private readonly IRepositoryAsync<PHB_BM16_TT344_DETAIL> _repository;
        public PhbBm16TT344DetailService(IRepositoryAsync<PHB_BM16_TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
