namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11122019_UPDATE_TABLE_PHB_L_PC_D_DETAIL_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHB_L_PC_D_DETAIL", "PC_KN", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHB_L_PC_D_DETAIL", "PC_KN");
        }
    }
}
