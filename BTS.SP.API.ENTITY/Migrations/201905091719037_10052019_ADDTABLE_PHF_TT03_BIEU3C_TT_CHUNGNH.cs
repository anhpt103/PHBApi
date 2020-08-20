namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10052019_ADDTABLE_PHF_TT03_BIEU3C_TT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TT03_BIEU3C_TT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE_PK = c.String(maxLength: 200),
                        MABAOCAO = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        NOIDUNG = c.String(maxLength: 1000),
                        GHICHU = c.String(maxLength: 1000),
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
            
            AddColumn("BTSTC.PHF_NDTHANHTRA", "THOIGIAN", c => c.String(maxLength: 30));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "TEN_BIEUMAU", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "TIEUDE_BIEUMAU", c => c.String(maxLength: 500));
            DropColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE");
            DropTable("BTSTC.PHF_TT08_2013_TONGHOP_TEMPLATE");
            DropTable("BTSTC.PHF_TT08_2013_TONGHOP");
            DropTable("BTSTC.PHF_TT08_2013");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_TT08_2013",
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
                        NAM = c.String(maxLength: 10),
                        MAPHONGBAN = c.String(maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT08_2013_TONGHOP",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_FILE = c.String(maxLength: 100),
                        MADONG = c.String(maxLength: 50),
                        SONGUOI_PHAIKEKHAI = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_DAKEKHAI = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_CONGKHAI_NIEMYET = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_CONGKHAI_TOCHUCHOP = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_DUOCXACMINH = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_COKETLUAN = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_XLKL = c.Decimal(precision: 10, scale: 0),
                        SONGUOI_XLTN = c.Decimal(precision: 10, scale: 0),
                        GHICHU = c.String(maxLength: 300),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TT08_2013_TONGHOP_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 10),
                        SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TEN_DONVI = c.String(maxLength: 500),
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
            
            AddColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_NDTHANHTRA", "TIEUDE_BIEUMAU");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "TEN_BIEUMAU");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "THOIGIAN");
            DropTable("BTSTC.PHF_TT03_BIEU3C_TT");
        }
    }
}
