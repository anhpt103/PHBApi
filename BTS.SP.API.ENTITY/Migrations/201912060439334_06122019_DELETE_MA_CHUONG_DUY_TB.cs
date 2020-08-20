namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06122019_DELETE_MA_CHUONG_DUY_TB : DbMigration
    {
        public override void Up()
        {
            DropColumn("BTSTC.PHB_PBDT_B05", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B06", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B07", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B111", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B121", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B122", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B123", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B124", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B125", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1301", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1302", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1303", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1304", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1305", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1306", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1307", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1308", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1309", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1310", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1311", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1312", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B14", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1501", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B1502", "CHUONG");
            DropColumn("BTSTC.PHB_PBDT_B32", "CHUONG");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_PBDT_B32", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1502", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1501", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B14", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1312", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1311", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1310", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1309", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1308", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1307", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1306", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1305", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1304", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1303", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1302", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B1301", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B125", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B124", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B123", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B122", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B121", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B111", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B07", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B06", "CHUONG", c => c.String());
            AddColumn("BTSTC.PHB_PBDT_B05", "CHUONG", c => c.String());
        }
    }
}
