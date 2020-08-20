namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25072019_CREATE_AUTHORIZE_PHF_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_AU_NGUOIDUNG_DOITUONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 30),
                        MA_DOITUONG = c.String(nullable: false, maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_AU_NGUOIDUNG_QUYEN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        MACHUCNANG = c.String(nullable: false, maxLength: 50),
                        XEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        THEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SUA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        XOA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DUYET = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_AU_NGUOIDUNG_VAITRO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 50),
                        MAVAITRO = c.String(nullable: false, maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_AU_NGUOIDUNG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        USERNAME = c.String(nullable: false, maxLength: 30),
                        PASSWORD = c.String(nullable: false, maxLength: 500),
                        FULLNAME = c.String(nullable: false, maxLength: 500),
                        EMAIL = c.String(maxLength: 500),
                        PHONE = c.String(maxLength: 20),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_AU_VAITRO_CHUCNANG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAVAITRO = c.String(nullable: false, maxLength: 50),
                        MACHUCNANG = c.String(nullable: false, maxLength: 50),
                        XEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        THEM = c.Decimal(nullable: false, precision: 1, scale: 0),
                        SUA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        XOA = c.Decimal(nullable: false, precision: 1, scale: 0),
                        DUYET = c.Decimal(nullable: false, precision: 1, scale: 0),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_AU_VAITRO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAVAITRO = c.String(nullable: false, maxLength: 50),
                        TENVAITRO = c.String(maxLength: 100),
                        MOTA = c.String(maxLength: 200),
                        TRANGTHAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("BTSTC.PHF_SYS_TUDIEN");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_SYS_TUDIEN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_TUDIEN = c.String(nullable: false, maxLength: 50),
                        MA_TUDIEN_CHA = c.String(maxLength: 50),
                        TEN_TUDIEN = c.String(nullable: false, maxLength: 500),
                        LOAI_TUDIEN = c.String(nullable: false, maxLength: 50),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_DONVI = c.String(maxLength: 50),
                        MA_PHONGBAN = c.String(maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("BTSTC.PHF_AU_VAITRO");
            DropTable("BTSTC.PHF_AU_VAITRO_CHUCNANG");
            DropTable("BTSTC.PHF_AU_NGUOIDUNG");
            DropTable("BTSTC.PHF_AU_NGUOIDUNG_VAITRO");
            DropTable("BTSTC.PHF_AU_NGUOIDUNG_QUYEN");
            DropTable("BTSTC.PHF_AU_NGUOIDUNG_DOITUONG");
        }
    }
}
