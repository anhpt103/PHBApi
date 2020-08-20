namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23082019_UPDATE_LENGTH_PHB121_DUYTB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHB_PBDT_B121_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_B121_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
