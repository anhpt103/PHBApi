namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23082019_add_3_tables_of_phb_pbdt_b1303_dungna : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_PBDT_B1303",
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
                "BTSTC.PHB_PBDT_B1303_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_PBDT_B1303_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        MA_SO = c.String(maxLength: 5),
                        MA_CHA = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 1000),
                        CQ_CHUTRI = c.String(maxLength: 250),
                        THOIGIAN_THUCHIEN = c.String(maxLength: 250),
                        QUYETDINH_PHEDUYET = c.String(maxLength: 250),
                        KPPD_TONGSO = c.Decimal(precision: 18, scale: 2),
                        KPPD_NGUON_NSNN = c.Decimal(precision: 18, scale: 2),
                        KPPD_NGUON_KHAC = c.Decimal(precision: 18, scale: 2),
                        NAMHH_TONGSO = c.Decimal(precision: 18, scale: 2),
                        NAMHH_DT = c.Decimal(precision: 18, scale: 2),
                        NAMHH_UOC_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        NAMHH_KINHPHI_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        LUYKE_TONGSO = c.Decimal(precision: 18, scale: 2),
                        LUYKE_NGUON_NSNN = c.Decimal(precision: 18, scale: 2),
                        LUYKE_NGUON_KHAC = c.Decimal(precision: 18, scale: 2),
                        NAMKH_TONGSO = c.Decimal(precision: 18, scale: 2),
                        NAMKH_NGUON_NSNN = c.Decimal(precision: 18, scale: 2),
                        NAMKH_NGUON_KHAC = c.Decimal(precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_OPTIONAL = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_PBDT_B1303_TEMPLATE",
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
            
            AlterColumn("BTSTC.PHB_PBDT_B05_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_B07_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHB_PBDT_B07_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B07_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("BTSTC.PHB_PBDT_B05_TEMPLATE", "CHI_TIEU", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("BTSTC.PHB_PBDT_B05_DETAIL", "CHI_TIEU", c => c.String(nullable: false, maxLength: 250));
            DropTable("BTSTC.PHB_PBDT_B1303_TEMPLATE");
            DropTable("BTSTC.PHB_PBDT_B1303_DETAIL");
            DropTable("BTSTC.PHB_PBDT_B1303");
        }
    }
}
