namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201908220737045_22082019_alter_b1301_b1302_dungna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_PBDT_B1301_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "DON_VI_TINH", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1302_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "DON_VI_TINH", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_B1302_TEMPLATE", "DON_VI_TINH", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1302_DETAIL", "DON_VI_TINH", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1301_TEMPLATE", "DON_VI_TINH", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B1301_DETAIL", "DON_VI_TINH", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
