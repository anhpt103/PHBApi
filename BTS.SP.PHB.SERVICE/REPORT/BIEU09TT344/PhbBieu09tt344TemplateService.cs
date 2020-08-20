using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU09TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU09TT344
{
    public interface IPhbBieu09tt344TemplateService : IBaseService<PHB_BIEU09TT344_TEMPLATE>
    { }
    public class PhbBieu09tt344TemplateService : BaseService<PHB_BIEU09TT344_TEMPLATE>, IPhbBieu09tt344TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU09TT344_TEMPLATE> _repository;

        public PhbBieu09tt344TemplateService(IRepositoryAsync<PHB_BIEU09TT344_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
