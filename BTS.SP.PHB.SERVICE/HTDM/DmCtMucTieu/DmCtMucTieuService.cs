using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmCtMucTieu
{
    public interface IDmCtMucTieuService : IBaseService<DM_CTMUCTIEU>
    {
        
    }
    public class DmCtMucTieuService:BaseService<DM_CTMUCTIEU>, IDmCtMucTieuService
    {
        private readonly IRepositoryAsync<DM_CTMUCTIEU> _repository;
        public DmCtMucTieuService(IRepositoryAsync<DM_CTMUCTIEU> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
