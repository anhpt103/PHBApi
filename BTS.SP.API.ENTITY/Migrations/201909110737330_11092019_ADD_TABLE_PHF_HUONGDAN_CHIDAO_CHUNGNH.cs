namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11092019_ADD_TABLE_PHF_HUONGDAN_CHIDAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_HUONGDAN_CHIDAO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        TEN_VANBAN = c.String(maxLength: 500),
                        FILEDINHKEM = c.String(maxLength: 1000),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NGAY_HIEULUC = c.DateTime(nullable: false),
                        THOIGIAN_CAPNHAT = c.String(maxLength: 30),
                        NGUOI_CAPNHAT = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_HUONGDAN_CHIDAO");
        }
    }
}
