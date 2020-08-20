using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.SERVICES;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu08TT344
{
    public interface IPhbBieu08TT344TemplateService : IBaseService<PHB_BIEU08TT344_TEMPLATE>
    {

    }
    public class PhbBieu08TT344TemplateService : BaseService<PHB_BIEU08TT344_TEMPLATE>, IPhbBieu08TT344TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU08TT344_TEMPLATE> _repository;
        public PhbBieu08TT344TemplateService(IRepositoryAsync<PHB_BIEU08TT344_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
