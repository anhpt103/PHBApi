using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.BM16_TT344
{
   
    public interface IPhbBm16TT344Service : IBaseService<PHB_BM16_TT344>
    {

    }
    public class PhbBm16TT344Service : BaseService<PHB_BM16_TT344>, IPhbBm16TT344Service
    {
        private readonly IRepositoryAsync<PHB_BM16_TT344> _repository;
        public PhbBm16TT344Service(IRepositoryAsync<PHB_BM16_TT344> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
