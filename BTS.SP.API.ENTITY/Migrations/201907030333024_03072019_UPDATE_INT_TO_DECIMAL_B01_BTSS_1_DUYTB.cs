namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03072019_UPDATE_INT_TO_DECIMAL_B01_BTSS_1_DUYTB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TONG_SO", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVKTTG_1", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVKTTG_2", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVDT_CAP1", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_DVDT_CAP1_CUNGTINH", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_DVDT_CAP1_KHACTINH", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_NHA_NUOC", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_NHA_NUOC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_DVDT_CAP1_KHACTINH", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "NGOAI_DVDT_CAP1_CUNGTINH", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVDT_CAP1", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVKTTG_2", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TRONG_DVKTTG_1", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHA_B01_BSTT_1_DETAIL", "TONG_SO", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
