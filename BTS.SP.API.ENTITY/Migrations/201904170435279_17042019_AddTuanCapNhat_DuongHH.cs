namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17042019_AddTuanCapNhat_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_CAPNHAT_BAOCAO", "TUAN", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_CAPNHAT_BAOCAO", "TUAN");
        }
    }
}
