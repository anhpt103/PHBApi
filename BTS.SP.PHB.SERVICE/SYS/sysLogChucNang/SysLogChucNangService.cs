using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.SYS.sysLogChucNang
{
    public interface ISysLogChucNangService : IBaseService<PHB_SYS_LOG_CHUCNANG>
    {
    }
    public class SysLogChucNangService : BaseService<PHB_SYS_LOG_CHUCNANG>, ISysLogChucNangService
    {
        private readonly IRepositoryAsync<PHB_SYS_LOG_CHUCNANG> _repository;

        public SysLogChucNangService(IRepositoryAsync<PHB_SYS_LOG_CHUCNANG> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
