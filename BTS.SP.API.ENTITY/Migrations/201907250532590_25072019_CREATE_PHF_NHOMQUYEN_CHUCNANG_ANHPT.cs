namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25072019_CREATE_PHF_NHOMQUYEN_CHUCNANG_ANHPT : DbMigration
    {
        public override void Up()
        {
            DropTable("BTSTC.PHF_AU_NGUOIDUNG_VAITRO");
            DropTable("BTSTC.PHF_AU_VAITRO_CHUCNANG");
            DropTable("BTSTC.PHF_AU_VAITRO");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_AU_VAITRO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAVAITRO = c.String(nullable: false, maxLength: 50),
                        TENVAITRO = c.String(maxLength: 100),
                        MOTA = c.String(maxLength: 200),
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
            
        }
    }
}
