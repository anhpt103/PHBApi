namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _06052019_EDITTABLE_TT03_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TT03_BIEU1A", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1B", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1C", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1D", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1E", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1F", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1G", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU1H", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU2A", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU2B", "MA_FILE_PK", c => c.String(maxLength: 200));
            AddColumn("BTSTC.PHF_TT03_BIEU2C", "MA_FILE_PK", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TT03_BIEU2C", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU2B", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU2A", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1H", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1G", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1F", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1E", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1D", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1C", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1B", "MA_FILE_PK");
            DropColumn("BTSTC.PHF_TT03_BIEU1A", "MA_FILE_PK");
        }
    }
}
