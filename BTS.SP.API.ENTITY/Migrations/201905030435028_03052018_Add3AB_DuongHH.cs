namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03052018_Add3AB_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT03_BIEU3A",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        MS = c.String(maxLength: 50),
                        NOIDUNG = c.String(maxLength: 1000),
                        DONVI_TINH = c.String(maxLength: 50),
                        SOLIEU = c.String(maxLength: 50),
                        ISBOLD = c.Decimal(precision: 10, scale: 0),
                        ISITALIC = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT03_BIEU3B",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        TT = c.String(maxLength: 50),
                        TENVU = c.String(maxLength: 1000),
                        TENCOQUSN = c.String(maxLength: 1000),
                        COQUANTHULY = c.String(maxLength: 1000),
                        TOMTAT = c.String(maxLength: 1000),
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
            DropTable("BTSTC.PHF_TT03_BIEU3B");
            DropTable("BTSTC.PHF_TT03_BIEU3A");
        }
    }
}
