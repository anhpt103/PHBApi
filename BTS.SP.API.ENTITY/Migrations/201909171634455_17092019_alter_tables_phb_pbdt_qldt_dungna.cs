namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17092019_alter_tables_phb_pbdt_qldt_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_QLDT_B01", "THANG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_PBDT_QLDT_B01_DETAIL", "PC_CONGVU", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_QLDT_B02", "THANG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_PBDT_QLDT_B02_DETAIL", "PC_THUHUT", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_QLDT_B03", "THANG", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_QLDT_B03", "THANG");
            DropColumn("BTSTC.PHB_PBDT_QLDT_B02_DETAIL", "PC_THUHUT");
            DropColumn("BTSTC.PHB_PBDT_QLDT_B02", "THANG");
            DropColumn("BTSTC.PHB_PBDT_QLDT_B01_DETAIL", "PC_CONGVU");
            DropColumn("BTSTC.PHB_PBDT_QLDT_B01", "THANG");
        }
    }
}
