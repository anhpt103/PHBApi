using BTS.SP.PHB.ENTITY.Rp.B02_TT137;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B02_TT137
{
  

    public interface IPhbB02TT137DetailService : IBaseService<PHB_B02_TT137_DETAIL>
    {

    }
    public class PhbB02TT137DetailService : BaseService<PHB_B02_TT137_DETAIL>, IPhbB02TT137DetailService
    {
        private readonly IRepositoryAsync<PHB_B02_TT137_DETAIL> _repository;
        public PhbB02TT137DetailService(IRepositoryAsync<PHB_B02_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
