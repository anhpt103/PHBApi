namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11012020_ADD_TABLE_PHF_CAPNHAT_KETLUAN_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_CAPNHAT_KETLUAN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_KLKT = c.String(maxLength: 200),
                        NGAY_LAP = c.DateTime(),
                        NGUOI_LAP = c.String(maxLength: 500),
                        MA_DOITUONG = c.String(maxLength: 50),
                        NAM = c.String(maxLength: 50),
                        TRUONG_DOAN = c.String(maxLength: 100),
                        SO_KLKT = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NGAY_KLKT = c.DateTime(),
                        DINH_KEMFILE = c.String(maxLength: 200),
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
            DropTable("BTSTC.PHF_CAPNHAT_KETLUAN");
        }
    }
}
