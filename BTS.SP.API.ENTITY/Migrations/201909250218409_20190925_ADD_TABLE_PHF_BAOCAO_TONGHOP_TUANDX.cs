namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190925_ADD_TABLE_PHF_BAOCAO_TONGHOP_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BAOCAO_TONGHOP",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_VANBAN = c.String(maxLength: 200),
                        TEN_VANBAN = c.String(maxLength: 500),
                        FILEDINHKEM = c.String(maxLength: 1000),
                        URL = c.String(maxLength: 250),
                        FILESCAN = c.String(maxLength: 1000),
                        URLSCAN = c.String(maxLength: 250),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        QUY = c.String(maxLength: 50),
                        TENQUY = c.String(maxLength: 50),
                        NGUOIKY = c.String(maxLength: 200),
                        NGAY_HIEULUC = c.DateTime(),
                        THOIGIAN_CAPNHAT = c.String(maxLength: 30),
                        TRANG_THAI = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_BAOCAO_TONGHOP");
        }
    }
}
