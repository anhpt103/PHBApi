namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15022020_UPDATE_DB_NONUMBER_PHF_KIENNGHI_TAMGIU_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGUOI_KIENNGHI", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_KIENNGHI", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHICHU_KIENNGHI", c => c.String(maxLength: 2000));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGUOI_XULY", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_XULY", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_XULY");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGUOI_XULY");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHICHU_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGUOI_KIENNGHI");
        }
    }
}
