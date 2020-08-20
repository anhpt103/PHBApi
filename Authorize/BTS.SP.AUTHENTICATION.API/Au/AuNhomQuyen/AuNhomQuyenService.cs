using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using BTS.SP.AUTHENTICATION.API;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyenChucNang;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.Services;
using Oracle.ManagedDataAccess.Client;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyen
{
    public interface IAuNhomQuyenService : IDataInfoService<AU_NHOMQUYEN>
    {
    }
    public class AuNhomQuyenService : DataInfoServiceBase<AU_NHOMQUYEN>, IAuNhomQuyenService
    {
        public AuNhomQuyenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override Expression<Func<AU_NHOMQUYEN, bool>> GetKeyFilter(AU_NHOMQUYEN instance)
        {
            return x => x.MANHOMQUYEN == instance.MANHOMQUYEN;
        }
    }
}
