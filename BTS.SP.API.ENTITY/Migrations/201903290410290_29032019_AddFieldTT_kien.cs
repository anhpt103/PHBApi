namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29032019_AddFieldTT_kien : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_NHANDULIEU_XA", "TRANGTHAI", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_NHANDULIEU_XA", "TRANGTHAI");
        }
    }
}
