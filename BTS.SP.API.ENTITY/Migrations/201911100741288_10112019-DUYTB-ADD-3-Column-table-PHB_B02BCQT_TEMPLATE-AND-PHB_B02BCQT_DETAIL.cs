namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10112019DUYTBADD3ColumntablePHB_B02BCQT_TEMPLATEANDPHB_B02BCQT_DETAIL : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_OPTIONAL", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_OPTIONAL", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_OPTIONAL");
            DropColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_ITALIC");
            DropColumn("BTSTC.PHB_B02BCQT_TEMPLATE", "IS_BOLD");
            DropColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_OPTIONAL");
            DropColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_ITALIC");
            DropColumn("BTSTC.PHB_B02BCQT_DETAIL", "IS_BOLD");
        }
    }
}
