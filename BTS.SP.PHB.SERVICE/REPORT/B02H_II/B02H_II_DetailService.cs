using BTS.SP.PHB.ENTITY.Rp.B02H_II;
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.REPORT.B02H_II
{
    public interface IB02H_IIDetailService : IBaseService<PHB_B02H_II_DETAIL>
    {

    }
    public class B02H_IIDetailService : BaseService<PHB_B02H_II_DETAIL>, IB02H_IIDetailService
    {
        private readonly IRepositoryAsync<PHB_B02H_II_DETAIL> _repository;
        public B02H_IIDetailService(IRepositoryAsync<PHB_B02H_II_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
