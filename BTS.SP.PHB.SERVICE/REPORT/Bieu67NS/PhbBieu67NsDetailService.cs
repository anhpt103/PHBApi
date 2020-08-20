using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU67NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu67NS
{
    public interface IPhbBieu67NsDetailService:IBaseService<PHB_BIEU67NS_DETAIL>
    {
        
    }
    public class PhbBieu67NsDetailService:BaseService<PHB_BIEU67NS_DETAIL>, IPhbBieu67NsDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU67NS_DETAIL> _repository;
        public PhbBieu67NsDetailService(IRepositoryAsync<PHB_BIEU67NS_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
