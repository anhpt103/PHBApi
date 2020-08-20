using BTS.SP.PHB.ENTITY.PBDT.B124;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.TT342.B124
{
    public interface IPHB_PBDT_B124_Service : IBaseService<PHB_PBDT_B124>
    {

    }
    public class PHB_PBDT_B124_Service : BaseService<PHB_PBDT_B124>, IPHB_PBDT_B124_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B124> _repository;
        public PHB_PBDT_B124_Service(IRepositoryAsync<PHB_PBDT_B124> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
