namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13082019_UPDATE_TBL_PHF_DM_BAOCAO_DONG_VUDQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "SOTHUTU_HIENTHI", c => c.String());
            AddColumn("BTSTC.PHF_DM_BAOCAO_DONG", "DINH_DANG", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "DINH_DANG");
            DropColumn("BTSTC.PHF_DM_BAOCAO_DONG", "SOTHUTU_HIENTHI");
        }
    }
}
