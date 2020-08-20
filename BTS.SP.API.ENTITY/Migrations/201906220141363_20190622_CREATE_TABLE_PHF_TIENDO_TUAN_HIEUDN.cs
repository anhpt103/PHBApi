namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190622_CREATE_TABLE_PHF_TIENDO_TUAN_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TIENDO_TUAN",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(nullable: false, maxLength: 50),
                        MA_PHIEU = c.String(maxLength: 50),
                        MA_DOITUONG = c.String(maxLength: 50),
                        LOAI_BAOCAO = c.String(maxLength: 50),
                        TUNGAY = c.DateTime(),
                        DENNGAY = c.DateTime(),
                        MAPHONG = c.String(maxLength: 50),
                        NOIDUNG = c.String(maxLength: 500),
                        DUKIEN = c.String(maxLength: 500),
                        TUAN = c.Decimal(precision: 10, scale: 0),
                        NAM = c.Decimal(nullable: false, precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_TIENDO_TUAN");
        }
    }
}
