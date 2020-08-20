namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14012019_EdittblPHF_BM_08TT_TCDN_ThanhND2ND : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_BM_08TT_TCDN", "DOITUONG_NO", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_BM_08TT_TCDN", "DOITUONG_NO");
        }
    }
}
