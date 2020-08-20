namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24062019_addTongTien_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TONG_TIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_DM_TCBC_TT03_BIEU01", "SAPXEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_TCBC_TT03_BIEU01", "SAPXEP", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TONG_TIEN");
        }
    }
}
