using BTS.SP.PHB.ENTITY.Rp.C_B04X;
using BTS.SP.PHB.SERVICE.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;

namespace BTS.SP.PHB.SERVICE.REPORT.C_B04X
{
    public interface IPhbBieuC_B04XDetail_TSCDService : IBaseService<PHB_C_B04X_DETAIL_TSCD> { }

    public class PhbBieuC_B04XDetail_TSCDService : BaseService<PHB_C_B04X_DETAIL_TSCD>, IPhbBieuC_B04XDetail_TSCDService
    {
        private readonly IRepositoryAsync<PHB_C_B04X_DETAIL_TSCD> _repository;
        public PhbBieuC_B04XDetail_TSCDService(IRepositoryAsync<PHB_C_B04X_DETAIL_TSCD> repository) : base(repository)
        {
            _repository = repository;   
        }
    }
}
