namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190516_Update_TT129_PL01A_01B_02_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129_PL01A", "DIEM_TUDANHGIA", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129_PL01B", "DIEM_TUDANHGIA", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DIEM_TOIDA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129_PL02", "DIEM_TUDANHGIA", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DONVITINH");
            DropColumn("BTSTC.PHF_TT129_PL01A", "KETQUA_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL01A", "KEHOACH_NAM");
            DropColumn("BTSTC.PHF_TT129_PL01A", "KETQUA_NAM");
            DropColumn("BTSTC.PHF_TT129_PL01A", "DOICHIEU_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL01A", "DOICHIEU_KEHOACH");
            DropColumn("BTSTC.PHF_TT129_PL01A", "GHICHU");
            DropColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DONVITINH");
            DropColumn("BTSTC.PHF_TT129_PL01B", "KETQUA_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL01B", "KEHOACH_NAM");
            DropColumn("BTSTC.PHF_TT129_PL01B", "KETQUA_NAM");
            DropColumn("BTSTC.PHF_TT129_PL01B", "DOICHIEU_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL01B", "DOICHIEU_KEHOACH");
            DropColumn("BTSTC.PHF_TT129_PL01B", "GHICHU");
            DropColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DONVITINH");
            DropColumn("BTSTC.PHF_TT129_PL02", "KETQUA_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL02", "KEHOACH_NAM");
            DropColumn("BTSTC.PHF_TT129_PL02", "KETQUA_NAM");
            DropColumn("BTSTC.PHF_TT129_PL02", "DOICHIEU_NAMTRUOC");
            DropColumn("BTSTC.PHF_TT129_PL02", "DOICHIEU_KEHOACH");
            DropColumn("BTSTC.PHF_TT129_PL02", "GHICHU");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TT129_PL02", "GHICHU", c => c.String(maxLength: 300));
            AddColumn("BTSTC.PHF_TT129_PL02", "DOICHIEU_KEHOACH", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02", "DOICHIEU_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02", "KETQUA_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02", "KEHOACH_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02", "KETQUA_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DONVITINH", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129_PL01B", "GHICHU", c => c.String(maxLength: 300));
            AddColumn("BTSTC.PHF_TT129_PL01B", "DOICHIEU_KEHOACH", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B", "DOICHIEU_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B", "KETQUA_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B", "KEHOACH_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B", "KETQUA_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DONVITINH", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TT129_PL01A", "GHICHU", c => c.String(maxLength: 300));
            AddColumn("BTSTC.PHF_TT129_PL01A", "DOICHIEU_KEHOACH", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01A", "DOICHIEU_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01A", "KETQUA_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01A", "KEHOACH_NAM", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01A", "KETQUA_NAMTRUOC", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DONVITINH", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_TT129_PL02", "DIEM_TUDANHGIA");
            DropColumn("BTSTC.PHF_TT129_PL02_TEMPLATE", "DIEM_TOIDA");
            DropColumn("BTSTC.PHF_TT129_PL01B", "DIEM_TUDANHGIA");
            DropColumn("BTSTC.PHF_TT129_PL01B_TEMPLATE", "DIEM_TOIDA");
            DropColumn("BTSTC.PHF_TT129_PL01A", "DIEM_TUDANHGIA");
            DropColumn("BTSTC.PHF_TT129_PL01A_TEMPLATE", "DIEM_TOIDA");
        }
    }
}
