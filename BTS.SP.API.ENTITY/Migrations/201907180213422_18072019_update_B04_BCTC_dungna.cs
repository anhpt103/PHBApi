namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18072019_update_B04_BCTC_dungna : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_SO_CUOI_NAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_SO_DAU_NAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TONG_CONG", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TSCD_HUU_HINH", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TSCD_VO_HINH", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NGUON_VON_KD", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CHENH_LECH_TY_GIA", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_THANG_DU_LUY_KE", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CAC_QUY", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CAI_CACH_TIEN_LUON", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_KHAC", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CONG", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NAM_NAY", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NAM_TRUOC", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NAM_TRUOC");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NAM_NAY");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CONG");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_KHAC");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CAI_CACH_TIEN_LUON");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CAC_QUY");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_THANG_DU_LUY_KE");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_CHENH_LECH_TY_GIA");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_NGUON_VON_KD");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TSCD_VO_HINH");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TSCD_HUU_HINH");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_TONG_CONG");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_SO_DAU_NAM");
            DropColumn("BTSTC.PHA_B04_BCTC_TEMPLATE", "IS_INCLUDED_SO_CUOI_NAM");
        }
    }
}
