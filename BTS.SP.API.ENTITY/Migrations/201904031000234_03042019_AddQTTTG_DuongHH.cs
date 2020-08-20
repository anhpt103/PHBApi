namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03042019_AddQTTTG_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_FILE_QTTTG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 250),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        THOIGIAN = c.String(maxLength: 30),
                        TEN_BIEUMAU = c.String(maxLength: 200),
                        TIEUDE_BIEUMAU = c.String(maxLength: 500),
                        TEN_DONVI = c.String(maxLength: 500),
                        DONVI_TINH = c.String(maxLength: 500),
                        TU_NGAY = c.DateTime(),
                        DEN_NGAY = c.DateTime(),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM01_BIENDONG_DK_GIA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        DIEN_GIAI = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM02_BIENDONG_KH_GIA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        DIEN_GIAI = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM03_TONGHOP_DAUVAO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        SANLUONG_NHAPKHO = c.Decimal(precision: 10, scale: 0),
                        GIACIF_GIATRI = c.Decimal(precision: 18, scale: 2),
                        GIACIF_TYLE = c.Decimal(precision: 18, scale: 2),
                        THUE_NHAPKHAU_GIATRI = c.Decimal(precision: 18, scale: 2),
                        THUE_NHAPKHAU_TYLE = c.Decimal(precision: 18, scale: 2),
                        THUE_TTDB_GIATRI = c.Decimal(precision: 18, scale: 2),
                        THUE_TTDB_TYLE = c.Decimal(precision: 18, scale: 2),
                        MUAHANG_VANCHUYEN_GIATRI = c.Decimal(precision: 18, scale: 2),
                        MUAHANG_VANCHUYEN_TYLE = c.Decimal(precision: 18, scale: 2),
                        MUAHANG_KHAC_GIATRI = c.Decimal(precision: 18, scale: 2),
                        MUAHANG_KHAC_TYLE = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM04_TONGHOP_SANXUAT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        SANLUONG_BAN = c.Decimal(precision: 10, scale: 0),
                        COT3 = c.Decimal(precision: 18, scale: 2),
                        COT4 = c.Decimal(precision: 18, scale: 2),
                        COT5 = c.Decimal(precision: 18, scale: 2),
                        COT6 = c.Decimal(precision: 18, scale: 2),
                        COT7 = c.Decimal(precision: 18, scale: 2),
                        COT8 = c.Decimal(precision: 18, scale: 2),
                        COT9 = c.Decimal(precision: 18, scale: 2),
                        COT10 = c.Decimal(precision: 18, scale: 2),
                        COT11 = c.Decimal(precision: 18, scale: 2),
                        COT12 = c.Decimal(precision: 18, scale: 2),
                        COT13 = c.Decimal(precision: 18, scale: 2),
                        COT14 = c.Decimal(precision: 18, scale: 2),
                        COT17 = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM05_TONGHOP_LUUTHONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        SANLUONG = c.Decimal(precision: 10, scale: 0),
                        COT3 = c.Decimal(precision: 18, scale: 2),
                        COT5 = c.Decimal(precision: 18, scale: 2),
                        COT6 = c.Decimal(precision: 18, scale: 2),
                        COT7 = c.Decimal(precision: 18, scale: 2),
                        COT8 = c.Decimal(precision: 18, scale: 2),
                        COT9 = c.Decimal(precision: 18, scale: 2),
                        COT10 = c.Decimal(precision: 18, scale: 2),
                        COT11 = c.Decimal(precision: 18, scale: 2),
                        COT12 = c.Decimal(precision: 18, scale: 2),
                        COT13 = c.Decimal(precision: 18, scale: 2),
                        COT14 = c.Decimal(precision: 18, scale: 2),
                        COT15 = c.Decimal(precision: 18, scale: 2),
                        COT16 = c.Decimal(precision: 18, scale: 2),
                        COT17 = c.Decimal(precision: 18, scale: 2),
                        COT18 = c.Decimal(precision: 18, scale: 2),
                        COT19 = c.Decimal(precision: 18, scale: 2),
                        COT20 = c.Decimal(precision: 18, scale: 2),
                        COT21 = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM06_TONGHOP_PHANTICH",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_SANPHAM = c.String(maxLength: 500),
                        DONVI_TINH = c.Decimal(precision: 10, scale: 0),
                        COT4 = c.Decimal(precision: 18, scale: 2),
                        COT5 = c.Decimal(precision: 18, scale: 2),
                        COT6 = c.Decimal(precision: 18, scale: 2),
                        COT7 = c.Decimal(precision: 18, scale: 2),
                        COT10 = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM07_PHANTICH_QUANTRI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DIENGIAI = c.String(maxLength: 500),
                        DONVI_TINH = c.Decimal(precision: 10, scale: 0),
                        TITLE_BIENDONGGIA_NGAY = c.String(),
                        BIENDONGGIA_NGAY = c.Decimal(precision: 18, scale: 2),
                        TRUNGGIAN_SANXUAT = c.Decimal(precision: 18, scale: 2),
                        SANPHAM_TRUNGGIAN_DINHMUC = c.Decimal(precision: 18, scale: 2),
                        SANPHAM_SANXUAT_DINHMUC = c.Decimal(precision: 18, scale: 2),
                        MUCBIENDONG_TYLE = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_BM08_TONGHOP_VIPHAM",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_VIPHAM = c.String(maxLength: 500),
                        SOTIEN_PHAT = c.Decimal(precision: 18, scale: 2),
                        BIENPHAP_KHACPHUC = c.String(maxLength: 500),
                        GHICHU = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_BM08_TONGHOP_VIPHAM");
            DropTable("BTSTC.PHF_BM07_PHANTICH_QUANTRI");
            DropTable("BTSTC.PHF_BM06_TONGHOP_PHANTICH");
            DropTable("BTSTC.PHF_BM05_TONGHOP_LUUTHONG");
            DropTable("BTSTC.PHF_BM04_TONGHOP_SANXUAT");
            DropTable("BTSTC.PHF_BM03_TONGHOP_DAUVAO");
            DropTable("BTSTC.PHF_BM02_BIENDONG_KH_GIA");
            DropTable("BTSTC.PHF_BM01_BIENDONG_DK_GIA");
            DropTable("BTSTC.PHF_BM_FILE_QTTTG");
        }
    }
}
