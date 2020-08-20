using BTS.SP.API.PHB.Services;
using BTS.SP.API.PHB.Utils;
using BTS.SP.PHB.ENTITY;
using BTS.SP.PHB.ENTITY.Auth;
using BTS.SP.PHB.ENTITY.Dm;
using BTS.SP.PHB.ENTITY.PBDT.B05;
using BTS.SP.PHB.ENTITY.PBDT.B06;
using BTS.SP.PHB.ENTITY.PBDT.B07;
using BTS.SP.PHB.ENTITY.PBDT.B111;
using BTS.SP.PHB.ENTITY.PBDT.B121;
using BTS.SP.PHB.ENTITY.PBDT.B122;
using BTS.SP.PHB.ENTITY.PBDT.B123;
using BTS.SP.PHB.ENTITY.PBDT.B124;
using BTS.SP.PHB.ENTITY.PBDT.B125;
using BTS.SP.PHB.ENTITY.PBDT.B1301;
using BTS.SP.PHB.ENTITY.PBDT.B1302;
using BTS.SP.PHB.ENTITY.PBDT.B1303;
using BTS.SP.PHB.ENTITY.PBDT.B1304;
using BTS.SP.PHB.ENTITY.PBDT.B1305;
using BTS.SP.PHB.ENTITY.PBDT.B1306;
using BTS.SP.PHB.ENTITY.PBDT.B1307;
using BTS.SP.PHB.ENTITY.PBDT.B1308;
using BTS.SP.PHB.ENTITY.PBDT.B1309;
using BTS.SP.PHB.ENTITY.PBDT.B1310;
using BTS.SP.PHB.ENTITY.PBDT.B1311;
using BTS.SP.PHB.ENTITY.PBDT.B1312;
using BTS.SP.PHB.ENTITY.PBDT.B14;
using BTS.SP.PHB.ENTITY.PBDT.B1501;
using BTS.SP.PHB.ENTITY.PBDT.B1502;
using BTS.SP.PHB.ENTITY.PBDT.B32;
using BTS.SP.PHB.ENTITY.PBDT.QLDT;
using BTS.SP.PHB.ENTITY.PBDT.TT344;
using BTS.SP.PHB.ENTITY.Rp;
using BTS.SP.PHB.ENTITY.Rp.B01A_TT137;
using BTS.SP.PHB.ENTITY.Rp.B01B_TT137;
using BTS.SP.PHB.ENTITY.Rp.B01BCQT;
using BTS.SP.PHB.ENTITY.Rp.B01BDG_TT137;
using BTS.SP.PHB.ENTITY.Rp.B02_TT137;
using BTS.SP.PHB.ENTITY.Rp.B02BCQT;
using BTS.SP.PHB.ENTITY.Rp.B02H_II;
using BTS.SP.PHB.ENTITY.Rp.B03_TT90;
using BTS.SP.PHB.ENTITY.Rp.B03BBCTC;
using BTS.SP.PHB.ENTITY.Rp.B03BCQT_BII1;
using BTS.SP.PHB.ENTITY.Rp.B04_TT90;
using BTS.SP.PHB.ENTITY.Rp.BIEU01A;
using BTS.SP.PHB.ENTITY.Rp.BIEU01B;
using BTS.SP.PHB.ENTITY.Rp.BIEU01C;
using BTS.SP.PHB.ENTITY.Rp.BIEU01CP2;
using BTS.SP.PHB.ENTITY.Rp.BIEU03;
using BTS.SP.PHB.ENTITY.Rp.BIEU03_TT137;
using BTS.SP.PHB.ENTITY.Rp.BIEU07TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU08TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU09TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU10TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU11TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU12TT344;
using BTS.SP.PHB.ENTITY.Rp.BIEU2A;
using BTS.SP.PHB.ENTITY.Rp.BIEU2B;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP1;
using BTS.SP.PHB.ENTITY.Rp.BIEU2CP2;
using BTS.SP.PHB.ENTITY.Rp.BIEU3A;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP1;
using BTS.SP.PHB.ENTITY.Rp.BIEU3BP2;
using BTS.SP.PHB.ENTITY.Rp.BIEU4A;
using BTS.SP.PHB.ENTITY.Rp.BIEU4BP1;
using BTS.SP.PHB.ENTITY.Rp.BIEU4BP2;
using BTS.SP.PHB.ENTITY.Rp.BIEU67NS;
using BTS.SP.PHB.ENTITY.Rp.BIEU68NS;
using BTS.SP.PHB.ENTITY.Rp.BIEU69NS;
using BTS.SP.PHB.ENTITY.Rp.BIEU70NS;
using BTS.SP.PHB.ENTITY.Rp.BM16_TT344;
using BTS.SP.PHB.ENTITY.Rp.BM48_TT342;
using BTS.SP.PHB.ENTITY.Rp.C_B01X;
using BTS.SP.PHB.ENTITY.Rp.C_B02AX;
using BTS.SP.PHB.ENTITY.Rp.C_B02B_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03A_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03B_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03C_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03D_X;
using BTS.SP.PHB.ENTITY.Rp.C_B03X;
using BTS.SP.PHB.ENTITY.Rp.C_B04X;
using BTS.SP.PHB.ENTITY.Rp.C_B06X;
using BTS.SP.PHB.ENTITY.Rp.DOICHIEUSOLIEU;
using BTS.SP.PHB.ENTITY.Rp.DUTOANLUONG;
using BTS.SP.PHB.ENTITY.Rp.F01_01BCQT;
using BTS.SP.PHB.ENTITY.Rp.F01_02BCQT_PII;
using BTS.SP.PHB.ENTITY.Rp.L_PC_D;
using BTS.SP.PHB.ENTITY.Rp.L_PC_DT;
using BTS.SP.PHB.ENTITY.Rp.L_PC_UB;
using BTS.SP.PHB.ENTITY.Rp.NHAP_DT_XA;
using BTS.SP.PHB.ENTITY.Rp.PHB.B02BCQT;
using BTS.SP.PHB.ENTITY.Rp.PHB.PL32_P1_TT01;
using BTS.SP.PHB.ENTITY.Rp.PHB.PL42_P1_TT01;
using BTS.SP.PHB.ENTITY.Rp.PHB_BM14TT134;
using BTS.SP.PHB.ENTITY.Rp.PHB_F01_02BCQT;
using BTS.SP.PHB.ENTITY.Rp.PHB_PL32_P2_TT01;
using BTS.SP.PHB.ENTITY.Rp.PL01_TT137;
using BTS.SP.PHB.ENTITY.Rp.PL02_TT137;
using BTS.SP.PHB.ENTITY.Rp.PL3_1;
using BTS.SP.PHB.ENTITY.Rp.PL41;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.B04_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BCTC_TH_TEMPLATE;
//using BTS.SP.PHB.ENTITY.Rp.PL1_TT137;
//using BTS.SP.PHB.SERVICE.REPORT.PL01_TT137;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.ENTITY.Sys;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDung;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDungNhomQuyen;
using BTS.SP.PHB.SERVICE.AUTH.AuNguoiDungQuyen;
using BTS.SP.PHB.SERVICE.AUTH.AuNhomQuyen;
using BTS.SP.PHB.SERVICE.AUTH.AuNhomQuyenChucNang;
using BTS.SP.PHB.SERVICE.AUTH.Shared;
using BTS.SP.PHB.SERVICE.HTDM.DmBaoCao;
using BTS.SP.PHB.SERVICE.HTDM.DmCanBo;
using BTS.SP.PHB.SERVICE.HTDM.DmChiTieuBaoCao;
using BTS.SP.PHB.SERVICE.HTDM.DmChuong;
using BTS.SP.PHB.SERVICE.HTDM.DmCoQuanThu;
using BTS.SP.PHB.SERVICE.HTDM.DmCtMucTieu;
using BTS.SP.PHB.SERVICE.HTDM.DmDBHC;
using BTS.SP.PHB.SERVICE.HTDM.DmDoiTuongNopThue;
using BTS.SP.PHB.SERVICE.HTDM.DmDuAn;
using BTS.SP.PHB.SERVICE.HTDM.DmDVQHNS;
using BTS.SP.PHB.SERVICE.HTDM.DmHoatDong;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmLoaiNganSach;
using BTS.SP.PHB.SERVICE.HTDM.DmNganhKT;
using BTS.SP.PHB.SERVICE.HTDM.DmNguonNganSach;
using BTS.SP.PHB.SERVICE.HTDM.DmNhomMucChi;
using BTS.SP.PHB.SERVICE.HTDM.DmNoiDungKt;
using BTS.SP.PHB.SERVICE.HTDM.DmTaiKhoan;
using BTS.SP.PHB.SERVICE.HTDM.DmTienLuong;
using BTS.SP.PHB.SERVICE.HTDM.DmTKKhoBac;
using BTS.SP.PHB.SERVICE.HTDM.DmTSCD;
using BTS.SP.PHB.SERVICE.HTDM.DmTyLeDieuTiet;
using BTS.SP.PHB.SERVICE.PBDT.B05;
using BTS.SP.PHB.SERVICE.PBDT.B06;
using BTS.SP.PHB.SERVICE.PBDT.B07;
using BTS.SP.PHB.SERVICE.PBDT.B111;
using BTS.SP.PHB.SERVICE.PBDT.B121;
using BTS.SP.PHB.SERVICE.PBDT.B122;
using BTS.SP.PHB.SERVICE.PBDT.B123;
using BTS.SP.PHB.SERVICE.PBDT.B1301;
using BTS.SP.PHB.SERVICE.PBDT.B1302;
using BTS.SP.PHB.SERVICE.PBDT.B1303;
using BTS.SP.PHB.SERVICE.PBDT.B1304;
using BTS.SP.PHB.SERVICE.PBDT.B1305;
using BTS.SP.PHB.SERVICE.PBDT.B1306;
using BTS.SP.PHB.SERVICE.PBDT.B1307;
using BTS.SP.PHB.SERVICE.PBDT.B1308;
using BTS.SP.PHB.SERVICE.PBDT.B1309;
using BTS.SP.PHB.SERVICE.PBDT.B1310;
using BTS.SP.PHB.SERVICE.PBDT.B1311;
using BTS.SP.PHB.SERVICE.PBDT.B1312;
using BTS.SP.PHB.SERVICE.PBDT.B14;
using BTS.SP.PHB.SERVICE.PBDT.B1501;
using BTS.SP.PHB.SERVICE.PBDT.B1502;
using BTS.SP.PHB.SERVICE.PBDT.B32;
using BTS.SP.PHB.SERVICE.PBDT.QLDT;
using BTS.SP.PHB.SERVICE.PBDT.TT342.B124;
using BTS.SP.PHB.SERVICE.PBDT.TT342.B125;
using BTS.SP.PHB.SERVICE.PBDT.TT344;
using BTS.SP.PHB.SERVICE.REPORT;
using BTS.SP.PHB.SERVICE.REPORT.B01A_TT137;
using BTS.SP.PHB.SERVICE.REPORT.B01B_TT137;
using BTS.SP.PHB.SERVICE.REPORT.B01BCQT;
using BTS.SP.PHB.SERVICE.REPORT.B01BDG_TT137;
using BTS.SP.PHB.SERVICE.REPORT.B02_TT137;
using BTS.SP.PHB.SERVICE.REPORT.B02BCQT;
using BTS.SP.PHB.SERVICE.REPORT.B02H_II;
using BTS.SP.PHB.SERVICE.REPORT.B03_TT90;
using BTS.SP.PHB.SERVICE.REPORT.B03BCQT_BII1;
using BTS.SP.PHB.SERVICE.REPORT.B03BCTC;
using BTS.SP.PHB.SERVICE.REPORT.B04_TT90;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu01B;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01C;
using BTS.SP.PHB.SERVICE.REPORT.BIEU01CP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu03;
using BTS.SP.PHB.SERVICE.REPORT.BIEU03_TT137;
using BTS.SP.PHB.SERVICE.REPORT.Bieu07TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu08TT344;
using BTS.SP.PHB.SERVICE.REPORT.BIEU09TT344;
using BTS.SP.PHB.SERVICE.REPORT.BIEU10TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu11TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu12TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bieu2A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu2B;
using BTS.SP.PHB.SERVICE.REPORT.BIEU2CP1;
using BTS.SP.PHB.SERVICE.REPORT.BIEU2CP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3BP1;
using BTS.SP.PHB.SERVICE.REPORT.Bieu3BP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4A;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4BP1;
using BTS.SP.PHB.SERVICE.REPORT.Bieu4BP2;
using BTS.SP.PHB.SERVICE.REPORT.Bieu67NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu68NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu69NS;
using BTS.SP.PHB.SERVICE.REPORT.Bieu70NS;
using BTS.SP.PHB.SERVICE.REPORT.BM16_TT344;
using BTS.SP.PHB.SERVICE.REPORT.Bm48TT342;
using BTS.SP.PHB.SERVICE.REPORT.C_B01X;
using BTS.SP.PHB.SERVICE.REPORT.C_B02aX;
using BTS.SP.PHB.SERVICE.REPORT.C_B02B_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03A_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03B_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03C_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03D_X;
using BTS.SP.PHB.SERVICE.REPORT.C_B03X;
using BTS.SP.PHB.SERVICE.REPORT.C_B04X;
using BTS.SP.PHB.SERVICE.REPORT.C_B06X;
using BTS.SP.PHB.SERVICE.REPORT.DOICHIEUSOLIEU;
using BTS.SP.PHB.SERVICE.REPORT.DoiChieuTABMIS;
using BTS.SP.PHB.SERVICE.REPORT.DUTOANLUONG;
using BTS.SP.PHB.SERVICE.REPORT.F01_01BCQT;
using BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT;
using BTS.SP.PHB.SERVICE.REPORT.F01_02BCQT_PII;
using BTS.SP.PHB.SERVICE.REPORT.L_PC_D;
using BTS.SP.PHB.SERVICE.REPORT.L_PC_DT;
using BTS.SP.PHB.SERVICE.REPORT.L_PC_UB;
using BTS.SP.PHB.SERVICE.REPORT.NHAP_DT_XA;
using BTS.SP.PHB.SERVICE.REPORT.PHB_BM14_TT134;
using BTS.SP.PHB.SERVICE.REPORT.PL01_TT137;
using BTS.SP.PHB.SERVICE.REPORT.PL02_TT137;
using BTS.SP.PHB.SERVICE.REPORT.PL31;
using BTS.SP.PHB.SERVICE.REPORT.PL32_P1_TT01;
using BTS.SP.PHB.SERVICE.REPORT.PL32_P2_TT01;
using BTS.SP.PHB.SERVICE.REPORT.PL41;
using BTS.SP.PHB.SERVICE.REPORT.PL42_P1_TT01;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_1;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B01_BSTT_2;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B02_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03A_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B03B_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B04_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.B05_BCTC;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BC04_BCTC_TT107;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.BCTC_TH_TEMPLATE;
using BTS.SP.PHB.SERVICE.REPORT_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.SERVICE.SYS.SysChucNang;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns;
using BTS.SP.PHB.SERVICE.SYS.SysDvqhns_QuanLy;
using BTS.SP.PHB.SERVICE.SYS.sysLogChucNang;
using BTS.SP.PHB.SERVICE.SYS.SysLogSyncMisa;
using BTS.SP.PHB.SERVICE.SYS.SysScheduler;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System.Web.Http;

namespace BTS.SP.API.PHB.App_Start
{
    public class UnityConfig
    {

        private static readonly UnityContainer container = new UnityContainer();

        public static void Register(HttpConfiguration config)
        {
            container.RegisterType<IDataContextAsync, PHBContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<BaseEntity>, Repository<BaseEntity>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISharedService, SharedService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<AU_NGUOIDUNG>, Repository<AU_NGUOIDUNG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuNguoiDungService, AuNguoiDungService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<AU_NHOMQUYEN>, Repository<AU_NHOMQUYEN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuNhomQuyenService, AuNhomQuyenService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<AU_NGUOIDUNG_NHOMQUYEN>, Repository<AU_NGUOIDUNG_NHOMQUYEN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuNguoiDungNhomQuyenService, AuNguoiDungNhomQuyenService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<AU_NGUOIDUNG_QUYEN>, Repository<AU_NGUOIDUNG_QUYEN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuNguoiDungQuyenService, AuNguoiDungQuyenService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<AU_NHOMQUYEN_CHUCNANG>, Repository<AU_NHOMQUYEN_CHUCNANG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuNhomQuyenChucNangService, AuNhomQuyenChucNangService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<SYS_CHUCNANG>, Repository<SYS_CHUCNANG>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysChucNangService, SysChucNangService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<SYS_DVQHNS>, Repository<SYS_DVQHNS>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysDvqhnsService, SysDvqhnsService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<SYS_DVQHNS_QUANLY>, Repository<SYS_DVQHNS_QUANLY>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysDvqhns_QuanLyService, SysDvqhns_QuanLyService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<DM_DBHC>, Repository<DM_DBHC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmDBHCService, DmDBHCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_SYS_LOG_CHUCNANG>, Repository<PHB_SYS_LOG_CHUCNANG>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysLogChucNangService, SysLogChucNangService>(new HierarchicalLifetimeManager());

            #region DM
            container.RegisterType<IRepositoryAsync<DM_CHUONG>, Repository<DM_CHUONG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmChuongService, DmChuongService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<DM_NGANHKT>, Repository<DM_NGANHKT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDM_NGANHKTService, DM_NGANHKTService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<DM_TKKHOBAC>, Repository<DM_TKKHOBAC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmTKKhoBacService, DmTKKhoBacService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<DM_CTMUCTIEU>, Repository<DM_CTMUCTIEU>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmCtMucTieuService, DmCtMucTieuService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_BAOCAO>, Repository<PHB_DM_BAOCAO>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmBaocaoService, DmBaocaoService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_DUAN>, Repository<PHB_DM_DUAN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmDuAnService, DmDuAnService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_DVQHNS>, Repository<PHB_DM_DVQHNS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmDVQHNSService, DmDVQHNSService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_HOATDONG>, Repository<PHB_DM_HOATDONG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmHoatDongService, DmHoatDongService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_LOAI_CAPPHAT>, Repository<PHB_DM_LOAI_CAPPHAT>>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_LOAIKHOAN>, Repository<PHB_DM_LOAIKHOAN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmLoaiKhoanService, DmLoaiKhoanService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_LOAINGANSACH>, Repository<PHB_DM_LOAINGANSACH>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmLoaiNganSachService, DmLoaiNganSachService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_NGUONNGANSACH>, Repository<PHB_DM_NGUONNGANSACH>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmNguonNganSachService, DmNguonNganSachService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_TAIKHOAN>, Repository<PHB_DM_TAIKHOAN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmTaiKhoanService, DmTaiKhoanService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_TSCD>, Repository<PHB_DM_TSCD>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmTSCDService, DmTSCDService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_NHOMMUCCHI>, Repository<PHB_DM_NHOMMUCCHI>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmNhomMucChiService, DmNhomMucChiService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_NOIDUNGKT>, Repository<PHB_DM_NOIDUNGKT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmNoiDungKtService, DmNoiDungKtService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_CANBO>, Repository<PHB_DM_CANBO>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmCanBoService, DmCanBoService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_TIENLUONG>, Repository<PHB_DM_TIENLUONG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmTienLuongService, DmTienLuongService>(new HierarchicalLifetimeManager());

            #endregion

            container.RegisterType<IRepositoryAsync<PHB_REPORT_FIELD>, Repository<PHB_REPORT_FIELD>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbReportFieldService, PhbReportFieldService>(new HierarchicalLifetimeManager());


            #region REPORT BIEU03_TT137
            container.RegisterType<IRepositoryAsync<PHB_BIEU03_TT137>, Repository<PHB_BIEU03_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBIEU03_TT137Service, BIEU03_TT137Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU03_TT137_TEMPLATE>, Repository<PHB_BIEU03_TT137_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBIEU03_TT137_TEMPLATEService, BIEU03_TT137_TEMPLATEService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU03_TT137_DETAIL>, Repository<PHB_BIEU03_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBIEU03_TT137_DETAILService, BIEU03_TT137_DETAILService>(new HierarchicalLifetimeManager());
            #endregion


            #region REPORT BIEU03
            container.RegisterType<IRepositoryAsync<PHB_BIEU03>, Repository<PHB_BIEU03>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu03Service, PhbBieu03Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU03_TEMPLATE>, Repository<PHB_BIEU03_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu03TemplateService, PhbBieu03TemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU03_DETAIL>, Repository<PHB_BIEU03_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu03DetailService, PhbBieu03DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region BC_NHAPDT_XA
            container.RegisterType<IRepositoryAsync<BC_NHAPDT_XA_DETAIL>, Repository<BC_NHAPDT_XA_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBC_NHAPDTXA_DETAILService, BC_NHAPDTXA_DETAILService>(new HierarchicalLifetimeManager());
            #endregion

            #region DM_CHITEU_BAOCAO
            container.RegisterType<IRepositoryAsync<DM_CHITIEU_BAOCAO>, Repository<DM_CHITIEU_BAOCAO>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDM_CT_BAOCAOService, DM_CT_BAOCAOService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU01A
            container.RegisterType<IRepositoryAsync<PHB_BIEU01A>, Repository<PHB_BIEU01A>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01AService, PhbBieu01AService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01A_DETAIL>, Repository<PHB_BIEU01A_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01ADetailService, PhbBieu01ADetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01A_TEMPLATE>, Repository<PHB_BIEU01A_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01ATemplateService, PhbBieu01ATemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU01B
            container.RegisterType<IRepositoryAsync<PHB_BIEU01B>, Repository<PHB_BIEU01B>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01BService, PhbBieu01BService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01B_DETAIL>, Repository<PHB_BIEU01B_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01BDetailService, PhbBieu01BDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01B_TEMPLATE>, Repository<PHB_BIEU01B_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu01BTemplateService, PhbBieu01BTemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU01C
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C>, Repository<PHB_BIEU01C>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CService, PhbBIEU01CService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C_DETAIL>, Repository<PHB_BIEU01C_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CDetailService, PhbBIEU01CDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C_TEMPLATE>, Repository<PHB_BIEU01C_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CTemplateService, PhbBIEU01CTemplateService>(new HierarchicalLifetimeManager());
            #endregion


            #region REPORT BIEU2A
            container.RegisterType<IRepositoryAsync<PHB_BIEU2A>, Repository<PHB_BIEU2A>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2AService, PhbBieu2AService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2A_DETAIL>, Repository<PHB_BIEU2A_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2ADetailService, PhbBieu2ADetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2A_TEMPLATE>, Repository<PHB_BIEU2A_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2ATemplateService, PhbBieu2ATemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU2B
            container.RegisterType<IRepositoryAsync<PHB_BIEU2B>, Repository<PHB_BIEU2B>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2BService, PhbBieu2BService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2B_DETAIL>, Repository<PHB_BIEU2B_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2BDetailService, PhbBieu2BDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2B_TEMPLATE>, Repository<PHB_BIEU2B_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu2BTemplateService, PhbBieu2BTemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU2C - Phan I
            container.RegisterType<IRepositoryAsync<PHB_BIEU2CP1>, Repository<PHB_BIEU2CP1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU2CP1Service, PhbBIEU2CP1Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2CP1_TEMPLATE>, Repository<PHB_BIEU2CP1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU2CP1TemplateService, PhbBIEU2CP1TemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2CP1_DETAIL>, Repository<PHB_BIEU2CP1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU2CP1DetailService, PhbBIEU2CP1DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU2C - Phan II
            container.RegisterType<IRepositoryAsync<PHB_BIEU2CP2>, Repository<PHB_BIEU2CP2>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU2CP2Service, PhbBIEU2CP2Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU2CP2_DETAIL>, Repository<PHB_BIEU2CP2_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU2CP2DetailService, PhbBIEU2CP2DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU3A
            container.RegisterType<IRepositoryAsync<PHB_BIEU3A>, Repository<PHB_BIEU3A>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3AService, PhbBieu3AService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU3A_DETAIL>, Repository<PHB_BIEU3A_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3ADetailService, PhbBieu3ADetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU3A_TEMPLATE>, Repository<PHB_BIEU3A_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3ATemplateService, PhbBieu3ATemplateService>(new HierarchicalLifetimeManager());


            #endregion

            #region REPORT BIEU3BP1
            container.RegisterType<IRepositoryAsync<PHB_BIEU3BP1>, Repository<PHB_BIEU3BP1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3BP1Service, PhbBieu3BP1Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU3BP1_TEMPLATE>, Repository<PHB_BIEU3BP1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3BP1TemplateService, PhbBieu3BP1TemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU3BP1_DETAIL>, Repository<PHB_BIEU3BP1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3BP1DetailService, PhbBieu3BP1DetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU3BP2
            container.RegisterType<IRepositoryAsync<PHB_BIEU3BP2>, Repository<PHB_BIEU3BP2>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3BP2Service, PhbBieu3BP2Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU3BP2_DETAIL>, Repository<PHB_BIEU3BP2_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu3BP2DetailService, PhbBieu3BP2DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PL32_P2_TT01
            container.RegisterType<IRepositoryAsync<PHB_PL32_P2_TT01>, Repository<PHB_PL32_P2_TT01>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL32_P2_TT01Service, PhbPL32_P2_TT01Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL32_P2_TT01_DETAIL>, Repository<PHB_PL32_P2_TT01_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL32_P2_TT01DetailService, PhbPL32_P2_TT01DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU4BP1
            container.RegisterType<IRepositoryAsync<PHB_BIEU4BP1>, Repository<PHB_BIEU4BP1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4BP1Service, PhbBieu4BP1Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU4BP1_TEMPLATE>, Repository<PHB_BIEU4BP1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4BP1TemplateService, PhbBieu4BP1TemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU4BP1_DETAIL>, Repository<PHB_BIEU4BP1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4BP1DetailService, PhbBieu4BP1DetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU4BP2
            container.RegisterType<IRepositoryAsync<PHB_BIEU4BP2>, Repository<PHB_BIEU4BP2>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4BP2Service, PhbBieu4BP2Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU4BP2_DETAIL>, Repository<PHB_BIEU4BP2_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4BP2DetailService, PhbBieu4BP2DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT BIEU4A
            container.RegisterType<IRepositoryAsync<PHB_BIEU4A>, Repository<PHB_BIEU4A>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4AService, PhbBieu4AService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU4A_DETAIL>, Repository<PHB_BIEU4A_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4ADetailService, PhbBieu4ADetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU4A_TEMPLATE>, Repository<PHB_BIEU4A_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu4ATemplateService, PhbBieu4ATemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT B01BCQT
            container.RegisterType<IRepositoryAsync<PHB_B01BCQT>, Repository<PHB_B01BCQT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BCQTService, PhbB01BCQTService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B01BCQT_TEMPLATE>, Repository<PHB_B01BCQT_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BCQTTemplateService, PhbB01BCQTTemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B01BCQT_DETAIL>, Repository<PHB_B01BCQT_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BCQTDetailService, PhbB01BCQTDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region B02H_II
            container.RegisterType<IRepositoryAsync<PHB_B02H_II>, Repository<PHB_B02H_II>>(new HierarchicalLifetimeManager());
            container.RegisterType<IB02H_IIService, B02H_IIService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B02H_II_DETAIL>, Repository<PHB_B02H_II_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IB02H_IIDetailService, B02H_IIDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT F01_01BCQT
            container.RegisterType<IRepositoryAsync<PHB_F01_01BCQT>, Repository<PHB_F01_01BCQT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IF01_01BCQTService, F01_01BCQTService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_F01_01BCQT_DETAIL>, Repository<PHB_F01_01BCQT_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IF01_01BCQTDetailService, F01_01BCQTDetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT F01_02BCQT

            container.RegisterType<IRepositoryAsync<PHB_F01_02BCQT>, Repository<PHB_F01_02BCQT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02BCQTService, PhbF01_02BCQTService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_F01_02BCQT_TEMPLATE>, Repository<PHB_F01_02BCQT_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02BCQTTemplateService, PhbF01_02BCQTTemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_F01_02BCQT_DETAIL>, Repository<PHB_F01_02BCQT_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02BCQTDetailService, PhbF01_02BCQTDetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU01C
            container.RegisterType<IRepositoryAsync<PHB_BIEU01CP2>, Repository<PHB_BIEU01CP2>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBIEU01CP2Service, BIEU01CP2Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01CP2_DETAIL>, Repository<PHB_BIEU01CP2_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBIEU01CP2DetailService, BIEU01CP2DetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT B03BCTC
            container.RegisterType<IRepositoryAsync<PHB_B03BBCTC>, Repository<PHB_B03BBCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BctcService, PhbB03BctcService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B03BBCTC_DETAIL>, Repository<PHB_B03BBCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BctcDetailService, PhbB03BctcDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B03BBCTC_TEMPLATE>, Repository<PHB_B03BBCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BctcTemplateService, PhbB03BctcTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            //#region REPORT BIEU04TT61
            //container.RegisterType<IRepositoryAsync<PHB_BIEU04_TT61>, Repository<PHB_BIEU04_TT61>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IBIEU04TT61Service, BIEU04TT61Service>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepositoryAsync<PHB_BIEU04_TT61_DETAILS>, Repository<PHB_BIEU04_TT61_DETAILS>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IBIEU04TT61DetailsService, BIEU04TT61DetailsService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IRepositoryAsync<PHB_BIEU04_TT61_TEMPLATE>, Repository<PHB_BIEU04_TT61_TEMPLATE>>(new HierarchicalLifetimeManager());
            //container.RegisterType<IBIEU04TT61TemplateService, BIEU04TT61TemplateService>(new HierarchicalLifetimeManager());

            //#endregion

            #region REPORT B02BCQT
            container.RegisterType<IRepositoryAsync<PHB_B02BCQT>, Repository<PHB_B02BCQT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02BcqtService, PhbB02BcqtService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B02BCQT_DETAIL>, Repository<PHB_B02BCQT_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02BcqtDetailService, PhbB02BcqtDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B02BCQT_TEMPLATE>, Repository<PHB_B02BCQT_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02BcqtTemplateService, PhbB02BcqtTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU01C
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C>, Repository<PHB_BIEU01C>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CService, PhbBIEU01CService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C_TEMPLATE>, Repository<PHB_BIEU01C_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CTemplateService, PhbBIEU01CTemplateService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU01C_DETAIL>, Repository<PHB_BIEU01C_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBIEU01CDetailService, PhbBIEU01CDetailService>(new HierarchicalLifetimeManager());
            #endregion
            #region REPORT F01_02BCQT_PII
            container.RegisterType<IRepositoryAsync<PHB_F01_02BCQT_PII>, Repository<PHB_F01_02BCQT_PII>>(new HierarchicalLifetimeManager());
            container.RegisterType<IF01_02BCQT_PIIService, F01_02BCQT_PIIService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_F01_02BCQT_PII_DETAIL>, Repository<PHB_F01_02BCQT_PII_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IF01_02BCQT_PIIDetailService, F01_02BCQT_PIIDetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU11TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU11TT344>, Repository<PHB_BIEU11TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu11TT344Service, PhbBieu11TT344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU11TT344_DETAIL>, Repository<PHB_BIEU11TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu11TT344DetailService, PhbBieu11TT344DetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT BIEU12TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU12TT344>, Repository<PHB_BIEU12TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu12TT344Service, PhbBieu12TT344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU12TT344_DETAIL>, Repository<PHB_BIEU12TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu12TT344DetailService, PhbBieu12TT344DetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region BIEU07TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU07TT344>, Repository<PHB_BIEU07TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu07TT344Service, PhbBieu07TT344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU07TT344_DETAIL>, Repository<PHB_BIEU07TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu07TT344DetailService, PhbBieu07TT344DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU07TT344_TEMPLATE>, Repository<PHB_BIEU07TT344_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu07TT344TemplateService, PhbBieu07TT344TemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region BIEU08TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU08TT344>, Repository<PHB_BIEU08TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu08TT344Service, PhbBieu08TT344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU08TT344_DETAIL>, Repository<PHB_BIEU08TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu08TT344DetailService, PhbBieu08TT344DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU08TT344_TEMPLATE>, Repository<PHB_BIEU08TT344_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu08TT344TemplateService, PhbBieu08TT344TemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region BIEU09TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU09TT344>, Repository<PHB_BIEU09TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu09tt344Service, PhbBieu09tt344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU09TT344_DETAIL>, Repository<PHB_BIEU09TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu09tt344DetailService, PhbBieu09tt344DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU09TT344_TEMPLATE>, Repository<PHB_BIEU09TT344_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu09tt344TemplateService, PhbBieu09tt344TemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region BIEU10TT344
            container.RegisterType<IRepositoryAsync<PHB_BIEU10TT344>, Repository<PHB_BIEU10TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu10tt344Service, PhbBieu10tt344Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU10TT344_DETAIL>, Repository<PHB_BIEU10TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu10tt344DetailService, PhbBieu10tt344DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region 70NS TT342
            container.RegisterType<IRepositoryAsync<PHB_BIEU70NS>, Repository<PHB_BIEU70NS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu70NsService, PhbBieu70NsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU70NS_DETAIL>, Repository<PHB_BIEU70NS_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu70NsDetailService, PhbBieu70NsDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU70NS_TEMPLATE>, Repository<PHB_BIEU70NS_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu70NsTemplateService, PhbBieu70NsTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region 67NS TT342
            container.RegisterType<IRepositoryAsync<PHB_BIEU67NS>, Repository<PHB_BIEU67NS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu67NsService, PhbBieu67NsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU67NS_DETAIL>, Repository<PHB_BIEU67NS_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu67NsDetailService, PhbBieu67NsDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU67NS_TEMPLATE>, Repository<PHB_BIEU67NS_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu67NsTemplateService, PhbBieu67NsTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region 68NS TT342
            container.RegisterType<IRepositoryAsync<PHB_BIEU68NS>, Repository<PHB_BIEU68NS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu68NsService, PhbBieu68NsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU68NS_DETAIL>, Repository<PHB_BIEU68NS_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu68NsDetailService, PhbBieu68NsDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU68NS_TEMPLATE>, Repository<PHB_BIEU68NS_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu68NsTemplateService, PhbBieu68NsTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region 69NS TT342
            container.RegisterType<IRepositoryAsync<PHB_BIEU69NS>, Repository<PHB_BIEU69NS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu69NsService, PhbBieu69NsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU69NS_DETAIL>, Repository<PHB_BIEU69NS_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu69NsDetailService, PhbBieu69NsDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_BIEU69NS_TEMPLATE>, Repository<PHB_BIEU69NS_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieu69NsTemplateService, PhbBieu69NsTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT PL31 TT01
            container.RegisterType<IRepositoryAsync<PHB_PL31>, Repository<PHB_PL31>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL31Service, PhbPL31Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL31_DETAIL>, Repository<PHB_PL31_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL31DetailService, PhbPL31DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL31_TEMPLATE>, Repository<PHB_PL31_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL31TemplateService, PhbPL31TemplateService>(new HierarchicalLifetimeManager());


            #endregion

            #region PL32 TT01 P1
            container.RegisterType<IRepositoryAsync<PHB_PL32_P1_TT01>, Repository<PHB_PL32_P1_TT01>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL32_P1_TT01Service, PhbPL32_P1_TT01Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL32_P1_TT01_DETAIL>, Repository<PHB_PL32_P1_TT01_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL32_P1_TT01DetailService, PhbPL32_P1_TT01DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region PL42 TT01 P1
            container.RegisterType<IRepositoryAsync<PHB_PL42_P1_TT01>, Repository<PHB_PL42_P1_TT01>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL42_P1_TT01Service, PhbPL42_P1_TT01Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL42_P1_TT01_DETAIL>, Repository<PHB_PL42_P1_TT01_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL42_P1_TT01DetailService, PhbPL42_P1_TT01DetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PL41 TT01
            container.RegisterType<IRepositoryAsync<PHB_PL41>, Repository<PHB_PL41>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL41Service, PhbPL41Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL41_DETAIL>, Repository<PHB_PL41_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL41DetailService, PhbPL41DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL41_TEMPLATE>, Repository<PHB_PL41_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbPL41TemplateService, PhbPL41TemplateService>(new HierarchicalLifetimeManager());


            #endregion

            #region Đối chiếu số liệu
            container.RegisterType<IRepositoryAsync<PHB_DOICHIEUSOLIEU>, Repository<PHB_DOICHIEUSOLIEU>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbDoiChieuSoLieuService, PhbDoiChieuSoLieuService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT B03BCQT_BII1
            container.RegisterType<IRepositoryAsync<PHB_B03BCQT_BII1>, Repository<PHB_B03BCQT_BII1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BCQT_BII1Service, PhbB03BCQT_BII1Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B03BCQT_BII1_DETAIL>, Repository<PHB_B03BCQT_BII1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BCQT_BII1DetailService, PhbB03BCQT_BII1DetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_B03BCQT_BII1_TEMPLATE>, Repository<PHB_B03BCQT_BII1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03BCQT_BII1TemplateService, PhbB03BCQT_BII1TemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PHB_C_B03X
            container.RegisterType<IRepositoryAsync<PHB_C_B03X>, Repository<PHB_C_B03X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbcB03xService, PhbcB03xService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B03X_DETAIL>, Repository<PHB_C_B03X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbcB03xDetailService, PhbcB03xDetailService>(new HierarchicalLifetimeManager());

            #endregion
            #region REPORT PHB_C_B01X
            container.RegisterType<IRepositoryAsync<PHB_C_B01X>, Repository<PHB_C_B01X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B01XService, PhbC_B01XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B01X_DETAIL>, Repository<PHB_C_B01X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B01XDetailService, PhbC_B01XDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B01X_TEMPLATE>, Repository<PHB_C_B01X_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B01XTemplateService, PhbC_B01XTemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PHB_C_B03B_X
            container.RegisterType<IRepositoryAsync<PHB_C_B03B_X>, Repository<PHB_C_B03B_X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03B_XService, Phb_C_B03B_XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B03B_X_DETAIL>, Repository<PHB_C_B03B_X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03B_XDetailService, Phb_C_B03B_XDetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT PHB_C_B03A_X
            container.RegisterType<IRepositoryAsync<PHB_C_B03A_X>, Repository<PHB_C_B03A_X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_C_B03A_XService, Phb_C_B03A_XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B03A_X_DETAIL>, Repository<PHB_C_B03A_X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03A_XDetailService, Phb_C_B03A_XDetailService>(new HierarchicalLifetimeManager());

            #endregion

            #region REPORT PHB_C_B03C_X
            container.RegisterType<IRepositoryAsync<PHB_C_B03C_X>, Repository<PHB_C_B03C_X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03C_XService, Phb_C_B03C_XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B03C_X_DETAIL>, Repository<PHB_C_B03C_X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03C_XDetailService, Phb_C_B03C_XDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PHB_L_PC_UB
            container.RegisterType<IRepositoryAsync<PHB_L_PC_UB>, Repository<PHB_L_PC_UB>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_UBService, PhbL_PC_UBService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_L_PC_UB_DETAIL>, Repository<PHB_L_PC_UB_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_UBDetailService, PhbL_PC_UBDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PHB_L_PC_D
            container.RegisterType<IRepositoryAsync<PHB_L_PC_D>, Repository<PHB_L_PC_D>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_DService, PhbL_PC_DService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_L_PC_D_DETAIL>, Repository<PHB_L_PC_D_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_DDetailService, PhbL_PC_DDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT PHB_L_PC_DT
            container.RegisterType<IRepositoryAsync<PHB_L_PC_DT>, Repository<PHB_L_PC_DT>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_DTService, PhbL_PC_DTService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_L_PC_DT_DETAIL>, Repository<PHB_L_PC_DT_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbL_PC_DTDetailService, PhbL_PC_DTDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT B02aX
            container.RegisterType<IRepositoryAsync<PHB_C_B02AX>, Repository<PHB_C_B02AX>>(new HierarchicalLifetimeManager());
            container.RegisterType<IB02aXService, B02aXService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B02AX_DETAIL>, Repository<PHB_C_B02AX_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IB02aXDetailService, B02aXDetailService>(new HierarchicalLifetimeManager());
            #endregion

            #region REPORT B02bX
            container.RegisterType<IRepositoryAsync<PHB_C_B02B_X>, Repository<PHB_C_B02B_X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IC_B02B_XService, C_B02B_XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B02B_X_DETAIL>, Repository<PHB_C_B02B_X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IC_B02B_XDetailService, C_B02B_XDetailService>(new HierarchicalLifetimeManager());
            #endregion


            #region REPORT PHB_C_B03D_X
            container.RegisterType<IRepositoryAsync<PHB_C_B03D_X>, Repository<PHB_C_B03D_X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03D_XService, Phb_C_B03D_XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B03D_X_DETAIL>, Repository<PHB_C_B03D_X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhb_C_B03D_XDetailService, Phb_C_B03D_XDetailService>(new HierarchicalLifetimeManager());
            #endregion


            #region REPORT C_B04X
            container.RegisterType<IRepositoryAsync<PHB_C_B04X>, Repository<PHB_C_B04X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieuC_B04XService, PhbBieuC_B04XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B04X_DETAIL>, Repository<PHB_C_B04X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieuC_B04XDetailService, PhbBieuC_B04XDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B04X_DETAIL_TSCD>, Repository<PHB_C_B04X_DETAIL_TSCD>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBieuC_B04XDetail_TSCDService, PhbBieuC_B04XDetail_TSCDService>(new HierarchicalLifetimeManager());
            #endregion
            #region REPORT PHB_C_B06X
            container.RegisterType<IRepositoryAsync<PHB_C_B06X>, Repository<PHB_C_B06X>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B06XService, PhbC_B06XService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B06X_DETAIL>, Repository<PHB_C_B06X_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B06XDetailService, PhbC_B06XDetailService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_C_B06X_TEMPLATE>, Repository<PHB_C_B06X_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbC_B06XTemplateService, PhbC_B06XTemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region BM48_TT342
            container.RegisterType<IRepositoryAsync<PHB_BM48_TT342>, Repository<PHB_BM48_TT342>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBm48TT342Service, PhbBm48TT342Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_BM48_TT342_DETAIL>, Repository<PHB_BM48_TT342_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBm48TT342DetailService, PhbBm48TT342DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_BM48_TT342_TEMPLATE>, Repository<PHB_BM48_TT342_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBm48TT342TemplateService, PhbBm48TT342TemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region PHB_B01A_TT137
            container.RegisterType<IRepositoryAsync<PHB_B01A_TT137>, Repository<PHB_B01A_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01aTT137Service, PhbB01aTT137Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01A_TT137_DETAIL>, Repository<PHB_B01A_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01aTT137DetailService, PhbB01aTT137DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01A_TT137_TEMPLATE>, Repository<PHB_B01A_TT137_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01aTT137TemplateService, PhbB01aTT137TemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region PHB_B01B_TT137
            container.RegisterType<IRepositoryAsync<PHB_B01B_TT137>, Repository<PHB_B01B_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bTT137Service, PhbB01bTT137Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01B_TT137_DETAIL>, Repository<PHB_B01B_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bTT137DetailService, PhbB01bTT137DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01B_TT137_TEMPLATE>, Repository<PHB_B01B_TT137_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bTT137TemplateService, PhbB01bTT137TemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region PHB_B01BDG_TT137
            container.RegisterType<IRepositoryAsync<PHB_B01BDG_TT137>, Repository<PHB_B01BDG_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bdgTT137Service, PhbB01bdgTT137Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01BDG_TT137_DETAIL>, Repository<PHB_B01BDG_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bdgTT137DetailService, PhbB01bdgTT137DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01BDG_TT137_TEMPLATE>, Repository<PHB_B01BDG_TT137_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01bdgTT137TemplateService, PhbB01bdgTT137TemplateService>(new HierarchicalLifetimeManager());

            #endregion


            #region PHB_B02_TT137
            container.RegisterType<IRepositoryAsync<PHB_B02_TT137>, Repository<PHB_B02_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02TT137Service, PhbB02TT137Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B02_TT137_DETAIL>, Repository<PHB_B02_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02TT137DetailService, PhbB02TT137DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B02_TT137_TEMPLATE>, Repository<PHB_B02_TT137_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB02TT137TemplateService, PhbB02TT137TemplateService>(new HierarchicalLifetimeManager());
            #endregion

            #region PHB_DUTOANLUONG
            container.RegisterType<IRepositoryAsync<PHB_DUTOANLUONG>, Repository<PHB_DUTOANLUONG>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbDuToanLuongService, PhbDuToanLuongService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DUTOANLUONG_DETAIL>, Repository<PHB_DUTOANLUONG_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbDuToanLuongDetailService, PhbDuToanLuongDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DUTOANLUONG_TEMPLATE>, Repository<PHB_DUTOANLUONG_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbDuToanLuongTemplateService, PhbDuToanLuongTemplateService>(new HierarchicalLifetimeManager());

            #endregion

            #region PBDT
            //
            // TT342
            //
            // B05
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B05>, Repository<PHB_PBDT_B05>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B05_Service, PHB_PBDT_B05_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B05_DETAIL>, Repository<PHB_PBDT_B05_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B05_DETAIL_Service, PHB_PBDT_B05_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B05_TEMPLATE>, Repository<PHB_PBDT_B05_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B05_TEMPLATE_Service, PHB_PBDT_B05_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B06
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B06>, Repository<PHB_PBDT_B06>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B06_Service, PHB_PBDT_B06_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B06_DATA>, Repository<PHB_PBDT_B06_DATA>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B06_DATA_Service, PHB_PBDT_B06_DATA_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B06_DETAIL>, Repository<PHB_PBDT_B06_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B06_DETAIL_Service, PHB_PBDT_B06_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B06_DONVI>, Repository<PHB_PBDT_B06_DONVI>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B06_DONVI_Service, PHB_PBDT_B06_DONVI_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B06_TEMPLATE>, Repository<PHB_PBDT_B06_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B06_TEMPLATE_Service, PHB_PBDT_B06_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B07
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B07>, Repository<PHB_PBDT_B07>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B07_Service, PHB_PBDT_B07_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B07_DETAIL>, Repository<PHB_PBDT_B07_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B07_DETAIL_Service, PHB_PBDT_B07_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B07_TEMPLATE>, Repository<PHB_PBDT_B07_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B07_TEMPLATE_Service, PHB_PBDT_B07_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B111
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B111>, Repository<PHB_PBDT_B111>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B111_Service, PHB_PBDT_B111_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B111_DETAIL>, Repository<PHB_PBDT_B111_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B111_DETAIL_Service, PHB_PBDT_B111_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B111_TEMPLATE>, Repository<PHB_PBDT_B111_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B111_TEMPLATE_Service, PHB_PBDT_B111_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1301
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1301>, Repository<PHB_PBDT_B1301>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1301_Service, PHB_PBDT_B1301_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1301_DETAIL>, Repository<PHB_PBDT_B1301_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1301_DETAIL_Service, PHB_PBDT_B1301_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1301_TEMPLATE>, Repository<PHB_PBDT_B1301_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1301_TEMPLATE_Service, PHB_PBDT_B1301_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1302
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1302>, Repository<PHB_PBDT_B1302>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1302_Service, PHB_PBDT_B1302_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1302_DETAIL>, Repository<PHB_PBDT_B1302_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1302_DETAIL_Service, PHB_PBDT_B1302_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1302_TEMPLATE>, Repository<PHB_PBDT_B1302_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1302_TEMPLATE_Service, PHB_PBDT_B1302_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1303
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1303>, Repository<PHB_PBDT_B1303>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1303_Service, PHB_PBDT_B1303_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1303_DETAIL>, Repository<PHB_PBDT_B1303_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1303_DETAIL_Service, PHB_PBDT_B1303_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1303_TEMPLATE>, Repository<PHB_PBDT_B1303_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1303_TEMPLATE_Service, PHB_PBDT_B1303_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1304
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1304>, Repository<PHB_PBDT_B1304>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1304_Service, PHB_PBDT_B1304_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1304_DETAIL>, Repository<PHB_PBDT_B1304_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1304_DETAIL_Service, PHB_PBDT_B1304_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1304_TEMPLATE>, Repository<PHB_PBDT_B1304_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1304_TEMPLATE_Service, PHB_PBDT_B1304_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1305
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1305>, Repository<PHB_PBDT_B1305>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1305_Service, PHB_PBDT_B1305_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1305_DETAIL>, Repository<PHB_PBDT_B1305_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1305_DETAIL_Service, PHB_PBDT_B1305_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1305_TEMPLATE>, Repository<PHB_PBDT_B1305_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1305_TEMPLATE_Service, PHB_PBDT_B1305_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1306
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1306>, Repository<PHB_PBDT_B1306>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1306_Service, PHB_PBDT_B1306_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1306_DETAIL>, Repository<PHB_PBDT_B1306_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1306_DETAIL_Service, PHB_PBDT_B1306_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1306_TEMPLATE>, Repository<PHB_PBDT_B1306_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1306_TEMPLATE_Service, PHB_PBDT_B1306_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1307
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1307>, Repository<PHB_PBDT_B1307>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1307_Service, PHB_PBDT_B1307_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1307_DETAIL>, Repository<PHB_PBDT_B1307_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1307_DETAIL_Service, PHB_PBDT_B1307_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1307_TEMPLATE>, Repository<PHB_PBDT_B1307_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1307_TEMPLATE_Service, PHB_PBDT_B1307_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1308
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1308>, Repository<PHB_PBDT_B1308>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1308_Service, PHB_PBDT_B1308_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1308_DETAIL>, Repository<PHB_PBDT_B1308_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1308_DETAIL_Service, PHB_PBDT_B1308_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1308_TEMPLATE>, Repository<PHB_PBDT_B1308_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1308_TEMPLATE_Service, PHB_PBDT_B1308_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1309
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1309>, Repository<PHB_PBDT_B1309>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1309_Service, PHB_PBDT_B1309_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1309_DETAIL>, Repository<PHB_PBDT_B1309_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1309_DETAIL_Service, PHB_PBDT_B1309_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1309_TEMPLATE>, Repository<PHB_PBDT_B1309_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1309_TEMPLATE_Service, PHB_PBDT_B1309_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1310
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1310>, Repository<PHB_PBDT_B1310>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1310_Service, PHB_PBDT_B1310_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1310_DETAIL>, Repository<PHB_PBDT_B1310_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1310_DETAIL_Service, PHB_PBDT_B1310_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1310_TEMPLATE>, Repository<PHB_PBDT_B1310_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1310_TEMPLATE_Service, PHB_PBDT_B1310_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1311
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1311>, Repository<PHB_PBDT_B1311>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1311_Service, PHB_PBDT_B1311_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1311_DETAIL>, Repository<PHB_PBDT_B1311_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1311_DETAIL_Service, PHB_PBDT_B1311_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1311_TEMPLATE>, Repository<PHB_PBDT_B1311_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1311_TEMPLATE_Service, PHB_PBDT_B1311_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1312
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1312>, Repository<PHB_PBDT_B1312>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1312_Service, PHB_PBDT_B1312_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1312_DETAIL>, Repository<PHB_PBDT_B1312_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1312_DETAIL_Service, PHB_PBDT_B1312_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1312_TEMPLATE>, Repository<PHB_PBDT_B1312_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1312_TEMPLATE_Service, PHB_PBDT_B1312_TEMPLATE_Service>(new HierarchicalLifetimeManager());


            // B14
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B14>, Repository<PHB_PBDT_B14>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B14_Service, PHB_PBDT_B14_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B14_DETAIL>, Repository<PHB_PBDT_B14_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B14_DETAIL_Service, PHB_PBDT_B14_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B14_TEMPLATE>, Repository<PHB_PBDT_B14_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B14_TEMPLATE_Service, PHB_PBDT_B14_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B1501
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1501>, Repository<PHB_PBDT_B1501>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1501_Service, PHB_PBDT_B1501_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1501_TEMPLATE>, Repository<PHB_PBDT_B1501_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1501_TEMPLATE_Service, PHB_PBDT_B1501_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1501_ROW>, Repository<PHB_PBDT_B1501_ROW>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1501_ROW_Service, PHB_PBDT_B1501_ROW_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1501_COLUMN>, Repository<PHB_PBDT_B1501_COLUMN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1501_COLUMN_Service, PHB_PBDT_B1501_COLUMN_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1501_DATA>, Repository<PHB_PBDT_B1501_DATA>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1501_DATA_Service, PHB_PBDT_B1501_DATA_Service>(new HierarchicalLifetimeManager());

            // B1502
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1502>, Repository<PHB_PBDT_B1502>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1502_Service, PHB_PBDT_B1502_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1502_TEMPLATE>, Repository<PHB_PBDT_B1502_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1502_TEMPLATE_Service, PHB_PBDT_B1502_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1502_ROW>, Repository<PHB_PBDT_B1502_ROW>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1502_ROW_Service, PHB_PBDT_B1502_ROW_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1502_COLUMN>, Repository<PHB_PBDT_B1502_COLUMN>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1502_COLUMN_Service, PHB_PBDT_B1502_COLUMN_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B1502_DATA>, Repository<PHB_PBDT_B1502_DATA>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B1502_DATA_Service, PHB_PBDT_B1502_DATA_Service>(new HierarchicalLifetimeManager());


            // B32
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B32>, Repository<PHB_PBDT_B32>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B32_Service, PHB_PBDT_B32_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B32_DETAIL>, Repository<PHB_PBDT_B32_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B32_DETAIL_Service, PHB_PBDT_B32_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B32_TEMPLATE>, Repository<PHB_PBDT_B32_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B32_TEMPLATE_Service, PHB_PBDT_B32_TEMPLATE_Service>(new HierarchicalLifetimeManager());


            // B121
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B121>, Repository<PHB_PBDT_B121>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B121_Serviec, PHB_PBDT_B121_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B121_DETAIL>, Repository<PHB_PBDT_B121_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B121_DETAIL_Service, PHB_PBDT_B121_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B121_TEMPLATE>, Repository<PHB_PBDT_B121_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B121_TEMPLATE_Service, PHB_PBDT_B121_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B122
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B122>, Repository<PHB_PBDT_B122>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B122_Service, PHB_PBDT_B122_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B122_DETAIL>, Repository<PHB_PBDT_B122_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B122_DETAIL_Service, PHB_PBDT_B122_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B122_TEMPLATE>, Repository<PHB_PBDT_B122_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B122_TEMPLATE_Service, PHB_PBDT_B122_TEMPLATE_Service>(new HierarchicalLifetimeManager());


            // B123
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B123>, Repository<PHB_PBDT_B123>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B123_Service, PHB_PBDT_B123_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B123_DETAIL>, Repository<PHB_PBDT_B123_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B123_DETAIL_Service, PHB_PBDT_B123_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B123_TEMPLATE>, Repository<PHB_PBDT_B123_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B123_TEMPLATE_Service, PHB_PBDT_B123_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B124
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B124>, Repository<PHB_PBDT_B124>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B124_Service, PHB_PBDT_B124_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B124_DETAIL>, Repository<PHB_PBDT_B124_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B124_DETAIL_Service, PHB_PBDT_B124_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B124_TEMPLATE>, Repository<PHB_PBDT_B124_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B124_TEMPLATE_Service, PHB_PBDT_B124_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            // B125
            container.RegisterType<IRepositoryAsync<PHB_PBDT_B125>, Repository<PHB_PBDT_B125>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B125_Service, PHB_PBDT_B125_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B125_DETAIL>, Repository<PHB_PBDT_B125_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B125_DETAIL_Service, PHB_PBDT_B125_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_B125_TEMPLATE>, Repository<PHB_PBDT_B125_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_B125_TEMPLATE_Service, PHB_PBDT_B125_TEMPLATE_Service>(new HierarchicalLifetimeManager());


            //
            // TT344
            //

            //B01
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B01>, Repository<PHB_PBDT_TT344_B01>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B01_Service, PHB_PBDT_TT344_B01_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B01_DETAIL>, Repository<PHB_PBDT_TT344_B01_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B01_DETAIL_Service, PHB_PBDT_TT344_B01_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B01_TEMPLATE>, Repository<PHB_PBDT_TT344_B01_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B01_TEMPLATE_Service, PHB_PBDT_TT344_B01_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B02
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B02>, Repository<PHB_PBDT_TT344_B02>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B02_Service, PHB_PBDT_TT344_B02_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B02_DETAIL>, Repository<PHB_PBDT_TT344_B02_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B02_DETAIL_Service, PHB_PBDT_TT344_B02_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B02_TEMPLATE>, Repository<PHB_PBDT_TT344_B02_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B02_TEMPLATE_Service, PHB_PBDT_TT344_B02_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B03
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B03>, Repository<PHB_PBDT_TT344_B03>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B03_Service, PHB_PBDT_TT344_B03_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B03_DETAIL>, Repository<PHB_PBDT_TT344_B03_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B03_DETAIL_Service, PHB_PBDT_TT344_B03_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B03_TEMPLATE>, Repository<PHB_PBDT_TT344_B03_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B03_TEMPLATE_Service, PHB_PBDT_TT344_B03_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B04
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B04>, Repository<PHB_PBDT_TT344_B04>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B04_Service, PHB_PBDT_TT344_B04_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B04_DETAIL>, Repository<PHB_PBDT_TT344_B04_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B04_DETAIL_Service, PHB_PBDT_TT344_B04_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B04_TEMPLATE>, Repository<PHB_PBDT_TT344_B04_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B04_TEMPLATE_Service, PHB_PBDT_TT344_B04_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B05
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B05>, Repository<PHB_PBDT_TT344_B05>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B05_Service, PHB_PBDT_TT344_B05_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B05_DETAIL>, Repository<PHB_PBDT_TT344_B05_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B05_DETAIL_Service, PHB_PBDT_TT344_B05_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B05_TEMPLATE>, Repository<PHB_PBDT_TT344_B05_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B05_TEMPLATE_Service, PHB_PBDT_TT344_B05_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B06
            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B06>, Repository<PHB_PBDT_TT344_B06>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B06_Service, PHB_PBDT_TT344_B06_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B06_DETAIL>, Repository<PHB_PBDT_TT344_B06_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B06_DETAIL_Service, PHB_PBDT_TT344_B06_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_TT344_B06_TEMPLATE>, Repository<PHB_PBDT_TT344_B06_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_TT344_B06_TEMPLATE_Service, PHB_PBDT_TT344_B06_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //
            // PBDT
            //

            //B01
            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B01>, Repository<PHB_PBDT_QLDT_B01>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B01_Service, PHB_PBDT_QLDT_B01_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B01_DETAIL>, Repository<PHB_PBDT_QLDT_B01_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B01_DETAIL_Service, PHB_PBDT_QLDT_B01_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B01_TEMPLATE>, Repository<PHB_PBDT_QLDT_B01_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B01_TEMPLATE_Service, PHB_PBDT_QLDT_B01_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B02
            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B02>, Repository<PHB_PBDT_QLDT_B02>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B02_Service, PHB_PBDT_QLDT_B02_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B02_DETAIL>, Repository<PHB_PBDT_QLDT_B02_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B02_DETAIL_Service, PHB_PBDT_QLDT_B02_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B02_TEMPLATE>, Repository<PHB_PBDT_QLDT_B02_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B02_TEMPLATE_Service, PHB_PBDT_QLDT_B02_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            //B03
            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B03>, Repository<PHB_PBDT_QLDT_B03>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B03_Service, PHB_PBDT_QLDT_B03_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B03_DETAIL>, Repository<PHB_PBDT_QLDT_B03_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B03_DETAIL_Service, PHB_PBDT_QLDT_B03_DETAIL_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PBDT_QLDT_B03_TEMPLATE>, Repository<PHB_PBDT_QLDT_B03_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PBDT_QLDT_B03_TEMPLATE_Service, PHB_PBDT_QLDT_B03_TEMPLATE_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<KEKHAICHUNGTU>, Repository<KEKHAICHUNGTU>>(new HierarchicalLifetimeManager());
            container.RegisterType<IkekhaichungtuService, kekhaichungtuService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<KEKHAICHUNGTUDETAIL>, Repository<KEKHAICHUNGTUDETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IkekhaichungtuDetailService, kekhaichungtuDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_BM16_TT344>, Repository<PHB_BM16_TT344>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBm16TT344Service, PhbBm16TT344Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_BM16_TT344_DETAIL>, Repository<PHB_BM16_TT344_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbBm16TT344DetailService, PhbBm16TT344DetailService>(new HierarchicalLifetimeManager());

            #endregion

            container.RegisterType<IRepositoryAsync<SP.PHB.ENTITY.Rp.DoiChieuTABMIS.PHC_DOICHIEUSOLIEUHEADER>, Repository<SP.PHB.ENTITY.Rp.DoiChieuTABMIS.PHC_DOICHIEUSOLIEUHEADER>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<SP.PHB.ENTITY.Rp.DoiChieuTABMIS.PHC_DOICHIEUSOLIEUDETAILS>, Repository<SP.PHB.ENTITY.Rp.DoiChieuTABMIS.PHC_DOICHIEUSOLIEUDETAILS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDoiChieuTABMISService, DoiChieuTABMISService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_DM_DOITUONGNOPTHUE>, Repository<PHB_DM_DOITUONGNOPTHUE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_DM_COQUANTHU>, Repository<PHB_DM_COQUANTHU>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_DM_TYLEDIEUTIET>, Repository<PHB_DM_TYLEDIEUTIET>>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmDoiTuongNopThueService, DmDoiTuongNopThueService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmCoQuanThuService, DmCoQuanThuService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDmTyLeDieuTietService, DmTyLeDieuTietService>(new HierarchicalLifetimeManager());


            #region PHA_BCTC
            container.RegisterType<IRepositoryAsync<PHA_B01_BCTC>, Repository<PHA_B01_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BCTCService, PhaB01BCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B01_BCTC_DETAIL>, Repository<PHA_B01_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BCTCDetailService, PhaB01BCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B01_BCTC_TEMPLATE>, Repository<PHA_B01_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BCTCTemplateService, PhaB01BCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B02_BCTC>, Repository<PHA_B02_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB02BCTCService, PhaB02BCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B02_BCTC_DETAIL>, Repository<PHA_B02_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB02BCTCDetailService, PhaB02BCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B02_BCTC_TEMPLATE>, Repository<PHA_B02_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB02BCTCTemplateService, PhaB02BCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B03A_BCTC>, Repository<PHB_B03A_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03ABCTCService, PhbB03ABCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B03A_BCTC_DETAIL>, Repository<PHB_B03A_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03ABCTCDetailService, PhbB03ABCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B03A_BCTC_TEMPLATE>, Repository<PHB_B03A_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03ABCTCTemplateService, PhbB03ABCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B03B_BCTC>, Repository<PHA_B03B_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB03BBCTCService, PhaB03BBCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B03B_BCTC_DETAIL>, Repository<PHA_B03B_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB03BBCTCDetailService, PhaB03BBCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B03B_BCTC_TEMPLATE>, Repository<PHA_B03B_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB03BBCTCTemplateService, PhaB03BBCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B04_BCTC>, Repository<PHA_B04_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB04BCTCService, PhaB04BCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B04_BCTC_DETAIL>, Repository<PHA_B04_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB04BCTCDetailService, PhaB04BCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B04_BCTC_TEMPLATE>, Repository<PHA_B04_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB04BCTCTemplateService, PhaB04BCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01_BSTT_2>, Repository<PHB_B01_BSTT_2>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BSTT2Service, PhbB01BSTT2Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01_BSTT_2_DETAIL>, Repository<PHB_B01_BSTT_2_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BSTT2DetailService, PhbB01BSTT2DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B01_BSTT_2_TEMPLATE>, Repository<PHB_B01_BSTT_2_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB01BSTT2TemplateService, PhbB01BSTT2TemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B01_BSTT_1_TEMPLATE>, Repository<PHA_B01_BSTT_1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BSTT1TemplateService, PhaB01BSTT1TemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B01_BSTT_1>, Repository<PHA_B01_BSTT_1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BSTT1Service, PhaB01BSTT1Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_B01_BSTT_1_DETAIL>, Repository<PHA_B01_BSTT_1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhaB01BSTT1DetailService, PhaB01BSTT1DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHA_BCTC_B01_BCTC_TH_TEMPLATE>, Repository<PHA_BCTC_B01_BCTC_TH_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHA_BCTC_B02_BCTC_TH_TEMPLATE>, Repository<PHA_BCTC_B02_BCTC_TH_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHA_BCTC_B03_BCTC_TH_TEMPLATE>, Repository<PHA_BCTC_B03_BCTC_TH_TEMPLATE>>(new HierarchicalLifetimeManager());

            container.RegisterType<IPha_bctc_b01_bctc_th_template_Service, Pha_bctc_b01_bctc_th_template_Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IPha_bctc_b02_bctc_th_template_Service, Pha_bctc_b02_bctc_th_template_Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IPha_bctc_b03_bctc_th_template_Service, Pha_bctc_b03_bctc_th_template_Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_F01_02_P1>, Repository<PHB_F01_02_P1>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02_P1Service, PhbF01_02_P1Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_F01_02_P1_DETAIL>, Repository<PHB_F01_02_P1_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02DetailService, PhbF01_02DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_F01_02_P1_TEMPLATE>, Repository<PHB_F01_02_P1_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbF01_02_P1TemplateService, PhbF01_02_P1TemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<BC04_BCTC_TT107>, Repository<BC04_BCTC_TT107>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBc04BCTCTT107Service, Bc04BCTCTT107Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<BC04_BCTC_TT107_TEMPLATE>, Repository<BC04_BCTC_TT107_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBc04BCTCTT107TemplateService, Bc04BCTCTT107TemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<BC04_BCTC_TT107_DETAILS>, Repository<BC04_BCTC_TT107_DETAILS>>(new HierarchicalLifetimeManager());
            container.RegisterType<IBc04BCTCTT107DetailService, Bc04BCTCTT107DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B05_BCTC>, Repository<PHB_B05_BCTC>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB05BCTCService, PhbB05BCTCService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B05_BCTC_TEMPLATE>, Repository<PHB_B05_BCTC_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB05BCTCTemplateService, PhbB05BCTCTemplateService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B05_BCTC_DETAIL>, Repository<PHB_B05_BCTC_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB05BCTCDetailService, PhbB05BCTCDetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B05_BCTC_WORK>, Repository<PHB_B05_BCTC_WORK>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB05BCTCWorkService, PhbB05BCTCWorkService>(new HierarchicalLifetimeManager());

            container.RegisterType<IXmlService_Ver001, XmlService_Ver001>(new HierarchicalLifetimeManager());
            #endregion

            #region PL_TT137
            container.RegisterType<IRepositoryAsync<PHB_PL01_TT137>, Repository<PHB_PL01_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PL01_TT137Service, PHB_PL01_TT137Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL01_TT137_DETAIL>, Repository<PHB_PL01_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PL01_TT137DetailService, PHB_PL01_TT137DetailService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_PL02_TT137>, Repository<PHB_PL02_TT137>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PL02_TT137Service, PHB_PL02_TT137Service>(new HierarchicalLifetimeManager());
            container.RegisterType<IRepositoryAsync<PHB_PL02_TT137_DETAIL>, Repository<PHB_PL02_TT137_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPHB_PL02_TT137DetailService, PHB_PL02_TT137DetailService>(new HierarchicalLifetimeManager());
            #endregion


            #region PHB_B03_TT90 PHB_B04_TT90

            container.RegisterType<IRepositoryAsync<PHB_B03_TT90>, Repository<PHB_B03_TT90>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03_TT90Service, B03_TT90Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B03_TT90_DETAIL>, Repository<PHB_B03_TT90_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03_TT90DetailService, B03_TT90_DETAILService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B03_TT90_TEMPLATE>, Repository<PHB_B03_TT90_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB03_TT90TemplateService, PHB_B03_TT90_TEMPLATEService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B04_TT90>, Repository<PHB_B04_TT90>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB04_TT90Service, B04_TT90Service>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B04_TT90_DETAIL>, Repository<PHB_B04_TT90_DETAIL>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB04_TT90DetailService, B04_TT90_DETAILService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<PHB_B04_TT90_TEMPLATE>, Repository<PHB_B04_TT90_TEMPLATE>>(new HierarchicalLifetimeManager());
            container.RegisterType<IPhbB04_TT90TemplateService, PHB_B04_TT90_TEMPLATEService>(new HierarchicalLifetimeManager());
            #endregion

            #region GHI LOG ĐỒNG BỘ DỮ LIỆU MISA
            container.RegisterType<IRepositoryAsync<SYS_LOG_SYNC_MISA>, Repository<SYS_LOG_SYNC_MISA>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysLogSyncMisaService, SysLogSyncMisaService>(new HierarchicalLifetimeManager());

            container.RegisterType<IRepositoryAsync<SYS_SCHEDULER>, Repository<SYS_SCHEDULER>>(new HierarchicalLifetimeManager());
            container.RegisterType<ISysSchedulerService, SysSchedulerService>(new HierarchicalLifetimeManager());
            #endregion

            config.DependencyResolver = new UnityResolver(container);
        }

        public static T Retrieve<T>()
        {
            return container.Resolve<T>();
        }
    }
}