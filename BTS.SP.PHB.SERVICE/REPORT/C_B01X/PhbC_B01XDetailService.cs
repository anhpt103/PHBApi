using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B01X
{
    public interface IPhbC_B01XDetailService : IBaseService<PHB_C_B01X_DETAIL>
    {
        
    }
    public class PhbC_B01XDetailService : BaseService<PHB_C_B01X_DETAIL>, IPhbC_B01XDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B01X_DETAIL> _repository;
        public PhbC_B01XDetailService(IRepositoryAsync<PHB_C_B01X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
