using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.SYS.SysDvqhns
{
    public interface ISysDvqhnsService : IBaseService<SYS_DVQHNS>
    {
    }
    public class SysDvqhnsService : BaseService<SYS_DVQHNS>, ISysDvqhnsService
    {
        private readonly IRepositoryAsync<SYS_DVQHNS> _repository;

        public SysDvqhnsService(IRepositoryAsync<SYS_DVQHNS> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
