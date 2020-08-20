namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15022019_AddLYDO_XDKH_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO");
        }
    }
}
