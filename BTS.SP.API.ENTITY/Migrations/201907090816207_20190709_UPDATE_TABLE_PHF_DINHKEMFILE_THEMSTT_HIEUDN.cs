namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190709_UPDATE_TABLE_PHF_DINHKEMFILE_THEMSTT_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DINHKEMFILE", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DINHKEMFILE", "STT");
        }
    }
}
