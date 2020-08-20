namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09112019_DUYTB_ADD_COLUMN_STTSAPXEP_PHB_B02BCTC_TEMPLATE : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02BCTC_TEMPLATE", "STT_SAPXEP", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02BCTC_TEMPLATE", "STT_SAPXEP");
        }
    }
}
