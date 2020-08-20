namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190916_UPDATE_TABLE_PHF_DM_BAOCAO_COLUMN_MA_PHONGBAN_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO", "MA_PHONGBAN", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO", "MA_PHONGBAN");
        }
    }
}
