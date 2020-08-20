using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BCTC_TH_TEMPLATE;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BCTC_TH_TEMPLATE
{
    public interface IPha_bctc_b02_bctc_th_template_Service : IBaseService<PHA_BCTC_B02_BCTC_TH_TEMPLATE>
    {
    }

    public class Pha_bctc_b02_bctc_th_template_Service : BaseService<PHA_BCTC_B02_BCTC_TH_TEMPLATE>, IPha_bctc_b02_bctc_th_template_Service
    {
        private readonly IRepositoryAsync<PHA_BCTC_B02_BCTC_TH_TEMPLATE> _repository;

        public Pha_bctc_b02_bctc_th_template_Service(IRepositoryAsync<PHA_BCTC_B02_BCTC_TH_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
