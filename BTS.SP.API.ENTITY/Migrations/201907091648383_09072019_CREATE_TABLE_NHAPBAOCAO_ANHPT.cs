namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09072019_CREATE_TABLE_NHAPBAOCAO_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_NHAPBAOCAO_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        MACOT = c.String(nullable: false, maxLength: 50),
                        MADONG = c.String(nullable: false, maxLength: 50),
                        TENDONG = c.String(nullable: false, maxLength: 500),
                        SOTHUTU = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_NHAPBAOCAO",
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
            DropTable("BTSTC.PHF_NHAPBAOCAO");
            DropTable("BTSTC.PHF_NHAPBAOCAO_CHITIET");
        }
    }
}
