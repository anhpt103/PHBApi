namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11022019_BigBigUpdate_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "THONGTIN_NGUOIKY", c => c.String());
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "THONGTIN_NGUOIKY", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "THONGTIN_NGUOIKY");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "THONGTIN_NGUOIKY");
        }
    }
}
