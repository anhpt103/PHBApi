using BTS.SP.PHB.ENTITY.Rp.BM14_TT144;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BM14_TT134
{
   
    public interface IPhbBm14TT134DetailService : IBaseService<PHB_BM14_TT134_DETAIL>
    {

    }
    public class PhbBm14TT134DetailService : BaseService<PHB_BM14_TT134_DETAIL>, IPhbBm14TT134DetailService
    {
        private readonly IRepositoryAsync<PHB_BM14_TT134_DETAIL> _repository;
        public PhbBm14TT134DetailService(IRepositoryAsync<PHB_BM14_TT134_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
