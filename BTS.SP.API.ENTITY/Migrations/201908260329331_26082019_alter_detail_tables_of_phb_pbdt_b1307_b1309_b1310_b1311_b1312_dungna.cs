namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26082019_alter_detail_tables_of_phb_pbdt_b1307_b1309_b1310_b1311_b1312_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "QD_PHE_DUYET", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "THOIGIAN_THUCHIEN", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "TONG_KINH_PHI", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_DUTOAN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "LUY_KE", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "DU_TOAN", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "DON_VI_TINH");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_SO_DOI_TUONG");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_HE_SO");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_KINH_PHI");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_SO_DOI_TUONG");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_HE_SO");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_DU_TOAN");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_SO_DOI_TUONG");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_HE_SO");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_KINH_PHI");
            DropColumn("BTSTC.PHB_PBDT_B1309_DETAIL", "DON_VI_TINH");
            DropColumn("BTSTC.PHB_PBDT_B1310_DETAIL", "DON_VI_TINH");
            DropColumn("BTSTC.PHB_PBDT_B1311_DETAIL", "DON_VI_TINH");
            DropColumn("BTSTC.PHB_PBDT_B1312_DETAIL", "DON_VI_TINH");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_PBDT_B1312_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHB_PBDT_B1311_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHB_PBDT_B1310_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHB_PBDT_B1309_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_KINH_PHI", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_HE_SO", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMKH_SO_DOI_TUONG", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_DU_TOAN", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_HE_SO", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_SO_DOI_TUONG", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_KINH_PHI", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_HE_SO", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH_SO_DOI_TUONG", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "DON_VI_TINH", c => c.String(maxLength: 250));
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "DU_TOAN");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "LUY_KE");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH_DUTOAN");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMHH");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "NAMTH");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "TONG_KINH_PHI");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "THOIGIAN_THUCHIEN");
            DropColumn("BTSTC.PHB_PBDT_B1307_DETAIL", "QD_PHE_DUYET");
        }
    }
}
