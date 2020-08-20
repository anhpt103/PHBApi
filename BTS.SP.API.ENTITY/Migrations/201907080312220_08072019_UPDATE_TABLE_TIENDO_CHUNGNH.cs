namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08072019_UPDATE_TABLE_TIENDO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "MA_TUAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "TUAN", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "MA_TUAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "MA_PHONGBAN", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "INDEX");
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "KETQUA_THANHTRA");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "MA_PHIEU");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "LOAI_BAOCAO");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "MAPHONG");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "DUKIEN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "DUKIEN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "MAPHONG", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "LOAI_BAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "MA_PHIEU", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "KETQUA_THANHTRA", c => c.String(maxLength: 1000));
            AddColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "INDEX", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "MA_PHONGBAN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "MA_TUAN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "TUAN");
            DropColumn("BTSTC.PHF_TIENDO_TUAN_CHITIET", "MA_TUAN");
        }
    }
}
