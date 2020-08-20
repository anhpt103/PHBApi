namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28022020_ADDCOLUMN_AU_NGUOIDUNG_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTAUTH.AU_NGUOIDUNG", "PHANHE", c => c.String(maxLength: 50));
            AddColumn("BTAUTH.AU_NGUOIDUNG", "ISADMIN", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTAUTH.AU_NGUOIDUNG", "ISADMIN");
            DropColumn("BTAUTH.AU_NGUOIDUNG", "PHANHE");
        }
    }
}
