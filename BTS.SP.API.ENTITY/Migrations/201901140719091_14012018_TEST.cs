namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012018_TEST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_05TT_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        NOIDUNG = c.String(maxLength: 1000),
                        SOTIEN = c.String(maxLength: 1000),
                        THOIHAN = c.String(maxLength: 1000),
                        NGUON_KHAUHAO = c.String(maxLength: 1000),
                        NGUON_LOINHUAN = c.String(maxLength: 1000),
                        NGUONKHAC = c.String(maxLength: 1000),
                        THIEUNGUON_KHAUHAO = c.String(maxLength: 1000),
                        THIEUNGUON_LOINHUAN = c.String(maxLength: 1000),
                        THIEUNGUON_KHAC = c.String(maxLength: 1000),
                        CHENHLECH = c.String(maxLength: 1000),
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
            
            DropTable("BTSTC.PHF_PL01_XLKN");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.PHF_PL01_XLKN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 500),
                        TONG_CUOC_THANHTRA = c.String(maxLength: 500),
                        TONG_DOAN_THANHTRA = c.String(maxLength: 500),
                        TD_DANGTHUCHIEN = c.String(maxLength: 500),
                        TD_DANGDUTHAO_KL = c.String(maxLength: 500),
                        TD_DACONGBODUTHAOKL = c.String(maxLength: 500),
                        TD_DANGTRINHLANHDAOBO = c.String(maxLength: 500),
                        DALUHANH_KL = c.String(maxLength: 500),
                        GHI_CHU = c.String(maxLength: 500),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        PHONG_THANHTRA = c.String(maxLength: 500),
                        DOT_BAOCAO = c.String(maxLength: 500),
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
            
            DropTable("BTSTC.PHF_BM_05TT_TCDN");
        }
    }
}
