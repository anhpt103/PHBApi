namespace BTS.SP.AUTHENTICATION.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27022020_ADDTABLE_DM_DBHC_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTAUTH.DM_DBHC",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MA_T = c.String(maxLength: 10),
                        MA_H = c.String(maxLength: 10),
                        MA_X = c.String(maxLength: 10),
                        MA_DBHC = c.String(maxLength: 500),
                        TEN_DBHC = c.String(maxLength: 500),
                        LOAI = c.Decimal(precision: 10, scale: 0),
                        MA_DBHC_CHA = c.String(maxLength: 10),
                        USER_NAME = c.String(maxLength: 20),
                        NGAY_PS = c.DateTime(),
                        NGAY_SD = c.DateTime(),
                        NGAY_HL = c.DateTime(nullable: false),
                        NGAY_HET_HL = c.DateTime(),
                        MA_THAMCHIEU = c.String(maxLength: 150),
                        CAN_CU = c.String(maxLength: 500),
                        VALID = c.Decimal(precision: 10, scale: 0),
                        TRANG_THAI = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("BTAUTH.DM_DBHC");
        }
    }
}
