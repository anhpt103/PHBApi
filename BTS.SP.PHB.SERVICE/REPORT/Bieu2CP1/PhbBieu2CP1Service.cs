using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP1;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1
{
    public interface IPhbBIEU2CP1Service : IBaseService<PHB_BIEU2CP1>
    {
        
    }
    public class PhbBIEU2CP1Service:BaseService<PHB_BIEU2CP1>, IPhbBIEU2CP1Service {
        private readonly IRepositoryAsync<PHB_BIEU2CP1> _repository;

        public PhbBIEU2CP1Service(IRepositoryAsync<PHB_BIEU2CP1> repository) : base(repository)
        {
            _repository = repository;
        }

      
    }
}
