using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.PHB.PL42_P1_TT01;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.PL42_P1_TT01
{
    public interface IPhbPL42_P1_TT01DetailService : IBaseService<PHB_PL42_P1_TT01_DETAIL>
    {

    }
    public class PhbPL42_P1_TT01DetailService : BaseService<PHB_PL42_P1_TT01_DETAIL>, IPhbPL42_P1_TT01DetailService
    {
        private readonly IRepositoryAsync<PHB_PL42_P1_TT01_DETAIL> _repository;
        public PhbPL42_P1_TT01DetailService(IRepositoryAsync<PHB_PL42_P1_TT01_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
