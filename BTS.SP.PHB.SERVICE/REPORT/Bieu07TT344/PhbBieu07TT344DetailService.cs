using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.ENTITY.Rp.BIEU07TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu07TT344
{
    public interface IPhbBieu07TT344DetailService : IBaseService<PHB_BIEU07TT344_DETAIL>
    {

    }
    public class PhbBieu07TT344DetailService : BaseService<PHB_BIEU07TT344_DETAIL>, IPhbBieu07TT344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU07TT344_DETAIL> _repository;
        public PhbBieu07TT344DetailService(IRepositoryAsync<PHB_BIEU07TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
