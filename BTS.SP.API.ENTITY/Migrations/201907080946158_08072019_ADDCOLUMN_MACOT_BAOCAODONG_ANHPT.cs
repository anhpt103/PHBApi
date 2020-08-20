namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08072019_ADDCOLUMN_MACOT_BAOCAODONG_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MACOT", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MACOT");
        }
    }
}
