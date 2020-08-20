namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_alters_tables_of_phb_pbdt_b1501_b1502_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B1501_DATA", "ROW_REFID", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHB_PBDT_B1502_DATA", "ROW_REFID", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHB_PBDT_B1501_DATA", "DETAIL_REFID");
            DropColumn("BTSTC.PHB_PBDT_B1502_DATA", "DETAIL_REFID");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_PBDT_B1502_DATA", "DETAIL_REFID", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHB_PBDT_B1501_DATA", "DETAIL_REFID", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHB_PBDT_B1502_DATA", "ROW_REFID");
            DropColumn("BTSTC.PHB_PBDT_B1501_DATA", "ROW_REFID");
        }
    }
}
