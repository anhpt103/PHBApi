using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU2CP2
{
    public interface IPhbBIEU2CP2DetailService : IBaseService<PHB_BIEU2CP2_DETAIL>
    {
        
    }
    public class PhbBIEU2CP2DetailService:BaseService<PHB_BIEU2CP2_DETAIL>,IPhbBIEU2CP2DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU2CP2_DETAIL> _repository;
        public PhbBIEU2CP2DetailService(IRepositoryAsync<PHB_BIEU2CP2_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
