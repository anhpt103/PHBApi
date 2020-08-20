using BTS.SP.PHB.ENTITY.Rp.B02_TT137;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B02_TT137
{
    public interface IPhbB02TT137Service : IBaseService<PHB_B02_TT137>
    {

    }
    public class PhbB02TT137Service : BaseService<PHB_B02_TT137>, IPhbB02TT137Service
    {
        private readonly IRepositoryAsync<PHB_B02_TT137> _repository;
        public PhbB02TT137Service(IRepositoryAsync<PHB_B02_TT137> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
