using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmDuAn
{
    public interface IDmDuAnService:IBaseService<PHB_DM_DUAN>
    {

    }
    public class DmDuAnService: BaseService<PHB_DM_DUAN>,IDmDuAnService
    {
        private readonly IRepositoryAsync<PHB_DM_DUAN> _repository;
        public DmDuAnService(IRepositoryAsync<PHB_DM_DUAN> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
