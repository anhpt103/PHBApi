using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU09TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU09TT344
{
    public interface IPhbBieu09tt344DetailService : IBaseService<PHB_BIEU09TT344_DETAIL>
    {
    }
    public class PhbBieu09tt344DetailService : BaseService<PHB_BIEU09TT344_DETAIL>, IPhbBieu09tt344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU09TT344_DETAIL> _repository;

        public PhbBieu09tt344DetailService(IRepositoryAsync<PHB_BIEU09TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
