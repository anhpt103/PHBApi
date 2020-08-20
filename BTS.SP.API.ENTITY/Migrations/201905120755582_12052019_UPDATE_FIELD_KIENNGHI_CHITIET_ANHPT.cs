namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12052019_UPDATE_FIELD_KIENNGHI_CHITIET_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "MA_CHUONG", c => c.String(maxLength: 150));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "COQUAN_QUANLYTHU", c => c.String(maxLength: 150));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "KHOBAC", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "KHOBAC");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "COQUAN_QUANLYTHU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET", "MA_CHUONG");
        }
    }
}
