namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27082019_UPDATE_COLUMN_TABLE_PHF_TEMP_DOTXUAT_DONVI_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "MA_DIABAN", c => c.String(maxLength: 50));
            AddColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_DIABAN", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "TEN_DIABAN");
            DropColumn("BTSTC.PHF_TEMP_DOTXUAT_DONVI", "MA_DIABAN");
        }
    }
}
