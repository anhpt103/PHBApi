namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10112019_ADD_COLUMN_PHB_B02BCQT_DETAIL : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02BCQT_DETAIL", "STT_SAPXEP", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02BCQT_DETAIL", "STT_SAPXEP");
        }
    }
}
