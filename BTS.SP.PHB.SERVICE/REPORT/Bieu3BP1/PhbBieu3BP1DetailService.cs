using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1
{
    public interface IPhbBieu3BP1DetailService:IBaseService<PHB_BIEU3BP1_DETAIL>
    {
        
    }
    public class PhbBieu3BP1DetailService:BaseService<PHB_BIEU3BP1_DETAIL>, IPhbBieu3BP1DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU3BP1_DETAIL> _repository;
        public PhbBieu3BP1DetailService(IRepositoryAsync<PHB_BIEU3BP1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
