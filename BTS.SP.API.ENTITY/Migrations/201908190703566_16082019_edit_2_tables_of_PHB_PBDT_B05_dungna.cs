namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16082019_edit_2_tables_of_PHB_PBDT_B05_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B05_DETAIL", "IS_OPTIONAL", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "IS_OPTIONAL", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "IS_OPTIONAL");
            DropColumn("BTSTC.PHB_PBDT_B05_DETAIL", "IS_OPTIONAL");
        }
    }
}
