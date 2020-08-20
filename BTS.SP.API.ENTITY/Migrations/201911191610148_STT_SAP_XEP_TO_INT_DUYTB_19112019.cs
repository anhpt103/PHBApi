namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class STT_SAP_XEP_TO_INT_DUYTB_19112019 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_C_B06X_DETAIL", "STT_SAP_XEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_C_B06X_DETAIL", "STT_SAP_XEP", c => c.String(maxLength: 50));
        }
    }
}
