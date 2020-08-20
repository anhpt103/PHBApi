using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.PHB_PL32_P2_TT01;

namespace BTS.SP.PHB.SERVICE.REPORT.PL32_P2_TT01
{
    public interface IPhbPL32_P2_TT01Service : IBaseService<PHB_PL32_P2_TT01>
    {
        
    }
    public class PhbPL32_P2_TT01Service:BaseService<PHB_PL32_P2_TT01>, IPhbPL32_P2_TT01Service
    {
        private readonly IRepositoryAsync<PHB_PL32_P2_TT01> _repository;
        public PhbPL32_P2_TT01Service(IRepositoryAsync<PHB_PL32_P2_TT01> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
