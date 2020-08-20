namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21012018_ADD_PHF_PL03_PL04_XLKN_VUDQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL03_XLKN_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(maxLength: 100),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 50),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
                        VBCC_SO = c.String(maxLength: 100),
                        VBCC_NGAY = c.DateTime(),
                        VBCC_NOIDUNG = c.String(maxLength: 500),
                        NOIDUNG_CC_MOI = c.String(maxLength: 500),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL03_XLKN_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 50),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL04_XLKN_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_BAOCAO = c.String(maxLength: 100),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 50),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
                        VBCC_SO = c.String(maxLength: 100),
                        VBCC_NGAY = c.DateTime(),
                        VBCC_NOIDUNG = c.String(maxLength: 500),
                        NOIDUNG_CC_MOI = c.String(maxLength: 500),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "BTSTC.PHF_PL04_XLKN_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        STT_TIEUDE = c.String(maxLength: 50),
                        STT_CHA = c.String(maxLength: 50),
                        DONVI_DUOC_THANHTRA = c.String(maxLength: 100),
                        IS_BOLD = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IS_ITALIC = c.Decimal(nullable: false, precision: 10, scale: 0),
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
            DropTable("BTSTC.PHF_PL04_XLKN_TEMPLATE");
            DropTable("BTSTC.PHF_PL04_XLKN_CT");
            DropTable("BTSTC.PHF_PL03_XLKN_TEMPLATE");
            DropTable("BTSTC.PHF_PL03_XLKN_CT");
        }
    }
}
