namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25042019_UpdateKienNghi_TamGiu_ANHPT : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "BTSTC.PHF_UPDATE_XULYKIENNGHI", newName: "PHF_KIENNGHI_TAMGIU");
            CreateTable(
                "BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 200),
                        MA_DOITUONG_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG_CHA = c.String(maxLength: 200),
                        NAM = c.String(maxLength: 50),
                        NAMKETLUAN = c.String(maxLength: 50),
                        THANG = c.String(maxLength: 50),
                        NOP_NSNN_THUEGTGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETNDN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEXNK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_CACKHOANKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GHITHU_GHICHI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_DUTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_QUYETTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THUVE_COPHAN_HOA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KIENNGHI_KHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TONGSO = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MASOTHUE = c.String(maxLength: 50),
                        SO_QUYETDINH_THU = c.String(maxLength: 70),
                        NGAY_QUYETDINH_THU = c.DateTime(),
                        GIATRI_QUYETDINH_THU = c.Decimal(precision: 18, scale: 2),
                        MA_NDKT = c.String(maxLength: 70),
                        SO_CHUNGTU = c.String(maxLength: 70),
                        NGAY_CHUNGTU = c.DateTime(),
                        NOP_TKTG = c.Decimal(precision: 18, scale: 2),
                        NOP_TRUCTIEP_NSNN = c.Decimal(precision: 18, scale: 2),
                        NGAY_NOP_NSNN = c.DateTime(),
                        NGAY_XULY_NOP_NSNN = c.DateTime(),
                        SO_CHUNGTU_NOP_NSNN = c.String(maxLength: 70),
                        SAPXEP = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TUNAM", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "DENNAM", c => c.Decimal(precision: 10, scale: 0));
            DropTable("BTSTC.PHF_TAIKHOAN_TAMGIU_CHITIET");
            DropTable("BTSTC.PHF_TAIKHOAN_TAMGIU");
            DropTable("BTSTC.PHF_UPDATE_KIENNGHI_CHITIET");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_UPDATE_KIENNGHI_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 200),
                        MA_DOITUONG_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG_CHA = c.String(maxLength: 200),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NAMKETLUAN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        THANG = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOP_NSNN_THUEGTGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETNDN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEXNK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_CACKHOANKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GHITHU_GHICHI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_DUTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_QUYETTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THUVE_COPHAN_HOA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KIENNGHI_KHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TONGSO = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TAIKHOAN_TAMGIU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHIEU = c.String(nullable: false, maxLength: 70),
                        TUNAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DENNAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MAPHONG = c.String(maxLength: 50),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TAIKHOAN_TAMGIU_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHIEU = c.String(nullable: false, maxLength: 70),
                        MA_DOITUONG = c.String(maxLength: 50),
                        MASOTHUE = c.String(maxLength: 50),
                        SO_QUYETDINH_THU = c.String(maxLength: 70),
                        NGAY_QUYETDINH_THU = c.DateTime(),
                        GIATRI_QUYETDINH_THU = c.Decimal(precision: 18, scale: 2),
                        MA_NDKT = c.String(maxLength: 70),
                        SO_CHUNGTU = c.String(maxLength: 70),
                        NGAY_CHUNGTU = c.DateTime(),
                        NOP_TKTG = c.Decimal(precision: 18, scale: 2),
                        NOP_TRUCTIEP_NSNN = c.Decimal(precision: 18, scale: 2),
                        NGAY_NOP_NSNN = c.DateTime(),
                        NGAY_XULY_NOP_NSNN = c.DateTime(),
                        SO_CHUNGTU_NOP_NSNN = c.String(maxLength: 70),
                        SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "DENNAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AlterColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TUNAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropTable("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET");
            RenameTable(name: "BTSTC.PHF_KIENNGHI_TAMGIU", newName: "PHF_UPDATE_XULYKIENNGHI");
        }
    }
}
