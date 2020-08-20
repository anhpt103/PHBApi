using BTS.SP.PHB.ENTITY.PBDT.B123;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B123
{
    public interface IPHB_PBDT_B123_Service : IBaseService<PHB_PBDT_B123>
    {

    }
    public class PHB_PBDT_B123_Service : BaseService<PHB_PBDT_B123>, IPHB_PBDT_B123_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B123> _repository;
        public PHB_PBDT_B123_Service(IRepositoryAsync<PHB_PBDT_B123> repository): base(repository)
        {
            _repository = repository;
        }
    }
}
