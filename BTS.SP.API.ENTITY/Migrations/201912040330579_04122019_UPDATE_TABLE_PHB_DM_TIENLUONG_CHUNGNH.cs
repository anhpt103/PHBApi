namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04122019_UPDATE_TABLE_PHB_DM_TIENLUONG_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_NLDD", c => c.Double(nullable: false));
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_NLDD", c => c.Double(nullable: false));
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_NLDD", c => c.Double(nullable: false));
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_NLDD", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "KP_CD_NLDD");
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "BHTN_NLDD");
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "BHYT_NLDD");
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "BHXH_NLDD");
        }
    }
}
