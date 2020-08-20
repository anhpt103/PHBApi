using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmNhomMucChi
{
    public interface IDmNhomMucChiService : IBaseService<PHB_DM_NHOMMUCCHI>
    {
    }
    public class DmNhomMucChiService: BaseService<PHB_DM_NHOMMUCCHI>, IDmNhomMucChiService
    {
        private readonly IRepositoryAsync<PHB_DM_NHOMMUCCHI> _repository;

        public DmNhomMucChiService(IRepositoryAsync<PHB_DM_NHOMMUCCHI> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
