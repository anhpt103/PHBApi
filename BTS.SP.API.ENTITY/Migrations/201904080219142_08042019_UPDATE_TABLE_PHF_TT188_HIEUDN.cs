namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08042019_UPDATE_TABLE_PHF_TT188_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT188", "TENFILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_TT188", "DUONGDAN", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT188", "DUONGDAN");
            DropColumn("BTSTC.PHF_TT188", "TENFILE");
        }
    }
}
