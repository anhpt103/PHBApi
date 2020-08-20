namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05032019_AddDuThao_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "SO_DUTHAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "NOIDUNG_DUTHAO", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "NGAY_DUTHAO", c => c.DateTime());
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "LOAI", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "LOAI");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "NGAY_DUTHAO");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "NOIDUNG_DUTHAO");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "SO_DUTHAO");
        }
    }
}
