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
    public interface IPHB_PBDT_B1311_Service : IBaseService<PHB_PBDT_B1311>
    {
    }

    public class PHB_PBDT_B1311_Service : BaseService<PHB_PBDT_B1311>, IPHB_PBDT_B1311_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B1311> _repository;
        public PHB_PBDT_B1311_Service(IRepositoryAsync<PHB_PBDT_B1311> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
