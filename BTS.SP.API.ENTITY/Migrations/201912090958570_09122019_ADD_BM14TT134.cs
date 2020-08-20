namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09122019_ADD_BM14TT134 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_BM14_TT134_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_BM14_TT134_REFID = c.String(nullable: false, maxLength: 50),
                        NGAY_THANG = c.DateTime(nullable: false),
                        NOIDUNG = c.String(nullable: false),
                        SO_TIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_BM14_TT134");
            DropTable("BTSTC.PHB_BM14_TT134_DETAIL");
        }
    }
}
