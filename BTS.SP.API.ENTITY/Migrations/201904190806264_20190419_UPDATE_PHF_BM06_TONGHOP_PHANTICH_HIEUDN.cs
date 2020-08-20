namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190419_UPDATE_PHF_BM06_TONGHOP_PHANTICH_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM06_TONGHOP_PHANTICH", "COT10", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM06_TONGHOP_PHANTICH", "COT10", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
