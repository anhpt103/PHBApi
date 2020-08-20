namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _07042019_ADD_KIENNGHITHANHTRA_ANHPT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_UPDATE_KIENNGHI_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        TEN_DOITUONG = c.String(maxLength: 200),
                        MA_DOITUONG_CHA = c.String(maxLength: 50),
                        TEN_DOITUONG_CHA = c.String(maxLength: 200),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        THANG = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOP_NSNN_THUEGTGT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETNDN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEXNK = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUETN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_THUEKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN_CACKHOANKHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NOP_NSNN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GHITHU_GHICHI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_DUTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GIAM_QUYETTOAN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        THUVE_COPHAN_HOA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KIENNGHI_KHAC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TONGSO = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_UPDATE_XULYKIENNGHI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MABAOCAO = c.String(maxLength: 50),
                        TENBAOCAO = c.String(maxLength: 300),
                        MAPHONGBAN = c.String(maxLength: 50),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                        QUY = c.String(maxLength: 20),
                        NGAY = c.String(maxLength: 30),
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
            DropTable("BTSTC.PHF_UPDATE_XULYKIENNGHI");
            DropTable("BTSTC.PHF_UPDATE_KIENNGHI_CHITIET");
        }
    }
}
