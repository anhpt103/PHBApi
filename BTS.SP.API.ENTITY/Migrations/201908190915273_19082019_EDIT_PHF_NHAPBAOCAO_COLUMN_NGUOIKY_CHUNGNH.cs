namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19082019_EDIT_PHF_NHAPBAOCAO_COLUMN_NGUOIKY_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY", c => c.DateTime(nullable: false));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGAYKY");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGAYKY", c => c.DateTime(nullable: false));
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY");
        }
    }
}
