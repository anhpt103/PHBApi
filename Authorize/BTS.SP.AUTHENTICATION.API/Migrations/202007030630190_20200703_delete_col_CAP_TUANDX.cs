namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200703_delete_col_CAP_TUANDX : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTAUTH.AU_NGUOIDUNG", "CAP");
        }
        


        public override void Down()
        {
            AddColumn("BTAUTH.AU_NGUOIDUNG", "CAP", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
    }
}
