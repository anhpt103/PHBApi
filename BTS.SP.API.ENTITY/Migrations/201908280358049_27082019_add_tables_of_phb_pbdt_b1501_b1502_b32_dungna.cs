namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_add_tables_of_phb_pbdt_b1501_b1502_b32_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B1501",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DONVI = c.String(maxLength: 150),
                        DON_VI_DT = c.String(maxLength: 150),
                        CAP_DU_TOAN = c.String(maxLength: 150),
                        NAM_THUC_HIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_HIEN_HANH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_KE_HOACH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHUONG = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1501_COLUMN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        COLUMN_REFID = c.String(maxLength: 50),
                        TITLE = c.String(maxLength: 500),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1501_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DETAIL_REFID = c.String(maxLength: 50),
                        COLUMN_REFID = c.String(maxLength: 50),
                        DATA = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1501_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B1501_REFID = c.String(nullable: false, maxLength: 50),
                        ROW_REFID = c.String(maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1501_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        DON_VI_TINH = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1502",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DONVI = c.String(maxLength: 150),
                        DON_VI_DT = c.String(maxLength: 150),
                        CAP_DU_TOAN = c.String(maxLength: 150),
                        NAM_THUC_HIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_HIEN_HANH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_KE_HOACH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHUONG = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1502_COLUMN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        COLUMN_REFID = c.String(maxLength: 50),
                        TITLE = c.String(maxLength: 500),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1502_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DETAIL_REFID = c.String(maxLength: 50),
                        COLUMN_REFID = c.String(maxLength: 50),
                        DATA = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1502_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B1502_REFID = c.String(nullable: false, maxLength: 50),
                        ROW_REFID = c.String(maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1502_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        DON_VI_TINH = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B32",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DONVI = c.String(maxLength: 150),
                        DON_VI_DT = c.String(maxLength: 150),
                        CAP_DU_TOAN = c.String(maxLength: 150),
                        NAM_THUC_HIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_HIEN_HANH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_KE_HOACH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHUONG = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B32_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B32_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        NAMHH_DU_TOAN = c.Decimal(precision: 18, scale: 2),
                        NAMHH_UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        NAMKH_DU_TOAN = c.Decimal(precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B32_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_PBDT_B32_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B32_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B32");
            DropTable("BTSTC.PHB_PBDT_B1502_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B1502_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B1502_DATA");
            DropTable("BTSTC.PHB_PBDT_B1502_COLUMN");
            DropTable("BTSTC.PHB_PBDT_B1502");
            DropTable("BTSTC.PHB_PBDT_B1501_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B1501_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B1501_DATA");
            DropTable("BTSTC.PHB_PBDT_B1501_COLUMN");
            DropTable("BTSTC.PHB_PBDT_B1501");
        }
    }
}
