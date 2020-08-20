using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmCanBo
{
    public interface IDmCanBoService : IBaseService<PHB_DM_CANBO>
    {

    }
    public class DmCanBoService : BaseService<PHB_DM_CANBO>, IDmCanBoService
    {
        private readonly IRepositoryAsync<PHB_DM_CANBO> _repository;
        public DmCanBoService(IRepositoryAsync<PHB_DM_CANBO> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}

