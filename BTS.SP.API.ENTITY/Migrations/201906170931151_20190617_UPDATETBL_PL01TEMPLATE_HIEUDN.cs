namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190617_UPDATETBL_PL01TEMPLATE_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_TT188_PL01_TEMPLATE", "STT", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_TT188_PL01_TEMPLATE", "STT", c => c.String(maxLength: 10));
        }
    }
}
