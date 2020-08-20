namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05042019duydmnguondp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_DIA_PHUONG", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_CHA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "GHI_CHU", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "USER_NAME", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "USER_NAME", c => c.String(maxLength: 4));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "GHI_CHU", c => c.String(maxLength: 4));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_CHA", c => c.String(maxLength: 4));
            AlterColumn("BTSTC.DM_NGUON_DIA_PHUONG", "MA_NGUON_DIA_PHUONG", c => c.String(maxLength: 4));
        }
    }
}
