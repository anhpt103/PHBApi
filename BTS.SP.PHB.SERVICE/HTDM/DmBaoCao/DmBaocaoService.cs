using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmBaoCao
{
    public interface IDmBaocaoService:IBaseService<PHB_DM_BAOCAO>
    {

    }
    public class DmBaocaoService : BaseService<PHB_DM_BAOCAO>,IDmBaocaoService
    {
        private readonly IRepositoryAsync<PHB_DM_BAOCAO> _repository;
        public DmBaocaoService(IRepositoryAsync<PHB_DM_BAOCAO> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
