namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11102019_DUYTB_CHANGE_STT_SAPXEP_TO_INT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_B02BCQT_DETAIL", "STT_SAPXEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "STT_SAPXEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "STT_SAPXEP", c => c.String(maxLength: 15));
            AlterColumn("BTSTC.PHB_B02BCQT_DETAIL", "STT_SAPXEP", c => c.String(maxLength: 15));
        }
    }
}
