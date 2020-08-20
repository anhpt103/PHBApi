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

    public interface IPhbB02TT137TemplateService : IBaseService<PHB_B02_TT137_TEMPLATE>
    {

    }
    public class PhbB02TT137TemplateService : BaseService<PHB_B02_TT137_TEMPLATE>, IPhbB02TT137TemplateService
    {
        private readonly IRepositoryAsync<PHB_B02_TT137_TEMPLATE> _repository;
        public PhbB02TT137TemplateService(IRepositoryAsync<PHB_B02_TT137_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
