using BTS.SP.PHB.ENTITY.Rp.BIEU03_TT137;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU03_TT137
{

    public interface IBIEU03_TT137Service : IBaseService<PHB_BIEU03_TT137>
    {

    }
    public class BIEU03_TT137Service : BaseService<PHB_BIEU03_TT137>, IBIEU03_TT137Service
    {
        private readonly IRepositoryAsync<PHB_BIEU03_TT137> _repository;
        public BIEU03_TT137Service(IRepositoryAsync<PHB_BIEU03_TT137> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
