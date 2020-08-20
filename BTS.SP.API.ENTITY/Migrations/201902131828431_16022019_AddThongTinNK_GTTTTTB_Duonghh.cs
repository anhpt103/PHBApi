namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16022019_AddThongTinNK_GTTTTTB_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_TT_THUOCBO", "THONGTIN_NGUOIKY", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_TT_THUOCBO", "THONGTIN_NGUOIKY");
        }
    }
}
