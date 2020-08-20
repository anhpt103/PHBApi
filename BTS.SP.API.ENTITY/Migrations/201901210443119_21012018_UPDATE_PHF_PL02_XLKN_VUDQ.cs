namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21012018_UPDATE_PHF_PL02_XLKN_VUDQ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 50));
            AlterColumn("BTSTC.PHF_PL02_XLKN_TEMPLATE", "STT_TIEUDE", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("BTSTC.PHF_PL02_XLKN_TEMPLATE", "STT_TIEUDE", c => c.String(maxLength: 5));
            AlterColumn("BTSTC.PHF_PL02_XLKN_CT", "STT_TIEUDE", c => c.String(maxLength: 5));
        }
    }
}
