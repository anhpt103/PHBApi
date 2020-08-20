namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _08042019_UpdateTable_KienNghiThanhTra : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "TUNAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "DENNAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "SHEET_DULIEU", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "EXCEL_FILENAME", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "NAM");
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "QUY");
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "NGAY");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "NGAY", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "QUY", c => c.String(maxLength: 20));
            AddColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "EXCEL_FILENAME");
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "SHEET_DULIEU");
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "DENNAM");
            DropColumn("BTSTC.PHF_UPDATE_XULYKIENNGHI", "TUNAM");
        }
    }
}
