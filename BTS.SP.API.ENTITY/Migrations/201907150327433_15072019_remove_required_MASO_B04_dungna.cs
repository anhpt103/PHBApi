namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15072019_remove_required_MASO_B04_dungna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHA_B04_BCTC_DETAIL", "MA_SO", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "MA_SO", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "MA_SO", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHA_B04_BCTC_DETAIL", "MA_SO", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
