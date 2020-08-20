using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BTS.SP.PHB.SERVICE.SYS.SysScheduler
{
    public interface ISysSchedulerService : IBaseService<SYS_SCHEDULER>
    {
        string GetScheduler(out List<SYS_SCHEDULER> outListScheduler);
    }
    public class SysSchedulerService : BaseService<SYS_SCHEDULER>, ISysSchedulerService
    {
        private readonly IRepositoryAsync<SYS_SCHEDULER> _repository;
        public SysSchedulerService(IRepositoryAsync<SYS_SCHEDULER> repository) : base(repository)
        {
            _repository = repository;
        }

        public string GetScheduler(out List<SYS_SCHEDULER> outListScheduler)
        {
            string msg = "";
            outListScheduler = new List<SYS_SCHEDULER>();
            try
            {
                outListScheduler = _repository.Queryable().ToList();
            }
            catch (Exception ex)
            {

                msg = ex.Message;
            }

            return msg;
        }
    }
}
