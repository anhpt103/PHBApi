using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;

namespace BTS.SP.PHB.SERVICE.REPORT.BIEU01C
{
    public interface IPhbBIEU01CTemplateService:IBaseService<PHB_BIEU01C_TEMPLATE>
    {
        
    }
    public class PhbBIEU01CTemplateService:BaseService<PHB_BIEU01C_TEMPLATE>, IPhbBIEU01CTemplateService
    {
        private readonly IRepositoryAsync<PHB_BIEU01C_TEMPLATE> _repository;
        public PhbBIEU01CTemplateService(IRepositoryAsync<PHB_BIEU01C_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
