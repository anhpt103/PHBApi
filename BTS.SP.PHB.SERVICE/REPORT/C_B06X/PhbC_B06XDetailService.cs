using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B06X
{
    public interface IPhbC_B06XDetailService : IBaseService<PHB_C_B06X_DETAIL>
    {
        
    }
    public class PhbC_B06XDetailService : BaseService<PHB_C_B06X_DETAIL>, IPhbC_B06XDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B06X_DETAIL> _repository;
        public PhbC_B06XDetailService(IRepositoryAsync<PHB_C_B06X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
