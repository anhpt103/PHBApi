namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22032019_AddField_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA_DETAIL", "MA_TKTN", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_NHANDULIEU_XA_DETAIL", "MA_TKTN");
        }
    }
}
