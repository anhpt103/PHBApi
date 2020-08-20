namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190615_AddTBL_PHF_DM_TIEUCHIBAOCAO_HIEUDN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "BTSTC.PHF_DM_TIEUCHIBAOCAO",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        MA_TIEUCHI = c.String(maxLength: 50),
                        TEN_TIEUCHI = c.String(nullable: false, maxLength: 500),
                        MA_CHA = c.String(maxLength: 50),
                        TEN_BAOCAO = c.String(maxLength: 50),
                        SAPXEP = c.String(maxLength: 50),
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
            DropTable("BTSTC.PHF_DM_TIEUCHIBAOCAO");
        }
    }
}
