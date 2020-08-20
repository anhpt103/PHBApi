using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;

namespace BTS.SP.PHB.SERVICE.REPORT.B01BCQT
{
    public interface IPhbB01BCQTTemplateService:IBaseService<PHB_B01BCQT_TEMPLATE>
    {
        
    }
    public class PhbB01BCQTTemplateService:BaseService<PHB_B01BCQT_TEMPLATE>, IPhbB01BCQTTemplateService
    {
        private readonly IRepositoryAsync<PHB_B01BCQT_TEMPLATE> _repository;
        public PhbB01BCQTTemplateService(IRepositoryAsync<PHB_B01BCQT_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
