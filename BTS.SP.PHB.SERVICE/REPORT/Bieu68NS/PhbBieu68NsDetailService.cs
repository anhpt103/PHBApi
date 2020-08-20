using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU68NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu68NS
{
    public interface IPhbBieu68NsDetailService:IBaseService<PHB_BIEU68NS_DETAIL>
    {
        
    }
    public class PhbBieu68NsDetailService:BaseService<PHB_BIEU68NS_DETAIL>, IPhbBieu68NsDetailService
    {
        private readonly IRepositoryAsync<PHB_BIEU68NS_DETAIL> _repository;
        public PhbBieu68NsDetailService(IRepositoryAsync<PHB_BIEU68NS_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
