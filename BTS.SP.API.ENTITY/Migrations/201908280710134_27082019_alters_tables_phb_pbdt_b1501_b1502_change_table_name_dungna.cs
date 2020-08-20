namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_alters_tables_phb_pbdt_b1501_b1502_change_table_name_dungna : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "BTSTC.PHB_PBDT_B1501_DETAIL", newName: "PHB_PBDT_B1501_ROW");
            RenameTable(name: "BTSTC.PHB_PBDT_B1502_DETAIL", newName: "PHB_PBDT_B1502_ROW");
        }
        
        public override void Down()
        {
            RenameTable(name: "BTSTC.PHB_PBDT_B1502_ROW", newName: "PHB_PBDT_B1502_DETAIL");
            RenameTable(name: "BTSTC.PHB_PBDT_B1501_ROW", newName: "PHB_PBDT_B1501_DETAIL");
        }
    }
}
