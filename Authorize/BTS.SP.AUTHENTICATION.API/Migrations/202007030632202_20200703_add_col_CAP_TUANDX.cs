namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200703_add_col_CAP_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTAUTH.AU_NGUOIDUNG", "CAP", c => c.Decimal(precision: 10, scale: 0));
        }
        


        public override void Down()
        {
            DropColumn("BTAUTH.AU_NGUOIDUNG", "CAP");
        }
    }
}
