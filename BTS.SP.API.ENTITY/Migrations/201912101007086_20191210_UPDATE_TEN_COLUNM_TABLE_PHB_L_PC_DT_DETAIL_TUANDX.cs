namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20191210_UPDATE_TEN_COLUNM_TABLE_PHB_L_PC_DT_DETAIL_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_L_PC_DT_DETAIL", "PHB_L_PC_DT_REFID", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHB_L_PC_DT_DETAIL", "PHB_L_PC_UB_REFID");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHB_L_PC_DT_DETAIL", "PHB_L_PC_UB_REFID", c => c.String(maxLength: 50));
            DropColumn("BTSTC.PHB_L_PC_DT_DETAIL", "PHB_L_PC_DT_REFID");
        }
    }
}
