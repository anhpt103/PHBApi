using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.ENTITY.Rp.PHB.B02BCQT;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.B02BCQT
{
    public interface IPhbB02BcqtDetailService : IBaseService<PHB_B02BCQT_DETAIL>
    { }
    public class PhbB02BcqtDetailService : BaseService<PHB_B02BCQT_DETAIL>, IPhbB02BcqtDetailService
    {
        private readonly IRepositoryAsync<PHB_B02BCQT_DETAIL> _repository;

        public PhbB02BcqtDetailService(IRepositoryAsync<PHB_B02BCQT_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
