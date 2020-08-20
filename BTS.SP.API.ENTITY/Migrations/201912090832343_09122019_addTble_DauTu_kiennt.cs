namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _09122019_addTble_DauTu_kiennt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_BAOCAO_DAUTU_DETAIL",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        STT = c.String(maxLength: 5),
                        NOIDUNG = c.String(maxLength: 250),
                        DIADIEM_MO_TK = c.String(maxLength: 250),
                        SEGMENT_5 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_6 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_7 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_8 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_9 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_10 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_11 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_12 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_13 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SEGMENT_14 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHA_BAOCAO_DAUTU",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        REFID = c.String(nullable: false, maxLength: 50),
                        MA_DVQHNS = c.String(maxLength: 10),
                        MA_DBHC = c.String(maxLength: 255),
                        TEN_DBHC = c.String(maxLength: 255),
                        MA_BAOCAO = c.String(nullable: false, maxLength: 20),
                        TEN_DVQHNS = c.String(maxLength: 255),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_BAOCAO_DAUTU");
            DropTable("BTSTC.PHA_BAOCAO_DAUTU_DETAIL");
        }
    }
}
