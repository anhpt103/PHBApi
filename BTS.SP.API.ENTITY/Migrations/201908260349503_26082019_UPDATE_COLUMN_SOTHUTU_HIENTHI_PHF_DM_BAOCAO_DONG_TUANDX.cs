namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26082019_UPDATE_COLUMN_SOTHUTU_HIENTHI_PHF_DM_BAOCAO_DONG_TUANDX : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "SOTHUTU_HIENTHI", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_DM_BAOCAO_DONG", "SOTHUTU_HIENTHI", c => c.String());
        }
    }
}
