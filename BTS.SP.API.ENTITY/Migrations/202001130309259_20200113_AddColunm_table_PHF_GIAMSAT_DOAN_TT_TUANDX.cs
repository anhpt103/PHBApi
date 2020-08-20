namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200113_AddColunm_table_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT");
        }
    }
}
