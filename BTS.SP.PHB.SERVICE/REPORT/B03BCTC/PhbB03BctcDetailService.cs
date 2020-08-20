using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B03BBCTC;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B03BCTC
{
    public interface IPhbB03BctcDetailService : IBaseService<PHB_B03BBCTC_DETAIL>
    {
        
    }
    public class PhbB03BctcDetailService : BaseService<PHB_B03BBCTC_DETAIL>, IPhbB03BctcDetailService
    {
        private readonly IRepositoryAsync<PHB_B03BBCTC_DETAIL> _repository;
        public PhbB03BctcDetailService(IRepositoryAsync<PHB_B03BBCTC_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
