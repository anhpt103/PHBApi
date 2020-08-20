namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13092019_UPDATE_TABLE_PHF_NHAPBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NHAPBAOCAO", "PHONE", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NHAPBAOCAO", "PHONE");
        }
    }
}
