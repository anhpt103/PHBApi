using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU11TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu11TT344
{
    public interface IPhbBieu11TT344DetailService : IBaseService<PHB_BIEU11TT344_DETAIL>
    {

    }
    public class PhbBieu11TT344DetailService : BaseService<PHB_BIEU11TT344_DETAIL>, IPhbBieu11TT344DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU11TT344_DETAIL> _repository;
        public PhbBieu11TT344DetailService(IRepositoryAsync<PHB_BIEU11TT344_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
