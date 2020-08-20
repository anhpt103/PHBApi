namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18052019_EDIT_DONVI_TABLE_PHF_TT03_BIEU3C_TT_TEMPLATE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", name: "DONVI_TP", newName: "DONVI");
        }
        
        public override void Down()
        {
            RenameColumn(table: "BTSTC.PHF_TT03_BIEU3C_TT_TEMPLATE", name: "DONVI", newName: "DONVI_TP");
        }
    }
}
