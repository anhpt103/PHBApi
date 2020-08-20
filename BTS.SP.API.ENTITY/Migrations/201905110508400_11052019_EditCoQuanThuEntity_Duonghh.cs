namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_EditCoQuanThuEntity_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "I_CREATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "I_CREATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "I_UPDATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "I_UPDATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "I_STATE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_COQUANTHU", "UNITCODE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "UNITCODE");
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "I_STATE");
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "I_UPDATE_BY");
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "I_UPDATE_DATE");
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "I_CREATE_BY");
            DropColumn("BTSTC.PHF_DM_COQUANTHU", "I_CREATE_DATE");
        }
    }
}
