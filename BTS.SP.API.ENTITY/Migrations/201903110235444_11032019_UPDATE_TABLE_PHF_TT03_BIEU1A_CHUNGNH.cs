namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11032019_UPDATE_TABLE_PHF_TT03_BIEU1A_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU1A", "DONVI", c => c.String(maxLength: 500));
            AlterColumn("BTSTC.PHF_TT03_BIEU1A", "COT31", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT03_BIEU1A", "COT31", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT03_BIEU1A", "DONVI", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
