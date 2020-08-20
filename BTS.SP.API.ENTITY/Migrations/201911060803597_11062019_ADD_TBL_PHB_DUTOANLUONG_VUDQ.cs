namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11062019_ADD_TBL_PHB_DUTOANLUONG_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_DUTOANLUONG_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_DUTOANLUONG_REFID = c.String(nullable: false, maxLength: 50),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        MA_CHI_TIEU = c.String(maxLength: 50),
                        TEN_CHI_TIEU = c.String(maxLength: 1000),
                        BC_DUOC_CAP = c.Double(nullable: false),
                        BC_CO_MAT = c.Double(nullable: false),
                        MA_NGACH = c.String(maxLength: 200),
                        HS_LUONG = c.Double(nullable: false),
                        HS_PC_CV = c.Double(nullable: false),
                        HS_PC_KV = c.Double(nullable: false),
                        HS_PC_TH = c.Double(nullable: false),
                        HS_PC_LT = c.Double(nullable: false),
                        HS_PC_NN_DH = c.Double(nullable: false),
                        HS_HD_DBQH_DBND = c.Double(nullable: false),
                        HS_PC_UDN = c.Double(nullable: false),
                        HS_PC_TNN = c.Double(nullable: false),
                        HS_PC_TN_NGHE_CV = c.Double(nullable: false),
                        HS_PC_TRUC = c.Double(nullable: false),
                        HS_PC_TN_VUOT_KHUNG = c.Double(nullable: false),
                        HS_PC_DB_KHAC = c.Double(nullable: false),
                        HS_PC_CT_LN = c.Double(nullable: false),
                        HS_PC_LX = c.Double(nullable: false),
                        HS_PC_CT_D_DTCT_XH = c.Double(nullable: false),
                        HS_PC_CVU = c.Double(nullable: false),
                        HS_PC_KHAC = c.Double(nullable: false),
                        CONG_HS = c.Double(nullable: false),
                        TIEN_LUONG_THANG = c.Double(nullable: false),
                        BHXH_CP = c.Double(nullable: false),
                        BHXH_LUONG = c.Double(nullable: false),
                        BHYT_CP = c.Double(nullable: false),
                        BHYT_LUONG = c.Double(nullable: false),
                        BHTN_CP = c.Double(nullable: false),
                        BHTN_LUONG = c.Double(nullable: false),
                        BH_TNLD_BNN_CP = c.Double(nullable: false),
                        BH_TNLD_BNN_LUONG = c.Double(nullable: false),
                        KPCD_CP = c.Double(nullable: false),
                        KPCD_LUONG = c.Double(nullable: false),
                        KPCD_NOP_CD = c.Double(nullable: false),
                        KPCD_DE_LAI_DV = c.Double(nullable: false),
                        THUE_TNCN = c.Double(nullable: false),
                        GT_DC = c.Double(nullable: false),
                        SO_THUC_LINH = c.Double(nullable: false),
                        GHI_CHI = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_DUTOANLUONG_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_CHI_TIEU = c.String(maxLength: 50),
                        MA_CHI_TIEU = c.String(maxLength: 50),
                        TEN_CHI_TIEU = c.String(maxLength: 1000),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_DUTOANLUONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_CHUONG = c.String(nullable: false, maxLength: 3),
                        MA_QHNS = c.String(nullable: false, maxLength: 10),
                        TEN_QHNS = c.String(maxLength: 255),
                        MA_QHNS_CHA = c.String(maxLength: 10),
                        MA_DV_SDNS = c.String(maxLength: 50),
                        MA_SO_SDNS = c.String(maxLength: 50),
                        MA_KBNN = c.String(maxLength: 50),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        KY_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHB_DUTOANLUONG");
            DropTable("BTSTC.PHB_DUTOANLUONG_TEMPLATE");
            DropTable("BTSTC.PHB_DUTOANLUONG_DETAIL");
        }
    }
}
