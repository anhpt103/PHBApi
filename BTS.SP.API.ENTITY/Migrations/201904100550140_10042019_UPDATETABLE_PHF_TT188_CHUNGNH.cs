namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10042019_UPDATETABLE_PHF_TT188_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT188", "MA_FILE", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT188", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT188", "GIMFILE", c => c.String(maxLength: 500));
            AddColumn("BTSTC.PHF_TT188", "URLFILE", c => c.String(maxLength: 250));
            DropColumn("BTSTC.PHF_TT188", "DUONGDAN");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TT188", "DUONGDAN", c => c.String(maxLength: 300));
            DropColumn("BTSTC.PHF_TT188", "URLFILE");
            DropColumn("BTSTC.PHF_TT188", "GIMFILE");
            DropColumn("BTSTC.PHF_TT188", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT188", "MA_FILE");
        }
    }
}
