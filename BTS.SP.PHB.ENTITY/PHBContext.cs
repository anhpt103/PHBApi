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
using BTS.SP.PHB.ENTITY.Rp.BIEU04_TT61;
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
using BTS.SP.PHB.ENTITY.Rp.BM14_TT144;
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
using BTS.SP.PHB.ENTITY.Rp.DoiChieuTABMIS;
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
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.BM05_BCTC;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_B01_BSTT_2;
using BTS.SP.PHB.ENTITY.Rp_PHA_BCTC.PHB_F01_02_P1;
using BTS.SP.PHB.ENTITY.Sys;
using Repository.Pattern.Ef6;
using System.Data.Entity;

namespace BTS.SP.PHB.ENTITY
{
    public class PHBContext : DataContext
    {
        public PHBContext() : base("name=STCConnection")
        {
        }
        public virtual DbSet<SYS_CHUCNANG> SYS_CHUCNANGs { get; set; }

        #region AUTH
        public virtual DbSet<AU_NGUOIDUNG> AU_NGUOIDUNGs { get; set; }
        public virtual DbSet<AU_NHOMQUYEN> AU_NHOMQUYENs { get; set; }
        public virtual DbSet<AU_NHOMQUYEN_CHUCNANG> AU_NHOMQUYEN_CHUCNANGs { get; set; }
        public virtual DbSet<AU_NGUOIDUNG_NHOMQUYEN> AU_NGUOIDUNG_NHOMQUYENs { get; set; }
        public virtual DbSet<AU_NGUOIDUNG_QUYEN> AU_NGUOIDUNG_QUYENs { get; set; }
        #endregion

        #region DM
        public virtual DbSet<DM_NGANHKT> DM_NGANHKTs { get; set; }
        public virtual DbSet<DM_CHUONG> DM_CHUONGs { get; set; }
        public virtual DbSet<DM_CTMUCTIEU> DM_CTMUCTIEUs { get; set; }
        public virtual DbSet<PHB_DM_BAOCAO> PHB_DM_BAOCAOs { get; set; }
        public virtual DbSet<PHB_DM_DUAN> PHB_DM_DUANs { get; set; }
        public virtual DbSet<PHB_DM_DVQHNS> PHB_DM_DVQHNSs { get; set; }
        public virtual DbSet<PHB_DM_HOATDONG> PHB_DM_HOATDONGs { get; set; }
        public virtual DbSet<PHB_DM_LOAI_CAPPHAT> PHB_DM_LOAI_CAPPHATs { get; set; }
        public virtual DbSet<PHB_DM_LOAIKHOAN> PHB_DM_LOAIKHOANs { get; set; }
        public virtual DbSet<PHB_DM_LOAINGANSACH> PHB_DM_LOAINGANSACHs { get; set; }
        public virtual DbSet<PHB_DM_NGUONNGANSACH> PHB_DM_NGUONNGANSACHs { get; set; }
        public virtual DbSet<PHB_DM_TAIKHOAN> PHB_DM_TAIKHOANs { get; set; }
        public virtual DbSet<PHB_DM_TSCD> PHB_DM_TSCDs { get; set; }

        public virtual DbSet<PHB_DM_NHOMMUCCHI> PHB_DM_NHOMMUCCHIs { get; set; }
        public virtual DbSet<PHB_DM_NOIDUNGKT> PHB_DM_NOIDUNGKTs { get; set; }
        public virtual DbSet<DM_DBHC> DM_DBHCs { get; set; }

        public virtual DbSet<DM_TKKHOBAC> DM_TKKHOBACs { get; set; }
        public virtual DbSet<PHB_DM_CANBO> PHB_DM_CANBOs { get; set; }
        public virtual DbSet<PHB_DM_TIENLUONG> PHB_DM_TIENLUONGs { get; set; }
        #endregion

        #region SYS
        public virtual DbSet<SYS_DVQHNS> SYS_DVQHNSs { get; set; }

        public DbSet<PHB_SYS_LOG_CHUCNANG> PHB_SYS_LOG_CHUCNANG { get; set; }
        public virtual DbSet<SYS_DVQHNS_QUANLY> SYS_DVQHNS_QUANLY { get; set; }

        #endregion

        #region REPORT
        public virtual DbSet<PHB_REPORT_FIELD> PHB_REPORT_FIELDs { get; set; }
        #region PHB_B02_TT137
        public virtual DbSet<PHB_B02_TT137> PHB_B02_TT137s { get; set; }
        public virtual DbSet<PHB_B02_TT137_TEMPLATE> PHB_B02_TT137_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_B02_TT137_DETAIL> PHB_B02_TT137_DETAILs { get; set; }
        #endregion
        #region PHB_BIEU03_TT137
        public virtual DbSet<PHB_BIEU03_TT137> PHB_BIEU03_TT137s { get; set; }
        public virtual DbSet<PHB_BIEU03_TT137_TEMPLATE> PHB_BIEU03_TT137_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_BIEU03_TT137_DETAIL> PHB_BIEU03_TT137_DETAILs { get; set; }
        #endregion

        #region BIEU03
        public virtual DbSet<PHB_BIEU03> PHB_BIEU03s { get; set; }
        public virtual DbSet<PHB_BIEU03_TEMPLATE> PHB_BIEU03_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_BIEU03_DETAIL> PHB_BIEU03_DETAILs { get; set; }
        #endregion

        #region BIEU01B
        public virtual DbSet<PHB_BIEU01B> PHB_BIEU01Bs { get; set; }
        public virtual DbSet<PHB_BIEU01B_DETAIL> PHB_BIEU01B_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU01B_TEMPLATE> PHB_BIEU01B_TEMPLATEs { get; set; }
        #endregion

        #region BIEU2A
        public virtual DbSet<PHB_BIEU2A> PHB_BIEU2As { get; set; }
        public virtual DbSet<PHB_BIEU2A_DETAIL> PHB_BIEU2A_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU2A_TEMPLATE> PHB_BIEU2A_TEMPLATEs { get; set; }
        #endregion

        #region BIEU2B
        public virtual DbSet<PHB_BIEU2B> PHB_BIEU2Bs { get; set; }
        public virtual DbSet<PHB_BIEU2B_DETAIL> PHB_BIEU2B_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU2B_TEMPLATE> PHB_BIEU2B_TEMPLATEs { get; set; }
        #endregion

        #region BIEU2C - Phan I
        public virtual DbSet<PHB_BIEU2CP1> PHB_BIEU2CP1s { get; set; }
        public virtual DbSet<PHB_BIEU2CP1_DETAIL> PHB_BIEU2CP1_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU2CP1_TEMPLATE> PHB_BIEU2CP1_TEMPLATEs { get; set; }
        #endregion

        #region BIEU3A
        public virtual DbSet<PHB_BIEU3A> PHB_BIEU3As { get; set; }
        public virtual DbSet<PHB_BIEU3A_DETAIL> PHB_BIEU3A_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU3A_TEMPLATE> PHB_BIEU3A_TEMPLATEs { get; set; }
        #endregion

        #region BIEU3BP1
        public virtual DbSet<PHB_BIEU3BP1> PHB_BIEU3BP1s { get; set; }
        public virtual DbSet<PHB_BIEU3BP1_TEMPLATE> PHB_BIEU3BP1_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_BIEU3BP1_DETAIL> PHB_BIEU3BP1_DETAILs { get; set; }

        #endregion

        #region BIEU3BP2
        public virtual DbSet<PHB_BIEU3BP2> PHB_BIEU3BP2s { get; set; }
        public virtual DbSet<PHB_BIEU3BP2_DETAIL> PHB_BIEU3BP2_DETAILs { get; set; }
        #endregion

        #region PHB_PL32_P2_TT01
        public virtual DbSet<PHB_PL32_P2_TT01> PHB_PL32_P2_TT01s { get; set; }
        public virtual DbSet<PHB_PL32_P2_TT01_DETAIL> PHB_PL32_P2_TT01_DETAILs { get; set; }
        #endregion

        #region BIEU4BP1
        public virtual DbSet<PHB_BIEU4BP1> PHB_BIEU4BP1s { get; set; }
        public virtual DbSet<PHB_BIEU4BP1_TEMPLATE> PHB_BIEU4BP1_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_BIEU4BP1_DETAIL> PHB_BIEU4BP1_DETAILs { get; set; }

        #endregion

        #region BIEU4BP2
        public virtual DbSet<PHB_BIEU4BP2> PHB_BIEU4BP2s { get; set; }
        public virtual DbSet<PHB_BIEU4BP2_DETAIL> PHB_BIEU4BP2_DETAILs { get; set; }
        #endregion

        #region BIEU4A
        public virtual DbSet<PHB_BIEU4A> PHB_BIEU4As { get; set; }
        public virtual DbSet<PHB_BIEU4A_DETAIL> PHB_BIEU4A_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU4A_TEMPLATE> PHB_BIEU4A_TEMPLATEs { get; set; }
        #endregion

        #region BIEU01A
        public virtual DbSet<PHB_BIEU01A> PHB_BIEU01As { get; set; }
        public virtual DbSet<PHB_BIEU01A_DETAIL> PHB_BIEU01A_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU01A_TEMPLATE> PHB_BIEU01A_TEMPLATEs { get; set; }
        #endregion

        #region F01-01BCQT
        public virtual DbSet<PHB_F01_01BCQT> PHB_F01_01BCQTs { get; set; }
        public virtual DbSet<PHB_F01_01BCQT_DETAIL> PHB_F01_01BCQT_DETAILs { get; set; }
        #endregion

        #region PL3.1
        public virtual DbSet<PHB_PL31> PHB_PL31s { get; set; }
        public virtual DbSet<PHB_PL31_DETAIL> PHB_PL31_DETAILs { get; set; }
        public virtual DbSet<PHB_PL31_TEMPLATE> PHB_PL31_TEMPLATEs { get; set; }
        #endregion

        #region PL4.1
        public virtual DbSet<PHB_PL41> PHB_PL41s { get; set; }
        public virtual DbSet<PHB_PL41_DETAIL> PHB_PL41_DETAILs { get; set; }
        public virtual DbSet<PHB_PL41_TEMPLATE> PHB_PL41_TEMPLATEs { get; set; }
        #endregion

        #region PL32_P1
        public virtual DbSet<PHB_PL32_P1_TT01> PHB_PL32_P1_TT01s { get; set; }
        public virtual DbSet<PHB_PL32_P1_TT01_DETAIL> PHB_PL32_P1_TT01_DETAILs { get; set; }
        #endregion

        #region PL42_P1
        public virtual DbSet<PHB_PL42_P1_TT01> PHB_PL42_P1_TT01s { get; set; }
        public virtual DbSet<PHB_PL42_P1_TT01_DETAIL> PHB_PL42_P1_TT01_DETAILs { get; set; }
        #endregion
        //#region B02CTHP1
        //public virtual DbSet<PHB_B02CTHP1> PHB_B02CTHP1s { get; set; }
        //public virtual DbSet<PHB_B02CTHP1_DETAIL> PHB_B02CTHP1_DETAILs { get; set; }
        //public virtual DbSet<PHB_B02CTHP1_TEMPLATE> PHB_B02CTHP1_TEMPLATEs { get; set; }
        //#endregion

        #region B01BCQT

        public virtual DbSet<PHB_B01BCQT> PHB_B01BCQTs { get; set; }
        public virtual DbSet<PHB_B01BCQT_DETAIL> PHB_B01BCQT_DETAILs { get; set; }
        public virtual DbSet<PHB_B01BCQT_TEMPLATE> PHB_B01BCQT_TEMPLATEs { get; set; }
        #endregion

        #region BIEU07TT344
        public virtual DbSet<PHB_BIEU07TT344> PHB_BIEU07TT344s { get; set; }
        public virtual DbSet<PHB_BIEU07TT344_DETAIL> PHB_BIEU07TT344_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU07TT344_TEMPLATE> PHB_BIEU07TT344_TEMPLATEs { get; set; }
        #endregion

        #region BIEU08TT344
        public virtual DbSet<PHB_BIEU08TT344> PHB_BIEU08TT344s { get; set; }
        public virtual DbSet<PHB_BIEU08TT344_DETAIL> PHB_BIEU08TT344_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU08TT344_TEMPLATE> PHB_BIEU08TT344_TEMPLATEs { get; set; }
        #endregion

        #region BIEU09TT344
        public virtual DbSet<PHB_BIEU09TT344> PHB_BIEU09TT344s { get; set; }
        public virtual DbSet<PHB_BIEU09TT344_DETAIL> PHB_BIEU09TT344_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU09TT344_TEMPLATE> PHB_BIEU09TT344_TEMPLATEs { get; set; }
        #endregion

        #region BIEU10TT344
        public virtual DbSet<PHB_BIEU10TT344> PHB_BIEU10TT344s { get; set; }
        public virtual DbSet<PHB_BIEU10TT344_DETAIL> PHB_BIEU10TT344_DETAILs { get; set; }
        #endregion

        #region B02BCQT

        public virtual DbSet<PHB_B02BCQT> PHB_B02BCQTs { get; set; }
        public virtual DbSet<PHB_B02BCQT_DETAIL> PHB_B02BCQT_DETAILs { get; set; }
        public virtual DbSet<PHB_B02BCQT_TEMPLATE> PHB_B02BCQT_TEMPLATEs { get; set; }
        #endregion

        #region BIEU01C - Phan I

        public virtual DbSet<PHB_BIEU01C> PHB_BIEU01Cs { get; set; }
        public virtual DbSet<PHB_BIEU01C_DETAIL> PHB_BIEU01C_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU01C_TEMPLATE> PHB_BIEU01C_TEMPLATEs { get; set; }
        #endregion

        #region B02H_II

        public virtual DbSet<PHB_B02H_II> PHB_B02H_IIs { get; set; }
        public virtual DbSet<PHB_B02H_II_DETAIL> PHB_B02H_II_DETAILs { get; set; }
        public virtual DbSet<PHB_B02H_II_TEMPLATE> PHB_B02H_II_TEMPLATEs { get; set; }
        #endregion

        #region BIEU2C - Phan II

        public virtual DbSet<PHB_BIEU2CP2> PHB_BIEU2CP2s { get; set; }
        public virtual DbSet<PHB_BIEU2CP2_DETAIL> PHB_BIEU2CP2_DETAILs { get; set; }
        #endregion

        #region BIEU01CP2

        public virtual DbSet<PHB_BIEU01CP2> PHB_BIEU01CP2s { get; set; }
        public virtual DbSet<PHB_BIEU01CP2_DETAIL> PHB_BIEU01CP2_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU01CP2_TEMPLATE> PHB_BIEU01CP2_TEMPLATEs { get; set; }
        #endregion

        #region F01-02BCQT_PII
        public virtual DbSet<PHB_F01_02BCQT_PII> PHB_F01_02BCQT_PIIs { get; set; }
        public virtual DbSet<PHB_F01_02BCQT_PII_DETAIL> PHB_F01_02BCQT_PII_DETAILs { get; set; }
        #endregion

        #region BIEU11TT342
        public virtual DbSet<PHB_BIEU11TT344> PHB_BIEU11TT344s { get; set; }
        public virtual DbSet<PHB_BIEU11TT344_DETAIL> PHB_BIEU11TT344_DETAILs { get; set; }

        #endregion

        #region F01_02BCQT

        public virtual DbSet<PHB_F01_02BCQT> PHB_F01_02BCQTs { get; set; }
        public virtual DbSet<PHB_F01_02BCQT_DETAIL> PHB_F01_02BCQT_DETAILs { get; set; }
        public virtual DbSet<PHB_F01_02BCQT_TEMPLATE> PHB_F01_02BCQT_TEMPLATEs { get; set; }
        #endregion

        #region BIEU12TT342
        public virtual DbSet<PHB_BIEU12TT344> PHB_BIEU12TT344s { get; set; }
        public virtual DbSet<PHB_BIEU12TT344_DETAIL> PHB_BIEU12TT344_DETAILs { get; set; }

        #endregion

        #region 70NS TT 342
        public virtual DbSet<PHB_BIEU70NS> PHB_BIEU70NSs { get; set; }
        public virtual DbSet<PHB_BIEU70NS_DETAIL> PHB_BIEU70NS_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU70NS_TEMPLATE> PHB_BIEU70NS_TEMPLATEs { get; set; }

        #endregion

        #region 69NS-TT342
        public virtual DbSet<PHB_BIEU69NS> PHB_BIEU69NSs { get; set; }
        public virtual DbSet<PHB_BIEU69NS_DETAIL> PHB_BIEU69NS_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU69NS_TEMPLATE> PHB_BIEU69NS_TEMPLATEs { get; set; }

        #endregion

        #region 68NS-TT342
        public virtual DbSet<PHB_BIEU68NS> PHB_BIEU68NSs { get; set; }
        public virtual DbSet<PHB_BIEU68NS_DETAIL> PHB_BIEU68NS_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU68NS_TEMPLATE> PHB_BIEU68NS_TEMPLATEs { get; set; }

        #endregion

        #region 67NS-TT342
        public virtual DbSet<PHB_BIEU67NS> PHB_BIEU67NSs { get; set; }
        public virtual DbSet<PHB_BIEU67NS_DETAIL> PHB_BIEU67NS_DETAILs { get; set; }
        public virtual DbSet<PHB_BIEU67NS_TEMPLATE> PHB_BIEU67NS_TEMPLATEs { get; set; }

        #endregion

        #region Đối chiếu số liệu
        public virtual DbSet<PHB_DOICHIEUSOLIEU> PHB_DOICHIEUSOLIEUs { get; set; }

        #endregion

        #region B03BCQT_BII1
        public virtual DbSet<PHB_B03BCQT_BII1> PHB_B03BCQT_BII1s { get; set; }
        public virtual DbSet<PHB_B03BCQT_BII1_DETAIL> PHB_B03BCQT_BII1_DETAILs { get; set; }
        public virtual DbSet<PHB_B03BCQT_BII1_TEMPLATE> PHB_B03BCQT_BII1_TEMPLATEs { get; set; }
        #endregion

        #region PHB_C_B03_X
        public virtual DbSet<PHB_C_B03X> PHB_C_B03_Xs { get; set; }
        public virtual DbSet<PHB_C_B03X_DETAIL> PHB_C_B03X_DETAILs { get; set; }
        #endregion

        #region PHB_C_B03B_X
        public virtual DbSet<PHB_C_B03B_X> PHB_C_B03B_Xs { get; set; }
        public virtual DbSet<PHB_C_B03B_X_DETAIL> PHB_C_B03B_X_DETAILs { get; set; }
        #endregion

        #region PHB_C_B01X
        public virtual DbSet<PHB_C_B01X> PHB_C_B01Xs { get; set; }
        public virtual DbSet<PHB_C_B01X_DETAIL> PHB_C_B01X_DETAILs { get; set; }
        public virtual DbSet<PHB_C_B01X_TEMPLATE> PHB_C_B01X_TEMPLATEs { get; set; }
        #endregion

        #region PHB_L_PC_UB
        public virtual DbSet<PHB_L_PC_UB> PHB_L_PC_UBs { get; set; }
        public virtual DbSet<PHB_L_PC_UB_DETAIL> PHB_L_PC_UB_DETAILs { get; set; }
        #endregion

        #region PHB_L_PC_D
        public virtual DbSet<PHB_L_PC_D> PHB_L_PC_Ds { get; set; }
        public virtual DbSet<PHB_L_PC_D_DETAIL> PHB_L_PC_D_DETAILs { get; set; }
        #endregion

        #region PHB_L_PC_DT
        public virtual DbSet<PHB_L_PC_DT> PHB_L_PC_DTs { get; set; }
        public virtual DbSet<PHB_L_PC_DT_DETAIL> PHB_L_PC_DT_DETAILs { get; set; }
        #endregion

        #region PHB_C_B03A_X
        public virtual DbSet<PHB_C_B03A_X> PHB_C_B03A_Xs { get; set; }
        public virtual DbSet<PHB_C_B03A_X_DETAIL> PHB_C_B03A_X_DETAILs { get; set; }
        #endregion

        #region PHB_C_B03b_X
        public virtual DbSet<PHB_C_B02AX> PHB_C_B02AXs { get; set; }
        public virtual DbSet<PHB_C_B02AX_DETAIL> PHB_C_B02AX_DETAILs { get; set; }
        #endregion


        #region PHB_C_B03D_X
        public virtual DbSet<PHB_C_B03D_X> PHB_C_B03D_Xs { get; set; }
        public virtual DbSet<PHB_C_B03D_X_DETAIL> PHB_C_B03D_X_DETAILs { get; set; }
        public virtual DbSet<PHB_C_B03D_X_TEMPLATE> PHB_C_B03D_X_TEMPLATEs { get; set; }
        #endregion

        #region PHB_C_B02B_X
        public virtual DbSet<PHB_C_B02B_X> PHB_C_B02B_Xs { get; set; }
        public virtual DbSet<PHB_C_B02B_X_DETAIL> PHB_C_B02B_X_DETAILs { get; set; }
        #endregion

        #region PHB_C_B03C_X
        public virtual DbSet<PHB_C_B03C_X> PHB_C_B03C_X { get; set; }
        public virtual DbSet<PHB_C_B03C_X_DETAIL> PHB_C_B03C_X_DETAILs { get; set; }
        #endregion

        #region PHB_BIEU04_TT61
        public virtual DbSet<PHB_BIEU04_TT61> PHB_BIEU04_TT61S { get; set; }
        public virtual DbSet<PHB_BIEU04_TT61_DETAILS> PHB_BIEU04_TT61_DETAILSs { get; set; }
        public virtual DbSet<PHB_BIEU04_TT61_TEMPLATE> PHB_BIEU04_TT61_TEMPLATEs { get; set; }
        #endregion
        #endregion

        #region PHB_C_B04X
        public virtual DbSet<PHB_C_B04X> PHB_C_B04Xs { get; set; }
        public virtual DbSet<PHB_C_B04X_DETAIL> PHB_C_B04X_DETAILs { get; set; }
        public virtual DbSet<PHB_C_B04X_DETAIL_TSCD> PHB_C_B04X_DETAIL_TSCD { get; set; }
        #endregion

        #region PHB_C_B06X
        public virtual DbSet<PHB_C_B06X> PHB_C_B06Xs { get; set; }
        public virtual DbSet<PHB_C_B06X_DETAIL> PHB_C_B06X_DETAILs { get; set; }
        public virtual DbSet<PHB_C_B06X_TEMPLATE> PHB_C_B06X_TEMPLATEs { get; set; }
        #endregion

        #region DM_PHC
        public virtual DbSet<DM_PHC_CHITIEUTHU_CHI> DM_PHC_CHITIEUTHU_CHIs { get; set; }
        #endregion

        #region DOICHIEU_TABMIS
        public virtual DbSet<PHC_DOICHIEUSOLIEUHEADER> PHC_DOICHIEUSOLIEUHEADERs { get; set; }
        public virtual DbSet<PHC_DOICHIEUSOLIEUDETAILS> PHC_DOICHIEUSOLIEUDETAILSs { get; set; }
        #endregion

        #region DM DOITUONGNOPTHUE DM_COQUANTHU DM_TYLEDIEUTIET
        public virtual DbSet<PHB_DM_DOITUONGNOPTHUE> PHB_DM_DOITUONGNOPTHUEs { get; set; }
        public virtual DbSet<PHB_DM_COQUANTHU> PHB_DM_COQUANTHUs { get; set; }
        public virtual DbSet<PHB_DM_TYLEDIEUTIET> PHB_DM_TYLEDIEUTIETs { get; set; }
        #endregion

        #region PHA_BCTC
        public virtual DbSet<PHA_B01_BCTC> PHA_B01_BCTCs { get; set; }
        public virtual DbSet<PHA_B01_BCTC_TEMPLATE> PHA_B01_BCTC_TEMPLATEs { get; set; }
        public virtual DbSet<PHA_B01_BCTC_DETAIL> PHA_B01_BCTC_DETAILs { get; set; }

        public virtual DbSet<PHA_B02_BCTC> PHA_B02_BCTCs { get; set; }
        public virtual DbSet<PHA_B02_BCTC_TEMPLATE> PHA_B02_BCTC_TEMPLATEs { get; set; }
        public virtual DbSet<PHA_B02_BCTC_DETAIL> PHA_B02_BCTC_DETAILs { get; set; }

        public virtual DbSet<PHB_B03A_BCTC> PHB_B03A_BCTCs { get; set; }
        public virtual DbSet<PHB_B03A_BCTC_DETAIL> PHB_B03A_BCTC_DETAILs { get; set; }
        public virtual DbSet<PHB_B03A_BCTC_TEMPLATE> PHB_B03A_BCTC_TEMPLATEs { get; set; }

        public virtual DbSet<PHA_B03B_BCTC> PHA_B03B_BCTCs { get; set; }
        public virtual DbSet<PHA_B03B_BCTC_DETAIL> PHA_B03B_BCTC_DETAILs { get; set; }
        public virtual DbSet<PHA_B03B_BCTC_TEMPLATE> PHA_B03B_BCTC_TEMPLATEs { get; set; }

        public virtual DbSet<PHA_B04_BCTC> PHA_B04_BCTCs { get; set; }
        public virtual DbSet<PHA_B04_BCTC_DETAIL> PHA_B04_BCTC_DETAILs { get; set; }
        public virtual DbSet<PHA_B04_BCTC_TEMPLATE> PHA_B04_BCTC_TEMPLATEs { get; set; }

        public virtual DbSet<PHA_B01_BSTT_1> PHA_B01_BSTT_1s { get; set; }
        public virtual DbSet<PHA_B01_BSTT_1_DETAIL> PHA_B01_BSTT_1_DETAILs { get; set; }
        public virtual DbSet<PHA_B01_BSTT_1_TEMPLATE> PHA_B01_BSTT_1_TEMPLATEs { get; set; }

        public virtual DbSet<PHB_B01_BSTT_2> PHB_B01_BSTT_2s { get; set; }
        public virtual DbSet<PHB_B01_BSTT_2_DETAIL> PHB_B01_BSTT_2_DETAILs { get; set; }
        public virtual DbSet<PHB_B01_BSTT_2_TEMPLATE> PHB_B01_BSTT_2_TEMPLATEs { get; set; }

        public virtual DbSet<PHA_BCTC_B01_BCTC_TH_TEMPLATE> PHA_BCTC_B01_BCTC_TH_TEMPLATE { get; set; }
        public virtual DbSet<PHA_BCTC_B02_BCTC_TH_TEMPLATE> PHA_BCTC_B02_BCTC_TH_TEMPLATE { get; set; }
        public virtual DbSet<PHA_BCTC_B03_BCTC_TH_TEMPLATE> PHA_BCTC_B03_BCTC_TH_TEMPLATE { get; set; }

        public virtual DbSet<PHB_F01_02_P1> PHB_F01_02_P1s { get; set; }
        public virtual DbSet<PHB_F01_02_P1_DETAIL> PHB_F01_02_P1_DETAILs { get; set; }
        public virtual DbSet<PHB_F01_02_P1_TEMPLATE> PHB_F01_02_P1_TEMPLATEs { get; set; }

        public virtual DbSet<BC04_BCTC_TT107> BC04_BCTC_TT107s { get; set; }
        public virtual DbSet<BC04_BCTC_TT107_DETAILS> BC04_BCTC_TT107_DETAILSs { get; set; }
        public virtual DbSet<BC04_BCTC_TT107_TEMPLATE> BC04_BCTC_TT107_TEMPLATEs { get; set; }

        public virtual DbSet<PHB_B05_BCTC> PHB_B05_BBCTCs { get; set; }
        public virtual DbSet<PHB_B05_BCTC_DETAIL> PHB_B05_BBCTC_DETAILs { get; set; }
        public virtual DbSet<PHB_B05_BCTC_TEMPLATE> PHB_B05_BCTC_TEMPLATEs { get; set; }
        public virtual DbSet<PHB_B05_BCTC_WORK> PHB_B05_BCTC_WORKs { get; set; }
        #endregion

        #region DAUVAO_PHB
        public virtual DbSet<PHB_BM48_TT342> PHB_BM48_TT342s { get; set; }
        public virtual DbSet<PHB_BM48_TT342_DETAIL> PHB_BM48_TT342_DETAILs { get; set; }
        public virtual DbSet<PHB_BM48_TT342_TEMPLATE> PHB_BM48_TT342_TEMPLATEs { get; set; }


        public virtual DbSet<PHB_B01A_TT137> PHB_B01A_TT137s { get; set; }
        public virtual DbSet<PHB_B01A_TT137_DETAIL> PHB_B01A_TT137_DETAILs { get; set; }
        public virtual DbSet<PHB_B01A_TT137_TEMPLATE> PHB_B01A_TT137_TEMPLATEs { get; set; }

        public virtual DbSet<PHB_B01B_TT137> PHB_B01B_TT137s { get; set; }
        public virtual DbSet<PHB_B01B_TT137_DETAIL> PHB_B01B_TT137_DETAILs { get; set; }
        public virtual DbSet<PHB_B01B_TT137_TEMPLATE> PHB_B01B_TT137_TEMPLATEs { get; set; }

        public virtual DbSet<PHB_B01BDG_TT137> PHB_B01BDG_TT137s { get; set; }
        public virtual DbSet<PHB_B01BDG_TT137_DETAIL> PHB_B01BDG_TT137_DETAILs { get; set; }
        public virtual DbSet<PHB_B01BDG_TT137_TEMPLATE> PHB_B01BDG_TT137_TEMPLATEs { get; set; }


        public virtual DbSet<PHB_DUTOANLUONG> PHB_DUTOANLUONGs { get; set; }
        public virtual DbSet<PHB_DUTOANLUONG_DETAIL> PHB_DUTOANLUONG_DETAILs { get; set; }
        public virtual DbSet<PHB_DUTOANLUONG_TEMPLATE> PHB_DUTOANLUONG_TEMPLATEs { get; set; }

        #region B03BCTC

        public virtual DbSet<PHB_B03BBCTC> PHB_B03BBCTCs { get; set; }
        public virtual DbSet<PHB_B03BBCTC_DETAIL> PHB_B03BBCTC_DETAILs { get; set; }
        public virtual DbSet<PHB_B03BBCTC_TEMPLATE> PHB_B03BBCTC_TEMPLATEs { get; set; }
        #endregion

        #endregion

        #region PHB_PBDT
        public virtual DbSet<BC_NHAPDT_XA_DETAIL> BC_NHAPDTXA_DETAIL { get; set; }
        #endregion

        #region DM_CHITIEU_BAOCAO
        public virtual DbSet<DM_CHITIEU_BAOCAO> DM_CHITIEU_BAOCAO { get; set; }
        #endregion

        #region PBDT
        public virtual DbSet<PHB_PBDT_B05> PHB_PBDT_B05 { get; set; }
        public virtual DbSet<PHB_PBDT_B05_DETAIL> PHB_PBDT_B05_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B05_TEMPLATE> PHB_PBDT_B05_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B06> PHB_PBDT_B06 { get; set; }
        public virtual DbSet<PHB_PBDT_B06_DETAIL> PHB_PBDT_B06_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B06_DONVI> PHB_PBDT_B06_DONVI { get; set; }
        public virtual DbSet<PHB_PBDT_B06_DATA> PHB_PBDT_B06_DATA { get; set; }
        public virtual DbSet<PHB_PBDT_B06_TEMPLATE> PHB_PBDT_B06_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B07> PHB_PBDT_B07 { get; set; }
        public virtual DbSet<PHB_PBDT_B07_DETAIL> PHB_PBDT_B07_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B07_TEMPLATE> PHB_PBDT_B07_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B111> PHB_PBDT_B111 { get; set; }
        public virtual DbSet<PHB_PBDT_B111_DETAIL> PHB_PBDT_B111_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B111_TEMPLATE> PHB_PBDT_B111_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B121> PHB_PBDT_B121 { get; set; }
        public virtual DbSet<PHB_PBDT_B121_DETAIL> PHB_PBDT_B121_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B121_TEMPLATE> PHB_PBDT_B121_TEMPLATE { get; set; }


        public virtual DbSet<PHB_PBDT_B122> PHB_PBDT_B122 { get; set; }
        public virtual DbSet<PHB_PBDT_B122_DETAIL> PHB_PBDT_B122_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B122_TEMPLATE> PHB_PBDT_B122_TEMPLATE { get; set; }


        public virtual DbSet<PHB_PBDT_B123> PHB_PBDT_B123 { get; set; }
        public virtual DbSet<PHB_PBDT_B123_DETAIL> PHB_PBDT_B123_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B123_TEMPLATE> PHB_PBDT_B123_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B124> PHB_PBDT_B124 { get; set; }
        public virtual DbSet<PHB_PBDT_B124_DETAIL> PHB_PBDT_B124_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B124_TEMPLATE> PHB_PBDT_B124_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B125> PHB_PBDT_B125 { get; set; }
        public virtual DbSet<PHB_PBDT_B125_DETAIL> PHB_PBDT_B125_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B125_TEMPLATE> PHB_PBDT_B125_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1301> PHB_PBDT_B1301 { get; set; }
        public virtual DbSet<PHB_PBDT_B1301_DETAIL> PHB_PBDT_B1301_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1301_TEMPLATE> PHB_PBDT_B1301_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1302> PHB_PBDT_B1302 { get; set; }
        public virtual DbSet<PHB_PBDT_B1302_DETAIL> PHB_PBDT_B1302_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1302_TEMPLATE> PHB_PBDT_B1302_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1303> PHB_PBDT_B1303 { get; set; }
        public virtual DbSet<PHB_PBDT_B1303_DETAIL> PHB_PBDT_B1303_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1303_TEMPLATE> PHB_PBDT_B1303_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1304> PHB_PBDT_B1304 { get; set; }
        public virtual DbSet<PHB_PBDT_B1304_DETAIL> PHB_PBDT_B1304_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1304_TEMPLATE> PHB_PBDT_B1304_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1305> PHB_PBDT_B1305 { get; set; }
        public virtual DbSet<PHB_PBDT_B1305_DETAIL> PHB_PBDT_B1305_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1305_TEMPLATE> PHB_PBDT_B1305_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1306> PHB_PBDT_B1306 { get; set; }
        public virtual DbSet<PHB_PBDT_B1306_DETAIL> PHB_PBDT_B1306_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1306_TEMPLATE> PHB_PBDT_B1306_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1307> PHB_PBDT_B1307 { get; set; }
        public virtual DbSet<PHB_PBDT_B1307_DETAIL> PHB_PBDT_B1307_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1307_TEMPLATE> PHB_PBDT_B1307_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1308> PHB_PBDT_B1308 { get; set; }
        public virtual DbSet<PHB_PBDT_B1308_DETAIL> PHB_PBDT_B1308_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1308_TEMPLATE> PHB_PBDT_B1308_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1309> PHB_PBDT_B1309 { get; set; }
        public virtual DbSet<PHB_PBDT_B1309_DETAIL> PHB_PBDT_B1309_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1309_TEMPLATE> PHB_PBDT_B1309_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1310> PHB_PBDT_B1310 { get; set; }
        public virtual DbSet<PHB_PBDT_B1310_DETAIL> PHB_PBDT_B1310_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1310_TEMPLATE> PHB_PBDT_B1310_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1311> PHB_PBDT_B1311 { get; set; }
        public virtual DbSet<PHB_PBDT_B1311_DETAIL> PHB_PBDT_B1311_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1311_TEMPLATE> PHB_PBDT_B1311_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1312> PHB_PBDT_B1312 { get; set; }
        public virtual DbSet<PHB_PBDT_B1312_DETAIL> PHB_PBDT_B1312_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B1312_TEMPLATE> PHB_PBDT_B1312_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B14> PHB_PBDT_B14 { get; set; }
        public virtual DbSet<PHB_PBDT_B14_DETAIL> PHB_PBDT_B14_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B14_TEMPLATE> PHB_PBDT_B14_TEMPLATE { get; set; }

        public virtual DbSet<PHB_PBDT_B1501> PHB_PBDT_B1501 { get; set; }
        public virtual DbSet<PHB_PBDT_B1501_TEMPLATE> PHB_PBDT_B1501_TEMPLATE { get; set; }
        public virtual DbSet<PHB_PBDT_B1501_COLUMN> PHB_PBDT_B1501_COLUMN { get; set; }
        public virtual DbSet<PHB_PBDT_B1501_ROW> PHB_PBDT_B1501_ROW { get; set; }
        public virtual DbSet<PHB_PBDT_B1501_DATA> PHB_PBDT_B1501_DATA { get; set; }

        public virtual DbSet<PHB_PBDT_B1502> PHB_PBDT_B1502 { get; set; }
        public virtual DbSet<PHB_PBDT_B1502_TEMPLATE> PHB_PBDT_B1502_TEMPLATE { get; set; }
        public virtual DbSet<PHB_PBDT_B1502_COLUMN> PHB_PBDT_B1502_COLUMN { get; set; }
        public virtual DbSet<PHB_PBDT_B1502_ROW> PHB_PBDT_B1502_ROW { get; set; }
        public virtual DbSet<PHB_PBDT_B1502_DATA> PHB_PBDT_B1502_DATA { get; set; }


        public virtual DbSet<PHB_PBDT_B32> PHB_PBDT_B32 { get; set; }
        public virtual DbSet<PHB_PBDT_B32_DETAIL> PHB_PBDT_B32_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_B32_TEMPLATE> PHB_PBDT_B32_TEMPLATE { get; set; }

        //
        // TT344
        //

        // B01
        public virtual DbSet<PHB_PBDT_TT344_B01> PHB_PBDT_TT344_B01 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B01_DETAIL> PHB_PBDT_TT344_B01_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B01_TEMPLATE> PHB_PBDT_TT344_B01_TEMPLATE { get; set; }

        // B02
        public virtual DbSet<PHB_PBDT_TT344_B02> PHB_PBDT_TT344_B02 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B02_DETAIL> PHB_PBDT_TT344_B02_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B02_TEMPLATE> PHB_PBDT_TT344_B02_TEMPLATE { get; set; }


        // B03
        public virtual DbSet<PHB_PBDT_TT344_B03> PHB_PBDT_TT344_B03 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B03_DETAIL> PHB_PBDT_TT344_B03_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B03_TEMPLATE> PHB_PBDT_TT344_B03_TEMPLATE { get; set; }


        // B04
        public virtual DbSet<PHB_PBDT_TT344_B04> PHB_PBDT_TT344_B04 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B04_DETAIL> PHB_PBDT_TT344_B04_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B04_TEMPLATE> PHB_PBDT_TT344_B04_TEMPLATE { get; set; }


        // B05
        public virtual DbSet<PHB_PBDT_TT344_B05> PHB_PBDT_TT344_B05 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B05_DETAIL> PHB_PBDT_TT344_B05_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B05_TEMPLATE> PHB_PBDT_TT344_B05_TEMPLATE { get; set; }


        // B06
        public virtual DbSet<PHB_PBDT_TT344_B06> PHB_PBDT_TT344_B06 { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B06_DETAIL> PHB_PBDT_TT344_B06_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_TT344_B06_TEMPLATE> PHB_PBDT_TT344_B06_TEMPLATE { get; set; }

        //
        // QLDT
        //
        // B01
        public virtual DbSet<PHB_PBDT_QLDT_B01> PHB_PBDT_QLDT_B01 { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B01_DETAIL> PHB_PBDT_QLDT_B01_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B01_TEMPLATE> PHB_PBDT_QLDT_B01_TEMPLATE { get; set; }

        // B02
        public virtual DbSet<PHB_PBDT_QLDT_B02> PHB_PBDT_QLDT_B02 { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B02_DETAIL> PHB_PBDT_QLDT_B02_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B02_TEMPLATE> PHB_PBDT_QLDT_B02_TEMPLATE { get; set; }

        // B03
        public virtual DbSet<PHB_PBDT_QLDT_B03> PHB_PBDT_QLDT_B03 { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B03_DETAIL> PHB_PBDT_QLDT_B03_DETAIL { get; set; }
        public virtual DbSet<PHB_PBDT_QLDT_B03_TEMPLATE> PHB_PBDT_QLDT_B03_TEMPLATE { get; set; }
        #endregion
        #region BM14_TT134
        public virtual DbSet<PHB_BM14_TT134> PHB_BM14_TT134s { get; set; }
        public virtual DbSet<PHB_BM14_TT134_DETAIL> PHB_BM14_TT134_DETAILs { get; set; }
        #endregion
        #region
        public virtual DbSet<KEKHAICHUNGTU> KEKHAICHUNGTUs { get; set; }
        public virtual DbSet<KEKHAICHUNGTUDETAIL> KEKHAICHUNGTUDETAILs { get; set; }
        #endregion

        #region PL_TT137
        public virtual DbSet<PHB_PL01_TT137> PHB_PL01_TT137s { get; set; }
        public virtual DbSet<PHB_PL01_TT137_DETAIL> PHB_PL01_TT137_DETAILs { get; set; }
        public virtual DbSet<PHB_PL02_TT137> PHB_PL02_TT137s { get; set; }
        public virtual DbSet<PHB_PL02_TT137_DETAIL> PHB_PL02_TT137_DETAILs { get; set; }
        #endregion

        #region
        public virtual DbSet<PHB_BM16_TT344> PHB_BM16_TT344s { get; set; }
        public virtual DbSet<PHB_BM16_TT344_DETAIL> PHB_BM16_TT344_DETAILs { get; set; }
        #endregion

        #region B03_TT90 B04_TT90

        public virtual DbSet<PHB_B03_TT90> PHB_B03_TT90s { get; set; }
        public virtual DbSet<PHB_B03_TT90_DETAIL> PHB_B03_TT90_DETAILs { get; set; }
        public virtual DbSet<PHB_B03_TT90_TEMPLATE> PHB_B03_TT90_TEMPLATEs { get; set; }

        public virtual DbSet<PHB_B04_TT90> PHB_B04_TT90s { get; set; }
        public virtual DbSet<PHB_B04_TT90_DETAIL> PHB_B04_TT90_DETAILs { get; set; }
        public virtual DbSet<PHB_B04_TT90_TEMPLATE> PHB_B04_TT90_TEMPLATEs { get; set; }
        #endregion

        #region
        /* Entity Table Đồng bộ dữ liệu MISA */
        public virtual DbSet<SYS_LOG_SYNC_MISA> SYS_LOG_SYNC_MISAs { get; set; }
        public virtual DbSet<SYS_SCHEDULER> SYS_SCHEDULERs { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BTSTC");
            base.OnModelCreating(modelBuilder);
        }
    }
}
