using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Services;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungNhomQuyen
{
    public interface IAuNguoiDungNhomQuyenService : IDataInfoService<AU_NGUOIDUNG_NHOMQUYEN>
    {
    }
    public class AuNguoiDungNhomQuyenService : DataInfoServiceBase<AU_NGUOIDUNG_NHOMQUYEN>, IAuNguoiDungNhomQuyenService
    {
        public AuNguoiDungNhomQuyenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

    }
}
