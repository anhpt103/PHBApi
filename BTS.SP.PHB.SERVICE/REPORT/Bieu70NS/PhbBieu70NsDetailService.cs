using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu70NS
{
    public interface IPhbBieu70NsDetailService:IBaseService<PHB_BIEU70NS_DETAIL>
    {
        
    }
    public class PhbBieu70NsDetailService:BaseService<PHB_BIEU70NS_DETAIL>, IPhbBieu70NsDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU70NS_DETAIL> _repository;
        public PhbBieu70NsDetailService(IRepositoryAsync<PHB_BIEU70NS_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
