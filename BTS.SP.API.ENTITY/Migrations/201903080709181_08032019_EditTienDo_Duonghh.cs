namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08032019_EditTienDo_Duonghh : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "SO_QUYETDINH", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "NGAY_THANG_QD", c => c.DateTime());
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "TOTRUONG_GIAMSATDOAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "THANHVIEN_GIAMSATDOAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "FILE_DINHKEM", c => c.String(maxLength: 1000));
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "SO_NGAY_THANG_QG");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "GIAMSAT_DOAN");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "MA_DOT");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "MA_DOT", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "GIAMSAT_DOAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "SO_NGAY_THANG_QG", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "FILE_DINHKEM");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "THANHVIEN_GIAMSATDOAN");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "TOTRUONG_GIAMSATDOAN");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "NGAY_THANG_QD");
            DropColumn("BTSTC.PHF_TIENDO_THUCHIEN_KH_CT", "SO_QUYETDINH");
        }
    }
}
