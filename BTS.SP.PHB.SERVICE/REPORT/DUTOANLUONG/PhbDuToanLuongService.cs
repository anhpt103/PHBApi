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
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG;

namespace BTS.SP.PHB.SERVICE.REPORT.DUTOANLUONG
{
    public interface IPhbDuToanLuongService : IBaseService<PHB_DUTOANLUONG>
    {
    }
    public class PhbDuToanLuongService : BaseService<PHB_DUTOANLUONG>, IPhbDuToanLuongService
    {
        private readonly IRepositoryAsync<PHB_DUTOANLUONG> _repository;
        public PhbDuToanLuongService(IRepositoryAsync<PHB_DUTOANLUONG> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
