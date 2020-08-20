namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _060819_update_pha_log_signin_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.LOG_SIGNIN", "DBHC", c => c.String(maxLength: 100));
            AddColumn("BTSTC.LOG_SIGNIN", "TEN_DBHC", c => c.String(maxLength: 500));
            AddColumn("BTSTC.LOG_SIGNIN", "DBHC_CHA", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.LOG_SIGNIN", "DBHC_CHA");
            DropColumn("BTSTC.LOG_SIGNIN", "TEN_DBHC");
            DropColumn("BTSTC.LOG_SIGNIN", "DBHC");
        }
    }
}
