namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190615_UpdateTBL_PHF_DM_TIEUCHIBAOCAO_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "TEN_TIEUCHI", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "TEN_TIEUCHI", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
