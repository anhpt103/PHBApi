namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19112019_EditField_kiennt : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.SYS_DVQHNS_QUANLY", "MA_SUNGHIEP", c => c.String(maxLength: 10));
            AddColumn("BTSTC.SYS_DVQHNS_QUANLY", "TEN_SUNGHIEP", c => c.String(maxLength: 100));
            DropColumn("BTSTC.SYS_DVQHNS_QUANLY", "SU_NGHIEP");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.SYS_DVQHNS_QUANLY", "SU_NGHIEP", c => c.String(maxLength: 10));
            DropColumn("BTSTC.SYS_DVQHNS_QUANLY", "TEN_SUNGHIEP");
            DropColumn("BTSTC.SYS_DVQHNS_QUANLY", "MA_SUNGHIEP");
        }
    }
}
