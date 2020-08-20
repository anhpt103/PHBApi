namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190615_UpdateTBL_PHF_DM_TIEUCHIBAOCAO_THEMINDAM_INNGHIENG_HIEUDN : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "INDAM", c => c.Decimal(precision: 10, scale: 0));
            AddColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "INNGHIENG", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "INNGHIENG");
            DropColumn("BTSTC.PHF_DM_TIEUCHIBAOCAO", "INDAM");
        }
    }
}
