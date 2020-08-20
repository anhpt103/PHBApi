using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1
{
    public interface IPhbF01_02DetailService : IBaseService<PHB_F01_02_P1_DETAIL>
    {

    }

    public class PhbF01_02DetailService : BaseService<PHB_F01_02_P1_DETAIL>, IPhbF01_02DetailService
    {
        private readonly IRepositoryAsync<PHB_F01_02_P1_DETAIL> _repository;

        public PhbF01_02DetailService(IRepositoryAsync<PHB_F01_02_P1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
