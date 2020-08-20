using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Helper;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.Models.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;

namespace BTS.SP.PHB.SERVICE.REPORT.Bm48TT342
{
    public interface IPhbBm48TT342Service : IBaseService<PHB_BM48_TT342>
    {
    }
    public class PhbBm48TT342Service : BaseService<PHB_BM48_TT342>, IPhbBm48TT342Service
    {
        private readonly IRepositoryAsync<PHB_BM48_TT342> _repository;
        public PhbBm48TT342Service(IRepositoryAsync<PHB_BM48_TT342> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
