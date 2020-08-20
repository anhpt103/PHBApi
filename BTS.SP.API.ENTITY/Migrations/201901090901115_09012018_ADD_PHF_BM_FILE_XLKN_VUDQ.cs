namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09012018_ADD_PHF_BM_FILE_XLKN_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_FILE_XLKN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 250),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DOT = c.String(maxLength: 50),
                        THOIGIAN = c.String(maxLength: 30),
                        TEN_BIEUMAU = c.String(maxLength: 200),
                        TIEUDE_BIEUMAU = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_BM_FILE_XLKN");
        }
    }
}
