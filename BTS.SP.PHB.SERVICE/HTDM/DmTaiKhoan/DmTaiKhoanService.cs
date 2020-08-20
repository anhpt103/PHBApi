using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmTaiKhoan
{
    public interface IDmTaiKhoanService:IBaseService<PHB_DM_TAIKHOAN>
    {
    }
    public class DmTaiKhoanService:BaseService<PHB_DM_TAIKHOAN>, IDmTaiKhoanService
    {
        private readonly IRepositoryAsync<PHB_DM_TAIKHOAN> _repository;

        public DmTaiKhoanService(IRepositoryAsync<PHB_DM_TAIKHOAN> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
