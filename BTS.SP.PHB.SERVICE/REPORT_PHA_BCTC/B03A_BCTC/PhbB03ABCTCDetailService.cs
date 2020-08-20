using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC
{
   
    public interface IPhbB03ABCTCDetailService : IBaseService<PHB_B03A_BCTC_DETAIL>
    {

    }

    public class PhbB03ABCTCDetailService : BaseService<PHB_B03A_BCTC_DETAIL>, IPhbB03ABCTCDetailService
    {
        private readonly IRepositoryAsync<PHB_B03A_BCTC_DETAIL> _repository;

        public PhbB03ABCTCDetailService(IRepositoryAsync<PHB_B03A_BCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }


    }

}
