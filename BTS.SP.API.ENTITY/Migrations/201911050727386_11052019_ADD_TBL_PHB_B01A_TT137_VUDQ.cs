namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11052019_ADD_TBL_PHB_B01A_TT137_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_B01A_TT137_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_B01A_TT137_REFID = c.String(nullable: false, maxLength: 50),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        MA_CHI_TIEU = c.String(maxLength: 50),
                        TEN_CHI_TIEU = c.String(maxLength: 1000),
                        DU_TOAN = c.Double(nullable: false),
                        THUC_HIEN = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_B01A_TT137_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        MA_CHI_TIEU = c.String(maxLength: 50),
                        TEN_CHI_TIEU = c.String(maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_B01A_TT137",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_CHUONG = c.String(nullable: false, maxLength: 3),
                        MA_QHNS = c.String(nullable: false, maxLength: 10),
                        TEN_QHNS = c.String(maxLength: 255),
                        MA_QHNS_CHA = c.String(maxLength: 10),
                        MA_DV_SDNS = c.String(maxLength: 50),
                        MA_SO_SDNS = c.String(maxLength: 50),
                        MA_KBNN = c.String(maxLength: 50),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        KY_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_B01A_TT137");
            DropTable("BTSTC.PHB_B01A_TT137_TEMPLATE");
            DropTable("BTSTC.PHB_B01A_TT137_DETAIL");
        }
    }
}
