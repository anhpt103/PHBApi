using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNguonNganSach
{
    public interface IDmNguonNganSachService:IBaseService<PHB_DM_NGUONNGANSACH>
    {
    }
    public class DmNguonNganSachService:BaseService<PHB_DM_NGUONNGANSACH>, IDmNguonNganSachService
    {
        private readonly IRepositoryAsync<PHB_DM_NGUONNGANSACH> _repository;
        public DmNguonNganSachService(IRepositoryAsync<PHB_DM_NGUONNGANSACH> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
