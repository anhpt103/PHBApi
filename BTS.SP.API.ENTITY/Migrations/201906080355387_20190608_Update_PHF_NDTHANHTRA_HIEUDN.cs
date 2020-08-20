namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190608_Update_PHF_NDTHANHTRA_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NDTHANHTRA", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "TENQUY", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NDTHANHTRA", "TENQUY");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "QUY");
        }
    }
}
