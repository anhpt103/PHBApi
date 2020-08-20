namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190712_UPDATE_PHF_BM_FILE_TCXD_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_TCXD", "LOAI_BAOCAO", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_FILE_TCXD", "LOAI_BAOCAO");
        }
    }
}
