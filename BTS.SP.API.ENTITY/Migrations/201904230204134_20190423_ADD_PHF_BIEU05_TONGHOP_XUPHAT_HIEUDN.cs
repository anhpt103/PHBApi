namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190423_ADD_PHF_BIEU05_TONGHOP_XUPHAT_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BIEU05_TONGHOP_XUPHAT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG_VIPHAM = c.String(maxLength: 500),
                        HINHTHUC_PHAT = c.String(maxLength: 200),
                        MUC_PHAT = c.String(maxLength: 100),
                        BIENPHAP_KHACPHUC = c.String(maxLength: 100),
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
                "BTSTC.PHF_BM_FILE_TTTCQuy",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 250),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_BM_FILE_TTTCQuy");
            DropTable("BTSTC.PHF_BIEU05_TONGHOP_XUPHAT");
        }
    }
}
