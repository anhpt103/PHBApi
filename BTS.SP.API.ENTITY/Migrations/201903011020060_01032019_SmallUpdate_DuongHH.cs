namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01032019_SmallUpdate_DuongHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KH_THANHTRA_COQUAN", "MA_PHONGBAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "MA_PHONGBAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_TT_THUOCBO", "MA_PHONGBAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "MA_PHONGBAN", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL01_XLKN_CT", "STT", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL01_XLKN_CT", "STT_CHA", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 5));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_CHA", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 5));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT_CHA", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT", c => c.String(maxLength: 200));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 5));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_CHA", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL04_XLKN_CT", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL03_XLKN_CT", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_TEMPLATE", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_CT", "STT_CHA", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_PL01_XLKN_CT", "STT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_QD_PHEDUYET_THANHTRA", "MA_PHONGBAN");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_TT_THUOCBO", "MA_PHONGBAN");
            DropColumn("BTSTC.PHF_QD_GIAOTHUCHIEN_THANHTRA", "MA_PHONGBAN");
            DropColumn("BTSTC.PHF_KH_THANHTRA_COQUAN", "MA_PHONGBAN");
        }
    }
}
