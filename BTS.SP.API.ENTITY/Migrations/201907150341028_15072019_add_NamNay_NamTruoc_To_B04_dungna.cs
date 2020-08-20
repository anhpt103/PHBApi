namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15072019_add_NamNay_NamTruoc_To_B04_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_B04_BCTC_DETAIL", "NAM_NAY", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHA_B04_BCTC_DETAIL", "NAM_TRUOC", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_B04_BCTC_DETAIL", "NAM_TRUOC");
            DropColumn("BTSTC.PHA_B04_BCTC_DETAIL", "NAM_NAY");
        }
    }
}
