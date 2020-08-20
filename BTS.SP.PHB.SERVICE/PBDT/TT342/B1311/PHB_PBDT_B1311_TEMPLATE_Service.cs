using BTS.SP.PHB.ENTITY.PBDT.B1311;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1311
{
    public interface IPHB_PBDT_B1311_TEMPLATE_Service : IBaseService<PHB_PBDT_B1311_TEMPLATE>
    {
    }

    public class PHB_PBDT_B1311_TEMPLATE_Service : BaseService<PHB_PBDT_B1311_TEMPLATE>, IPHB_PBDT_B1311_TEMPLATE_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1311_TEMPLATE> _repository;
        public PHB_PBDT_B1311_TEMPLATE_Service(IRepositoryAsync<PHB_PBDT_B1311_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
