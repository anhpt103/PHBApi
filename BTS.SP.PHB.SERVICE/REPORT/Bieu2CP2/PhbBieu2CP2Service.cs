using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU2CP2
{
    public interface IPhbBIEU2CP2Service : IBaseService<PHB_BIEU2CP2>
    {
        
    }
    public class PhbBIEU2CP2Service:BaseService<PHB_BIEU2CP2>, IPhbBIEU2CP2Service {
        private readonly IRepositoryAsync<PHB_BIEU2CP2> _repository;

        public PhbBIEU2CP2Service(IRepositoryAsync<PHB_BIEU2CP2> repository) : base(repository)
        {
            _repository = repository;
        }

      
    }
}
