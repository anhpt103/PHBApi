namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05052019_EditSoLieu_DuongHH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU3A", "SOLIEU", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU3A", "SOLIEU", c => c.String(maxLength: 50));
        }
    }
}
