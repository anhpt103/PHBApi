namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07022020_UPDATE_DB_PHF_XD_KH_THANHTRA_BO_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO", c => c.String());
        }
    }
}
