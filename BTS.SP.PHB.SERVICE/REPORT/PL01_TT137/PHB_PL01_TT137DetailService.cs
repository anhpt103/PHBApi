using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.PL01_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.PL01_TT137
{
    public interface IPHB_PL01_TT137DetailService : IBaseService<PHB_PL01_TT137_DETAIL>
    {
        
    }
    public class PHB_PL01_TT137DetailService : BaseService<PHB_PL01_TT137_DETAIL>, IPHB_PL01_TT137DetailService
    {
        private readonly IRepositoryAsync<PHB_PL01_TT137_DETAIL> _repository;
        public PHB_PL01_TT137DetailService(IRepositoryAsync<PHB_PL01_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
