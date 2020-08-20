namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14082019_UPDATE_PHF_DM_BAOCAO_DONG_VUDQ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MACOT", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MADONG", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MADONG", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "MACOT", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
