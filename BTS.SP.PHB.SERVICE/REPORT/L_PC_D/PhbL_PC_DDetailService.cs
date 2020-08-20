using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.L_PC_D;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.L_PC_D
{
    public interface IPhbL_PC_DDetailService : IBaseService<PHB_L_PC_D_DETAIL>
    {
        
    }
    public class PhbL_PC_DDetailService : BaseService<PHB_L_PC_D_DETAIL>, IPhbL_PC_DDetailService
    {
        private readonly IRepositoryAsync<PHB_L_PC_D_DETAIL> _repository;
        public PhbL_PC_DDetailService(IRepositoryAsync<PHB_L_PC_D_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
