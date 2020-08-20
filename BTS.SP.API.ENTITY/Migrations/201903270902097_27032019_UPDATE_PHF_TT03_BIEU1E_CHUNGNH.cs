namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27032019_UPDATE_PHF_TT03_BIEU1E_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU1E", "NOIDUNG", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT03_BIEU1E", "NOIDUNG");
        }
    }
}
