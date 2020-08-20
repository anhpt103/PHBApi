namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10122019_DELETE_REQUIRED_PHB_L_PC_UB_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "HO_VATEN", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CHUC_DANH", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_L_PC_UB", "MA_CHUONG", c => c.String(maxLength: 3));
            AlterColumn("BTSTC.PHB_L_PC_UB", "MA_QHNS", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_L_PC_UB", "MA_QHNS", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("BTSTC.PHB_L_PC_UB", "MA_CHUONG", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "CHUC_DANH", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_L_PC_UB_DETAIL", "HO_VATEN", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
