namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27032019_addBieu2_Duonghh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BIEU02_TONGHOP_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DOITUONG_THANHTRA = c.String(maxLength: 1000),
                        QUYETDINH_THANHTRA = c.String(maxLength: 1000),
                        KETLUAN_THANHTRA = c.String(maxLength: 1000),
                        MASO_DUAN = c.String(maxLength: 1000),
                        KIENNGHI_THUHOI = c.String(maxLength: 1000),
                        TONGMUC_TONGSO = c.String(maxLength: 1000),
                        TONGMUC_DONGIA = c.String(maxLength: 1000),
                        TONGMUC_KHOILUONG = c.String(maxLength: 1000),
                        TONGMUC_KHAC = c.String(maxLength: 1000),
                        DUTOAN_TONGSO = c.String(maxLength: 1000),
                        DUTOAN_DONGIA = c.String(maxLength: 1000),
                        DUTOAN_KHOILUONG = c.String(maxLength: 1000),
                        DUTOAN_KHAC = c.String(maxLength: 1000),
                        THANHTOAN_TONGSO = c.String(maxLength: 1000),
                        THANHTOAN_DONGIA = c.String(maxLength: 1000),
                        THANHTOAN_KHOILUONG = c.String(maxLength: 1000),
                        THANHTOAN_KHAC = c.String(maxLength: 1000),
                        KIENNGHI_SOTIEN = c.String(maxLength: 1000),
                        KIENNGHI_NGUYENNHAN = c.String(maxLength: 1000),
                        KIENNGHI_KHAC = c.String(maxLength: 1000),
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
            DropTable("BTSTC.PHF_BIEU02_TONGHOP_TCDN");
        }
    }
}
