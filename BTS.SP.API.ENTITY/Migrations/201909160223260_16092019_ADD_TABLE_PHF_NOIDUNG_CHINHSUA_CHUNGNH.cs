namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16092019_ADD_TABLE_PHF_NOIDUNG_CHINHSUA_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_NOIDUNG_CHINHSUA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        TENBAOCAO = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        QUY = c.String(maxLength: 50),
                        TENQUY = c.String(maxLength: 50),
                        NOIDUNG_CHINHSUA = c.String(maxLength: 2000),
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
            DropTable("BTSTC.PHF_NOIDUNG_CHINHSUA");
        }
    }
}
