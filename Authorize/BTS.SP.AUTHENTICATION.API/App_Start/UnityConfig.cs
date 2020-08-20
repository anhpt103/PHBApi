using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDung;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungNhomQuyen;
using BTS.SP.AUTHENTICATION.API.Au.AuNguoiDungQuyen;
using BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyen;
using BTS.SP.AUTHENTICATION.API.Au.AuNhomQuyenChucNang;
using BTS.SP.AUTHENTICATION.API.Entities;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.SysChucNang;
using BTS.SP.AUTHENTICATION.API.Dm.Entities;
using BTS.SP.AUTHENTICATION.API.ServiceFunc.DmDBHC;

namespace BTS.SP.AUTHENTICATION.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            container.RegisterType<IDataContext, AuthContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IRepository<AU_NGUOIDUNG>, Repository<AU_NGUOIDUNG>>();
            container.RegisterType<IAuNguoiDungService, AuNguoiDungService>();

            container.RegisterType<IRepository<AU_NHOMQUYEN>, Repository<AU_NHOMQUYEN>>();
            container.RegisterType<IAuNhomQuyenService, AuNhomQuyenService>();

            container.RegisterType<IRepository<SYS_CHUCNANG>, Repository<SYS_CHUCNANG>>();
            container.RegisterType<ISysChucNangService, SysChucNangService>();

            container.RegisterType<IRepository<AU_NHOMQUYEN_CHUCNANG>, Repository<AU_NHOMQUYEN_CHUCNANG>>();
            container.RegisterType<IAuNhomQuyenChucNangService, AuNhomQuyenChucNangService>();

            container.RegisterType<IRepository<AU_NGUOIDUNG_NHOMQUYEN>, Repository<AU_NGUOIDUNG_NHOMQUYEN>>();
            container.RegisterType<IAuNguoiDungNhomQuyenService, AuNguoiDungNhomQuyenService>();

            container.RegisterType<IRepository<AU_NGUOIDUNG_QUYEN>, Repository<AU_NGUOIDUNG_QUYEN>>();
            container.RegisterType<IAuNguoiDungQuyenService, AuNguoiDungQuyenService>();

            container.RegisterType<IRepository<LogSignin>, Repository<LogSignin>>();

            container.RegisterType<IRepository<DM_DBHC>, Repository<DM_DBHC>>();
            container.RegisterType<IDM_DBHCService, DM_DBHCService>();

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}