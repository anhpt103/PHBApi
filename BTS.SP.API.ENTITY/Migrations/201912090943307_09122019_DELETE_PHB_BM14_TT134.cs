namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09122019_DELETE_PHB_BM14_TT134 : DbMigration
    {
        public override void Up()
        {
            DropTable("BTSTC.PHB_B03BCQT_BII1_DETAIL");
            DropTable("BTSTC.PHB_B03BCQT_BII1_TEMPLATE");
            DropTable("BTSTC.PHB_B03BCQT_BII1");
            DropTable("BTSTC.PHB_BM14_TT134_DETAIL");
            DropTable("BTSTC.PHB_BM14_TT134");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHB_BM14_TT134",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_XA = c.String(maxLength: 50),
                        MA_KTC = c.String(maxLength: 50),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        TONG_TIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_BM14_TT134_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_BM14_TT134_REFID = c.String(nullable: false, maxLength: 50),
                        NGAY_THANG = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_B03BCQT_BII1",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_CHUONG = c.String(nullable: false, maxLength: 3),
                        MA_QHNS = c.String(nullable: false, maxLength: 10),
                        TEN_QHNS = c.String(maxLength: 255),
                        MA_QHNS_CHA = c.String(maxLength: 10),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        KY_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_B03BCQT_BII1_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        LOAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        MA_NOIDUNGKT = c.String(nullable: false, maxLength: 4),
                        TEN_NOIDUNGKT = c.String(nullable: false, maxLength: 255),
                        INDAM = c.Decimal(precision: 10, scale: 0),
                        INNGHIENG = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_B03BCQT_BII1_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_B03BCQT_BII1_REFID = c.String(nullable: false, maxLength: 50),
                        LOAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_NOIDUNGKT = c.String(nullable: false, maxLength: 4),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        TEN_NOIDUNGKT = c.String(maxLength: 255),
                        SAP_XEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TONG_THU = c.Double(nullable: false),
                        TIEN_NSNN = c.Double(nullable: false),
                        TIEN_KHAUTRU = c.Double(nullable: false),
                        GHI_CHU = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
