namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02122019_UPDATE_PHB_L_PC_UB_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHB_DM_CANBO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_CAN_BO = c.String(nullable: false, maxLength: 15),
                        MA_NGACH_LUONG = c.String(nullable: false, maxLength: 15),
                        TEN_CAN_BO = c.String(nullable: false, maxLength: 250),
                        DIA_CHI = c.String(nullable: false, maxLength: 250),
                        CHUC_VU = c.String(nullable: false, maxLength: 100),
                        PHONG_BAN = c.String(nullable: false, maxLength: 100),
                        HE_SO_LUONG = c.Double(nullable: false),
                        GIAM_TRU = c.Double(nullable: false),
                        DTCQ = c.Double(nullable: false),
                        DTNR = c.Double(nullable: false),
                        DTDD = c.Double(nullable: false),
                        EMAIL = c.String(maxLength: 100),
                        GIOI_TINH = c.String(maxLength: 5),
                        SO_CMND = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_CAP = c.DateTime(),
                        NOI_CAP = c.String(maxLength: 50),
                        MA_SO_THUE = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SO_TK_CANHAN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NH_MO_TK = c.String(maxLength: 100),
                        TRANG_THAI = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_DM_TIENLUONG",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MUC_LUONG_TT = c.Double(nullable: false),
                        GIAM_TRU = c.Double(nullable: false),
                        BHXH_CQD = c.Double(nullable: false),
                        BHYT_CQD = c.Double(nullable: false),
                        BHTN_CQD = c.Double(nullable: false),
                        KP_CD_CQD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_L_PC_UB_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        PHB_L_PC_UB_REFID = c.String(nullable: false, maxLength: 50),
                        LOAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT = c.String(maxLength: 5),
                        HO_VATEN = c.String(nullable: false, maxLength: 50),
                        CHUC_DANH = c.String(nullable: false, maxLength: 250),
                        HE_SOLUONG = c.Double(nullable: false),
                        PC_KV = c.Double(nullable: false),
                        PC_CHUCVU = c.Double(nullable: false),
                        PC_THAMNIEN = c.Double(nullable: false),
                        PC_TRACHNHIEM = c.Double(nullable: false),
                        PC_CONGVU = c.Double(nullable: false),
                        PC_LOAIXA = c.Double(nullable: false),
                        PC_LAUNAM = c.Double(nullable: false),
                        PC_THUHUT = c.Double(nullable: false),
                        CKPT_BHXH = c.Double(nullable: false),
                        CKPT_BHYT = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHB_L_PC_UB",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_CHUONG = c.String(nullable: false, maxLength: 3),
                        MA_QHNS = c.String(nullable: false, maxLength: 10),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        KY_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TRANG_THAI = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                        MA_DBHC = c.String(maxLength: 5),
                        MA_DBHC_CHA = c.String(maxLength: 5),
                        TEN_QHNS = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("BTSTC.Students");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.Students",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        Age = c.Decimal(nullable: false, precision: 10, scale: 0),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("BTSTC.PHB_L_PC_UB");
            DropTable("BTSTC.PHB_L_PC_UB_DETAIL");
            DropTable("BTSTC.PHB_DM_TIENLUONG");
            DropTable("BTSTC.PHB_DM_CANBO");
        }
    }
}
