namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07022020_UPDATE_DB_ADD_COLUMN_PHF_XD_KH_THANHTRA_BO_CHITIET_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO_CHITIET", "LY_DO");
        }
    }
}
