using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU10TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU10TT344
{
    public interface IPhbBieu10tt344DetailService : IBaseService<PHB_BIEU10TT344_DETAIL>
    { }
    public class PhbBieu10tt344DetailService : BaseService<PHB_BIEU10TT344_DETAIL>, IPhbBieu10tt344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU10TT344_DETAIL> _repository;

        public PhbBieu10tt344DetailService(IRepositoryAsync<PHB_BIEU10TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
