using BTS.SP.PHB.ENTITY.Rp.L_PC_DT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.L_PC_DT
{
    public interface IPhbL_PC_DTDetailService : IBaseService<PHB_L_PC_DT_DETAIL>
    {

    }
    public class PhbL_PC_DTDetailService : BaseService<PHB_L_PC_DT_DETAIL>, IPhbL_PC_DTDetailService
    {
        private readonly IRepositoryAsync<PHB_L_PC_DT_DETAIL> _repository;
        public PhbL_PC_DTDetailService(IRepositoryAsync<PHB_L_PC_DT_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
