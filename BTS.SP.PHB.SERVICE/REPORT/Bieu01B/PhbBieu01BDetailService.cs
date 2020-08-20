using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU01B;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu01B
{
    public interface IPhbBieu01BDetailService : IBaseService<PHB_BIEU01B_DETAIL>
    {
        
    }
    public class PhbBieu01BDetailService:BaseService<PHB_BIEU01B_DETAIL>, IPhbBieu01BDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU01B_DETAIL> _repository;
        public PhbBieu01BDetailService(IRepositoryAsync<PHB_BIEU01B_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
