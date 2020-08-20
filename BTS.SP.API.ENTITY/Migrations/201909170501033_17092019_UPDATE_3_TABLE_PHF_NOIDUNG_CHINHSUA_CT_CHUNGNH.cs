namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17092019_UPDATE_3_TABLE_PHF_NOIDUNG_CHINHSUA_CT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "MACOT", c => c.String(nullable: false, maxLength: 50));
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "MADONG", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "TENDONG", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "SOTHUTU", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "SOTHUTU_HIENTHI", c => c.String());
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "DINH_DANG", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOIDUNG_CHINHSUA");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA", "NOIDUNG_CHINHSUA");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_NOIDUNG_CHINHSUA", "NOIDUNG_CHINHSUA", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_NHAPBAOCAO_CHITIET", "NOIDUNG_CHINHSUA", c => c.String(maxLength: 2000));
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "DINH_DANG");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "SOTHUTU_HIENTHI");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "SOTHUTU");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "TENDONG");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "MADONG");
            DropColumn("BTSTC.PHF_NOIDUNG_CHINHSUA_CT", "MACOT");
        }
    }
}
