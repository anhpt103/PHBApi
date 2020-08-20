namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04072019_UPDATE_TABLE_PHF_DM_BAOCAO_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_CREATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_CREATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_UPDATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_UPDATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_STATE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_COT", "UNITCODE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_CREATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_CREATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_UPDATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_UPDATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_STATE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "UNITCODE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO", "I_CREATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO", "I_CREATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO", "I_UPDATE_DATE", c => c.DateTime());
            AddColumn("BTSTC.PHF_DM_BAOCAO", "I_UPDATE_BY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO", "I_STATE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_BAOCAO", "UNITCODE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO", "UNITCODE");
            DropColumn("BTSTC.PHF_DM_BAOCAO", "I_STATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO", "I_UPDATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO", "I_UPDATE_DATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO", "I_CREATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO", "I_CREATE_DATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "UNITCODE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_STATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_UPDATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_UPDATE_DATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_CREATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "I_CREATE_DATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "UNITCODE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_STATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_UPDATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_UPDATE_DATE");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_CREATE_BY");
            DropColumn("BTSTC.PHF_DM_BAOCAO_COT", "I_CREATE_DATE");
        }
    }
}
