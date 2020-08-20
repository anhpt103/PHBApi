namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191115_ADD_2_TABLE_BC_NHAPDT_XA_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.BC_NHAPDT_XA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DBHC = c.String(nullable: false, maxLength: 10),
                        TEN_DBHC = c.String(maxLength: 255),
                        MA_DBHC_CHA = c.String(maxLength: 10),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LOAI_DT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(nullable: false, maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.BC_NHAPDT_XA_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_CHITIEU = c.String(nullable: false, maxLength: 10),
                        TEN_CHITIEU = c.String(nullable: false, maxLength: 500),
                        NSNN = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NSX = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DTPT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CTX = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.BC_NHAPDT_XA_DETAIL");
            DropTable("BTSTC.BC_NHAPDT_XA");
        }
    }
}
