namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_BigUpdate_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3A", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU3B", "MA_FILE_PK", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT03_BIEU3B", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU3A", "MA_FILE_PK");
        }
    }
}
