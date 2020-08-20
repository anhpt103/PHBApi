namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190423_ADD_PHF_BIEU01_02_04_TONGHOP_NGUONTHU_GIAINGAN_NGHIAVU_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BIEU01_TONGHOP_NGUONTHU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NGUONTHU = c.String(maxLength: 500),
                        TONGSO_THU = c.Decimal(precision: 18, scale: 2),
                        THEOCD_KEHOACH = c.Decimal(precision: 18, scale: 2),
                        THEOCD_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        THEOCD_TYLETH = c.Decimal(precision: 18, scale: 2),
                        SAICD_TONGSO = c.Decimal(precision: 18, scale: 2),
                        SAICD_NGOAISO = c.Decimal(precision: 18, scale: 2),
                        GHICHU = c.String(maxLength: 500),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "BTSTC.PHF_BIEU04_TONGHOP_NGHIAVU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TRICH_YEU = c.String(maxLength: 500),
                        NGHIAVU_NSNN_TONDONG = c.String(maxLength: 200),
                        PHATHIEN_THANHTRA = c.String(maxLength: 100),
                        NGHIAVU_NSNN_PHAINOP = c.String(maxLength: 200),
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
        
        public override void Down()
        {
            DropTable("BTSTC.PHF_BIEU04_TONGHOP_NGHIAVU");
            DropTable("BTSTC.PHF_BIEU02_TONGHOP_GIAINGAN");
            DropTable("BTSTC.PHF_BIEU01_TONGHOP_NGUONTHU");
        }
    }
}
