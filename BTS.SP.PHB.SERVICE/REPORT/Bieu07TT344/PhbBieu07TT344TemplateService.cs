using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU07TT344;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.SERVICE.SERVICES;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu07TT344
{
    public interface IPhbBieu07TT344TemplateService : IBaseService<PHB_BIEU07TT344_TEMPLATE>
    {

    }
    public class PhbBieu07TT344TemplateService : BaseService<PHB_BIEU07TT344_TEMPLATE>, IPhbBieu07TT344TemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU07TT344_TEMPLATE> _repository;
        public PhbBieu07TT344TemplateService(IRepositoryAsync<PHB_BIEU07TT344_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
