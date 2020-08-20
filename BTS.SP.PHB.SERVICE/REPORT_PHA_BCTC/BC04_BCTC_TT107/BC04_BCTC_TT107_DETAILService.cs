using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107
{
    public interface IBc04BCTCTT107DetailService : IBaseService<BC04_BCTC_TT107_DETAILS>
    {

    }

    public class Bc04BCTCTT107DetailService : BaseService<BC04_BCTC_TT107_DETAILS>, IBc04BCTCTT107DetailService
    {
        private readonly IRepositoryAsync<BC04_BCTC_TT107_DETAILS> _repository;

        public Bc04BCTCTT107DetailService(IRepositoryAsync<BC04_BCTC_TT107_DETAILS> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
