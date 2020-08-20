namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04092019_UPDATE_TABLE_PHF_NHAPBAOCAO_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MA_FILE_PK", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "MA_FILE_PK");
        }
    }
}
