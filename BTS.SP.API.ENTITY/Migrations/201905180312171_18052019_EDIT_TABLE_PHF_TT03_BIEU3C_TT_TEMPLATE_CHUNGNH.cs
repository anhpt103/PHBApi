namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18052019_EDIT_TABLE_PHF_TT03_BIEU3C_TT_TEMPLATE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI_TP", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", "DONVI_TP");
        }
    }
}
