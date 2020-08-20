using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU01CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU01CP2
{
    public interface IBIEU01CP2DetailService : IBaseService<PHB_BIEU01CP2_DETAIL>
    {
        
    }
    public class BIEU01CP2DetailService:BaseService<PHB_BIEU01CP2_DETAIL>, IBIEU01CP2DetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU01CP2_DETAIL> _repository;
        public BIEU01CP2DetailService(IRepositoryAsync<PHB_BIEU01CP2_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
