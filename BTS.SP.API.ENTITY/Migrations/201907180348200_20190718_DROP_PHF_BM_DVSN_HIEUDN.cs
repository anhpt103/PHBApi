namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190718_DROP_PHF_BM_DVSN_HIEUDN : DbMigration
    {
        public override void Up()
        {
            DropTable("BTSTC.PHF_BM_01TT_DVSN");
            DropTable("BTSTC.PHF_BM_02TT_DVSN");
            DropTable("BTSTC.PHF_BM_03TT_DVSN");
            DropTable("BTSTC.PHF_BM_04TT_DVSN");
            DropTable("BTSTC.PHF_BM_05TT_DVSN");
            DropTable("BTSTC.PHF_BM_06TT_DVSN");
            DropTable("BTSTC.PHF_BM_07TT_DVSN");
            DropTable("BTSTC.PHF_BM_08TT_DVSN");
            DropTable("BTSTC.PHF_BM_09TT_DVSN");
            DropTable("BTSTC.PHF_BM_FILE_DVSN");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_BM_FILE_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 250),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DOT = c.String(maxLength: 50),
                        THOIGIAN = c.String(maxLength: 30),
                        TEN_BIEUMAU = c.String(maxLength: 200),
                        TIEUDE_BIEUMAU = c.String(maxLength: 500),
                        MA_DOITUONG = c.String(maxLength: 50),
                        LOAI_BAOCAO = c.String(maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_09TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        HOTEN = c.String(maxLength: 1000),
                        SOLIEU_DV_CHIUTHUE = c.Decimal(precision: 18, scale: 2),
                        SOLIEU_DV_PHAINOP = c.Decimal(precision: 18, scale: 2),
                        THANHTRA_DV_CHIUTHUE = c.Decimal(precision: 18, scale: 2),
                        THANHTRA_DV_PHAINOP = c.Decimal(precision: 18, scale: 2),
                        NGUYENNHAN = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_08TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_CHI = c.String(maxLength: 1000),
                        SOTIEN = c.Decimal(precision: 18, scale: 2),
                        NGUYENNHAN_TRICH_CAOHON = c.String(),
                        NGUYENNHAN_TRICH_SAINGUON = c.String(),
                        NGUYENNHAN_TRICH_SAI_TYLE = c.String(),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_07TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG = c.String(maxLength: 1000),
                        BAOCAO_DONVI = c.Decimal(precision: 18, scale: 2),
                        THANHTRA_XACDINH = c.Decimal(precision: 18, scale: 2),
                        NGUYENNHAN = c.String(),
                        IS_BOLD = c.Decimal(precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_06TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_CHI = c.String(maxLength: 1000),
                        SOTIEN = c.Decimal(precision: 18, scale: 2),
                        TITLE_NGUYENNHAN = c.String(),
                        NGUYENNHAN = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_05TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        HOTEN = c.String(maxLength: 1000),
                        CHILUONG_SAI_CHEDO = c.Decimal(precision: 18, scale: 2),
                        CHIBH_SAI_CHEDO = c.Decimal(precision: 18, scale: 2),
                        CHITN_SAI_CHEDO = c.Decimal(precision: 18, scale: 2),
                        CHI_KHAC = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_04TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_THU = c.String(maxLength: 1000),
                        SOTIEN = c.Decimal(precision: 18, scale: 2),
                        NGUYENNHAN = c.String(),
                        TITLE_NGUYENNHAN = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_03TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NGUONTHU = c.String(maxLength: 1000),
                        THUCTHI_DUOCGIAO = c.Decimal(precision: 18, scale: 2),
                        THANHTRA_XACDINH = c.Decimal(precision: 18, scale: 2),
                        TITLE_NGUYENNHAN = c.String(),
                        NGUYENNHAN = c.String(),
                        GHICHU = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_02TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        THOI_KY = c.String(maxLength: 500),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_CHI = c.String(maxLength: 1000),
                        THUCTHI_DUOCGIAO = c.Decimal(precision: 18, scale: 2),
                        THANHTRA_XACDINH = c.Decimal(precision: 18, scale: 2),
                        TITLE_NGUYENNHAN = c.String(),
                        NGUYENNHAN = c.String(),
                        GHICHU = c.String(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM_01TT_DVSN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_CHI = c.String(maxLength: 1000),
                        THUCTHI_NAM = c.Decimal(precision: 18, scale: 2),
                        QUYETTOAN_CHI_NAM = c.Decimal(precision: 18, scale: 2),
                        THUCTHI_DUOCGIAO = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
