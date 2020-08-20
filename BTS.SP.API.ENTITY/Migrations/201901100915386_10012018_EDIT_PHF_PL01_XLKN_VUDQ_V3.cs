namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10012018_EDIT_PHF_PL01_XLKN_VUDQ_V3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_PL01_XLKN", "TD_DANGDUTHAO_KL", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_PL01_XLKN", "TD_DANGDUTHAO_KL");
        }
    }
}
