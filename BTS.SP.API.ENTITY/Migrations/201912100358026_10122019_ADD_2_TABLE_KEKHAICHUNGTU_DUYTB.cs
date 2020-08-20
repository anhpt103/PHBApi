namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10122019_ADD_2_TABLE_KEKHAICHUNGTU_DUYTB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.KEKHAICHUNGTUDETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        KEKHAICHUNGTUREFID = c.String(nullable: false, maxLength: 50),
                        SO_CTU = c.String(maxLength: 50),
                        NGAY_THANG = c.DateTime(nullable: false),
                        NOIDUNG = c.String(nullable: false),
                        SO_TIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.KEKHAICHUNGTU",
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
            DropTable("BTSTC.KEKHAICHUNGTU");
            DropTable("BTSTC.KEKHAICHUNGTUDETAIL");
        }
    }
}
