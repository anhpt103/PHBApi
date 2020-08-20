namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06062019_ANHPT_DELETE_KIENNGHI_TAMGIU : DbMigration
    {
        public override void Up()
        {
            DropTable("BTSTC.PHF_KIENNGHI_TAMGIU");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_KIENNGHI_TAMGIU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHONGBAN = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 200),
                        MA_DOITUONG_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG_CHA = c.String(maxLength: 200),
                        NAMKETLUAN = c.String(maxLength: 10),
                        NAM = c.String(maxLength: 10),
                        THANG = c.String(maxLength: 20),
                        NSNN_THUEGTGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN_THUETNDN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN_THUEXNK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN_THUETN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN_THUEKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN_KHOANKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NSNN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GHITHUCHI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAMDUTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAMQUYETTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THUVECP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KIENNGHI_KHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TONGSO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SO_KETLUAN_THANHTRA = c.String(maxLength: 100),
                        NGAY_KETLUAN_THANHTRA = c.DateTime(),
                        MASOTHUE = c.String(maxLength: 80),
                        SO_QUYETDINH_THU = c.String(maxLength: 80),
                        NGAY_QUYETDINH_THU = c.DateTime(),
                        GIATRI_QUYETDINH_THU = c.Decimal(precision: 18, scale: 2),
                        MA_NDKT = c.String(maxLength: 70),
                        MA_CHUONG = c.String(maxLength: 80),
                        COQUAN_QUANLYTHU = c.String(maxLength: 150),
                        KHOBAC = c.String(maxLength: 150),
                        SO_CHUNGTU = c.String(maxLength: 70),
                        NGAY_CHUNGTU = c.DateTime(),
                        NOP_TKTG = c.Decimal(precision: 18, scale: 2),
                        NGAY_NOP_NSNN = c.DateTime(),
                        NGAY_XULY_NOP_NSNN = c.DateTime(),
                        SO_CHUNGTU_NOP_NSNN = c.String(maxLength: 70),
                        SAPXEP = c.Decimal(precision: 10, scale: 0),
                        LOAI_DULIEU = c.String(maxLength: 50),
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
