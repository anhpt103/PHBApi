using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2
{
   
    public interface IPhbB01BSTT2DetailService : IBaseService<PHB_B01_BSTT_2_DETAIL>
    {

    }

    public class PhbB01BSTT2DetailService : BaseService<PHB_B01_BSTT_2_DETAIL>, IPhbB01BSTT2DetailService
    {
        private readonly IRepositoryAsync<PHB_B01_BSTT_2_DETAIL> _repository;

        public PhbB01BSTT2DetailService(IRepositoryAsync<PHB_B01_BSTT_2_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
