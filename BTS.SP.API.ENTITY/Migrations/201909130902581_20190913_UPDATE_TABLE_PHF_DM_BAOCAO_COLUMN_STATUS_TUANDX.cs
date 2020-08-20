namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190913_UPDATE_TABLE_PHF_DM_BAOCAO_COLUMN_STATUS_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO", "STATUS", c => c.Decimal(precision: 10, scale: 0));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO", "STATUS");
        }
    }
}
