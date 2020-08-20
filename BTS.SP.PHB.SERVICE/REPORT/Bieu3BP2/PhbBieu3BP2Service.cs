using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.Bieu3BP2
{
    public interface IPhbBieu3BP2Service : IBaseService<PHB_BIEU3BP2>
    {
        
    }
    public class PhbBieu3BP2Service:BaseService<PHB_BIEU3BP2>, IPhbBieu3BP2Service
    {
        private readonly IRepositoryAsync<PHB_BIEU3BP2> _repository;
        public PhbBieu3BP2Service(IRepositoryAsync<PHB_BIEU3BP2> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
