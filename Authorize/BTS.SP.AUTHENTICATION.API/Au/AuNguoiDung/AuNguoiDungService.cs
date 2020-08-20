using System;
using System.Collections.Generic;
using System.Linq;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.Helper;
using BTS.SP.AUTHENTICATION.API.Services;

namespace BTS.SP.AUTHENTICATION.API.Au.AuNguoiDung
{
    public interface IAuNguoiDungService : IDataInfoService<AU_NGUOIDUNG>
    {
        AU_NGUOIDUNG FindUser(string username, string password);
    }
    public class AuNguoiDungService: DataInfoServiceBase<AU_NGUOIDUNG>, IAuNguoiDungService
    {
        public AuNguoiDungService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public AU_NGUOIDUNG FindUser(string username, string password)
        {
            try
            {
                password = MD5Encrypt.MD5Hash(password);
                var b = UnitOfWork.Repository<AU_NGUOIDUNG>().DbSet.ToList();
                var a =  UnitOfWork.Repository<AU_NGUOIDUNG>().DbSet.FirstOrDefault(x =>
                    x.USERNAME.Equals(username) && x.PASSWORD.Equals(password));
                return a;
            }
            catch (Exception ex)
            {
                AU_NGUOIDUNG a = new AU_NGUOIDUNG();
                a.USERNAME = ex.ToString();
                a.MA_DONVI = "ERROR";
                return a;
            }
        }
    }
}
