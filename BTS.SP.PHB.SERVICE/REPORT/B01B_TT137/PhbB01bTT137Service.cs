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
using BTS.SP.PHB.ENTITY.Rp.B01B_TT137;

namespace BTS.SP.PHB.SERVICE.REPORT.B01B_TT137
{
    public interface IPhbB01bTT137Service : IBaseService<PHB_B01B_TT137>
    {
    }
    public class PhbB01bTT137Service : BaseService<PHB_B01B_TT137>, IPhbB01bTT137Service
    {
        private readonly IRepositoryAsync<PHB_B01B_TT137> _repository;
        public PhbB01bTT137Service(IRepositoryAsync<PHB_B01B_TT137> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
