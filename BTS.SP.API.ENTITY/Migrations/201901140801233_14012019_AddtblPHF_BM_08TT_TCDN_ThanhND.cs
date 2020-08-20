namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012019_AddtblPHF_BM_08TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_08TT_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOIDUNG = c.String(maxLength: 200),
                        SOTIEN_VAY = c.String(maxLength: 200),
                        THOIHAN_TRA = c.String(maxLength: 200),
                        NTN_KHAUHAO = c.String(maxLength: 200),
                        NTN_LOINHUAN = c.String(maxLength: 200),
                        NTN_KHAC = c.String(maxLength: 200),
                        TNTN_KHAUHAO = c.String(maxLength: 200),
                        TNTN_LOINHUAN = c.String(maxLength: 200),
                        TNTN_KHAC = c.String(maxLength: 200),
                        CHENHLECH = c.String(maxLength: 200),
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
            DropTable("BTSTC.PHF_BM_08TT_TCDN");
        }
    }
}
