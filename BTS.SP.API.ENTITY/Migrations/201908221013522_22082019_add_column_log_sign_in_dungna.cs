namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22082019_add_column_log_sign_in_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.LOG_SIGNIN", "CHI_TIET", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.LOG_SIGNIN", "GHI_CHU", c => c.String(maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_B121_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_B121_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 250));
            DropColumn("BTSTC.LOG_SIGNIN", "GHI_CHU");
            DropColumn("BTSTC.LOG_SIGNIN", "CHI_TIET");
        }
    }
}
