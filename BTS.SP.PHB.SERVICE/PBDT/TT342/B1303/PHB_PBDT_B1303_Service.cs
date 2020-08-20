using BTS.SP.PHB.ENTITY.PBDT.B1303;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B1303
{
    public interface IPHB_PBDT_B1303_Service : IBaseService<PHB_PBDT_B1303>
    {
    }

    public class PHB_PBDT_B1303_Service : BaseService<PHB_PBDT_B1303>, IPHB_PBDT_B1303_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1303> _repository;
        public PHB_PBDT_B1303_Service(IRepositoryAsync<PHB_PBDT_B1303> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
