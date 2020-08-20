using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1
{
   
    public interface IPhaB01BSTT1DetailService : IBaseService<PHA_B01_BSTT_1_DETAIL>
    {

    }

    public class PhaB01BSTT1DetailService : BaseService<PHA_B01_BSTT_1_DETAIL>, IPhaB01BSTT1DetailService
    {
        private readonly IRepositoryAsync<PHA_B01_BSTT_1_DETAIL> _repository;

        public PhaB01BSTT1DetailService(IRepositoryAsync<PHA_B01_BSTT_1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
