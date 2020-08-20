using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.DoiChieuTABMIS;
using BTS.SP.PHB.SERVICE.Models.DoiChieuTABMIS;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.DoiChieuTABMIS
{
    public interface IDoiChieuTABMISService : IBaseService<PHC_DOICHIEUSOLIEUHEADER>
    {
    }
    public class DoiChieuTABMISService : BaseService<PHC_DOICHIEUSOLIEUHEADER>, IDoiChieuTABMISService
    {

        private readonly IRepositoryAsync<PHC_DOICHIEUSOLIEUHEADER> _repository;

        public DoiChieuTABMISService(IRepositoryAsync<PHC_DOICHIEUSOLIEUHEADER> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
