using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu69NS
{
    public interface IPhbBieu69NsDetailService:IBaseService<PHB_BIEU69NS_DETAIL>
    {
        
    }
    public class PhbBieu69NsDetailService:BaseService<PHB_BIEU69NS_DETAIL>, IPhbBieu69NsDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU69NS_DETAIL> _repository;
        public PhbBieu69NsDetailService(IRepositoryAsync<PHB_BIEU69NS_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
