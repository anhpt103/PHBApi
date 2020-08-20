using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.L_PC_UB;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.L_PC_UB
{
    public interface IPhbL_PC_UBDetailService : IBaseService<PHB_L_PC_UB_DETAIL>
    {
        
    }
    public class PhbL_PC_UBDetailService : BaseService<PHB_L_PC_UB_DETAIL>, IPhbL_PC_UBDetailService
    {
        private readonly IRepositoryAsync<PHB_L_PC_UB_DETAIL> _repository;
        public PhbL_PC_UBDetailService(IRepositoryAsync<PHB_L_PC_UB_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
