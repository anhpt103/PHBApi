namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2020111_ADD_TABLE_PHF_GIAMSAT_DOAN_TT_TUANDX : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_GIAMSAT_DOAN_TT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MAQD = c.String(maxLength: 50),
                        NAM = c.String(maxLength: 50),
                        FILEDINHKEM = c.String(maxLength: 500),
                        URL = c.String(maxLength: 250),
                        MAPHONGBAN = c.String(maxLength: 50),
                        MADOITUONG = c.String(maxLength: 50),
                        NGAYLAP = c.DateTime(),
                        NGAYQDGS = c.DateTime(),
                        TOTRUONG = c.String(maxLength: 250),
                        THANHVIEN = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_GIAMSAT_DOAN_TT");
        }
    }
}
