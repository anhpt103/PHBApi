using BTS.SP.PHB.ENTITY.PBDT.B1301;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1301
{
    public interface IPHB_PBDT_B1301_TEMPLATE_Service : IBaseService<PHB_PBDT_B1301_TEMPLATE>
    {
    }

    public class PHB_PBDT_B1301_TEMPLATE_Service : BaseService<PHB_PBDT_B1301_TEMPLATE>, IPHB_PBDT_B1301_TEMPLATE_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1301_TEMPLATE> _repository;
        public PHB_PBDT_B1301_TEMPLATE_Service(IRepositoryAsync<PHB_PBDT_B1301_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
