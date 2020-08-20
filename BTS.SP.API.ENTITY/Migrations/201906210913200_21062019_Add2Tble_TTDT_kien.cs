namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062019_Add2Tble_TTDT_kien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_THONGTRI_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REF_ID = c.String(maxLength: 100),
                        MA_CHUONG = c.String(maxLength: 10),
                        MA_NGANHKT = c.String(maxLength: 10),
                        MA_NDKT = c.String(maxLength: 10),
                        MA_NV = c.String(maxLength: 10),
                        NOI_DUNG = c.String(maxLength: 10),
                        LOAI_TIEN = c.String(maxLength: 3),
                        SO_TIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_THONGTRI_YDUTOAN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REF_ID = c.String(maxLength: 100),
                        SO_TT = c.String(maxLength: 10),
                        NGAY_TT = c.DateTime(nullable: false),
                        MA_LNV = c.String(maxLength: 10),
                        TEN_LNV = c.String(maxLength: 100),
                        MA_CQTC = c.String(maxLength: 10),
                        TEN_CQTC = c.String(maxLength: 100),
                        MA_DVSDNS = c.String(maxLength: 10),
                        TEN_DVSDNS = c.String(maxLength: 100),
                        MA_CTMT = c.String(maxLength: 10),
                        TEN_CTMT = c.String(maxLength: 100),
                        MA_DBHC = c.String(maxLength: 10),
                        NOI_MO_TK = c.String(maxLength: 250),
                        SO_TK = c.String(maxLength: 100),
                        TRONG_DUTOAN = c.String(maxLength: 1),
                        NIEN_DO = c.String(maxLength: 5),
                        MAU_THONGTRI = c.String(maxLength: 3),
                        NOI_DUNG = c.String(maxLength: 250),
                        CHI_DAN = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_THONGTRI_YDUTOAN");
            DropTable("BTSTC.PHA_THONGTRI_CHITIET");
        }
    }
}
