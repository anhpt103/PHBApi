namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10072019_UPDATE_TABLE_BAOCAODONG_ANHPT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "TENDONG", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "TENDONG", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
