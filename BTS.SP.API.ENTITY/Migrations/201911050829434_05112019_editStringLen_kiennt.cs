namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05112019_editStringLen_kiennt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHI_DAN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHA_THONGTRI_YDUTOAN", "CHI_DAN", c => c.String(maxLength: 250));
        }
    }
}
