using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDBHC
{
    public interface IDmDBHCService : IBaseService<DM_DBHC>
    {
        
    }
    public class DmDBHCService : BaseService<DM_DBHC>, IDmDBHCService
    {
        private readonly IRepositoryAsync<DM_DBHC> _repository;

        public DmDBHCService(IRepositoryAsync<DM_DBHC> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
