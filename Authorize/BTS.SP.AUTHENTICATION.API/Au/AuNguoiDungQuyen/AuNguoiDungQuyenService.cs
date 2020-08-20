using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Services;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungQuyen
{
    public interface IAuNguoiDungQuyenService : IDataInfoService<AU_NGUOIDUNG_QUYEN>
    {
        
    }
    public class AuNguoiDungQuyenService: DataInfoServiceBase<AU_NGUOIDUNG_QUYEN>, IAuNguoiDungQuyenService
    {
        public AuNguoiDungQuyenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
