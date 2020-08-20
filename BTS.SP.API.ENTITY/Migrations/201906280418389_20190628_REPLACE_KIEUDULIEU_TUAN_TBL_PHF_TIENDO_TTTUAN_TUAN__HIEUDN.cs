namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190628_REPLACE_KIEUDULIEU_TUAN_TBL_PHF_TIENDO_TTTUAN_TUAN__HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TIENDO_TTTUAN_TUAN", "TUAN", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TIENDO_TTTUAN_TUAN", "TUAN", c => c.String(maxLength: 50));
        }
    }
}
