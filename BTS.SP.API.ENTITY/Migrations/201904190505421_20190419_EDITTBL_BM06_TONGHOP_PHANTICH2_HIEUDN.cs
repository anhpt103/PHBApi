namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190419_EDITTBL_BM06_TONGHOP_PHANTICH2_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM06_TONGHOP_PHANTICH", "DONVI_TINH", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM06_TONGHOP_PHANTICH", "DONVI_TINH", c => c.Decimal(precision: 10, scale: 0));
        }
    }
}
