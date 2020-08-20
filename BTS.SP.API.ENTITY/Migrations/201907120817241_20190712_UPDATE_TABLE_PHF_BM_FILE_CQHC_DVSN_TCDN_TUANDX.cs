namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20190712_UPDATE_TABLE_PHF_BM_FILE_CQHC_DVSN_TCDN_TUANDX : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_FILE_CQHC", "LOAI_BAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_BM_FILE_DVSN", "LOAI_BAOCAO", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_BM_FILE_TCDN", "LOAI_BAOCAO", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_FILE_TCDN", "LOAI_BAOCAO");
            DropColumn("BTSTC.PHF_BM_FILE_DVSN", "LOAI_BAOCAO");
            DropColumn("BTSTC.PHF_BM_FILE_CQHC", "LOAI_BAOCAO");
        }
    }
}
