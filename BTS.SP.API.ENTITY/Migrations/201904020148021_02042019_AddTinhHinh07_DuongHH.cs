namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02042019_AddTinhHinh07_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_07_TINHHINH_CQHC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        CHI_TIEU = c.String(maxLength: 1000),
                        THUCHIEN_NAM = c.Decimal(precision: 18, scale: 2),
                        DUTOAN_PHAPLENH = c.Decimal(precision: 18, scale: 2),
                        THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        THUCHIEN_SO_PHAPLENH = c.Decimal(precision: 18, scale: 2),
                        THUCHIEN_SO_NAM = c.Decimal(precision: 18, scale: 2),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_BM_07_TINHHINH_CQHC");
        }
    }
}
