using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B03_TT90
{
    public interface IPhbB03_TT90TemplateService : IBaseService<PHB_B03_TT90_TEMPLATE>
    {

    }
    public class PHB_B03_TT90_TEMPLATEService : BaseService<PHB_B03_TT90_TEMPLATE>, IPhbB03_TT90TemplateService
    {
        private readonly IRepositoryAsync<PHB_B03_TT90_TEMPLATE> _repository;
        public PHB_B03_TT90_TEMPLATEService(IRepositoryAsync<PHB_B03_TT90_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
