using BTS.SP.PHB.ENTITY.PBDT.B121;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B121
{
    public interface IPHB_PBDT_B121_Serviec : IBaseService<PHB_PBDT_B121>
    {

    }
    public class PHB_PBDT_B121_Service : BaseService<PHB_PBDT_B121>, IPHB_PBDT_B121_Serviec
    { 
        private readonly IRepositoryAsync<PHB_PBDT_B121> _repository;
        public PHB_PBDT_B121_Service(IRepositoryAsync<PHB_PBDT_B121> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
