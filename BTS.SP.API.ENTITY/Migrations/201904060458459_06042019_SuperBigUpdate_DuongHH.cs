namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06042019_SuperBigUpdate_DuongHH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL01_TINHHINH_TTTCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        TAISAN_CODINH_DAUNAM = c.Decimal(precision: 18, scale: 2),
                        TAISAN_CODINH_CUOINAM = c.Decimal(precision: 18, scale: 2),
                        DAUTU_NGANHAN = c.Decimal(precision: 18, scale: 2),
                        DAUTU_DAIHAN = c.Decimal(precision: 18, scale: 2),
                        NOPHAITHU_NGANHAN = c.Decimal(precision: 18, scale: 2),
                        NOPHAITHU_DAIHAN = c.Decimal(precision: 18, scale: 2),
                        HANG_TONKHO = c.Decimal(precision: 18, scale: 2),
                        CPXDCBDD = c.Decimal(precision: 18, scale: 2),
                        CACKHOAN_KHAC = c.Decimal(precision: 18, scale: 2),
                        TONGCONG = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL02_NGUONVON_TTTCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        NO_TONGCONG = c.Decimal(precision: 18, scale: 2),
                        NO_NGANHAN = c.Decimal(precision: 18, scale: 2),
                        NO_DAIHAN = c.Decimal(precision: 18, scale: 2),
                        NO_KHOANKHAC = c.Decimal(precision: 18, scale: 2),
                        VON_TONGCONG = c.Decimal(precision: 18, scale: 2),
                        VON_CSH = c.Decimal(precision: 18, scale: 2),
                        VON_DTPT = c.Decimal(precision: 18, scale: 2),
                        VON_CHUAPHANPHOI = c.Decimal(precision: 18, scale: 2),
                        VON_KHOANKHAC = c.Decimal(precision: 18, scale: 2),
                        TONGCONG = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL03_DAUTU_TTTCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        GIATRI_DAUTU = c.Decimal(precision: 18, scale: 2),
                        VON_DIEULE = c.Decimal(precision: 18, scale: 2),
                        LNCT_CHIA = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL04_KETQUA_TTTCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        DONVI = c.String(maxLength: 500),
                        TONG_DOANHTHU = c.Decimal(precision: 18, scale: 2),
                        TONG_CHIPHI = c.Decimal(precision: 18, scale: 2),
                        LOINHUAN_THUCHIEN = c.Decimal(precision: 18, scale: 2),
                        LOINHUAN_SAUTHUE = c.Decimal(precision: 18, scale: 2),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MHTC_DDHD", c => c.String());
            AddColumn("BTSTC.PHF_DM_DOITUONG", "COCHE_QL_TC", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_DOITUONG", "COCHE_QL_TC");
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MHTC_DDHD");
            DropTable("BTSTC.PHF_PL04_KETQUA_TTTCDN");
            DropTable("BTSTC.PHF_PL03_DAUTU_TTTCDN");
            DropTable("BTSTC.PHF_PL02_NGUONVON_TTTCDN");
            DropTable("BTSTC.PHF_PL01_TINHHINH_TTTCDN");
        }
    }
}
