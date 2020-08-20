namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08042019_UpdateTable_KienNghiThanhTra2_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_UPDATE_KIENNGHI_CHITIET", "NAMKETLUAN", c => c.Decimal(nullable: false, precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_UPDATE_KIENNGHI_CHITIET", "NAMKETLUAN");
        }
    }
}
