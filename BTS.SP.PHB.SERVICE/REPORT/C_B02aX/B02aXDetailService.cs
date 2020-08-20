using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B02aX
{
    public interface IB02aXDetailService : IBaseService<PHB_C_B02AX_DETAIL>
    {
        
    }
    public class B02aXDetailService : BaseService<PHB_C_B02AX_DETAIL>, IB02aXDetailService
    {
        private readonly IRepositoryAsync<PHB_C_B02AX_DETAIL> _repository;
        public B02aXDetailService(IRepositoryAsync<PHB_C_B02AX_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
