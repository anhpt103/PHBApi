namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26072019_ADDCOLUMN_MAPHONGBAN_PHF_NGUOIDUNG_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_AU_NGUOIDUNG", "MAPHONGBAN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_AU_NGUOIDUNG", "MAPHONGBAN");
        }
    }
}
