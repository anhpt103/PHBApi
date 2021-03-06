namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_ADDTABLE_PHF_KHAIBAO_ND_THUCHIEN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_KHAIBAO_ND_THUCHIEN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 200),
                        TENBAOCAO = c.String(maxLength: 500),
                        NAM = c.String(maxLength: 50),
                        DINHKEMFILE = c.String(maxLength: 200),
                        MAPHONGBAN = c.String(maxLength: 50),
                        MADOITUONG = c.String(maxLength: 50),
                        NOIDUNG = c.String(maxLength: 2000),
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
            DropTable("BTSTC.PHF_KHAIBAO_ND_THUCHIEN");
        }
    }
}
