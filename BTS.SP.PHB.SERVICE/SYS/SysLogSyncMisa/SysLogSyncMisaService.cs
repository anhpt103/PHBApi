using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;

namespace BTS.SP.PHB.SERVICE.SYS.SysLogSyncMisa
{
    public interface ISysLogSyncMisaService : IBaseService<SYS_LOG_SYNC_MISA>
    {
        void WriteLogDatabase(SYS_LOG_SYNC_MISA log);
    }
    public class SysLogSyncMisaService : BaseService<SYS_LOG_SYNC_MISA>, ISysLogSyncMisaService
    {
        private readonly IRepositoryAsync<SYS_LOG_SYNC_MISA> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public SysLogSyncMisaService(IRepositoryAsync<SYS_LOG_SYNC_MISA> repository, IUnitOfWorkAsync unitOfWork) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void WriteLogDatabase(SYS_LOG_SYNC_MISA log)
        {
            try
            {
                _repository.Insert(log);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                WriteLogs.LogError(ex);
            }
        }
    }
}
