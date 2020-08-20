namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22082019_add_3_table_PHB_PBDT_TT342_B111_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B111",
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
                        NAM_THUC_HIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_HIEN_HANH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM_KE_HOACH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CHUONG = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B111_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B111_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        NAMHH_DT_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        NAMHH_DT_CHI_DTPT = c.Decimal(precision: 18, scale: 2),
                        NAMHH_DT_CHI_THUONGXUYEN = c.Decimal(precision: 18, scale: 2),
                        NAMHH_UOCTH_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        NAMHH_UOCTH_CHI_DTPT = c.Decimal(precision: 18, scale: 2),
                        NAMHH_UOCTH_CHI_THUONGXUYEN = c.Decimal(precision: 18, scale: 2),
                        NAMKH_DT_TONG_SO = c.Decimal(precision: 18, scale: 2),
                        NAMKH_DT_CHI_DTPT = c.Decimal(precision: 18, scale: 2),
                        NAMKH_DT_CHI_THUONG_XUYEN = c.Decimal(precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B111_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_PBDT_B111_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B111_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B111");
        }
    }
}
