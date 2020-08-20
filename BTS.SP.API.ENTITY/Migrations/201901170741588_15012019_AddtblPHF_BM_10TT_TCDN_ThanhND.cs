namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15012019_AddtblPHF_BM_10TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_10TT_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CACKHOAN_PHAINOP = c.String(maxLength: 200),
                        SO_PHAINOP = c.String(maxLength: 200),
                        SO_PHATSINH = c.String(maxLength: 200),
                        SO_DANOP = c.String(maxLength: 200),
                        SCPN_TONGSO = c.String(maxLength: 200),
                        SCPN_SOCHAMNOP = c.String(maxLength: 200),
                        SCPN_NGUYENNHAN = c.String(maxLength: 200),
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
            DropTable("BTSTC.PHF_BM_10TT_TCDN");
        }
    }
}
