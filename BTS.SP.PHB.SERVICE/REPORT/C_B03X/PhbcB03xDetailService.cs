using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B03X
{
    public interface IPhbcB03xDetailService : IBaseService<PHB_C_B03X_DETAIL>
    {
        
    }
    public class PhbcB03xDetailService : BaseService<PHB_C_B03X_DETAIL>, IPhbcB03xDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B03X_DETAIL> _repository;

        public PhbcB03xDetailService(IRepositoryAsync<PHB_C_B03X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
