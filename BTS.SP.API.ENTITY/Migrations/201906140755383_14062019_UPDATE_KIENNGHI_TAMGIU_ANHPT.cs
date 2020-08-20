namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14062019_UPDATE_KIENNGHI_TAMGIU_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN");
        }
    }
}
