namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15012019_EditPHF_BM_09TT_TCDN_ThanhND3th : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM_09TT_TCDN", "STT", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_CHA", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM_09TT_TCDN", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_BM_09TT_TCDN", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
