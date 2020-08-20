using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.PL01_TT137;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.PL01_TT137
{
    public interface IPHB_PL01_TT137Service : IBaseService<PHB_PL01_TT137>
    {
    }
    public class PHB_PL01_TT137Service : BaseService<PHB_PL01_TT137>, IPHB_PL01_TT137Service
    {
        private readonly IRepositoryAsync<PHB_PL01_TT137> _repository;
        public PHB_PL01_TT137Service(IRepositoryAsync<PHB_PL01_TT137> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
