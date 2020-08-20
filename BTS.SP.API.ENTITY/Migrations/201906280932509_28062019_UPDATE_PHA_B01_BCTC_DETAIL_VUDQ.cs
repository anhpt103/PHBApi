namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28062019_UPDATE_PHA_B01_BCTC_DETAIL_VUDQ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHA_B01_BCTC_DETAIL", "THUYET_MINH", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_B01_BCTC_DETAIL", "THUYET_MINH", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
