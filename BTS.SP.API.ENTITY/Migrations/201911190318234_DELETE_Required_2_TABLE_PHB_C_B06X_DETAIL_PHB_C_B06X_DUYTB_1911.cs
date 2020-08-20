namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DELETE_Required_2_TABLE_PHB_C_B06X_DETAIL_PHB_C_B06X_DUYTB_1911 : DbMigration
    {
        public override void Up()
        {
            
            AlterColumn("BTSTC.PHB_C_B06X_DETAIL", "MA_CHITIEU", c => c.String(maxLength: 20));
            AlterColumn("BTSTC.PHB_C_B06X", "MA_CHUONG", c => c.String(maxLength: 3));
            AlterColumn("BTSTC.PHB_C_B06X", "MA_QHNS", c => c.String(maxLength: 10));
           
        }
        
        public override void Down()
        {
          
            AlterColumn("BTSTC.PHB_C_B06X", "MA_QHNS", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("BTSTC.PHB_C_B06X", "MA_CHUONG", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("BTSTC.PHB_C_B06X_DETAIL", "MA_CHITIEU", c => c.String(nullable: false, maxLength: 20));
           
        }
    }
}
