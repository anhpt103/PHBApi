namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190510_ADD_TT129_PL1A_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT129_PL1A_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 10),
                        SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOIDUNG = c.String(maxLength: 500),
                        DIEMTOIDA = c.Decimal(precision: 18, scale: 2),
                        MADONG = c.String(maxLength: 50),
                        INDAM = c.Decimal(precision: 10, scale: 0),
                        INNGHIENG = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT129_PL1A",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 100),
                        MADONG = c.String(maxLength: 50),
                        DIEM_TUDANHGIA = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT129",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        TENBAOCAO = c.String(maxLength: 500),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 100),
                        GIMFILE = c.String(maxLength: 500),
                        URLFILE = c.String(maxLength: 250),
                        TUNGAY = c.DateTime(nullable: false),
                        DENNGAY = c.DateTime(nullable: false),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MAPHONGBAN = c.String(maxLength: 50),
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
            DropTable("BTSTC.PHF_TT129");
            DropTable("BTSTC.PHF_TT129_PL1A");
            DropTable("BTSTC.PHF_TT129_PL1A_TEMPLATE");
        }
    }
}
