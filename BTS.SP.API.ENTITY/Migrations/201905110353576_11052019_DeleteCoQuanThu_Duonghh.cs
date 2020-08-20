namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_DeleteCoQuanThu_Duonghh : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "BTSTC.DM_COQUANTHU", newName: "PHF_DM_COQUANTHU");
        }
        
        public override void Down()
        {
            RenameTable(name: "BTSTC.PHF_DM_COQUANTHU", newName: "DM_COQUANTHU");
        }
    }
}
