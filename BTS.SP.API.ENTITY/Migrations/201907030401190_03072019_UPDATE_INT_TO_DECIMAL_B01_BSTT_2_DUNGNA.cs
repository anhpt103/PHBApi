namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03072019_UPDATE_INT_TO_DECIMAL_B01_BSTT_2_DUNGNA : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT08_2013", "QUY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT08_2013", "TENQUY", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHB_B01_BSTT_2_DETAIL", "NAM_NAY", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_B01_BSTT_2_DETAIL", "NAM_NAY", c => c.Decimal(precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_TT08_2013", "TENQUY");
            DropColumn("BTSTC.PHF_TT08_2013", "QUY");
        }
    }
}
