namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19082019_UPDATE_PHF_NHAPBAOCAO_COLUMN_NGAYKY_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "NGAYKY", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "NGAYKY");
        }
    }
}
