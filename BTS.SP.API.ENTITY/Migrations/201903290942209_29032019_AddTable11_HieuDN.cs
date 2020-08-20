namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29032019_AddTable11_HieuDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_11_TONGHOP_CQHC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        SO_TOKHAI = c.String(maxLength: 1000),
                        NGAY_DANGKY = c.DateTime(nullable: false),
                        LOAIHINH = c.String(maxLength: 1000),
                        TEN_CONGTY = c.String(maxLength: 1000),
                        TEN_HANGKHAIBAO = c.String(maxLength: 1000),
                        MABIEU_THUENK = c.String(maxLength: 1000),
                        TRIGIA_TINHTHUE = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DANGKB_MAHS = c.String(maxLength: 1000),
                        DANGKB_THUESUAT_THUENK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DANGKB_THUESUAT_THUEVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEXUATDC_MAHS = c.String(maxLength: 1000),
                        DEXUATDC_THUESUAT_THUENK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DEXUATDC_THUESUAT_THUEVAT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RASOATTANGTHU_THUENK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RASOATTANGTHU_THUEGTGT = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            DropTable("BTSTC.PHF_BM_11_TONGHOP_CQHC");
        }
    }
}
