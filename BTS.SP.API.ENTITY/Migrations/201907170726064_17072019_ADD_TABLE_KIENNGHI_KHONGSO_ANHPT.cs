namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17072019_ADD_TABLE_KIENNGHI_KHONGSO_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_KIENNGHI_KHONGSO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHONGBAN = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        NGUOI_KIENNGHI = c.String(maxLength: 50),
                        NGAY_KIENNGHI = c.DateTime(),
                        NOIDUNG_KIENNGHI = c.String(maxLength: 2000),
                        GHICHU_KIENNGHI = c.String(maxLength: 2000),
                        NGUOI_XULY = c.String(maxLength: 50),
                        NGAY_XULY = c.DateTime(),
                        NOIDUNG_THUCHIEN = c.String(maxLength: 2000),
                        GHICHU_THUCHIEN = c.String(maxLength: 2000),
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
            DropTable("BTSTC.PHF_KIENNGHI_KHONGSO");
        }
    }
}
