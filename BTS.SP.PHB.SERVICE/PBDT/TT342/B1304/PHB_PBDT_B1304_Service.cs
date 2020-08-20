using BTS.SP.PHB.ENTITY.PBDT.B1304;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1304
{
    public interface IPHB_PBDT_B1304_Service : IBaseService<PHB_PBDT_B1304>
    {
    }

    public class PHB_PBDT_B1304_Service : BaseService<PHB_PBDT_B1304>, IPHB_PBDT_B1304_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1304> _repository;
        public PHB_PBDT_B1304_Service(IRepositoryAsync<PHB_PBDT_B1304> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
