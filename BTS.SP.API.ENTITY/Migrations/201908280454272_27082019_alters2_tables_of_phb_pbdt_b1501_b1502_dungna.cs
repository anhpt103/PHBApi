namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_alters2_tables_of_phb_pbdt_b1501_b1502_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B1501_COLUMN", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_PBDT_B1502_COLUMN", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B1502_COLUMN", "STT");
            DropColumn("BTSTC.PHB_PBDT_B1501_COLUMN", "STT");
        }
    }
}
