namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblPHF_BM_07TT_TCDN_ThanhND : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_BM_07TT_TCDN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MA_FILE = c.String(maxLength: 200),
                        MA_FILE_PK = c.String(maxLength: 200),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        TOCHUC_CANHAN = c.String(maxLength: 200),
                        QDPD_SO = c.String(maxLength: 200),
                        QDPD_NGAYTHANG = c.String(maxLength: 200),
                        QDPD_SOTIEN = c.String(maxLength: 200),
                        HOPDONG_SO = c.String(maxLength: 200),
                        HOPDONG_NGAYTHANG = c.String(maxLength: 200),
                        HOPDONG_SOTIEN = c.String(maxLength: 200),
                        THOIGIAN_GOP = c.String(maxLength: 200),
                        THOIGIAN_GOPDEN = c.String(maxLength: 200),
                        TYLE_SHVGOP = c.String(maxLength: 200),
                        NGVG_COPHANHOA = c.String(maxLength: 200),
                        NGVG_THANHLAPMOI = c.String(maxLength: 200),
                        NGVG_KHAC = c.String(maxLength: 200),
                        DOANHTHU_THUNHAP = c.String(maxLength: 200),
                        LOINHUAN_THUCHIEN = c.String(maxLength: 200),
                        CTLN_NAM = c.String(maxLength: 200),
                        CTLN_LUYKE = c.String(maxLength: 200),
                        DUPHONG_GIAMGIA = c.String(maxLength: 200),
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
            DropTable("BTSTC.PHF_BM_07TT_TCDN");
        }
    }
}
