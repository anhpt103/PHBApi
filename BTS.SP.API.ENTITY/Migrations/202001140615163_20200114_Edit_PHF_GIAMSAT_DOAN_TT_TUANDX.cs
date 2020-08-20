namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20200114_Edit_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYLAP", c => c.DateTime());
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYQDGS", c => c.DateTime());
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TOTRUONG", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THANHVIEN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THANHVIEN");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "TOTRUONG");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "THOIGIAN_CAPNHAT");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYQDGS");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "NGAYLAP");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "URL");
            DropColumn("BTSTC.PHF_GIAMSAT_DOAN_TT", "FILEDINHKEM");
        }
    }
}
