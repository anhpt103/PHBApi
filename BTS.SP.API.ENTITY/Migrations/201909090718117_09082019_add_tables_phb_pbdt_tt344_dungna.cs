namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09082019_add_tables_phb_pbdt_tt344_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B01",
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
                "BTSTC.PHB_PBDT_TT344_B01_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B01_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DU_TOAN = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B01_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B02",
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
                "BTSTC.PHB_PBDT_TT344_B02_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B02_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UOCTH_THU_NSNN = c.Decimal(precision: 18, scale: 2),
                        UOCTH_THU_NSX = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_THU_NSNN = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_THU_NSX = c.Decimal(precision: 18, scale: 2),
                        SOSANH_THU_NSNN = c.Decimal(precision: 18, scale: 2),
                        SOSANH_THU_NSX = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B02_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B03",
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
                "BTSTC.PHB_PBDT_TT344_B03_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B03_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DUTOAN_NAMTRUOC_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_NAMTRUOC_DTPT = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_NAMTRUOC_TX = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_NAM_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_NAM_DTPT = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_NAM_TX = c.Decimal(precision: 18, scale: 2),
                        SOSANH_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        SOSANH_DTPT = c.Decimal(precision: 18, scale: 2),
                        SOSANH_TX = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B03_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B04",
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
                "BTSTC.PHB_PBDT_TT344_B04_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B04_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        THOIGIAN_KCHT = c.String(maxLength: 500),
                        TONG_DU_TOAN_TONGSO = c.Decimal(precision: 18, scale: 2),
                        TONG_DU_TOAN_DAN_DONG_GOP = c.Decimal(precision: 18, scale: 2),
                        GIATRI_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        GIATRI_THANHTOAN = c.Decimal(precision: 18, scale: 2),
                        DT_TONGSO = c.Decimal(precision: 18, scale: 2),
                        DT_NAMTRUOC = c.Decimal(precision: 18, scale: 2),
                        DT_CANDOI_NGANSACH = c.Decimal(precision: 18, scale: 2),
                        DT_TONGSO1 = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B04_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B05",
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
                "BTSTC.PHB_PBDT_TT344_B05_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B05_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        UOC_TH_THU = c.Decimal(precision: 18, scale: 2),
                        UOC_TH_CHI = c.Decimal(precision: 18, scale: 2),
                        UOC_TH_CHENH_LECH = c.Decimal(precision: 18, scale: 2),
                        KEHOACH_THU = c.Decimal(precision: 18, scale: 2),
                        KEHOACH_CHI = c.Decimal(precision: 18, scale: 2),
                        KEHOACH_CHENH_LECH = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B05_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B06",
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
                "BTSTC.PHB_PBDT_TT344_B06_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_TT344_B06_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHUONG = c.String(maxLength: 500),
                        LOAI = c.String(maxLength: 500),
                        KHOAN = c.String(maxLength: 500),
                        DIEN_GIAI = c.String(maxLength: 500),
                        DU_TOAN = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B06_TEMPLATE",
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
            
            DropTable("BTSTC.PHB_PBDT_QLDT_COL_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_QLDT_FORM");
            DropTable("BTSTC.PHB_PBDT_QLDT_FORM_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_QLDT_ROW_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TEST_COL");
            DropTable("BTSTC.PHB_PBDT_TEST_DATA");
            DropTable("BTSTC.PHB_PBDT_TEST_ROW");
            DropTable("BTSTC.PHB_PBDT_TT344_B01_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B02_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B02_ROW");
            DropTable("BTSTC.PHB_PBDT_TT344_B03_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B04_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B04_ROW");
            DropTable("BTSTC.PHB_PBDT_TT344_B05_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B05_ROW");
            DropTable("BTSTC.PHB_PBDT_TT344_B06_DATA");
            DropTable("BTSTC.PHB_PBDT_TT344_B06_ROW");
            DropTable("BTSTC.PHB_PBDT_TT344_COL_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_FORM");
            DropTable("BTSTC.PHB_PBDT_TT344_FORM_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_ROW_TEMPLATE");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_ROW_TEMPLATE",
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
                "BTSTC.PHB_PBDT_TT344_FORM_TEMPLATE",
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
                "BTSTC.PHB_PBDT_TT344_FORM",
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
                        MA_DBHC = c.String(maxLength: 50),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_COL_TEMPLATE",
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
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B06_ROW",
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
                "BTSTC.PHB_PBDT_TT344_B06_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B05_ROW",
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
                "BTSTC.PHB_PBDT_TT344_B05_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B04_ROW",
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
                "BTSTC.PHB_PBDT_TT344_B04_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B03_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B02_ROW",
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
                "BTSTC.PHB_PBDT_TT344_B02_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_TT344_B01_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DATA = c.Decimal(precision: 18, scale: 2),
                        ROW_REFID = c.String(maxLength: 50),
                        COL_REFID = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "BTSTC.PHB_PBDT_QLDT_ROW_TEMPLATE",
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
                "BTSTC.PHB_PBDT_QLDT_FORM_TEMPLATE",
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
                "BTSTC.PHB_PBDT_QLDT_FORM",
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
                "BTSTC.PHB_PBDT_QLDT_COL_TEMPLATE",
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
            
            
            DropTable("BTSTC.PHB_PBDT_TT344_B06_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B06_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B06");
            DropTable("BTSTC.PHB_PBDT_TT344_B05_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B05_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B05");
            DropTable("BTSTC.PHB_PBDT_TT344_B04_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B04_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B04");
            DropTable("BTSTC.PHB_PBDT_TT344_B03_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B03_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B03");
            DropTable("BTSTC.PHB_PBDT_TT344_B02_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B02_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B02");
            DropTable("BTSTC.PHB_PBDT_TT344_B01_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_TT344_B01_DETAIL");
            DropTable("BTSTC.PHB_PBDT_TT344_B01");
        }
    }
}
