using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmLoaiNganSach
{
    public interface IDmLoaiNganSachService:IBaseService<PHB_DM_LOAINGANSACH>
    {
    }
    public class DmLoaiNganSachService: BaseService<PHB_DM_LOAINGANSACH>,IDmLoaiNganSachService
    {
        private readonly IRepositoryAsync<PHB_DM_LOAINGANSACH> _repository;

        public DmLoaiNganSachService(IRepositoryAsync<PHB_DM_LOAINGANSACH> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
