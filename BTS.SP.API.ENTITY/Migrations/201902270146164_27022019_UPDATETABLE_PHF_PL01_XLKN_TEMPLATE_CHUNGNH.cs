namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27022019_UPDATETABLE_PHF_PL01_XLKN_TEMPLATE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.String(maxLength: 50));
        }
    }
}
