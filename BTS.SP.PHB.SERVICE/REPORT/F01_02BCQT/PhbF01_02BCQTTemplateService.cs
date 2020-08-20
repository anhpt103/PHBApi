using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;

namespace BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT
{
    public interface IPhbF01_02BCQTTemplateService:IBaseService<PHB_F01_02BCQT_TEMPLATE>
    {
        
    }
    public class PhbF01_02BCQTTemplateService:BaseService<PHB_F01_02BCQT_TEMPLATE>, IPhbF01_02BCQTTemplateService
    {
        private readonly IRepositoryAsync<PHB_F01_02BCQT_TEMPLATE> _repository;
        public PhbF01_02BCQTTemplateService(IRepositoryAsync<PHB_F01_02BCQT_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
