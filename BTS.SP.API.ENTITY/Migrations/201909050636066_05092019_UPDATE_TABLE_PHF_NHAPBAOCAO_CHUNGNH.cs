namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05092019_UPDATE_TABLE_PHF_NHAPBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "HANBAOCAO", c => c.String(maxLength: 50));
        }
    }
}
