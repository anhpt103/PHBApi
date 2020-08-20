namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13052019_UPDATE_PHF_TT03_BIEU3B_TT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU3B_TT", "SONGAY", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU3B_TT", "SONGAY", c => c.Decimal(precision: 10, scale: 0));
        }
    }
}
