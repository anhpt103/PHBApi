namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15012018_ADD_TBL_PL02_XLKN_CT_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL02_XLKN_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(maxLength: 100),
                        QUY = c.String(maxLength: 50),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
                        KETLUAN_THANHTRA = c.String(maxLength: 200),
                        TONG_CONG = c.Decimal(precision: 18, scale: 2),
                        NSNN_CONG = c.Decimal(precision: 18, scale: 2),
                        NSNN_THUTHUE = c.Decimal(precision: 18, scale: 2),
                        NSNN_LEPHI = c.Decimal(precision: 18, scale: 2),
                        NSNN_PHAT_VIPHAMHC = c.Decimal(precision: 18, scale: 2),
                        NSNN_CHI_KHONG_DUNG_CHE_DO = c.Decimal(precision: 18, scale: 2),
                        NSNN_KHAC = c.Decimal(precision: 18, scale: 2),
                        GTDT_CONG = c.Decimal(precision: 18, scale: 2),
                        GTDT_TDDT_KODUNG = c.Decimal(precision: 18, scale: 2),
                        GTDT_KHAC = c.Decimal(precision: 18, scale: 2),
                        GTQT_CONG = c.Decimal(precision: 18, scale: 2),
                        GTQT_KODUNG_CHEDO = c.Decimal(precision: 18, scale: 2),
                        GTQT_NGHIEMTHU_KODUNG = c.Decimal(precision: 18, scale: 2),
                        GTQT_KHAC = c.Decimal(precision: 18, scale: 2),
                        KNTCK_CONG = c.Decimal(precision: 18, scale: 2),
                        KNTCK_XULY_HACHTOAN = c.Decimal(precision: 18, scale: 2),
                        KNTCK_GIAM_GT_CONGTRINH = c.Decimal(precision: 18, scale: 2),
                        KNTCK_KHAC = c.Decimal(precision: 18, scale: 2),
                        GHI_CHU = c.String(maxLength: 500),
                        QD_TT_SO_NGAY = c.String(maxLength: 200),
                        TRUONG_DOAN_TT = c.String(maxLength: 200),
                        TV_DOAN_TT = c.String(maxLength: 500),
                        DONVI_DUOC_TT = c.String(maxLength: 500),
                        QUY_III_TK = c.String(maxLength: 500),
                        QUY_III_KL = c.String(maxLength: 500),
                        CHIN_THANG_TK = c.String(maxLength: 500),
                        CHIN_THANG_KL = c.String(maxLength: 500),
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
                "BTSTC.PHF_PL02_XLKN_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
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
            DropTable("BTSTC.PHF_PL02_XLKN_TEMPLATE");
            DropTable("BTSTC.PHF_PL02_XLKN_CT");
        }
    }
}
