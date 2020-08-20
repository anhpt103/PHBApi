using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;

namespace BTS.SP.PHB.SERVICE.REPORT.Bm48TT342
{
    public interface IPhbBm48TT342TemplateService : IBaseService<PHB_BM48_TT342_TEMPLATE>
    {
        
    }
    public class PhbBm48TT342TemplateService : BaseService<PHB_BM48_TT342_TEMPLATE>, IPhbBm48TT342TemplateService
    {
        private readonly IRepositoryAsync<PHB_BM48_TT342_TEMPLATE> _repository;
        public PhbBm48TT342TemplateService(IRepositoryAsync<PHB_BM48_TT342_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
