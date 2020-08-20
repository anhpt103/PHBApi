using BTS.SP.PHB.ENTITY.PBDT.B32;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B32
{
    public interface IPHB_PBDT_B32_Service : IBaseService<PHB_PBDT_B32>
    {
    }

    public class PHB_PBDT_B32_Service : BaseService<PHB_PBDT_B32>, IPHB_PBDT_B32_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B32> _repository;
        public PHB_PBDT_B32_Service(IRepositoryAsync<PHB_PBDT_B32> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
