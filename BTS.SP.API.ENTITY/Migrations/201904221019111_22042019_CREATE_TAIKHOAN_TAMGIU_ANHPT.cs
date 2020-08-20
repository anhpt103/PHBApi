namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22042019_CREATE_TAIKHOAN_TAMGIU_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TAIKHOAN_TAMGIU_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHIEU = c.String(nullable: false, maxLength: 70),
                        MA_DOITUONG = c.String(maxLength: 50),
                        MASOTHUE = c.String(maxLength: 50),
                        SO_QUYETDINH_THU = c.String(maxLength: 70),
                        NGAY_QUYETDINH_THU = c.DateTime(),
                        GIATRI_QUYETDINH_THU = c.Decimal(precision: 18, scale: 2),
                        MA_NDKT = c.String(maxLength: 70),
                        SO_CHUNGTU = c.String(maxLength: 70),
                        NGAY_CHUNGTU = c.DateTime(),
                        NOP_TKTG = c.Decimal(precision: 18, scale: 2),
                        NOP_TRUCTIEP_NSNN = c.Decimal(precision: 18, scale: 2),
                        NGAY_NOP_NSNN = c.DateTime(),
                        NGAY_XULY_NOP_NSNN = c.DateTime(),
                        SO_CHUNGTU_NOP_NSNN = c.String(maxLength: 70),
                        SAPXEP = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_TAIKHOAN_TAMGIU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAPHIEU = c.String(nullable: false, maxLength: 70),
                        TUNAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DENNAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        MAPHONG = c.String(maxLength: 50),
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
            DropTable("BTSTC.PHF_TAIKHOAN_TAMGIU");
            DropTable("BTSTC.PHF_TAIKHOAN_TAMGIU_CHITIET");
        }
    }
}
