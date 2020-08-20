namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26062019_delete_col_PHA_THONGTRI_YDUTOAN_VUDQ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_TCBC_TT03_BIEU01", "SAPXEP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_TCBC_TT03_BIEU01", "SAPXEP", c => c.String(maxLength: 50));
        }
    }
}
