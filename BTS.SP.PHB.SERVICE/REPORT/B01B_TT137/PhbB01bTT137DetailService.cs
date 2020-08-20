using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.B01B_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01B_TT137
{
    public interface IPhbB01bTT137DetailService : IBaseService<PHB_B01B_TT137_DETAIL>
    {
        
    }
    public class PhbB01bTT137DetailService : BaseService<PHB_B01B_TT137_DETAIL>, IPhbB01bTT137DetailService
    {
        private readonly IRepositoryAsync<PHB_B01B_TT137_DETAIL> _repository;
        public PhbB01bTT137DetailService(IRepositoryAsync<PHB_B01B_TT137_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
