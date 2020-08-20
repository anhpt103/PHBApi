namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29082019_alter_tables_of_phb_pbdt_b06_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B06_DATA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        DETAIL_REFID = c.String(nullable: false, maxLength: 50),
                        DONVI_REFID = c.String(nullable: false, maxLength: 50),
                        UOC_THUC_HIEN = c.Decimal(precision: 18, scale: 2),
                        DU_TOAN = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B06_DONVI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PHB_PBDT_B06_REFID = c.String(nullable: false, maxLength: 50),
                        DONVI_REFID = c.String(nullable: false, maxLength: 50),
                        TEN_DON_VI = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            
            DropTable("BTSTC.PHB_PBDT_B06_DETAIL_DONVI");
        }
        
        public override void Down()
        {
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
            
            
            DropTable("BTSTC.PHB_PBDT_B06_DONVI");
            DropTable("BTSTC.PHB_PBDT_B06_DATA");
        }
    }
}
