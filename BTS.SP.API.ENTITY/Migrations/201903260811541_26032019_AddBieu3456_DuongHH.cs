namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26032019_AddBieu3456_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BIEU03_CONGTY_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_DONVI = c.String(maxLength: 1000),
                        MASO_THUE = c.String(maxLength: 1000),
                        GIAM_DOC = c.String(maxLength: 1000),
                        KETOAN_TRUONG = c.String(maxLength: 1000),
                        DIEN_THOAI = c.String(maxLength: 1000),
                        DIA_CHI = c.String(maxLength: 1000),
                        NGANHNGHE_KINHDOANH = c.String(maxLength: 1000),
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
            
            CreateTable(
                "BTSTC.PHF_BIEU04_TAICHINH_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_DONVI = c.String(maxLength: 1000),
                        NO_PHAITRA = c.String(maxLength: 1000),
                        NGUONVON_CHUSOHUU = c.String(maxLength: 1000),
                        DOANHTHU = c.String(maxLength: 1000),
                        LOINHUAN_TRUOCTHUE = c.String(maxLength: 1000),
                        VON_DIEULE = c.String(maxLength: 1000),
                        VONGOP_TCT = c.String(maxLength: 1000),
                        TYLE_VONGOP = c.String(maxLength: 1000),
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
            
            CreateTable(
                "BTSTC.PHF_BIEU05_DUAN_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        TEN_DONVI = c.String(maxLength: 1000),
                        TONGMUC_DAUTU = c.String(maxLength: 1000),
                        NGAYBATDAU_THUCHIEN_DA = c.DateTime(),
                        THUCHIEN_NAM = c.String(maxLength: 1000),
                        LUYKE_GIATRI_THUCHIEN = c.String(maxLength: 1000),
                        GHICHU = c.String(maxLength: 1000),
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
            
            CreateTable(
                "BTSTC.PHF_BIEU06_TAILIEU_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG = c.String(maxLength: 1000),
                        GHICHU = c.String(maxLength: 1000),
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
            DropTable("BTSTC.PHF_BIEU06_TAILIEU_TCDN");
            DropTable("BTSTC.PHF_BIEU05_DUAN_TCDN");
            DropTable("BTSTC.PHF_BIEU04_TAICHINH_TCDN");
            DropTable("BTSTC.PHF_BIEU03_CONGTY_TCDN");
        }
    }
}
