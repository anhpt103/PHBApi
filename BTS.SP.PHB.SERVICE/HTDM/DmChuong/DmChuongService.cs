using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmChuong
{
    public interface IDmChuongService: IBaseService<DM_CHUONG>
    {
    }
    public class DmChuongService : BaseService<DM_CHUONG>,IDmChuongService
    {
        private readonly IRepositoryAsync<DM_CHUONG> _repository;
        public DmChuongService(IRepositoryAsync<DM_CHUONG> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
