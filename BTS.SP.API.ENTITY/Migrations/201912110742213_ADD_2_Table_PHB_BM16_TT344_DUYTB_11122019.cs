namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADD_2_Table_PHB_BM16_TT344_DUYTB_11122019 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_BM16_TT344_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_BM16_TT344_REFID = c.String(nullable: false, maxLength: 50),
                        CHUONG = c.String(nullable: false, maxLength: 50),
                        LOAI = c.String(nullable: false, maxLength: 50),
                        KHOAN = c.String(nullable: false, maxLength: 50),
                        MUC = c.String(nullable: false, maxLength: 50),
                        TIEUMUC = c.String(nullable: false, maxLength: 50),
                        SCTTU = c.String(maxLength: 50),
                        SOTIENTU = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SOTIENTT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MA_KBNN = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_BM16_TT344",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_XA = c.String(maxLength: 50),
                        TEN_DIABAN = c.String(maxLength: 50),
                        MA_BAOCAO_TU = c.String(maxLength: 50),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        TONG_TIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_BM16_TT344");
            DropTable("BTSTC.PHB_BM16_TT344_DETAIL");
        }
    }
}
