using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmHoatDong
{
    public interface IDmHoatDongService:IBaseService<PHB_DM_HOATDONG>
    {
    }
    public class DmHoatDongService: BaseService<PHB_DM_HOATDONG>,IDmHoatDongService
    {
        private readonly IRepositoryAsync<PHB_DM_HOATDONG> _repository;

        public DmHoatDongService(IRepositoryAsync<PHB_DM_HOATDONG> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
