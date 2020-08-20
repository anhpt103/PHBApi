namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _130819_add_output_report_template_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_BCTC_B01_BCTC_TH_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 1000),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        MA_SO = c.String(maxLength: 1000),
                        THUYET_MINH = c.String(maxLength: 1000),
                        SO_CUOI_NAM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SO_DAU_NAM = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_CUOI_NAM = c.String(maxLength: 1000),
                        MA_CHA = c.String(maxLength: 1000),
                        XML_PARENT_FIELD_NAME = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_BCTC_B02_BCTC_TH_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 1000),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        MA_SO = c.String(maxLength: 1000),
                        MA_SO_CHA = c.String(maxLength: 1000),
                        THUYET_MINH = c.String(maxLength: 1000),
                        NAM_NAY = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NAM_TRUOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_NAM_NAY = c.String(maxLength: 1000),
                        IS_MINUS = c.Decimal(nullable: false, precision: 10, scale: 0),
                        XML_PARENT_FIELD_NAME = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_BCTC_B03_BCTC_TH_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 1000),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        MA_SO = c.String(maxLength: 1000),
                        MA_SO_CHA = c.String(maxLength: 1000),
                        THUYET_MINH = c.String(maxLength: 1000),
                        NAM_NAY = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NAM_TRUOC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_NAM_NAY = c.String(maxLength: 1000),
                        XML_PARENT_FIELD_NAME = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_BCTC_B03_BCTC_TH_TEMPLATE");
            DropTable("BTSTC.PHA_BCTC_B02_BCTC_TH_TEMPLATE");
            DropTable("BTSTC.PHA_BCTC_B01_BCTC_TH_TEMPLATE");
        }
    }
}
