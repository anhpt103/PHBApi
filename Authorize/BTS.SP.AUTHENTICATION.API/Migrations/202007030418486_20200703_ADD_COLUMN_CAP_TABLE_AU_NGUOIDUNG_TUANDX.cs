namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200703_ADD_COLUMN_CAP_TABLE_AU_NGUOIDUNG_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTAUTH.AU_NGUOIDUNG", "CAP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTAUTH.AU_NGUOIDUNG", "CAP");
        }
    }
}
