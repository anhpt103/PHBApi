namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22062019_AddTrgThai_Kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRANG_THAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "TRANG_THAI");
        }
    }
}
