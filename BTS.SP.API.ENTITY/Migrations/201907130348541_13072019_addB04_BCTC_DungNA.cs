namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13072019_addB04_BCTC_DungNA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_B04_BCTC_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHA_B04_BCTC_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 250),
                        MA_SO = c.String(nullable: false, maxLength: 250),
                        MA_SO_CHA = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SO_CUOI_NAM = c.Decimal(precision: 18, scale: 2),
                        SO_DAU_NAM = c.Decimal(precision: 18, scale: 2),
                        TONG_CONG = c.Decimal(precision: 18, scale: 2),
                        TSCD_HUU_HINH = c.Decimal(precision: 18, scale: 2),
                        TSCD_VO_HINH = c.Decimal(precision: 18, scale: 2),
                        NGUON_VON_KD = c.Decimal(precision: 18, scale: 2),
                        CHENH_LECH_TY_GIA = c.Decimal(precision: 18, scale: 2),
                        THANG_DU_LUY_KE = c.Decimal(precision: 18, scale: 2),
                        CAC_QUY = c.Decimal(precision: 18, scale: 2),
                        CAI_CACH_TIEN_LUON = c.Decimal(precision: 18, scale: 2),
                        KHAC = c.Decimal(precision: 18, scale: 2),
                        CONG = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_B04_BCTC_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 250),
                        MA_SO = c.String(nullable: false, maxLength: 250),
                        MA_SO_CHA = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_B04_BCTC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DONVI = c.String(maxLength: 150),
                        DON_VI_DT = c.String(maxLength: 150),
                        CAP_DU_TOAN = c.String(maxLength: 150),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_B04_BCTC");
            DropTable("BTSTC.PHA_B04_BCTC_TEMPLATE");
            DropTable("BTSTC.PHA_B04_BCTC_DETAIL");
        }
    }
}
