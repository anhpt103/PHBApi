namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30082019_UPDATE_TALBE_PHF_NHAPBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "MA_FILE_PK", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "MA_FILE_PK");
        }
    }
}
