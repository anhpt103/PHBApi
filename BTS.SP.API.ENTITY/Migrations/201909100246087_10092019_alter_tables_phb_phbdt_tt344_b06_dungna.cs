namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10092019_alter_tables_phb_phbdt_tt344_b06_dungna : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_PBDT_TT344_B06_DETAIL", "CHI_TIEU", c => c.String(maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_TT344_B06_TEMPLATE", "CHI_TIEU", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_TT344_B06_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_TT344_B06_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
