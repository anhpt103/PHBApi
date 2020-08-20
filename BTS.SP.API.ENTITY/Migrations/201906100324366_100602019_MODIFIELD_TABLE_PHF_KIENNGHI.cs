namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100602019_MODIFIELD_TABLE_PHF_KIENNGHI : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_DULIEU");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "LOAI_DULIEU", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN");
        }
    }
}
