namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25012019_UPDATE_PHF_BM_01TT_DVSN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_01TT_DVSN", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_01TT_DVSN", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_BM_01TT_DVSN", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_BM_01TT_DVSN", "MA_DONVI");
            DropColumn("BTSTC.PHF_BM_01TT_DVSN", "THOI_KY");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_BM_01TT_DVSN", "THOI_KY", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_BM_01TT_DVSN", "MA_DONVI", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_BM_01TT_DVSN", "IS_ITALIC");
            DropColumn("BTSTC.PHF_BM_01TT_DVSN", "IS_BOLD");
            DropColumn("BTSTC.PHF_BM_01TT_DVSN", "STT_CHA");
        }
    }
}
