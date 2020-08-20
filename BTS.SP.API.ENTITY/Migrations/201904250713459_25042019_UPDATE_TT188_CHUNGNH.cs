namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25042019_UPDATE_TT188_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT188_PL02", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT188_PL02", "TEN_FILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_TT188_PL03", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT188_PL03", "TEN_FILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_TT188", "TEN_FILE", c => c.String(maxLength: 100));
            DropColumn("BTSTC.PHF_TT188", "MA_FILE");
            DropColumn("BTSTC.PHF_TT188", "TENFILE");
        }
        
        public override void Down()
        {
            AddColumn("BTSTC.PHF_TT188", "TENFILE", c => c.String(maxLength: 100));
            AddColumn("BTSTC.PHF_TT188", "MA_FILE", c => c.String(maxLength: 200));
            DropColumn("BTSTC.PHF_TT188", "TEN_FILE");
            DropColumn("BTSTC.PHF_TT188_PL03", "TEN_FILE");
            DropColumn("BTSTC.PHF_TT188_PL03", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT188_PL02", "TEN_FILE");
            DropColumn("BTSTC.PHF_TT188_PL02", "MA_FILE_PK");
        }
    }
}
