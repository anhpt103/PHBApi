namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20032019addTblNhanDLXa_kien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHA_NHANDULIEU_XA",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAQHNS = c.String(maxLength: 10),
                        CHUONG = c.String(maxLength: 10),
                        MACTMT = c.String(maxLength: 10),
                        KHOAN = c.String(maxLength: 10),
                        TIEUMUC = c.String(maxLength: 10),
                        MANV = c.String(maxLength: 10),
                        SOTIEN = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LOAI = c.String(maxLength: 10),
                        MUC = c.String(maxLength: 10),
                        NHOM = c.String(maxLength: 10),
                        TIEUNHOM = c.String(maxLength: 10),
                        MA_KHOBAC = c.String(maxLength: 10),
                        MA_CAPNGANSACH = c.String(maxLength: 10),
                        MA_DBHC = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("BTSTC.PHA_NHANDULIEU_XA");
        }
    }
}
