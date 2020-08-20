using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.QLDT
{
    public interface IPHB_PBDT_QLDT_B01_Service : IBaseService<PHB_PBDT_QLDT_B01>
    {
    }

    public class PHB_PBDT_QLDT_B01_Service : BaseService<PHB_PBDT_QLDT_B01>, IPHB_PBDT_QLDT_B01_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_QLDT_B01> _repository;
        public PHB_PBDT_QLDT_B01_Service(IRepositoryAsync<PHB_PBDT_QLDT_B01> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
