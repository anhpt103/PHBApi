using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107
{
    public interface IBc04BCTCTT107TemplateService : IBaseService<BC04_BCTC_TT107_TEMPLATE>
    {

    }

    public class Bc04BCTCTT107TemplateService : BaseService<BC04_BCTC_TT107_TEMPLATE>, IBc04BCTCTT107TemplateService
    {
        private readonly IRepositoryAsync<BC04_BCTC_TT107_TEMPLATE> _repository;

        public Bc04BCTCTT107TemplateService(IRepositoryAsync<BC04_BCTC_TT107_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
