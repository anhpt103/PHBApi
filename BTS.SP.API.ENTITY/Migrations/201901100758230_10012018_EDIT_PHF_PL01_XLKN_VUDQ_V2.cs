namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10012018_EDIT_PHF_PL01_XLKN_VUDQ_V2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_PL01_XLKN", "IS_BOLD", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_PL01_XLKN", "IS_ITALIC", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL01_XLKN", "STT", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL01_XLKN", "STT_CHA", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_PL01_XLKN", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_PL01_XLKN", "IS_ITALIC");
            DropColumn("BTSTC.PHF_PL01_XLKN", "IS_BOLD");
            DropColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "IS_ITALIC");
            DropColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "IS_BOLD");
        }
    }
}
