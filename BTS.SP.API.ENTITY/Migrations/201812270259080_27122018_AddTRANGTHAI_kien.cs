namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27122018_AddTRANGTHAI_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_HACHTOAN_CHI", "TRANG_THAI", c => c.String(maxLength: 1));
            AddColumn("BTSTC.PHA_HACHTOAN_KHAC", "TRANG_THAI", c => c.String(maxLength: 1));
            AddColumn("BTSTC.PHA_HACHTOAN_THU", "TRANG_THAI", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_HACHTOAN_THU", "TRANG_THAI");
            DropColumn("BTSTC.PHA_HACHTOAN_KHAC", "TRANG_THAI");
            DropColumn("BTSTC.PHA_HACHTOAN_CHI", "TRANG_THAI");
        }
    }
}
