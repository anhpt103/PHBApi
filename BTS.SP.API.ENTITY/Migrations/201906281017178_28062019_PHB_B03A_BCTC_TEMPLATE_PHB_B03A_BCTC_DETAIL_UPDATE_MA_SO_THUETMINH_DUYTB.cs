namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28062019_PHB_B03A_BCTC_TEMPLATE_PHB_B03A_BCTC_DETAIL_UPDATE_MA_SO_THUETMINH_DUYTB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_B03A_BCTC_DETAIL", "MA_SO", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_B03A_BCTC_DETAIL", "THUYET_MINH", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "MA_SO", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_B03A_BCTC_TEMPLATE", "MA_SO", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_B03A_BCTC_DETAIL", "THUYET_MINH", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_B03A_BCTC_DETAIL", "MA_SO", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
