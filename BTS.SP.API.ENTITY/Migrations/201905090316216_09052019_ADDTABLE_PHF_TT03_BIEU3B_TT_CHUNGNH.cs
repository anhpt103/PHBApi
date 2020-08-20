namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09052019_ADDTABLE_PHF_TT03_BIEU3B_TT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT03_BIEU3B_TT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE_PK = c.String(maxLength: 200),
                        MABAOCAO = c.String(maxLength: 200),
                        HOTEN = c.String(maxLength: 500),
                        CHUCVU = c.String(maxLength: 500),
                        DONVI = c.String(maxLength: 500),
                        HANHVI = c.String(maxLength: 500),
                        KHIENTRACH = c.Decimal(precision: 10, scale: 0),
                        CANHCAO = c.Decimal(precision: 10, scale: 0),
                        HABACLUONG = c.Decimal(precision: 10, scale: 0),
                        GIANGCHUC = c.Decimal(precision: 10, scale: 0),
                        CACHCHUC = c.Decimal(precision: 10, scale: 0),
                        SATHAI = c.Decimal(precision: 10, scale: 0),
                        SONGAY = c.Decimal(precision: 10, scale: 0),
                        IS_BOLD = c.Decimal(precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_TT03_BIEU3B_TT");
        }
    }
}
