using BTS.SP.PHB.ENTITY.Rp.C_B03C_X;
using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B03C_X
{
    public interface IPhb_C_B03C_XDetailService : IBaseService<PHB_C_B03C_X_DETAIL>
    {

    }
    public class Phb_C_B03C_XDetailService : BaseService<PHB_C_B03C_X_DETAIL>, IPhb_C_B03C_XDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B03C_X_DETAIL> _repository;
        public Phb_C_B03C_XDetailService(IRepositoryAsync<PHB_C_B03C_X_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
