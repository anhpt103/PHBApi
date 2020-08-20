namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04122019_UPDATE_PHB_TABLE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "MA_TIEN_LUONG", c => c.String(nullable: false, maxLength: 15));
            AddColumn("BTSTC.PHB_DM_TIENLUONG", "TRANG_THAI", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PHB_L_PC_UB_REFID", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHB_L_PC_UB", "REFID", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHB_L_PC_UB", "NGUOI_TAO", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_L_PC_UB", "NGUOI_TAO", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("BTSTC.PHB_L_PC_UB", "REFID", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "PHB_L_PC_UB_REFID", c => c.String(nullable: false, maxLength: 50));
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "TRANG_THAI");
            DropColumn("BTSTC.PHB_DM_TIENLUONG", "MA_TIEN_LUONG");
        }
    }
}
