using BTS.SP.PHB.ENTITY.PBDT.B125;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.PBDT.TT342.B125
{
    public interface IPHB_PBDT_B125_Service : IBaseService<PHB_PBDT_B125>
    {

    }
    public class PHB_PBDT_B125_Service : BaseService<PHB_PBDT_B125>, IPHB_PBDT_B125_Service
    {
        private readonly IRepositoryAsync<PHB_PBDT_B125> _repository;
        public PHB_PBDT_B125_Service(IRepositoryAsync<PHB_PBDT_B125> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
