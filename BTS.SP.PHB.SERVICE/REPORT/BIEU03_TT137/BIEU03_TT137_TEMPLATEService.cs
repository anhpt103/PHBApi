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
    public interface IBIEU03_TT137_TEMPLATEService : IBaseService<PHB_BIEU03_TT137_TEMPLATE>
    {

    }
    public class BIEU03_TT137_TEMPLATEService : BaseService<PHB_BIEU03_TT137_TEMPLATE>, IBIEU03_TT137_TEMPLATEService
    {
        private readonly IRepositoryAsync<PHB_BIEU03_TT137_TEMPLATE> _repository;
        public BIEU03_TT137_TEMPLATEService(IRepositoryAsync<PHB_BIEU03_TT137_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
