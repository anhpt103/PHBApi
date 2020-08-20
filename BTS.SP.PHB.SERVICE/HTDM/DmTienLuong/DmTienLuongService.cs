using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTienLuong
{
    public interface IDmTienLuongService : IBaseService<PHB_DM_TIENLUONG>
    {

    }
    public class DmTienLuongService : BaseService<PHB_DM_TIENLUONG>, IDmTienLuongService
    {
        private readonly IRepositoryAsync<PHB_DM_TIENLUONG> _repository;
        public DmTienLuongService(IRepositoryAsync<PHB_DM_TIENLUONG> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
