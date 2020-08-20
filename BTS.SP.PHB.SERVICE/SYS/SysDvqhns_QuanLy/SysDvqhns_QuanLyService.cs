using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy
{
    public interface ISysDvqhns_QuanLyService : IBaseService<SYS_DVQHNS_QUANLY>
    {
    }
    public class SysDvqhns_QuanLyService : BaseService<SYS_DVQHNS_QUANLY>, ISysDvqhns_QuanLyService
    {
        private readonly IRepositoryAsync<SYS_DVQHNS_QUANLY> _repository;

        public SysDvqhns_QuanLyService(IRepositoryAsync<SYS_DVQHNS_QUANLY> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
