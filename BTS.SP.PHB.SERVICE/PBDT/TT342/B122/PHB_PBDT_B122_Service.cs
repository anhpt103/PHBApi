using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.PBDT.B122;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;



namespace BTS.SP.PHB.SERVICE.PBDT.B122
{
    public interface IPHB_PBDT_B122_Service : IBaseService<PHB_PBDT_B122>
    {

    }
    public class PHB_PBDT_B122_Service : BaseService<PHB_PBDT_B122>, IPHB_PBDT_B122_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B122> _repository;
        public PHB_PBDT_B122_Service(IRepositoryAsync<PHB_PBDT_B122> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
