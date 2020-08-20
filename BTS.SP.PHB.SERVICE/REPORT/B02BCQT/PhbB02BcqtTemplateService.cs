using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B02BCQT
{
    public interface IPhbB02BcqtTemplateService : IBaseService<PHB_B02BCQT_TEMPLATE>
    { }
    public class PhbB02BcqtTemplateService : BaseService<PHB_B02BCQT_TEMPLATE>, IPhbB02BcqtTemplateService
    {
        private readonly IRepositoryAsync<PHB_B02BCQT_TEMPLATE> _repository;

        public PhbB02BcqtTemplateService(IRepositoryAsync<PHB_B02BCQT_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
