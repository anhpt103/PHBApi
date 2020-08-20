namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21082019_phb_pbdt_b07_add_columns_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B07_DETAIL", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B07_DETAIL", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B07_DETAIL", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B07_DETAIL", "MA_SO");
        }
    }
}
