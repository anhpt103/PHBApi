namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21042019_EditTableBaoCaoChiTiet_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_CAPNHAT_BAOCAO_CHITIET", "KETQUA_THANHTRA", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_CAPNHAT_BAOCAO_CHITIET", "KETQUA_THANHTRA");
        }
    }
}
