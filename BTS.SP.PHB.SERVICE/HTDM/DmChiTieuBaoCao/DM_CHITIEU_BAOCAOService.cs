using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmChiTieuBaoCao
{
        public interface IDM_CT_BAOCAOService : IBaseService<DM_CHITIEU_BAOCAO>
        {

        }
        public class DM_CT_BAOCAOService : BaseService<DM_CHITIEU_BAOCAO>, IDM_CT_BAOCAOService
    {
            private readonly IRepositoryAsync<DM_CHITIEU_BAOCAO> _repository;
            public DM_CT_BAOCAOService(IRepositoryAsync<DM_CHITIEU_BAOCAO> repository) : base(repository)
            {
                _repository = repository;
            }
        }
    }
