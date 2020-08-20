using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B03_TT90
{
    public interface IPhbB03_TT90DetailService : IBaseService<PHB_B03_TT90_DETAIL>
    {

    }
    public class B03_TT90_DETAILService : BaseService<PHB_B03_TT90_DETAIL>, IPhbB03_TT90DetailService
    {
        private readonly IRepositoryAsync<PHB_B03_TT90_DETAIL> _repository;
        public B03_TT90_DETAILService(IRepositoryAsync<PHB_B03_TT90_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
