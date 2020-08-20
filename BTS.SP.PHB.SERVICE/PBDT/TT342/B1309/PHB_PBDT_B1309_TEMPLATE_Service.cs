using BTS.SP.PHB.ENTITY.PBDT.B1309;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1309
{
    public interface IPHB_PBDT_B1309_TEMPLATE_Service : IBaseService<PHB_PBDT_B1309_TEMPLATE>
    {
    }

    public class PHB_PBDT_B1309_TEMPLATE_Service : BaseService<PHB_PBDT_B1309_TEMPLATE>, IPHB_PBDT_B1309_TEMPLATE_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1309_TEMPLATE> _repository;
        public PHB_PBDT_B1309_TEMPLATE_Service(IRepositoryAsync<PHB_PBDT_B1309_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
