namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1903_AddMaFile_NDTT_DUONGHH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_NDTHANHTRA", "TEN_FILE", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_NDTHANHTRA", "TEN_FILE");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_NDTHANHTRA", "MA_FILE");
        }
    }
}
