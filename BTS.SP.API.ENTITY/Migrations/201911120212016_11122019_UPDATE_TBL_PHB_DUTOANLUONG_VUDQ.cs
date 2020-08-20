namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11122019_UPDATE_TBL_PHB_DUTOANLUONG_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_DUTOANLUONG", "HE_SO", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_DUTOANLUONG", "HE_SO");
        }
    }
}
