namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17072019_ADD_TABLE_PHF_DECUONGKEKHAI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DECUONGKEKHAI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 100),
                        TENBAOCAO = c.String(maxLength: 500),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAM = c.String(maxLength: 50),
                        DINHKEMFILE = c.String(maxLength: 200),
                        MAPHONGBAN = c.String(maxLength: 50),
                        MADOITUONG = c.String(maxLength: 50),
                        LINHVUC = c.String(maxLength: 50),
                        NGAY_LAPPHIEU = c.DateTime(),
                        URL = c.String(maxLength: 250),
                        THOIGIAN = c.String(maxLength: 30),
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
            DropTable("BTSTC.PHF_DECUONGKEKHAI");
        }
    }
}
