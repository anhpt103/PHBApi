namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_add_4_tables_of_phb_pbdt_b06_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B06",
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
                "BTSTC.PHB_PBDT_B06_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DETAIL_REFID = c.String(nullable: false, maxLength: 50),
                        PHB_PBDT_B06_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        TONGSO_UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        TONGSO_DU_TOAN = c.Decimal(precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B06_DETAIL_DONVI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DETAIL_REFID = c.String(nullable: false, maxLength: 50),
                        TEN_DON_VI = c.String(nullable: false, maxLength: 500),
                        UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        DU_TOAN = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B06_TEMPLATE",
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
            DropTable("BTSTC.PHB_PBDT_B06_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B06_DETAIL_DONVI");
            DropTable("BTSTC.PHB_PBDT_B06_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B06");
        }
    }
}
