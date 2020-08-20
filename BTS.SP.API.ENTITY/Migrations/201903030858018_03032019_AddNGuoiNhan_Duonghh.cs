namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03032019_AddNGuoiNhan_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO", "MA_PHONGBAN_NHAN", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO", "MA_PHONGBAN_NHAN");
        }
    }
}
