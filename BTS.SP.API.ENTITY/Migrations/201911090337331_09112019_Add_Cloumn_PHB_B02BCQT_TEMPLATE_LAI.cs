namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09112019_Add_Cloumn_PHB_B02BCQT_TEMPLATE_LAI : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "STT_SAPXEP", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "STT_SAPXEP");
        }
    }
}
