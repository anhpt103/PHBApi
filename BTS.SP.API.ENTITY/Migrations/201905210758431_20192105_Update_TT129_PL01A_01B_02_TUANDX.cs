namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20192105_Update_TT129_PL01A_01B_02_TUANDX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DIEM_TOIDA", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
        }
    }
}
