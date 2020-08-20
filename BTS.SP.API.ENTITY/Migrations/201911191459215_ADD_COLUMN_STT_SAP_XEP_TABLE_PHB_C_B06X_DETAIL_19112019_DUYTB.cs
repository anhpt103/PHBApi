namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_COLUMN_STT_SAP_XEP_TABLE_PHB_C_B06X_DETAIL_19112019_DUYTB : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_C_B06X_DETAIL", "STT_SAP_XEP", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_C_B06X_DETAIL", "STT_SAP_XEP");
        }
    }
}
