using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNoiDungKt
{
    public interface IDmNoiDungKtService : IBaseService<PHB_DM_NOIDUNGKT>
    {
    }
    public class DmNoiDungKtService: BaseService<PHB_DM_NOIDUNGKT>, IDmNoiDungKtService
    {
        private readonly IRepositoryAsync<PHB_DM_NOIDUNGKT> _repository;

        public DmNoiDungKtService(IRepositoryAsync<PHB_DM_NOIDUNGKT> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
