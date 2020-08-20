using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01A_TT137
{
    public interface IPhbB01aTT137TemplateService : IBaseService<PHB_B01A_TT137_TEMPLATE>
    {
        
    }
    public class PhbB01aTT137TemplateService : BaseService<PHB_B01A_TT137_TEMPLATE>, IPhbB01aTT137TemplateService
    {
        private readonly IRepositoryAsync<PHB_B01A_TT137_TEMPLATE> _repository;
        public PhbB01aTT137TemplateService(IRepositoryAsync<PHB_B01A_TT137_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
