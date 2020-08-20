namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_ADDTABLE_PHF_TEMP_DOTXUAT_DONVI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TEMP_DOTXUAT_DONVI",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_DVQHNS = c.String(maxLength: 50),
                        TEN_DVQHNS = c.String(maxLength: 200),
                        MA_CHUONG = c.String(maxLength: 50),
                        MA_LOAI = c.String(maxLength: 50),
                        MA_KHOAN = c.String(maxLength: 50),
                        MA_MUC = c.String(maxLength: 50),
                        MA_TIEUMUC = c.String(maxLength: 50),
                        SOTIEN = c.Decimal(nullable: false, precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_TEMP_DOTXUAT_DONVI");
        }
    }
}
