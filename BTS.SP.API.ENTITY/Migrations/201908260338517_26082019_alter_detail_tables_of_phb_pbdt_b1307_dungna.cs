namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26082019_alter_detail_tables_of_phb_pbdt_b1307_dungna : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
