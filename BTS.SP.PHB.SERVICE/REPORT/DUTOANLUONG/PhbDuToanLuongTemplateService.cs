using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG;

namespace BTS.SP.PHB.SERVICE.REPORT.DUTOANLUONG
{
    public interface IPhbDuToanLuongTemplateService : IBaseService<PHB_DUTOANLUONG_TEMPLATE>
    {
        
    }
    public class PhbDuToanLuongTemplateService : BaseService<PHB_DUTOANLUONG_TEMPLATE>, IPhbDuToanLuongTemplateService
    {
        private readonly IRepositoryAsync<PHB_DUTOANLUONG_TEMPLATE> _repository;
        public PhbDuToanLuongTemplateService(IRepositoryAsync<PHB_DUTOANLUONG_TEMPLATE> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
