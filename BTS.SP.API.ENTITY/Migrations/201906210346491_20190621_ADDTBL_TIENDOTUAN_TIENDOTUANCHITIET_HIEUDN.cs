namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190621_ADDTBL_TIENDOTUAN_TIENDOTUANCHITIET_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_TIENDO_TUAN_CHITIET",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(nullable: false, maxLength: 50),
                        INDEX = c.Decimal(nullable: false, precision: 10, scale: 0),
                        NOIDUNG_CHITIET = c.String(maxLength: 1000),
                        KETQUA_THANHTRA = c.String(maxLength: 1000),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropTable("BTSTC.PHF_TIENDO_TUAN_CHITIET");
        }
    }
}
