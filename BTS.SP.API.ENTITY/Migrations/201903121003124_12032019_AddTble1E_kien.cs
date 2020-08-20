namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12032019_AddTble1E_kien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT03_BIEU1E",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
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
                        COT17 = c.Decimal(precision: 18, scale: 2),
                        COT18 = c.Decimal(precision: 18, scale: 2),
                        COT19 = c.Decimal(precision: 18, scale: 2),
                        COT20 = c.Decimal(precision: 18, scale: 2),
                        COT21 = c.Decimal(precision: 18, scale: 2),
                        COT22 = c.Decimal(precision: 18, scale: 2),
                        COT23 = c.Decimal(precision: 18, scale: 2),
                        COT24 = c.Decimal(precision: 18, scale: 2),
                        COT25 = c.Decimal(precision: 18, scale: 2),
                        COT26 = c.Decimal(precision: 18, scale: 2),
                        COT27 = c.String(maxLength: 1000),
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
            DropTable("BTSTC.PHF_TT03_BIEU1E");
        }
    }
}
