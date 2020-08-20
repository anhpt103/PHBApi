namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17092019_add_tables_phb_pbdt_qldt_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B01",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DBHC = c.String(maxLength: 150),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B01_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_QLDT_B01_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        HO_TEN = c.String(maxLength: 500),
                        CHUC_DANH = c.String(maxLength: 500),
                        HE_SO_LUONG = c.Decimal(precision: 18, scale: 2),
                        PC_KV = c.Decimal(precision: 18, scale: 2),
                        PC_CHUCVU = c.Decimal(precision: 18, scale: 2),
                        PC_THAMNIEN = c.Decimal(precision: 18, scale: 2),
                        PC_TRACHNHIEM = c.Decimal(precision: 18, scale: 2),
                        PC_LOAIXA = c.Decimal(precision: 18, scale: 2),
                        PC_LAUNAM = c.Decimal(precision: 18, scale: 2),
                        PC_THUHUT = c.Decimal(precision: 18, scale: 2),
                        TONG_HESO = c.Decimal(precision: 18, scale: 2),
                        TONG_CONG = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHXH = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHYT = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_CONG = c.Decimal(precision: 18, scale: 2),
                        THUC_LINH = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B01_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B02",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DBHC = c.String(maxLength: 150),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B02_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_QLDT_B02_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        HO_TEN = c.String(maxLength: 500),
                        CHUC_DANH = c.String(maxLength: 500),
                        HE_SO_LUONG = c.Decimal(precision: 18, scale: 2),
                        PC_KV = c.Decimal(precision: 18, scale: 2),
                        PC_CHUCVU = c.Decimal(precision: 18, scale: 2),
                        PC_CONGVU = c.Decimal(precision: 18, scale: 2),
                        PC_LOAIXA = c.Decimal(precision: 18, scale: 2),
                        PC_LAUNAM = c.Decimal(precision: 18, scale: 2),
                        TONG_HESO_PC = c.Decimal(precision: 18, scale: 2),
                        TONG_HESO = c.Decimal(precision: 18, scale: 2),
                        THANH_TIEN = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHXH = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHYT = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_CONG = c.Decimal(precision: 18, scale: 2),
                        THUC_LINH = c.Decimal(precision: 18, scale: 2),
                        GHI_CHU = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B02_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B03",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DBHC = c.String(maxLength: 150),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B03_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_QLDT_B03_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        HO_TEN = c.String(maxLength: 500),
                        CHUC_DANH = c.String(maxLength: 500),
                        HE_SO_LUONG = c.Decimal(precision: 18, scale: 2),
                        PC_KV = c.Decimal(precision: 18, scale: 2),
                        PC_CV = c.Decimal(precision: 18, scale: 2),
                        PC_CONGVU = c.Decimal(precision: 18, scale: 2),
                        PC_LOAIXA = c.Decimal(precision: 18, scale: 2),
                        PC_KN = c.Decimal(precision: 18, scale: 2),
                        PC_LAUNAM = c.Decimal(precision: 18, scale: 2),
                        PC_THUHUT = c.Decimal(precision: 18, scale: 2),
                        TONG_HESO = c.Decimal(precision: 18, scale: 2),
                        TONG_CONG = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHXH = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_BHYT = c.Decimal(precision: 18, scale: 2),
                        LOAI_TRU_CONG = c.Decimal(precision: 18, scale: 2),
                        TONG_CONG2 = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_QLDT_B03_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_PBDT_QLDT_B03_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_QLDT_B03_DETAIL");
            DropTable("BTSTC.PHB_PBDT_QLDT_B03");
            DropTable("BTSTC.PHB_PBDT_QLDT_B02_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_QLDT_B02_DETAIL");
            DropTable("BTSTC.PHB_PBDT_QLDT_B02");
            DropTable("BTSTC.PHB_PBDT_QLDT_B01_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_QLDT_B01_DETAIL");
            DropTable("BTSTC.PHB_PBDT_QLDT_B01");
        }
    }
}
