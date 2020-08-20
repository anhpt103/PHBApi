namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190608_Update_PHF_NDTHANHTRA_ADDUSER_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NDTHANHTRA", "USER", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NDTHANHTRA", "USER");
        }
    }
}
