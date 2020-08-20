using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1
{
    public interface IPhbBIEU2CP1DetailService : IBaseService<PHB_BIEU2CP1_DETAIL>
    {
        
    }
    public class PhbBIEU2CP1DetailService:BaseService<PHB_BIEU2CP1_DETAIL>,IPhbBIEU2CP1DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU2CP1_DETAIL> _repository;
        public PhbBIEU2CP1DetailService(IRepositoryAsync<PHB_BIEU2CP1_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
