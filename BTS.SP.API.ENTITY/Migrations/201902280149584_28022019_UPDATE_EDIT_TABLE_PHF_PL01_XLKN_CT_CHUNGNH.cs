namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28022019_UPDATE_EDIT_TABLE_PHF_PL01_XLKN_CT_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_PL01_XLKN_CT", "MA_FILE", c => c.String(maxLength: 100));
            DropColumn("BTSTC.PHF_PL01_XLKN_CT", "MA_BAOCAO");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_PL01_XLKN_CT", "MA_BAOCAO", c => c.String(maxLength: 100));
            DropColumn("BTSTC.PHF_PL01_XLKN_CT", "MA_FILE");
        }
    }
}
