namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_EditCoQuanThu_Duonghh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.DM_COQUANTHU", "TEN_COQUANTHU", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.DM_COQUANTHU", "TEN_COQUANTHU", c => c.String(maxLength: 50));
        }
    }
}
