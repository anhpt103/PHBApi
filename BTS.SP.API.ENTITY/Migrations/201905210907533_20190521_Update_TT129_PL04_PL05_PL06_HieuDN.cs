namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190521_Update_TT129_PL04_PL05_PL06_HieuDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT129_PL04_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT129_PL05_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT129_PL06_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT129_PL06_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_TT129_PL05_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_TT129_PL04_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
        }
    }
}
