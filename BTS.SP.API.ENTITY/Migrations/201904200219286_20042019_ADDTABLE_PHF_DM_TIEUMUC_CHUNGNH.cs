namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20042019_ADDTABLE_PHF_DM_TIEUMUC_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DM_TIEUMUC",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_TIEUMUC = c.String(maxLength: 6),
                        NGAY_HL = c.DateTime(nullable: false),
                        NGAY_HET_HL = c.DateTime(),
                        TEN_TIEUMUC = c.String(maxLength: 500),
                        TRANG_THAI = c.String(maxLength: 1),
                        MA_MUC = c.String(maxLength: 4),
                        GHI_CHU = c.String(maxLength: 500),
                        USER_NAME = c.String(maxLength: 20),
                        NGAY_PS = c.DateTime(),
                        NGAY_SD = c.DateTime(),
                        LOAI = c.String(maxLength: 10),
                        LOAI_KHOANTHU = c.String(maxLength: 10),
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
            DropTable("BTSTC.PHF_DM_TIEUMUC");
        }
    }
}
