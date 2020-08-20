using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.PL3_1;

namespace BTS.SP.PHB.SERVICE.REPORT.PL31
{
    public interface IPhbPL31DetailService : IBaseService<PHB_PL31_DETAIL>
    {
        
    }
    public class PhbPL31DetailService:BaseService<PHB_PL31_DETAIL>, IPhbPL31DetailService
    {
        private readonly IRepositoryAsync<PHB_PL31_DETAIL> _repository;
        public PhbPL31DetailService(IRepositoryAsync<PHB_PL31_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
