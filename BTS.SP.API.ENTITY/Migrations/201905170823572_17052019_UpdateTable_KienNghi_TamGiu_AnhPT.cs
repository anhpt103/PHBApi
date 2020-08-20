namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17052019_UpdateTable_KienNghi_TamGiu_AnhPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_DULIEU", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_DULIEU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT");
        }
    }
}
