namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _05032019_ADDTABLE_PHF_PL05_XLKN_CT_AND_PHF_PL05_XLKN_TEMPLATE_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_PL05_XLKN_CT",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_FILE = c.String(maxLength: 100),
                        MA_FILE_PK = c.String(maxLength: 200),
                        STT = c.String(maxLength: 200),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 200),
                        THOIGIAN_DKTK = c.String(maxLength: 500),
                        DOITUONG = c.String(maxLength: 500),
                        NOIDUNG = c.String(maxLength: 500),
                        TONGSONGUOI_DK = c.String(maxLength: 500),
                        CANBOPHONG_DK = c.String(maxLength: 500),
                        GHI_CHU = c.String(maxLength: 500),
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
                "BTSTC.PHF_PL05_XLKN_TEMPLATE",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 200),
                        STT_TIEUDE = c.String(maxLength: 5),
                        STT_CHA = c.String(maxLength: 200),
                        DOITUONG = c.String(maxLength: 500),
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
            DropTable("BTSTC.PHF_PL05_XLKN_TEMPLATE");
            DropTable("BTSTC.PHF_PL05_XLKN_CT");
        }
    }
}
