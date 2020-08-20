using System;
using System.Collections.Generic;
using System.Linq;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS
{
    public interface IDmDVQHNSService:IBaseService<PHB_DM_DVQHNS>
    {
    }
    public class DmDVQHNSService : BaseService<PHB_DM_DVQHNS>,IDmDVQHNSService
    {
        private readonly IRepositoryAsync<PHB_DM_DVQHNS> _repository;

        public DmDVQHNSService(IRepositoryAsync<PHB_DM_DVQHNS> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
