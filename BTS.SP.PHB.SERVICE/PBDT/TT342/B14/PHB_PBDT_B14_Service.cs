using BTS.SP.PHB.ENTITY.PBDT.B14;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.B14
{
    public interface IPHB_PBDT_B14_Service : IBaseService<PHB_PBDT_B14>
    {
    }

    public class PHB_PBDT_B14_Service : BaseService<PHB_PBDT_B14>, IPHB_PBDT_B14_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B14> _repository;
        public PHB_PBDT_B14_Service(IRepositoryAsync<PHB_PBDT_B14> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
