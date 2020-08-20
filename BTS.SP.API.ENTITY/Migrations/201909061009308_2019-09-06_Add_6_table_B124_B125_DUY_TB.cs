namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190906_Add_6_table_B124_B125_DUY_TB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B124",
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
                "BTSTC.PHB_PBDT_B124_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B124_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        NAM_HIEN_HANH_DU_TOAN = c.Decimal(precision: 18, scale: 2),
                        NAM_HIEN_HANH_UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        NAM_KE_HOACH = c.Decimal(precision: 18, scale: 2),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B124_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 500),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B125",
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
                "BTSTC.PHB_PBDT_B125_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B125_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        NAM_HIEN_HANH_DU_TOAN = c.Decimal(precision: 18, scale: 2),
                        NAM_HIEN_HANH_UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        NAM_KE_HOACH = c.Decimal(precision: 18, scale: 2),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B125_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 500),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "TRANG_THAI", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MADONG", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "TENDONG", c => c.String(maxLength: 2000));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO", c => c.DateTime(nullable: false));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG");
            DropTable("BTSTC.PHB_PBDT_COL_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_FORM");
            DropTable("BTSTC.PHB_PBDT_FORM_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_ROW_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TEST_COL");
            DropTable("BTSTC.PHB_PBDT_TEST_DATA");
            DropTable("BTSTC.PHB_PBDT_TEST_ROW");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_TEST_ROW",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FORM_REFID = c.String(maxLength: 50),
                        ROW_REFID = c.String(maxLength: 50),
                        STT_SAP_XEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 25),
                        MA_SO = c.String(maxLength: 25),
                        MA_CHA = c.String(maxLength: 25),
                        ROW_LEVEL = c.Decimal(precision: 10, scale: 0),
                        PHEP_TOAN = c.Decimal(precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHI_TIEU = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TEST_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TEST_COL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FORM_REF_ID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                        STT_SAP_XEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 25),
                        MA_SO = c.String(maxLength: 25),
                        MA_CHA = c.String(maxLength: 25),
                        CAP_MA_CHA = c.Decimal(precision: 10, scale: 0),
                        PHEP_TOAN = c.Decimal(precision: 10, scale: 0),
                        DATA_TYPE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COL_LEVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COLSPAN = c.Decimal(precision: 10, scale: 0),
                        ROWSPAN = c.Decimal(precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_STORE_DATA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COLUMN_NAME = c.String(maxLength: 500),
                        EXCEL_COLUMN = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_ROW_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAO_CAO = c.String(maxLength: 50),
                        ROW_REFID = c.String(maxLength: 50),
                        STT_SAP_XEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 25),
                        MA_SO = c.String(maxLength: 25),
                        MA_CHA = c.String(maxLength: 25),
                        ROW_LEVEL = c.Decimal(precision: 10, scale: 0),
                        PHEP_TOAN = c.Decimal(precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHI_TIEU = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_FORM_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAO_CAO = c.String(maxLength: 50),
                        TEN_BAO_CAO = c.String(maxLength: 500),
                        MIEU_TA = c.String(maxLength: 500),
                        DON_VI_TIEN = c.String(maxLength: 50),
                        IS_DYNAMIC_ROW = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_DYNAMIC_COLUMN = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_FORM",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAO_CAO = c.String(maxLength: 50),
                        FORM_REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 250),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 250),
                        MA_DON_VI = c.String(maxLength: 50),
                        CHUONG = c.String(maxLength: 150),
                        NAM_THUC_HIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_HIEN_HANH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_KE_HOACH = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_COL_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAO_CAO = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                        STT_SAP_XEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 25),
                        MA_SO = c.String(maxLength: 25),
                        MA_CHA = c.String(maxLength: 25),
                        CAP_MA_CHA = c.Decimal(precision: 10, scale: 0),
                        PHEP_TOAN = c.Decimal(precision: 10, scale: 0),
                        DATA_TYPE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COL_LEVEL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COLSPAN = c.Decimal(precision: 10, scale: 0),
                        ROWSPAN = c.Decimal(precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_STORE_DATA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        COLUMN_NAME = c.String(maxLength: 500),
                        EXCEL_COLUMN = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NOIDUNG", c => c.String(maxLength: 1500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "TENDONG", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MADONG", c => c.String(nullable: false, maxLength: 50));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "TRANG_THAI");
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOILAP");
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MA_FILE_PK");
            DropTable("BTSTC.PHB_PBDT_B125_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B125_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B125");
            DropTable("BTSTC.PHB_PBDT_B124_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B124_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B124");
        }
    }
}
