namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22012019_update_length_STT_TIEUDE_PHF_BM_05TT_NSDP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "STT_TIEUDE", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_BM_05TT_NSDP", "STT_TIEUDE", c => c.String(maxLength: 5));
        }
    }
}
