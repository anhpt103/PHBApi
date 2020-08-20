namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190521_Update_TT129_PL03_HieuDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT129_PL03_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT129_PL03_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
        }
    }
}
