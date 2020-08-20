namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03072019_PHB_ADD3TABLE_B01BSTT_DUYTB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_B01_BSTT_1_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 250),
                        MA_SO = c.String(maxLength: 250),
                        MA_SO_CHA = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_B01_BSTT_1",
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
            
            CreateTable(
                "BTSTC.PHA_B01_BSTT_1_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHA_B01_BSTT_1_REFID = c.String(nullable: false, maxLength: 50),
                        STT_SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        CHI_TIEU = c.String(nullable: false, maxLength: 250),
                        MA_SO = c.String(maxLength: 250),
                        MA_SO_CHA = c.String(maxLength: 250),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TONG_SO = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRONG_DVKTTG_1 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRONG_DVKTTG_2 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRONG_DVDT_CAP1 = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGOAI_DVDT_CAP1_CUNGTINH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGOAI_DVDT_CAP1_KHACTINH = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGOAI_NHA_NUOC = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_B01_BSTT_1_DETAIL");
            DropTable("BTSTC.PHA_B01_BSTT_1");
            DropTable("BTSTC.PHA_B01_BSTT_1_TEMPLATE");
        }
    }
}
