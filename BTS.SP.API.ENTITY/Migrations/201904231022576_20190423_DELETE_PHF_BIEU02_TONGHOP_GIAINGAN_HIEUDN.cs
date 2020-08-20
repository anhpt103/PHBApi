namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190423_DELETE_PHF_BIEU02_TONGHOP_GIAINGAN_HIEUDN : DbMigration
    {
        public override void Up()
        {
            DropTable("BTSTC.PHF_BIEU02_TONGHOP_GIAINGAN");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_BIEU02_TONGHOP_GIAINGAN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NGUON_KINHPHI = c.String(maxLength: 500),
                        KEHOACHGN_TONG = c.Decimal(precision: 18, scale: 2),
                        KEHOACHGN_NAM = c.Decimal(precision: 18, scale: 2),
                        THUCHIENGN_TONG = c.Decimal(precision: 18, scale: 2),
                        THUCHIENGN_NAM = c.Decimal(precision: 18, scale: 2),
                        TYLEGN_TONG = c.Decimal(precision: 18, scale: 2),
                        TYLEGN_NAM = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
