using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.ENTITY.PBDT.TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.TT344
{
    public interface IPHB_PBDT_TT344_B04_Service : IBaseService<PHB_PBDT_TT344_B04>
    {
    }

    public class PHB_PBDT_TT344_B04_Service : BaseService<PHB_PBDT_TT344_B04>, IPHB_PBDT_TT344_B04_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_TT344_B04> _repository;
        public PHB_PBDT_TT344_B04_Service(IRepositoryAsync<PHB_PBDT_TT344_B04> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
