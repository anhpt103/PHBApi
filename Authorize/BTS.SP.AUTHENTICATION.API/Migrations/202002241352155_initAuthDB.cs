namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initAuthDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTAUTH.AU_NGUOIDUNG_NHOMQUYEN",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PHANHE = c.String(nullable: false, maxLength: 50),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        MANHOMQUYEN = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.AU_NGUOIDUNG_QUYEN",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PHANHE = c.String(nullable: false, maxLength: 50),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        MACHUCNANG = c.String(nullable: false, maxLength: 50),
                        XEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        THEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SUA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        XOA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DUYET = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.AU_NGUOIDUNG",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        USERNAME = c.String(nullable: false, maxLength: 30),
                        PASSWORD = c.String(nullable: false, maxLength: 500),
                        FULLNAME = c.String(nullable: false, maxLength: 500),
                        EMAIL = c.String(maxLength: 500),
                        PHONE = c.String(maxLength: 20),
                        MA_DBHC = c.String(nullable: false, maxLength: 50),
                        CHUCVU = c.String(maxLength: 50),
                        MA_PHONGBAN = c.String(maxLength: 50),
                        GHICHU = c.String(maxLength: 500),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DBHC_CHA = c.String(maxLength: 50),
                        MA_QHNS = c.String(maxLength: 2000),
                        LOAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.AU_NHOMQUYEN_CHUCNANG",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PHANHE = c.String(nullable: false, maxLength: 50),
                        MANHOMQUYEN = c.String(nullable: false, maxLength: 50),
                        MACHUCNANG = c.String(nullable: false, maxLength: 50),
                        XEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        THEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SUA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        XOA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DUYET = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.AU_NHOMQUYEN",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PHANHE = c.String(nullable: false, maxLength: 50),
                        MANHOMQUYEN = c.String(nullable: false, maxLength: 50),
                        TENNHOMQUYEN = c.String(maxLength: 100),
                        MOTA = c.String(maxLength: 200),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Active = c.Decimal(nullable: false, precision: 1, scale: 0),
                        RefreshTokenLifeTime = c.Decimal(nullable: false, precision: 10, scale: 0),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.LOG_SIGNIN",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        DIACHIMAY = c.String(maxLength: 100),
                        THOIGIANTRUYCAP = c.DateTime(nullable: false),
                        CHUCNANG = c.String(maxLength: 500),
                        CHI_TIET = c.String(maxLength: 1000),
                        GHI_CHU = c.String(maxLength: 1000),
                        DBHC = c.String(maxLength: 100),
                        TEN_DBHC = c.String(maxLength: 500),
                        DBHC_CHA = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BTAUTH.SYS_CHUCNANG",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PHANHE = c.String(nullable: false, maxLength: 50),
                        MACHUCNANG = c.String(nullable: false, maxLength: 30),
                        TENCHUCNANG = c.String(maxLength: 300),
                        MACHA = c.String(maxLength: 30),
                        STATE = c.String(maxLength: 100),
                        SOTHUTU = c.String(maxLength: 50),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("BTAUTH.SYS_CHUCNANG");
            DropTable("BTAUTH.RefreshTokens");
            DropTable("BTAUTH.LOG_SIGNIN");
            DropTable("BTAUTH.Clients");
            DropTable("BTAUTH.AU_NHOMQUYEN");
            DropTable("BTAUTH.AU_NHOMQUYEN_CHUCNANG");
            DropTable("BTAUTH.AU_NGUOIDUNG");
            DropTable("BTAUTH.AU_NGUOIDUNG_QUYEN");
            DropTable("BTAUTH.AU_NGUOIDUNG_NHOMQUYEN");
        }
    }
}
