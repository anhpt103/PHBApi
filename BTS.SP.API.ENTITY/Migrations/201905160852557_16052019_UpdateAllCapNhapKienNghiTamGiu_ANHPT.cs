namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16052019_UpdateAllCapNhapKienNghiTamGiu_ANHPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_DOITUONG", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TEN_DOITUONG", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_DOITUONG_CHA", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TEN_DOITUONG_CHA", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NAMKETLUAN", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NAM", c => c.String(maxLength: 10));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THANG", c => c.String(maxLength: 20));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_KIENNGHI", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_KETLUAN_THANHTRA", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_KETLUAN_THANHTRA", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MASOTHUE", c => c.String(maxLength: 80));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_QUYETDINH_THU", c => c.String(maxLength: 80));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_QUYETDINH_THU", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIATRI_QUYETDINH_THU", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_NDKT", c => c.String(maxLength: 70));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_CHUONG", c => c.String(maxLength: 80));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "COQUAN_QUANLYTHU", c => c.String(maxLength: 150));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KHOBAC", c => c.String(maxLength: 150));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_CHUNGTU", c => c.String(maxLength: 70));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_CHUNGTU", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NOP_TKTG", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_NOP_NSNN", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_XULY_NOP_NSNN", c => c.DateTime());
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_CHUNGTU_NOP_NSNN", c => c.String(maxLength: 70));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SAPXEP", c => c.Decimal(precision: 10, scale: 0));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MABAOCAO");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TENBAOCAO");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TUNAM");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "DENNAM");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SHEET_DULIEU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "EXCEL_FILENAME");
            DropTable("BTSTC.PHF_KIENNGHI_TAMGIU_CHITIET");
        }
        
        public override void Down()
        {
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
                        SO_KETLUAN_THANHTRA = c.String(maxLength: 100),
                        NGAY_KETLUAN_THANHTRA = c.DateTime(),
                        GHITHU_GHICHI_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        GIAM_DUTOAN_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        GIAM_QUYETTOAN_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        THUVE_COPHAN_HOA_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        KIENNGHI_KHAC_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        TONGSO_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        MASOTHUE = c.String(maxLength: 50),
                        SO_QUYETDINH_THU = c.String(maxLength: 70),
                        NGAY_QUYETDINH_THU = c.DateTime(),
                        GIATRI_QUYETDINH_THU = c.Decimal(precision: 18, scale: 2),
                        MA_NDKT = c.String(maxLength: 70),
                        MA_CHUONG = c.String(maxLength: 150),
                        COQUAN_QUANLYTHU = c.String(maxLength: 150),
                        KHOBAC = c.String(maxLength: 150),
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
            
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "EXCEL_FILENAME", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SHEET_DULIEU", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "DENNAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TUNAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TENBAOCAO", c => c.String(maxLength: 300));
            AddColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MABAOCAO", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SAPXEP");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_CHUNGTU_NOP_NSNN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_XULY_NOP_NSNN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_NOP_NSNN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NOP_TKTG");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_CHUNGTU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_CHUNGTU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KHOBAC");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "COQUAN_QUANLYTHU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_CHUONG");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_NDKT");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIATRI_QUYETDINH_THU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_QUYETDINH_THU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_QUYETDINH_THU");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MASOTHUE");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NGAY_KETLUAN_THANHTRA");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "SO_KETLUAN_THANHTRA");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TONGSO_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "KIENNGHI_KHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THUVECP_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMQUYETTOAN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GIAMDUTOAN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "GHITHUCHI_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_KHOANKHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEKHAC_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEXNK_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUETNDN_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_THUCHIEN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NSNN_THUEGTGT_KIENNGHI");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "THANG");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NAM");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "NAMKETLUAN");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TEN_DOITUONG_CHA");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_DOITUONG_CHA");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "TEN_DOITUONG");
            DropColumn("BTSTC.PHF_KIENNGHI_TAMGIU", "MA_DOITUONG");
        }
    }
}
