using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu08TT344
{
    public interface IPhbBieu08TT344DetailService : IBaseService<PHB_BIEU08TT344_DETAIL>
    {

    }
    public class PhbBieu08TT344DetailService : BaseService<PHB_BIEU08TT344_DETAIL>, IPhbBieu08TT344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU08TT344_DETAIL> _repository;
        public PhbBieu08TT344DetailService(IRepositoryAsync<PHB_BIEU08TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
