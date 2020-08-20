namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26032019_EditDoiTuong_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_LINHVUC", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "DIA_CHI", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "TEN_NGUOI_DAIDIEN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "SDT", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MASO_THUE", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "DONVI_CHUQUAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "TAIKHOAN_SO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "DIACHI_GIAODICH", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_NDKT", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_CHUONG", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_DUAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "TEN_DUAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "NHOM_DUAN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "CAP_QD_DAUTU", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "CHU_DAUTU", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "DIADIEM_THUCHIEN_DA", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "THOIGIAN_KHOICONG_HOANTHANH", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "NGUON_VON", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "TONGMUC_DAUTU", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "GIATRI_HOPDONG", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "KEHOACH_VON", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "KHOILUONG_THUCHIEN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_DM_DOITUONG", "GIATRI_THANHTOAN", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_DOITUONG", "GIATRI_THANHTOAN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "KHOILUONG_THUCHIEN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "KEHOACH_VON");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "GIATRI_HOPDONG");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "TONGMUC_DAUTU");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "NGUON_VON");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "THOIGIAN_KHOICONG_HOANTHANH");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "DIADIEM_THUCHIEN_DA");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "CHU_DAUTU");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "CAP_QD_DAUTU");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "NHOM_DUAN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "TEN_DUAN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_DUAN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_CHUONG");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_NDKT");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "DIACHI_GIAODICH");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "TAIKHOAN_SO");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "DONVI_CHUQUAN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MASO_THUE");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "SDT");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "TEN_NGUOI_DAIDIEN");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "DIA_CHI");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_LINHVUC");
        }
    }
}
