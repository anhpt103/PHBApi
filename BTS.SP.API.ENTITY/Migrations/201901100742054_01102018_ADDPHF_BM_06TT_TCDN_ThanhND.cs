namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01102018_ADDPHF_BM_06TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_06TT_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        QUYETDINH_PHEDUYET = c.String(maxLength: 200),
                        TMDTPD_TONGMUC = c.String(maxLength: 50),
                        TMDTPD_TUCO = c.String(maxLength: 50),
                        TMDTPD_NSCAP = c.String(maxLength: 50),
                        TMDTPD_VAYUUDAI = c.String(maxLength: 50),
                        TMDTPD_VAYNGANHANG = c.String(maxLength: 50),
                        TMDTPD_KHAC = c.String(maxLength: 50),
                        GIATRI_KLNT = c.String(maxLength: 50),
                        THGN__TONGSO = c.String(maxLength: 50),
                        THGN__TAMUNG_CHUDAUTU = c.String(maxLength: 50),
                        THGN__TAMUNG_NHATHAU = c.String(maxLength: 50),
                        THGN_THANHTOAN_TUCO = c.String(maxLength: 50),
                        THGN_THANHTOAN_NSCAP = c.String(maxLength: 50),
                        THGN_THANHTOAN_VAYNGANHANG = c.String(maxLength: 50),
                        THGN_THANHTOAN_KHAC = c.String(maxLength: 50),
                        TDTHDA_PHEDUYET_KHOICONG = c.String(maxLength: 50),
                        TDTHDA_PHEDUYET_HOANTHANH = c.String(maxLength: 50),
                        TDTHDA_THUCTE_KHOICONG = c.String(maxLength: 50),
                        TDTHDA_THUCTE_HOANTHANH = c.String(maxLength: 50),
                        GT_KHOILUONG_DODANG = c.String(maxLength: 50),
                        MA_FILE_PK = c.String(maxLength: 200),
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
            DropTable("BTSTC.PHF_BM_06TT_TCDN");
        }
    }
}
