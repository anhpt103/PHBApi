using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.C_B02B_X;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B02B_X
{
    public interface IC_B02B_XDetailService : IBaseService<PHB_C_B02B_X_DETAIL>
    {

    }
    public class C_B02B_XDetailService : BaseService<PHB_C_B02B_X_DETAIL>, IC_B02B_XDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B02B_X_DETAIL> _repository;
        public C_B02B_XDetailService(IRepositoryAsync<PHB_C_B02B_X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
