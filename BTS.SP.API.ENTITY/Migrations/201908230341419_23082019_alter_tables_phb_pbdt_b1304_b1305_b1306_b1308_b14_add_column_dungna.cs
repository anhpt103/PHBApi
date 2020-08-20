namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23082019_alter_tables_phb_pbdt_b1304_b1305_b1306_b1308_b14_add_column_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B05_DETAIL", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B05_DETAIL", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1304_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1304_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1305_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1305_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1306_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1306_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1308_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B1308_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B14_TEMPLATE", "MA_SO", c => c.String(maxLength: 5));
            AddColumn("BTSTC.PHB_PBDT_B14_TEMPLATE", "MA_CHA", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B14_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B14_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1308_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1308_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1306_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1306_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1305_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1305_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1304_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1304_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "MA_SO");
            DropColumn("BTSTC.PHB_PBDT_B05_DETAIL", "MA_CHA");
            DropColumn("BTSTC.PHB_PBDT_B05_DETAIL", "MA_SO");
        }
    }
}
