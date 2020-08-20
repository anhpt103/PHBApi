namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06082019_ADD_TABLE_PHF_CAUHINHBAOCAO_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_CAUHINHBAOCAO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        NGUOILAP = c.String(maxLength: 100),
                        NGAYLAP = c.DateTime(nullable: false),
                        FILEDINHKEM = c.String(maxLength: 1000),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        QUY = c.String(maxLength: 50),
                        TENQUY = c.String(maxLength: 50),
                        NOIDUNG = c.String(maxLength: 1500),
                        MAPHONGBAN = c.String(maxLength: 50),
                        THOIGIAN = c.String(maxLength: 30),
                        URL = c.String(maxLength: 250),
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
            DropTable("BTSTC.PHF_CAUHINHBAOCAO");
        }
    }
}
