
using BTS.SP.PHB.SERVICE.SERVICES;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BTS.SP.PHB.ENTITY.Rp.NHAP_DT_XA;

namespace BTS.SP.PHB.SERVICE.REPORT.NHAP_DT_XA
{
    public interface IBC_NHAPDTXA_DETAILService : IBaseService<BC_NHAPDT_XA_DETAIL>
    {
    }

    public class BC_NHAPDTXA_DETAILService : BaseService<BC_NHAPDT_XA_DETAIL>, IBC_NHAPDTXA_DETAILService
    {
        private readonly IRepositoryAsync<BC_NHAPDT_XA_DETAIL> _repository;
        public BC_NHAPDTXA_DETAILService(IRepositoryAsync<BC_NHAPDT_XA_DETAIL> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
