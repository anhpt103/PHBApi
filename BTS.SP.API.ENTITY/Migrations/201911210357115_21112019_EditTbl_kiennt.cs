namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21112019_EditTbl_kiennt : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_COT", c => c.String(maxLength: 50));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "TRANG_THAI", c => c.String(maxLength: 10));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "LOAI_CHITIEU", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "GIA_TRI", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC_XA", c => c.String(maxLength: 20));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC_USER", c => c.String(maxLength: 20));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "CAP", c => c.Decimal(precision: 10, scale: 0));
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC", c => c.String(maxLength: 20));
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_CHITIEU", c => c.String(maxLength: 50));
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "TEN_CHITIEU");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NSNN");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NSX");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "DTPT");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "CTX");
            DropTable("BTSTC.BC_NHAPDT_XA");
        }
        
        public override void Down()
        {
            CreateTable(
                "BTSTC.BC_NHAPDT_XA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DBHC = c.String(nullable: false, maxLength: 10),
                        MA_PK = c.String(maxLength: 50),
                        TEN_DBHC = c.String(maxLength: 255),
                        MA_DBHC_CHA = c.String(maxLength: 10),
                        NAM_BC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        LOAI_DT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_TAO = c.DateTime(nullable: false),
                        NGUOI_TAO = c.String(maxLength: 150),
                        NGAY_SUA = c.DateTime(),
                        NGUOI_SUA = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "CTX", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "DTPT", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NSX", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NSNN", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "TEN_CHITIEU", c => c.String(nullable: false, maxLength: 500));
            AddColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_PK", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_CHITIEU", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC", c => c.String(nullable: false, maxLength: 10));
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "CAP");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC_USER");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_DBHC_XA");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "GIA_TRI");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "NAM");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "LOAI_CHITIEU");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "TRANG_THAI");
            DropColumn("BTSTC.BC_NHAPDT_XA_DETAIL", "MA_COT");
        }
    }
}
