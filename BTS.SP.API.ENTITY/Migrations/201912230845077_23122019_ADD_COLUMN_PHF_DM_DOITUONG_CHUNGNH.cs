namespace BTS.SP.API.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23122019_ADD_COLUMN_PHF_DM_DOITUONG_CHUNGNH : DbMigration
    {
        public override void Up()
        {
            AddColumn("BTSTC.PHF_DM_DOITUONG", "MA_DIABAN", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("BTSTC.PHF_DM_DOITUONG", "MA_DIABAN");
        }
    }
}
