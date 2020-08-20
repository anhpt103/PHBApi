using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B04_TT90
{
    public interface IPhbB04_TT90DetailService : IBaseService<PHB_B04_TT90_DETAIL>
    {

    }
    public class B04_TT90_DETAILService : BaseService<PHB_B04_TT90_DETAIL>, IPhbB04_TT90DetailService
    {
        private readonly IRepositoryAsync<PHB_B04_TT90_DETAIL> _repository;
        public B04_TT90_DETAILService(IRepositoryAsync<PHB_B04_TT90_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
