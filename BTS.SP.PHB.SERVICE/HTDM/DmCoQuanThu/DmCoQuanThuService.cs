using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.HTDM.DmCoQuanThu
{
    public interface IDmCoQuanThuService : IBaseService<PHB_DM_COQUANTHU>
    {

    }
    public class DmCoQuanThuService : BaseService<PHB_DM_COQUANTHU>, IDmCoQuanThuService
    {
        private readonly IRepositoryAsync<PHB_DM_COQUANTHU> _repository;
        public DmCoQuanThuService(IRepositoryAsync<PHB_DM_COQUANTHU> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
