using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu12TT344
{
    public interface IPhbBieu12TT344DetailService : IBaseService<PHB_BIEU12TT344_DETAIL>
    {

    }
    public class PhbBieu12TT344DetailService : BaseService<PHB_BIEU12TT344_DETAIL>, IPhbBieu12TT344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU12TT344_DETAIL> _repository;
        public PhbBieu12TT344DetailService(IRepositoryAsync<PHB_BIEU12TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
