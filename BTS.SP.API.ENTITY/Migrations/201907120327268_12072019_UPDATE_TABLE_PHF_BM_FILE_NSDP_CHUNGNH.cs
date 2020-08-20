namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12072019_UPDATE_TABLE_PHF_BM_FILE_NSDP_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_NSDP", "LOAI_BAOCAO", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_FILE_NSDP", "LOAI_BAOCAO");
        }
    }
}
