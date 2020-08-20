namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190603_UPDATE_TABLE_TT08_2013_TONGHOP_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT08_2013_TONGHOP", "SONGUOI_XLKL_KHONGTT", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT08_2013_TONGHOP", "SONGUOI_XLKL_KHONGTT");
        }
    }
}
