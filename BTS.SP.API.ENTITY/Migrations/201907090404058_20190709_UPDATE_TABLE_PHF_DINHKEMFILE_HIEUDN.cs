namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190709_UPDATE_TABLE_PHF_DINHKEMFILE_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DINHKEMFILE", "URL", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DINHKEMFILE", "URL");
        }
    }
}
