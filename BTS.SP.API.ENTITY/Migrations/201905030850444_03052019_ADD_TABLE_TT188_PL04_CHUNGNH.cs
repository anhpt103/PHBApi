namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03052019_ADD_TABLE_TT188_PL04_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT188_PL04_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 10),
                        SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOIDUNG = c.String(maxLength: 500),
                        DONVITINH = c.String(maxLength: 50),
                        MADONG = c.String(maxLength: 50),
                        INDAM = c.Decimal(precision: 10, scale: 0),
                        INNGHIENG = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT188_PL04",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 100),
                        MADONG = c.String(maxLength: 50),
                        DONVI = c.String(maxLength: 500),
                        COT1 = c.Decimal(precision: 18, scale: 2),
                        COT2 = c.Decimal(precision: 18, scale: 2),
                        COT3 = c.Decimal(precision: 18, scale: 2),
                        COT4 = c.Decimal(precision: 18, scale: 2),
                        COT5 = c.Decimal(precision: 18, scale: 2),
                        COT6 = c.Decimal(precision: 18, scale: 2),
                        COT7 = c.Decimal(precision: 18, scale: 2),
                        COT8 = c.Decimal(precision: 18, scale: 2),
                        COT9 = c.Decimal(precision: 18, scale: 2),
                        COT10 = c.Decimal(precision: 18, scale: 2),
                        COT11 = c.Decimal(precision: 18, scale: 2),
                        COT12 = c.Decimal(precision: 18, scale: 2),
                        COT13 = c.Decimal(precision: 18, scale: 2),
                        COT14 = c.Decimal(precision: 18, scale: 2),
                        COT15 = c.Decimal(precision: 18, scale: 2),
                        COT16 = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(maxLength: 300),
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
            DropTable("BTSTC.PHF_TT188_PL04");
            DropTable("BTSTC.PHF_TT188_PL04_TEMPLATE");
        }
    }
}
