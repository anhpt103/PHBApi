namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23012019_AddmaDotTienDo_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "MA_DOT", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "MA_DOT");
        }
    }
}
