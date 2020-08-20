namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21062019_ADDTABLE_PHF_DM_TCBC_TT03_BIEU01_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DM_TCBC_TT03_BIEU01",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        STT = c.String(maxLength: 50),
                        NOIDUNG = c.String(maxLength: 2000),
                        TEN_BAOCAO = c.String(maxLength: 50),
                        TRANG_THAI = c.String(maxLength: 50),
                        SAPXEP = c.String(maxLength: 50),
                        INDAM = c.Decimal(precision: 10, scale: 0),
                        INNGHIENG = c.Decimal(precision: 10, scale: 0),
                        I_CREATE_DATE = c.DateTime(),
                        I_CREATE_BY = c.String(maxLength: 50),
                        I_UPDATE_DATE = c.DateTime(),
                        I_UPDATE_BY = c.String(maxLength: 50),
                        I_STATE = c.String(maxLength: 50),
                        UNITCODE = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("BTSTC.PHF_TIENDO_TUAN", "NAM");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TIENDO_TUAN", "NAM", c => c.Decimal(nullable: false, precision: 10, scale: 0));
            DropTable("BTSTC.PHF_DM_TCBC_TT03_BIEU01");
        }
    }
}
