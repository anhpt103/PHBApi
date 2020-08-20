namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14052019_EDIT_PHF_DM_TIEUMUC_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "MA_TIEUMUC", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "TRANG_THAI", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "MA_MUC", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "USER_NAME", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "LOAI", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "LOAI_KHOANTHU", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "LOAI_KHOANTHU", c => c.String(maxLength: 10));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "LOAI", c => c.String(maxLength: 10));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "USER_NAME", c => c.String(maxLength: 20));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "MA_MUC", c => c.String(maxLength: 4));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "TRANG_THAI", c => c.String(maxLength: 1));
            AlterColumn("BTSTC.PHF_DM_TIEUMUC", "MA_TIEUMUC", c => c.String(maxLength: 6));
        }
    }
}
