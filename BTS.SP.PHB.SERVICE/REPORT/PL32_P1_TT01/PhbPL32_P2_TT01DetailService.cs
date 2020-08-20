using BTS.SP.PHB.ENTITY.Rp.PHB.PL32_P1_TT01;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.PL32_P1_TT01
{
    public interface IPhbPL32_P1_TT01DetailService : IBaseService<PHB_PL32_P1_TT01_DETAIL>
    {

    }
    public class PhbPL32_P1_TT01DetailService : BaseService<PHB_PL32_P1_TT01_DETAIL>, IPhbPL32_P1_TT01DetailService
    {
        private readonly IRepositoryAsync<PHB_PL32_P1_TT01_DETAIL> _repository;
        public PhbPL32_P1_TT01DetailService(IRepositoryAsync<PHB_PL32_P1_TT01_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
