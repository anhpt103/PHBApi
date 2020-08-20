namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21082019_UPDATE_lenght_Column_CHITIEU_DUYTB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_PBDT_B121_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_NHAPBAOCAO", "NGUOIKY", c => c.DateTime(nullable: false));
            AlterColumn("BTSTC.PHB_PBDT_B121_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
