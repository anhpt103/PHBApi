namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062019_Edit_TTDT_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "DONVI", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGAY_TAO", c => c.DateTime());
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGUOI_TAO", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGAY_TT", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGAY_TT", c => c.DateTime(nullable: false));
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGUOI_TAO");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "NGAY_TAO");
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "DONVI");
        }
    }
}
