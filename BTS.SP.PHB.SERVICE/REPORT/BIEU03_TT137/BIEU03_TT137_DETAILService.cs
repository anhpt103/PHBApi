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
    public interface IBIEU03_TT137_DETAILService : IBaseService<PHB_BIEU03_TT137_DETAIL>
    {

    }
    public class BIEU03_TT137_DETAILService : BaseService<PHB_BIEU03_TT137_DETAIL>, IBIEU03_TT137_DETAILService
    {
        private readonly IRepositoryAsync<PHB_BIEU03_TT137_DETAIL> _repository;
        public BIEU03_TT137_DETAILService(IRepositoryAsync<PHB_BIEU03_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
