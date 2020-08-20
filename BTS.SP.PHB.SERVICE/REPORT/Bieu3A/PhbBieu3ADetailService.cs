using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU3A;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3A
{
    public interface IPhbBieu3ADetailService : IBaseService<PHB_BIEU3A_DETAIL>
    {
        
    }
    public class PhbBieu3ADetailService:BaseService<PHB_BIEU3A_DETAIL>, IPhbBieu3ADetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU3A_DETAIL> _repository;
        public PhbBieu3ADetailService(IRepositoryAsync<PHB_BIEU3A_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
