namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02032019_AddLoaiPhanCong_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_DONVI_PHONGBAN", "IS_SUPER", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_XD_KH_THANHTRA_BO", "LOAI_PHIEU", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_XD_KH_THANHTRA_BO", "LOAI_PHIEU");
            DropColumn("BTSTC.PHF_DM_DONVI_PHONGBAN", "IS_SUPER");
        }
    }
}
